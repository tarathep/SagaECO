namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_ADD_SLOT_MATERIAL" />.
    /// </summary>
    public class SSMG_IRIS_ADD_SLOT_MATERIAL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_ADD_SLOT_MATERIAL"/> class.
        /// </summary>
        public SSMG_IRIS_ADD_SLOT_MATERIAL()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)5092;
        }

        /// <summary>
        /// Sets the Slot.
        /// </summary>
        public byte Slot
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Material.
        /// </summary>
        public uint Material
        {
            set
            {
                this.PutUInt(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Gold.
        /// </summary>
        public int Gold
        {
            set
            {
                this.PutInt(value, (ushort)7);
            }
        }
    }
}
