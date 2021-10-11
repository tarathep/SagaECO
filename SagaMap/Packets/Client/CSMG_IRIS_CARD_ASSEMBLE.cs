namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_IRIS_CARD_ASSEMBLE" />.
    /// </summary>
    public class CSMG_IRIS_CARD_ASSEMBLE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_IRIS_CARD_ASSEMBLE"/> class.
        /// </summary>
        public CSMG_IRIS_CARD_ASSEMBLE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the CardID.
        /// </summary>
        public uint CardID
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
            return (Packet)new CSMG_IRIS_CARD_ASSEMBLE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnIrisCardAssemble(this);
        }
    }
}
