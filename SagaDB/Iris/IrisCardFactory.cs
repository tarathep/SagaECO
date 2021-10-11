namespace SagaDB.Iris
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="IrisCardFactory" />.
    /// </summary>
    public class IrisCardFactory : Factory<IrisCardFactory, IrisCard>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IrisCardFactory"/> class.
        /// </summary>
        public IrisCardFactory()
        {
            this.loadingTab = "Loading Iris Card database";
            this.loadedTab = " cards loaded.";
            this.databaseName = "Iris Card";
            this.FactoryType = FactoryType.CSV;
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="IrisCard"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, IrisCard item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="IrisCard"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(IrisCard item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="IrisCard"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(IrisCard item, string[] paras)
        {
            item.ID = uint.Parse(paras[0]);
            item.Name = paras[3];
            item.Serial = paras[5];
            item.Rarity = (Rarity)int.Parse(paras[8]);
            item.NextCard = uint.Parse(paras[9]);
            item.Rank = int.Parse(paras[10]);
            if (this.toBool(paras[11]))
                item.CanWeapon = true;
            if (this.toBool(paras[12]))
                item.CanArmor = true;
            if (this.toBool(paras[13]))
                item.CanNeck = true;
            for (int index = 1; index < 7; ++index)
            {
                Elements key = (Elements)index;
                item.Elements.Add(key, int.Parse(paras[13 + index]));
            }
            for (int index = 0; index < 3; ++index)
            {
                uint key1 = uint.Parse(paras[20 + index * 2]);
                if (Factory<IrisAbilityFactory, AbilityVector>.Instance.Items.ContainsKey(key1))
                {
                    AbilityVector key2 = Factory<IrisAbilityFactory, AbilityVector>.Instance.Items[key1];
                    item.Abilities.Add(key2, int.Parse(paras[21 + index * 2]));
                }
            }
        }

        /// <summary>
        /// The toBool.
        /// </summary>
        /// <param name="input">The input<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool toBool(string input)
        {
            return input == "1";
        }
    }
}
