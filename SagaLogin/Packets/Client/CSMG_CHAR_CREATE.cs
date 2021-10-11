namespace SagaLogin.Packets.Client
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAR_CREATE" />.
    /// </summary>
    public class CSMG_CHAR_CREATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAR_CREATE"/> class.
        /// </summary>
        public CSMG_CHAR_CREATE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Slot.
        /// </summary>
        public byte Slot
        {
            get
            {
                return this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                byte[] bytes = this.GetBytes((ushort)this.GetByte((ushort)3), (ushort)4);
                return Global.Unicode.GetString(bytes).Replace("\0", "");
            }
        }

        /// <summary>
        /// Gets the Race.
        /// </summary>
        public PC_RACE Race
        {
            get
            {
                return (PC_RACE)this.GetByte((ushort)this.GetDataOffset());
            }
        }

        /// <summary>
        /// Gets the Gender.
        /// </summary>
        public PC_GENDER Gender
        {
            get
            {
                return (PC_GENDER)this.GetByte((ushort)((uint)this.GetDataOffset() + 1U));
            }
        }

        /// <summary>
        /// Gets the HairStyle.
        /// </summary>
        public byte HairStyle
        {
            get
            {
                return this.GetByte((ushort)((uint)this.GetDataOffset() + 2U));
            }
        }

        /// <summary>
        /// Gets the HairColor.
        /// </summary>
        public byte HairColor
        {
            get
            {
                return this.GetByte((ushort)((uint)this.GetDataOffset() + 3U));
            }
        }

        /// <summary>
        /// Gets the Face.
        /// </summary>
        public byte Face
        {
            get
            {
                return this.GetByte((ushort)((uint)this.GetDataOffset() + 4U));
            }
        }

        /// <summary>
        /// The GetDataOffset.
        /// </summary>
        /// <returns>The <see cref="byte"/>.</returns>
        private byte GetDataOffset()
        {
            return (byte)(4U + (uint)this.GetByte((ushort)3));
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_CHAR_CREATE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnCharCreate(this);
        }
    }
}
