namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_GOLD_UPDATE" />.
    /// </summary>
    public class SSMG_PLAYER_GOLD_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_GOLD_UPDATE"/> class.
        /// </summary>
        public SSMG_PLAYER_GOLD_UPDATE()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)2540;
        }

        /// <summary>
        /// Sets the Gold.
        /// </summary>
        public uint Gold
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
