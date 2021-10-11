namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DelayTrap" />.
    /// </summary>
    public class DelayTrap : ISkill
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
            uint itemID = 10022307;
            if (Singleton<SkillHandler>.Instance.CountItem(sActor, itemID) <= 0)
                return -12;
            Singleton<SkillHandler>.Instance.TakeItem(sActor, itemID, (ushort)1);
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
            int lifetime = 2000 + 2000 * (int)level;
            Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            DelayTrap.DelayTrapBuff delayTrapBuff = new DelayTrap.DelayTrapBuff(args, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, (Addition)delayTrapBuff);
        }

        /// <summary>
        /// Defines the <see cref="DelayTrapBuff" />.
        /// </summary>
        public class DelayTrapBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the args.
            /// </summary>
            private SkillArg args;

            /// <summary>
            /// Defines the x.
            /// </summary>
            private short x;

            /// <summary>
            /// Defines the y.
            /// </summary>
            private short y;

            /// <summary>
            /// Initializes a new instance of the <see cref="DelayTrapBuff"/> class.
            /// </summary>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public DelayTrapBuff(SkillArg args, SagaDB.Actor.Actor actor, int lifetime)
        : base(args.skill, actor, nameof(DelayTrap), lifetime)
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
                this.x = actor.X;
                this.y = actor.Y;
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                int level = (int)skill.skill.Level;
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(this.x, this.y, (short)150, (SagaDB.Actor.Actor[])null);
                float num = (float)(1.0 + 1.0 * (double)level);
                List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
                {
                    if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(actor, dActor2))
                        dActor1.Add(dActor2);
                }
                float ATKBonus = num * (1f / (float)dActor1.Count);
                Singleton<SkillHandler>.Instance.PhysicalAttack(actor, dActor1, this.args, Elements.Neutral, ATKBonus);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.args, actor, true);
            }
        }
    }
}
