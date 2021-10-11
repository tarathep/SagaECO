namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_POINT" />.
    /// </summary>
    public class SSMG_QUEST_POINT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_POINT"/> class.
        /// </summary>
        public SSMG_QUEST_POINT()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)6510;
        }

        /// <summary>
        /// Sets the QuestPoint.
        /// </summary>
        public ushort QuestPoint
        {
            set
            {
                this.PutUShort(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ResetTime.
        /// </summary>
        public uint ResetTime
        {
            set
            {
                this.PutUInt(value, (ushort)4);
            }
        }
    }
}
