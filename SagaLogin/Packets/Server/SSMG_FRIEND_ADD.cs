namespace SagaLogin.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_ADD" />.
    /// </summary>
    public class SSMG_FRIEND_ADD : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_ADD"/> class.
        /// </summary>
        public SSMG_FRIEND_ADD()
        {
            this.data = new byte[7];
            this.ID = (ushort)211;
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
                byte[] numArray = new byte[7 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
            }
        }
    }
}
