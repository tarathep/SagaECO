namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_COUNT_UPDATE" />.
    /// </summary>
    public class SSMG_ITEM_COUNT_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_COUNT_UPDATE"/> class.
        /// </summary>
        public SSMG_ITEM_COUNT_UPDATE()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)2511;
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

        /// <summary>
        /// Sets the Stack.
        /// </summary>
        public ushort Stack
        {
            set
            {
                this.PutUShort(value, (ushort)6);
            }
        }
    }
}
