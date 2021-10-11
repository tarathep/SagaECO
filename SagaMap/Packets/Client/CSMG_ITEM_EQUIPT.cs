namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_ITEM_EQUIPT" />.
    /// </summary>
    public class CSMG_ITEM_EQUIPT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_ITEM_EQUIPT"/> class.
        /// </summary>
        public CSMG_ITEM_EQUIPT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets or sets the InventoryID.
        /// </summary>
        public uint InventoryID
        {
            set
            {
                this.data = new byte[6];
                this.PutUInt(value, (ushort)2);
            }
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_ITEM_EQUIPT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnItemEquipt(this);
        }
    }
}
