namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;
    using SagaMap.PC;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DefaultPassiveSkill" />.
    /// </summary>
    public class DefaultPassiveSkill : Addition
    {
        /// <summary>
        /// Defines the Variable.
        /// </summary>
        public Dictionary<string, int> Variable = new Dictionary<string, int>();

        /// <summary>
        /// Defines the canEnd.
        /// </summary>
        private bool canEnd = false;

        /// <summary>
        /// Defines the skill.
        /// </summary>
        public SagaDB.Skill.Skill skill;

        /// <summary>
        /// Defines the activate.
        /// </summary>
        private bool activate;

        /// <summary>
        /// Defines the endTime.
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// Defines the period.
        /// </summary>
        private int period;

        /// <summary>
        /// Defines the lifeTime.
        /// </summary>
        private int lifeTime;

        /// <summary>
        /// Defines the OnAdditionStart.
        /// </summary>
        public event DefaultPassiveSkill.StartEventHandler OnAdditionStart;

        /// <summary>
        /// Defines the OnAdditionEnd.
        /// </summary>
        public event DefaultPassiveSkill.EndEventHandler OnAdditionEnd;

        /// <summary>
        /// Defines the OnUpdate.
        /// </summary>
        public event DefaultPassiveSkill.UpdateEventHandler OnUpdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPassiveSkill"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">Actor, which this addition get attached to.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="ifActivate">The ifActivate<see cref="bool"/>.</param>
        public DefaultPassiveSkill(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name, bool ifActivate)
        {
            this.Name = name;
            this.skill = skill;
            this.AttachedActor = actor;
            this.activate = ifActivate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPassiveSkill"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="ifActivate">The ifActivate<see cref="bool"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public DefaultPassiveSkill(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name, bool ifActivate, int period, int lifetime)
        {
            this.Name = name;
            this.skill = skill;
            this.AttachedActor = actor;
            this.activate = ifActivate;
            this.period = period;
            this.lifeTime = lifetime;
        }

        /// <summary>
        /// Gets a value indicating whether IfActivate.
        /// </summary>
        public override bool IfActivate
        {
            get
            {
                return this.activate;
            }
        }


        public int this[string name]
        {
            get
            {
                if (this.Variable.ContainsKey(name))
                    return this.Variable[name];
                return 0;
            }
            set
            {
                if (this.Variable.ContainsKey(name))
                    this.Variable.Remove(name);
                this.Variable.Add(name, value);
            }
        }
        /// <summary>
        /// Gets the RestLifeTime.
        /// </summary>
        public override int RestLifeTime
        {
            get
            {
                return (int)(this.endTime - DateTime.Now).TotalMilliseconds;
            }
        }

        /// <summary>
        /// Gets the TotalLifeTime.
        /// </summary>
        public override int TotalLifeTime
        {
            get
            {
                return this.lifeTime;
            }
        }

        /// <summary>
        /// The AdditionEnd.
        /// </summary>
        public override void AdditionEnd()
        {
            if (this.lifeTime != 0)
                this.TimerEnd();
            if (this.canEnd && this.AttachedActor.Status != null)
                this.OnAdditionEnd(this.AttachedActor, this);
            if (this.AttachedActor.type != ActorType.PC || this.AttachedActor.Status == null)
                return;
            ActorPC attachedActor = (ActorPC)this.AttachedActor;
            Singleton<StatusFactory>.Instance.CalcStatus(attachedActor);
            MapClient.FromActorPC(attachedActor).SendPlayerInfo();
        }

        /// <summary>
        /// The AdditionStart.
        /// </summary>
        public override void AdditionStart()
        {
            this.canEnd = true;
            if (this.lifeTime != 0)
            {
                this.endTime = DateTime.Now + new TimeSpan(0, this.lifeTime / 60000, this.lifeTime / 1000 % 60);
                this.InitTimer(this.period, 0);
                this.TimerStart();
            }
            if (this.AttachedActor.Status != null)
                this.OnAdditionStart(this.AttachedActor, this);
            if (this.AttachedActor.type != ActorType.PC)
                return;
            ActorPC attachedActor = (ActorPC)this.AttachedActor;
            Singleton<StatusFactory>.Instance.CalcStatus(attachedActor);
            MapClient.FromActorPC(attachedActor).SendPlayerInfo();
        }

        /// <summary>
        /// The OnTimerUpdate.
        /// </summary>
        public override void OnTimerUpdate()
        {
            this.OnUpdate(this.AttachedActor, this);
        }

        /// <summary>
        /// The OnTimerEnd.
        /// </summary>
        public override void OnTimerEnd()
        {
            this.AdditionEnd();
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        public delegate void StartEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill);

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        public delegate void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill);

        /// <summary>
        /// The UpdateEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        public delegate void UpdateEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill);
    }
}
