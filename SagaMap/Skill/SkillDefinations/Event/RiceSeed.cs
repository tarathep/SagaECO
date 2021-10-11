namespace SagaMap.Skill.SkillDefinations.Event
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="RiceSeed" />.
    /// </summary>
    public class RiceSeed : ISkill
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
            float Damage = 9999f;
            Singleton<SkillHandler>.Instance.FixAttack(sActor, dActor, args, Elements.Neutral, Damage);
            dActor.MP += 9999U;
            if (dActor.MP > dActor.MaxMP)
                dActor.MP = dActor.MaxMP;
            dActor.SP += 9999U;
            if (dActor.SP > dActor.MaxSP)
                dActor.SP = dActor.MaxSP;
            args.Init();
            args.flag[0] = AttackFlag.HP_HEAL | AttackFlag.MP_HEAL | AttackFlag.SP_HEAL;
        }
    }
}
