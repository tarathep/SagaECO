namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Skill.SkillDefinations.Global;

    /// <summary>
    /// Defines the <see cref="ChokingGas" />.
    /// </summary>
    public class ChokingGas : Trap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChokingGas"/> class.
        /// </summary>
        public ChokingGas()
      : base(true, 200U, Trap.PosType.sActor)
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
            this.LifeTime = 30000;
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
            int rate = 35 + 10 * level;
            if (mActor.Status.Additions.ContainsKey("Silence") || !Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.Silence, rate))
                return;
            Silence silence = new Silence(args.skill, mActor, 10000);
            SkillHandler.ApplyAddition(mActor, (Addition)silence);
        }
    }
}
