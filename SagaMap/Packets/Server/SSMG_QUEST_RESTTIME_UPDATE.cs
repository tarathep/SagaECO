namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_RESTTIME_UPDATE" />.
    /// </summary>
    public class SSMG_QUEST_RESTTIME_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_RESTTIME_UPDATE"/> class.
        /// </summary>
        public SSMG_QUEST_RESTTIME_UPDATE()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6520;
        }

        /// <summary>
        /// Sets the RestTime.
        /// </summary>
        public int RestTime
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }
    }
}
