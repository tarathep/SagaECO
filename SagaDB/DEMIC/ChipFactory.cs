namespace SagaDB.DEMIC
{
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="ChipFactory" />.
    /// </summary>
    public class ChipFactory : Factory<ChipFactory, Chip.BaseData>
    {
        /// <summary>
        /// Defines the byChipID.
        /// </summary>
        private Dictionary<short, Chip.BaseData> byChipID = new Dictionary<short, Chip.BaseData>();

        /// <summary>
        /// Gets the ByChipID.
        /// </summary>
        public Dictionary<short, Chip.BaseData> ByChipID
        {
            get
            {
                return this.byChipID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChipFactory"/> class.
        /// </summary>
        public ChipFactory()
        {
            this.loadingTab = "Loading DEMIC Chip database";
            this.loadedTab = " chips loaded.";
            this.databaseName = "DEMIC Chip";
            this.FactoryType = FactoryType.CSV;
        }

        /// <summary>
        /// The GetChip.
        /// </summary>
        /// <param name="itemID">The itemID<see cref="uint"/>.</param>
        /// <returns>The <see cref="Chip"/>.</returns>
        public Chip GetChip(uint itemID)
        {
            if (this.items.ContainsKey(itemID))
                return new Chip(this.items[itemID]);
            Logger.ShowWarning("Cannot find chip:" + itemID.ToString());
            return (Chip)null;
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="Chip.BaseData"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, Chip.BaseData item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="Chip.BaseData"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(Chip.BaseData item)
        {
            return item.itemID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="Chip.BaseData"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(Chip.BaseData item, string[] paras)
        {
            item.chipID = short.Parse(paras[0]);
            item.itemID = uint.Parse(paras[1]);
            this.byChipID.Add(item.chipID, item);
            item.name = paras[2];
            item.type = byte.Parse(paras[3]);
            item.model = Singleton<ModelFactory>.Instance.Models[uint.Parse(paras[7])];
            item.possibleLv = byte.Parse(paras[8]);
            item.engageTaskChip = short.Parse(paras[9]);
            item.hp = short.Parse(paras[10]);
            item.mp = short.Parse(paras[11]);
            item.sp = short.Parse(paras[12]);
            item.str = short.Parse(paras[13]);
            item.mag = short.Parse(paras[14]);
            item.vit = short.Parse(paras[15]);
            item.dex = short.Parse(paras[16]);
            item.agi = short.Parse(paras[17]);
            item.intel = short.Parse(paras[18]);
            item.skill1 = uint.Parse(paras[19]);
            item.skill2 = uint.Parse(paras[20]);
            item.skill3 = uint.Parse(paras[21]);
        }
    }
}
