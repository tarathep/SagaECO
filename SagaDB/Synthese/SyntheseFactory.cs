namespace SagaDB.Synthese
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="SyntheseFactory" />.
    /// </summary>
    public class SyntheseFactory : Factory<SyntheseFactory, SyntheseInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntheseFactory"/> class.
        /// </summary>
        public SyntheseFactory()
        {
            this.loadingTab = "Loading synthese database";
            this.loadedTab = " syntheses loaded.";
            this.databaseName = "synthese";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="SyntheseInfo"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(SyntheseInfo item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="SyntheseInfo"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(SyntheseInfo item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="SyntheseInfo"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, SyntheseInfo item)
        {
            switch (root.Name.ToLower())
            {
                case "synthese":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            return;
                        case "skillid":
                            item.SkillID = ushort.Parse(current.InnerText);
                            return;
                        case "skilllv":
                            item.SkillLv = byte.Parse(current.InnerText);
                            return;
                        case "gold":
                            item.Gold = uint.Parse(current.InnerText);
                            return;
                        case "requiredtool":
                            item.RequiredTool = uint.Parse(current.InnerText);
                            return;
                        case "material":
                            item.Materials.Add(new ItemElement()
                            {
                                ID = uint.Parse(current.GetAttribute("id")),
                                Count = ushort.Parse(current.GetAttribute("count"))
                            });
                            return;
                        case "product":
                            item.Products.Add(new ItemElement()
                            {
                                ID = uint.Parse(current.GetAttribute("id")),
                                Count = ushort.Parse(current.GetAttribute("count")),
                                Rate = int.Parse(current.GetAttribute("rate"))
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
