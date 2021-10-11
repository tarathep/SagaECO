namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_COST_LIMIT" />.
    /// </summary>
    public class SSMG_DEM_COST_LIMIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_COST_LIMIT"/> class.
        /// </summary>
        public SSMG_DEM_COST_LIMIT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7770;
        }

        /// <summary>
        /// Sets the CurrentEP.
        /// </summary>
        public short CurrentEP
        {
            set
            {
                this.PutShort(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the EPRequired.
        /// </summary>
        public short EPRequired
        {
            set
            {
                this.PutShort(value, (ushort)4);
            }
        }
    }
}
