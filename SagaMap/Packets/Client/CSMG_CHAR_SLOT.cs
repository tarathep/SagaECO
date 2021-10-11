namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAR_SLOT" />.
    /// </summary>
    public class CSMG_CHAR_SLOT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAR_SLOT"/> class.
        /// </summary>
        public CSMG_CHAR_SLOT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_CHAR_SLOT();
        }

        /// <summary>
        /// Gets the Slot.
        /// </summary>
        public byte Slot
        {
            get
            {
                return this.GetByte((ushort)6);
            }
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnCharSlot(this);
        }
    }
}
