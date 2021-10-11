namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_LOGIN_ALLOWED" />.
    /// </summary>
    public class SSMG_LOGIN_ALLOWED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_LOGIN_ALLOWED"/> class.
        /// </summary>
        public SSMG_LOGIN_ALLOWED()
        {
            this.data = new byte[10];
            this.offset = (ushort)14;
            this.ID = (ushort)15;
        }

        /// <summary>
        /// Sets the FrontWord.
        /// </summary>
        public uint FrontWord
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the BackWord.
        /// </summary>
        public uint BackWord
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }
    }
}
