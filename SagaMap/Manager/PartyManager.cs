namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Dungeon;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Server;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="PartyManager" />.
    /// </summary>
    public class PartyManager : Singleton<PartyManager>
    {
        /// <summary>
        /// Defines the partys.
        /// </summary>
        private Dictionary<uint, SagaDB.Party.Party> partys = new Dictionary<uint, SagaDB.Party.Party>();

        /// <summary>
        /// The GetParty.
        /// </summary>
        /// <param name="pattern">The pattern<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Party.Party"/>.</returns>
        public SagaDB.Party.Party GetParty(uint pattern)
        {
            if (pattern == 0U)
                return (SagaDB.Party.Party)null;
            SagaDB.Party.Party party;
            if (this.partys.ContainsKey(pattern))
            {
                party = this.partys[pattern];
            }
            else
            {
                party = new SagaDB.Party.Party();
                party.ID = pattern;
                this.partys.Add(pattern, party);
                if (party.Name == null)
                {
                    party = MapServer.charDB.GetParty(party.ID);
                    if (party == null)
                    {
                        Logger.ShowDebug("Party with ID:" + (object)pattern + " not found!", Logger.defaultlogger);
                        this.partys.Remove(pattern);
                        return (SagaDB.Party.Party)null;
                    }
                    this.partys[party.ID] = party;
                }
                foreach (byte index in party.Members.Keys.ToArray<byte>())
                {
                    MapClient client = MapClientManager.Instance.FindClient(party.Members[index]);
                    if (client != null)
                    {
                        party.Members[index] = client.Character;
                        if ((int)party.Leader.CharID == (int)client.Character.CharID)
                            party.Leader = client.Character;
                    }
                }
            }
            return party;
        }

        /// <summary>
        /// The GetParty.
        /// </summary>
        /// <param name="pattern">The pattern<see cref="SagaDB.Party.Party"/>.</param>
        /// <returns>The <see cref="SagaDB.Party.Party"/>.</returns>
        public SagaDB.Party.Party GetParty(SagaDB.Party.Party pattern)
        {
            if (pattern == null || pattern.ID == 0U)
                return (SagaDB.Party.Party)null;
            SagaDB.Party.Party party1;
            if (this.partys.ContainsKey(pattern.ID))
            {
                party1 = this.partys[pattern.ID];
            }
            else
            {
                SagaDB.Party.Party party2 = pattern;
                this.partys.Add(pattern.ID, pattern);
                if (party2.Name == null)
                {
                    party1 = MapServer.charDB.GetParty(party2.ID);
                    if (party1 == null)
                    {
                        Logger.ShowDebug("Party with ID:" + (object)pattern.ID + " not found!", Logger.defaultlogger);
                        this.partys.Remove(pattern.ID);
                        return (SagaDB.Party.Party)null;
                    }
                    this.partys[party1.ID] = party1;
                }
                else
                    party1 = pattern;
                foreach (byte index in party1.Members.Keys.ToArray<byte>())
                {
                    MapClient client = MapClientManager.Instance.FindClient(party1.Members[index]);
                    if (client != null)
                    {
                        party1.Members[index] = client.Character;
                        if ((int)party1.Leader.CharID == (int)client.Character.CharID)
                            party1.Leader = client.Character;
                    }
                }
            }
            return party1;
        }

        /// <summary>
        /// The PlayerOnline.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void PlayerOnline(SagaDB.Party.Party party, ActorPC pc)
        {
            if (party == null)
                return;
            if (!party.IsMember(pc))
            {
                pc.Party = (SagaDB.Party.Party)null;
            }
            else
            {
                party.MemberOnline(pc);
                foreach (ActorPC pc1 in party.Members.Values)
                {
                    if (pc1 != pc && pc1.Online)
                        MapClient.FromActorPC(pc1).SendPartyMemberInfo(pc);
                }
            }
        }

        /// <summary>
        /// The PlayerOffline.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void PlayerOffline(SagaDB.Party.Party party, ActorPC pc)
        {
            if (party == null)
                return;
            pc.Online = false;
            foreach (ActorPC pc1 in party.Members.Values)
            {
                if (pc1 != pc && pc1.Online)
                    MapClient.FromActorPC(pc1).SendPartyMemberState(pc);
            }
        }

        /// <summary>
        /// The UpdatePartyInfo.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void UpdatePartyInfo(SagaDB.Party.Party party)
        {
            if (party == null)
                return;
            foreach (ActorPC pc in party.Members.Values)
            {
                if (pc.Online)
                    MapClient.FromActorPC(pc).SendPartyInfo();
            }
        }

        /// <summary>
        /// The UpdateMemberPosition.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void UpdateMemberPosition(SagaDB.Party.Party party, ActorPC pc)
        {
            if (party == null)
                return;
            foreach (ActorPC pc1 in party.Members.Values)
            {
                if (pc1 != pc && pc1.Online)
                    MapClient.FromActorPC(pc1).SendPartyMemberPosition(pc);
            }
        }

        /// <summary>
        /// The UpdateMemberDungeonPosition.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void UpdateMemberDungeonPosition(SagaDB.Party.Party party, ActorPC pc)
        {
            if (party == null)
                return;
            foreach (ActorPC pc1 in party.Members.Values)
            {
                if (pc1 != pc && pc1.Online)
                {
                    MapClient.FromActorPC(pc1).SendPartyMemberDeungeonPosition(pc);
                    MapClient.FromActorPC(pc).SendPartyMemberDeungeonPosition(pc1);
                }
            }
        }

        /// <summary>
        /// The UpdateMemberHPMPSP.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void UpdateMemberHPMPSP(SagaDB.Party.Party party, ActorPC pc)
        {
            if (party == null)
                return;
            foreach (ActorPC pc1 in party.Members.Values)
            {
                if (pc1 != pc && pc1.Online && (int)pc1.MapID == (int)pc.MapID)
                    MapClient.FromActorPC(pc1).SendPartyMemberHPMPSP(pc);
            }
        }

        /// <summary>
        /// The UpdatePartyName.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void UpdatePartyName(SagaDB.Party.Party party)
        {
            if (party == null)
                return;
            MapServer.charDB.SaveParty(party);
            foreach (ActorPC pc in party.Members.Values)
            {
                if (pc.Online)
                    MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc, true);
            }
        }

        /// <summary>
        /// The PartyChat.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="content">The content<see cref="string"/>.</param>
        public void PartyChat(SagaDB.Party.Party party, ActorPC pc, string content)
        {
            if (party == null)
                return;
            foreach (ActorPC pc1 in party.Members.Values)
            {
                if (pc1.Online)
                    MapClient.FromActorPC(pc1).SendChatParty(pc.Name, content);
            }
        }

        /// <summary>
        /// The CreateParty.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="SagaDB.Party.Party"/>.</returns>
        public SagaDB.Party.Party CreateParty(ActorPC pc)
        {
            SagaDB.Party.Party party = new SagaDB.Party.Party();
            party.Name = Singleton<LocalManager>.Instance.Strings.PARTY_NEW_NAME;
            MapServer.charDB.NewParty(party);
            this.partys.Add(party.ID, party);
            party.Leader = pc;
            this.AddMember(party, pc);
            pc.Party = party;
            return party;
        }

        /// <summary>
        /// The AddMember.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void AddMember(SagaDB.Party.Party party, ActorPC pc)
        {
            if (party == null || party.IsMember(pc))
                return;
            int num = (int)party.NewMember(pc);
            pc.Party = party;
            if (pc.Online)
                MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc, true);
            MapServer.charDB.SaveParty(party);
            this.UpdatePartyInfo(party);
        }

        /// <summary>
        /// The DeleteMember.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="uint"/>.</param>
        /// <param name="reason">The reason<see cref="SSMG_PARTY_DELETE.Result"/>.</param>
        public void DeleteMember(SagaDB.Party.Party party, uint pc, SSMG_PARTY_DELETE.Result reason)
        {
            if (party == null || !party.IsMember(pc))
                return;
            foreach (ActorPC pc1 in party.Members.Values)
            {
                if ((int)pc1.CharID == (int)pc)
                {
                    if (pc1.Online)
                    {
                        MapClient.FromActorPC(pc1).SendPartyMeDelete(reason);
                        pc1.Party = (SagaDB.Party.Party)null;
                        MapClient.FromActorPC(pc1).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc1, false);
                    }
                    pc1.Party = (SagaDB.Party.Party)null;
                }
                else if (pc1.Online)
                    MapClient.FromActorPC(pc1).SendPartyMemberDelete(pc);
            }
            party.DeleteMemeber(pc);
            MapServer.charDB.SaveParty(party);
            if (party.Members.Count != 1)
                return;
            this.PartyDismiss(party);
        }

        /// <summary>
        /// The PartyDismiss.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void PartyDismiss(SagaDB.Party.Party party)
        {
            if (party == null)
                return;
            if (party.Leader.DungeonID != 0U)
                Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(party.Leader.DungeonID).Destory(DestroyType.PartyDismiss);
            foreach (ActorPC pc in party.Members.Values)
            {
                try
                {
                    if (pc.Online)
                    {
                        MapClient.FromActorPC(pc).SendPartyMeDelete(SSMG_PARTY_DELETE.Result.DISMISSED);
                        pc.Party = (SagaDB.Party.Party)null;
                        MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PARTY_NAME_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc, false);
                    }
                }
                catch
                {
                }
            }
            MapServer.charDB.DeleteParty(party);
            if (!this.partys.ContainsKey(party.ID))
                return;
            this.partys.Remove(party.ID);
        }
    }
}
