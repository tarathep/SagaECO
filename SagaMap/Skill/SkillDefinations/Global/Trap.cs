namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System;

    /// <summary>
    /// Defines the <see cref="Trap" />.
    /// </summary>
    public class Trap : ISkill
    {
        /// <summary>
        /// Defines the factor.
        /// </summary>
        public float factor = 1f;

        /// <summary>
        /// Defines the posType.
        /// </summary>
        public Trap.PosType posType;

        /// <summary>
        /// Defines the Range.
        /// </summary>
        public uint Range;

        /// <summary>
        /// Defines the OneTimes.
        /// </summary>
        public bool OneTimes;

        /// <summary>
        /// Defines the LifeTime.
        /// </summary>
        public int LifeTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trap"/> class.
        /// </summary>
        /// <param name="OneTimes">The OneTimes<see cref="bool"/>.</param>
        /// <param name="Range">The Range<see cref="uint"/>.</param>
        /// <param name="posType">The posType<see cref="Trap.PosType"/>.</param>
        public Trap(bool OneTimes, uint Range, Trap.PosType posType)
        {
            this.LifeTime = 0;
            this.OneTimes = OneTimes;
            this.Range = Range;
            this.posType = posType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trap"/> class.
        /// </summary>
        /// <param name="OneTimes">The OneTimes<see cref="bool"/>.</param>
        /// <param name="posType">The posType<see cref="Trap.PosType"/>.</param>
        public Trap(bool OneTimes, Trap.PosType posType)
        {
            this.LifeTime = 0;
            this.OneTimes = OneTimes;
            this.Range = 100U;
            this.posType = posType;
        }

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
            this.BeforeProc(sActor, dActor, args, level);
            this.factor = 1f;
            if (sActor.Status.Additions.ContainsKey("TrapDamUp"))
                this.factor = (float)(1.10000002384186 + 0.0199999995529652 * (double)((DefaultPassiveSkill)sActor.Status.Additions["TrapDamUp"]).skill.Level);
            ActorSkill _dActor = new ActorSkill(args.skill, sActor);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            Trap.Activator activator = new Trap.Activator(sActor, _dActor, args, level, this.LifeTime, this.OneTimes, this.factor);
            if (this.posType == Trap.PosType.sActor)
            {
                _dActor.MapID = sActor.MapID;
                _dActor.X = sActor.X;
                _dActor.Y = sActor.Y;
            }
            else
            {
                _dActor.MapID = sActor.MapID;
                _dActor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
                _dActor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            }
            SkillEventHandler skillEventHandler = new SkillEventHandler();
            skillEventHandler.ActorMove += new SkillEventHandler.MoveEventHandler(activator.ActorMoveEvent);
            _dActor.e = (ActorEventHandler)skillEventHandler;
            _dActor.sightRange = this.Range;
            map.RegisterActor((SagaDB.Actor.Actor)_dActor);
            _dActor.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)_dActor);
            activator.ProcSkill += new Trap.Activator.ProcSkillHandler(this.ProcSkill);
            activator.OnTimer += new Trap.Activator.OnTimerHandler(this.OnTimer);
            activator.Activate();
        }

        /// <summary>
        /// The ProcSkill.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="map">The map<see cref="Map"/>.</param>
        /// <param name="level">The level<see cref="int"/>.</param>
        /// <param name="factor">The factor<see cref="float"/>.</param>
        public virtual void ProcSkill(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor)
        {
        }

        /// <summary>
        /// The BeforeProc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public virtual void BeforeProc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
        }

        /// <summary>
        /// The OnTimer.
        /// </summary>
        /// <param name="timer">The timer<see cref="Trap.Activator"/>.</param>
        public virtual void OnTimer(Trap.Activator timer)
        {
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        public class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the State.
            /// </summary>
            public int State = 0;

            /// <summary>
            /// Defines the sActor.
            /// </summary>
            public SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Defines the actor.
            /// </summary>
            public ActorSkill actor;

            /// <summary>
            /// Defines the skill.
            /// </summary>
            public SkillArg skill;

            /// <summary>
            /// Defines the map.
            /// </summary>
            public Map map;

            /// <summary>
            /// Defines the lifetime.
            /// </summary>
            public int lifetime;

            /// <summary>
            /// Defines the level.
            /// </summary>
            public int level;

            /// <summary>
            /// Defines the OneTimes.
            /// </summary>
            public bool OneTimes;

            /// <summary>
            /// Defines the factor.
            /// </summary>
            public float factor;

            /// <summary>
            /// Defines the ProcSkill.
            /// </summary>
            public event Trap.Activator.ProcSkillHandler ProcSkill;

            /// <summary>
            /// Defines the OnTimer.
            /// </summary>
            public event Trap.Activator.OnTimerHandler OnTimer;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="_sActor">The _sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_dActor">The _dActor<see cref="ActorSkill"/>.</param>
            /// <param name="_args">The _args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="OneTimes">The OneTimes<see cref="bool"/>.</param>
            /// <param name="factor">The factor<see cref="float"/>.</param>
            public Activator(SagaDB.Actor.Actor _sActor, ActorSkill _dActor, SkillArg _args, byte level, int lifetime, bool OneTimes, float factor)
            {
                this.sActor = _sActor;
                this.actor = _dActor;
                this.skill = _args.Clone();
                this.dueTime = 0;
                this.period = 1000;
                this.lifetime = lifetime;
                this.level = (int)level;
                this.map = Singleton<MapManager>.Instance.GetMap(this.sActor.MapID);
                this.OneTimes = OneTimes;
                this.factor = factor;
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
                        if (this.OnTimer != null)
                            this.OnTimer(this);
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

            /// <summary>
            /// The ActorMoveEvent.
            /// </summary>
            /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="pos">The pos<see cref="short[]"/>.</param>
            /// <param name="dir">The dir<see cref="ushort"/>.</param>
            /// <param name="speed">The speed<see cref="ushort"/>.</param>
            public void ActorMoveEvent(SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
            {
                if (!Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.sActor, mActor) || this.ProcSkill == null)
                    return;
                this.ProcSkill(this.sActor, mActor, this.actor, this.skill, this.map, this.level, this.factor);
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
                if (this.OneTimes)
                {
                    this.Deactivate();
                    this.map.DeleteActor((SagaDB.Actor.Actor)this.actor);
                }
            }

            /// <summary>
            /// The ProcSkillHandler.
            /// </summary>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="map">The map<see cref="Map"/>.</param>
            /// <param name="level">The level<see cref="int"/>.</param>
            /// <param name="factor">The factor<see cref="float"/>.</param>
            public delegate void ProcSkillHandler(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor mActor, ActorSkill actor, SkillArg args, Map map, int level, float factor);

            /// <summary>
            /// The OnTimerHandler.
            /// </summary>
            /// <param name="timer">The timer<see cref="Trap.Activator"/>.</param>
            public delegate void OnTimerHandler(Trap.Activator timer);
        }

        /// <summary>座標跟誰相同</summary>
        public enum PosType
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            sActor,

            /// <summary>
            /// Defines the args.
            /// </summary>
            args,
        }
    }
}
