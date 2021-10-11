namespace SagaMap.Skill.SkillDefinations.Vates
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="LightOne" />.
    /// </summary>
    public class LightOne : ISkill
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
            float MATKBonus = 0.0f;
            switch (level)
            {
                case 1:
                    MATKBonus = 1.2f;
                    break;
                case 2:
                    MATKBonus = 1.45f;
                    break;
                case 3:
                    MATKBonus = 1.7f;
                    break;
                case 4:
                    MATKBonus = 1.95f;
                    break;
                case 5:
                    MATKBonus = 2.2f;
                    break;
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Holy, MATKBonus);
        }
    }
}
