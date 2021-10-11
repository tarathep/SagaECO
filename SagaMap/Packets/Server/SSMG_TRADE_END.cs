namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_END" />.
    /// </summary>
    public class SSMG_TRADE_END : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_END"/> class.
        /// </summary>
        public SSMG_TRADE_END()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)2588;
        }
    }
}
