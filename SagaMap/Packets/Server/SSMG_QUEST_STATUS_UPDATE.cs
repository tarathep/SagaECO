namespace SagaMap.Packets.Server
{
    using SagaDB.Quests;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_STATUS_UPDATE" />.
    /// </summary>
    public class SSMG_QUEST_STATUS_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_STATUS_UPDATE"/> class.
        /// </summary>
        public SSMG_QUEST_STATUS_UPDATE()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)6535;
        }

        /// <summary>
        /// Sets the Status.
        /// </summary>
        public QuestStatus Status
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }
    }
}
