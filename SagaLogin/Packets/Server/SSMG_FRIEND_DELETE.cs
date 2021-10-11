namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_DELETE" />.
    /// </summary>
    public class SSMG_FRIEND_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_DELETE"/> class.
        /// </summary>
        public SSMG_FRIEND_DELETE()
        {
            this.data = new byte[6];
            this.ID = (ushort)216;
        }

        /// <summary>
        /// Sets the CharID.
        /// </summary>
        public uint CharID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
