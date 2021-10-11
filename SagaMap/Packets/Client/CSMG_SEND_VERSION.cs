namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_SEND_VERSION" />.
    /// </summary>
    public class CSMG_SEND_VERSION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_SEND_VERSION"/> class.
        /// </summary>
        public CSMG_SEND_VERSION()
        {
            this.size = 10U;
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The GetVersion.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetVersion()
        {
            return Conversions.bytes2HexString(this.GetBytes((ushort)6, (ushort)4));
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_SEND_VERSION();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnSendVersion(this);
        }
    }
}
