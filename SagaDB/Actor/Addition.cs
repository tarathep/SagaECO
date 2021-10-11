namespace SagaDB.Actor
{
    using SagaLib;
    using System;

    /// <summary>
    /// A class which contains the information about a players addition bonus (such as Buff).
    /// </summary>
    public abstract class Addition
    {
        /// <summary>
        /// Defines the m_myActor.
        /// </summary>
        private SagaDB.Actor.Actor m_myActor;

        /// <summary>
        /// Defines the m_task.
        /// </summary>
        internal MultiRunTask m_task;

        /// <summary>
        /// Defines the m_starttime.
        /// </summary>
        private DateTime m_starttime;

        /// <summary>
        /// Defines the m_name.
        /// </summary>
        private string m_name;

        /// <summary>
        /// Defines the m_activated.
        /// </summary>
        private bool m_activated;

        /// <summary>
        /// Defines the MyType.
        /// </summary>
        public Addition.AdditionType MyType;

        /// <summary>
        /// Gets or sets the AttachedActor.
        /// </summary>
        public SagaDB.Actor.Actor AttachedActor
        {
            get
            {
                return this.m_myActor;
            }
            set
            {
                this.m_myActor = value;
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_name;
            }
            set
            {
                this.m_name = value;
            }
        }

        /// <summary>
        /// Gets the Interval.
        /// </summary>
        public int Interval
        {
            get
            {
                if (this.m_task != null)
                    return this.m_task.period;
                return -1;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Activated.
        /// </summary>
        public bool Activated
        {
            get
            {
                return this.m_activated;
            }
            set
            {
                this.m_activated = value;
            }
        }

        /// <summary>
        /// Gets or sets the StartTime.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.m_starttime;
            }
            set
            {
                this.m_starttime = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IfActivate.
        /// </summary>
        public virtual bool IfActivate
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Gets or sets the TotalLifeTime.
        /// </summary>
        public virtual int TotalLifeTime
        {
            get
            {
                return int.MaxValue;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets the RestLifeTime.
        /// </summary>
        public virtual int RestLifeTime
        {
            get
            {
                return int.MaxValue;
            }
        }

        /// <summary>
        /// The AdditionStart.
        /// </summary>
        public abstract void AdditionStart();

        /// <summary>
        /// The AdditionEnd.
        /// </summary>
        public abstract void AdditionEnd();

        /// <summary>
        /// Method that be called once Timer call back function get invoked.
        /// </summary>
        public virtual void OnTimerUpdate()
        {
        }

        /// <summary>
        /// The OnTimerStart.
        /// </summary>
        public virtual void OnTimerStart()
        {
        }

        /// <summary>
        /// The OnTimerEnd.
        /// </summary>
        public virtual void OnTimerEnd()
        {
        }

        /// <summary>
        /// The InitTimer.
        /// </summary>
        /// <param name="interval">Interval.</param>
        /// <param name="duetime">Due Time.</param>
        protected void InitTimer(int interval, int duetime)
        {
            this.m_task = new MultiRunTask(duetime, interval);
            this.m_task.Name = this.Name;
            this.m_task.Func = new MultiRunTask.func(this.timerCallback);
        }

        /// <summary>
        /// The TimerStart.
        /// </summary>
        protected void TimerStart()
        {
            if (this.m_task == null)
                return;
            this.m_task.Activate();
        }

        /// <summary>
        /// The TimerEnd.
        /// </summary>
        protected void TimerEnd()
        {
            if (this.m_task == null)
                return;
            this.m_task.Deactivate();
        }

        /// <summary>
        /// The timerCallback.
        /// </summary>
        internal void timerCallback()
        {
            if (this.RestLifeTime > 100)
            {
                this.OnTimerUpdate();
            }
            else
            {
                this.m_task.Deactivate();
                this.OnTimerEnd();
            }
        }

        /// <summary>
        /// Defines the AdditionType.
        /// </summary>
        public enum AdditionType
        {
            /// <summary>
            /// Defines the PassiveSkill.
            /// </summary>
            PassiveSkill,

            /// <summary>
            /// Defines the Buff.
            /// </summary>
            Buff,

            /// <summary>
            /// Defines the Debuff.
            /// </summary>
            Debuff,
        }
    }
}
