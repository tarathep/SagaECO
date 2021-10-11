namespace SagaDB.Ring
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="RingFameTable" />.
    /// </summary>
    public class RingFameTable : Factory<RingFameTable, RingFame>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RingFameTable"/> class.
        /// </summary>
        public RingFameTable()
        {
            this.loadingTab = "Loading Ring Fame database";
            this.loadedTab = " entries loaded.";
            this.databaseName = " Ring fame";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="RingFame"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(RingFame item)
        {
            return item.Level;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="RingFame"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(RingFame item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="RingFame"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, RingFame item)
        {
            switch (root.Name.ToLower())
            {
                case "level":
                    switch (current.Name.ToLower())
                    {
                        case "level":
                            item.Level = uint.Parse(current.InnerText);
                            return;
                        case "fame":
                            item.Fame = uint.Parse(current.InnerText);
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
