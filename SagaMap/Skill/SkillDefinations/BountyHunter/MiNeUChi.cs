namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MiNeUChi" />.
    /// </summary>
    public class MiNeUChi : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)pc, dActor) ? 0 : -14;
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
            int[] numArray1 = new int[6] { 0, 30, 40, 50, 55, 60 };
            int[] numArray2 = new int[6]
            {
        0,
        8000,
        7500,
        7000,
        6500,
        6000
            };
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, numArray1[(int)level]))
            {
                Stun stun = new Stun(args.skill, dActor, numArray2[(int)level]);
                SkillHandler.ApplyAddition(dActor, (Addition)stun);
            }
            float ATKBonus = (float)(1.0 + 0.100000001490116 * (double)level);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
        }
    }
}
