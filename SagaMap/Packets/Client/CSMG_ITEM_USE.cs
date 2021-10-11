namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_ITEM_USE" />.
    /// </summary>
    public class CSMG_ITEM_USE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_ITEM_USE"/> class.
        /// </summary>
        public CSMG_ITEM_USE()
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
        /// Gets the ActorID.
        /// </summary>
        public uint ActorID
        {
            get
            {
                return this.GetUInt((ushort)6);
            }
        }

        /// <summary>
        /// Gets the X.
        /// </summary>
        public byte X
        {
            get
            {
                return this.GetByte((ushort)10);
            }
        }

        /// <summary>
        /// Gets the Y.
        /// </summary>
        public byte Y
        {
            get
            {
                return this.GetByte((ushort)11);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_ITEM_USE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnItemUse(this);
        }
    }
}
