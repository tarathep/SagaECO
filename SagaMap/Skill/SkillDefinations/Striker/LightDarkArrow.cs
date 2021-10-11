namespace SagaMap.Skill.SkillDefinations.Striker
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="LightDarkArrow" />.
    /// </summary>
    public class LightDarkArrow : ISkill
    {
        /// <summary>
        /// Defines the ArrowElement.
        /// </summary>
        private Elements ArrowElement = Elements.Neutral;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightDarkArrow"/> class.
        /// </summary>
        /// <param name="e">The e<see cref="Elements"/>.</param>
        public LightDarkArrow(Elements e)
        {
            this.ArrowElement = e;
        }

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
            float ATKBonus = (float)(1.29999995231628 + 0.200000002980232 * (double)level);
            args.argType = SkillArg.ArgType.Attack;
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, this.ArrowElement, ATKBonus);
        }
    }
}
