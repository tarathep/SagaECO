namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_FGARDEN_FURNITURE_SETUP" />.
    /// </summary>
    public class CSMG_FGARDEN_FURNITURE_SETUP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_FGARDEN_FURNITURE_SETUP"/> class.
        /// </summary>
        public CSMG_FGARDEN_FURNITURE_SETUP()
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
        /// Gets the X.
        /// </summary>
        public short X
        {
            get
            {
                return this.GetShort((ushort)6);
            }
        }

        /// <summary>
        /// Gets the Y.
        /// </summary>
        public short Y
        {
            get
            {
                return this.GetShort((ushort)8);
            }
        }

        /// <summary>
        /// Gets the Z.
        /// </summary>
        public short Z
        {
            get
            {
                return this.GetShort((ushort)10);
            }
        }

        /// <summary>
        /// Gets the Dir.
        /// </summary>
        public ushort Dir
        {
            get
            {
                return this.GetUShort((ushort)12);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_FGARDEN_FURNITURE_SETUP();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnFGardenFurnitureSetup(this);
        }
    }
}
