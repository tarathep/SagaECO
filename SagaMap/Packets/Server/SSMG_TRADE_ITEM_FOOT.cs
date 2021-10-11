namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_ITEM_FOOT" />.
    /// </summary>
    public class SSMG_TRADE_ITEM_FOOT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_ITEM_FOOT"/> class.
        /// </summary>
        public SSMG_TRADE_ITEM_FOOT()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)2593;
        }
    }
}
