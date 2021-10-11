namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_MAP_UPDATE" />.
    /// </summary>
    public class SSMG_FRIEND_MAP_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_MAP_UPDATE"/> class.
        /// </summary>
        public SSMG_FRIEND_MAP_UPDATE()
        {
            this.data = new byte[10];
            this.ID = (ushort)232;
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
        /// Sets the MapID.
        /// </summary>
        public uint MapID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }
    }
}
