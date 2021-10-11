namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAT_EMOTION" />.
    /// </summary>
    public class CSMG_CHAT_EMOTION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAT_EMOTION"/> class.
        /// </summary>
        public CSMG_CHAT_EMOTION()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Emotion.
        /// </summary>
        public uint Emotion
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
            return (Packet)new CSMG_CHAT_EMOTION();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnEmotion(this);
        }
    }
}
