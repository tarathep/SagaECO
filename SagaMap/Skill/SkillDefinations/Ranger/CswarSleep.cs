namespace SagaMap.Skill.SkillDefinations.Ranger
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Skill.SkillDefinations.Global;

    /// <summary>
    /// Defines the <see cref="CswarSleep" />.
    /// </summary>
    public class CswarSleep : Trap
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="CswarSleep"/> class.
        /// </summary>
        public CswarSleep()
      : base(false, Trap.PosType.sActor)
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CswarSleep"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public CswarSleep(bool MobUse)
      : base(false, Trap.PosType.sActor)
        {
            this.MobUse = MobUse;
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
            this.LifeTime = 2000 + 1000 * (int)level;
            this.Range = new uint[6]
            {
        0U,
        100U,
        200U,
        200U,
        300U,
        300U
            }[(int)level];
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
            if (this.MobUse)
                level = 5;
            int[] numArray = new int[6]
            {
        0,
        3000,
        4000,
        5000,
        5000,
        6000
            };
            int rate = 20 + 10 * level;
            int lifetime = numArray[level];
            if (!mActor.Status.Additions.ContainsKey("Sleep") && Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.Sleep, rate))
            {
                鈍足 鈍足 = new 鈍足(args.skill, mActor, lifetime);
                SkillHandler.ApplyAddition(mActor, (Addition)鈍足);
            }
            if (mActor.Status.Additions.ContainsKey("硬直") || !Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, mActor, SkillHandler.DefaultAdditions.硬直, rate))
                return;
            硬直 硬直 = new 硬直(args.skill, mActor, lifetime);
            SkillHandler.ApplyAddition(mActor, (Addition)硬直);
        }
    }
}
