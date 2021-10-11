namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Mob;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Knowledge" />.
    /// </summary>
    public class Knowledge : DefaultPassiveSkill
    {
        /// <summary>
        /// Defines the MobTypes.
        /// </summary>
        public List<MobType> MobTypes = new List<MobType>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Knowledge"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public Knowledge(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name)
      : base(skill, actor, name, true)
        {
            this.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEvent);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Knowledge"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="peroid">The peroid<see cref="int"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public Knowledge(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name, int peroid, int lifetime)
      : base(skill, actor, name, true, peroid, lifetime)
        {
            this.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEvent);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Knowledge"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="mobTypes">The mobTypes<see cref="MobType[]"/>.</param>
        public Knowledge(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name, params MobType[] mobTypes)
      : base(skill, actor, name, true)
        {
            this.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEvent);
            this.MobTypes.AddRange((IEnumerable<MobType>)mobTypes);
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
