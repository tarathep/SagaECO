namespace SagaLogin.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_DETAIL_UPDATE" />.
    /// </summary>
    public class SSMG_FRIEND_DETAIL_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_DETAIL_UPDATE"/> class.
        /// </summary>
        public SSMG_FRIEND_DETAIL_UPDATE()
        {
            this.data = new byte[12];
            this.ID = (ushort)227;
        }

        /// <summary>
        /// Sets the CharID.
        /// </summary>
        public uint CharID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Job.
        /// </summary>
        public PC_JOB Job
        {
            set
            {
                this.PutUShort((ushort)value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Level.
        /// </summary>
        public byte Level
        {
            set
            {
                this.PutUShort((ushort)value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the JobLevel.
        /// </summary>
        public byte JobLevel
        {
            set
            {
                this.PutUShort((ushort)value, (ushort)10);
            }
        }
    }
}
