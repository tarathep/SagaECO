namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AcidMist" />.
    /// </summary>
    public class AcidMist : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            if ((int)args.x >= (int)map.Width || (int)args.y >= (int)map.Height)
                return -6;
            return map.Info.earth[(int)args.x, (int)args.y] > (byte)0 ? 0 : -12;
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
            new AcidMist.Activator(sActor, _dActor, args, level).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the count.
            /// </summary>
            private int count = 0;

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
            /// Defines the times.
            /// </summary>
            private int times;

            /// <summary>
            /// Defines the lifetime.
            /// </summary>
            private int lifetime;

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
                this.factor = (float)(1.5 + 0.100000001490116 * (double)level);
                this.dueTime = 0;
                this.times = 30 - 5 * (int)level;
                this.period = 500;
                this.lifetime = 35000 - 5000 * (int)level;
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
                    if (this.count < this.times && this.lifetime > 0)
                    {
                        List<SagaDB.Actor.Actor> actorsArea = this.map.GetActorsArea((SagaDB.Actor.Actor)this.actor, (short)300, false);
                        List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                        this.skill.affectedActors.Clear();
                        foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
                        {
                            if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.sActor, dActor2))
                            {
                                dActor1.Add(dActor2);
                                ++this.count;
                            }
                        }
                        Singleton<SkillHandler>.Instance.MagicAttack(this.sActor, dActor1, this.skill, Elements.Earth, this.factor);
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
                    }
                    else
                    {
                        this.Deactivate();
                        this.map.DeleteActor((SagaDB.Actor.Actor)this.actor);
                    }
                    this.lifetime -= this.period;
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
