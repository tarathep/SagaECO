namespace SagaLib
{
    using SagaLib.VirtualFileSystem;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="FactoryList{K, T}" />.
    /// </summary>
    /// <typeparam name="K">.</typeparam>
    /// <typeparam name="T">.</typeparam>
    public abstract class FactoryList<K, T> where K : new() where T : new()
    {
        /// <summary>
        /// Defines the items.
        /// </summary>
        protected Dictionary<uint, List<T>> items = new Dictionary<uint, List<T>>();

        /// <summary>
        /// Defines the loadingTab.
        /// </summary>
        protected string loadingTab = "";

        /// <summary>
        /// Defines the loadedTab.
        /// </summary>
        protected string loadedTab = "";

        /// <summary>
        /// Defines the databaseName.
        /// </summary>
        protected string databaseName = "";

        /// <summary>
        /// Defines the type.
        /// </summary>
        private FactoryType type;

        /// <summary>
        /// Defines the path.
        /// </summary>
        private string path;

        /// <summary>
        /// Defines the encoding.
        /// </summary>
        private Encoding encoding;

        /// <summary>
        /// Defines the isFolder.
        /// </summary>
        private bool isFolder;

        /// <summary>
        /// Gets the Items.
        /// </summary>
        public Dictionary<uint, List<T>> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Gets or sets the FactoryType.
        /// </summary>
        public FactoryType FactoryType
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="T"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected abstract uint GetKey(T item);

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="T"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected abstract void ParseCSV(T item, string[] paras);

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="T"/>.</param>
        protected abstract void ParseXML(XmlElement root, XmlElement current, T item);

        /// <summary>
        /// The Reload.
        /// </summary>
        public void Reload()
        {
            this.items.Clear();
            this.Init(this.path, this.encoding, this.isFolder);
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="files">The files<see cref="string[]"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void Init(string[] files, Encoding encoding)
        {
            int num = 0;
            this.encoding = encoding;
            switch (this.type)
            {
                case FactoryType.CSV:
                    foreach (string file in files)
                        num += this.InitCSV(file, encoding);
                    break;
                case FactoryType.XML:
                    foreach (string file in files)
                        num += this.InitXML(file, encoding);
                    break;
                default:
                    throw new Exception(string.Format("No FactoryType set for class:{0}", (object)this.ToString()));
            }
            Logger.ProgressBarHide(num.ToString() + this.loadedTab);
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        /// <param name="isFolder">The isFolder<see cref="bool"/>.</param>
        public void Init(string path, Encoding encoding, bool isFolder)
        {
            int num = 0;
            this.path = path;
            this.encoding = encoding;
            this.isFolder = isFolder;
            string[] strArray;
            if (isFolder)
            {
                string pattern = "*.*";
                if (this.FactoryType == FactoryType.CSV)
                    pattern = "*.csv";
                else if (this.FactoryType == FactoryType.XML)
                    pattern = "*.xml";
                strArray = Singleton<VirtualFileSystemManager>.Instance.FileSystem.SearchFile(path, pattern);
            }
            else
                strArray = new string[1] { path };
            switch (this.type)
            {
                case FactoryType.CSV:
                    foreach (string path1 in strArray)
                        num += this.InitCSV(path1, encoding);
                    break;
                case FactoryType.XML:
                    foreach (string path1 in strArray)
                        num += this.InitXML(path1, encoding);
                    break;
                default:
                    throw new Exception(string.Format("No FactoryType set for class:{0}", (object)this.ToString()));
            }
            Logger.ProgressBarHide(num.ToString() + this.loadedTab);
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void Init(string path, Encoding encoding)
        {
            this.Init(path, encoding, false);
        }

        /// <summary>
        /// The FindRoot.
        /// </summary>
        /// <param name="doc">The doc<see cref="XmlDocument"/>.</param>
        /// <returns>The <see cref="XmlElement"/>.</returns>
        private XmlElement FindRoot(XmlDocument doc)
        {
            foreach (object childNode in doc.ChildNodes)
            {
                if (childNode.GetType() == typeof(XmlElement))
                    return (XmlElement)childNode;
            }
            return (XmlElement)null;
        }

        /// <summary>
        /// The ParseNode.
        /// </summary>
        /// <param name="ele">The ele<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="T"/>.</param>
        private void ParseNode(XmlElement ele, T item)
        {
            foreach (object childNode in ele.ChildNodes)
            {
                if (childNode.GetType() == typeof(XmlElement))
                {
                    XmlElement xmlElement = (XmlElement)childNode;
                    this.ParseXML(ele, xmlElement, item);
                    if (xmlElement.ChildNodes.Count != 0)
                        this.ParseNode(xmlElement, item);
                }
            }
        }

        /// <summary>
        /// The InitXML.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int InitXML(string path, Encoding encoding)
        {
            XmlDocument doc = new XmlDocument();
            int num = 0;
            try
            {
                doc.Load(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path));
                XmlElement root = this.FindRoot(doc);
                XmlNodeList childNodes = root.ChildNodes;
                DateTime now = DateTime.Now;
                string loadingTab = this.loadingTab;
                if (childNodes.Count > 100)
                    Logger.ProgressBarShow(0U, (uint)childNodes.Count, loadingTab);
                foreach (object obj1 in childNodes)
                {
                    T obj2 = new T();
                    if (obj1.GetType() == typeof(XmlElement))
                    {
                        XmlElement xmlElement = (XmlElement)obj1;
                        this.ParseXML(root, xmlElement, obj2);
                        if (xmlElement.ChildNodes.Count != 0)
                            this.ParseNode(xmlElement, obj2);
                        uint key = this.GetKey(obj2);
                        if (!this.items.ContainsKey(key))
                            this.items.Add(key, new List<T>());
                        this.items[key].Add(obj2);
                        if ((DateTime.Now - now).TotalMilliseconds > 10.0)
                        {
                            now = DateTime.Now;
                            if (childNodes.Count > 100)
                                Logger.ProgressBarShow((uint)num, (uint)childNodes.Count, loadingTab);
                        }
                        ++num;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex.Message);
            }
            return num;
        }

        /// <summary>
        /// The InitCSV.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int InitCSV(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path), encoding);
            int num1 = 0;
            int num2 = 0;
            string loadingTab = this.loadingTab;
            Logger.ProgressBarShow(0U, (uint)streamReader.BaseStream.Length, loadingTab);
            DateTime now = DateTime.Now;
            while (!streamReader.EndOfStream)
            {
                ++num2;
                string str = streamReader.ReadLine();
                try
                {
                    T obj = new T();
                    if (str.IndexOf('#') != -1)
                        str = str.Substring(0, str.IndexOf('#'));
                    if (!(str == ""))
                    {
                        string[] paras = str.Split(',');
                        if (paras.Length >= 2)
                        {
                            for (int index = 0; index < paras.Length; ++index)
                            {
                                if (paras[index] == "")
                                    paras[index] = "0";
                            }
                            this.ParseCSV(obj, paras);
                            uint key = this.GetKey(obj);
                            if (!this.items.ContainsKey(key))
                                this.items.Add(key, new List<T>());
                            this.items[key].Add(obj);
                            if ((DateTime.Now - now).TotalMilliseconds > 10.0)
                            {
                                now = DateTime.Now;
                                Logger.ProgressBarShow((uint)streamReader.BaseStream.Position, (uint)streamReader.BaseStream.Length, loadingTab);
                            }
                            ++num1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing " + this.databaseName + " db!\r\n       File:" + path + ":" + num2.ToString() + "\r\n       Content:" + str);
                }
            }
            streamReader.Close();
            return num1;
        }

        /// <summary>
        /// Gets or sets the Instance.
        /// </summary>
        public static K Instance
        {
            get
            {
                return FactoryList<K, T>.SingletonHolder.instance;
            }
            set
            {
                FactoryList<K, T>.SingletonHolder.instance = value;
            }
        }

        /// <summary>
        /// Sealed class to avoid any heritage from this helper class.
        /// </summary>
        private sealed class SingletonHolder
        {
            /// <summary>
            /// Defines the instance.
            /// </summary>
            internal static K instance = new K();
        }
    }
}
