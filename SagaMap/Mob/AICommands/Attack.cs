namespace SagaMap.Mob.AICommands
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Skill;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Attack" />.
    /// </summary>
    public class Attack : AICommand
    {
        /// <summary>
        /// Defines the counter.
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Defines the status.
        /// </summary>
        private CommandStatus status;

        /// <summary>
        /// Defines the mob.
        /// </summary>
        private MobAI mob;

        /// <summary>
        /// Defines the dest.
        /// </summary>
        private SagaDB.Actor.Actor dest;

        /// <summary>
        /// Defines the attacking.
        /// </summary>
        private bool attacking;

        /// <summary>
        /// Defines the attacktask.
        /// </summary>
        private MobAttack attacktask;

        /// <summary>
        /// Defines the active.
        /// </summary>
        public bool active;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private short x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private short y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Attack"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="MobAI"/>.</param>
        public Attack(MobAI mob)
        {
            this.mob = mob;
            this.Status = CommandStatus.INIT;
            int num = 0;
            if (this.mob.Mob.type == ActorType.MOB)
                num = (int)((ActorMob)this.mob.Mob).BaseData.aspd;
            if (this.mob.Mob.type == ActorType.PET)
                num = (int)((ActorMob)this.mob.Mob).BaseData.aspd;
            if (this.mob.Mob.type == ActorType.SHADOW || this.mob.Mob.type == ActorType.GOLEM || this.mob.Mob.type == ActorType.PC)
                num = (int)this.mob.Mob.Status.aspd;
            this.attacktask = new MobAttack(mob, this.dest);
            this.x = mob.Mob.X;
            this.y = mob.Mob.Y;
        }

        /// <summary>
        /// The GetName.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName()
        {
            return nameof(Attack);
        }

        /// <summary>
        /// The CurrentTarget.
        /// </summary>
        /// <returns>The <see cref="SagaDB.Actor.Actor"/>.</returns>
        private SagaDB.Actor.Actor CurrentTarget()
        {
            uint num1 = 0;
            uint num2 = 0;
            SagaDB.Actor.Actor actor = (SagaDB.Actor.Actor)null;
            uint[] array = new uint[this.mob.Hate.Keys.Count];
            this.mob.Hate.Keys.CopyTo(array, 0);
            for (uint index = 0; (long)index < (long)this.mob.Hate.Keys.Count; ++index)
            {
                if (array[(uint)index] != 0U && (int)array[(uint)index] != (int)this.mob.Mob.ActorID && (this.mob.Master == null || ((int)array[(uint)index] != (int)this.mob.Master.ActorID || this.mob.Hate.Count <= 1)) && num2 < this.mob.Hate[array[(uint)index]])
                {
                    num2 = this.mob.Hate[array[(uint)index]];
                    num1 = array[(uint)index];
                    actor = this.mob.map.GetActor(num1);
                    if (actor == null)
                    {
                        this.mob.Hate.Remove(num1);
                        num1 = 0U;
                        num2 = 0U;
                        this.active = false;
                        index = 0U;
                    }
                    else if (actor.Status.Additions.ContainsKey("Hiding"))
                        this.mob.Hate.Remove(num1);
                    else if (actor.Status.Additions.ContainsKey("IAmTree"))
                    {
                        this.mob.Hate.Remove(num1);
                    }
                    else
                    {
                        this.active = true;
                        if (actor.type == ActorType.PC && this.mob.Mob.type != ActorType.PET && ((ActorPC)actor).PossessionTarget != 0U)
                        {
                            this.mob.Hate.Remove(num1);
                            num1 = 0U;
                            num2 = 0U;
                            this.active = false;
                            index = 0U;
                        }
                    }
                }
            }
            if (num1 != 0U)
            {
                actor = this.mob.map.GetActor(num1);
                if (actor != null && actor.HP == 0U)
                {
                    this.mob.Hate.Remove(actor.ActorID);
                    num1 = 0U;
                    this.active = false;
                }
            }
            if (num1 == 0U)
            {
                this.active = false;
                return (SagaDB.Actor.Actor)null;
            }
            if (this.dest != null && (int)this.dest.ActorID != (int)num1 && this.attacktask.Activated())
                this.attacktask.Deactivate();
            return actor;
        }

        /// <summary>
        /// The CheckAggro.
        /// </summary>
        private void CheckAggro()
        {
            double num = double.MaxValue;
            SagaDB.Actor.Actor actor1 = (SagaDB.Actor.Actor)null;
            bool flag = false;
            if (this.mob.Master != null)
            {
                if (!this.mob.Hate.ContainsKey(this.mob.Master.ActorID))
                    this.mob.Hate.Add(this.mob.Master.ActorID, 1U);
                if (this.mob.Master.type == ActorType.PC)
                    flag = true;
            }
            foreach (uint visibleActor in this.mob.Mob.VisibleActors)
            {
                SagaDB.Actor.Actor actor2 = this.mob.map.GetActor(visibleActor);
                if (actor2 != null && !actor2.Buff.Transparent && ((int)actor2.MapID == (int)this.mob.map.ID && !actor2.Status.Additions.ContainsKey("IAmTree")) && actor2.HP != 0U)
                {
                    if (this.mob.Mob.type != ActorType.PC && actor2.type == ActorType.MOB && (((MobEventHandler)actor2.e).AI.Mode.Symbol && !flag))
                    {
                        this.mob.Hate.Add(actor2.ActorID, 20U);
                        this.SendAggroEffect();
                    }
                    if ((this.mob.Mob.type == ActorType.PC || this.mob.Mob.Buff.ゾンビ || (actor2.type == ActorType.PC || actor2.type == ActorType.PET) || actor2.type == ActorType.SHADOW || actor2.type == ActorType.MOB && flag) && (this.mob.Mob.type != ActorType.PC || Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.mob.Mob, actor2)) && (!flag || actor2.type != ActorType.SHADOW) && ((!flag || actor2.type != ActorType.PET) && (!flag || actor2.type != ActorType.PC)) && (this.mob.Mob.type == ActorType.PC || actor2.type != ActorType.PC || ((ActorPC)actor2).PossessionTarget == 0U))
                    {
                        double lengthD = MobAI.GetLengthD(actor2.X, actor2.Y, this.mob.Mob.X, this.mob.Mob.Y);
                        if (lengthD < num)
                        {
                            byte x = SagaLib.Global.PosX16to8(this.mob.Mob.X, this.mob.map.Width);
                            byte y = SagaLib.Global.PosY16to8(this.mob.Mob.Y, this.mob.map.Height);
                            byte x2 = SagaLib.Global.PosX16to8(actor2.X, this.mob.map.Width);
                            byte y2 = SagaLib.Global.PosY16to8(actor2.Y, this.mob.map.Height);
                            List<MapNode> path = this.mob.FindPath(x, y, x2, y2);
                            if ((int)path[path.Count - 1].x == (int)x2 && (int)path[path.Count - 1].y == (int)y2)
                            {
                                if (actor2.type == ActorType.SHADOW && actor1 != actor2)
                                {
                                    num = 0.0;
                                    actor1 = actor2;
                                }
                                else
                                {
                                    num = lengthD;
                                    actor1 = actor2;
                                }
                            }
                        }
                    }
                }
            }
            if (num > 1000.0)
                return;
            this.mob.Hate.Add(actor1.ActorID, 20U);
            this.SendAggroEffect();
        }

        /// <summary>
        /// The SendAggroEffect.
        /// </summary>
        private void SendAggroEffect()
        {
            this.mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
            {
                actorID = this.mob.Mob.ActorID,
                effectID = 4539U
            }, this.mob.Mob, true);
        }

        /// <summary>
        /// The hasPlayerInSight.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool hasPlayerInSight()
        {
            if (this.mob.Mob.type == ActorType.PC)
                return true;
            foreach (uint visibleActor in this.mob.Mob.VisibleActors)
            {
                SagaDB.Actor.Actor actor = this.mob.map.GetActor(visibleActor);
                if (actor != null && (int)actor.MapID == (int)this.mob.map.ID && (actor.type == ActorType.PC && ((ActorPC)actor).Online))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="para">The para<see cref="object"/>.</param>
        public void Update(object para)
        {
            ActorPet actorPet = (ActorPet)null;
            if (this.mob.Mob.type == ActorType.PET)
                actorPet = (ActorPet)this.mob.Mob;
            if (this.mob.Hate.Count == 0 && !this.hasPlayerInSight())
            {
                ++this.counter;
                if (this.counter > 100)
                {
                    this.mob.Pause();
                    this.counter = 0;
                    return;
                }
            }
            if (actorPet != null && !this.mob.Hate.ContainsKey(actorPet.Owner.ActorID))
                this.mob.Hate.Add(actorPet.Owner.ActorID, 1U);
            if (this.mob.Master != null && !this.mob.Hate.ContainsKey(this.mob.Master.ActorID))
                this.mob.Hate.Add(this.mob.Master.ActorID, 1U);
            if (this.mob.Mob.Tasks.ContainsKey("AutoCast"))
            {
                if (this.attacktask.Activated())
                    this.attacktask.Deactivate();
                this.attacking = false;
            }
            else
            {
                if ((DateTime.Now - this.mob.LastSkillCast).TotalSeconds >= 3.0)
                {
                    if (this.mob.Master != null && this.mob.Master.type == ActorType.PC)
                    {
                        ActorPC master = (ActorPC)this.mob.Master;
                        if (master.BattleStatus == (byte)1 && Global.Random.Next(0, 99) < this.mob.Mode.EventMasterCombatSkillRate)
                        {
                            this.mob.OnShouldCastSkill(this.mob.Mode.EventMasterCombat, (SagaDB.Actor.Actor)master);
                            this.mob.LastSkillCast = DateTime.Now;
                        }
                    }
                    if (actorPet != null)
                    {
                        ActorPC owner = actorPet.Owner;
                        if (owner.BattleStatus == (byte)1 && Global.Random.Next(0, 99) < this.mob.Mode.EventMasterCombatSkillRate)
                        {
                            this.mob.OnShouldCastSkill(this.mob.Mode.EventMasterCombat, (SagaDB.Actor.Actor)owner);
                            this.mob.LastSkillCast = DateTime.Now;
                        }
                    }
                }
                this.dest = this.CurrentTarget();
                if ((this.mob.Mode.Active || this.mob.Mob.Buff.ゾンビ) && (this.dest == null || this.dest == this.mob.Master))
                    this.CheckAggro();
                if (this.dest == null)
                {
                    this.mob.AIActivity = Activity.IDLE;
                    if (!this.mob.commands.ContainsKey("Chase"))
                        return;
                    this.mob.commands.Remove("Chase");
                }
                else
                {
                    this.mob.AIActivity = Activity.BUSY;
                    if (this.mob.commands.ContainsKey("Move"))
                        this.mob.commands.Remove("Move");
                    this.attacktask.dActor = this.dest;
                    if ((DateTime.Now - this.mob.LastSkillCast).TotalSeconds >= 3.0 && Global.Random.Next(0, 99) < this.mob.Mode.EventAttackingSkillRate)
                    {
                        this.mob.OnShouldCastSkill(this.mob.Mode.EventAttacking, this.dest);
                        this.mob.LastSkillCast = DateTime.Now;
                    }
                    if (this.mob.commands.ContainsKey("Chase"))
                    {
                        if (this.attacktask.Activated())
                            this.attacktask.Deactivate();
                        this.attacking = false;
                    }
                    else
                    {
                        if ((int)this.x != (int)this.mob.Mob.X || (int)this.y != (int)this.mob.Mob.Y)
                        {
                            short x2;
                            short y2;
                            this.mob.map.FindFreeCoord(this.mob.Mob.X, this.mob.Mob.Y, out x2, out y2, this.mob.Mob);
                            bool flag = false;
                            if (this.mob.Mob.type == ActorType.PET && ((ActorMob)this.mob.Mob).BaseData.mobType == MobType.MAGIC_CREATURE)
                                flag = true;
                            if (((int)this.mob.Mob.X != (int)x2 || (int)this.mob.Mob.Y != (int)y2) && !this.mob.Mode.RunAway && !flag)
                            {
                                short[] pos = new short[2] { x2, y2 };
                                this.mob.map.MoveActor(Map.MOVE_TYPE.START, this.mob.Mob, pos, MobAI.GetDir((short)((int)pos[0] - (int)x2), (short)((int)pos[1] - (int)y2)), (ushort)((uint)this.mob.Mob.Speed / 20U));
                                return;
                            }
                            this.x = this.mob.Mob.X;
                            this.y = this.mob.Mob.Y;
                        }
                        if (this.dest.HP == 0U)
                        {
                            if (actorPet != null)
                            {
                                if ((int)this.dest.ActorID != (int)actorPet.Owner.ActorID)
                                {
                                    if (this.mob.Hate.ContainsKey(this.dest.ActorID))
                                        this.mob.Hate.Remove(this.dest.ActorID);
                                    if (this.attacktask.Activated())
                                        this.attacktask.Deactivate();
                                    this.attacktask = (MobAttack)null;
                                    return;
                                }
                            }
                            else
                            {
                                if (this.mob.Hate.ContainsKey(this.dest.ActorID))
                                    this.mob.Hate.Remove(this.dest.ActorID);
                                if (this.attacktask.Activated())
                                    this.attacktask.Deactivate();
                                this.attacktask = (MobAttack)null;
                                return;
                            }
                        }
                        float num = this.mob.Mob.type == ActorType.PC ? 1f : ((ActorMob)this.mob.Mob).BaseData.mobSize;
                        bool flag1 = false;
                        if (this.mob.Mob.type == ActorType.PET && this.dest.type == ActorType.PC && (((ActorMob)this.mob.Mob).BaseData.mobType == MobType.MAGIC_CREATURE && MobAI.GetLengthD(this.mob.Mob.X, this.mob.Mob.Y, this.dest.X, this.dest.Y) > 0.0))
                            flag1 = true;
                        if (MobAI.GetLengthD(this.mob.Mob.X, this.mob.Mob.Y, this.dest.X, this.dest.Y) > (double)num * 150.0 || flag1)
                        {
                            if (this.mob.Mode.RunAway && MobAI.GetLengthD(this.mob.Mob.X, this.mob.Mob.Y, this.dest.X, this.dest.Y) >= 2000.0)
                                return;
                            this.mob.commands.Add("Chase", (AICommand)new Chase(this.mob, this.dest));
                            if (this.attacktask.Activated())
                                this.attacktask.Deactivate();
                            this.attacking = false;
                        }
                        else if (!this.mob.Mode.RunAway || Global.Random.Next(0, 99) < 70)
                        {
                            if (this.mob.CanAttack && (actorPet == null || (int)this.dest.ActorID != (int)actorPet.Owner.ActorID))
                            {
                                if (!this.attacktask.Activated())
                                    this.attacktask.Activate();
                                this.attacking = true;
                            }
                        }
                        else
                        {
                            this.mob.commands.Add("Chase", (AICommand)new Chase(this.mob, this.dest));
                            if (this.attacktask.Activated())
                                this.attacktask.Deactivate();
                            this.attacking = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public CommandStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.dest == null)
                return;
            if (this.attacking && this.attacktask != null)
                this.attacktask.Deactivate();
            this.attacktask = (MobAttack)null;
            this.status = CommandStatus.FINISHED;
        }
    }
}
