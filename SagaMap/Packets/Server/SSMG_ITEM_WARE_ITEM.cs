namespace SagaMap.Packets.Server
{
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_WARE_ITEM" />.
    /// </summary>
    public class SSMG_ITEM_WARE_ITEM : HasItemDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_WARE_ITEM"/> class.
        /// </summary>
        public SSMG_ITEM_WARE_ITEM()
        {
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_Iris)
                this.data = new byte[170];
            else
                this.data = new byte[217];
            this.offset = (ushort)2;
            this.ID = (ushort)2553;
        }

        /// <summary>
        /// Sets the Item.
        /// </summary>
        public SagaDB.Item.Item Item
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_Iris)
                    this.PutByte((byte)166, (ushort)2);
                else
                    this.PutByte((byte)214, (ushort)2);
                this.offset = (ushort)7;
                this.ItemDetail = value;
            }
        }

        /// <summary>
        /// Sets the InventorySlot.
        /// </summary>
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Place.
        /// </summary>
        public WarehousePlace Place
        {
            set
            {
                this.PutByte((byte)value, (ushort)15);
            }
        }
    }
}
