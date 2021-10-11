namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="HpLostDamUp" />.
    /// </summary>
    public class HpLostDamUp : ISkill
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
            int lifetime = 5000 + 5000 * (int)level;
            int HPLost = 20 + 10 * (int)level;
            int DamUp = 20 + 10 * (int)level;
            HpLostDamUp.HpLostDamUpBuff hpLostDamUpBuff = new HpLostDamUp.HpLostDamUpBuff(args.skill, dActor, lifetime, HPLost, DamUp);
            SkillHandler.ApplyAddition(dActor, (Addition)hpLostDamUpBuff);
        }

        /// <summary>
        /// Defines the <see cref="HpLostDamUpBuff" />.
        /// </summary>
        public class HpLostDamUpBuff : DefaultBuff
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HpLostDamUpBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="HPLost">The HPLost<see cref="int"/>.</param>
            /// <param name="DamUp">The DamUp<see cref="int"/>.</param>
            public HpLostDamUpBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, int HPLost, int DamUp)
        : base(skill, actor, nameof(HpLostDamUp), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this[nameof(DamUp)] = DamUp;
                this[nameof(HPLost)] = HPLost;
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
