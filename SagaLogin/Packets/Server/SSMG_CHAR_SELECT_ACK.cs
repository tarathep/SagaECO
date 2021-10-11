namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAR_SELECT_ACK" />.
    /// </summary>
    public class SSMG_CHAR_SELECT_ACK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAR_SELECT_ACK"/> class.
        /// </summary>
        public SSMG_CHAR_SELECT_ACK()
        {
            this.data = new byte[18];
            this.offset = (ushort)14;
            this.ID = (ushort)168;
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
