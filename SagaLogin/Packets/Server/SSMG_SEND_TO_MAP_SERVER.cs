namespace SagaLogin.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_SEND_TO_MAP_SERVER" />.
    /// </summary>
    public class SSMG_SEND_TO_MAP_SERVER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SEND_TO_MAP_SERVER"/> class.
        /// </summary>
        public SSMG_SEND_TO_MAP_SERVER()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)51;
        }

        /// <summary>
        /// Sets the ServerID.
        /// </summary>
        public byte ServerID
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the IP.
        /// </summary>
        public string IP
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value);
                byte b = (byte)(bytes.Length + 1);
                byte[] numArray = new byte[(int)b + 8];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte(b, (ushort)3);
                this.PutBytes(bytes, (ushort)4);
            }
        }

        /// <summary>
        /// The GetDataOffset.
        /// </summary>
        /// <returns>The <see cref="byte"/>.</returns>
        private byte GetDataOffset()
        {
            return (byte)(4U + (uint)this.GetByte((ushort)3));
        }

        /// <summary>
        /// Sets the Port.
        /// </summary>
        public int Port
        {
            set
            {
                this.PutInt(value, (ushort)this.GetDataOffset());
            }
        }
    }
}
