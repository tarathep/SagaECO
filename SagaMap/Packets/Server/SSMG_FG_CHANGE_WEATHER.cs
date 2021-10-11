namespace SagaMap.Packets.Server
{
    using SagaLib;
    using SagaMap.Scripting;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_CHANGE_WEATHER" />.
    /// </summary>
    public class SSMG_FG_CHANGE_WEATHER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_CHANGE_WEATHER"/> class.
        /// </summary>
        public SSMG_FG_CHANGE_WEATHER()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5052;
        }

        /// <summary>
        /// Sets the Weather.
        /// </summary>
        public FG_Weather Weather
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }
    }
}
