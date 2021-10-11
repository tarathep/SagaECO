namespace SagaMap.Skill.SkillDefinations.Event
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SumMobCastSkill" />.
    /// </summary>
    public class SumMobCastSkill : ISkill
    {
        /// <summary>
        /// Defines the NextSkillID.
        /// </summary>
        private uint NextSkillID;

        /// <summary>
        /// Defines the MobID.
        /// </summary>
        private uint MobID;

        /// <summary>
        /// Initializes a new instance of the <see cref="SumMobCastSkill"/> class.
        /// </summary>
        /// <param name="NextSkillID">The NextSkillID<see cref="uint"/>.</param>
        /// <param name="MobID">The MobID<see cref="uint"/>.</param>
        public SumMobCastSkill(uint NextSkillID, uint MobID)
        {
            this.NextSkillID = NextSkillID;
            this.MobID = MobID;
        }

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
            int lifetime = 5000;
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            map.GetRandomPosAroundActor(sActor);
            ActorMob mob = map.SpawnMob(this.MobID, SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)100, sActor);
            SumMobCastSkill.SumMobCastSkillBuff mobCastSkillBuff = new SumMobCastSkill.SumMobCastSkillBuff(args.skill, dActor, mob, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)mobCastSkillBuff);
            ((MobEventHandler)mob.e).AI.CastSkill(this.NextSkillID, (byte)1, sActor);
        }

        /// <summary>
        /// Defines the <see cref="SumMobCastSkillBuff" />.
        /// </summary>
        public class SumMobCastSkillBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the mob.
            /// </summary>
            private ActorMob mob;

            /// <summary>
            /// Initializes a new instance of the <see cref="SumMobCastSkillBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public SumMobCastSkillBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, ActorMob mob, int lifetime)
        : base(skill, actor, nameof(SumMobCastSkillBuff), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.mob = mob;
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                this.mob.ClearTaskAddition();
                Singleton<MapManager>.Instance.GetMap(this.mob.MapID).DeleteActor((SagaDB.Actor.Actor)this.mob);
            }
        }
    }
}
