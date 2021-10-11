namespace SagaLib
{
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="SingleRunTask" />.
    /// </summary>
    public class SingleRunTask
    {
        /// <summary>
        /// Defines the myTimer.
        /// </summary>
        private Timer myTimer;

        /// <summary>
        /// Defines the dueTime.
        /// </summary>
        public int dueTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleRunTask"/> class.
        /// </summary>
        public SingleRunTask()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleRunTask"/> class.
        /// </summary>
        /// <param name="dueTime">The dueTime<see cref="int"/>.</param>
        public SingleRunTask(int dueTime)
        {
            this.dueTime = dueTime;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public virtual void CallBack(object o)
        {
        }

        /// <summary>
        /// The Activate.
        /// </summary>
        public void Activate()
        {
            this.myTimer = new Timer(new TimerCallback(this.CallBack), (object)null, this.dueTime, -1);
        }

        /// <summary>
        /// The Deactivate.
        /// </summary>
        public void Deactivate()
        {
            this.myTimer.Dispose();
        }
    }
}
