namespace SagaDB.DEMIC
{
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="ChipShopFactory" />.
    /// </summary>
    public class ChipShopFactory : Factory<ChipShopFactory, ChipShopCategory>
    {
        /// <summary>
        /// Defines the lastItem.
        /// </summary>
        private ShopChip lastItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChipShopFactory"/> class.
        /// </summary>
        public ChipShopFactory()
        {
            this.loadingTab = "Loading Chip shop database";
            this.loadedTab = " shop caterories loaded.";
            this.databaseName = " Chip shop";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetCategoryFromLv.
        /// </summary>
        /// <param name="lv">The lv<see cref="byte"/>.</param>
        /// <returns>The <see cref="List{ChipShopCategory}"/>.</returns>
        public List<ChipShopCategory> GetCategoryFromLv(byte lv)
        {
            return this.items.Values.Where<ChipShopCategory>((Func<ChipShopCategory, bool>)(category => (int)category.PossibleLv <= (int)lv)).ToList<ChipShopCategory>();
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="ChipShopCategory"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(ChipShopCategory item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="ChipShopCategory"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(ChipShopCategory item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="ChipShopCategory"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, ChipShopCategory item)
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
                        case "lv":
                            item.PossibleLv = byte.Parse(current.InnerText);
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
                            ShopChip shopChip = new ShopChip();
                            uint key = uint.Parse(current.InnerText);
                            if (!item.Items.ContainsKey(key))
                                item.Items.Add(key, shopChip);
                            else
                                Logger.ShowWarning(string.Format("Item:{0} already added for shop category:{1}! overwriting....", (object)key, (object)item.ID));
                            shopChip.ItemID = key;
                            this.lastItem = shopChip;
                            return;
                        case "exp":
                            this.lastItem.EXP = uint.Parse(current.InnerText);
                            return;
                        case "jexp":
                            this.lastItem.JEXP = uint.Parse(current.InnerText);
                            return;
                        case "comment":
                            this.lastItem.Description = current.InnerText;
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
