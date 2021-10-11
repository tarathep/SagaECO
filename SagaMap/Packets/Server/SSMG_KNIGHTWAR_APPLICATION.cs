namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_KNIGHTWAR_APPLICATION" />.
    /// </summary>
    public class SSMG_KNIGHTWAR_APPLICATION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_KNIGHTWAR_APPLICATION"/> class.
        /// </summary>
        public SSMG_KNIGHTWAR_APPLICATION()
        {
            this.data = new byte[22];
            this.offset = (ushort)2;
            this.ID = (ushort)7000;
        }

        /// <summary>
        /// Sets the Time.
        /// </summary>
        public int Time
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the EastCount.
        /// </summary>
        public int EastCount
        {
            set
            {
                this.PutInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the WestCount.
        /// </summary>
        public int WestCount
        {
            set
            {
                this.PutInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the SouthCount.
        /// </summary>
        public int SouthCount
        {
            set
            {
                this.PutInt(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the NorthCount.
        /// </summary>
        public int NorthCount
        {
            set
            {
                this.PutInt(value, (ushort)18);
            }
        }
    }
}
