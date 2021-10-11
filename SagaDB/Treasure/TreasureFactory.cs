namespace SagaDB.Treasure
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="TreasureFactory" />.
    /// </summary>
    public class TreasureFactory : FactoryString<TreasureFactory, TreasureList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreasureFactory"/> class.
        /// </summary>
        public TreasureFactory()
        {
            this.loadingTab = "Loading Treasure database";
            this.loadedTab = " treasure groups loaded.";
            this.databaseName = "treasure";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="TreasureList"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        protected override string GetKey(TreasureList item)
        {
            return item.Name;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="TreasureList"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(TreasureList item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="TreasureList"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, TreasureList item)
        {
            switch (current.Name.ToLower())
            {
                case "treasurelist":
                    item.Name = current.Attributes[0].InnerText;
                    break;
                case nameof(item):
                    TreasureItem treasureItem = new TreasureItem();
                    treasureItem.ID = uint.Parse(current.InnerText);
                    treasureItem.Rate = int.Parse(current.GetAttribute("rate"));
                    treasureItem.Count = int.Parse(current.GetAttribute("count"));
                    item.Items.Add(treasureItem);
                    item.TotalRate += treasureItem.Rate;
                    break;
            }
        }

        /// <summary>
        /// The GetRandomItem.
        /// </summary>
        /// <param name="groupName">The groupName<see cref="string"/>.</param>
        /// <returns>The <see cref="TreasureItem"/>.</returns>
        public TreasureItem GetRandomItem(string groupName)
        {
            if (this.items.ContainsKey(groupName))
            {
                TreasureList treasureList = this.items[groupName];
                int num1 = Global.Random.Next(0, treasureList.TotalRate);
                int num2 = 0;
                foreach (TreasureItem treasureItem in treasureList.Items)
                {
                    num2 += treasureItem.Rate;
                    if (num1 <= num2)
                        return treasureItem;
                }
                return (TreasureItem)null;
            }
            Logger.ShowDebug("Cannot find TreasureGroup:" + groupName, Logger.defaultlogger);
            return (TreasureItem)null;
        }
    }
}
