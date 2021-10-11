namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAR_STATUS" />.
    /// </summary>
    public class SSMG_CHAR_STATUS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAR_STATUS"/> class.
        /// </summary>
        public SSMG_CHAR_STATUS()
        {
            this.data = new byte[5];
            this.offset = (ushort)14;
            this.ID = (ushort)221;
            this.PutByte((byte)1, (ushort)2);
            this.PutByte((byte)1, (ushort)3);
        }

        /// <summary>
        /// Sets the MapID.
        /// </summary>
        public uint MapID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
