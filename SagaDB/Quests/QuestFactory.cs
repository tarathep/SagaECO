namespace SagaDB.Quests
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="QuestFactory" />.
    /// </summary>
    public class QuestFactory : Factory<QuestFactory, QuestInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestFactory"/> class.
        /// </summary>
        public QuestFactory()
        {
            this.loadingTab = "Loading Quest database";
            this.loadedTab = " quests loaded.";
            this.databaseName = "quest";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="QuestInfo"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(QuestInfo item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="QuestInfo"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(QuestInfo item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="QuestInfo"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, QuestInfo item)
        {
            switch (root.Name.ToLower())
            {
                case "questdb":
                    item.GroupID = uint.Parse(root.Attributes[0].InnerText);
                    break;
                case "quest":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            return;
                        case "name":
                            item.Name = current.InnerText;
                            return;
                        case "type":
                            item.QuestType = (QuestType)Enum.Parse(typeof(QuestType), current.InnerText);
                            return;
                        case "time":
                            item.TimeLimit = int.Parse(current.InnerText);
                            return;
                        case "rewarditem":
                            item.RewardItem = uint.Parse(current.InnerText);
                            return;
                        case "rewardcount":
                            item.RewardCount = byte.Parse(current.InnerText);
                            return;
                        case "requiredquestpoints":
                            item.RequiredQuestPoint = byte.Parse(current.InnerText);
                            return;
                        case "dungeonid":
                            item.DungeonID = uint.Parse(current.InnerText);
                            return;
                        case "minlv":
                            item.MinLevel = byte.Parse(current.InnerText);
                            return;
                        case "maxlv":
                            item.MaxLevel = byte.Parse(current.InnerText);
                            return;
                        case "jobtype":
                            item.JobType = (JobType)Enum.Parse(typeof(JobType), current.InnerText);
                            return;
                        case "job":
                            item.Job = (PC_JOB)Enum.Parse(typeof(PC_JOB), current.InnerText);
                            return;
                        case "race":
                            item.Race = (PC_RACE)Enum.Parse(typeof(PC_RACE), current.InnerText);
                            return;
                        case "gender":
                            item.Gender = (PC_GENDER)Enum.Parse(typeof(PC_GENDER), current.InnerText);
                            return;
                        case "exp":
                            item.EXP = uint.Parse(current.InnerText);
                            return;
                        case "jexp":
                            item.JEXP = uint.Parse(current.InnerText);
                            return;
                        case "gold":
                            item.Gold = uint.Parse(current.InnerText);
                            return;
                        case "cp":
                            item.CP = uint.Parse(current.InnerText);
                            return;
                        case "fame":
                            item.Fame = uint.Parse(current.InnerText);
                            return;
                        case "mapid1":
                            item.MapID1 = uint.Parse(current.InnerText);
                            return;
                        case "mapid2":
                            item.MapID2 = uint.Parse(current.InnerText);
                            return;
                        case "mapid3":
                            item.MapID3 = uint.Parse(current.InnerText);
                            return;
                        case "objectid1":
                            item.ObjectID1 = uint.Parse(current.InnerText);
                            return;
                        case "count1":
                            item.Count1 = int.Parse(current.InnerText);
                            return;
                        case "objectid2":
                            item.ObjectID2 = uint.Parse(current.InnerText);
                            return;
                        case "count2":
                            item.Count2 = int.Parse(current.InnerText);
                            return;
                        case "objectid3":
                            item.ObjectID3 = uint.Parse(current.InnerText);
                            return;
                        case "count3":
                            item.Count3 = int.Parse(current.InnerText);
                            return;
                        case "party":
                            item.Party = bool.Parse(current.InnerText);
                            return;
                        case "npcsource":
                            item.NPCSource = uint.Parse(current.InnerText);
                            return;
                        case "npcdestination":
                            item.NPCDestination = uint.Parse(current.InnerText);
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
