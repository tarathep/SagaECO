namespace SagaMap.Skill.SkillDefinations.Knight
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="DJoint" />.
    /// </summary>
    public class DJoint : ISkill
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
            return sActor.PossessionTarget != 0U ? -23 : 0;
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
            int lifetime = 60000 - 10000 * (int)level;
            float rate = (float)(0.100000001490116 + 0.100000001490116 * (double)level);
            DJoint.DJointBuff djointBuff = new DJoint.DJointBuff(args.skill, sActor, dActor, lifetime, rate);
            SkillHandler.ApplyAddition(dActor, (Addition)djointBuff);
        }

        /// <summary>
        /// Defines the <see cref="DJointBuff" />.
        /// </summary>
        public class DJointBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the rate.
            /// </summary>
            private float rate;

            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Initializes a new instance of the <see cref="DJointBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="rate">The rate<see cref="float"/>.</param>
            public DJointBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor actor, int lifetime, float rate)
        : base(skill, actor, nameof(DJoint), lifetime)
            {
                this.rate = rate;
                this.sActor = sActor;
                this["Target"] = (int)sActor.ActorID;
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
                skill["Rate"] = 10 + 10 * (int)skill.skill.Level;
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                skill["Rate"] = 0;
            }
        }
    }
}
