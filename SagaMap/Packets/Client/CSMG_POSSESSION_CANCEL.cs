namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_POSSESSION_CANCEL" />.
    /// </summary>
    public class CSMG_POSSESSION_CANCEL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_POSSESSION_CANCEL"/> class.
        /// </summary>
        public CSMG_POSSESSION_CANCEL()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets or sets the PossessionPosition.
        /// </summary>
        public PossessionPosition PossessionPosition
        {
            get
            {
                return (PossessionPosition)this.GetByte((ushort)2);
            }
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_POSSESSION_CANCEL();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnPossessionCancel(this);
        }
    }
}
