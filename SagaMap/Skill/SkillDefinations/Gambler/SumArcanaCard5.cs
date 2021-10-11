namespace SagaMap.Skill.SkillDefinations.Gambler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SumArcanaCard5" />.
    /// </summary>
    public class SumArcanaCard5 : ISkill
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
            int lifetime = 12000;
            uint mobID = 10330006;
            ActorMob mob = Singleton<MapManager>.Instance.GetMap(sActor.MapID).SpawnMob(mobID, (short)((int)sActor.X + SagaLib.Global.Random.Next(1, 11)), (short)((int)sActor.Y + SagaLib.Global.Random.Next(1, 11)), (short)2500, sActor);
            MobEventHandler e = (MobEventHandler)mob.e;
            e.AI.Mode = new AIMode(0);
            e.AI.Mode.EventAttackingSkillRate = 0;
            SumArcanaCard5.SumArcanaCardBuff sumArcanaCardBuff = new SumArcanaCard5.SumArcanaCardBuff(args, sActor, mob, lifetime);
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
            /// Defines the arg.
            /// </summary>
            private SkillArg arg;

            /// <summary>
            /// Initializes a new instance of the <see cref="SumArcanaCardBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public SumArcanaCardBuff(SkillArg skill, SagaDB.Actor.Actor actor, ActorMob mob, int lifetime)
        : base(skill.skill, actor, "SumArcanaCard", lifetime, 6000)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.TimeUpdate);
                this.mob = mob;
                this.arg = skill.Clone();
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                this.TimeUpdate(actor, skill);
            }

            /// <summary>
            /// The TimeUpdate.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void TimeUpdate(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                int rate = 60;
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                foreach (SagaDB.Actor.Actor actor1 in map.GetActorsArea((SagaDB.Actor.Actor)this.mob, (short)600, false))
                {
                    if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(actor, actor1) && Singleton<SkillHandler>.Instance.CanAdditionApply(actor, actor1, SkillHandler.DefaultAdditions.Confuse, rate))
                    {
                        Confuse confuse = new Confuse(this.arg.skill, actor1, 3000);
                        SkillHandler.ApplyAddition(actor1, (Addition)confuse);
                    }
                }
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.arg, actor, false);
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
