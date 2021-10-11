namespace SagaDB.ECOShop
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="ECOShopFactory" />.
    /// </summary>
    public class ECOShopFactory : Factory<ECOShopFactory, ShopCategory>
    {
        /// <summary>
        /// Defines the lastItem.
        /// </summary>
        private ShopItem lastItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECOShopFactory"/> class.
        /// </summary>
        public ECOShopFactory()
        {
            this.loadingTab = "Loading ECO shop database";
            this.loadedTab = " shop caterories loaded.";
            this.databaseName = " ECO shop";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="ShopCategory"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(ShopCategory item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="ShopCategory"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(ShopCategory item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="ShopCategory"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, ShopCategory item)
        {
            switch (root.Name.ToLower())
            {
                case "category":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            return;
                        case "name":
                            item.Name = current.InnerText;
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case nameof(item):
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            ShopItem shopItem = new ShopItem();
                            uint key = uint.Parse(current.InnerText);
                            if (!item.Items.ContainsKey(key))
                                item.Items.Add(key, shopItem);
                            else
                                Logger.ShowWarning(string.Format("Item:{0} already added for shop category:{1}! overwriting....", (object)key, (object)item.ID));
                            this.lastItem = shopItem;
                            return;
                        case "points":
                            this.lastItem.points = uint.Parse(current.InnerText);
                            return;
                        case "comment":
                            this.lastItem.comment = current.InnerText;
                            return;
                        case "rentalminutes":
                            this.lastItem.rental = int.Parse(current.InnerText);
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
