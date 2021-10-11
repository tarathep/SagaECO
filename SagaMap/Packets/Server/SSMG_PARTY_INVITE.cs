namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_INVITE" />.
    /// </summary>
    public class SSMG_PARTY_INVITE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_INVITE"/> class.
        /// </summary>
        public SSMG_PARTY_INVITE()
        {
            this.data = new byte[9];
            this.offset = (ushort)2;
            this.ID = (ushort)6602;
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
        /// Sets the Name.
        /// </summary>
        public string Name
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[9 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                byte length = (byte)bytes.Length;
                this.PutByte(length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
                this.PutByte((byte)1, (ushort)(7U + (uint)length));
            }
        }
    }
}
