namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_FOOTER" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_FOOTER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_FOOTER"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_FOOTER()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)6146;
        }
    }
}
