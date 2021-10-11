namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_ITEM_ENHANCE_SELECT" />.
    /// </summary>
    public class CSMG_ITEM_ENHANCE_SELECT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_ITEM_ENHANCE_SELECT"/> class.
        /// </summary>
        public CSMG_ITEM_ENHANCE_SELECT()
        {
            this.offset = (ushort)2;
            this.data = new byte[6];
        }

        /// <summary>
        /// Gets or sets the InventorySlot.
        /// </summary>
        public uint InventorySlot
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
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_ITEM_ENHANCE_SELECT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnItemEnhanceSelect(this);
        }
    }
}
