namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_PLAYER_STATS_UP" />.
    /// </summary>
    public class CSMG_PLAYER_STATS_UP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_PLAYER_STATS_UP"/> class.
        /// </summary>
        public CSMG_PLAYER_STATS_UP()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Type.
        /// </summary>
        public byte Type
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
            return (Packet)new CSMG_PLAYER_STATS_UP();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnStatsUp(this);
        }
    }
}
