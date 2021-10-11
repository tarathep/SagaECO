namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Skill.SkillDefinations.Global;

    /// <summary>
    /// Defines the <see cref="PitTrap" />.
    /// </summary>
    public class PitTrap : Trap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PitTrap"/> class.
        /// </summary>
        public PitTrap()
      : base(true, 100U, Trap.PosType.sActor)
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
            int rate = 30 + 10 * level;
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.鈍足, rate))
                return;
            int[] numArray = new int[6]
            {
        0,
        6000,
        5500,
        5000,
        4500,
        4000
            };
            鈍足 鈍足 = new 鈍足(args.skill, mActor, numArray[level]);
            SkillHandler.ApplyAddition(mActor, (Addition)鈍足);
        }
    }
}
