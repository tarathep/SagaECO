namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="HealingTree" />.
    /// </summary>
    public class HealingTree : ISkill
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
            ActorSkill _dActor = new ActorSkill(args.skill, sActor);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            _dActor.MapID = dActor.MapID;
            _dActor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            _dActor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            _dActor.e = (ActorEventHandler)new NullEventHandler();
            map.RegisterActor((SagaDB.Actor.Actor)_dActor);
            _dActor.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)_dActor);
            new HealingTree.Activator(sActor, _dActor, args, level).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Defines the actor.
            /// </summary>
            private ActorSkill actor;

            /// <summary>
            /// Defines the skill.
            /// </summary>
            private SkillArg skill;

            /// <summary>
            /// Defines the factor.
            /// </summary>
            private float factor;

            /// <summary>
            /// Defines the map.
            /// </summary>
            private Map map;

            /// <summary>
            /// Defines the lifetime.
            /// </summary>
            private int lifetime;

            /// <summary>
            /// Defines the mob.
            /// </summary>
            private ActorMob mob;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="_sActor">The _sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_dActor">The _dActor<see cref="ActorSkill"/>.</param>
            /// <param name="_args">The _args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            public Activator(SagaDB.Actor.Actor _sActor, ActorSkill _dActor, SkillArg _args, byte level)
            {
                this.sActor = _sActor;
                this.actor = _dActor;
                this.skill = _args.Clone();
                this.factor = 0.1f * (float)level;
                this.dueTime = 1000;
                this.period = 1000;
                this.lifetime = 5000 * (int)level;
                this.map = Singleton<MapManager>.Instance.GetMap(this.actor.MapID);
                this.mob = this.map.SpawnMob(30480000U, this.actor.X, this.actor.Y, (short)2500, this.sActor);
            }

            /// <summary>
            /// The CallBack.
            /// </summary>
            /// <param name="o">The o<see cref="object"/>.</param>
            public override void CallBack(object o)
            {
                ClientManager.EnterCriticalArea();
                uint HP_Add = 10U * (uint)this.skill.skill.Level;
                try
                {
                    this.skill.affectedActors.Clear();
                    if (this.lifetime > 0)
                    {
                        this.lifetime -= this.period;
                        foreach (SagaDB.Actor.Actor act in this.map.GetActorsArea((SagaDB.Actor.Actor)this.actor, (short)200, false))
                        {
                            if (act.type == ActorType.PC || act.type == ActorType.PET || act.type == ActorType.SHADOW)
                                this.RecoverHP(act, HP_Add);
                        }
                        this.skill.Init();
                    }
                    else
                    {
                        this.Deactivate();
                        this.map.DeleteActor((SagaDB.Actor.Actor)this.actor);
                        this.map.DeleteActor((SagaDB.Actor.Actor)this.mob);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
            }

            /// <summary>
            /// The RecoverHP.
            /// </summary>
            /// <param name="act">The act<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="HP_Add">The HP_Add<see cref="uint"/>.</param>
            public void RecoverHP(SagaDB.Actor.Actor act, uint HP_Add)
            {
                Singleton<SkillHandler>.Instance.FixAttack(this.sActor, act, this.skill, Elements.Holy, (float)-HP_Add);
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
            }
        }
    }
}
