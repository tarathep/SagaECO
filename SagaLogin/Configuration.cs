namespace SagaLogin
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="Configuration" />.
    /// </summary>
    public class Configuration : Singleton<Configuration>
    {
        /// <summary>
        /// Defines the password.
        /// </summary>
        private string password = "saga";

        /// <summary>
        /// Defines the startup.
        /// </summary>
        private Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting> startup = new Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting>();

        /// <summary>
        /// Defines the startitem.
        /// </summary>
        private Dictionary<PC_RACE, Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>> startitem = new Dictionary<PC_RACE, Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>>();

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
        /// Defines the dbType.
        /// </summary>
        private int dbType;

        /// <summary>
        /// Defines the encoding.
        /// </summary>
        private string encoding;

        /// <summary>
        /// Defines the version.
        /// </summary>
        private SagaLib.Version version;

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
        /// Gets or sets the Password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
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
        /// Gets or sets the StartupSetting.
        /// </summary>
        public Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting> StartupSetting
        {
            get
            {
                return this.startup;
            }
            set
            {
                this.startup = value;
            }
        }

        /// <summary>
        /// Gets or sets the StartItem.
        /// </summary>
        public Dictionary<PC_RACE, Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>> StartItem
        {
            get
            {
                return this.startitem;
            }
            set
            {
                this.startitem = value;
            }
        }

        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        public SagaLib.Version Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
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
                    Logger.ShowDebug("DB Encoding not set, set to default value: GBK", Logger.CurrentLogger);
                    this.encoding = "GBK";
                }
                return this.encoding;
            }
            set
            {
                this.encoding = value;
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
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.startitem.Add(PC_RACE.EMIL, new Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>());
            this.startitem.Add(PC_RACE.TITANIA, new Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>());
            this.startitem.Add(PC_RACE.DOMINION, new Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>());
            this.startitem.Add(PC_RACE.DEM, new Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>());
        }

        /// <summary>
        /// The Initialization.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void Initialization(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(path);
                foreach (object childNode1 in xmlDocument["SagaLogin"].ChildNodes)
                {
                    if (childNode1.GetType() == typeof(XmlElement))
                    {
                        XmlElement xmlElement1 = (XmlElement)childNode1;
                        switch (xmlElement1.Name.ToLower())
                        {
                            case "dbtype":
                                this.dbType = int.Parse(xmlElement1.InnerText);
                                break;
                            case "port":
                                this.port = int.Parse(xmlElement1.InnerText);
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
                            case "dbencoding":
                                this.encoding = xmlElement1.InnerText;
                                break;
                            case "password":
                                this.password = xmlElement1.InnerText;
                                break;
                            case "loglevel":
                                this.loglevel = int.Parse(xmlElement1.InnerText);
                                break;
                            case "startstatus":
                                PC_RACE key1 = PC_RACE.EMIL;
                                switch (xmlElement1.Attributes["race"].Value.ToUpper())
                                {
                                    case "EMIL":
                                        key1 = PC_RACE.EMIL;
                                        break;
                                    case "TITANIA":
                                        key1 = PC_RACE.TITANIA;
                                        break;
                                    case "DOMINION":
                                        key1 = PC_RACE.DOMINION;
                                        break;
                                    case "DEM":
                                        key1 = PC_RACE.DEM;
                                        break;
                                }
                                SagaLogin.Configurations.StartupSetting startupSetting = new SagaLogin.Configurations.StartupSetting();
                                foreach (object childNode2 in xmlElement1.ChildNodes)
                                {
                                    if (childNode2.GetType() == typeof(XmlElement))
                                    {
                                        XmlElement xmlElement2 = (XmlElement)childNode2;
                                        switch (xmlElement2.Name.ToLower())
                                        {
                                            case "str":
                                                startupSetting.Str = ushort.Parse(xmlElement2.InnerText);
                                                break;
                                            case "dex":
                                                startupSetting.Dex = ushort.Parse(xmlElement2.InnerText);
                                                break;
                                            case "int":
                                                startupSetting.Int = ushort.Parse(xmlElement2.InnerText);
                                                break;
                                            case "vit":
                                                startupSetting.Vit = ushort.Parse(xmlElement2.InnerText);
                                                break;
                                            case "agi":
                                                startupSetting.Agi = ushort.Parse(xmlElement2.InnerText);
                                                break;
                                            case "mag":
                                                startupSetting.Mag = ushort.Parse(xmlElement2.InnerText);
                                                break;
                                            case "startmap":
                                                startupSetting.StartMap = uint.Parse(xmlElement2.InnerText);
                                                break;
                                            case "startx":
                                                startupSetting.X = byte.Parse(xmlElement2.InnerText);
                                                break;
                                            case "starty":
                                                startupSetting.Y = byte.Parse(xmlElement2.InnerText);
                                                break;
                                        }
                                    }
                                }
                                this.startup.Add(key1, startupSetting);
                                break;
                            case "startitem":
                                Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>> dictionary = (Dictionary<PC_GENDER, List<SagaLogin.Configurations.StartItem>>)null;
                                PC_GENDER key2 = PC_GENDER.FEMALE;
                                switch (xmlElement1.Attributes["race"].Value.ToUpper())
                                {
                                    case "EMIL":
                                        dictionary = this.startitem[PC_RACE.EMIL];
                                        break;
                                    case "TITANIA":
                                        dictionary = this.startitem[PC_RACE.TITANIA];
                                        break;
                                    case "DOMINION":
                                        dictionary = this.startitem[PC_RACE.DOMINION];
                                        break;
                                    case "DEM":
                                        dictionary = this.startitem[PC_RACE.DEM];
                                        break;
                                }
                                switch (xmlElement1.Attributes["gender"].Value.ToUpper())
                                {
                                    case "MALE":
                                        key2 = PC_GENDER.MALE;
                                        break;
                                    case "FEMALE":
                                        key2 = PC_GENDER.FEMALE;
                                        break;
                                }
                                List<SagaLogin.Configurations.StartItem> startItemList = new List<SagaLogin.Configurations.StartItem>();
                                dictionary.Add(key2, startItemList);
                                IEnumerator enumerator = xmlElement1.ChildNodes.GetEnumerator();
                                try
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        object current = enumerator.Current;
                                        if (current.GetType() == typeof(XmlElement))
                                        {
                                            XmlElement xmlElement2 = (XmlElement)current;
                                            SagaLogin.Configurations.StartItem startItem = new SagaLogin.Configurations.StartItem();
                                            foreach (object childNode2 in xmlElement2.ChildNodes)
                                            {
                                                if (childNode2.GetType() == typeof(XmlElement))
                                                {
                                                    XmlElement xmlElement3 = (XmlElement)childNode2;
                                                    switch (xmlElement3.Name.ToLower())
                                                    {
                                                        case "itemid":
                                                            startItem.ItemID = uint.Parse(xmlElement3.InnerText);
                                                            break;
                                                        case "slot":
                                                            startItem.Slot = (ContainerType)Enum.Parse(typeof(ContainerType), xmlElement3.InnerText.ToUpper());
                                                            break;
                                                        case "count":
                                                            startItem.Count = byte.Parse(xmlElement3.InnerText);
                                                            break;
                                                    }
                                                }
                                            }
                                            startItemList.Add(startItem);
                                        }
                                    }
                                    break;
                                }
                                finally
                                {
                                    (enumerator as IDisposable)?.Dispose();
                                }
                        }
                    }
                }
                Logger.ShowInfo("Done reading configuration...");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
    }
}
