namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_OPEN_OK" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_OPEN_OK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_OPEN_OK"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_OPEN_OK()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6142;
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
