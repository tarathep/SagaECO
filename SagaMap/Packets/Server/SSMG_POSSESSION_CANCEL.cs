namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_POSSESSION_CANCEL" />.
    /// </summary>
    public class SSMG_POSSESSION_CANCEL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_POSSESSION_CANCEL"/> class.
        /// </summary>
        public SSMG_POSSESSION_CANCEL()
        {
            this.data = new byte[14];
            this.offset = (ushort)2;
            this.ID = (ushort)6016;
        }

        /// <summary>
        /// Sets the FromID.
        /// </summary>
        public uint FromID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ToID.
        /// </summary>
        public uint ToID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Position.
        /// </summary>
        public PossessionPosition Position
        {
            set
            {
                this.PutByte((byte)value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)12);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public byte Dir
        {
            set
            {
                this.PutByte(value, (ushort)13);
            }
        }
    }
}
