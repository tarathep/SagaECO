namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_DELETE" />.
    /// </summary>
    public class SSMG_QUEST_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_DELETE"/> class.
        /// </summary>
        public SSMG_QUEST_DELETE()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)6540;
        }
    }
}
