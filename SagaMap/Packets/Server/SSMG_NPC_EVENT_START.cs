namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_EVENT_START" />.
    /// </summary>
    public class SSMG_NPC_EVENT_START : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_EVENT_START"/> class.
        /// </summary>
        public SSMG_NPC_EVENT_START()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)1500;
        }
    }
}
