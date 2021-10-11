namespace SagaMap.Packets.Server
{
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_INFO" />.
    /// </summary>
    public class SSMG_ITEM_INFO : HasItemDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_INFO"/> class.
        /// </summary>
        public SSMG_ITEM_INFO()
        {
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_Iris)
                this.data = new byte[171];
            else
                this.data = new byte[218];
            this.offset = (ushort)2;
            this.ID = (ushort)515;
        }

        /// <summary>
        /// Sets the Item.
        /// </summary>
        public SagaDB.Item.Item Item
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_Iris)
                    this.PutByte((byte)166, (ushort)3);
                else
                    this.PutByte((byte)214, (ushort)3);
                this.offset = (ushort)8;
                this.ItemDetail = value;
            }
        }

        /// <summary>
        /// Sets the Size.
        /// </summary>
        public byte Size
        {
            set
            {
                this.PutByte(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the InventorySlot.
        /// </summary>
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the Container.
        /// </summary>
        public ContainerType Container
        {
            set
            {
                if (value >= ContainerType.HEAD2)
                    this.PutByte((byte)(value - 200), (ushort)16);
                else
                    this.PutByte((byte)value, (ushort)16);
            }
        }
    }
}
