namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PoisonMash" />.
    /// </summary>
    public class PoisonMash : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoisonMash"/> class.
        /// </summary>
        public PoisonMash()
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PoisonMash"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public PoisonMash(bool MobUse)
        {
            this.MobUse = MobUse;
        }

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
            if (this.MobUse)
                level = (byte)5;
            new PoisonMash.Activator(sActor, _dActor, args, level).Activate();
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
                this.factor = 0.02f * (float)level;
                this.dueTime = 0;
                this.period = 500;
                this.lifetime = 45000 - 5000 * (int)level;
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
                        List<SagaDB.Actor.Actor> actorsArea = this.map.GetActorsArea((SagaDB.Actor.Actor)this.actor, (short)300, false);
                        List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
                        List<SagaDB.Actor.Actor> dActor = new List<SagaDB.Actor.Actor>();
                        uint num = (uint)((double)this.sActor.MaxHP * (double)this.factor);
                        this.skill.affectedActors.Clear();
                        foreach (SagaDB.Actor.Actor actor in actorsArea)
                        {
                            if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.sActor, actor) && !Singleton<SkillHandler>.Instance.isBossMob(actor))
                                dActor.Add(actor);
                        }
                        Singleton<SkillHandler>.Instance.FixAttack(this.sActor, dActor, this.skill, Elements.Earth, (float)num);
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
                        this.lifetime -= this.period;
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
