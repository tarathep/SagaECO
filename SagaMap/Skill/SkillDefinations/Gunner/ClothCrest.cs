namespace SagaMap.Skill.SkillDefinations.Gunner
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="ClothCrest" />.
    /// </summary>
    public class ClothCrest : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)sActor, dActor) && this.CheckPossible((SagaDB.Actor.Actor)sActor) ? 0 : -5;
        }

        /// <summary>
        /// The CheckPossible.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool CheckPossible(SagaDB.Actor.Actor sActor)
        {
            if (sActor.type != ActorType.PC)
                return true;
            ActorPC actorPc = (ActorPC)sActor;
            return actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && (actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.GUN || actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.DUALGUN || actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.RIFLE || actorPc.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0);
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
            float ATKBonus = (float)((0.300000011920929 + 0.5 * (double)level) * 5.0);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
        }
    }
}
