namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAT_PARTY" />.
    /// </summary>
    public class SSMG_CHAT_PARTY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAT_PARTY"/> class.
        /// </summary>
        public SSMG_CHAT_PARTY()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)1031;
        }

        /// <summary>
        /// Sets the Sender.
        /// </summary>
        public string Sender
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[bytes.Length + 4];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)2);
                this.PutBytes(bytes, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Content.
        /// </summary>
        public string Content
        {
            set
            {
                byte num = this.GetByte((ushort)2);
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[bytes.Length + this.data.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)(3U + (uint)num));
                this.PutBytes(bytes, (ushort)(4U + (uint)num));
            }
        }
    }
}
