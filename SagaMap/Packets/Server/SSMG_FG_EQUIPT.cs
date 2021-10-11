namespace SagaMap.Packets.Server
{
    using SagaDB.FGarden;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_EQUIPT" />.
    /// </summary>
    public class SSMG_FG_EQUIPT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_EQUIPT"/> class.
        /// </summary>
        public SSMG_FG_EQUIPT()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)7161;
        }

        /// <summary>
        /// Sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Place.
        /// </summary>
        public FGardenSlot Place
        {
            set
            {
                this.PutUInt((uint)value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Color.
        /// </summary>
        public byte Color
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }
    }
}
