namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Raid" />.
    /// </summary>
    public class Raid : ISkill
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
            return sActor.Status.Additions.ContainsKey("Hiding") || sActor.Status.Additions.ContainsKey("Cloaking") ? 0 : -12;
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
            float ATKBonus = (float)(2.5 + 1.0 * (double)level);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            int rate = 20 + 5 * (int)level;
            int lifetime = 2000 + 1500 * (int)level;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Stun stun = new Stun(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)stun);
            }
            Singleton<SkillHandler>.Instance.PushBack(sActor, dActor, 4);
        }
    }
}
