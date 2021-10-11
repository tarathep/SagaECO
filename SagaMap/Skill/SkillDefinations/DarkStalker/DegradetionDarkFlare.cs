namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="DegradetionDarkFlare" />.
    /// </summary>
    public class DegradetionDarkFlare : ISkill
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
            return 0;
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
            float MATKBonus = new float[4]
            {
        0.0f,
        0.94f,
        1.04f,
        1.09f
            }[(int)level];
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Dark, MATKBonus);
            int rate = 5 + 10 * (int)level;
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, nameof(DegradetionDarkFlare), rate) || Singleton<SkillHandler>.Instance.isBossMob(dActor))
                return;
            DegradetionDarkFlare.DegradetionDarkFlareBuff degradetionDarkFlareBuff = new DegradetionDarkFlare.DegradetionDarkFlareBuff(sActor, args, dActor, 20000, 2000);
            SkillHandler.ApplyAddition(dActor, (Addition)degradetionDarkFlareBuff);
        }

        /// <summary>
        /// Defines the <see cref="DegradetionDarkFlareBuff" />.
        /// </summary>
        public class DegradetionDarkFlareBuff : DefaultBuff
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
            /// Initializes a new instance of the <see cref="DegradetionDarkFlareBuff"/> class.
            /// </summary>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="period">The period<see cref="int"/>.</param>
            public DegradetionDarkFlareBuff(SagaDB.Actor.Actor sActor, SkillArg args, SagaDB.Actor.Actor actor, int lifetime, int period)
        : base(args.skill, actor, nameof(DegradetionDarkFlareBuff), lifetime, period)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.TimerUpdate);
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
            /// The TimerUpdate.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void TimerUpdate(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                uint num = (uint)((double)actor.MaxHP * 0.0149999996647239);
                Singleton<SkillHandler>.Instance.FixAttack(this.sActor, actor, this.args, Elements.Dark, (float)num);
                Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.args, actor, false);
            }
        }
    }
}
