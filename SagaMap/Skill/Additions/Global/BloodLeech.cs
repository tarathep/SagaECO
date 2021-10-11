namespace SagaMap.Skill.Additions.Global
{
    /// <summary>
    /// Defines the <see cref="BloodLeech" />.
    /// </summary>
    public class BloodLeech : DefaultBuff
    {
        /// <summary>
        /// Defines the rate.
        /// </summary>
        public float rate;

        /// <summary>
        /// Initializes a new instance of the <see cref="BloodLeech"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="rate">The rate<see cref="float"/>.</param>
        public BloodLeech(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, float rate)
      : base(skill, actor, nameof(BloodLeech), lifetime)
        {
            this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
            this.rate = rate;
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
