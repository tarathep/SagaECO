namespace SagaMap.Packets.Client
{
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_ITEM_MOVE" />.
    /// </summary>
    public class CSMG_ITEM_MOVE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_ITEM_MOVE"/> class.
        /// </summary>
        public CSMG_ITEM_MOVE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets or sets the InventoryID.
        /// </summary>
        public uint InventoryID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Gets or sets the Target.
        /// </summary>
        public ContainerType Target
        {
            get
            {
                return (ContainerType)this.GetByte((ushort)6);
            }
            set
            {
                this.PutByte((byte)value, (ushort)6);
            }
        }

        /// <summary>
        /// Gets or sets the Count.
        /// </summary>
        public ushort Count
        {
            get
            {
                return this.GetUShort((ushort)7);
            }
            set
            {
                this.PutUShort(value, (ushort)7);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_ITEM_MOVE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnItemMove(this);
        }
    }
}
