namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="Combination" />.
    /// </summary>
    public class Combination : ISkill
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
            uint index1 = 2136;
            uint index2 = 2359;
            uint index3 = 2137;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills2.ContainsKey(index1))
                args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(index1, actorPc.Skills2[index1].Level, 0));
            else if (actorPc.SkillsReserve.ContainsKey(index1))
                args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(index1, actorPc.SkillsReserve[index1].Level, 0));
            if (actorPc.Skills2.ContainsKey(index2))
                args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(index2, actorPc.Skills2[index2].Level, 0));
            else if (actorPc.SkillsReserve.ContainsKey(index2))
                args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(index2, actorPc.SkillsReserve[index2].Level, 0));
            if (actorPc.Skills2.ContainsKey(index3))
            {
                args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(index3, actorPc.Skills2[index3].Level, 0));
            }
            else
            {
                if (!actorPc.SkillsReserve.ContainsKey(index3))
                    return;
                args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(index3, actorPc.SkillsReserve[index3].Level, 0));
            }
        }
    }
}
