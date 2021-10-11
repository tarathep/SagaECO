namespace SagaMap.ActorEventHandlers
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Scripting;
    using SagaMap.Skill;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Tasks.Mob;
    using System;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="MobEventHandler" />.
    /// </summary>
    public class MobEventHandler : ActorEventHandler
    {
        /// <summary>
        /// Defines the mob.
        /// </summary>
        private ActorMob mob;

        /// <summary>
        /// Defines the AI.
        /// </summary>
        public MobAI AI;

        /// <summary>
        /// Defines the currentCall.
        /// </summary>
        private MobCallback currentCall;

        /// <summary>
        /// Defines the currentPC.
        /// </summary>
        private ActorPC currentPC;

        /// <summary>
        /// Defines the Dying.
        /// </summary>
        public event MobCallback Dying;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobEventHandler"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
        public MobEventHandler(ActorMob mob)
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
            if (aActor.type == ActorType.PC && !this.AI.Activated)
                this.AI.Start();
            if (aActor.type != ActorType.SHADOW || this.AI.Hate.Count == 0 || this.AI.Hate.ContainsKey(aActor.ActorID))
                return;
            this.AI.Hate.Add(aActor.ActorID, this.mob.MaxHP);
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
            if (dActor.type != ActorType.PC || !this.AI.Hate.ContainsKey(dActor.ActorID))
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
            this.AI.OnSeenSkillUse((SkillArg)args);
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
        /// The checkDropSpecial.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool checkDropSpecial()
        {
            if (this.AI.firstAttacker == null || this.AI.firstAttacker.Status == null)
                return false;
            foreach (Addition addition in this.AI.firstAttacker.Status.Additions.Values)
            {
                if (addition.GetType() == typeof(Knowledge) && ((Knowledge)addition).MobTypes.Contains(this.mob.BaseData.mobType))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The OnDie.
        /// </summary>
        public void OnDie()
        {
            this.OnDie(true);
        }

        /// <summary>
        /// The OnDie.
        /// </summary>
        /// <param name="loot">The loot<see cref="bool"/>.</param>
        public void OnDie(bool loot)
        {
            this.mob.Buff.Dead = true;
            if (this.mob.Status.Additions.ContainsKey("Rebone"))
            {
                this.mob.Buff.Dead = false;
                this.mob.HP = this.mob.MaxHP;
                SkillHandler.RemoveAddition((SagaDB.Actor.Actor)this.mob, "Rebone");
                this.AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.mob, false);
                SkillHandler.ApplyAddition((SagaDB.Actor.Actor)this.mob, (Addition)new Zombie((SagaDB.Actor.Actor)this.mob));
                this.mob.Status.undead = true;
                this.AI.DamageTable.Clear();
                this.AI.Hate.Clear();
                this.AI.firstAttacker = (SagaDB.Actor.Actor)null;
            }
            else
            {
                try
                {
                    if (this.Dying != null)
                    {
                        if (this.AI.lastAttacker.type == ActorType.PC)
                            this.RunCallback(this.Dying, (ActorPC)this.AI.lastAttacker);
                        else
                            this.RunCallback(this.Dying, (ActorPC)null);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                this.AI.Pause();
                if (loot)
                {
                    int num1 = SagaLib.Global.Random.Next(0, 10000);
                    int num2 = 0;
                    int num3 = 0;
                    bool flag1 = false;
                    bool flag2 = false;
                    if (this.mob.BaseData.stampDrop != null && (double)SagaLib.Global.Random.Next(0, 1000000) <= (double)this.mob.BaseData.stampDrop.Rate * (double)Singleton<Configuration>.Instance.StampDropRate / 100.0)
                    {
                        this.AI.map.AddItemDrop(this.mob.BaseData.stampDrop.ItemID, (string)null, (SagaDB.Actor.Actor)this.mob, false);
                        flag1 = true;
                    }
                    if ((!flag1 || Singleton<Configuration>.Instance.MultipleDrop) && this.checkDropSpecial())
                    {
                        foreach (MobData.DropData dropData in this.mob.BaseData.dropItemsSpecial)
                        {
                            if (!Singleton<Configuration>.Instance.MultipleDrop)
                            {
                                int num4 = num2 + dropData.Rate;
                                if (num1 >= num2 && num1 < num4)
                                {
                                    this.AI.map.AddItemDrop(dropData.ItemID, dropData.TreasureGroup, (SagaDB.Actor.Actor)this.mob, dropData.Party);
                                    flag2 = true;
                                }
                                num2 = num4;
                            }
                            else if ((double)SagaLib.Global.Random.Next(0, 10000) < (double)dropData.Rate * (double)Singleton<Configuration>.Instance.SpecialDropRate)
                            {
                                this.AI.map.AddItemDrop(dropData.ItemID, dropData.TreasureGroup, (SagaDB.Actor.Actor)this.mob, dropData.Party);
                                flag2 = true;
                            }
                        }
                    }
                    int num5 = 0;
                    num3 = 0;
                    if (!flag1 && !flag2 || Singleton<Configuration>.Instance.MultipleDrop)
                    {
                        foreach (MobData.DropData dropItem in this.mob.BaseData.dropItems)
                        {
                            if (!Singleton<Configuration>.Instance.MultipleDrop)
                            {
                                int num4 = num5 + dropItem.Rate;
                                if (num1 >= num5 && num1 < num4)
                                    this.AI.map.AddItemDrop(dropItem.ItemID, dropItem.TreasureGroup, (SagaDB.Actor.Actor)this.mob, dropItem.Party);
                                num5 = num4;
                            }
                            else if ((double)SagaLib.Global.Random.Next(0, 10000) < (double)dropItem.Rate * (double)Singleton<Configuration>.Instance.GlobalDropRate)
                                this.AI.map.AddItemDrop(dropItem.ItemID, dropItem.TreasureGroup, (SagaDB.Actor.Actor)this.mob, dropItem.Party);
                        }
                    }
                }
                this.mob.ClearTaskAddition();
                this.AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.mob, false);
                DeleteCorpse deleteCorpse = new DeleteCorpse(this.mob);
                this.mob.Tasks.Add("DeleteCorpse", (MultiRunTask)deleteCorpse);
                deleteCorpse.Activate();
                if (this.AI.SpawnDelay != 0)
                {
                    Respawn respawn = new Respawn(this.mob, this.AI.SpawnDelay);
                    this.mob.Tasks.Add("Respawn", (MultiRunTask)respawn);
                    respawn.Activate();
                }
                this.AI.firstAttacker = (SagaDB.Actor.Actor)null;
                this.mob.Status.attackingActors.Clear();
                this.AI.DamageTable.Clear();
                this.mob.VisibleActors.Clear();
                if (this.AI.Mode.Symbol || this.AI.Mode.SymbolTrash)
                    Singleton<ODWarManager>.Instance.SymbolDown(this.mob.MapID, this.mob);
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
            this.AI.OnSeenSkillUse((SkillArg)args);
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

        /// <summary>
        /// The RunCallback.
        /// </summary>
        /// <param name="callback">The callback<see cref="MobCallback"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void RunCallback(MobCallback callback, ActorPC pc)
        {
            this.currentCall = callback;
            this.currentPC = pc;
            new Thread(new ThreadStart(this.Run)).Start();
        }

        /// <summary>
        /// The Run.
        /// </summary>
        private void Run()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (this.currentCall != null)
                {
                    if (this.currentPC != null)
                        this.currentCall(this, this.currentPC);
                    else if (this.AI.map.Creator != null)
                        this.currentCall(this, this.AI.map.Creator);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
