namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_TAKEOFF" />.
    /// </summary>
    public class SSMG_FG_TAKEOFF : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_TAKEOFF"/> class.
        /// </summary>
        public SSMG_FG_TAKEOFF()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)6371;
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
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
