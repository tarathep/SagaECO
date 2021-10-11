namespace SagaMap.Skill.SkillDefinations.Machinery
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MirrorSkill" />.
    /// </summary>
    public class MirrorSkill : ISkill
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
            int lifetime = 3000 * (int)level;
            MirrorSkill.MirrorSkillBuff mirrorSkillBuff = new MirrorSkill.MirrorSkillBuff(args, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)mirrorSkillBuff);
        }

        /// <summary>
        /// Defines the <see cref="MirrorSkillBuff" />.
        /// </summary>
        public class MirrorSkillBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the args.
            /// </summary>
            private SkillArg args;

            /// <summary>
            /// Initializes a new instance of the <see cref="MirrorSkillBuff"/> class.
            /// </summary>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public MirrorSkillBuff(SkillArg args, SagaDB.Actor.Actor actor, int lifetime)
        : base(args.skill, actor, nameof(MirrorSkill), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.args = args.Clone();
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
                float ATKBonus = (float)SagaLib.Global.Random.Next(10, 400) / 100f;
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(actor, (short)200, false);
                List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
                {
                    if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(actor, dActor2))
                        dActor1.Add(dActor2);
                }
                Singleton<SkillHandler>.Instance.PhysicalAttack(actor, dActor1, this.args, Elements.Neutral, ATKBonus);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.args, actor, false);
            }
        }
    }
}
