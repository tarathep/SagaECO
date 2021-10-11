namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_ADD_OK" />.
    /// </summary>
    public class SSMG_FRIEND_ADD_OK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_ADD_OK"/> class.
        /// </summary>
        public SSMG_FRIEND_ADD_OK()
        {
            this.data = new byte[6];
            this.ID = (ushort)213;
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
