namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_GOLD" />.
    /// </summary>
    public class SSMG_TRADE_GOLD : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_GOLD"/> class.
        /// </summary>
        public SSMG_TRADE_GOLD()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)2591;
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
