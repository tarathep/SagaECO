namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_IRIS_CARD_REMOVE" />.
    /// </summary>
    public class CSMG_IRIS_CARD_REMOVE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_IRIS_CARD_REMOVE"/> class.
        /// </summary>
        public CSMG_IRIS_CARD_REMOVE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the CardSlot.
        /// </summary>
        public short CardSlot
        {
            get
            {
                return this.GetShort((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Unknown.
        /// </summary>
        public byte Unknown
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
            return (Packet)new CSMG_IRIS_CARD_REMOVE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnIrisCardRemove(this);
        }
    }
}
