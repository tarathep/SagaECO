namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_DEM_DEMIC_INITIALIZE" />.
    /// </summary>
    public class CSMG_DEM_DEMIC_INITIALIZE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_DEM_DEMIC_INITIALIZE"/> class.
        /// </summary>
        public CSMG_DEM_DEMIC_INITIALIZE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Page.
        /// </summary>
        public byte Page
        {
            get
            {
                return this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_DEM_DEMIC_INITIALIZE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnDEMDemicInitialize(this);
        }
    }
}
