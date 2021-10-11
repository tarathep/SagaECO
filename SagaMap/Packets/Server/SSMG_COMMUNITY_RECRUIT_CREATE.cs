namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_RECRUIT_CREATE" />.
    /// </summary>
    public class SSMG_COMMUNITY_RECRUIT_CREATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_RECRUIT_CREATE"/> class.
        /// </summary>
        public SSMG_COMMUNITY_RECRUIT_CREATE()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7051;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }
    }
}
