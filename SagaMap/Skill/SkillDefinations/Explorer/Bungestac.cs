namespace SagaMap.Skill.SkillDefinations.Explorer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Skill.SkillDefinations.Global;

    /// <summary>
    /// Defines the <see cref="Bungestac" />.
    /// </summary>
    public class Bungestac : Trap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bungestac"/> class.
        /// </summary>
        public Bungestac()
      : base(false, 100U, Trap.PosType.sActor)
        {
        }

        /// <summary>
        /// The BeforeProc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public override void BeforeProc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            this.LifeTime = 22000 - 2000 * (int)level;
        }

        /// <summary>
        /// The ProcSkill.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="map">The map<see cref="Map"/>.</param>
        /// <param name="level">The level<see cref="int"/>.</param>
        /// <param name="factor">The factor<see cref="float"/>.</param>
        public override void ProcSkill(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
            factor *= (float)(0.649999976158142 + 0.25 * (double)level);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, mActor, args, Elements.Neutral, factor);
            if (mActor.Status.Additions.ContainsKey("CannotMove"))
                return;
            int rate = 30 + 10 * level;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.CannotMove, rate))
            {
                int lifetime = 11000 - 1000 * level;
                CannotMove cannotMove = new CannotMove(args.skill, mActor, lifetime);
                SkillHandler.ApplyAddition(mActor, (Addition)cannotMove);
            }
        }
    }
}
