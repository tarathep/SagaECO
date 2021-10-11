namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_DEM_PARTS_UNEQUIP" />.
    /// </summary>
    public class CSMG_DEM_PARTS_UNEQUIP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_DEM_PARTS_UNEQUIP"/> class.
        /// </summary>
        public CSMG_DEM_PARTS_UNEQUIP()
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
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_DEM_PARTS_UNEQUIP();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnDEMPartsUnequip(this);
        }
    }
}
