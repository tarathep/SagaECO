namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Brandish" />.
    /// </summary>
    public class Brandish : ISkill
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
            int min = 2;
            int max = 4;
            float ATKBonus = 0.75f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            int num = SagaLib.Global.Random.Next(min, max);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            for (int index = 0; index < num; ++index)
                dActor1.Add(dActor);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
