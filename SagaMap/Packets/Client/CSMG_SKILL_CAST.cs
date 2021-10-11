namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_SKILL_CAST" />.
    /// </summary>
    public class CSMG_SKILL_CAST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_SKILL_CAST"/> class.
        /// </summary>
        public CSMG_SKILL_CAST()
        {
            this.offset = (ushort)2;
            this.data = new byte[14];
        }

        /// <summary>
        /// Gets or sets the SkillID.
        /// </summary>
        public ushort SkillID
        {
            get
            {
                return this.GetUShort((ushort)2);
            }
            set
            {
                this.PutUShort(value, (ushort)2);
            }
        }

        /// <summary>
        /// Gets or sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            get
            {
                return this.GetUInt((ushort)4);
            }
            set
            {
                this.PutUInt(value, (ushort)4);
            }
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public byte X
        {
            get
            {
                return this.GetByte((ushort)8);
            }
            set
            {
                this.PutByte(value, (ushort)8);
            }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public byte Y
        {
            get
            {
                return this.GetByte((ushort)9);
            }
            set
            {
                this.PutByte(value, (ushort)9);
            }
        }

        /// <summary>
        /// Gets or sets the SkillLv.
        /// </summary>
        public byte SkillLv
        {
            get
            {
                return this.GetByte((ushort)10);
            }
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Gets or sets the Random.
        /// </summary>
        public short Random
        {
            get
            {
                return this.GetShort((ushort)11);
            }
            set
            {
                this.PutShort(value, (ushort)11);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_SKILL_CAST();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnSkillCast(this);
        }
    }
}
