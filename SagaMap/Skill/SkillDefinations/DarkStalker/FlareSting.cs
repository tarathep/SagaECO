namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="FlareSting" />.
    /// </summary>
    public class FlareSting : ISkill
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
            float ATKBonus = new float[6]
            {
        0.0f,
        2.25f,
        2.5f,
        2.75f,
        3f,
        3.25f
            }[(int)level];
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Dark, ATKBonus);
            int num = 25 + 5 * (int)level;
            if (args.hp.Count <= 0 || (SagaLib.Global.Random.Next(0, 99) >= num || args.hp[0] <= 0 || Singleton<SkillHandler>.Instance.isBossMob(dActor)))
                return;
            args.autoCast.Add(new AutoCastInfo()
            {
                delay = 500,
                level = level,
                skillID = 2404U
            });
        }
    }
}
