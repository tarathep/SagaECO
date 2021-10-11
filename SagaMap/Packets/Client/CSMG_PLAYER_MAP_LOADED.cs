namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_PLAYER_MAP_LOADED" />.
    /// </summary>
    public class CSMG_PLAYER_MAP_LOADED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_PLAYER_MAP_LOADED"/> class.
        /// </summary>
        public CSMG_PLAYER_MAP_LOADED()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_PLAYER_MAP_LOADED();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnMapLoaded(this);
        }
    }
}
