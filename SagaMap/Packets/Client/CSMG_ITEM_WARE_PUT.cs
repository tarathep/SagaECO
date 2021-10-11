namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_ITEM_WARE_PUT" />.
    /// </summary>
    public class CSMG_ITEM_WARE_PUT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_ITEM_WARE_PUT"/> class.
        /// </summary>
        public CSMG_ITEM_WARE_PUT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the InventoryID.
        /// </summary>
        public uint InventoryID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Count.
        /// </summary>
        public ushort Count
        {
            get
            {
                return this.GetUShort((ushort)6);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_ITEM_WARE_PUT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnItemWarePut(this);
        }
    }
}
