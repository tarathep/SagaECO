namespace SagaMap.ActorEventHandlers
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Packets.Client;
    using SagaMap.Tasks.Mob;
    using System;

    /// <summary>
    /// Defines the <see cref="PetEventHandler" />.
    /// </summary>
    public class PetEventHandler : ActorEventHandler
    {
        /// <summary>
        /// Defines the mob.
        /// </summary>
        private ActorPet mob;

        /// <summary>
        /// Defines the AI.
        /// </summary>
        public MobAI AI;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetEventHandler"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="ActorPet"/>.</param>
        public PetEventHandler(ActorPet mob)
        {
            this.mob = mob;
            this.AI = new MobAI((SagaDB.Actor.Actor)mob);
        }

        /// <summary>
        /// The OnActorAppears.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorAppears(SagaDB.Actor.Actor aActor)
        {
            if (!this.mob.VisibleActors.Contains(aActor.ActorID))
                this.mob.VisibleActors.Add(aActor.ActorID);
            if ((int)aActor.ActorID != (int)this.mob.Owner.ActorID || this.mob.type == ActorType.SHADOW || this.AI.Hate.ContainsKey(aActor.ActorID))
                return;
            this.AI.Hate.Add(aActor.ActorID, 1U);
        }

        /// <summary>
        /// The OnActorChangeEquip.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeEquip(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorChat.
        /// </summary>
        /// <param name="cActor">The cActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChat(SagaDB.Actor.Actor cActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorDisappears.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorDisappears(SagaDB.Actor.Actor dActor)
        {
            if (this.mob.VisibleActors.Contains(dActor.ActorID))
                this.mob.VisibleActors.Remove(dActor.ActorID);
            if (dActor.type != ActorType.PC || (int)dActor.ActorID == (int)this.mob.Owner.ActorID || !this.AI.Hate.ContainsKey(dActor.ActorID))
                return;
            this.AI.Hate.Remove(dActor.ActorID);
        }

        /// <summary>
        /// The OnActorSkillUse.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorSkillUse(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorStartsMoving.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void OnActorStartsMoving(SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
        {
        }

        /// <summary>
        /// The OnActorStopsMoving.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void OnActorStopsMoving(SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
        {
        }

        /// <summary>
        /// The OnCreate.
        /// </summary>
        /// <param name="success">The success<see cref="bool"/>.</param>
        public void OnCreate(bool success)
        {
        }

        /// <summary>
        /// The OnActorChangeEmotion.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeEmotion(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorChangeMotion.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeMotion(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnDelete.
        /// </summary>
        public void OnDelete()
        {
            this.mob.VisibleActors.Clear();
            this.AI.Pause();
        }

        /// <summary>
        /// The OnCharInfoUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnCharInfoUpdate(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The OnPlayerSizeChange.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnPlayerSizeChange(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The OnDie.
        /// </summary>
        public void OnDie()
        {
            this.mob.Buff.Dead = true;
            this.mob.ClearTaskAddition();
            if (this.mob.type != ActorType.SHADOW)
            {
                PCEventHandler e = (PCEventHandler)this.mob.Owner.e;
                e.Client.DeletePet();
                CSMG_ITEM_MOVE p = new CSMG_ITEM_MOVE();
                p.data = new byte[11];
                if (!this.mob.Owner.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    return;
                SagaDB.Item.Item equipment = this.mob.Owner.Inventory.Equipments[EnumEquipSlot.PET];
                if (equipment.Durability != (ushort)0)
                    --equipment.Durability;
                e.Client.SendItemInfo(equipment);
                e.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.PET_FRIENDLY_DOWN, (object)this.mob.Name));
                e.OnShowEffect((SagaDB.Actor.Actor)e.Client.Character, (MapEventArgs)new EffectArg()
                {
                    actorID = e.Client.Character.ActorID,
                    effectID = 8044U
                });
                p.InventoryID = equipment.Slot;
                p.Target = ContainerType.BODY;
                p.Count = (ushort)1;
                e.Client.OnItemMove(p);
            }
            else
            {
                this.mob.Owner.Slave.Remove((SagaDB.Actor.Actor)this.mob);
                this.AI.Pause();
                DeleteCorpse deleteCorpse = new DeleteCorpse((ActorMob)this.mob);
                this.mob.Tasks.Add("DeleteCorpse", (MultiRunTask)deleteCorpse);
                deleteCorpse.Activate();
                if (this.mob.Tasks.ContainsKey("Shadow"))
                {
                    this.mob.Tasks["Shadow"].Deactivate();
                    this.mob.Tasks.Remove("Shadow");
                }
            }
        }

        /// <summary>
        /// The OnKick.
        /// </summary>
        public void OnKick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnMapLoaded.
        /// </summary>
        public void OnMapLoaded()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnReSpawn.
        /// </summary>
        public void OnReSpawn()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnSendMessage.
        /// </summary>
        /// <param name="from">The from<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public void OnSendMessage(string from, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnSendWhisper.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="flag">The flag<see cref="byte"/>.</param>
        public void OnSendWhisper(string name, string message, byte flag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnTeleport.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void OnTeleport(short x, short y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnAttack.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnAttack(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnHPMPSPUpdate.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnHPMPSPUpdate(SagaDB.Actor.Actor sActor)
        {
        }

        /// <summary>
        /// The OnPlayerChangeStatus.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnPlayerChangeStatus(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorChangeBuff.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorChangeBuff(SagaDB.Actor.Actor sActor)
        {
        }

        /// <summary>
        /// The OnLevelUp.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnLevelUp(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnPlayerMode.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnPlayerMode(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The OnShowEffect.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnShowEffect(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorPossession.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorPossession(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorPartyUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorPartyUpdate(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorSpeedChange.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorSpeedChange(SagaDB.Actor.Actor mActor)
        {
        }

        /// <summary>
        /// The OnSignUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnSignUpdate(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The PropertyUpdate.
        /// </summary>
        /// <param name="arg">The arg<see cref="UpdateEvent"/>.</param>
        /// <param name="para">The para<see cref="int"/>.</param>
        public void PropertyUpdate(UpdateEvent arg, int para)
        {
            if (arg != UpdateEvent.SPEED)
                return;
            this.AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.mob, true);
        }

        /// <summary>
        /// The PropertyRead.
        /// </summary>
        /// <param name="arg">The arg<see cref="UpdateEvent"/>.</param>
        public void PropertyRead(UpdateEvent arg)
        {
        }

        /// <summary>
        /// The OnActorRingUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorRingUpdate(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorWRPRankingUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorWRPRankingUpdate(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorChangeAttackType.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorChangeAttackType(ActorPC aActor)
        {
        }
    }
}
