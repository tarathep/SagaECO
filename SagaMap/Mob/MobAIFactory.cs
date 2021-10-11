namespace SagaMap.Mob
{
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="MobAIFactory" />.
    /// </summary>
    public class MobAIFactory : Factory<MobAIFactory, AIMode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobAIFactory"/> class.
        /// </summary>
        public MobAIFactory()
        {
            this.loadingTab = "Loading MobAI database";
            this.loadedTab = " AIs loaded.";
            this.databaseName = "MobAI";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="AIMode"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(AIMode item)
        {
            return item.MobID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="AIMode"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(AIMode item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="AIMode"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, AIMode item)
        {
            switch (root.Name.ToLower())
            {
                case "mob":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.MobID = uint.Parse(current.InnerText);
                            return;
                        case "aimode":
                            item.AI = int.Parse(current.InnerText);
                            return;
                        case "eventattackingonskillcast":
                            if (current.Attributes.Count > 0)
                            {
                                item.EventAttackingSkillRate = int.Parse(current.Attributes["Rate"].InnerText);
                                return;
                            }
                            item.EventAttackingSkillRate = 50;
                            return;
                        case "eventmastercombatonskillcast":
                            if (current.Attributes.Count > 0)
                            {
                                item.EventMasterCombatSkillRate = int.Parse(current.Attributes["Rate"].InnerText);
                                return;
                            }
                            item.EventMasterCombatSkillRate = 50;
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "eventattackingonskillcast":
                    switch (current.Name.ToLower())
                    {
                        case "skill":
                            uint key1 = uint.Parse(current.InnerText);
                            int num1 = int.Parse(current.Attributes["Rate"].InnerText);
                            item.EventAttacking.Add(key1, num1);
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
                case "eventmastercombatonskillcast":
                    switch (current.Name.ToLower())
                    {
                        case "skill":
                            uint key2 = uint.Parse(current.InnerText);
                            int num2 = int.Parse(current.Attributes["Rate"].InnerText);
                            item.EventMasterCombat.Add(key2, num2);
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
