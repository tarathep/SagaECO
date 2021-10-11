namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_MEMBER_DETAIL" />.
    /// </summary>
    public class SSMG_PARTY_MEMBER_DETAIL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_MEMBER_DETAIL"/> class.
        /// </summary>
        public SSMG_PARTY_MEMBER_DETAIL()
        {
            this.data = new byte[22];
            this.offset = (ushort)2;
            this.ID = (ushort)6645;
        }

        /// <summary>
        /// Sets the PartyIndex.
        /// </summary>
        public uint PartyIndex
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the CharID.
        /// </summary>
        public uint CharID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Job.
        /// </summary>
        public PC_JOB Job
        {
            set
            {
                this.PutUInt((uint)value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Level.
        /// </summary>
        public byte Level
        {
            set
            {
                this.PutUInt((uint)value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the JobLevel.
        /// </summary>
        public byte JobLevel
        {
            set
            {
                this.PutUInt((uint)value, (ushort)18);
            }
        }
    }
}
