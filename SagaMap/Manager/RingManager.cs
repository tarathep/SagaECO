namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaDB.Ring;
    using SagaLib;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Server;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="RingManager" />.
    /// </summary>
    public class RingManager : Singleton<RingManager>
    {
        /// <summary>
        /// Defines the rings.
        /// </summary>
        private Dictionary<uint, SagaDB.Ring.Ring> rings = new Dictionary<uint, SagaDB.Ring.Ring>();

        /// <summary>
        /// The GetRing.
        /// </summary>
        /// <param name="pattern">The pattern<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Ring.Ring"/>.</returns>
        public SagaDB.Ring.Ring GetRing(uint pattern)
        {
            if (pattern == 0U)
                return (SagaDB.Ring.Ring)null;
            SagaDB.Ring.Ring ring;
            if (this.rings.ContainsKey(pattern))
            {
                ring = this.rings[pattern];
            }
            else
            {
                ring = new SagaDB.Ring.Ring();
                ring.ID = pattern;
                this.rings.Add(pattern, ring);
                if (ring.Name == null)
                {
                    ring = MapServer.charDB.GetRing(ring.ID);
                    if (ring == null)
                    {
                        Logger.ShowDebug("Ring with ID:" + (object)pattern + " not found!", Logger.defaultlogger);
                        this.rings.Remove(pattern);
                        return (SagaDB.Ring.Ring)null;
                    }
                    this.rings[ring.ID] = ring;
                }
                foreach (int index in ring.Members.Keys.ToArray<int>())
                {
                    MapClient client = MapClientManager.Instance.FindClient(ring.Members[index]);
                    if (client != null)
                    {
                        ring.Members[index] = client.Character;
                        if ((int)ring.Leader.CharID == (int)client.Character.CharID)
                            ring.Leader = client.Character;
                    }
                }
            }
            return ring;
        }

        /// <summary>
        /// The GetRing.
        /// </summary>
        /// <param name="pattern">The pattern<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <returns>The <see cref="SagaDB.Ring.Ring"/>.</returns>
        public SagaDB.Ring.Ring GetRing(SagaDB.Ring.Ring pattern)
        {
            if (pattern == null || pattern.ID == 0U)
                return (SagaDB.Ring.Ring)null;
            SagaDB.Ring.Ring ring1;
            if (this.rings.ContainsKey(pattern.ID))
            {
                ring1 = this.rings[pattern.ID];
            }
            else
            {
                SagaDB.Ring.Ring ring2 = pattern;
                this.rings.Add(pattern.ID, pattern);
                if (ring2.Name == null)
                {
                    ring1 = MapServer.charDB.GetRing(ring2.ID);
                    if (ring1 == null)
                    {
                        Logger.ShowDebug("Ring with ID:" + (object)pattern.ID + " not found!", Logger.defaultlogger);
                        this.rings.Remove(pattern.ID);
                        return (SagaDB.Ring.Ring)null;
                    }
                    this.rings[ring1.ID] = ring1;
                }
                else
                    ring1 = pattern;
                foreach (int index in ring1.Members.Keys.ToArray<int>())
                {
                    MapClient client = MapClientManager.Instance.FindClient(ring1.Members[index]);
                    if (client != null)
                    {
                        ring1.Members[index] = client.Character;
                        if ((int)ring1.Leader.CharID == (int)client.Character.CharID)
                            ring1.Leader = client.Character;
                    }
                }
            }
            return ring1;
        }

        /// <summary>
        /// The PlayerOnline.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void PlayerOnline(SagaDB.Ring.Ring ring, ActorPC pc)
        {
            if (ring == null)
                return;
            if (!ring.IsMember(pc))
            {
                pc.Ring = (SagaDB.Ring.Ring)null;
            }
            else
            {
                ring.MemberOnline(pc);
                foreach (ActorPC pc1 in ring.Members.Values.ToArray<ActorPC>())
                {
                    if (pc1 != pc && pc1.Online)
                        MapClient.FromActorPC(pc1).SendRingMemberInfo(pc);
                }
            }
        }

        /// <summary>
        /// The PlayerOffline.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void PlayerOffline(SagaDB.Ring.Ring ring, ActorPC pc)
        {
            if (ring == null)
                return;
            pc.Online = false;
            foreach (ActorPC pc1 in ring.Members.Values.ToArray<ActorPC>())
            {
                if (pc1 != pc && pc1.Online)
                    MapClient.FromActorPC(pc1).SendRingMemberState(pc);
            }
        }

        /// <summary>
        /// The UpdateRingInfo.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="reason">The reason<see cref="SSMG_RING_INFO.Reason"/>.</param>
        public void UpdateRingInfo(SagaDB.Ring.Ring ring, SSMG_RING_INFO.Reason reason)
        {
            if (ring == null)
                return;
            foreach (ActorPC pc in ring.Members.Values.ToArray<ActorPC>())
            {
                if (pc.Online)
                    MapClient.FromActorPC(pc).SendRingInfo(reason);
            }
            if (reason != SSMG_RING_INFO.Reason.UPDATED)
                return;
            MapServer.charDB.SaveRing(ring, false);
        }

        /// <summary>
        /// The RingChat.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="content">The content<see cref="string"/>.</param>
        public void RingChat(SagaDB.Ring.Ring ring, ActorPC pc, string content)
        {
            if (ring == null)
                return;
            foreach (ActorPC pc1 in ring.Members.Values.ToArray<ActorPC>())
            {
                if (pc1.Online)
                    MapClient.FromActorPC(pc1).SendChatRing(pc.Name, content);
            }
        }

        /// <summary>
        /// The CreateRing.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="SagaDB.Ring.Ring"/>.</returns>
        public SagaDB.Ring.Ring CreateRing(ActorPC pc, string name)
        {
            SagaDB.Ring.Ring ring = new SagaDB.Ring.Ring();
            ring.Name = name;
            MapServer.charDB.NewRing(ring);
            if (ring.ID == uint.MaxValue)
                return (SagaDB.Ring.Ring)null;
            this.rings.Add(ring.ID, ring);
            ring.Leader = pc;
            this.AddMember(ring, pc);
            pc.Ring = ring;
            return ring;
        }

        /// <summary>
        /// The AddMember.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void AddMember(SagaDB.Ring.Ring ring, ActorPC pc)
        {
            if (ring == null || ring.IsMember(pc))
                return;
            int index = ring.NewMember(pc);
            pc.Ring = ring;
            if (pc.Online)
                MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.RING_NAME_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc, true);
            if (ring.MemberCount > 1)
            {
                this.UpdateRingInfo(ring, SSMG_RING_INFO.Reason.JOIN);
            }
            else
            {
                ring.Rights[index].SetValue(RingRight.RingMaster, true);
                ring.Rights[index].SetValue(RingRight.AddRight, true);
                ring.Rights[index].SetValue(RingRight.KickRight, true);
                this.UpdateRingInfo(ring, SSMG_RING_INFO.Reason.CREATE);
            }
            MapServer.charDB.SaveRing(ring, true);
        }

        /// <summary>
        /// The DeleteMember.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="reason">The reason<see cref="SSMG_RING_QUIT.Reasons"/>.</param>
        public void DeleteMember(SagaDB.Ring.Ring ring, ActorPC pc, SSMG_RING_QUIT.Reasons reason)
        {
            if (ring == null || !ring.IsMember(pc))
                return;
            foreach (ActorPC pc1 in ring.Members.Values.ToArray<ActorPC>())
            {
                if (pc1.Online)
                {
                    if ((int)pc1.CharID == (int)pc.CharID)
                    {
                        if (pc1.Online)
                        {
                            MapClient.FromActorPC(pc1).SendRingMeDelete(reason);
                            pc1.Ring = (SagaDB.Ring.Ring)null;
                            MapClient.FromActorPC(pc1).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.RING_NAME_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc1, false);
                        }
                        pc1.Ring = (SagaDB.Ring.Ring)null;
                    }
                    else
                        MapClient.FromActorPC(pc1).SendRingMemberDelete(pc);
                }
            }
            ring.DeleteMemeber(pc);
            MapServer.charDB.SaveRing(ring, true);
        }

        /// <summary>
        /// The RingDismiss.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        public void RingDismiss(SagaDB.Ring.Ring ring)
        {
            if (ring == null)
                return;
            foreach (ActorPC pc in ring.Members.Values.ToArray<ActorPC>())
            {
                if (pc.Online)
                {
                    MapClient.FromActorPC(pc).SendRingMeDelete(SSMG_RING_QUIT.Reasons.DISSOLVE);
                    pc.Ring = (SagaDB.Ring.Ring)null;
                    MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.RING_NAME_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc, false);
                }
            }
            MapServer.charDB.DeleteRing(ring);
            if (!this.rings.ContainsKey(ring.ID))
                return;
            this.rings.Remove(ring.ID);
        }

        /// <summary>
        /// The SetMemberRight.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="pc">The pc<see cref="uint"/>.</param>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void SetMemberRight(SagaDB.Ring.Ring ring, uint pc, int value)
        {
            if (ring == null || !ring.IsMember(pc))
                return;
            ring.Rights[ring.IndexOf(pc)].Value = value;
            foreach (ActorPC pc1 in ring.Members.Values.ToArray<ActorPC>())
            {
                SSMG_RING_RIGHT_UPDATE ssmgRingRightUpdate = new SSMG_RING_RIGHT_UPDATE();
                ssmgRingRightUpdate.CharID = pc;
                ssmgRingRightUpdate.Right = ring.Rights[ring.IndexOf(pc)].Value;
                if (pc1.Online)
                    MapClient.FromActorPC(pc1).netIO.SendPacket((Packet)ssmgRingRightUpdate);
            }
        }
    }
}
