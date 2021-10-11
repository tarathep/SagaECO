namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WhipFlourish" />.
    /// </summary>
    public class WhipFlourish : ISkill
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
            return Singleton<SkillHandler>.Instance.isEquipmentRight((SagaDB.Actor.Actor)sActor, ItemType.ROPE) || sActor.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0 ? 0 : -5;
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
        0.45f,
        0.7f,
        1f,
        1.5f,
        2f
            }[(int)level];
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)100, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
