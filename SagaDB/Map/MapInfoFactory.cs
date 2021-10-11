namespace SagaDB.Map
{
    using ICSharpCode.SharpZipLib.Zip;
    using SagaDB.Marionette;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="MapInfoFactory" />.
    /// </summary>
    public class MapInfoFactory : Singleton<MapInfoFactory>
    {
        /// <summary>
        /// Defines the mapInfos.
        /// </summary>
        private Dictionary<uint, SagaDB.Map.MapInfo> mapInfos = new Dictionary<uint, SagaDB.Map.MapInfo>();

        /// <summary>
        /// Defines the mapObjects.
        /// </summary>
        private Dictionary<string, List<MapObject>> mapObjects;

        /// <summary>
        /// Gets the MapInfo.
        /// </summary>
        public Dictionary<uint, SagaDB.Map.MapInfo> MapInfo
        {
            get
            {
                return this.mapInfos;
            }
        }

        /// <summary>
        /// Gets the MapObjects.
        /// </summary>
        public Dictionary<string, List<MapObject>> MapObjects
        {
            get
            {
                return this.mapObjects;
            }
        }

        /// <summary>
        /// The LoadMapObjects.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void LoadMapObjects(string path)
        {
            this.mapObjects = (Dictionary<string, List<MapObject>>)new BinaryFormatter().Deserialize(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path));
        }

        /// <summary>
        /// The ApplyMapObject.
        /// </summary>
        public void ApplyMapObject()
        {
            if (this.mapObjects == null)
                return;
            foreach (string key1 in this.mapObjects.Keys)
            {
                uint key2 = uint.Parse(key1);
                if (this.mapInfos.ContainsKey(key2))
                {
                    SagaDB.Map.MapInfo mapInfo = this.mapInfos[key2];
                    foreach (MapObject mapObject in this.mapObjects[key1])
                    {
                        if (mapObject.Name.IndexOf("kadoukyo") == -1 && mapObject.Name.IndexOf("hasi") == -1)
                        {
                            int[,][] positionMatrix = mapObject.PositionMatrix;
                            for (int index1 = 0; index1 < mapObject.Width; ++index1)
                            {
                                for (int index2 = 0; index2 < mapObject.Height; ++index2)
                                {
                                    int index3 = (int)mapObject.X + positionMatrix[index1, index2][0];
                                    int index4 = (int)mapObject.Y + positionMatrix[index1, index2][1];
                                    if (index3 < (int)mapInfo.width && index4 < (int)mapInfo.height && (index3 >= 0 && index4 >= 0))
                                        mapInfo.walkable[index3, index4] = (byte)0;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The LoadGatherInterval.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void LoadGatherInterval(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path), encoding);
            Logger.ShowInfo("Loading Gather database...");
            int num1 = 0;
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                try
                {
                    SagaDB.Map.MapInfo mapInfo = (SagaDB.Map.MapInfo)null;
                    if (!(str == ""))
                    {
                        if (!(str.Substring(0, 1) == "#"))
                        {
                            string[] strArray = str.Split(',');
                            for (int index = 0; index < strArray.Length; ++index)
                            {
                                if (strArray[index] == "" || strArray[index].ToLower() == "null")
                                    strArray[index] = "0";
                            }
                            uint key = uint.Parse(strArray[0]);
                            if (this.mapInfos.ContainsKey(key))
                                mapInfo = this.mapInfos[key];
                            if (mapInfo != null)
                            {
                                for (int index = 0; index < 8; ++index)
                                {
                                    int num2 = int.Parse(strArray[1 + index]);
                                    if (num2 > 0)
                                        mapInfo.gatherInterval.Add((GatherType)index, num2);
                                }
                                ++num1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing gather db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Logger.ShowInfo(num1.ToString() + " gather informations loaded.");
            streamReader.Close();
        }

        /// <summary>
        /// The LoadFlags.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void LoadFlags(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path));
                foreach (object childNode in xmlDocument["MapFlags"].ChildNodes)
                {
                    if (childNode.GetType() == typeof(XmlElement))
                    {
                        XmlElement xmlElement = (XmlElement)childNode;
                        switch (xmlElement.Name.ToLower())
                        {
                            case "map":
                                uint key = uint.Parse(xmlElement.Attributes[0].InnerText);
                                if (this.mapInfos.ContainsKey(key))
                                {
                                    this.mapInfos[key].Flag.Value = int.Parse(xmlElement.InnerText);
                                    continue;
                                }
                                continue;
                            default:
                                continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void Init(string path)
        {
            this.Init(path, true);
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="fullinfo">The fullinfo<see cref="bool"/>.</param>
        public void Init(string path, bool fullinfo)
        {
            Stream stream = Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path);
            ZipFile zipFile = new ZipFile(stream);
            int count1 = (int)zipFile.Count;
            stream.Position = 0L;
            ZipInputStream zipInputStream = new ZipInputStream(stream);
            string label = "Loading MapInfo.zip";
            Logger.ProgressBarShow(0U, (uint)count1, label);
            DateTime now = DateTime.Now;
            ZipEntry nextEntry = zipInputStream.GetNextEntry();
            byte[] buffer = new byte[2048];
            int num1 = 0;
            while (nextEntry != null)
            {
                MemoryStream memoryStream = new MemoryStream((int)nextEntry.Size);
                while (true)
                {
                    int count2 = zipInputStream.Read(buffer, 0, 2048);
                    if (count2 > 0)
                        memoryStream.Write(buffer, 0, count2);
                    else
                        break;
                }
                memoryStream.Flush();
                BinaryReader binaryReader = new BinaryReader((Stream)memoryStream);
                memoryStream.Position = 0L;
                SagaDB.Map.MapInfo mapInfo = new SagaDB.Map.MapInfo()
                {
                    id = binaryReader.ReadUInt32()
                };
                mapInfo.id = uint.Parse(Path.GetFileNameWithoutExtension(nextEntry.Name));
                mapInfo.name = Global.Unicode.GetString(binaryReader.ReadBytes(32)).Replace("\0", "");
                mapInfo.width = binaryReader.ReadUInt16();
                mapInfo.height = binaryReader.ReadUInt16();
                if (fullinfo)
                {
                    mapInfo.walkable = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    mapInfo.holy = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    mapInfo.dark = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    mapInfo.neutral = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    mapInfo.fire = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    mapInfo.wind = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    mapInfo.water = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    mapInfo.earth = new byte[(int)mapInfo.width, (int)mapInfo.height];
                    byte num2 = binaryReader.ReadByte();
                    byte num3 = binaryReader.ReadByte();
                    byte num4 = binaryReader.ReadByte();
                    byte num5 = binaryReader.ReadByte();
                    byte num6 = binaryReader.ReadByte();
                    byte num7 = binaryReader.ReadByte();
                    byte num8 = binaryReader.ReadByte();
                    memoryStream.Position += 2L;
                    for (int index1 = 0; index1 < (int)mapInfo.height; ++index1)
                    {
                        for (int index2 = 0; index2 < (int)mapInfo.width; ++index2)
                        {
                            uint key = binaryReader.ReadUInt32();
                            if (key != 0U)
                            {
                                if (!mapInfo.events.ContainsKey(key))
                                {
                                    mapInfo.events.Add(key, new byte[2]
                                    {
                    (byte) index2,
                    (byte) index1
                                    });
                                }
                                else
                                {
                                    byte[] numArray = new byte[mapInfo.events[key].Length + 2];
                                    mapInfo.events[key].CopyTo((Array)numArray, 0);
                                    numArray[numArray.Length - 2] = (byte)index2;
                                    numArray[numArray.Length - 1] = (byte)index1;
                                    mapInfo.events[key] = numArray;
                                }
                            }
                            mapInfo.holy[index2, index1] = (byte)(((int)binaryReader.ReadByte() + (int)num2) * 2);
                            mapInfo.dark[index2, index1] = (byte)(((int)binaryReader.ReadByte() + (int)num3) * 2);
                            mapInfo.neutral[index2, index1] = (byte)(((int)binaryReader.ReadByte() + (int)num4) * 2);
                            mapInfo.fire[index2, index1] = (byte)(((int)binaryReader.ReadByte() + (int)num5) * 2);
                            mapInfo.wind[index2, index1] = (byte)(((int)binaryReader.ReadByte() + (int)num6) * 2);
                            mapInfo.water[index2, index1] = (byte)(((int)binaryReader.ReadByte() + (int)num7) * 2);
                            mapInfo.earth[index2, index1] = (byte)(((int)binaryReader.ReadByte() + (int)num8) * 2);
                            memoryStream.Position += 2L;
                            mapInfo.walkable[index2, index1] = binaryReader.ReadByte();
                            memoryStream.Position += 3L;
                        }
                    }
                }
                this.mapInfos.Add(mapInfo.id, mapInfo);
                memoryStream.Close();
                nextEntry = zipInputStream.GetNextEntry();
                ++num1;
                if ((DateTime.Now - now).TotalMilliseconds > 40.0)
                {
                    now = DateTime.Now;
                    Logger.ProgressBarShow((uint)num1, (uint)count1, label);
                }
            }
            zipInputStream.Close();
            zipFile.Close();
            Logger.ProgressBarHide(num1.ToString() + " maps loaded.");
        }
    }
}
