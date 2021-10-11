namespace SagaLogin.Packets.Server
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_STATUS_UPDATE" />.
    /// </summary>
    public class SSMG_FRIEND_STATUS_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_STATUS_UPDATE"/> class.
        /// </summary>
        public SSMG_FRIEND_STATUS_UPDATE()
        {
            this.data = new byte[7];
            this.ID = (ushort)237;
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

        /// <summary>
        /// Sets the Status.
        /// </summary>
        public CharStatus Status
        {
            set
            {
                this.PutByte((byte)value, (ushort)6);
            }
        }
    }
}
