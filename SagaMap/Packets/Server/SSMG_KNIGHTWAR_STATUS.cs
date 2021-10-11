namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_KNIGHTWAR_STATUS" />.
    /// </summary>
    public class SSMG_KNIGHTWAR_STATUS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_KNIGHTWAR_STATUS"/> class.
        /// </summary>
        public SSMG_KNIGHTWAR_STATUS()
        {
            this.data = new byte[22];
            this.offset = (ushort)2;
            this.ID = (ushort)7005;
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
        /// Sets the EastPoint.
        /// </summary>
        public int EastPoint
        {
            set
            {
                this.PutInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the WestPoint.
        /// </summary>
        public int WestPoint
        {
            set
            {
                this.PutInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the SouthPoint.
        /// </summary>
        public int SouthPoint
        {
            set
            {
                this.PutInt(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the NorthPoint.
        /// </summary>
        public int NorthPoint
        {
            set
            {
                this.PutInt(value, (ushort)18);
            }
        }
    }
}
