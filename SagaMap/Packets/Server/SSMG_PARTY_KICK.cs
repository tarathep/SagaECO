namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_KICK" />.
    /// </summary>
    public class SSMG_PARTY_KICK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_KICK"/> class.
        /// </summary>
        public SSMG_PARTY_KICK()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6611;
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
