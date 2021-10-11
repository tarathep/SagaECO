namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_EXP" />.
    /// </summary>
    public class SSMG_PLAYER_EXP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_EXP"/> class.
        /// </summary>
        public SSMG_PLAYER_EXP()
        {
            if (Singleton<Configuration>.Instance.Version >= Version.Saga10)
                this.data = new byte[34];
            else
                this.data = new byte[18];
            this.offset = (ushort)2;
            this.ID = (ushort)565;
        }

        /// <summary>
        /// Sets the EXPPercentage.
        /// </summary>
        public uint EXPPercentage
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the JEXPPercentage.
        /// </summary>
        public uint JEXPPercentage
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the WRP.
        /// </summary>
        public int WRP
        {
            set
            {
                this.PutInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the ECoin.
        /// </summary>
        public uint ECoin
        {
            set
            {
                this.PutUInt(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the Exp.
        /// </summary>
        public long Exp
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version < Version.Saga10)
                    return;
                this.PutLong(value, (ushort)18);
            }
        }

        /// <summary>
        /// Sets the JExp.
        /// </summary>
        public long JExp
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version < Version.Saga10)
                    return;
                this.PutLong(value, (ushort)26);
            }
        }
    }
}
