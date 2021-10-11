namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_PC_APPEAR" />.
    /// </summary>
    public class SSMG_ACTOR_PC_APPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_PC_APPEAR"/> class.
        /// </summary>
        public SSMG_ACTOR_PC_APPEAR()
        {
            this.data = new byte[24];
            this.offset = (ushort)2;
            this.ID = (ushort)4620;
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
        /// Sets the Dir.
        /// </summary>
        public byte Dir
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the PossessionActorID.
        /// </summary>
        public uint PossessionActorID
        {
            set
            {
                this.PutUInt(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the PossessionPosition.
        /// </summary>
        public PossessionPosition PossessionPosition
        {
            set
            {
                this.PutByte((byte)value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the HP.
        /// </summary>
        public uint HP
        {
            set
            {
                this.PutUInt(value, (ushort)16);
            }
        }

        /// <summary>
        /// Sets the MaxHP.
        /// </summary>
        public uint MaxHP
        {
            set
            {
                this.PutUInt(value, (ushort)20);
            }
        }
    }
}
