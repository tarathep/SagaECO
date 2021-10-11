namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Mob;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SomeTypeHitUp" />.
    /// </summary>
    public class SomeTypeHitUp : DefaultPassiveSkill
    {
        /// <summary>
        /// Defines the MobTypes.
        /// </summary>
        public Dictionary<MobType, ushort> MobTypes = new Dictionary<MobType, ushort>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SomeTypeHitUp"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public SomeTypeHitUp(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name)
      : base(skill, actor, name, true)
        {
            this.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEvent);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SomeTypeHitUp"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="peroid">The peroid<see cref="int"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public SomeTypeHitUp(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name, int peroid, int lifetime)
      : base(skill, actor, name, true, peroid, lifetime)
        {
            this.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEvent);
        }

        /// <summary>
        /// The AddMobType.
        /// </summary>
        /// <param name="type">The type<see cref="MobType"/>.</param>
        /// <param name="addValue">The addValue<see cref="ushort"/>.</param>
        public void AddMobType(MobType type, ushort addValue)
        {
            this.MobTypes.Add(type, addValue);
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
