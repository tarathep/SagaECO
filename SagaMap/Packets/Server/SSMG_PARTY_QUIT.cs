namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_QUIT" />.
    /// </summary>
    public class SSMG_PARTY_QUIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_QUIT"/> class.
        /// </summary>
        public SSMG_PARTY_QUIT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6606;
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
