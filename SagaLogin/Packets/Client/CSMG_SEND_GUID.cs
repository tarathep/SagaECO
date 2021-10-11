namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_SEND_GUID" />.
    /// </summary>
    public class CSMG_SEND_GUID : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_SEND_GUID"/> class.
        /// </summary>
        public CSMG_SEND_GUID()
        {
            this.size = 360U;
            this.offset = (ushort)8;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_SEND_GUID();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnSendGUID(this);
        }
    }
}
