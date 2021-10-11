namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="ChgstRand" />.
    /// </summary>
    public class ChgstRand : ISkill
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
            float MATKBonus = (float)(1.10000002384186 + 0.100000001490116 * (double)level);
            int rate = 2 * (int)level;
            int lifetime = 4500 + 1000 * (int)level;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Dark, MATKBonus);
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Stun stun = new Stun(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)stun);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, rate))
            {
                鈍足 鈍足 = new 鈍足(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)鈍足);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Silence, rate))
            {
                Silence silence = new Silence(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)silence);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.CannotMove, rate))
            {
                CannotMove cannotMove = new CannotMove(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)cannotMove);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Confuse, rate))
            {
                Confuse confuse = new Confuse(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)confuse);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Frosen, rate))
            {
                Freeze freeze = new Freeze(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)freeze);
            }
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Poison, rate))
                return;
            Poison poison = new Poison(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)poison);
        }
    }
}
