namespace SagaMap.Packets.Server
{
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_WARE_HEADER" />.
    /// </summary>
    public class SSMG_ITEM_WARE_HEADER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_WARE_HEADER"/> class.
        /// </summary>
        public SSMG_ITEM_WARE_HEADER()
        {
            this.data = new byte[18];
            this.offset = (ushort)2;
            this.ID = (ushort)2550;
        }

        /// <summary>
        /// Sets the Place.
        /// </summary>
        public WarehousePlace Place
        {
            set
            {
                this.PutInt((int)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the CountCurrent.
        /// </summary>
        public int CountCurrent
        {
            set
            {
                this.PutInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the CountAll.
        /// </summary>
        public int CountAll
        {
            set
            {
                this.PutInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the CountMax.
        /// </summary>
        public int CountMax
        {
            set
            {
                this.PutInt(value, (ushort)14);
            }
        }
    }
}
