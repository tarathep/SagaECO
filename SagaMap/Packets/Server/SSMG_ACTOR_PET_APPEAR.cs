namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_PET_APPEAR" />.
    /// </summary>
    public class SSMG_ACTOR_PET_APPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_PET_APPEAR"/> class.
        /// </summary>
        public SSMG_ACTOR_PET_APPEAR()
        {
            if (Singleton<Configuration>.Instance.Version == Version.Saga6)
                this.data = new byte[30];
            if (Singleton<Configuration>.Instance.Version >= Version.Saga9)
                this.data = new byte[36];
            this.offset = (ushort)2;
            this.ID = (ushort)4655;
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
        /// Sets the Unknown.
        /// </summary>
        public byte Unknown
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the OwnerActorID.
        /// </summary>
        public uint OwnerActorID
        {
            set
            {
                this.PutUInt(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the OwnerCharID.
        /// </summary>
        public uint OwnerCharID
        {
            set
            {
                this.PutUInt(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the OwnerLevel.
        /// </summary>
        public byte OwnerLevel
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutByte(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the OwnerWRP.
        /// </summary>
        public uint OwnerWRP
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutUInt(value, (ushort)16);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version == Version.Saga6)
                    this.PutByte(value, (ushort)15);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutByte(value, (ushort)21);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version == Version.Saga6)
                    this.PutByte(value, (ushort)16);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutByte(value, (ushort)22);
            }
        }

        /// <summary>
        /// Sets the Speed.
        /// </summary>
        public ushort Speed
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version == Version.Saga6)
                    this.PutUShort(value, (ushort)17);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutUShort(value, (ushort)23);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public byte Dir
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version == Version.Saga6)
                    this.PutByte(value, (ushort)19);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutByte(value, (ushort)25);
            }
        }

        /// <summary>
        /// Sets the HP.
        /// </summary>
        public uint HP
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version == Version.Saga6)
                    this.PutUInt(value, (ushort)20);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutUInt(value, (ushort)26);
            }
        }

        /// <summary>
        /// Sets the MaxHP.
        /// </summary>
        public uint MaxHP
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version == Version.Saga6)
                    this.PutUInt(value, (ushort)24);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9)
                    return;
                this.PutUInt(value, (ushort)30);
            }
        }
    }
}
