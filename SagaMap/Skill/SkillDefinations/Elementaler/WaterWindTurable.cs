namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WaterWindTurable" />.
    /// </summary>
    internal class WaterWindTurable : ISkill
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
            return map.CheckActorSkillInRange(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)300) ? -17 : 0;
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
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            actor.e = (ActorEventHandler)new NullEventHandler();
            actor.Stackable = false;
            map.RegisterActor((SagaDB.Actor.Actor)actor);
            actor.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)actor);
            new WaterWindTurable.Activator(sActor, actor, args, level).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the countMax.
            /// </summary>
            private int countMax = 3;

            /// <summary>
            /// Defines the count.
            /// </summary>
            private int count = 0;

            /// <summary>
            /// Defines the factor.
            /// </summary>
            private float factor = 1f;

            /// <summary>
            /// Defines the TotalLv.
            /// </summary>
            private int TotalLv = 0;

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
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="caster">The caster<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            public Activator(SagaDB.Actor.Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                this.map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;
                ActorPC actorPc = (ActorPC)caster;
                switch (level)
                {
                    case 1:
                        this.factor *= 1f;
                        this.countMax = 3;
                        break;
                    case 2:
                        this.factor *= 1.2f;
                        this.countMax = 4;
                        break;
                    case 3:
                        this.factor *= 1.4f;
                        this.countMax = 4;
                        break;
                    case 4:
                        this.factor *= 1.6f;
                        this.countMax = 5;
                        break;
                    case 5:
                        this.factor *= 1.8f;
                        this.countMax = 5;
                        break;
                }
                if (actorPc.Skills2.ContainsKey(3036U))
                {
                    this.TotalLv = (int)actorPc.Skills2[3036U].BaseData.lv;
                    if (this.TotalLv == 2 || this.TotalLv == 1)
                        this.factor += 0.3f;
                    else if (this.TotalLv == 4 || this.TotalLv == 3)
                        this.factor += 0.6f;
                    else if (this.TotalLv == 5)
                        this.factor += 0.9f;
                }
                if (actorPc.SkillsReserve.ContainsKey(3036U))
                {
                    this.TotalLv = (int)actorPc.SkillsReserve[3036U].BaseData.lv;
                    if (this.TotalLv == 2 || this.TotalLv == 1)
                        this.factor += 0.3f;
                    else if (this.TotalLv == 4 || this.TotalLv == 3)
                        this.factor += 0.6f;
                    else if (this.TotalLv == 5)
                        this.factor += 0.9f;
                }
                if (actorPc.Skills2.ContainsKey(3025U))
                {
                    this.TotalLv = (int)actorPc.Skills2[3025U].BaseData.lv;
                    if (this.TotalLv == 3 || this.TotalLv == 2)
                        this.factor += 0.3f;
                    else if (this.TotalLv == 5 || this.TotalLv == 4)
                        this.factor += 0.6f;
                }
                if (!actorPc.SkillsReserve.ContainsKey(3025U))
                    return;
                this.TotalLv = (int)actorPc.SkillsReserve[3025U].BaseData.lv;
                if (this.TotalLv == 3 || this.TotalLv == 2)
                    this.factor += 0.3f;
                else if (this.TotalLv == 5 || this.TotalLv == 4)
                    this.factor += 0.6f;
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
                        List<SagaDB.Actor.Actor> actorsArea = this.map.GetActorsArea((SagaDB.Actor.Actor)this.actor, (short)300, false);
                        List<SagaDB.Actor.Actor> dActor = new List<SagaDB.Actor.Actor>();
                        this.skill.affectedActors.Clear();
                        foreach (SagaDB.Actor.Actor actor in actorsArea)
                        {
                            if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.caster, actor))
                            {
                                硬直 硬直 = new 硬直(this.skill.skill, actor, 400);
                                SkillHandler.ApplyAddition(actor, (Addition)硬直);
                                dActor.Add(actor);
                            }
                        }
                        Singleton<SkillHandler>.Instance.MagicAttack(this.caster, dActor, this.skill, Elements.Water, this.factor);
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
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
                ++this.count;
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
