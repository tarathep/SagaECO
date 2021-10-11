namespace SagaMap.Dungeon
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="DungeonMapsFactory" />.
    /// </summary>
    public class DungeonMapsFactory : Factory<DungeonMapsFactory, DungeonMap>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonMapsFactory"/> class.
        /// </summary>
        public DungeonMapsFactory()
        {
            this.loadingTab = "Loading Dungeon Map database";
            this.loadedTab = " dungeon maps loaded.";
            this.databaseName = "dungeon map";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="DungeonMap"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(DungeonMap item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="DungeonMap"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(DungeonMap item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="DungeonMap"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, DungeonMap item)
        {
            switch (root.Name.ToLower())
            {
                case "map":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            return;
                        case "type":
                            item.MapType = (MapType)Enum.Parse(typeof(MapType), current.InnerText);
                            return;
                        case "theme":
                            item.Theme = (Theme)Enum.Parse(typeof(Theme), current.InnerText);
                            return;
                        case "gate":
                            GateType key = (GateType)Enum.Parse(typeof(GateType), current.GetAttribute("type"));
                            byte num1 = byte.Parse(current.GetAttribute("x"));
                            byte num2 = byte.Parse(current.GetAttribute("y"));
                            uint num3 = uint.Parse(current.InnerText);
                            if (item.Gates.ContainsKey(key))
                                return;
                            item.Gates.Add(key, new DungeonGate()
                            {
                                GateType = key,
                                X = num1,
                                Y = num2,
                                NPCID = num3
                            });
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
