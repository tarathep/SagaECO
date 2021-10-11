namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_FURNITURE_RECONFIG" />.
    /// </summary>
    public class SSMG_FG_FURNITURE_RECONFIG : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_FURNITURE_RECONFIG"/> class.
        /// </summary>
        public SSMG_FG_FURNITURE_RECONFIG()
        {
            this.data = new byte[14];
            this.offset = (ushort)2;
            this.ID = (ushort)7186;
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
        /// Sets the X.
        /// </summary>
        public short X
        {
            set
            {
                this.PutShort(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public short Y
        {
            set
            {
                this.PutShort(value, (ushort)8);
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
