namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_NPC_EVENT_START" />.
    /// </summary>
    public class CSMG_NPC_EVENT_START : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_NPC_EVENT_START"/> class.
        /// </summary>
        public CSMG_NPC_EVENT_START()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the EventID.
        /// </summary>
        public uint EventID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the X.
        /// </summary>
        public byte X
        {
            get
            {
                return this.GetByte((ushort)6);
            }
        }

        /// <summary>
        /// Gets the Y.
        /// </summary>
        public byte Y
        {
            get
            {
                return this.GetByte((ushort)7);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_NPC_EVENT_START();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnNPCEventStart(this);
        }
    }
}
