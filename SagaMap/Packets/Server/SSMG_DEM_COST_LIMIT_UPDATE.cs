namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_COST_LIMIT_UPDATE" />.
    /// </summary>
    public class SSMG_DEM_COST_LIMIT_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_COST_LIMIT_UPDATE"/> class.
        /// </summary>
        public SSMG_DEM_COST_LIMIT_UPDATE()
        {
            this.data = new byte[9];
            this.offset = (ushort)2;
            this.ID = (ushort)7773;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_DEM_COST_LIMIT_UPDATE.Results Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the CurrentEP.
        /// </summary>
        public short CurrentEP
        {
            set
            {
                this.PutShort(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the EPRequired.
        /// </summary>
        public short EPRequired
        {
            set
            {
                this.PutShort(value, (ushort)5);
            }
        }

        /// <summary>
        /// Sets the CL.
        /// </summary>
        public short CL
        {
            set
            {
                this.PutShort(value, (ushort)7);
            }
        }

        /// <summary>
        /// Defines the Results.
        /// </summary>
        public enum Results
        {
            /// <summary>
            /// Defines the LV_MAXIMUM.
            /// </summary>
            LV_MAXIMUM = -4,

            /// <summary>
            /// Defines the CL_MAXIMUM.
            /// </summary>
            CL_MAXIMUM = -3,

            /// <summary>
            /// Defines the NOT_ENOUGH_EP.
            /// </summary>
            NOT_ENOUGH_EP = -2,

            /// <summary>
            /// Defines the FAILED.
            /// </summary>
            FAILED = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
