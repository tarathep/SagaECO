namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_IDENTIFY" />.
    /// </summary>
    public class SSMG_ITEM_IDENTIFY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_IDENTIFY"/> class.
        /// </summary>
        public SSMG_ITEM_IDENTIFY()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)2513;
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
        /// Sets a value indicating whether Identify.
        /// </summary>
        public bool Identify
        {
            set
            {
                if (!value)
                    return;
                this.PutInt(this.GetInt((ushort)6) | 1, (ushort)6);
            }
        }

        /// <summary>
        /// Sets a value indicating whether Lock.
        /// </summary>
        public bool Lock
        {
            set
            {
                if (!value)
                    return;
                this.PutInt(this.GetInt((ushort)6) | 32, (ushort)6);
            }
        }
    }
}
