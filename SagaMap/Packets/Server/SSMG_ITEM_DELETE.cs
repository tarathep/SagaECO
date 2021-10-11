namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_DELETE" />.
    /// </summary>
    public class SSMG_ITEM_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_DELETE"/> class.
        /// </summary>
        public SSMG_ITEM_DELETE()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)2510;
        }

        /// <summary>
        /// Sets the InventorySlot.
        /// </summary>
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
