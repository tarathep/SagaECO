namespace SagaMap.Skill.SkillDefinations.Swordman
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="StunBlow" />.
    /// </summary>
    public class StunBlow : ISkill
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
            args.type = ATTACK_TYPE.BLOW;
            float ATKBonus = 1.5f;
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            if (level <= (byte)1 || (args.flag[0] & AttackFlag.HP_DAMAGE) == AttackFlag.NONE)
                return;
            int rate = 0;
            int lifetime = 0;
            switch (level)
            {
                case 2:
                    rate = 10;
                    lifetime = 3000;
                    break;
                case 3:
                    rate = 15;
                    lifetime = 4000;
                    break;
                case 4:
                    rate = 22;
                    lifetime = 5000;
                    break;
                case 5:
                    rate = 30;
                    lifetime = 6000;
                    break;
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Stun stun = new Stun(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)stun);
            }
        }
    }
}
