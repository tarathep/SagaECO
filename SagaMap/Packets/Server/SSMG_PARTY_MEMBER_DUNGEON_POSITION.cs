namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_MEMBER_DUNGEON_POSITION" />.
    /// </summary>
    public class SSMG_PARTY_MEMBER_DUNGEON_POSITION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_MEMBER_DUNGEON_POSITION"/> class.
        /// </summary>
        public SSMG_PARTY_MEMBER_DUNGEON_POSITION()
        {
            this.data = new byte[13];
            this.offset = (ushort)2;
            this.ID = (ushort)7300;
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
        /// Sets the MapID.
        /// </summary>
        public uint MapID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public byte Dir
        {
            set
            {
                this.PutByte(value, (ushort)12);
            }
        }
    }
}
