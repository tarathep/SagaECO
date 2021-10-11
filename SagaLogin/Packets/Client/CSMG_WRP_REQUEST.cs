namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_WRP_REQUEST" />.
    /// </summary>
    public class CSMG_WRP_REQUEST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_WRP_REQUEST"/> class.
        /// </summary>
        public CSMG_WRP_REQUEST()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_WRP_REQUEST();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnWRPRequest(this);
        }
    }
}
