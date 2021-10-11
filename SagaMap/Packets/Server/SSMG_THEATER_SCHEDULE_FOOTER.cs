namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_THEATER_SCHEDULE_FOOTER" />.
    /// </summary>
    public class SSMG_THEATER_SCHEDULE_FOOTER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_THEATER_SCHEDULE_FOOTER"/> class.
        /// </summary>
        public SSMG_THEATER_SCHEDULE_FOOTER()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6812;
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
