namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAT_SIT" />.
    /// </summary>
    public class CSMG_CHAT_SIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAT_SIT"/> class.
        /// </summary>
        public CSMG_CHAT_SIT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_CHAT_SIT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnSit(this);
        }
    }
}
