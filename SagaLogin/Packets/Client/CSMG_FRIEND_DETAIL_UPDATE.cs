namespace SagaLogin.Packets.Client
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_FRIEND_DETAIL_UPDATE" />.
    /// </summary>
    public class CSMG_FRIEND_DETAIL_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_FRIEND_DETAIL_UPDATE"/> class.
        /// </summary>
        public CSMG_FRIEND_DETAIL_UPDATE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Job.
        /// </summary>
        public PC_JOB Job
        {
            get
            {
                return (PC_JOB)this.GetUShort((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Level.
        /// </summary>
        public byte Level
        {
            get
            {
                return (byte)this.GetUShort((ushort)4);
            }
        }

        /// <summary>
        /// Gets the JobLevel.
        /// </summary>
        public byte JobLevel
        {
            get
            {
                return (byte)this.GetUShort((ushort)6);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_FRIEND_DETAIL_UPDATE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnFriendDetailUpdate(this);
        }
    }
}
