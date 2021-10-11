namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// [00][0E][02][3A]
    /// [2D] //Lv
    /// [2D] //JobLv(1次職)
    /// [01] //JobLv(エキスパート)
    /// [01] //JobLv(テクニカル)
    /// [00][2E] //ボーナスポイント
    /// [00][08] //スキルポイント(1次職)
    /// [00][00] //スキルポイント(エキスパート)
    /// [00][00] //スキルポイント(テクニカル).
    /// </summary>
    public class SSMG_PLAYER_LEVEL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_LEVEL"/> class.
        /// </summary>
        public SSMG_PLAYER_LEVEL()
        {
            this.data = new byte[15];
            this.offset = (ushort)2;
            this.ID = (ushort)570;
        }

        /// <summary>
        /// Sets the Level.
        /// </summary>
        public byte Level
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the JobLevel.
        /// </summary>
        public byte JobLevel
        {
            set
            {
                this.PutByte(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the JobLevel2X.
        /// </summary>
        public byte JobLevel2X
        {
            set
            {
                this.PutByte(value, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the JobLevel2T.
        /// </summary>
        public byte JobLevel2T
        {
            set
            {
                this.PutByte(value, (ushort)5);
            }
        }

        /// <summary>
        /// Sets the JobLevelJoint.
        /// </summary>
        public byte JobLevelJoint
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the BonusPoint.
        /// </summary>
        public ushort BonusPoint
        {
            set
            {
                this.PutUShort(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the SkillPoint.
        /// </summary>
        public ushort SkillPoint
        {
            set
            {
                this.PutUShort(value, (ushort)9);
            }
        }

        /// <summary>
        /// Sets the Skill2XPoint.
        /// </summary>
        public ushort Skill2XPoint
        {
            set
            {
                this.PutUShort(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the Skill2TPoint.
        /// </summary>
        public ushort Skill2TPoint
        {
            set
            {
                this.PutUShort(value, (ushort)13);
            }
        }
    }
}
