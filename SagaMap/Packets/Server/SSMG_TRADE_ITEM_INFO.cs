namespace SagaMap.Packets.Server
{
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_ITEM_INFO" />.
    /// </summary>
    public class SSMG_TRADE_ITEM_INFO : HasItemDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_ITEM_INFO"/> class.
        /// </summary>
        public SSMG_TRADE_ITEM_INFO()
        {
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_Iris)
                this.data = new byte[170];
            else
                this.data = new byte[217];
            this.offset = (ushort)2;
            this.ID = (ushort)2590;
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
        /// Sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the Container.
        /// </summary>
        public ContainerType Container
        {
            set
            {
                this.PutByte((byte)value, (ushort)15);
            }
        }
    }
}
