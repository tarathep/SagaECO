namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_REQUEST_MAP_SERVER" />.
    /// </summary>
    public class CSMG_REQUEST_MAP_SERVER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_REQUEST_MAP_SERVER"/> class.
        /// </summary>
        public CSMG_REQUEST_MAP_SERVER()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Slot.
        /// </summary>
        public uint Slot
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
            return (Packet)new CSMG_REQUEST_MAP_SERVER();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnRequestMapServer(this);
        }
    }
}
