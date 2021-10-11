namespace SagaMap
{
    using SagaDB;
    using SagaDB.Actor;
    using SagaDB.DEMIC;
    using SagaDB.ECOShop;
    using SagaDB.Iris;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Marionette;
    using SagaDB.Mob;
    using SagaDB.Npc;
    using SagaDB.ODWar;
    using SagaDB.Quests;
    using SagaDB.Ring;
    using SagaDB.Skill;
    using SagaDB.Synthese;
    using SagaDB.Theater;
    using SagaDB.Treasure;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using SagaMap.Dungeon;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Network.Client;
    using SagaMap.Network.LoginServer;
    using SagaMap.Skill;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="MapServer" />.
    /// </summary>
    public class MapServer
    {
        /// <summary>
        /// Defines the shutingdown.
        /// </summary>
        public static bool shutingdown = false;

        /// <summary>
        /// Defines the shouldRefreshStatistic.
        /// </summary>
        public static bool shouldRefreshStatistic = true;

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
                        MapServer.charDB = (ActorDB)new MySQLActorDB(Singleton<Configuration>.Instance.DBHost, Singleton<Configuration>.Instance.DBPort, Singleton<Configuration>.Instance.DBName, Singleton<Configuration>.Instance.DBUser, Singleton<Configuration>.Instance.DBPass);
                        MapServer.accountDB = (AccountDB)new MySQLAccountDB(Singleton<Configuration>.Instance.DBHost, Singleton<Configuration>.Instance.DBPort, Singleton<Configuration>.Instance.DBName, Singleton<Configuration>.Instance.DBUser, Singleton<Configuration>.Instance.DBPass);
                        MapServer.charDB.Connect();
                        MapServer.accountDB.Connect();
                        return true;
                    case 1:
                        MapServer.accountDB = (AccountDB)new AccessAccountDB(Singleton<Configuration>.Instance.DBHost);
                        MapServer.charDB = (ActorDB)new AccessActorDb(Singleton<Configuration>.Instance.DBHost);
                        MapServer.charDB.Connect();
                        MapServer.accountDB.Connect();
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
            if (!MapServer.charDB.isConnected())
            {
                Logger.ShowWarning("LOST CONNECTION TO CHAR DB SERVER!", (Logger)null);
                flag = true;
            }
            while (flag)
            {
                Logger.ShowInfo("Trying to reconnect to char db server ..", (Logger)null);
                MapServer.charDB.Connect();
                if (!MapServer.charDB.isConnected())
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
            if (!MapServer.accountDB.isConnected())
            {
                Logger.ShowWarning("LOST CONNECTION TO CHAR DB SERVER!", (Logger)null);
                flag = true;
            }
            while (flag)
            {
                Logger.ShowInfo("Trying to reconnect to char db server ..", (Logger)null);
                MapServer.accountDB.Connect();
                if (!MapServer.accountDB.isConnected())
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
            Console.CancelKeyPress += new ConsoleCancelEventHandler(MapServer.ShutingDown);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(MapServer.CurrentDomain_ProcessExit);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MapServer.CurrentDomain_UnhandledException);
            Logger logger = new Logger("SagaMap.log");
            Logger.defaultlogger = logger;
            Logger.CurrentLogger = logger;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("     SagaECO Map Server (C)2020 BokieTarathep\n     github.com/tarathep/SagaECO               ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Logger.ShowInfo("Version Informations:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaMap");
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
            Logger.ShowInfo(Singleton<LocalManager>.Instance.Strings.INITIALIZATION, (Logger)null);
            Singleton<Configuration>.Instance.Initialization("./Config/SagaMap.xml");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Current Packet Version:[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write((object)Singleton<Configuration>.Instance.Version);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("]");
            Singleton<LocalManager>.Instance.CurrentLanguage = (LocalManager.Languages)Enum.Parse(typeof(LocalManager.Languages), Singleton<Configuration>.Instance.Language);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Current Language:[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write((object)Singleton<LocalManager>.Instance);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("]");
            Console.ResetColor();
            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Singleton<Configuration>.Instance.LogLevel;
            Logger.ShowInfo("Initializing VirtualFileSystem...");
            Singleton<VirtualFileSystemManager>.Instance.Init(FileSystems.Real, ".");
            Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.Init(Singleton<VirtualFileSystemManager>.Instance.FileSystem.SearchFile("DB/", "item*.csv", SearchOption.TopDirectoryOnly), Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<IrisAbilityFactory, AbilityVector>.Instance.Init("DB/iris_ability_vector_info.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<IrisCardFactory, IrisCard>.Instance.Init("DB/iris_card.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<ModelFactory>.Instance.Init("DB/demic_chip_model.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<ChipFactory, Chip.BaseData>.Instance.Init("DB/demic_chip.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<SyntheseFactory, SyntheseInfo>.Instance.Init("DB/SyntheseDB.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            FactoryString<TreasureFactory, TreasureList>.Instance.Init(Singleton<VirtualFileSystemManager>.Instance.FileSystem.SearchFile("DB/Treasure", "*.xml", SearchOption.AllDirectories), (Encoding)null);
            Singleton<MobFactory>.Instance.Init("DB/monster.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<MobFactory>.Instance.InitPet("DB/pet.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<MobFactory>.Instance.InitPetLimit("DB/pet_limit.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<MarionetteFactory>.Instance.Init("DB/marionette.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<SkillFactory>.Instance.Init("DB/SkillDB.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<SkillFactory>.Instance.LoadSkillList("DB/SkillList.xml");
            Singleton<ExperienceManager>.Instance.LoadTable("DB/exp.xml");
            Factory<RingFameTable, RingFame>.Instance.Init("DB/RingFame.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<QuestFactory, QuestInfo>.Instance.Init("DB/Quests", (Encoding)null, true);
            Factory<NPCFactory, NPC>.Instance.Init("DB/npc.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<ShopFactory, Shop>.Instance.Init("DB/ShopDB.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<ECOShopFactory, ShopCategory>.Instance.Init("DB/ECOShop.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<ChipShopFactory, ChipShopCategory>.Instance.Init("DB/ChipShop.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<MapInfoFactory>.Instance.Init("DB/MapInfo.zip");
            Singleton<MapInfoFactory>.Instance.LoadFlags("DB/MapFlags.xml");
            Singleton<MapInfoFactory>.Instance.LoadGatherInterval("DB/pick_interval.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<MapInfoFactory>.Instance.LoadMapObjects("DB/MapObjects.dat");
            Singleton<MapInfoFactory>.Instance.ApplyMapObject();
            Singleton<MapInfoFactory>.Instance.MapObjects.Clear();
            Singleton<MapManager>.Instance.MapInfos = Singleton<MapInfoFactory>.Instance.MapInfo;
            Singleton<MapManager>.Instance.LoadMaps();
            Factory<DungeonMapsFactory, DungeonMap>.Instance.Init("DB/Dungeon/DungeonMaps.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.Init("DB/Dungeon/Dungeons.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<ScriptManager>.Instance.LoadScript("./Scripts");
            Factory<MobAIFactory, AIMode>.Instance.Init("DB/MobAI.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Singleton<MobSpawnManager>.Instance.LoadSpawn("DB/Spawns");
            FactoryList<TheaterFactory, Movie>.Instance.Init("DB/TheaterSchedule.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Init("DB/ODWar.xml", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
            SagaMap.Tasks.System.Theater.Instance.Activate();
            Singleton<SkillHandler>.Instance.Init();
            SagaLib.Global.clientMananger = (ClientManager)MapClientManager.Instance;
            Singleton<AtCommand>.Instance.LoadCommandLevelSetting("./Config/GMCommand.csv");
            LoginSession loginSession = new LoginSession();
            while (loginSession.state != LoginSession.SESSION_STATE.IDENTIFIED && loginSession.state != LoginSession.SESSION_STATE.REJECTED)
                Thread.Sleep(1000);
            if (loginSession.state == LoginSession.SESSION_STATE.REJECTED)
            {
                Logger.ShowError("Shutting down in 20sec.", (Logger)null);
                MapClientManager.Instance.Abort();
                Thread.Sleep(20000);
            }
            else if (!MapServer.StartDatabase())
            {
                Logger.ShowError("cannot connect to dbserver", (Logger)null);
                Logger.ShowError("Shutting down in 20sec.", (Logger)null);
                MapClientManager.Instance.Abort();
                Thread.Sleep(20000);
            }
            else
            {
                if (Singleton<Configuration>.Instance.SQLLog)
                    Logger.defaultSql = (MySQLConnectivity)(MapServer.charDB as MySQLActorDB);
                Singleton<ScriptManager>.Instance.VariableHolder = MapServer.charDB.LoadServerVar();
                MapClientManager.Instance.Start();
                if (!MapClientManager.Instance.StartNetwork(Singleton<Configuration>.Instance.Port))
                {
                    Logger.ShowError("cannot listen on port: " + (object)Singleton<Configuration>.Instance.Port);
                    Logger.ShowInfo("Shutting down in 20sec.");
                    MapClientManager.Instance.Abort();
                    Thread.Sleep(20000);
                }
                else
                {
                    if (Logger.defaultSql != null)
                    {
                        Logger.ShowInfo("Clearing SQL Logs");
                        Logger.defaultSql.SQLExecuteNonQuery(string.Format("DELETE FROM `log` WHERE `eventTime` < '{0}';", (object)Logger.defaultSql.ToSQLDateTime(DateTime.Now - new TimeSpan(3, 0, 0, 0))));
                    }
                    Logger.ShowInfo(Singleton<LocalManager>.Instance.Strings.ACCEPTING_CLIENT);
                    SagaMap.Tasks.System.ODWar.Instance.Activate();
                    DateTime now = DateTime.Now;
                    foreach (SagaDB.ODWar.ODWar odWar in Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items.Values)
                        Singleton<ODWarManager>.Instance.StartODWar(odWar.MapID);
                    new Thread(new ThreadStart(MapServer.ConsoleThread)).Start();
                    while (true)
                    {
                        try
                        {
                            if (MapServer.shouldRefreshStatistic && Singleton<Configuration>.Instance.OnlineStatistics)
                            {
                                try
                                {
                                    StreamReader streamReader = new StreamReader("Config/OnlineStatisticTemplate.htm", true);
                                    string end = streamReader.ReadToEnd();
                                    streamReader.Close();
                                    string str1 = end.Substring(0, end.IndexOf("<template for one row>"));
                                    string str2 = end.Substring(end.IndexOf("<template for one row>") + "<template for one row>".Length);
                                    string str3 = str2.Substring(str2.IndexOf("</template for one row>") + "</template for one row>".Length);
                                    string str4 = str2.Substring(0, str2.IndexOf("</template for one row>"));
                                    string str5 = "";
                                    foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                                    {
                                        try
                                        {
                                            string str6 = str4.Replace("{CharName}", mapClient.Character.Name).Replace("{Job}", mapClient.Character.Job.ToString()).Replace("{BaseLv}", mapClient.Character.Level.ToString());
                                            string str7 = (mapClient.Character.Job != mapClient.Character.JobBasic ? (mapClient.Character.Job != mapClient.Character.Job2X ? str6.Replace("{JobLv}", mapClient.Character.JobLevel2T.ToString()) : str6.Replace("{JobLv}", mapClient.Character.JobLevel2X.ToString())) : str6.Replace("{JobLv}", mapClient.Character.JobLevel1.ToString())).Replace("{Map}", mapClient.map.Info.name);
                                            str5 += str7;
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    StreamWriter streamWriter = new StreamWriter(Singleton<Configuration>.Instance.StatisticsPagePath, false, SagaLib.Global.Unicode);
                                    streamWriter.Write(str1);
                                    streamWriter.Write(str5);
                                    streamWriter.Write(str3);
                                    streamWriter.Flush();
                                    streamWriter.Close();
                                }
                                catch (Exception ex)
                                {
                                    Logger.ShowError(ex);
                                }
                                MapServer.shouldRefreshStatistic = false;
                            }
                            MapServer.EnsureCharDB();
                            MapServer.EnsureAccountDB();
                            if (!MapServer.shutingdown)
                                MapClientManager.Instance.NetworkLoop(10);
                            Thread.Sleep(1);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The CurrentDomain_ProcessExit.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The CurrentDomain_DomainUnload.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ConsoleThread.
        /// </summary>
        private static void ConsoleThread()
        {
            while (true)
            {
                try
                {
                    string[] args = Console.ReadLine().Split(' ');
                    int num1;
                    switch (args[0].ToLower())
                    {
                        case "printthreads":
                            ClientManager.PrintAllThreads();
                            break;
                        case "printtaskinfo":
                            string str1 = "Active AI count:";
                            num1 = Singleton<AIThread>.Instance.ActiveAI;
                            string str2 = num1.ToString();
                            Logger.ShowWarning(str1 + str2);
                            List<string> registeredTasks = Singleton<TaskManager>.Instance.RegisteredTasks;
                            string str3 = "Active Tasks:";
                            num1 = registeredTasks.Count;
                            string str4 = num1.ToString();
                            Logger.ShowWarning(str3 + str4);
                            using (List<string>.Enumerator enumerator = registeredTasks.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                    Logger.ShowWarning(enumerator.Current);
                                break;
                            }
                        case "printband":
                            int num2 = 0;
                            int num3 = 0;
                            Logger.ShowWarning("Bandwidth usage information:");
                            try
                            {
                                foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                                {
                                    num2 += mapClient.netIO.UpStreamBand;
                                    num3 += mapClient.netIO.DownStreamBand;
                                    Logger.ShowWarning(string.Format("Client:{0} Receive:{1:0.##}KB/s Send:{2:0.##}KB/s", (object)mapClient.ToString(), (object)(float)((double)mapClient.netIO.DownStreamBand / 1024.0), (object)(float)((double)mapClient.netIO.UpStreamBand / 1024.0)));
                                }
                            }
                            catch
                            {
                            }
                            Logger.ShowWarning(string.Format("Total: Receive:{0:0.##}KB/s Send:{1:0.##}KB/s", (object)(float)((double)num3 / 1024.0), (object)(float)((double)num2 / 1024.0)));
                            break;
                        case "announce":
                            if (args.Length > 1)
                            {
                                StringBuilder stringBuilder = new StringBuilder(args[1]);
                                for (int index = 2; index < args.Length; ++index)
                                    stringBuilder.Append(" " + args[index]);
                                string text = stringBuilder.ToString();
                                try
                                {
                                    foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                                        mapClient.SendAnnounce(text);
                                }
                                catch (Exception ex)
                                {
                                }
                                break;
                            }
                            break;
                        case "kick":
                            if (args.Length > 1)
                            {
                                try
                                {
                                    MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => c.Character.Name == args[1])).First<MapClient>().netIO.Disconnect();
                                }
                                catch (Exception ex)
                                {
                                }
                                break;
                            }
                            break;
                        case "quit":
                            Logger.ShowInfo("Closing.....", (Logger)null);
                            MapServer.shutingdown = true;
                            MapServer.charDB.SaveServerVar(Singleton<ScriptManager>.Instance.VariableHolder);
                            MapClient[] array = new MapClient[MapClientManager.Instance.Clients.Count];
                            MapClientManager.Instance.Clients.CopyTo(array);
                            Logger.ShowInfo("Saving player's data.....", (Logger)null);
                            foreach (MapClient mapClient in array)
                            {
                                try
                                {
                                    if (mapClient.Character != null)
                                        mapClient.netIO.Disconnect();
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            Logger.ShowInfo("Saving golem's data.....", (Logger)null);
                            foreach (SagaMap.Map map in Singleton<MapManager>.Instance.Maps.Values)
                            {
                                foreach (SagaDB.Actor.Actor actor in map.Actors.Values)
                                {
                                    if (actor.type == ActorType.GOLEM)
                                    {
                                        try
                                        {
                                            ActorGolem actorGolem = (ActorGolem)actor;
                                            MapServer.charDB.SaveChar(actorGolem.Owner, false);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                if (map.IsMapInstance)
                                    map.OnDestrory();
                            }
                            Environment.Exit(Environment.ExitCode);
                            break;
                        case "who":
                            foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                            {
                                byte num4 = SagaLib.Global.PosX16to8(mapClient.Character.X, mapClient.map.Width);
                                byte num5 = SagaLib.Global.PosY16to8(mapClient.Character.Y, mapClient.map.Height);
                                Logger.ShowInfo(mapClient.Character.Name + "(CharID:" + (object)mapClient.Character.CharID + ")[" + mapClient.Map.Name + " " + num4.ToString() + "," + num5.ToString() + "]");
                            }
                            string onlinePlayerInfo = Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO;
                            num1 = MapClientManager.Instance.OnlinePlayer.Count;
                            string str5 = num1.ToString();
                            Logger.ShowInfo(onlinePlayerInfo + str5);
                            break;
                        case "kick2":
                            if (args.Length > 1)
                            {
                                try
                                {
                                    IEnumerable<MapClient> source = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => (int)c.Character.CharID == (int)uint.Parse(args[1])));
                                    if (source.Count<MapClient>() > 0)
                                        source.First<MapClient>().netIO.Disconnect();
                                }
                                catch (Exception ex)
                                {
                                }
                                break;
                            }
                            break;
                    }
                }
                catch
                {
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
            MapServer.shutingdown = true;
            MapServer.charDB.SaveServerVar(Singleton<ScriptManager>.Instance.VariableHolder);
            MapClient[] array = new MapClient[MapClientManager.Instance.Clients.Count];
            MapClientManager.Instance.Clients.CopyTo(array);
            Logger.ShowInfo("Saving player's data.....", (Logger)null);
            foreach (MapClient mapClient in array)
            {
                try
                {
                    if (mapClient.Character != null)
                        mapClient.netIO.Disconnect();
                }
                catch (Exception ex)
                {
                }
            }
            Logger.ShowInfo("Saving golem's data.....", (Logger)null);
            foreach (SagaMap.Map map in Singleton<MapManager>.Instance.Maps.Values.ToArray<SagaMap.Map>())
            {
                foreach (SagaDB.Actor.Actor actor in map.Actors.Values.ToArray<SagaDB.Actor.Actor>())
                {
                    if (actor.type == ActorType.GOLEM)
                    {
                        try
                        {
                            ActorGolem actorGolem = (ActorGolem)actor;
                            MapServer.charDB.SaveChar(actorGolem.Owner, true, false);
                        }
                        catch
                        {
                        }
                    }
                }
                if (map.IsMapInstance)
                {
                    try
                    {
                        map.OnDestrory();
                    }
                    catch
                    {
                    }
                }
            }
            Logger.ShowInfo("Closing MySQL connection....");
            if (MapServer.charDB.GetType() == typeof(MySQLConnectivity))
            {
                MySQLConnectivity charDb = (MySQLConnectivity)MapServer.charDB;
                while (!charDb.CanClose)
                    Thread.Sleep(100);
            }
            if (MapServer.accountDB.GetType() != typeof(MySQLConnectivity))
                return;
            MySQLConnectivity accountDb = (MySQLConnectivity)MapServer.accountDB;
            while (!accountDb.CanClose)
                Thread.Sleep(100);
        }

        /// <summary>
        /// The CurrentDomain_UnhandledException.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="UnhandledExceptionEventArgs"/>.</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exceptionObject = e.ExceptionObject as Exception;
            MapServer.shutingdown = true;
            Logger.ShowError("Fatal: An unhandled exception is thrown, terminating...");
            Logger.ShowError("Error Message:" + exceptionObject.Message);
            Logger.ShowError("Call Stack:" + exceptionObject.StackTrace);
            Logger.ShowError("Trying to save all player's data");
            MapServer.charDB.SaveServerVar(Singleton<ScriptManager>.Instance.VariableHolder);
            MapClient[] array = new MapClient[MapClientManager.Instance.Clients.Count];
            MapClientManager.Instance.Clients.CopyTo(array);
            foreach (MapClient mapClient in array)
            {
                try
                {
                    if (mapClient.Character != null)
                        mapClient.netIO.Disconnect();
                }
                catch (Exception ex)
                {
                }
            }
            Logger.ShowError("Trying to clear golem actor");
            foreach (SagaMap.Map map in Singleton<MapManager>.Instance.Maps.Values.ToArray<SagaMap.Map>())
            {
                foreach (SagaDB.Actor.Actor actor in map.Actors.Values)
                {
                    if (actor.type == ActorType.GOLEM)
                    {
                        try
                        {
                            ActorGolem actorGolem = (ActorGolem)actor;
                            MapServer.charDB.SaveChar(actorGolem.Owner, true, false);
                        }
                        catch
                        {
                        }
                    }
                }
                if (map.IsMapInstance)
                    map.OnDestrory();
            }
            Logger.ShowInfo("Closing MySQL connection....");
            if (MapServer.charDB.GetType() == typeof(MySQLConnectivity))
            {
                MySQLConnectivity charDb = (MySQLConnectivity)MapServer.charDB;
                while (!charDb.CanClose)
                    Thread.Sleep(100);
            }
            if (MapServer.accountDB.GetType() != typeof(MySQLConnectivity))
                return;
            MySQLConnectivity accountDb = (MySQLConnectivity)MapServer.accountDB;
            while (!accountDb.CanClose)
                Thread.Sleep(100);
        }
    }
}
