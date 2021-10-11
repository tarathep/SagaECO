namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAT_MOTION" />.
    /// </summary>
    public class SSMG_CHAT_MOTION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAT_MOTION"/> class.
        /// </summary>
        public SSMG_CHAT_MOTION()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)4636;
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
        /// Sets the Motion.
        /// </summary>
        public MotionType Motion
        {
            set
            {
                this.PutUShort((ushort)value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Loop.
        /// </summary>
        public byte Loop
        {
            set
            {
                this.PutByte(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the Special.
        /// </summary>
        public byte Special
        {
            set
            {
                this.PutByte(value, (ushort)9);
            }
        }
    }
}
