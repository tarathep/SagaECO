namespace SagaLogin.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAT_WHISPER_FAILED" />.
    /// </summary>
    public class SSMG_CHAT_WHISPER_FAILED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAT_WHISPER_FAILED"/> class.
        /// </summary>
        public SSMG_CHAT_WHISPER_FAILED()
        {
            this.data = new byte[7];
            this.ID = (ushort)202;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public uint Result
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Receiver.
        /// </summary>
        public string Receiver
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[bytes.Length + 7];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
            }
        }
    }
}
