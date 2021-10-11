namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_FURNITURE_MOTION" />.
    /// </summary>
    public class SSMG_FG_FURNITURE_MOTION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_FURNITURE_MOTION"/> class.
        /// </summary>
        public SSMG_FG_FURNITURE_MOTION()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)7176;
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
        public ushort Motion
        {
            set
            {
                this.PutUShort(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the EndMotion.
        /// </summary>
        public ushort EndMotion
        {
            set
            {
                this.PutUShort(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the Z.
        /// </summary>
        public short Z
        {
            set
            {
                this.PutShort(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public ushort Dir
        {
            set
            {
                this.PutUShort(value, (ushort)12);
            }
        }
    }
}
