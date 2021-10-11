namespace SagaMap.Packets.Client
{
    using SagaDB.FGarden;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_FGARDEN_EQUIPT" />.
    /// </summary>
    public class CSMG_FGARDEN_EQUIPT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_FGARDEN_EQUIPT"/> class.
        /// </summary>
        public CSMG_FGARDEN_EQUIPT()
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
        /// Gets the Place.
        /// </summary>
        public FGardenSlot Place
        {
            get
            {
                return (FGardenSlot)this.GetUInt((ushort)6);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_FGARDEN_EQUIPT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnFGardenEquipt(this);
        }
    }
}
