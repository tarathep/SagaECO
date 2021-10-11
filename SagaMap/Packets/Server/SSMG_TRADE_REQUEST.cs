namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_REQUEST" />.
    /// </summary>
    public class SSMG_TRADE_REQUEST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_REQUEST"/> class.
        /// </summary>
        public SSMG_TRADE_REQUEST()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)2572;
        }

        /// <summary>
        /// Sets the Name.
        /// </summary>
        public string Name
        {
            set
            {
                value += "\0";
                byte[] bytes = Global.Unicode.GetBytes(value);
                byte[] numArray = new byte[3 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)2);
                this.PutBytes(bytes, (ushort)3);
            }
        }
    }
}
