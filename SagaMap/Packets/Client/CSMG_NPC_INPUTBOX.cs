namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_NPC_INPUTBOX" />.
    /// </summary>
    public class CSMG_NPC_INPUTBOX : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_NPC_INPUTBOX"/> class.
        /// </summary>
        public CSMG_NPC_INPUTBOX()
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
                return Global.Unicode.GetString(this.GetBytes((ushort)this.GetByte((ushort)2), (ushort)3)).Replace("\0", "");
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_NPC_INPUTBOX();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnNPCInputBox(this);
        }
    }
}
