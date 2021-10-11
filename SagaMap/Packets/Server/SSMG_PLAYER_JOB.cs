namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_JOB" />.
    /// </summary>
    public class SSMG_PLAYER_JOB : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_JOB"/> class.
        /// </summary>
        public SSMG_PLAYER_JOB()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)580;
        }

        /// <summary>
        /// Sets the Job.
        /// </summary>
        public PC_JOB Job
        {
            set
            {
                this.PutUInt((uint)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the JointJob.
        /// </summary>
        public PC_JOB JointJob
        {
            set
            {
                this.PutUInt((uint)(value - 1000), (ushort)6);
            }
        }
    }
}
