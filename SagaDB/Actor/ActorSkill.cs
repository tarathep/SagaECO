namespace SagaDB.Actor
{
    /// <summary>
    /// Defines the <see cref="ActorSkill" />.
    /// </summary>
    public class ActorSkill : ActorMob
    {
        /// <summary>
        /// Defines the skill.
        /// </summary>
        private SagaDB.Skill.Skill skill;

        /// <summary>
        /// Defines the caster.
        /// </summary>
        private SagaDB.Actor.Actor caster;

        /// <summary>
        /// Defines the stackable.
        /// </summary>
        private bool stackable;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorSkill"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="caster">The caster<see cref="SagaDB.Actor.Actor"/>.</param>
        public ActorSkill(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor caster)
        {
            this.type = ActorType.SKILL;
            this.skill = skill;
            this.caster = caster;
        }

        /// <summary>
        /// Gets or sets the Skill.
        /// </summary>
        public SagaDB.Skill.Skill Skill
        {
            get
            {
                return this.skill;
            }
            set
            {
                this.skill = value;
            }
        }

        /// <summary>
        /// Gets the Caster.
        /// </summary>
        public SagaDB.Actor.Actor Caster
        {
            get
            {
                return this.caster;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Stackable.
        /// </summary>
        public bool Stackable
        {
            get
            {
                return this.stackable;
            }
            set
            {
                this.stackable = value;
            }
        }
    }
}
