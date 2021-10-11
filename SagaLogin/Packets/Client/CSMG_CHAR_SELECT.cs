namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAR_SELECT" />.
    /// </summary>
    public class CSMG_CHAR_SELECT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAR_SELECT"/> class.
        /// </summary>
        public CSMG_CHAR_SELECT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Slot.
        /// </summary>
        public byte Slot
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
            return (Packet)new CSMG_CHAR_SELECT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnCharSelect(this);
        }
    }
}
