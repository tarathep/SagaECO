namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_COUNT_UPDATE" />.
    /// </summary>
    public class SSMG_QUEST_COUNT_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_COUNT_UPDATE"/> class.
        /// </summary>
        public SSMG_QUEST_COUNT_UPDATE()
        {
            this.data = new byte[15];
            this.offset = (ushort)2;
            this.ID = (ushort)6515;
            this.PutByte((byte)3, (ushort)2);
        }

        /// <summary>
        /// Sets the Count1.
        /// </summary>
        public int Count1
        {
            set
            {
                this.PutInt(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Count2.
        /// </summary>
        public int Count2
        {
            set
            {
                this.PutInt(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the Count3.
        /// </summary>
        public int Count3
        {
            set
            {
                this.PutInt(value, (ushort)11);
            }
        }
    }
}
