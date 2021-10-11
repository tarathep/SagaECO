namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_BBS_OPEN" />.
    /// </summary>
    public class SSMG_COMMUNITY_BBS_OPEN : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_BBS_OPEN"/> class.
        /// </summary>
        public SSMG_COMMUNITY_BBS_OPEN()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6900;
        }

        /// <summary>
        /// Sets the Gold.
        /// </summary>
        public uint Gold
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
