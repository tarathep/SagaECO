namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_THEATER_SCHEDULE_HEADER" />.
    /// </summary>
    public class SSMG_THEATER_SCHEDULE_HEADER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_THEATER_SCHEDULE_HEADER"/> class.
        /// </summary>
        public SSMG_THEATER_SCHEDULE_HEADER()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)6810;
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

        /// <summary>
        /// Sets the Count.
        /// </summary>
        public int Count
        {
            set
            {
                this.PutInt(value, (ushort)6);
            }
        }
    }
}
