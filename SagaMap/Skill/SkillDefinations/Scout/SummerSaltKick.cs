namespace SagaMap.Skill.SkillDefinations.Scout
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SummerSaltKick" />.
    /// </summary>
    public class SummerSaltKick : ISkill
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
            return 0;
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
            uint key = 125;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills2.ContainsKey(key) && actorPc.Skills2[key].Level == (byte)3)
                ATKBonus = (float)(0.980000019073486 + 0.319999992847443 * (double)level);
            if (actorPc.SkillsReserve.ContainsKey(key) && actorPc.SkillsReserve[key].Level == (byte)3)
                ATKBonus = (float)(0.980000019073486 + 0.319999992847443 * (double)level);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            if ((args.flag[0] & AttackFlag.HP_DAMAGE) == AttackFlag.NONE)
                return;
            Singleton<SkillHandler>.Instance.PushBack(sActor, dActor, 3);
            硬直 硬直 = new 硬直(args.skill, dActor, 2000);
            SkillHandler.ApplyAddition(dActor, (Addition)硬直);
        }
    }
}
