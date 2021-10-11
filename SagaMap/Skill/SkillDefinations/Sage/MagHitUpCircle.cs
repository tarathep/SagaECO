namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MagHitUpCircle" />.
    /// </summary>
    public class MagHitUpCircle : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
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
            int lifetime = 1000 * (int)level;
            float rate = (float)(1.0 + 0.200000002980232 * (double)level);
            foreach (SagaDB.Actor.Actor actor in Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)100, false))
            {
                if (actor.type == ActorType.PC)
                {
                    MagHitUpCircle.MagHitUpCircleBuff magHitUpCircleBuff = new MagHitUpCircle.MagHitUpCircleBuff(args.skill, actor, lifetime, rate);
                    SkillHandler.ApplyAddition(actor, (Addition)magHitUpCircleBuff);
                }
            }
        }

        /// <summary>
        /// Defines the <see cref="MagHitUpCircleBuff" />.
        /// </summary>
        public class MagHitUpCircleBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the Rate.
            /// </summary>
            public float Rate;

            /// <summary>
            /// Initializes a new instance of the <see cref="MagHitUpCircleBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="rate">The rate<see cref="float"/>.</param>
            public MagHitUpCircleBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, float rate)
        : base(skill, actor, nameof(MagHitUpCircle), lifetime)
            {
                this.Rate = rate;
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
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
        }
    }
}
