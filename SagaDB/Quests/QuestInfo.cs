namespace SagaDB.Quests
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="QuestInfo" />.
    /// </summary>
    public class QuestInfo
    {
        /// <summary>
        /// Defines the job.
        /// </summary>
        private PC_JOB job = PC_JOB.NONE;

        /// <summary>
        /// Defines the race.
        /// </summary>
        private PC_RACE race = PC_RACE.NONE;

        /// <summary>
        /// Defines the gender.
        /// </summary>
        private PC_GENDER gender = PC_GENDER.NONE;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the type.
        /// </summary>
        private QuestType type;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the time.
        /// </summary>
        private int time;

        /// <summary>
        /// Defines the minlv.
        /// </summary>
        private byte minlv;

        /// <summary>
        /// Defines the maxlv.
        /// </summary>
        private byte maxlv;

        /// <summary>
        /// Defines the rewarditem.
        /// </summary>
        private uint rewarditem;

        /// <summary>
        /// Defines the rewardcount.
        /// </summary>
        private byte rewardcount;

        /// <summary>
        /// Defines the requiredQuestPoint.
        /// </summary>
        private byte requiredQuestPoint;

        /// <summary>
        /// Defines the dungeonID.
        /// </summary>
        private uint dungeonID;

        /// <summary>
        /// Defines the exp.
        /// </summary>
        private uint exp;

        /// <summary>
        /// Defines the jexp.
        /// </summary>
        private uint jexp;

        /// <summary>
        /// Defines the gold.
        /// </summary>
        private uint gold;

        /// <summary>
        /// Defines the fame.
        /// </summary>
        private uint fame;

        /// <summary>
        /// Defines the cp.
        /// </summary>
        private uint cp;

        /// <summary>
        /// Defines the mapid1.
        /// </summary>
        private uint mapid1;

        /// <summary>
        /// Defines the mapid2.
        /// </summary>
        private uint mapid2;

        /// <summary>
        /// Defines the mapid3.
        /// </summary>
        private uint mapid3;

        /// <summary>
        /// Defines the obj1.
        /// </summary>
        private uint obj1;

        /// <summary>
        /// Defines the obj2.
        /// </summary>
        private uint obj2;

        /// <summary>
        /// Defines the obj3.
        /// </summary>
        private uint obj3;

        /// <summary>
        /// Defines the count1.
        /// </summary>
        private int count1;

        /// <summary>
        /// Defines the count2.
        /// </summary>
        private int count2;

        /// <summary>
        /// Defines the count3.
        /// </summary>
        private int count3;

        /// <summary>
        /// Defines the party.
        /// </summary>
        private bool party;

        /// <summary>
        /// Defines the npcsource.
        /// </summary>
        private uint npcsource;

        /// <summary>
        /// Defines the npcdest.
        /// </summary>
        private uint npcdest;

        /// <summary>
        /// Defines the groupID.
        /// </summary>
        private uint groupID;

        /// <summary>
        /// Defines the jobtype.
        /// </summary>
        private JobType jobtype;

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the GroupID.
        /// </summary>
        public uint GroupID
        {
            get
            {
                return this.groupID;
            }
            set
            {
                this.groupID = value;
            }
        }

        /// <summary>
        /// Gets or sets the QuestType.
        /// </summary>
        public QuestType QuestType
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the TimeLimit.
        /// </summary>
        public int TimeLimit
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }

        /// <summary>
        /// Gets or sets the RewardItem.
        /// </summary>
        public uint RewardItem
        {
            get
            {
                return this.rewarditem;
            }
            set
            {
                this.rewarditem = value;
            }
        }

        /// <summary>
        /// Gets or sets the RewardCount.
        /// </summary>
        public byte RewardCount
        {
            get
            {
                return this.rewardcount;
            }
            set
            {
                this.rewardcount = value;
            }
        }

        /// <summary>
        /// Gets or sets the RequiredQuestPoint.
        /// </summary>
        public byte RequiredQuestPoint
        {
            get
            {
                return this.requiredQuestPoint;
            }
            set
            {
                this.requiredQuestPoint = value;
            }
        }

        /// <summary>
        /// Gets or sets the DungeonID.
        /// </summary>
        public uint DungeonID
        {
            get
            {
                return this.dungeonID;
            }
            set
            {
                this.dungeonID = value;
            }
        }

        /// <summary>
        /// Gets or sets the MinLevel.
        /// </summary>
        public byte MinLevel
        {
            get
            {
                return this.minlv;
            }
            set
            {
                this.minlv = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxLevel.
        /// </summary>
        public byte MaxLevel
        {
            get
            {
                return this.maxlv;
            }
            set
            {
                this.maxlv = value;
            }
        }

        /// <summary>
        /// Gets or sets the EXP.
        /// </summary>
        public uint EXP
        {
            get
            {
                return this.exp;
            }
            set
            {
                this.exp = value;
            }
        }

        /// <summary>
        /// Gets or sets the JEXP.
        /// </summary>
        public uint JEXP
        {
            get
            {
                return this.jexp;
            }
            set
            {
                this.jexp = value;
            }
        }

        /// <summary>
        /// Gets or sets the Gold.
        /// </summary>
        public uint Gold
        {
            get
            {
                return this.gold;
            }
            set
            {
                this.gold = value;
            }
        }

        /// <summary>
        /// Gets or sets the CP.
        /// </summary>
        public uint CP
        {
            get
            {
                return this.cp;
            }
            set
            {
                this.cp = value;
            }
        }

        /// <summary>
        /// Gets or sets the Fame.
        /// </summary>
        public uint Fame
        {
            get
            {
                return this.fame;
            }
            set
            {
                this.fame = value;
            }
        }

        /// <summary>
        /// Gets or sets the MapID1.
        /// </summary>
        public uint MapID1
        {
            get
            {
                return this.mapid1;
            }
            set
            {
                this.mapid1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the MapID2.
        /// </summary>
        public uint MapID2
        {
            get
            {
                return this.mapid2;
            }
            set
            {
                this.mapid2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the MapID3.
        /// </summary>
        public uint MapID3
        {
            get
            {
                return this.mapid3;
            }
            set
            {
                this.mapid3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the ObjectID1.
        /// </summary>
        public uint ObjectID1
        {
            get
            {
                return this.obj1;
            }
            set
            {
                this.obj1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the ObjectID2.
        /// </summary>
        public uint ObjectID2
        {
            get
            {
                return this.obj2;
            }
            set
            {
                this.obj2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the ObjectID3.
        /// </summary>
        public uint ObjectID3
        {
            get
            {
                return this.obj3;
            }
            set
            {
                this.obj3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the Count1.
        /// </summary>
        public int Count1
        {
            get
            {
                return this.count1;
            }
            set
            {
                this.count1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the Count2.
        /// </summary>
        public int Count2
        {
            get
            {
                return this.count2;
            }
            set
            {
                this.count2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the Count3.
        /// </summary>
        public int Count3
        {
            get
            {
                return this.count3;
            }
            set
            {
                this.count3 = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Party.
        /// </summary>
        public bool Party
        {
            get
            {
                return this.party;
            }
            set
            {
                this.party = value;
            }
        }

        /// <summary>
        /// Gets or sets the NPCSource.
        /// </summary>
        public uint NPCSource
        {
            get
            {
                return this.npcsource;
            }
            set
            {
                this.npcsource = value;
            }
        }

        /// <summary>
        /// Gets or sets the NPCDestination.
        /// </summary>
        public uint NPCDestination
        {
            get
            {
                return this.npcdest;
            }
            set
            {
                this.npcdest = value;
            }
        }

        /// <summary>
        /// Gets or sets the JobType.
        /// </summary>
        public JobType JobType
        {
            get
            {
                return this.jobtype;
            }
            set
            {
                this.jobtype = value;
            }
        }

        /// <summary>
        /// Gets or sets the Job.
        /// </summary>
        public PC_JOB Job
        {
            get
            {
                return this.job;
            }
            set
            {
                this.job = value;
            }
        }

        /// <summary>
        /// Gets or sets the Race.
        /// </summary>
        public PC_RACE Race
        {
            get
            {
                return this.race;
            }
            set
            {
                this.race = value;
            }
        }

        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        public PC_GENDER Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
