namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AllRateUp" />.
    /// </summary>
    public class AllRateUp : ISkill
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
            float[] numArray = new float[6]
            {
        0.0f,
        1.4f,
        1.6f,
        1.8f,
        1.9f,
        1.95f
            };
            AllRateUp.AllRateUpBuff allRateUpBuff = new AllRateUp.AllRateUpBuff(args.skill, sActor, numArray[(int)level]);
            SkillHandler.ApplyAddition(sActor, (Addition)allRateUpBuff);
        }

        /// <summary>
        /// Defines the <see cref="AllRateUpBuff" />.
        /// </summary>
        public class AllRateUpBuff : DefaultPassiveSkill
        {
            /// <summary>
            /// Defines the Rate.
            /// </summary>
            public float Rate = 0.0f;

            /// <summary>
            /// Initializes a new instance of the <see cref="AllRateUpBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="rate">The rate<see cref="float"/>.</param>
            public AllRateUpBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, float rate)
        : base(skill, actor, nameof(AllRateUp), true)
            {
                this.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEvent);
                this.Rate = rate;
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
            {
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
            {
            }
        }
    }
}
