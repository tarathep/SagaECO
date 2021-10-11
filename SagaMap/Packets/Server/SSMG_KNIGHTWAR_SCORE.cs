namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_KNIGHTWAR_SCORE" />.
    /// </summary>
    public class SSMG_KNIGHTWAR_SCORE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_KNIGHTWAR_SCORE"/> class.
        /// </summary>
        public SSMG_KNIGHTWAR_SCORE()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)7010;
        }

        /// <summary>
        /// Sets the Score.
        /// </summary>
        public int Score
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the DeathCount.
        /// </summary>
        public int DeathCount
        {
            set
            {
                this.PutInt(value, (ushort)6);
            }
        }
    }
}
