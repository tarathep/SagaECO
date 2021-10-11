namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_LEARN" />.
    /// </summary>
    public class SSMG_SKILL_LEARN : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_LEARN"/> class.
        /// </summary>
        public SSMG_SKILL_LEARN()
        {
            this.data = new byte[9];
            this.offset = (ushort)2;
            this.ID = (ushort)552;
        }

        /// <summary>
        /// Sets the SkillID.
        /// </summary>
        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the SkillPoints.
        /// </summary>
        public ushort SkillPoints
        {
            set
            {
                this.PutUShort(value, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the SkillPoints2.
        /// </summary>
        public ushort SkillPoints2
        {
            set
            {
                this.PutUShort(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_SKILL_LEARN.LearnResult Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)8);
            }
        }

        /// <summary>
        /// Defines the LearnResult.
        /// </summary>
        public enum LearnResult
        {
            /// <summary>
            /// Defines the SKILL_MAX_LEVEL_EXEED.
            /// </summary>
            SKILL_MAX_LEVEL_EXEED = -5,

            /// <summary>
            /// Defines the SKILL_NOT_LEARNED.
            /// </summary>
            SKILL_NOT_LEARNED = -4,

            /// <summary>
            /// Defines the NOT_ENOUGH_JOB_LEVEL.
            /// </summary>
            NOT_ENOUGH_JOB_LEVEL = -3,

            /// <summary>
            /// Defines the NOT_ENOUGH_SKILL_POINT.
            /// </summary>
            NOT_ENOUGH_SKILL_POINT = -2,

            /// <summary>
            /// Defines the SKILL_NOT_EXIST.
            /// </summary>
            SKILL_NOT_EXIST = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
