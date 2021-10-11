namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_MEMBER" />.
    /// </summary>
    public class SSMG_PARTY_MEMBER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_MEMBER"/> class.
        /// </summary>
        public SSMG_PARTY_MEMBER()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)6625;
        }

        /// <summary>
        /// Sets the PartyIndex.
        /// </summary>
        public int PartyIndex
        {
            set
            {
                this.PutInt(value, (ushort)2);
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
        /// Sets the CharName.
        /// </summary>
        public string CharName
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[12 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)10);
                this.PutBytes(bytes, (ushort)11);
            }
        }

        /// <summary>
        /// Sets a value indicating whether Leader.
        /// </summary>
        public bool Leader
        {
            set
            {
                byte num = this.GetByte((ushort)10);
                if (value)
                    this.PutByte((byte)1, (ushort)(11U + (uint)num));
                else
                    this.PutByte((byte)0, (ushort)(11U + (uint)num));
            }
        }
    }
}
