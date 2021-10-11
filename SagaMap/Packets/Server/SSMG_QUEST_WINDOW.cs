namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_WINDOW" />.
    /// </summary>
    public class SSMG_QUEST_WINDOW : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_WINDOW"/> class.
        /// </summary>
        public SSMG_QUEST_WINDOW()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)6506;
        }
    }
}
