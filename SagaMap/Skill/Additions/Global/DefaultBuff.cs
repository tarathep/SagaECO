namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;
    using SagaMap.PC;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DefaultBuff" />.
    /// </summary>
    public class DefaultBuff : Addition
    {
        /// <summary>
        /// Defines the Variable.
        /// </summary>
        public Dictionary<string, int> Variable = new Dictionary<string, int>();

        /// <summary>
        /// Defines the endTime.
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// Defines the lifeTime.
        /// </summary>
        private int lifeTime;

        /// <summary>
        /// Defines the period.
        /// </summary>
        private int period;

        /// <summary>
        /// Defines the skill.
        /// </summary>
        public SagaDB.Skill.Skill skill;

        /// <summary>
        /// Defines the OnCheckValid.
        /// </summary>
        public DefaultBuff.ValidCheckEventHandler OnCheckValid;

        /// <summary>
        /// Defines the OnAdditionStart.
        /// </summary>
        public event DefaultBuff.StartEventHandler OnAdditionStart;

        /// <summary>
        /// Defines the OnAdditionEnd.
        /// </summary>
        public event DefaultBuff.EndEventHandler OnAdditionEnd;

        /// <summary>
        /// Defines the OnUpdate.
        /// </summary>
        public event DefaultBuff.UpdateEventHandler OnUpdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultBuff"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public DefaultBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name, int lifetime)
      : this(skill, actor, name, lifetime, lifetime)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultBuff"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        public DefaultBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string name, int lifetime, int period)
        {
            this.Name = name;
            this.skill = skill;
            this.AttachedActor = actor;
            this.lifeTime = lifetime;
            this.period = period;
        }


        public int this[string name]
        {
            get
            {
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();
                int num = !this.Variable.ContainsKey(name) ? 0 : this.Variable[name];
                if (!blocked)
                    ClientManager.LeaveCriticalArea();
                return num;
            }
            set
            {
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();
                if (this.Variable.ContainsKey(name))
                    this.Variable.Remove(name);
                this.Variable.Add(name, value);
                if (blocked)
                    return;
                ClientManager.LeaveCriticalArea();
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
        /// Gets or sets the TotalLifeTime.
        /// </summary>
        public override int TotalLifeTime
        {
            get
            {
                return this.lifeTime;
            }
            set
            {
                int milliseconds = value - this.lifeTime;
                this.lifeTime = value;
                this.endTime += new TimeSpan(0, 0, 0, 0, milliseconds);
            }
        }

        /// <summary>
        /// The AdditionEnd.
        /// </summary>
        public override void AdditionEnd()
        {
            SkillHandler.RemoveAddition(this.AttachedActor, (Addition)this, true);
            this.TimerEnd();
            if (this.OnAdditionEnd != null && this.AttachedActor.Status != null)
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
            if (this.period != int.MaxValue)
            {
                this.endTime = DateTime.Now + new TimeSpan(0, this.lifeTime / 60000, this.lifeTime / 1000 % 60);
                this.InitTimer(this.period, 0);
                this.TimerStart();
            }
            if (this.OnAdditionStart != null && this.AttachedActor.Status != null)
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
            if (this.OnUpdate == null)
                return;
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
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        public delegate void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill);

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        public delegate void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill);

        /// <summary>
        /// The UpdateEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        public delegate void UpdateEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill);

        /// <summary>
        /// The ValidCheckEventHandler.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="result">The result<see cref="int"/>.</param>
        public delegate void ValidCheckEventHandler(ActorPC sActor, SagaDB.Actor.Actor dActor, out int result);
    }
}
