namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_STAMP_USE" />.
    /// </summary>
    public class SSMG_STAMP_USE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_STAMP_USE"/> class.
        /// </summary>
        public SSMG_STAMP_USE()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)7105;
        }

        /// <summary>
        /// Sets the Genre.
        /// </summary>
        public StampGenre Genre
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Slot.
        /// </summary>
        public byte Slot
        {
            set
            {
                this.PutByte(value, (ushort)3);
            }
        }
    }
}
