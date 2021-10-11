namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PullWhip" />.
    /// </summary>
    public class PullWhip : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)sActor, dActor) ? 0 : -14;
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
            float ATKBonus = (float)(1.10000002384186 + 0.300000011920929 * (double)level);
            uint key = 2337;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills2.ContainsKey(key))
            {
                int level1 = (int)actorPc.Skills2[key].Level;
                ATKBonus = level1 > 2 ? (level1 <= 2 || level1 >= 4 ? (float)(1.39999997615814 + 0.349999994039536 * (double)level) : (float)(1.35000002384186 + 0.349999994039536 * (double)level)) : (float)(1.14999997615814 + 0.349999994039536 * (double)level);
            }
            if (actorPc.SkillsReserve.ContainsKey(key))
            {
                int level1 = (int)actorPc.SkillsReserve[key].Level;
                ATKBonus = level1 > 2 ? (level1 <= 2 || level1 >= 4 ? (float)(1.39999997615814 + 0.349999994039536 * (double)level) : (float)(1.35000002384186 + 0.349999994039536 * (double)level)) : (float)(1.14999997615814 + 0.349999994039536 * (double)level);
            }
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            if (dActor.type != ActorType.MOB || Singleton<SkillHandler>.Instance.isBossMob((ActorMob)dActor))
                return;
            硬直 硬直 = new 硬直(args.skill, dActor, 1000);
            SkillHandler.ApplyAddition(dActor, (Addition)硬直);
        }
    }
}
