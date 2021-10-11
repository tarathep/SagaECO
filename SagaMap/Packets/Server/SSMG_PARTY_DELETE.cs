namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_DELETE" />.
    /// </summary>
    public class SSMG_PARTY_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_DELETE"/> class.
        /// </summary>
        public SSMG_PARTY_DELETE()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)6621;
        }

        /// <summary>
        /// Sets the PartyID.
        /// </summary>
        public uint PartyID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the PartyName.
        /// </summary>
        public string PartyName
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[11 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the Reason.
        /// </summary>
        public SSMG_PARTY_DELETE.Result Reason
        {
            set
            {
                byte num = this.GetByte((ushort)6);
                this.PutInt((int)value, (ushort)(7U + (uint)num));
            }
        }

        /// <summary>
        /// Defines the Result.
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// Defines the DISMISSED.
            /// </summary>
            DISMISSED = 1,

            /// <summary>
            /// Defines the QUIT.
            /// </summary>
            QUIT = 2,

            /// <summary>
            /// Defines the KICKED.
            /// </summary>
            KICKED = 3,
        }
    }
}
