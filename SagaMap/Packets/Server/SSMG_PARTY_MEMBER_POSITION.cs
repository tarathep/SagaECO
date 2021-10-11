namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_MEMBER_POSITION" />.
    /// </summary>
    public class SSMG_PARTY_MEMBER_POSITION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_MEMBER_POSITION"/> class.
        /// </summary>
        public SSMG_PARTY_MEMBER_POSITION()
        {
            this.data = new byte[22];
            this.offset = (ushort)2;
            this.ID = (ushort)6640;
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
        /// Sets the MapID.
        /// </summary>
        public uint MapID
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutUInt((uint)value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutUInt((uint)value, (ushort)18);
            }
        }
    }
}
