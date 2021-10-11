namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_MOVE" />.
    /// </summary>
    public class SSMG_NPC_MOVE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_MOVE"/> class.
        /// </summary>
        public SSMG_NPC_MOVE()
        {
            this.data = new byte[18];
            this.offset = (ushort)2;
            this.ID = (ushort)1513;
            this.PutByte(byte.MaxValue, (ushort)10);
        }

        /// <summary>
        /// Sets the NPCID.
        /// </summary>
        public uint NPCID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the Speed.
        /// </summary>
        public ushort Speed
        {
            set
            {
                this.PutUShort(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the Type.
        /// </summary>
        public byte Type
        {
            set
            {
                this.PutByte(value, (ushort)12);
            }
        }

        /// <summary>
        /// Sets the Motion.
        /// </summary>
        public ushort Motion
        {
            set
            {
                this.PutUShort(value, (ushort)13);
            }
        }

        /// <summary>
        /// Sets the MotionSpeed.
        /// </summary>
        public ushort MotionSpeed
        {
            set
            {
                this.PutUShort(value, (ushort)15);
            }
        }
    }
}
