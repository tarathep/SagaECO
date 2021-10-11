namespace SagaDB.Iris
{
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="IrisAbilityFactory" />.
    /// </summary>
    public class IrisAbilityFactory : Factory<IrisAbilityFactory, AbilityVector>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IrisAbilityFactory"/> class.
        /// </summary>
        public IrisAbilityFactory()
        {
            this.loadingTab = "Loading Ability database";
            this.loadedTab = " abilities loaded.";
            this.databaseName = "Iris Ability";
            this.FactoryType = FactoryType.CSV;
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="AbilityVector"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, AbilityVector item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="AbilityVector"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(AbilityVector item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="AbilityVector"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(AbilityVector item, string[] paras)
        {
            item.ID = uint.Parse(paras[0]);
            item.Name = paras[1];
            for (int index1 = 0; index1 < 10; ++index1)
            {
                if (!(paras[2 + index1 * 2] == "0"))
                {
                    string[] strArray1 = paras[2 + index1 * 2].Split('|');
                    string[] strArray2 = paras[2 + index1 * 2 + 1].Split('|');
                    Dictionary<ReleaseAbility, int> dictionary = new Dictionary<ReleaseAbility, int>();
                    item.Abilities.Add((byte)(index1 + 1), dictionary);
                    for (int index2 = 0; index2 < strArray1.Length; ++index2)
                    {
                        ReleaseAbility key = (ReleaseAbility)(int.Parse(strArray1[index2]) - 1);
                        dictionary.Add(key, int.Parse(strArray2[index2]));
                    }
                }
            }
        }
    }
}
