namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="StaffCtrl" />.
    /// </summary>
    public class StaffCtrl : ISkill
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
            int lifetime = 20000 * (int)level;
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorMob actorMob = map.SpawnMob(26330000U, SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)2500, sActor);
            MobEventHandler e = (MobEventHandler)actorMob.e;
            e.AI.Mode = new AIMode(1);
            e.AI.Mode.EventAttacking.Add(3281U, 100);
            e.AI.Mode.EventAttackingSkillRate = 100;
            StaffCtrl.StaffCtrlBuff staffCtrlBuff = new StaffCtrl.StaffCtrlBuff((SagaDB.Actor.Actor)actorMob, args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, (Addition)staffCtrlBuff);
        }

        /// <summary>
        /// Defines the <see cref="StaffCtrlBuff" />.
        /// </summary>
        public class StaffCtrlBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the mob.
            /// </summary>
            private SagaDB.Actor.Actor mob;

            /// <summary>
            /// Initializes a new instance of the <see cref="StaffCtrlBuff"/> class.
            /// </summary>
            /// <param name="mob">The mob<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public StaffCtrlBuff(SagaDB.Actor.Actor mob, SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
        : base(skill, actor, nameof(StaffCtrl), lifetime)
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
                if (this.mob == null)
                    return;
                this.mob.ClearTaskAddition();
                map.DeleteActor(this.mob);
            }
        }
    }
}
