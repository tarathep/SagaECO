namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_DEMIC_CONFIRM_RESULT" />.
    /// </summary>
    public class SSMG_DEM_DEMIC_CONFIRM_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_DEMIC_CONFIRM_RESULT"/> class.
        /// </summary>
        public SSMG_DEM_DEMIC_CONFIRM_RESULT()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)7759;
        }

        /// <summary>
        /// Sets the Page.
        /// </summary>
        public byte Page
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_DEM_DEMIC_CONFIRM_RESULT.Results Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)3);
            }
        }

        /// <summary>
        /// Defines the Results.
        /// </summary>
        public enum Results
        {
            /// <summary>
            /// Defines the NOT_ENOUGH_EP.
            /// </summary>
            NOT_ENOUGH_EP = -3,

            /// <summary>
            /// Defines the TOO_MANY_ITEMS.
            /// </summary>
            TOO_MANY_ITEMS = -2,

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
