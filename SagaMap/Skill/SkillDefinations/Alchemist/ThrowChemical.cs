namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System;

    /// <summary>
    /// Defines the <see cref="ThrowChemical" />.
    /// </summary>
    public class ThrowChemical : ISkill
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
            new ThrowChemical.Activator(sActor, _dActor, args, level).Activate();
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
            /// Defines the rate.
            /// </summary>
            private int rate;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="_sActor">The _sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_dActor">The _dActor<see cref="ActorSkill"/>.</param>
            /// <param name="_args">The _args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            public Activator(SagaDB.Actor.Actor _sActor, ActorSkill _dActor, SkillArg _args, byte level)
            {
                int[] numArray = new int[6]
                {
          0,
          15000,
          15000,
          20000,
          20000,
          18000
                };
                this.sActor = _sActor;
                this.actor = _dActor;
                this.skill = _args.Clone();
                this.factor = 0.1f * (float)level;
                this.dueTime = 1000;
                this.period = numArray[(int)level];
                this.lifetime = 20000;
                this.rate = 40 - 5 * (int)level;
                this.map = Singleton<MapManager>.Instance.GetMap(this.actor.MapID);
            }

            /// <summary>
            /// The CallBack.
            /// </summary>
            /// <param name="o">The o<see cref="object"/>.</param>
            public override void CallBack(object o)
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (this.lifetime > 0)
                    {
                        this.lifetime -= this.period;
                        foreach (SagaDB.Actor.Actor actor in this.map.GetActorsArea((SagaDB.Actor.Actor)this.actor, (short)150, false))
                        {
                            if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.sActor, actor))
                            {
                                switch (this.skill.skill.Level)
                                {
                                    case 1:
                                        if (Singleton<SkillHandler>.Instance.CanAdditionApply(this.sActor, actor, SkillHandler.DefaultAdditions.鈍足, this.rate))
                                        {
                                            鈍足 鈍足 = new 鈍足(this.skill.skill, actor, 10000);
                                            SkillHandler.ApplyAddition(actor, (Addition)鈍足);
                                            break;
                                        }
                                        break;
                                    case 2:
                                        if (Singleton<SkillHandler>.Instance.CanAdditionApply(this.sActor, actor, SkillHandler.DefaultAdditions.Silence, this.rate))
                                        {
                                            Silence silence = new Silence(this.skill.skill, actor, 10000);
                                            SkillHandler.ApplyAddition(actor, (Addition)silence);
                                            break;
                                        }
                                        break;
                                    case 3:
                                        if (Singleton<SkillHandler>.Instance.CanAdditionApply(this.sActor, actor, SkillHandler.DefaultAdditions.Poison, this.rate))
                                        {
                                            Poison poison = new Poison(this.skill.skill, actor, 10000);
                                            SkillHandler.ApplyAddition(actor, (Addition)poison);
                                            break;
                                        }
                                        break;
                                    case 4:
                                        if (Singleton<SkillHandler>.Instance.CanAdditionApply(this.sActor, actor, SkillHandler.DefaultAdditions.Confuse, this.rate))
                                        {
                                            Confuse confuse = new Confuse(this.skill.skill, actor, 10000);
                                            SkillHandler.ApplyAddition(actor, (Addition)confuse);
                                            break;
                                        }
                                        break;
                                    case 5:
                                        if (Singleton<SkillHandler>.Instance.CanAdditionApply(this.sActor, actor, SkillHandler.DefaultAdditions.Stun, this.rate))
                                        {
                                            Stun stun = new Stun(this.skill.skill, actor, 10000);
                                            SkillHandler.ApplyAddition(actor, (Addition)stun);
                                            break;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.Deactivate();
                        this.map.DeleteActor((SagaDB.Actor.Actor)this.actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
