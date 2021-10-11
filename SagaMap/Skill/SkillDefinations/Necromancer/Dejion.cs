namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="Dejion" />.
    /// </summary>
    public class Dejion : ISkill
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
            int num = 10 + 10 * (int)level;
            if (dActor.type == ActorType.MOB && Singleton<SkillHandler>.Instance.isBossMob((ActorMob)dActor) || SagaLib.Global.Random.Next(0, 99) >= num)
                return;
            dActor.HP = 0U;
            dActor.e.OnDie();
            args.affectedActors.Add(dActor);
            args.Init();
            args.flag[0] = AttackFlag.DIE;
        }
    }
}
