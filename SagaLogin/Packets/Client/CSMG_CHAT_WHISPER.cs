namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAT_WHISPER" />.
    /// </summary>
    public class CSMG_CHAT_WHISPER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAT_WHISPER"/> class.
        /// </summary>
        public CSMG_CHAT_WHISPER()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Receiver.
        /// </summary>
        public string Receiver
        {
            get
            {
                byte num = this.GetByte((ushort)2);
                return Global.Unicode.GetString(this.GetBytes((ushort)num, (ushort)3)).Replace("\0", "");
            }
        }

        /// <summary>
        /// Gets the Content.
        /// </summary>
        public string Content
        {
            get
            {
                byte num1 = this.GetByte((ushort)2);
                byte num2 = this.GetByte((ushort)(3U + (uint)num1));
                return Global.Unicode.GetString(this.GetBytes((ushort)num2, (ushort)(4U + (uint)num1))).Replace("\0", "");
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_CHAT_WHISPER();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnChatWhisper(this);
        }
    }
}
