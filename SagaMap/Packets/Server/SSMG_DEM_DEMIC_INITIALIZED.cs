namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_DEMIC_INITIALIZED" />.
    /// </summary>
    public class SSMG_DEM_DEMIC_INITIALIZED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_DEMIC_INITIALIZED"/> class.
        /// </summary>
        public SSMG_DEM_DEMIC_INITIALIZED()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7757;
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
        public SSMG_DEM_DEMIC_INITIALIZED.Results Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the EngageTask.
        /// </summary>
        public byte EngageTask
        {
            set
            {
                byte num = value;
                this.PutByte(num != byte.MaxValue ? (byte)((uint)num + 1U) : (byte)0, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the EngageTask2.
        /// </summary>
        public byte EngageTask2
        {
            set
            {
                byte num = value;
                this.PutByte(num != byte.MaxValue ? (byte)((uint)num + 1U) : (byte)0, (ushort)5);
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
