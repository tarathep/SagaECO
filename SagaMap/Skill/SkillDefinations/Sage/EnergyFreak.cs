namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="EnergyFreak" />.
    /// </summary>
    public class EnergyFreak : ISkill
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
            int lifetime = (2 + (int)level) * 1000;
            float MATKBonus = (float)(0.800000011920929 + 0.200000002980232 * (double)level);
            int num = 10 * (int)level;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Neutral, MATKBonus);
            if (SagaLib.Global.Random.Next(0, 99) >= num || Singleton<SkillHandler>.Instance.isBossMob(dActor))
                return;
            Silence silence = new Silence(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)silence);
            Poison poison = new Poison(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)poison);
            CannotMove cannotMove = new CannotMove(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)cannotMove);
        }
    }
}
