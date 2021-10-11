namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="ThrowNugget" />.
    /// </summary>
    public class ThrowNugget : ISkill
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
            float ATKBonus = (float)(1.0 + 1.0 * (double)level);
            uint itemID = new uint[6]
            {
        0U,
        10015708U,
        10015700U,
        10015600U,
        10015800U,
        10015707U
            }[(int)level];
            ActorPC pc = (ActorPC)sActor;
            if (Singleton<SkillHandler>.Instance.CountItem(pc, itemID) <= 0)
                return;
            Singleton<SkillHandler>.Instance.TakeItem(pc, itemID, (ushort)1);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
        }
    }
}
