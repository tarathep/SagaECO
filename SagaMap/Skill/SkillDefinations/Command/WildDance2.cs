namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WildDance2" />.
    /// </summary>
    public class WildDance2 : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)pc, dActor) ? 0 : -14;
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
            float ATKBonus = (float)(0.400000005960464 + 0.400000005960464 * (double)level);
            uint key = 125;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills2.ContainsKey(key) && actorPc.Skills2[key].Level == (byte)3)
                ATKBonus = (float)(0.519999980926514 + 0.519999980926514 * (double)level);
            if (actorPc.SkillsReserve.ContainsKey(key) && actorPc.SkillsReserve[key].Level == (byte)3)
                ATKBonus = (float)(0.519999980926514 + 0.519999980926514 * (double)level);
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            for (int index = 0; index < 4; ++index)
                dActor1.Add(dActor);
            args.delayRate = 4f;
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
