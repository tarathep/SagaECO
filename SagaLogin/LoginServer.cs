namespace SagaLogin
{
    using SagaDB;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using SagaLogin.Manager;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="LoginServer" />.
    /// </summary>
    public class LoginServer
    {
        /// <summary>
        /// Defines the charDB.
        /// </summary>
        public static ActorDB charDB;

        /// <summary>
        /// Defines the accountDB.
        /// </summary>
        public static AccountDB accountDB;

        /// <summary>
        /// The StartDatabase.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool StartDatabase()
        {
            try
            {
                switch (Singleton<Configuration>.Instance.DBType)
                {
                    case 0:
                        LoginServer.charDB = (ActorDB)new MySQLActorDB(Singleton<Configuration>.Instance.DBHost, Singleton<Configuration>.Instance.DBPort, Singleton<Configuration>.Instance.DBName, Singleton<Configuration>.Instance.DBUser, Singleton<Configuration>.Instance.DBPass);
                        LoginServer.accountDB = (AccountDB)new MySQLAccountDB(Singleton<Configuration>.Instance.DBHost, Singleton<Configuration>.Instance.DBPort, Singleton<Configuration>.Instance.DBName, Singleton<Configuration>.Instance.DBUser, Singleton<Configuration>.Instance.DBPass);
                        LoginServer.charDB.Connect();
                        LoginServer.accountDB.Connect();
                        return true;
                    case 1:
                        LoginServer.accountDB = (AccountDB)new AccessAccountDB(Singleton<Configuration>.Instance.DBHost);
                        LoginServer.charDB = (ActorDB)new AccessActorDb(Singleton<Configuration>.Instance.DBHost);
                        LoginServer.charDB.Connect();
                        LoginServer.accountDB.Connect();
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// The EnsureCharDB.
        /// </summary>
        public static void EnsureCharDB()
        {
            bool flag = false;
            if (!LoginServer.charDB.isConnected())
            {
                Logger.ShowWarning("LOST CONNECTION TO CHAR DB SERVER!", (Logger)null);
                flag = true;
            }
            while (flag)
            {
                Logger.ShowInfo("Trying to reconnect to char db server ..", (Logger)null);
                LoginServer.charDB.Connect();
                if (!LoginServer.charDB.isConnected())
                {
                    Logger.ShowError("Failed.. Trying again in 10sec", (Logger)null);
                    Thread.Sleep(10000);
                    flag = true;
                }
                else
                {
                    Logger.ShowInfo("SUCCESSFULLY RE-CONNECTED to char db server...", (Logger)null);
                    Logger.ShowInfo("Clients can now connect again", (Logger)null);
                    flag = false;
                }
            }
        }

        /// <summary>
        /// The EnsureAccountDB.
        /// </summary>
        public static void EnsureAccountDB()
        {
            bool flag = false;
            if (!LoginServer.accountDB.isConnected())
            {
                Logger.ShowWarning("LOST CONNECTION TO CHAR DB SERVER!", (Logger)null);
                flag = true;
            }
            while (flag)
            {
                Logger.ShowInfo("Trying to reconnect to char db server ..", (Logger)null);
                LoginServer.accountDB.Connect();
                if (!LoginServer.accountDB.isConnected())
                {
                    Logger.ShowError("Failed.. Trying again in 10sec", (Logger)null);
                    Thread.Sleep(10000);
                    flag = true;
                }
                else
                {
                    Logger.ShowInfo("SUCCESSFULLY RE-CONNECTED to char db server...", (Logger)null);
                    Logger.ShowInfo("Clients can now connect again", (Logger)null);
                    flag = false;
                }
            }
        }

        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        private static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(LoginServer.ShutingDown);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(LoginServer.CurrentDomain_UnhandledException);
            Logger logger = new Logger("SagaLogin.log");
            Logger.defaultlogger = logger;
            Logger.CurrentLogger = logger;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("     SagaECO Login Server (C)2020 BokieTarathep\n     github.com/tarathep/SagaECO               ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Logger.ShowInfo("Version Informations:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaLogin");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":SVN Rev." + GlobalInfo.Version + "(" + GlobalInfo.ModifyDate + ")");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaLib");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":SVN Rev." + SagaLib.GlobalInfo.Version + "(" + SagaLib.GlobalInfo.ModifyDate + ")");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaDB");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":SVN Rev." + SagaDB.GlobalInfo.Version + "(" + SagaDB.GlobalInfo.ModifyDate + ")");
            Logger.ShowInfo("Starting Initialization...", (Logger)null);
            Singleton<Configuration>.Instance.Initialization("./Config/SagaLogin.xml");
            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Singleton<Configuration>.Instance.LogLevel;
            Logger.ShowInfo("Initializing VirtualFileSystem...");
            Singleton<VirtualFileSystemManager>.Instance.Init(FileSystems.Real, ".");
            Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.Init(Singleton<VirtualFileSystemManager>.Instance.FileSystem.SearchFile("DB/", "item*.csv", SearchOption.TopDirectoryOnly), Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<MapInfoFactory>.Instance.Init("DB/MapInfo.zip", false);
            if (!LoginServer.StartDatabase())
            {
                Logger.ShowError("cannot connect to dbserver", (Logger)null);
                Logger.ShowError("Shutting down in 20sec.", (Logger)null);
                Thread.Sleep(20000);
            }
            else
            {
                LoginClientManager.Instance.Start();
                if (!LoginClientManager.Instance.StartNetwork(Singleton<Configuration>.Instance.Port))
                {
                    Logger.ShowError("cannot listen on port: " + (object)Singleton<Configuration>.Instance.Port);
                    Logger.ShowInfo("Shutting down in 20sec.");
                    Thread.Sleep(20000);
                }
                else
                {
                    Global.clientMananger = (ClientManager)LoginClientManager.Instance;
                    Console.WriteLine("Accepting clients.");
                    while (true)
                    {
                        LoginServer.EnsureCharDB();
                        LoginServer.EnsureAccountDB();
                        LoginClientManager.Instance.NetworkLoop(10);
                        Thread.Sleep(1);
                    }
                }
            }
        }

        /// <summary>
        /// The ShutingDown.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="args">The args<see cref="ConsoleCancelEventArgs"/>.</param>
        private static void ShutingDown(object sender, ConsoleCancelEventArgs args)
        {
            Logger.ShowInfo("Closing.....", (Logger)null);
        }

        /// <summary>
        /// The CurrentDomain_UnhandledException.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="UnhandledExceptionEventArgs"/>.</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exceptionObject = e.ExceptionObject as Exception;
            Logger.ShowError("Fatal: An unhandled exception is thrown, terminating...");
            Logger.ShowError("Error Message:" + exceptionObject.Message);
            Logger.ShowError("Call Stack:" + exceptionObject.StackTrace);
        }
    }
}
