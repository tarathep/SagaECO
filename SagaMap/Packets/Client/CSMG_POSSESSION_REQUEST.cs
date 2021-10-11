namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_POSSESSION_REQUEST" />.
    /// </summary>
    public class CSMG_POSSESSION_REQUEST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_POSSESSION_REQUEST"/> class.
        /// </summary>
        public CSMG_POSSESSION_REQUEST()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the ActorID.
        /// </summary>
        public uint ActorID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the PossessionPosition.
        /// </summary>
        public PossessionPosition PossessionPosition
        {
            get
            {
                return (PossessionPosition)this.GetByte((ushort)6);
            }
        }

        /// <summary>
        /// Gets the Comment.
        /// </summary>
        public string Comment
        {
            get
            {
                byte[] bytes = this.GetBytes((ushort)this.GetByte((ushort)7), (ushort)8);
                return Global.Unicode.GetString(bytes).Replace("\0", "");
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_POSSESSION_REQUEST();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnPossessionRequest(this);
        }
    }
}
