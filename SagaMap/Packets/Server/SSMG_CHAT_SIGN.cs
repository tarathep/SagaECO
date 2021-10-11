namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAT_SIGN" />.
    /// </summary>
    public class SSMG_CHAT_SIGN : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAT_SIGN"/> class.
        /// </summary>
        public SSMG_CHAT_SIGN()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)1051;
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Message.
        /// </summary>
        public string Message
        {
            set
            {
                if (value != "")
                {
                    if (value.Substring(value.Length - 1, 1) != "\0")
                        value += "\0";
                    byte[] bytes = Global.Unicode.GetBytes(value);
                    byte[] numArray = new byte[bytes.Length + 7];
                    this.data.CopyTo((Array)numArray, 0);
                    this.data = numArray;
                    this.PutByte((byte)bytes.Length, (ushort)6);
                    this.PutBytes(bytes, (ushort)7);
                }
                else
                    this.PutByte((byte)1, (ushort)6);
            }
        }
    }
}
