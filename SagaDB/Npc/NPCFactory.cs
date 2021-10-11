namespace SagaDB.Npc
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="NPCFactory" />.
    /// </summary>
    public class NPCFactory : Factory<NPCFactory, NPC>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPCFactory"/> class.
        /// </summary>
        public NPCFactory()
        {
            this.loadingTab = "Loading NPC database";
            this.loadedTab = " npcs loaded.";
            this.databaseName = "npc";
            this.FactoryType = FactoryType.CSV;
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="NPC"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, NPC item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="NPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(NPC item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="NPC"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(NPC item, string[] paras)
        {
            item.ID = uint.Parse(paras[0]);
            item.Name = paras[1];
            item.MapID = uint.Parse(paras[2]);
            item.X = byte.Parse(paras[3]);
            item.Y = byte.Parse(paras[4]);
        }
    }
}
