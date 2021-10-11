namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAT_MOTION" />.
    /// </summary>
    public class CSMG_CHAT_MOTION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAT_MOTION"/> class.
        /// </summary>
        public CSMG_CHAT_MOTION()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Motion.
        /// </summary>
        public MotionType Motion
        {
            get
            {
                return (MotionType)this.GetUShort((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Loop.
        /// </summary>
        public byte Loop
        {
            get
            {
                return this.GetByte((ushort)4);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_CHAT_MOTION();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnMotion(this);
        }
    }
}
