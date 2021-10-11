namespace SagaMap.Skill.SkillDefinations.Marionest
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="EnemyCharming" />.
    /// </summary>
    public class EnemyCharming : ISkill
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
            return dActor.type == ActorType.PC || dActor.type == ActorType.MOB && !Singleton<SkillHandler>.Instance.isBossMob((ActorMob)dActor) ? 0 : -13;
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
            if (dActor.type == ActorType.PC)
            {
                int num = 10 + 10 * (int)level;
                if (SagaLib.Global.Random.Next(0, 99) >= num)
                    return;
                dActor.HP = 0U;
                dActor.e.OnDie();
                args.affectedActors.Add(dActor);
                args.Init();
                args.flag[0] = AttackFlag.DIE;
            }
            else
            {
                if (dActor.type != ActorType.MOB)
                    return;
                int num = 40 + 10 * (int)level;
                if (SagaLib.Global.Random.Next(0, 99) < num)
                {
                    Map map = Singleton<MapManager>.Instance.GetMap(dActor.MapID);
                    uint id = ((ActorMob)dActor).BaseData.id;
                    short x = dActor.X;
                    short y = dActor.Y;
                    map.DeleteActor(dActor);
                    ActorMob mob = map.SpawnMob(id, x, y, (short)2500, sActor);
                    EnemyCharming.EnemyCharmingBuff enemyCharmingBuff = new EnemyCharming.EnemyCharmingBuff(args.skill, sActor, mob, 600000);
                    SkillHandler.ApplyAddition(sActor, (Addition)enemyCharmingBuff);
                }
            }
        }

        /// <summary>
        /// Defines the <see cref="EnemyCharmingBuff" />.
        /// </summary>
        public class EnemyCharmingBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the mob.
            /// </summary>
            private ActorMob mob;

            /// <summary>
            /// Initializes a new instance of the <see cref="EnemyCharmingBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public EnemyCharmingBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, ActorMob mob, int lifetime)
        : base(skill, actor, nameof(EnemyCharming), lifetime)
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
