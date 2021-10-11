namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Skill.SkillDefinations.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ClayMore" />.
    /// </summary>
    public class ClayMore : Trap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClayMore"/> class.
        /// </summary>
        public ClayMore()
      : base(true, 300U, Trap.PosType.sActor)
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
            this.LifeTime = 20000 + 1000 * (int)level;
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
            int lifetime = 1500;
            ClayMore.ClayMoreBuff clayMoreBuff = new ClayMore.ClayMoreBuff(args, sActor, actor, lifetime);
            SkillHandler.ApplyAddition(sActor, (Addition)clayMoreBuff);
        }

        /// <summary>
        /// Defines the <see cref="ClayMoreBuff" />.
        /// </summary>
        public class ClayMoreBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Defines the args.
            /// </summary>
            private SkillArg args;

            /// <summary>
            /// Initializes a new instance of the <see cref="ClayMoreBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public ClayMoreBuff(SkillArg skill, SagaDB.Actor.Actor sActor, ActorSkill actor, int lifetime)
        : base(skill.skill, (SagaDB.Actor.Actor)actor, nameof(ClayMore), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.sActor = sActor;
                this.args = skill.Clone();
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
                float ATKBonus = (float)(2.5 + 0.5 * (double)skill.skill.Level);
                Map map = Singleton<MapManager>.Instance.GetMap(this.sActor.MapID);
                List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(this.sActor, (short)350, false);
                List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
                {
                    if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.sActor, dActor2))
                        dActor1.Add(dActor2);
                }
                Singleton<SkillHandler>.Instance.PhysicalAttack(this.sActor, dActor1, this.args, Elements.Neutral, ATKBonus);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.args, actor, false);
            }
        }
    }
}
