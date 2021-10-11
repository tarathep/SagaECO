namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PONG" />.
    /// </summary>
    public class SSMG_PONG : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PONG"/> class.
        /// </summary>
        public SSMG_PONG()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)51;
        }
    }
}
