namespace SagaDB.ODWar
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="ODWarFactory" />.
    /// </summary>
    public class ODWarFactory : Factory<ODWarFactory, SagaDB.ODWar.ODWar>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ODWarFactory"/> class.
        /// </summary>
        public ODWarFactory()
        {
            this.loadingTab = "Loading ODWar database";
            this.loadedTab = " OD War loaded.";
            this.databaseName = "OD War";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.ODWar.ODWar"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(SagaDB.ODWar.ODWar item)
        {
            return item.MapID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.ODWar.ODWar"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(SagaDB.ODWar.ODWar item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.ODWar.ODWar"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, SagaDB.ODWar.ODWar item)
        {
            switch (root.Name.ToLower())
            {
                case "odwar":
                    switch (current.Name.ToLower())
                    {
                        case "map":
                            item.MapID = uint.Parse(current.InnerText);
                            return;
                        case "symboltrash":
                            item.SymbolTrash = uint.Parse(current.InnerText);
                            return;
                        case "symbol":
                            SagaDB.ODWar.ODWar.Symbol symbol = new SagaDB.ODWar.ODWar.Symbol();
                            symbol.id = int.Parse(current.GetAttribute("id"));
                            symbol.x = byte.Parse(current.GetAttribute("x"));
                            symbol.y = byte.Parse(current.GetAttribute("y"));
                            symbol.mobID = uint.Parse(current.InnerText);
                            item.Symbols.Add(symbol.id, symbol);
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "schedules":
                    switch (current.Name.ToLower())
                    {
                        case "schedule":
                            int num = int.Parse(current.GetAttribute("time"));
                            int key = int.Parse(current.InnerText);
                            if (item.StartTime.ContainsKey(key))
                                return;
                            item.StartTime.Add(key, num);
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "demchamp":
                    switch (current.Name.ToLower())
                    {
                        case "mob":
                            item.DEMChamp.Add(uint.Parse(current.InnerText));
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "dem":
                    switch (current.Name.ToLower())
                    {
                        case "mob":
                            item.DEMNormal.Add(uint.Parse(current.InnerText));
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "boss":
                    switch (current.Name.ToLower())
                    {
                        case "mob":
                            item.Boss.Add(uint.Parse(current.InnerText));
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "wavestrong":
                    if (item.WaveStrong == null)
                        item.WaveStrong = new SagaDB.ODWar.ODWar.Wave();
                    switch (current.Name.ToLower())
                    {
                        case "demchamp":
                            item.WaveStrong.DEMChamp = int.Parse(current.InnerText);
                            return;
                        case "dem":
                            item.WaveStrong.DEMNormal = int.Parse(current.InnerText);
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "waveweak":
                    if (item.WaveWeak == null)
                        item.WaveWeak = new SagaDB.ODWar.ODWar.Wave();
                    switch (current.Name.ToLower())
                    {
                        case "demchamp":
                            item.WaveWeak.DEMChamp = int.Parse(current.InnerText);
                            return;
                        case "dem":
                            item.WaveWeak.DEMNormal = int.Parse(current.InnerText);
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
            }
        }
    }
}
