namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="EnergyFlare" />.
    /// </summary>
    public class EnergyFlare : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)pc, dActor) && (dActor.type != ActorType.MOB || !Singleton<SkillHandler>.Instance.isBossMob(dActor)) ? 0 : -14;
        }

        /// <summary>
        /// The Proc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            float MATKBonus = (float)(1.20000004768372 + 0.100000001490116 * (double)level);
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Neutral, MATKBonus);
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, nameof(EnergyFlare), 40))
                return;
            EnergyFlare.EnergyFlareBuff energyFlareBuff = new EnergyFlare.EnergyFlareBuff(args, sActor, dActor, 20000, 2000);
            SkillHandler.ApplyAddition(dActor, (Addition)energyFlareBuff);
        }

        /// <summary>
        /// Defines the <see cref="EnergyFlareBuff" />.
        /// </summary>
        public class EnergyFlareBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the args.
            /// </summary>
            private SkillArg args;

            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Initializes a new instance of the <see cref="EnergyFlareBuff"/> class.
            /// </summary>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="period">The period<see cref="int"/>.</param>
            public EnergyFlareBuff(SkillArg args, SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor actor, int lifetime, int period)
        : base(args.skill, actor, nameof(EnergyFlare), lifetime, period)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.UpdateTimeHandler);
                this.args = args.Clone();
                this.sActor = sActor;
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
            }

            /// <summary>
            /// The UpdateTimeHandler.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void UpdateTimeHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                if (actor.HP > 0U && !actor.Buff.Dead)
                {
                    Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                    int num = (int)((double)actor.MaxHP * 0.0199999995529652);
                    Singleton<SkillHandler>.Instance.FixAttack(this.sActor, actor, this.args, Elements.Neutral, (float)num);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.args, actor, false);
                }
                else
                    skill.AdditionEnd();
            }
        }
    }
}
