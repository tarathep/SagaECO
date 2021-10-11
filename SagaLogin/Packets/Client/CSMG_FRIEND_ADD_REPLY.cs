namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_FRIEND_ADD_REPLY" />.
    /// </summary>
    public class CSMG_FRIEND_ADD_REPLY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_FRIEND_ADD_REPLY"/> class.
        /// </summary>
        public CSMG_FRIEND_ADD_REPLY()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Reply.
        /// </summary>
        public uint Reply
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the CharID.
        /// </summary>
        public uint CharID
        {
            get
            {
                return this.GetUInt((ushort)6);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_FRIEND_ADD_REPLY();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnFriendAddReply(this);
        }
    }
}
