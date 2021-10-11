namespace SagaLib
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="DatabaseWaitress" />.
    /// </summary>
    public class DatabaseWaitress
    {
        /// <summary>
        /// Defines the waitressQueue.
        /// </summary>
        public static AutoResetEvent waitressQueue = new AutoResetEvent(true);

        /// <summary>
        /// Defines the blockedThread.
        /// </summary>
        private static List<Thread> blockedThread = new List<Thread>();

        /// <summary>
        /// Defines the Coordinator.
        /// </summary>
        public static Thread Coordinator;

        /// <summary>
        /// Defines the currentBlocker.
        /// </summary>
        private static Thread currentBlocker;

        /// <summary>
        /// Gets a value indicating whether Blocked.
        /// </summary>
        public static bool Blocked
        {
            get
            {
                return DatabaseWaitress.blockedThread.Contains(Thread.CurrentThread);
            }
        }

        /// <summary>
        /// The EnterCriticalArea.
        /// </summary>
        public static void EnterCriticalArea()
        {
            if (DatabaseWaitress.blockedThread.Contains(Thread.CurrentThread))
            {
                Logger.ShowDebug("Current thread is already blocked, skip blocking to avoid deadlock!", Logger.defaultlogger);
            }
            else
            {
                DatabaseWaitress.waitressQueue.WaitOne();
                DatabaseWaitress.blockedThread.Add(Thread.CurrentThread);
                DatabaseWaitress.currentBlocker = Thread.CurrentThread;
            }
        }

        /// <summary>
        /// The LeaveCriticalArea.
        /// </summary>
        public static void LeaveCriticalArea()
        {
            DatabaseWaitress.LeaveCriticalArea(Thread.CurrentThread);
        }

        /// <summary>
        /// The LeaveCriticalArea.
        /// </summary>
        /// <param name="blocker">The blocker<see cref="Thread"/>.</param>
        public static void LeaveCriticalArea(Thread blocker)
        {
            if (DatabaseWaitress.blockedThread.Contains(blocker) || DatabaseWaitress.blockedThread.Count != 0)
            {
                if (DatabaseWaitress.blockedThread.Contains(blocker))
                    DatabaseWaitress.blockedThread.Remove(blocker);
                else if (DatabaseWaitress.blockedThread.Count > 0)
                    DatabaseWaitress.blockedThread.RemoveAt(0);
                DatabaseWaitress.currentBlocker = (Thread)null;
                DatabaseWaitress.waitressQueue.Set();
            }
            else
                Logger.ShowDebug("Current thread isn't blocked while trying unblock, skiping", Logger.defaultlogger);
        }
    }
}
