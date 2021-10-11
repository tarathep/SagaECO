namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_QUIT_RESULT" />.
    /// </summary>
    public class SSMG_RING_QUIT_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_QUIT_RESULT"/> class.
        /// </summary>
        public SSMG_RING_QUIT_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6846;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }
    }
}
