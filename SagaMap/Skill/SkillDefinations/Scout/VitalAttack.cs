namespace SagaMap.Skill.SkillDefinations.Scout
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="VitalAttack" />.
    /// </summary>
    public class VitalAttack : ISkill
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
            float ATKBonus = (float)(0.75 + 0.25 * (double)level);
            sActor.Status.cri_skill += (short)(55 + (int)level * 5);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            sActor.Status.cri_skill -= (short)(55 + (int)level * 5);
            if ((args.flag[0] & AttackFlag.HP_DAMAGE) == AttackFlag.NONE)
                return;
            int rate = 0;
            int lifetime = 3000;
            switch (level)
            {
                case 2:
                    rate = 15;
                    break;
                case 3:
                    rate = 25;
                    break;
                case 4:
                    rate = 35;
                    break;
                case 5:
                    rate = 45;
                    break;
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, rate))
            {
                鈍足 鈍足 = new 鈍足(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)鈍足);
            }
        }
    }
}
