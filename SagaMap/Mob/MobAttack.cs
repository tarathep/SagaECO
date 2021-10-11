namespace SagaMap.Mob
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Skill;
    using System;

    /// <summary>
    /// Defines the <see cref="MobAttack" />.
    /// </summary>
    public class MobAttack : MultiRunTask
    {
        /// <summary>
        /// Defines the mob.
        /// </summary>
        private MobAI mob;

        /// <summary>
        /// Defines the dActor.
        /// </summary>
        public SagaDB.Actor.Actor dActor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobAttack"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="MobAI"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public MobAttack(MobAI mob, SagaDB.Actor.Actor dActor)
        {
            this.dueTime = 0;
            this.mob = mob;
            this.period = this.calcDelay(mob.Mob);
            this.dActor = dActor;
        }

        /// <summary>
        /// The calcDelay.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int calcDelay(SagaDB.Actor.Actor actor)
        {
            int num1 = 0;
            if (this.mob.Mob.type == ActorType.MOB)
                num1 = (int)((ActorMob)this.mob.Mob).BaseData.aspd;
            if (this.mob.Mob.type == ActorType.PET)
                num1 = (int)((ActorMob)this.mob.Mob).BaseData.aspd;
            if (this.mob.Mob.type == ActorType.SHADOW || this.mob.Mob.type == ActorType.GOLEM || this.mob.Mob.type == ActorType.PC)
                num1 = (int)this.mob.Mob.Status.aspd;
            int num2 = num1 + (int)actor.Status.aspd_skill;
            if (num2 > 960)
                num2 = 960;
            uint num3;
            if (actor.type == ActorType.PC)
            {
                ActorPC actorPc = (ActorPC)actor;
                num3 = !actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) ? 2000U - (uint)((double)(2000 * num2) * (1.0 / 1000.0)) : (!actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand ? 2000U - (uint)((double)(2000 * num2) * (1.0 / 1000.0)) : 2400U - (uint)((double)(2400 * num2) * (1.0 / 1000.0)));
            }
            else
                num3 = 2000U - (uint)((double)(2000 * num2) * (1.0 / 1000.0));
            if ((double)actor.Status.aspd_skill_perc != 0.0)
                num3 = (uint)((double)num3 / (double)actor.Status.aspd_skill_perc);
            return (int)num3;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            try
            {
                if (!this.mob.CanAttack)
                    return;
                if (this.mob.Mob.HP == 0U || this.dActor.HP == 0U || !this.mob.Hate.ContainsKey(this.dActor.ActorID) || this.mob.Mob.Tasks.ContainsKey("AutoCast"))
                {
                    if (this.mob.Hate.ContainsKey(this.dActor.ActorID))
                        this.mob.Hate.Remove(this.dActor.ActorID);
                    if (!this.Activated())
                        return;
                    this.Deactivate();
                }
                else if (this.mob.Mob.type == ActorType.PET && (int)((ActorPet)this.mob.Mob).Owner.ActorID == (int)this.dActor.ActorID)
                {
                    if (!this.Activated())
                        return;
                    this.Deactivate();
                }
                else
                {
                    if (this.mob.Master != null && (int)this.dActor.ActorID == (int)this.mob.Master.ActorID)
                        return;
                    if (this.dActor.type == ActorType.PC && this.dActor.HP == 0U)
                    {
                        if (this.mob.Hate.ContainsKey(this.dActor.ActorID))
                            this.mob.Hate.Remove(this.dActor.ActorID);
                        if (!this.Activated())
                            return;
                        this.Deactivate();
                    }
                    else
                    {
                        SkillArg skillArg = new SkillArg();
                        Singleton<SkillHandler>.Instance.Attack(this.mob.Mob, this.dActor, skillArg);
                        this.mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, (MapEventArgs)skillArg, this.mob.Mob, true);
                        this.period = this.calcDelay(this.mob.Mob);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
    }
}
