namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_MOB_APPEAR" />.
    /// </summary>
    public class SSMG_ACTOR_MOB_APPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_MOB_APPEAR"/> class.
        /// </summary>
        public SSMG_ACTOR_MOB_APPEAR()
        {
            this.data = new byte[23];
            this.offset = (ushort)2;
            this.ID = (ushort)4640;
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
        /// Sets the MobID.
        /// </summary>
        public uint MobID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the Speed.
        /// </summary>
        public ushort Speed
        {
            set
            {
                this.PutUShort(value, (ushort)12);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public byte Dir
        {
            set
            {
                this.PutByte(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the HP.
        /// </summary>
        public uint HP
        {
            set
            {
                this.PutUInt(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the MaxHP.
        /// </summary>
        public uint MaxHP
        {
            set
            {
                this.PutUInt(value, (ushort)19);
            }
        }
    }
}
