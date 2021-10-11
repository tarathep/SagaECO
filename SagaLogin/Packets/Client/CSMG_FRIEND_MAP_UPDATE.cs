namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_FRIEND_MAP_UPDATE" />.
    /// </summary>
    public class CSMG_FRIEND_MAP_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_FRIEND_MAP_UPDATE"/> class.
        /// </summary>
        public CSMG_FRIEND_MAP_UPDATE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the MapID.
        /// </summary>
        public uint MapID
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
            return (Packet)new CSMG_FRIEND_MAP_UPDATE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnFriendMapUpdate(this);
        }
    }
}
