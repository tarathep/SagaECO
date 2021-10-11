namespace SagaMap.Skill.SkillDefinations.Swordman
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="Iai2" />.
    /// </summary>
    public class Iai2 : ISkill
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
            return this.CheckPossible((SagaDB.Actor.Actor)pc) ? 0 : -5;
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
            return actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) || actorPc.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0;
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
            if (!this.CheckPossible(sActor))
                return;
            Singleton<SkillHandler>.Instance.SetNextComboSkill(sActor, 2202U);
            args.type = ATTACK_TYPE.SLASH;
            float ATKBonus = (float)(1.20000004768372 + 0.300000011920929 * (double)level);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
        }
    }
}
