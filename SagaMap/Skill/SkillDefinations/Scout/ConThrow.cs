namespace SagaMap.Skill.SkillDefinations.Scout
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ConThrow" />.
    /// </summary>
    public class ConThrow : ISkill
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
            return !pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) || pc.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count <= 0 || pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.THROW && pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.CARD && pc.Inventory.GetContainer(ContainerType.RIGHT_HAND2)[0].BaseData.itemType != ItemType.THROW && pc.Inventory.GetContainer(ContainerType.RIGHT_HAND2)[0].BaseData.itemType != ItemType.CARD ? -5 : 0;
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
            int num = (int)level + 1;
            float ATKBonus = 0.0f;
            args.argType = SkillArg.ArgType.Attack;
            switch (level)
            {
                case 1:
                case 2:
                    ATKBonus = 1.1f;
                    break;
                case 3:
                case 4:
                    ATKBonus = 1.15f;
                    break;
                case 5:
                    ATKBonus = 1.2f;
                    break;
            }
            if (sActor.type == ActorType.PC)
            {
                ActorPC actorPc = (ActorPC)sActor;
                if (num > (int)actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack)
                    num = (int)actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack;
            }
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            for (int index = 0; index < num; ++index)
                dActor1.Add(dActor);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
