namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="RushBom" />.
    /// </summary>
    public class RushBom : ISkill
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
            uint skillID1 = 2410;
            uint skillID2 = 2411;
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID1, level, 0));
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID1, level, 300));
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID2, level, 3000));
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID2, level, 3300));
        }
    }
}
