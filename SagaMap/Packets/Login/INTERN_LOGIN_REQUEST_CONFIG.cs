namespace SagaMap.Packets.Login
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="INTERN_LOGIN_REQUEST_CONFIG" />.
    /// </summary>
    public class INTERN_LOGIN_REQUEST_CONFIG : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="INTERN_LOGIN_REQUEST_CONFIG"/> class.
        /// </summary>
        public INTERN_LOGIN_REQUEST_CONFIG()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)65521;
        }

        /// <summary>
        /// Sets the Version.
        /// </summary>
        public Version Version
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }
    }
}
