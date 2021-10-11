namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_MEMBER_HPMPSP" />.
    /// </summary>
    public class SSMG_PARTY_MEMBER_HPMPSP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_MEMBER_HPMPSP"/> class.
        /// </summary>
        public SSMG_PARTY_MEMBER_HPMPSP()
        {
            this.data = new byte[34];
            this.offset = (ushort)2;
            this.ID = (ushort)6635;
        }

        /// <summary>
        /// Sets the PartyIndex.
        /// </summary>
        public byte PartyIndex
        {
            set
            {
                this.PutUInt((uint)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the CharID.
        /// </summary>
        public uint CharID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the HP.
        /// </summary>
        public uint HP
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the MaxHP.
        /// </summary>
        public uint MaxHP
        {
            set
            {
                this.PutUInt(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the MP.
        /// </summary>
        public uint MP
        {
            set
            {
                this.PutUInt(value, (ushort)18);
            }
        }

        /// <summary>
        /// Sets the MaxMP.
        /// </summary>
        public uint MaxMP
        {
            set
            {
                this.PutUInt(value, (ushort)22);
            }
        }

        /// <summary>
        /// Sets the SP.
        /// </summary>
        public uint SP
        {
            set
            {
                this.PutUInt(value, (ushort)26);
            }
        }

        /// <summary>
        /// Sets the MaxSP.
        /// </summary>
        public uint MaxSP
        {
            set
            {
                this.PutUInt(value, (ushort)30);
            }
        }
    }
}
