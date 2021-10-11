namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_WAREHOUSE_GET" />.
    /// </summary>
    public class SSMG_GOLEM_WAREHOUSE_GET : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_WAREHOUSE_GET"/> class.
        /// </summary>
        public SSMG_GOLEM_WAREHOUSE_GET()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6137;
        }
    }
}
