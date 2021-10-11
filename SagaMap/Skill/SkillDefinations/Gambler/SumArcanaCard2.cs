namespace SagaMap.Skill.SkillDefinations.Gambler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SumArcanaCard2" />.
    /// </summary>
    public class SumArcanaCard2 : ISkill
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
            int lifetime = 6000;
            uint mobID = 10100101;
            ActorMob mob = Singleton<MapManager>.Instance.GetMap(sActor.MapID).SpawnMob(mobID, (short)((int)sActor.X + SagaLib.Global.Random.Next(1, 11)), (short)((int)sActor.Y + SagaLib.Global.Random.Next(1, 11)), (short)2500, sActor);
            ((MobEventHandler)mob.e).AI.Mode = new AIMode(1);
            SumArcanaCard2.SumArcanaCardBuff sumArcanaCardBuff = new SumArcanaCard2.SumArcanaCardBuff(args.skill, sActor, mob, lifetime);
            SkillHandler.ApplyAddition(sActor, (Addition)sumArcanaCardBuff);
        }

        /// <summary>
        /// Defines the <see cref="SumArcanaCardBuff" />.
        /// </summary>
        public class SumArcanaCardBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the mob.
            /// </summary>
            private ActorMob mob;

            /// <summary>
            /// Initializes a new instance of the <see cref="SumArcanaCardBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public SumArcanaCardBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, ActorMob mob, int lifetime)
        : base(skill, actor, "SumArcanaCard", lifetime)
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
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.mob.ClearTaskAddition();
                map.DeleteActor((SagaDB.Actor.Actor)this.mob);
            }
        }
    }
}
