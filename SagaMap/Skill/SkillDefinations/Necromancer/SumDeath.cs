namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SumDeath" />.
    /// </summary>
    public class SumDeath : ISkill
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
            ActorMob mob = Singleton<MapManager>.Instance.GetMap(sActor.MapID).SpawnMob(new uint[6]
            {
        0U,
        10250004U,
        10250005U,
        10250402U,
        10251001U,
        10251200U
            }[(int)level], (short)((int)sActor.X + SagaLib.Global.Random.Next(1, 10)), (short)((int)sActor.Y + SagaLib.Global.Random.Next(1, 10)), (short)2500, sActor);
            sActor.Slave.Add((SagaDB.Actor.Actor)mob);
            if (level < (byte)3)
            {
                int lifetime = 8000;
                SumDeath.SumDeathBuff sumDeathBuff = new SumDeath.SumDeathBuff(args.skill, sActor, mob, lifetime);
                SkillHandler.ApplyAddition(sActor, (Addition)sumDeathBuff);
            }
            uint[] numArray = new uint[6]
            {
        0U,
        3325U,
        3326U,
        3327U,
        3328U,
        3329U
            };
            MobAI ai = ((MobEventHandler)mob.e).AI;
            ai.CastSkill(numArray[(int)level], level, dActor);
            if (level != (byte)1)
                return;
            ai.CastSkill(numArray[(int)level], level, dActor);
            ai.CastSkill(numArray[(int)level], level, dActor);
        }

        /// <summary>
        /// Defines the <see cref="SumDeathBuff" />.
        /// </summary>
        public class SumDeathBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Defines the mob.
            /// </summary>
            private ActorMob mob;

            /// <summary>
            /// Initializes a new instance of the <see cref="SumDeathBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public SumDeathBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor sActor, ActorMob mob, int lifetime)
        : base(skill, sActor, nameof(SumDeath) + SagaLib.Global.Random.Next(0, 99).ToString(), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.sActor = sActor;
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
                this.sActor.Slave.Remove((SagaDB.Actor.Actor)this.mob);
                this.mob.ClearTaskAddition();
                Singleton<MapManager>.Instance.GetMap(this.sActor.MapID).DeleteActor((SagaDB.Actor.Actor)this.mob);
            }
        }
    }
}
