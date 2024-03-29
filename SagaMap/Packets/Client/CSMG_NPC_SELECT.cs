namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_NPC_SELECT" />.
    /// </summary>
    public class CSMG_NPC_SELECT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_NPC_SELECT"/> class.
        /// </summary>
        public CSMG_NPC_SELECT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Result.
        /// </summary>
        public byte Result
        {
            get
            {
                return this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_NPC_SELECT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnNPCSelect(this);
        }
    }
}
