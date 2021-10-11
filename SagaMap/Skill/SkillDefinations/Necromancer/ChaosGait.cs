namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ChaosGait" />.
    /// </summary>
    public class ChaosGait : ISkill
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
            try
            {
                if (Singleton<MapManager>.Instance.GetMap(sActor.MapID).CheckActorSkillInRange(dActor.X, dActor.Y, (short)200))
                    return -17;
            }
            catch (Exception ex)
            {
            }
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
            ActorSkill _Actor = new ActorSkill(args.skill, sActor);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            _Actor.MapID = dActor.MapID;
            _Actor.X = dActor.X;
            _Actor.Y = dActor.Y;
            _Actor.e = (ActorEventHandler)new NullEventHandler();
            map.RegisterActor((SagaDB.Actor.Actor)_Actor);
            _Actor.invisble = false;
            _Actor.Stackable = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)_Actor);
            new ChaosGait.Activator(sActor, dActor, _Actor, args, level).Activate();
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
            /// Defines the dActor.
            /// </summary>
            private SagaDB.Actor.Actor dActor;

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
            /// Defines the nowTimes.
            /// </summary>
            private int nowTimes;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="_sActor">The _sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_dActor">The _dActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_Actor">The _Actor<see cref="ActorSkill"/>.</param>
            /// <param name="_args">The _args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            public Activator(SagaDB.Actor.Actor _sActor, SagaDB.Actor.Actor _dActor, ActorSkill _Actor, SkillArg _args, byte level)
            {
                this.sActor = _sActor;
                this.dActor = _dActor;
                this.actor = _Actor;
                this.skill = _args.Clone();
                this.factor = (float)(0.100000001490116 + 0.5 * (double)level);
                this.times = new int[6] { 0, 4, 4, 5, 5, 6 }[(int)level];
                this.nowTimes = 0;
                this.dueTime = 1000;
                this.period = 1000;
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
                    if (this.nowTimes < this.times)
                    {
                        Map map = Singleton<MapManager>.Instance.GetMap(this.sActor.MapID);
                        List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(this.dActor, (short)200, true);
                        List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                        foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
                        {
                            if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.sActor, dActor2))
                                dActor1.Add(dActor2);
                        }
                        Singleton<SkillHandler>.Instance.MagicAttack(this.sActor, dActor1, this.skill, Elements.Dark, this.factor);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
                        ++this.nowTimes;
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
