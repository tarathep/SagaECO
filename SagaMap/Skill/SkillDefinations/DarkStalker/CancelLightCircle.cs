namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="CancelLightCircle" />.
    /// </summary>
    public class CancelLightCircle : ISkill
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
            return map.CheckActorSkillInRange(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)100) ? -17 : 0;
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
            _dActor.Stackable = false;
            new CancelLightCircle.Activator(sActor, _dActor, args, level).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the times.
            /// </summary>
            private int times = 0;

            /// <summary>
            /// Defines the olight.
            /// </summary>
            private byte[,] olight = new byte[3, 3];

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
                this.dueTime = 0;
                this.period = 1000;
                this.lifetime = 25000 + 5000 * (int)level;
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
                        if (this.times == 0)
                        {
                            for (byte index1 = (byte)((uint)this.skill.x - 1U); (int)index1 < (int)this.skill.x + 1; ++index1)
                            {
                                for (byte index2 = (byte)((uint)this.skill.y - 1U); (int)index2 < (int)this.skill.y + 1; ++index2)
                                {
                                    this.olight[(int)index1 - (int)this.skill.x + 1, (int)index2 - (int)this.skill.y + 1] = this.map.Info.holy[(int)index1, (int)index2];
                                    this.map.Info.holy[(int)index1, (int)index2] = (byte)0;
                                }
                            }
                        }
                        ++this.times;
                        this.lifetime -= this.period;
                    }
                    else
                    {
                        for (byte index1 = (byte)((uint)this.skill.x - 1U); (int)index1 < (int)this.skill.x + 1; ++index1)
                        {
                            for (byte index2 = (byte)((uint)this.skill.y - 1U); (int)index2 < (int)this.skill.y + 1; ++index2)
                                this.map.Info.holy[(int)index1, (int)index2] = this.olight[(int)index1 - (int)this.skill.x + 1, (int)index2 - (int)this.skill.y + 1];
                        }
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
