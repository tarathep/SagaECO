namespace SagaMap.Packets.Server
{
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_WAREHOUSE_ITEM" />.
    /// </summary>
    public class SSMG_GOLEM_WAREHOUSE_ITEM : HasItemDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_WAREHOUSE_ITEM"/> class.
        /// </summary>
        public SSMG_GOLEM_WAREHOUSE_ITEM()
        {
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_Iris)
                this.data = new byte[170];
            else
                this.data = new byte[217];
            this.offset = (ushort)2;
            this.ID = (ushort)6134;
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
