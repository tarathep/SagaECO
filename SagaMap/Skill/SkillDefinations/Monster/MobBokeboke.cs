namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MobBokeboke" />.
    /// </summary>
    public class MobBokeboke : ISkill
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
            SagaDB.Actor.Actor actor = dActor;
            int lifetime = 3000;
            int rate1 = 4;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Stone, rate1))
            {
                Stone stone = new Stone(args.skill, actor, lifetime);
                SkillHandler.ApplyAddition(actor, (Addition)stone);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Frosen, rate1))
            {
                Freeze freeze = new Freeze(args.skill, actor, lifetime);
                SkillHandler.ApplyAddition(actor, (Addition)freeze);
            }
            int rate2 = 6;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Poison, rate2))
            {
                Poison poison = new Poison(args.skill, actor, lifetime);
                SkillHandler.ApplyAddition(actor, (Addition)poison);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Confuse, rate2))
            {
                Confuse confuse = new Confuse(args.skill, actor, lifetime);
                SkillHandler.ApplyAddition(actor, (Addition)confuse);
            }
            int rate3 = 8;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Stun, rate3))
            {
                Stun stun = new Stun(args.skill, actor, lifetime);
                SkillHandler.ApplyAddition(actor, (Addition)stun);
            }
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Sleep, rate3))
            {
                Sleep sleep = new Sleep(args.skill, actor, lifetime);
                SkillHandler.ApplyAddition(actor, (Addition)sleep);
            }
            int rate4 = 10;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.鈍足, rate4))
            {
                鈍足 鈍足 = new 鈍足(args.skill, actor, lifetime);
                SkillHandler.ApplyAddition(actor, (Addition)鈍足);
            }
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Silence, rate4))
                return;
            Silence silence = new Silence(args.skill, actor, lifetime);
            SkillHandler.ApplyAddition(actor, (Addition)silence);
        }
    }
}
