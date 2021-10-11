namespace SagaMap.Skill.SkillDefinations.Marionest
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MarioCtrl" />.
    /// </summary>
    public class MarioCtrl : ISkill
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
            int lifetime = 25000;
            uint[] numArray = new uint[6]
            {
        0U,
        26320000U,
        26280000U,
        26290000U,
        26300000U,
        26310000U
            };
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorMob mob = map.SpawnMob(numArray[(int)level], SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)2500, sActor);
            MarioCtrl.MarioCtrlBuff marioCtrlBuff = new MarioCtrl.MarioCtrlBuff(args.skill, sActor, lifetime, mob);
            SkillHandler.ApplyAddition(sActor, (Addition)marioCtrlBuff);
            if (sActor.Status.Additions.ContainsKey("MarioCtrlMove"))
                return;
            CannotMove cannotMove = new CannotMove(args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, (Addition)cannotMove);
        }

        /// <summary>
        /// Defines the <see cref="MarioCtrlBuff" />.
        /// </summary>
        public class MarioCtrlBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the mob.
            /// </summary>
            private ActorMob mob;

            /// <summary>
            /// Initializes a new instance of the <see cref="MarioCtrlBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
            public MarioCtrlBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, ActorMob mob)
        : base(skill, actor, nameof(MarioCtrl), lifetime)
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
