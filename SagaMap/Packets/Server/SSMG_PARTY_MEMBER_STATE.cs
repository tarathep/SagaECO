namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_MEMBER_STATE" />.
    /// </summary>
    public class SSMG_PARTY_MEMBER_STATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_MEMBER_STATE"/> class.
        /// </summary>
        public SSMG_PARTY_MEMBER_STATE()
        {
            this.data = new byte[14];
            this.offset = (ushort)2;
            this.ID = (ushort)6630;
        }

        /// <summary>
        /// Sets the PartyIndex.
        /// </summary>
        public uint PartyIndex
        {
            set
            {
                this.PutUInt(value, (ushort)2);
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
        /// Sets a value indicating whether Online.
        /// </summary>
        public bool Online
        {
            set
            {
                if (value)
                    this.PutUInt(1U, (ushort)10);
                else
                    this.PutUInt(0U, (ushort)10);
            }
        }
    }
}
