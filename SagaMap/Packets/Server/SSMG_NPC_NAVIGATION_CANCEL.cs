namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_NAVIGATION_CANCEL" />.
    /// </summary>
    public class SSMG_NPC_NAVIGATION_CANCEL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_NAVIGATION_CANCEL"/> class.
        /// </summary>
        public SSMG_NPC_NAVIGATION_CANCEL()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)6701;
        }
    }
}
