namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_BUY_SET" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_BUY_SET : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_BUY_SET"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_BUY_SET()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)6174;
        }
    }
}
