namespace SagaDB.Npc
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="ShopFactory" />.
    /// </summary>
    public class ShopFactory : Factory<ShopFactory, Shop>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopFactory"/> class.
        /// </summary>
        public ShopFactory()
        {
            this.loadingTab = "Loading Shop database";
            this.loadedTab = " shops loaded.";
            this.databaseName = "shop";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="Shop"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, Shop item)
        {
            switch (root.Name.ToLower())
            {
                case "shop":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            return;
                        case "npc":
                            string innerText = current.InnerText;
                            char[] chArray = new char[1] { ',' };
                            foreach (string s in innerText.Split(chArray))
                                item.RelatedNPC.Add(uint.Parse(s));
                            return;
                        case "sellrate":
                            item.SellRate = uint.Parse(current.InnerText);
                            return;
                        case "buyrate":
                            item.BuyRate = uint.Parse(current.InnerText);
                            return;
                        case "buylimit":
                            item.BuyLimit = uint.Parse(current.InnerText);
                            return;
                        case "goods":
                            item.Goods.Add(uint.Parse(current.InnerText));
                            return;
                        case "shoptype":
                            item.ShopType = (ShopType)byte.Parse(current.InnerText);
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
            }
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="Shop"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(Shop item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="Shop"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(Shop item, string[] paras)
        {
            throw new NotImplementedException();
        }
    }
}
