namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Rush" />.
    /// </summary>
    public class Rush : ISkill
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
            float ATKBonus = 0.75f;
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            dActor1.Add(dActor);
            dActor1.Add(dActor);
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
