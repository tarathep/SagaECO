namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_RECRUIT_DELETE" />.
    /// </summary>
    public class SSMG_COMMUNITY_RECRUIT_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_RECRUIT_DELETE"/> class.
        /// </summary>
        public SSMG_COMMUNITY_RECRUIT_DELETE()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7061;
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
