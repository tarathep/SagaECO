namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAT_EMOTION" />.
    /// </summary>
    public class SSMG_CHAT_EMOTION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAT_EMOTION"/> class.
        /// </summary>
        public SSMG_CHAT_EMOTION()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)4631;
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
        /// Sets the Emotion.
        /// </summary>
        public uint Emotion
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }
    }
}
