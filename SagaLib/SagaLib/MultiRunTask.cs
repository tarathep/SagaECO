namespace SagaLib
{
    using System;

    /// <summary>
    /// Defines the <see cref="MultiRunTask" />.
    /// </summary>
    public class MultiRunTask
    {
        /// <summary>
        /// Defines the NextUpdateTime.
        /// </summary>
        public DateTime NextUpdateTime = DateTime.Now;

        /// <summary>
        /// Defines the dueTime.
        /// </summary>
        public int dueTime;

        /// <summary>
        /// Defines the period.
        /// </summary>
        public int period;

        /// <summary>
        /// Defines the Func.
        /// </summary>
        public MultiRunTask.func Func;

        /// <summary>
        /// Defines the activate.
        /// </summary>
        private bool activate;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiRunTask"/> class.
        /// </summary>
        public MultiRunTask()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiRunTask"/> class.
        /// </summary>
        /// <param name="dueTime">The dueTime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        public MultiRunTask(int dueTime, int period)
        {
            this.dueTime = dueTime;
            this.period = period;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public virtual void CallBack(object o)
        {
            if (this.Func == null)
                return;
            this.Func();
        }

        /// <summary>
        /// The Activated.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Activated()
        {
            return this.activate;
        }

        /// <summary>
        /// The Activate.
        /// </summary>
        public void Activate()
        {
            this.NextUpdateTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, this.dueTime);
            Singleton<TaskManager>.Instance.RegisterTask(this);
            this.activate = true;
        }

        /// <summary>
        /// The Deactivate.
        /// </summary>
        public virtual void Deactivate()
        {
            Singleton<TaskManager>.Instance.RemoveTask(this);
            this.activate = false;
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this.name != null)
                return this.name;
            return base.ToString();
        }

        /// <summary>
        /// The func.
        /// </summary>
        public delegate void func();
    }
}
