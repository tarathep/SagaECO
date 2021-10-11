namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_PLAYER_MOVE" />.
    /// </summary>
    public class CSMG_PLAYER_MOVE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_PLAYER_MOVE"/> class.
        /// </summary>
        public CSMG_PLAYER_MOVE()
        {
            this.data = new byte[10];
            this.ID = (ushort)4600;
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public short X
        {
            get
            {
                return this.GetShort((ushort)2);
            }
            set
            {
                this.PutShort(value, (ushort)2);
            }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public short Y
        {
            get
            {
                return this.GetShort((ushort)4);
            }
            set
            {
                this.PutShort(value, (ushort)4);
            }
        }

        /// <summary>
        /// Gets or sets the Dir.
        /// </summary>
        public ushort Dir
        {
            get
            {
                return this.GetUShort((ushort)6);
            }
            set
            {
                this.PutUShort(value, (ushort)6);
            }
        }

        /// <summary>
        /// Gets or sets the MoveType.
        /// </summary>
        public MoveType MoveType
        {
            get
            {
                return (MoveType)this.GetUShort((ushort)8);
            }
            set
            {
                this.PutUShort((ushort)value, (ushort)8);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_PLAYER_MOVE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnMove(this);
        }
    }
}
