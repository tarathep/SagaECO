namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAT_SIGN" />.
    /// </summary>
    public class CSMG_CHAT_SIGN : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAT_SIGN"/> class.
        /// </summary>
        public CSMG_CHAT_SIGN()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Content.
        /// </summary>
        public string Content
        {
            get
            {
                byte num = (byte)((uint)this.GetByte((ushort)2) - 1U);
                return Global.Unicode.GetString(this.GetBytes((ushort)num, (ushort)3));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_CHAT_SIGN();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnSign(this);
        }
    }
}
