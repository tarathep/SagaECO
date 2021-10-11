namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_MAX_HPMPSP" />.
    /// </summary>
    public class SSMG_PLAYER_MAX_HPMPSP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_MAX_HPMPSP"/> class.
        /// </summary>
        public SSMG_PLAYER_MAX_HPMPSP()
        {
            this.data = new byte[23];
            this.offset = (ushort)2;
            this.ID = (ushort)545;
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
        /// Sets the MaxHP.
        /// </summary>
        public uint MaxHP
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version >= Version.Saga9)
                    this.PutByte((byte)4, (ushort)6);
                else
                    this.PutByte((byte)3, (ushort)6);
                this.PutUInt(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the MaxMP.
        /// </summary>
        public uint MaxMP
        {
            set
            {
                this.PutUInt(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the MaxSP.
        /// </summary>
        public uint MaxSP
        {
            set
            {
                this.PutUInt(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the MaxEP.
        /// </summary>
        public uint MaxEP
        {
            set
            {
                this.PutUInt(value, (ushort)19);
            }
        }
    }
}
