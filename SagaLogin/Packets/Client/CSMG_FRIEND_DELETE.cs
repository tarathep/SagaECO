namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_FRIEND_DELETE" />.
    /// </summary>
    public class CSMG_FRIEND_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_FRIEND_DELETE"/> class.
        /// </summary>
        public CSMG_FRIEND_DELETE()
        {
            this.offset = (ushort)2;
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
            return (Packet)new CSMG_FRIEND_DELETE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnFriendDelete(this);
        }
    }
}
