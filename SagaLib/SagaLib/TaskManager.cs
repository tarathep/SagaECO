namespace SagaLib
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="TaskManager" />.
    /// </summary>
    public class TaskManager : Singleton<TaskManager>
    {
        /// <summary>
        /// Defines the threadpool.
        /// </summary>
        private List<Thread> threadpool = new List<Thread>();

        /// <summary>
        /// Defines the fifo.
        /// </summary>
        private Queue<MultiRunTask> fifo = new Queue<MultiRunTask>();

        /// <summary>
        /// Defines the registered.
        /// </summary>
        private List<MultiRunTask> registered = new List<MultiRunTask>();

        /// <summary>
        /// Defines the main.
        /// </summary>
        private Thread main;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskManager"/> class.
        /// </summary>
        public TaskManager()
        {
            this.SetWorkerCount(2);
            this.Start();
        }

        /// <summary>
        /// The SetWorkerCount.
        /// </summary>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetWorkerCount(int count)
        {
            foreach (Thread thread in this.threadpool)
            {
                ClientManager.RemoveThread(thread.Name);
                thread.Abort();
            }
            this.threadpool.Clear();
            for (int index = 0; index < count; ++index)
            {
                Thread thread = new Thread(new ThreadStart(this.Worker));
                thread.Name = string.Format("Worker({0})", (object)thread.ManagedThreadId);
                ClientManager.AddThread(thread);
                thread.Start();
                this.threadpool.Add(thread);
            }
        }

        /// <summary>
        /// The Start.
        /// </summary>
        public void Start()
        {
            if (this.main != null)
            {
                ClientManager.RemoveThread(this.main.Name);
                this.main.Abort();
            }
            this.main = new Thread(new ThreadStart(this.MainLoop));
            this.main.Name = string.Format("ThreadPoolMainLoop({0})", (object)this.main.ManagedThreadId);
            ClientManager.AddThread(this.main);
            this.main.Start();
        }

        /// <summary>
        /// The RegisterTask.
        /// </summary>
        /// <param name="task">The task<see cref="MultiRunTask"/>.</param>
        public void RegisterTask(MultiRunTask task)
        {
            lock (this.registered)
            {
                if (this.registered.Contains(task))
                    return;
                this.registered.Add(task);
            }
        }

        /// <summary>
        /// The RemoveTask.
        /// </summary>
        /// <param name="task">The task<see cref="MultiRunTask"/>.</param>
        public void RemoveTask(MultiRunTask task)
        {
            lock (this.registered)
            {
                if (!this.registered.Contains(task))
                    return;
                this.registered.Remove(task);
            }
        }

        /// <summary>
        /// Gets the RegisteredTasks.
        /// </summary>
        public List<string> RegisteredTasks
        {
            get
            {
                List<string> stringList = new List<string>();
                lock (this.registered)
                {
                    foreach (MultiRunTask multiRunTask in this.registered)
                        stringList.Add(multiRunTask.ToString());
                }
                return stringList;
            }
        }

        /// <summary>
        /// The MainLoop.
        /// </summary>
        private void MainLoop()
        {
            try
            {
                while (true)
                {
                    DateTime now = DateTime.Now;
                    lock (this.registered)
                    {
                        foreach (MultiRunTask multiRunTask in this.registered)
                        {
                            try
                            {
                                if (now > multiRunTask.NextUpdateTime)
                                {
                                    lock (this.fifo)
                                        this.fifo.Enqueue(multiRunTask);
                                    multiRunTask.NextUpdateTime = now + new TimeSpan(0, 0, 0, 0, multiRunTask.period);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }
                    }
                    Thread.Sleep(10);
                }
            }
            catch (ThreadAbortException ex)
            {
                ClientManager.RemoveThread(Thread.CurrentThread.Name);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.RemoveThread(Thread.CurrentThread.Name);
        }

        /// <summary>
        /// The Worker.
        /// </summary>
        private void Worker()
        {
            try
            {
                while (true)
                {
                    do
                    {
                        MultiRunTask multiRunTask = (MultiRunTask)null;
                        lock (this.fifo)
                        {
                            if (this.fifo.Count > 0)
                            {
                                try
                                {
                                    multiRunTask = this.fifo.Dequeue();
                                }
                                catch
                                {
                                }
                            }
                        }
                        if (multiRunTask != null)
                        {
                            try
                            {
                                multiRunTask.CallBack((object)null);
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }
                    }
                    while (this.fifo.Count != 0);
                    Thread.Sleep(10);
                }
            }
            catch (ThreadAbortException ex)
            {
                ClientManager.RemoveThread(Thread.CurrentThread.Name);
                Logger.ShowInfo("TaskManager: Terminating Worker....");
            }
            catch (Exception ex)
            {
                Logger.ShowError("Critical ERROR! Worker terminated unexpected!");
                Logger.ShowError(ex);
            }
            ClientManager.RemoveThread(Thread.CurrentThread.Name);
        }
    }
}
