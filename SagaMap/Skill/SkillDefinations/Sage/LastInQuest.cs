namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="LastInQuest" />.
    /// </summary>
    public class LastInQuest : ISkill
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
            return Singleton<MapManager>.Instance.GetMap(pc.MapID).CheckActorSkillInRange(dActor.X, dActor.Y, (short)200) ? -17 : 0;
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
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            actor.MapID = dActor.MapID;
            actor.X = dActor.X;
            actor.Y = dActor.Y;
            actor.e = (ActorEventHandler)new NullEventHandler();
            map.RegisterActor((SagaDB.Actor.Actor)actor);
            actor.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)actor);
            actor.Stackable = false;
            new LastInQuest.Activator(sActor, dActor, actor, args, level).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the countMax.
            /// </summary>
            private int countMax = 0;

            /// <summary>
            /// Defines the count.
            /// </summary>
            private int count = 0;

            /// <summary>
            /// Defines the factor.
            /// </summary>
            private float factor = 0.0f;

            /// <summary>
            /// Defines the actor.
            /// </summary>
            private ActorSkill actor;

            /// <summary>
            /// Defines the caster.
            /// </summary>
            private SagaDB.Actor.Actor caster;

            /// <summary>
            /// Defines the skill.
            /// </summary>
            private SkillArg skill;

            /// <summary>
            /// Defines the map.
            /// </summary>
            private Map map;

            /// <summary>
            /// Defines the dActor.
            /// </summary>
            private SagaDB.Actor.Actor dActor;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="caster">The caster<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="theDActor">The theDActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            public Activator(SagaDB.Actor.Actor caster, SagaDB.Actor.Actor theDActor, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                this.map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.period = 1000;
                this.dueTime = 0;
                this.countMax = new int[6] { 0, 3, 3, 4, 4, 5 }[(int)level];
                this.factor = (float)(0.5 + 0.5 * (double)level);
                this.dActor = theDActor;
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
                    if (this.count < this.countMax)
                    {
                        List<SagaDB.Actor.Actor> actorsArea = this.map.GetActorsArea(this.dActor, (short)200, true);
                        List<SagaDB.Actor.Actor> dActor = new List<SagaDB.Actor.Actor>();
                        this.skill.affectedActors.Clear();
                        foreach (SagaDB.Actor.Actor actor in actorsArea)
                        {
                            if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.caster, actor))
                            {
                                硬直 硬直 = new 硬直(this.skill.skill, actor, 500);
                                SkillHandler.ApplyAddition(actor, (Addition)硬直);
                                dActor.Add(actor);
                            }
                        }
                        Singleton<SkillHandler>.Instance.MagicAttack(this.caster, dActor, this.skill, Elements.Neutral, this.factor);
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
                        ++this.count;
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
