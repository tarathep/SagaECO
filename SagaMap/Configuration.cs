namespace SagaMap
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="Configuration" />.
    /// </summary>
    public class Configuration : Singleton<Configuration>
    {
        /// <summary>
        /// Defines the warehouse.
        /// </summary>
        private int warehouse = 100;

        /// <summary>
        /// Defines the loginPass.
        /// </summary>
        private string loginPass = "saga";

        /// <summary>
        /// Defines the hostedMaps.
        /// </summary>
        private List<uint> hostedMaps = new List<uint>();

        /// <summary>
        /// Defines the version.
        /// </summary>
        private SagaLib.Version version = SagaLib.Version.Saga6;

        /// <summary>
        /// Defines the jobSwitchReduceItem.
        /// </summary>
        private uint jobSwitchReduceItem = 10024500;

        /// <summary>
        /// Defines the maxFurnitureCount.
        /// </summary>
        private uint maxFurnitureCount = 100;

        /// <summary>
        /// Defines the sqlLog.
        /// </summary>
        private bool sqlLog = true;

        /// <summary>
        /// Defines the startupSetting.
        /// </summary>
        private Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting> startupSetting = new Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting>();

        /// <summary>
        /// Defines the motd.
        /// </summary>
        private List<string> motd = new List<string>();

        /// <summary>
        /// Defines the reference.
        /// </summary>
        private List<string> reference = new List<string>();

        /// <summary>
        /// Defines the monitorAccounts.
        /// </summary>
        private List<string> monitorAccounts = new List<string>();

        /// <summary>
        /// Defines the questGoldRate.
        /// </summary>
        private int questGoldRate = 100;

        /// <summary>
        /// Defines the questUpdateTime.
        /// </summary>
        private int questUpdateTime = 24;

        /// <summary>
        /// Defines the questUpdateAmount.
        /// </summary>
        private int questUpdateAmount = 5;

        /// <summary>
        /// Defines the questPointsMax.
        /// </summary>
        private int questPointsMax = 15;

        /// <summary>
        /// Defines the stampDropRate.
        /// </summary>
        private int stampDropRate = 100;

        /// <summary>
        /// Defines the ringfameemblem.
        /// </summary>
        private int ringfameemblem = 300;

        /// <summary>
        /// Defines the deathBaseRateEmil.
        /// </summary>
        private float deathBaseRateEmil = 0.1f;

        /// <summary>
        /// Defines the deathJobRateEmil.
        /// </summary>
        private float deathJobRateEmil = 0.02f;

        /// <summary>
        /// Defines the deathBaseRateDom.
        /// </summary>
        private float deathBaseRateDom = 0.1f;

        /// <summary>
        /// Defines the deathJobRateDom.
        /// </summary>
        private float deathJobRateDom = 0.02f;

        /// <summary>
        /// Defines the pvpDmgRatePhysic.
        /// </summary>
        private float pvpDmgRatePhysic = 1f;

        /// <summary>
        /// Defines the pvpDmgRateMagic.
        /// </summary>
        private float pvpDmgRateMagic = 1f;

        /// <summary>
        /// Defines the payloadRate.
        /// </summary>
        private float payloadRate = 1f;

        /// <summary>
        /// Defines the volumeRate.
        /// </summary>
        private float volumeRate = 1f;

        /// <summary>
        /// Defines the onlineStatic.
        /// </summary>
        private bool onlineStatic = true;

        /// <summary>
        /// Defines the statisticsPage.
        /// </summary>
        private string statisticsPage = "index.htm";

        /// <summary>
        /// Defines the multipleDrop.
        /// </summary>
        private bool multipleDrop = false;

        /// <summary>
        /// Defines the globalDropRate.
        /// </summary>
        private float globalDropRate = 1f;

        /// <summary>
        /// Defines the specialDropRate.
        /// </summary>
        private float specialDropRate = 1f;

        /// <summary>
        /// Defines the dbhost.
        /// </summary>
        private string dbhost;

        /// <summary>
        /// Defines the dbuser.
        /// </summary>
        private string dbuser;

        /// <summary>
        /// Defines the dbpass.
        /// </summary>
        private string dbpass;

        /// <summary>
        /// Defines the dbname.
        /// </summary>
        private string dbname;

        /// <summary>
        /// Defines the language.
        /// </summary>
        private string language;

        /// <summary>
        /// Defines the loginhost.
        /// </summary>
        private string loginhost;

        /// <summary>
        /// Defines the host.
        /// </summary>
        private string host;

        /// <summary>
        /// Defines the dbport.
        /// </summary>
        private int dbport;

        /// <summary>
        /// Defines the port.
        /// </summary>
        private int port;

        /// <summary>
        /// Defines the loglevel.
        /// </summary>
        private int loglevel;

        /// <summary>
        /// Defines the loginport.
        /// </summary>
        private int loginport;

        /// <summary>
        /// Defines the dbType.
        /// </summary>
        private int dbType;

        /// <summary>
        /// Defines the encoding.
        /// </summary>
        private string encoding;

        /// <summary>
        /// Defines the clientversion.
        /// </summary>
        private string clientversion;

        /// <summary>
        /// Defines the exprate.
        /// </summary>
        private int exprate;

        /// <summary>
        /// Defines the questrate.
        /// </summary>
        private int questrate;

        /// <summary>
        /// Gets or sets the Host.
        /// </summary>
        public string Host
        {
            get
            {
                return this.host;
            }
            set
            {
                this.host = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBHost.
        /// </summary>
        public string DBHost
        {
            get
            {
                return this.dbhost;
            }
            set
            {
                this.dbhost = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBUser.
        /// </summary>
        public string DBUser
        {
            get
            {
                return this.dbuser;
            }
            set
            {
                this.dbuser = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBPass.
        /// </summary>
        public string DBPass
        {
            get
            {
                return this.dbpass;
            }
            set
            {
                this.dbpass = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBName.
        /// </summary>
        public string DBName
        {
            get
            {
                return this.dbname;
            }
            set
            {
                this.dbname = value;
            }
        }

        /// <summary>
        /// Gets or sets the LoginPass.
        /// </summary>
        public string LoginPass
        {
            get
            {
                return this.loginPass;
            }
            set
            {
                this.loginPass = value;
            }
        }

        /// <summary>
        /// Gets or sets the ClientVersion.
        /// </summary>
        public string ClientVersion
        {
            get
            {
                return this.clientversion;
            }
            set
            {
                this.clientversion = value;
            }
        }

        /// <summary>
        /// Gets or sets the Port.
        /// </summary>
        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        /// <summary>
        /// Gets or sets the LoginHost.
        /// </summary>
        public string LoginHost
        {
            get
            {
                return this.loginhost;
            }
            set
            {
                this.loginhost = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBPort.
        /// </summary>
        public int DBPort
        {
            get
            {
                return this.dbport;
            }
            set
            {
                this.dbport = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBType.
        /// </summary>
        public int DBType
        {
            get
            {
                return this.dbType;
            }
            set
            {
                this.dbType = value;
            }
        }

        /// <summary>
        /// Gets or sets the LoginPort.
        /// </summary>
        public int LoginPort
        {
            get
            {
                return this.loginport;
            }
            set
            {
                this.loginport = value;
            }
        }

        /// <summary>
        /// Gets the EXPRate.
        /// </summary>
        public int EXPRate
        {
            get
            {
                return this.exprate;
            }
        }

        /// <summary>
        /// Gets the StampDropRate.
        /// </summary>
        public int StampDropRate
        {
            get
            {
                return this.stampDropRate;
            }
        }

        /// <summary>
        /// Gets the QuestRate.
        /// </summary>
        public int QuestRate
        {
            get
            {
                return this.questrate;
            }
        }

        /// <summary>
        /// Gets the QuestGoldRate.
        /// </summary>
        public int QuestGoldRate
        {
            get
            {
                return this.questGoldRate;
            }
        }

        /// <summary>
        /// Gets the WarehouseLimit.
        /// </summary>
        public int WarehouseLimit
        {
            get
            {
                return this.warehouse;
            }
        }

        /// <summary>
        /// Gets the Version.
        /// </summary>
        public SagaLib.Version Version
        {
            get
            {
                return this.version;
            }
        }

        /// <summary>
        /// Gets the JobSwitchReduceItem.
        /// </summary>
        public uint JobSwitchReduceItem
        {
            get
            {
                return this.jobSwitchReduceItem;
            }
        }

        /// <summary>
        /// Gets the RingFameNeededForEmblem.
        /// </summary>
        public int RingFameNeededForEmblem
        {
            get
            {
                return this.ringfameemblem;
            }
        }

        /// <summary>
        /// Gets or sets the StartupSetting.
        /// </summary>
        public Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting> StartupSetting
        {
            get
            {
                return this.startupSetting;
            }
            set
            {
                this.startupSetting = value;
            }
        }

        /// <summary>
        /// Gets the Motd.
        /// </summary>
        public List<string> Motd
        {
            get
            {
                return this.motd;
            }
        }

        /// <summary>
        /// Gets the ScriptReference.
        /// </summary>
        public List<string> ScriptReference
        {
            get
            {
                return this.reference;
            }
        }

        /// <summary>
        /// Gets the MonitorAccounts.
        /// </summary>
        public List<string> MonitorAccounts
        {
            get
            {
                return this.monitorAccounts;
            }
        }

        /// <summary>
        /// Gets or sets the Language.
        /// </summary>
        public string Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;
            }
        }

        /// <summary>
        /// Gets or sets the HostedMaps.
        /// </summary>
        public List<uint> HostedMaps
        {
            get
            {
                return this.hostedMaps;
            }
            set
            {
                this.hostedMaps = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether SQLLog.
        /// </summary>
        public bool SQLLog
        {
            get
            {
                return this.sqlLog;
            }
        }

        /// <summary>
        /// Gets or sets the QuestUpdateTime.
        /// </summary>
        public int QuestUpdateTime
        {
            get
            {
                return this.questUpdateTime;
            }
            set
            {
                this.questUpdateTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the QuestUpdateAmount.
        /// </summary>
        public int QuestUpdateAmount
        {
            get
            {
                return this.questUpdateAmount;
            }
            set
            {
                this.questUpdateAmount = value;
            }
        }

        /// <summary>
        /// Gets or sets the QuestPointsMax.
        /// </summary>
        public int QuestPointsMax
        {
            get
            {
                return this.questPointsMax;
            }
            set
            {
                this.questPointsMax = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxFurnitureCount.
        /// </summary>
        public uint MaxFurnitureCount
        {
            get
            {
                return this.maxFurnitureCount;
            }
            set
            {
                this.maxFurnitureCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the LogLevel.
        /// </summary>
        public int LogLevel
        {
            get
            {
                return this.loglevel;
            }
            set
            {
                this.loglevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the DeathPenaltyBaseEmil.
        /// </summary>
        public float DeathPenaltyBaseEmil
        {
            get
            {
                return this.deathBaseRateEmil;
            }
            set
            {
                this.deathBaseRateEmil = value;
            }
        }

        /// <summary>
        /// Gets or sets the DeathPenaltyJobEmil.
        /// </summary>
        public float DeathPenaltyJobEmil
        {
            get
            {
                return this.deathJobRateEmil;
            }
            set
            {
                this.deathJobRateEmil = value;
            }
        }

        /// <summary>
        /// Gets or sets the DeathPenaltyBaseDominion.
        /// </summary>
        public float DeathPenaltyBaseDominion
        {
            get
            {
                return this.deathBaseRateDom;
            }
            set
            {
                this.deathBaseRateDom = value;
            }
        }

        /// <summary>
        /// Gets or sets the DeathPenaltyJobDominion.
        /// </summary>
        public float DeathPenaltyJobDominion
        {
            get
            {
                return this.deathJobRateDom;
            }
            set
            {
                this.deathJobRateDom = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether OnlineStatistics.
        /// </summary>
        public bool OnlineStatistics
        {
            get
            {
                return this.onlineStatic;
            }
            set
            {
                this.onlineStatic = value;
            }
        }

        /// <summary>
        /// Gets or sets the StatisticsPagePath.
        /// </summary>
        public string StatisticsPagePath
        {
            get
            {
                return this.statisticsPage;
            }
            set
            {
                this.statisticsPage = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether MultipleDrop.
        /// </summary>
        public bool MultipleDrop
        {
            get
            {
                return this.multipleDrop;
            }
            set
            {
                this.multipleDrop = value;
            }
        }

        /// <summary>
        /// Gets or sets the GlobalDropRate.
        /// </summary>
        public float GlobalDropRate
        {
            get
            {
                return this.globalDropRate;
            }
            set
            {
                this.globalDropRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the SpecialDropRate.
        /// </summary>
        public float SpecialDropRate
        {
            get
            {
                return this.specialDropRate;
            }
            set
            {
                this.specialDropRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the TwitterID.
        /// </summary>
        public string TwitterID
        {
            get
            {
                return this.TwitterID;
            }
            set
            {
                this.TwitterID = value;
            }
        }

        /// <summary>
        /// Gets or sets the TwitterPasswd.
        /// </summary>
        public string TwitterPasswd
        {
            get
            {
                return this.TwitterPasswd;
            }
            set
            {
                this.TwitterPasswd = value;
            }
        }

        /// <summary>
        /// Gets or sets the PVPDamageRatePhysic.
        /// </summary>
        public float PVPDamageRatePhysic
        {
            get
            {
                return this.pvpDmgRatePhysic;
            }
            set
            {
                this.pvpDmgRatePhysic = value;
            }
        }

        /// <summary>
        /// Gets or sets the PVPDamageRateMagic.
        /// </summary>
        public float PVPDamageRateMagic
        {
            get
            {
                return this.pvpDmgRateMagic;
            }
            set
            {
                this.pvpDmgRateMagic = value;
            }
        }

        /// <summary>
        /// Gets or sets the PayloadRate.
        /// </summary>
        public float PayloadRate
        {
            get
            {
                return this.payloadRate;
            }
            set
            {
                this.payloadRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the VolumeRate.
        /// </summary>
        public float VolumeRate
        {
            get
            {
                return this.volumeRate;
            }
            set
            {
                this.volumeRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the DBEncoding.
        /// </summary>
        public string DBEncoding
        {
            get
            {
                if (this.encoding == null)
                {
                    Logger.ShowDebug("DB Encoding not set, set to default value: UTF-8", Logger.CurrentLogger);
                    this.encoding = "utf-8";
                }
                return this.encoding;
            }
            set
            {
                this.encoding = value;
            }
        }

        /// <summary>
        /// The InitXML.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        private void InitXML(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            bool flag = false;
            try
            {
                xmlDocument.Load(path);
                foreach (object childNode in xmlDocument["SagaMap"].ChildNodes)
                {
                    if (childNode.GetType() == typeof(XmlElement))
                    {
                        XmlElement xmlElement1 = (XmlElement)childNode;
                        switch (xmlElement1.Name.ToLower())
                        {
                            case "host":
                                this.host = xmlElement1.InnerText;
                                break;
                            case "port":
                                this.port = int.Parse(xmlElement1.InnerText);
                                break;
                            case "dbtype":
                                this.dbType = int.Parse(xmlElement1.InnerText);
                                break;
                            case "dbhost":
                                this.dbhost = xmlElement1.InnerText;
                                break;
                            case "dbport":
                                this.dbport = int.Parse(xmlElement1.InnerText);
                                break;
                            case "dbuser":
                                this.dbuser = xmlElement1.InnerText;
                                break;
                            case "dbpass":
                                this.dbpass = xmlElement1.InnerText;
                                break;
                            case "dbname":
                                this.dbname = xmlElement1.InnerText;
                                break;
                            case "loginhost":
                                this.loginhost = xmlElement1.InnerText;
                                break;
                            case "loginport":
                                this.loginport = int.Parse(xmlElement1.InnerText);
                                break;
                            case "loginpass":
                                this.loginPass = xmlElement1.InnerText;
                                break;
                            case "loglevel":
                                this.loglevel = int.Parse(xmlElement1.InnerText);
                                break;
                            case "clientversion":
                                this.clientversion = xmlElement1.InnerText;
                                break;
                            case "language":
                                this.language = xmlElement1.InnerText;
                                break;
                            case "dbencoding":
                                this.encoding = xmlElement1.InnerText;
                                break;
                            case "motd":
                                string innerText1 = xmlElement1.InnerText;
                                char[] chArray1 = new char[1] { '\n' };
                                foreach (string str1 in innerText1.Split(chArray1))
                                {
                                    string str2 = str1.Replace("\r", "").Replace(" ", "");
                                    if (str2 != "")
                                        this.motd.Add(str2);
                                }
                                break;
                            case "monitoraccounts":
                                string innerText2 = xmlElement1.InnerText;
                                char[] chArray2 = new char[1] { '\n' };
                                foreach (string str1 in innerText2.Split(chArray2))
                                {
                                    string str2 = str1.Replace("\r", "").Replace(" ", "");
                                    if (str2 != "")
                                        this.monitorAccounts.Add(str2);
                                }
                                break;
                            case "hostedmaps":
                                IEnumerator enumerator1 = xmlElement1.ChildNodes.GetEnumerator();
                                try
                                {
                                    while (enumerator1.MoveNext())
                                    {
                                        object current = enumerator1.Current;
                                        if (current.GetType() == typeof(XmlElement))
                                        {
                                            XmlElement xmlElement2 = (XmlElement)current;
                                            switch (xmlElement2.Name.ToLower())
                                            {
                                                case "mapid":
                                                    this.hostedMaps.Add(uint.Parse(xmlElement2.InnerText));
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                }
                                finally
                                {
                                    (enumerator1 as IDisposable)?.Dispose();
                                }
                            case "scriptreference":
                                IEnumerator enumerator2 = xmlElement1.ChildNodes.GetEnumerator();
                                try
                                {
                                    while (enumerator2.MoveNext())
                                    {
                                        object current = enumerator2.Current;
                                        if (current.GetType() == typeof(XmlElement))
                                        {
                                            XmlElement xmlElement2 = (XmlElement)current;
                                            switch (xmlElement2.Name.ToLower())
                                            {
                                                case "assembly":
                                                    this.reference.Add(xmlElement2.InnerText);
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                }
                                finally
                                {
                                    (enumerator2 as IDisposable)?.Dispose();
                                }
                            case "exprate":
                                this.exprate = int.Parse(xmlElement1.InnerText);
                                break;
                            case "stampdroprate":
                                this.stampDropRate = int.Parse(xmlElement1.InnerText);
                                break;
                            case "questrate":
                                this.questrate = int.Parse(xmlElement1.InnerText);
                                break;
                            case "questgoldrate":
                                this.questGoldRate = int.Parse(xmlElement1.InnerText);
                                break;
                            case "warehouselimit":
                                this.warehouse = int.Parse(xmlElement1.InnerText);
                                break;
                            case "version":
                                try
                                {
                                    this.version = (SagaLib.Version)Enum.Parse(typeof(SagaLib.Version), xmlElement1.InnerText);
                                    flag = true;
                                    break;
                                }
                                catch
                                {
                                    Logger.ShowWarning(string.Format("Cannot find Version:[{0}], using default version:[{1}]", (object)xmlElement1.InnerText, (object)this.version));
                                    break;
                                }
                            case "jobswitchreduceitem":
                                this.jobSwitchReduceItem = uint.Parse(xmlElement1.InnerText);
                                break;
                            case "ringfameneededforemblem":
                                this.ringfameemblem = int.Parse(xmlElement1.InnerText);
                                break;
                            case "maxfurniturecount":
                                this.maxFurnitureCount = uint.Parse(xmlElement1.InnerText);
                                break;
                            case "deathpenaltybaseemil":
                                this.deathBaseRateEmil = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "deathpenaltyjobemil":
                                this.deathJobRateEmil = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "deathpenaltybasedominion":
                                this.deathBaseRateDom = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "deathpenaltyjobdominion":
                                this.deathJobRateDom = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "sqllog":
                                this.sqlLog = xmlElement1.InnerText == "1";
                                break;
                            case "questupdatetime":
                                this.questUpdateTime = int.Parse(xmlElement1.InnerText);
                                break;
                            case "questupdateamount":
                                this.questUpdateAmount = int.Parse(xmlElement1.InnerText);
                                break;
                            case "questpointsmax":
                                this.questPointsMax = int.Parse(xmlElement1.InnerText);
                                break;
                            case "onlinestatistic":
                                this.onlineStatic = int.Parse(xmlElement1.InnerText) == 1;
                                break;
                            case "statisticpagepath":
                                this.statisticsPage = xmlElement1.InnerText;
                                break;
                            case "sqlloglevel":
                                Logger.SQLLogLevel.Value = int.Parse(xmlElement1.InnerText);
                                break;
                            case "multipledrop":
                                this.multipleDrop = xmlElement1.InnerText == "1";
                                break;
                            case "globaldroprate":
                                this.globalDropRate = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "specialdroprate":
                                this.specialDropRate = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "pvpdamageratephysic":
                                this.pvpDmgRatePhysic = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "pvpdamageratemagic":
                                this.pvpDmgRateMagic = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "payloadrate":
                                this.payloadRate = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "volumerate":
                                this.volumeRate = (float)int.Parse(xmlElement1.InnerText) / 100f;
                                break;
                            case "TwitterID":
                                this.TwitterID = xmlElement1.InnerText;
                                break;
                            case "TwitterPasswd":
                                this.TwitterPasswd = xmlElement1.InnerText;
                                break;
                        }
                    }
                }
                if (!flag)
                    Logger.ShowWarning(string.Format("Packet Version not set, using default version:[{0}], \r\n         please change Config/SagaMap.xml to set version", (object)this.version));
                Logger.ShowInfo("Done reading configuration...");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The InitDat.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        private void InitDat(string path)
        {
            BinaryReader binaryReader = new BinaryReader((Stream)new FileStream(path, FileMode.Open, FileAccess.Read));
            if (binaryReader.ReadInt32() != 305419896)
                return;
            binaryReader.ReadInt32();
            byte num1 = binaryReader.ReadByte();
            this.host = Global.Unicode.GetString(binaryReader.ReadBytes((int)num1));
            this.port = binaryReader.ReadInt32();
            byte num2 = binaryReader.ReadByte();
            this.dbhost = Global.Unicode.GetString(binaryReader.ReadBytes((int)num2));
            this.dbport = binaryReader.ReadInt32();
            this.loglevel = binaryReader.ReadInt32();
            this.sqlLog = binaryReader.ReadBoolean();
            Logger.SQLLogLevel.Value = binaryReader.ReadInt32();
            byte num3 = binaryReader.ReadByte();
            this.dbuser = Global.Unicode.GetString(binaryReader.ReadBytes((int)num3));
            byte num4 = binaryReader.ReadByte();
            this.dbpass = Global.Unicode.GetString(binaryReader.ReadBytes((int)num4));
            byte num5 = binaryReader.ReadByte();
            this.loginhost = Global.Unicode.GetString(binaryReader.ReadBytes((int)num5));
            this.loginport = binaryReader.ReadInt32();
            byte num6 = binaryReader.ReadByte();
            this.loginPass = Global.Unicode.GetString(binaryReader.ReadBytes((int)num6));
            byte num7 = binaryReader.ReadByte();
            this.language = Global.Unicode.GetString(binaryReader.ReadBytes((int)num7));
            this.version = (SagaLib.Version)binaryReader.ReadByte();
            byte num8 = binaryReader.ReadByte();
            this.encoding = Global.Unicode.GetString(binaryReader.ReadBytes((int)num8));
            this.exprate = binaryReader.ReadInt32();
            this.questrate = binaryReader.ReadInt32();
            this.questGoldRate = binaryReader.ReadInt32();
            this.stampDropRate = binaryReader.ReadInt32();
            this.questUpdateTime = binaryReader.ReadInt32();
            this.questUpdateAmount = binaryReader.ReadInt32();
            this.questPointsMax = binaryReader.ReadInt32();
            this.warehouse = binaryReader.ReadInt32();
            this.deathBaseRateEmil = binaryReader.ReadSingle();
            this.deathJobRateEmil = binaryReader.ReadSingle();
            this.deathBaseRateDom = binaryReader.ReadSingle();
            this.deathJobRateDom = binaryReader.ReadSingle();
            this.jobSwitchReduceItem = binaryReader.ReadUInt32();
            this.ringfameemblem = binaryReader.ReadInt32();
            this.maxFurnitureCount = binaryReader.ReadUInt32();
            this.onlineStatic = binaryReader.ReadBoolean();
            byte num9 = binaryReader.ReadByte();
            this.statisticsPage = Global.Unicode.GetString(binaryReader.ReadBytes((int)num9));
            this.multipleDrop = binaryReader.ReadBoolean();
            this.globalDropRate = binaryReader.ReadSingle();
            int num10 = binaryReader.ReadInt32();
            for (int index = 0; index < num10; ++index)
            {
                byte num11 = binaryReader.ReadByte();
                this.motd.Add(Global.Unicode.GetString(binaryReader.ReadBytes((int)num11)));
            }
            int num12 = binaryReader.ReadInt32();
            for (int index = 0; index < num12; ++index)
            {
                byte num11 = binaryReader.ReadByte();
                this.reference.Add(Global.Unicode.GetString(binaryReader.ReadBytes((int)num11)));
            }
            int num13 = binaryReader.ReadInt32();
            for (int index = 0; index < num13; ++index)
                this.hostedMaps.Add(binaryReader.ReadUInt32());
        }

        /// <summary>
        /// The Initialization.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void Initialization(string path)
        {
            this.hostedMaps.Clear();
            this.InitXML(path);
        }
    }
}
