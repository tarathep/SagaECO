namespace SagaMap.Skill.SkillDefinations.Archer
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ConArrow" />.
    /// </summary>
    public class ConArrow : ISkill
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
            return !pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) || (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.BOW || Singleton<SkillHandler>.Instance.CheckDEMRightEquip((SagaDB.Actor.Actor)pc, ItemType.PARTS_BLOW)) ? -5 : 0;
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
            int num = 2;
            float ATKBonus = 0.0f;
            args.argType = SkillArg.ArgType.Attack;
            switch (level)
            {
                case 1:
                    ATKBonus = 0.85f;
                    break;
                case 2:
                    ATKBonus = 0.95f;
                    break;
                case 3:
                    ATKBonus = 1.05f;
                    break;
                case 4:
                    ATKBonus = 1.15f;
                    break;
                case 5:
                    ATKBonus = 1.25f;
                    break;
            }
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            for (int index = 0; index < num; ++index)
                dActor1.Add(dActor);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
