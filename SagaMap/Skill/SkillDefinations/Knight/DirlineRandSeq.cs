namespace SagaMap.Skill.SkillDefinations.Knight
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="DirlineRandSeq" />.
    /// </summary>
    public class DirlineRandSeq : ISkill
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
            return Singleton<SkillHandler>.Instance.isEquipmentRight((SagaDB.Actor.Actor)sActor, ItemType.SPEAR, ItemType.RAPIER) || sActor.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0 ? 0 : -5;
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
            uint skillID = 2382;
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID, level, 0));
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID, level, 560));
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID, level, 560));
        }
    }
}
