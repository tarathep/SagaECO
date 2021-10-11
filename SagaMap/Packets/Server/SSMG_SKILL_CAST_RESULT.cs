namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_CAST_RESULT" />.
    /// </summary>
    public class SSMG_SKILL_CAST_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_CAST_RESULT"/> class.
        /// </summary>
        public SSMG_SKILL_CAST_RESULT()
        {
            this.data = new byte[21];
            this.offset = (ushort)2;
            this.ID = (ushort)5001;
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
        /// Sets the Result.
        /// </summary>
        public byte Result
        {
            set
            {
                this.PutByte(value, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)5);
            }
        }

        /// <summary>
        /// Sets the CastTime.
        /// </summary>
        public uint CastTime
        {
            set
            {
                this.PutUInt(value, (ushort)9);
            }
        }

        /// <summary>
        /// Sets the TargetID.
        /// </summary>
        public uint TargetID
        {
            set
            {
                this.PutUInt(value, (ushort)13);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)17);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)18);
            }
        }

        /// <summary>
        /// Sets the SkillLv.
        /// </summary>
        public byte SkillLv
        {
            set
            {
                this.PutByte(value, (ushort)19);
            }
        }
    }
}
