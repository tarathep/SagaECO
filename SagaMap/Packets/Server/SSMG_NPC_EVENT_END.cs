namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_EVENT_END" />.
    /// </summary>
    public class SSMG_NPC_EVENT_END : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_EVENT_END"/> class.
        /// </summary>
        public SSMG_NPC_EVENT_END()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)1501;
        }
    }
}
