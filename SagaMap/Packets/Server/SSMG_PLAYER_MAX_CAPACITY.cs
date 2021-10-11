namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_MAX_CAPACITY" />.
    /// </summary>
    public class SSMG_PLAYER_MAX_CAPACITY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_MAX_CAPACITY"/> class.
        /// </summary>
        public SSMG_PLAYER_MAX_CAPACITY()
        {
            this.data = new byte[36];
            this.offset = (ushort)2;
            this.ID = (ushort)561;
        }

        /// <summary>
        /// Sets the CapacityBody.
        /// </summary>
        public uint CapacityBody
        {
            set
            {
                this.PutByte((byte)4, (ushort)2);
                this.PutUInt(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the CapacityRight.
        /// </summary>
        public uint CapacityRight
        {
            set
            {
                this.PutUInt(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the CapacityLeft.
        /// </summary>
        public uint CapacityLeft
        {
            set
            {
                this.PutUInt(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the CapacityBack.
        /// </summary>
        public uint CapacityBack
        {
            set
            {
                this.PutUInt(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the PayloadBody.
        /// </summary>
        public uint PayloadBody
        {
            set
            {
                this.PutByte((byte)4, (ushort)19);
                this.PutUInt(value, (ushort)20);
            }
        }

        /// <summary>
        /// Sets the PayloadRight.
        /// </summary>
        public uint PayloadRight
        {
            set
            {
                this.PutUInt(value, (ushort)24);
            }
        }

        /// <summary>
        /// Sets the PayloadLeft.
        /// </summary>
        public uint PayloadLeft
        {
            set
            {
                this.PutUInt(value, (ushort)28);
            }
        }

        /// <summary>
        /// Sets the PayloadBack.
        /// </summary>
        public uint PayloadBack
        {
            set
            {
                this.PutUInt(value, (ushort)32);
            }
        }
    }
}
