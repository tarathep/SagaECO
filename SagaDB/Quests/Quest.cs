namespace SagaDB.Quests
{
    using SagaDB.Actor;
    using SagaDB.Npc;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="Quest" />.
    /// </summary>
    public class Quest
    {
        /// <summary>
        /// Defines the info.
        /// </summary>
        private QuestInfo info;

        /// <summary>
        /// Defines the status.
        /// </summary>
        private QuestStatus status;

        /// <summary>
        /// Defines the endTime.
        /// </summary>
        private DateTime endTime;

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
        /// Defines the npc.
        /// </summary>
        private NPC npc;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quest"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        public Quest(uint id)
        {
            this.info = Factory<QuestFactory, QuestInfo>.Instance.Items[id];
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.info.ID;
            }
            set
            {
                this.info.ID = value;
            }
        }

        /// <summary>
        /// Gets or sets the QuestType.
        /// </summary>
        public QuestType QuestType
        {
            get
            {
                return this.info.QuestType;
            }
            set
            {
                this.info.QuestType = value;
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.info.Name;
            }
            set
            {
                this.info.Name = value;
            }
        }

        /// <summary>
        /// Gets the Detail.
        /// </summary>
        public QuestInfo Detail
        {
            get
            {
                return this.info;
            }
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public QuestStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        /// <summary>
        /// Gets or sets the EndTime.
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the CurrentCount1.
        /// </summary>
        public int CurrentCount1
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
        /// Gets or sets the CurrentCount2.
        /// </summary>
        public int CurrentCount2
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
        /// Gets or sets the CurrentCount3.
        /// </summary>
        public int CurrentCount3
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
        /// Gets or sets the NPC.
        /// </summary>
        public NPC NPC
        {
            get
            {
                return this.npc;
            }
            set
            {
                this.npc = value;
            }
        }

        /// <summary>
        /// The Difficulty.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>难度.</returns>
        public QuestDifficulty Difficulty(ActorPC pc)
        {
            int num = (int)this.Detail.MinLevel - (int)pc.Level;
            if (Math.Abs(num) <= 3)
                return QuestDifficulty.BEST_FIT;
            if (Math.Abs(num) > 3 && Math.Abs(num) <= 9)
                return QuestDifficulty.NORMAL;
            if (num > 9)
                return QuestDifficulty.TOO_HARD;
            return num < -9 ? QuestDifficulty.TOO_EASY : QuestDifficulty.NORMAL;
        }
    }
}
