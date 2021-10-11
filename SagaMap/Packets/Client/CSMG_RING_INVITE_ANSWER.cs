namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_RING_INVITE_ANSWER" />.
    /// </summary>
    public class CSMG_RING_INVITE_ANSWER : Packet
    {
        /// <summary>
        /// Defines the accepted.
        /// </summary>
        private bool accepted = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_RING_INVITE_ANSWER"/> class.
        /// </summary>
        /// <param name="accepted">The accepted<see cref="bool"/>.</param>
        public CSMG_RING_INVITE_ANSWER(bool accepted)
        {
            this.offset = (ushort)2;
            this.accepted = accepted;
        }

        /// <summary>
        /// Gets the CharID.
        /// </summary>
        public uint CharID
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
            return (Packet)new CSMG_RING_INVITE_ANSWER(this.accepted);
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRingInviteAnswer(this, this.accepted);
        }
    }
}
