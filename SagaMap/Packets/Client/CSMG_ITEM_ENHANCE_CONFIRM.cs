namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_ITEM_ENHANCE_CONFIRM" />.
    /// </summary>
    public class CSMG_ITEM_ENHANCE_CONFIRM : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_ITEM_ENHANCE_CONFIRM"/> class.
        /// </summary>
        public CSMG_ITEM_ENHANCE_CONFIRM()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the InventorySlot.
        /// </summary>
        public uint InventorySlot
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the ItemID.
        /// </summary>
        public uint ItemID
        {
            get
            {
                return this.GetUInt((ushort)6);
            }
        }

        /// <summary>
        /// Gets the Amount.
        /// </summary>
        public ushort Amount
        {
            get
            {
                return this.GetUShort((ushort)10);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_ITEM_ENHANCE_CONFIRM();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnItemEnhanceConfirm(this);
        }
    }
}
