namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.SkillDefinations.Global;

    /// <summary>
    /// Defines the <see cref="GrassTrap" />.
    /// </summary>
    public class GrassTrap : Trap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GrassTrap"/> class.
        /// </summary>
        public GrassTrap()
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
            this.LifeTime = 7000 + 2000 * (int)level;
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
            factor = 0.33f;
            float Damage = (float)mActor.HP * factor;
            if ((double)Damage > 9999.0)
                Damage = 9999f;
            Singleton<SkillHandler>.Instance.FixAttack(sActor, mActor, args, Elements.Neutral, Damage);
        }
    }
}
