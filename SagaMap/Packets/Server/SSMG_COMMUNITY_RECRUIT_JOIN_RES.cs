namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_RECRUIT_JOIN_RES" />.
    /// </summary>
    public class SSMG_COMMUNITY_RECRUIT_JOIN_RES : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_RECRUIT_JOIN_RES"/> class.
        /// </summary>
        public SSMG_COMMUNITY_RECRUIT_JOIN_RES()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)7081;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public JoinRes Result
        {
            set
            {
                this.PutInt((int)value, (ushort)2);
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
    }
}
