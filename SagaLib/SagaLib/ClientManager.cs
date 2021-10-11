namespace SagaLib
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="ClientManager" />.
    /// </summary>
    public class ClientManager
    {
        /// <summary>
        /// Defines the noCheckDeadLock.
        /// </summary>
        public static bool noCheckDeadLock = false;

        /// <summary>
        /// Defines the enteredcriarea.
        /// </summary>
        private static bool enteredcriarea = false;

        /// <summary>
        /// Defines the blockedThread.
        /// </summary>
        private static List<Thread> blockedThread = new List<Thread>();

        /// <summary>
        /// Defines the Threads.
        /// </summary>
        private static Dictionary<string, Thread> Threads = new Dictionary<string, Thread>();

        /// <summary>
        /// Defines the listener.
        /// </summary>
        public TcpListener listener;

        /// <summary>
        /// Defines the packetCoordinator.
        /// </summary>
        public Thread packetCoordinator;

        /// <summary>
        /// Defines the waitressQueue.
        /// </summary>
        public AutoResetEvent waitressQueue;

        /// <summary>
        /// Defines the currentBlocker.
        /// </summary>
        private static Thread currentBlocker;

        /// <summary>
        /// Defines the timestamp.
        /// </summary>
        private static DateTime timestamp;

        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type.
        /// </summary>
        public Dictionary<ushort, Packet> commandTable;

        /// <summary>
        /// Gets a value indicating whether Blocked.
        /// </summary>
        public static bool Blocked
        {
            get
            {
                return ClientManager.blockedThread.Contains(Thread.CurrentThread);
            }
        }

        /// <summary>
        /// The AddThread.
        /// </summary>
        /// <param name="thread">The thread<see cref="Thread"/>.</param>
        public static void AddThread(Thread thread)
        {
            ClientManager.AddThread(thread.Name, thread);
        }

        /// <summary>
        /// The AddThread.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="thread">The thread<see cref="Thread"/>.</param>
        public static void AddThread(string name, Thread thread)
        {
            if (ClientManager.Threads.ContainsKey(name))
                return;
            lock (ClientManager.Threads)
            {
                try
                {
                    ClientManager.Threads.Add(name, thread);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Logger.ShowDebug("Threads count:" + (object)ClientManager.Threads.Count, (Logger)null);
                }
            }
        }

        /// <summary>
        /// The RemoveThread.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public static void RemoveThread(string name)
        {
            if (!ClientManager.Threads.ContainsKey(name))
                return;
            lock (ClientManager.Threads)
                ClientManager.Threads.Remove(name);
        }

        /// <summary>
        /// The GetThread.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="Thread"/>.</returns>
        public static Thread GetThread(string name)
        {
            if (!ClientManager.Threads.ContainsKey(name))
                return (Thread)null;
            lock (ClientManager.Threads)
                return ClientManager.Threads[name];
        }

        /// <summary>
        /// The checkCriticalArea.
        /// </summary>

        [Obsolete]
        public void checkCriticalArea()
        {
            while (true)
            {
                if (ClientManager.enteredcriarea && ((DateTime.Now - ClientManager.timestamp).TotalSeconds > 10.0 && !ClientManager.noCheckDeadLock && !Debugger.IsAttached))
                {
                    Logger.ShowError("Deadlock detected");
                    Logger.ShowError("Automatically unlocking....");
                    try
                    {
                        if (ClientManager.currentBlocker != null)
                        {
                            Logger.ShowError("Call Stack of current blocking Thread:");
                            Logger.ShowError("Thread name:" + ClientManager.getThreadName(ClientManager.currentBlocker));
                            if (ClientManager.currentBlocker.ThreadState != System.Threading.ThreadState.Running)
                                Logger.ShowWarning("Unexpected thread state:" + ClientManager.currentBlocker.ThreadState.ToString());
                            ClientManager.currentBlocker.Suspend();
                            StackTrace stackTrace = new StackTrace(ClientManager.currentBlocker, true);
                            ClientManager.currentBlocker.Resume();
                            foreach (StackFrame frame in stackTrace.GetFrames())
                                Logger.ShowError("at " + frame.GetMethod().ReflectedType.FullName + "." + frame.GetMethod().Name + " " + frame.GetFileName() + ":" + (object)frame.GetFileLineNumber());
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    Console.WriteLine();
                    Logger.ShowError("Call Stack of all blocking Threads:");
                    foreach (Thread thread in ClientManager.blockedThread.ToArray())
                    {
                        try
                        {
                            Logger.ShowError("Thread name:" + ClientManager.getThreadName(thread));
                            if (thread.ThreadState != System.Threading.ThreadState.Running)
                                Logger.ShowWarning("Unexpected thread state:" + thread.ThreadState.ToString());
                            thread.Suspend();
                            StackTrace stackTrace = new StackTrace(thread, true);
                            thread.Resume();
                            foreach (StackFrame frame in stackTrace.GetFrames())
                                Logger.ShowError("at " + frame.GetMethod().ReflectedType.FullName + "." + frame.GetMethod().Name + " " + frame.GetFileName() + ":" + (object)frame.GetFileLineNumber());
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Logger.ShowError("Call Stack of all Threads:");
                    string[] array = new string[ClientManager.Threads.Keys.Count];
                    ClientManager.Threads.Keys.CopyTo(array, 0);
                    foreach (string name in array)
                    {
                        try
                        {
                            Thread thread = ClientManager.GetThread(name);
                            Logger.ShowError("Thread name:" + name);
                            if (thread.ThreadState != System.Threading.ThreadState.Running)
                                Logger.ShowWarning("Unexpected thread state:" + thread.ThreadState.ToString());
                            thread.Suspend();
                            StackTrace stackTrace = new StackTrace(thread, true);
                            thread.Resume();
                            foreach (StackFrame frame in stackTrace.GetFrames())
                                Logger.ShowError("at " + frame.GetMethod().ReflectedType.FullName + "." + frame.GetMethod().Name + " " + frame.GetFileName() + ":" + (object)frame.GetFileLineNumber());
                        }
                        catch
                        {
                        }
                        Console.WriteLine();
                    }
                    ClientManager.LeaveCriticalArea(ClientManager.currentBlocker);
                }
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// The getThreadName.
        /// </summary>
        /// <param name="thread">The thread<see cref="Thread"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string getThreadName(Thread thread)
        {
            foreach (string key in ClientManager.Threads.Keys)
            {
                if (thread == ClientManager.Threads[key])
                    return key;
            }
            return "";
        }

        /// <summary>
        /// The PrintAllThreads.
        /// </summary>
        [Obsolete]
        public static void PrintAllThreads()
        {
            Logger.ShowWarning("Call Stack of all blocking Threads:");
            foreach (Thread thread in ClientManager.blockedThread.ToArray())
            {
                try
                {
                    Logger.ShowWarning("Thread name:" + ClientManager.getThreadName(thread));
                    thread.Suspend();
                    StackTrace stackTrace = new StackTrace(thread, true);
                    thread.Resume();
                    foreach (StackFrame frame in stackTrace.GetFrames())
                        Logger.ShowWarning("at " + frame.GetMethod().ReflectedType.FullName + "." + frame.GetMethod().Name + " " + frame.GetFileName() + ":" + (object)frame.GetFileLineNumber());
                }
                catch
                {
                }
                Console.WriteLine();
            }
            Logger.ShowWarning("Call Stack of all Threads:");
            string[] array = new string[ClientManager.Threads.Keys.Count];
            ClientManager.Threads.Keys.CopyTo(array, 0);
            foreach (string name in array)
            {
                try
                {
                    Thread thread = ClientManager.GetThread(name);
                    thread.Suspend();
                    StackTrace stackTrace = new StackTrace(thread, true);
                    thread.Resume();
                    Logger.ShowWarning("Thread name:" + name);
                    foreach (StackFrame frame in stackTrace.GetFrames())
                        Logger.ShowWarning("at " + frame.GetMethod().ReflectedType.FullName + "." + frame.GetMethod().Name + " " + frame.GetFileName() + ":" + (object)frame.GetFileLineNumber());
                }
                catch
                {
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// The EnterCriticalArea.
        /// </summary>
        public static void EnterCriticalArea()
        {
            if (ClientManager.blockedThread.Contains(Thread.CurrentThread))
            {
                Logger.ShowDebug("Current thread is already blocked, skip blocking to avoid deadlock!", Logger.defaultlogger);
            }
            else
            {
                Global.clientMananger.waitressQueue.WaitOne();
                ClientManager.timestamp = DateTime.Now;
                ClientManager.enteredcriarea = true;
                ClientManager.blockedThread.Add(Thread.CurrentThread);
                ClientManager.currentBlocker = Thread.CurrentThread;
            }
        }

        /// <summary>
        /// The LeaveCriticalArea.
        /// </summary>
        public static void LeaveCriticalArea()
        {
            ClientManager.LeaveCriticalArea(Thread.CurrentThread);
        }

        /// <summary>
        /// The LeaveCriticalArea.
        /// </summary>
        /// <param name="blocker">The blocker<see cref="Thread"/>.</param>
        public static void LeaveCriticalArea(Thread blocker)
        {
            if (ClientManager.blockedThread.Contains(blocker) || ClientManager.blockedThread.Count != 0)
            {
                int seconds = (DateTime.Now - ClientManager.timestamp).Seconds;
                if (seconds > 5)
                    Logger.ShowDebug(string.Format("Thread({0}) used unnormal time till unlock({1} sec)", (object)blocker.Name, (object)seconds), Logger.defaultlogger);
                ClientManager.enteredcriarea = false;
                if (ClientManager.blockedThread.Contains(blocker))
                    ClientManager.blockedThread.Remove(blocker);
                else if (ClientManager.blockedThread.Count > 0)
                    ClientManager.blockedThread.RemoveAt(0);
                ClientManager.currentBlocker = (Thread)null;
                ClientManager.timestamp = DateTime.Now;
                Global.clientMananger.waitressQueue.Set();
            }
            else
                Logger.ShowDebug("Current thread isn't blocked while trying unblock, skiping", Logger.defaultlogger);
        }

        /// <summary>
        /// The Start.
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// The Stop.
        /// </summary>
        public void Stop()
        {
            this.listener.Stop();
        }

        /// <summary>
        /// The StartNetwork.
        /// </summary>
        /// <param name="port">The port<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [Obsolete]
        public bool StartNetwork(int port)
        {
            this.listener = new TcpListener(port);
            try
            {
                this.listener.Start();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// The GetClient.
        /// </summary>
        /// <param name="SessionID">The SessionID<see cref="uint"/>.</param>
        /// <returns>The <see cref="Client"/>.</returns>
        public virtual Client GetClient(uint SessionID)
        {
            return (Client)null;
        }

        /// <summary>
        /// The NetworkLoop.
        /// </summary>
        /// <param name="maxNewConnections">The maxNewConnections<see cref="int"/>.</param>
        public virtual void NetworkLoop(int maxNewConnections)
        {
        }

        /// <summary>
        /// The OnClientDisconnect.
        /// </summary>
        /// <param name="client">The client<see cref="Client"/>.</param>
        public virtual void OnClientDisconnect(Client client)
        {
        }
    }
}
