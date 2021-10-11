namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="Timer" />.
    /// </summary>
    public class Timer : MultiRunTask
    {
        /// <summary>
        /// Defines the customObjects.
        /// </summary>
        private List<object> customObjects = new List<object>();

        /// <summary>
        /// Defines the pc.
        /// </summary>
        private ActorPC pc;

        /// <summary>
        /// Defines the needScript.
        /// </summary>
        private bool needScript;

        /// <summary>
        /// Defines the OnTimerCall.
        /// </summary>
        public event TimerCallback OnTimerCall;

        /// <summary>
        /// Gets or sets the AttachedPC.
        /// </summary>
        public ActorPC AttachedPC
        {
            get
            {
                return this.pc;
            }
            set
            {
                this.pc = value;
            }
        }

        /// <summary>
        /// Gets the CustomObjects.
        /// </summary>
        public List<object> CustomObjects
        {
            get
            {
                return this.customObjects;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether NeedScript.
        /// </summary>
        public bool NeedScript
        {
            get
            {
                return this.needScript;
            }
            set
            {
                this.needScript = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        /// <param name="due">The due<see cref="int"/>.</param>
        public Timer(string name, int period, int due)
        {
            this.Name = name;
            this.period = period;
            this.dueTime = due;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            if (this.needScript && this.pc != null)
            {
                if (MapClient.FromActorPC(this.pc).scriptThread != null)
                    return;
                MapClient.FromActorPC(this.pc).scriptThread = new Thread(new ThreadStart(this.Run));
                MapClient.FromActorPC(this.pc).scriptThread.Start();
            }
            else if (this.OnTimerCall != null)
                this.OnTimerCall(this, this.pc);
        }

        /// <summary>
        /// The Run.
        /// </summary>
        private void Run()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (this.OnTimerCall != null)
                    this.OnTimerCall(this, this.pc);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
            MapClient.FromActorPC(this.pc).scriptThread = (Thread)null;
        }
    }
}
