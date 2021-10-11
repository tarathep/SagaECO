namespace SagaMap.Mob
{
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="AIThread" />.
    /// </summary>
    public class AIThread : Singleton<AIThread>
    {
        /// <summary>
        /// Defines the ais.
        /// </summary>
        private static List<MobAI> ais = new List<MobAI>();

        /// <summary>
        /// Defines the deleting.
        /// </summary>
        private static List<MobAI> deleting = new List<MobAI>();

        /// <summary>
        /// Defines the adding.
        /// </summary>
        private static List<MobAI> adding = new List<MobAI>();

        /// <summary>
        /// Defines the mainThread.
        /// </summary>
        private Thread mainThread;

        /// <summary>
        /// Initializes a new instance of the <see cref="AIThread"/> class.
        /// </summary>
        public AIThread()
        {
            this.mainThread = new Thread(new ThreadStart(AIThread.mainLoop));
            this.mainThread.Name = string.Format("MobAIThread({0})", (object)this.mainThread.ManagedThreadId);
            ClientManager.AddThread(this.mainThread);
            this.mainThread.Start();
        }

        /// <summary>
        /// The RegisterAI.
        /// </summary>
        /// <param name="ai">The ai<see cref="MobAI"/>.</param>
        public void RegisterAI(MobAI ai)
        {
            lock (AIThread.adding)
                AIThread.adding.Add(ai);
        }

        /// <summary>
        /// The RemoveAI.
        /// </summary>
        /// <param name="ai">The ai<see cref="MobAI"/>.</param>
        public void RemoveAI(MobAI ai)
        {
            lock (AIThread.deleting)
                AIThread.deleting.Add(ai);
        }

        /// <summary>
        /// Gets the ActiveAI.
        /// </summary>
        public int ActiveAI
        {
            get
            {
                return AIThread.ais.Count;
            }
        }

        /// <summary>
        /// The mainLoop.
        /// </summary>
        private static void mainLoop()
        {
            while (true)
            {
                lock (AIThread.deleting)
                {
                    foreach (MobAI mobAi in AIThread.deleting)
                    {
                        if (AIThread.ais.Contains(mobAi))
                            AIThread.ais.Remove(mobAi);
                    }
                    AIThread.deleting.Clear();
                }
                lock (AIThread.adding)
                {
                    foreach (MobAI mobAi in AIThread.adding)
                    {
                        if (!AIThread.ais.Contains(mobAi))
                            AIThread.ais.Add(mobAi);
                    }
                    AIThread.adding.Clear();
                }
                foreach (MobAI ai in AIThread.ais)
                {
                    if (ai.Activated)
                    {
                        if (DateTime.Now > ai.NextUpdateTime)
                        {
                            try
                            {
                                ai.CallBack((object)null);
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                            ai.NextUpdateTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, ai.period);
                        }
                    }
                }
                if (AIThread.ais.Count == 0)
                    Thread.Sleep(500);
                else
                    Thread.Sleep(10);
            }
        }
    }
}
