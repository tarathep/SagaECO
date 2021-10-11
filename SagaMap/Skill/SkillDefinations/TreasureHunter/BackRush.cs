namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="BackRush" />.
    /// </summary>
    public class BackRush : ISkill
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
            if (!Singleton<SkillHandler>.Instance.isEquipmentRight((SagaDB.Actor.Actor)sActor, ItemType.ROPE) && sActor.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count <= 0)
                return -5;
            return dActor.type == ActorType.MOB && Singleton<SkillHandler>.Instance.isBossMob((ActorMob)dActor) ? -14 : 0;
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
            int lifetime = 2000 + 1000 * (int)level;
            硬直 硬直1 = new 硬直(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)硬直1);
            硬直 硬直2 = new 硬直(args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, (Addition)硬直2);
        }
    }
}
