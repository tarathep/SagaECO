namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Bind" />.
    /// </summary>
    public class Bind : ISkill
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
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
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
            ActorSkill actorSkill = new ActorSkill(args.skill, sActor);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            actorSkill.MapID = dActor.MapID;
            actorSkill.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actorSkill.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            actorSkill.e = (ActorEventHandler)new NullEventHandler();
            map.RegisterActor((SagaDB.Actor.Actor)actorSkill);
            actorSkill.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorSkill);
            new Bind.Activator((SagaDB.Actor.Actor)actorSkill, args, level).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the actor.
            /// </summary>
            private SagaDB.Actor.Actor actor;

            /// <summary>
            /// Defines the skill.
            /// </summary>
            private SkillArg skill;

            /// <summary>
            /// Defines the map.
            /// </summary>
            private Map map;

            /// <summary>
            /// Defines the level.
            /// </summary>
            private byte level;

            /// <summary>
            /// Defines the lifetime.
            /// </summary>
            private int lifetime;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="_actor">The _actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_args">The _args<see cref="SkillArg"/>.</param>
            /// <param name="_level">The _level<see cref="byte"/>.</param>
            public Activator(SagaDB.Actor.Actor _actor, SkillArg _args, byte _level)
            {
                this.level = _level;
                this.actor = _actor;
                this.skill = _args;
                this.dueTime = 0;
                this.period = 1000;
                this.map = Singleton<MapManager>.Instance.GetMap(this.actor.MapID);
                this.lifetime = 5000 + 1000 * (int)this.level;
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
                        try
                        {
                            List<SagaDB.Actor.Actor> actorsArea = this.map.GetActorsArea(this.actor, (short)300, false);
                            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
                            int rate = 5 + 5 * (int)this.level;
                            foreach (SagaDB.Actor.Actor actor in actorsArea)
                            {
                                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.actor, actor) && Singleton<SkillHandler>.Instance.CanAdditionApply(this.actor, actor, SkillHandler.DefaultAdditions.鈍足, rate))
                                {
                                    鈍足 鈍足 = new 鈍足(this.skill.skill, actor, this.lifetime);
                                    SkillHandler.ApplyAddition(actor, (Addition)鈍足);
                                }
                            }
                            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, this.actor, false);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                    else
                    {
                        this.Deactivate();
                        this.map.DeleteActor(this.actor);
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
