namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_STATUS" />.
    /// </summary>
    public class SSMG_TRADE_STATUS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_STATUS"/> class.
        /// </summary>
        public SSMG_TRADE_STATUS()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)2585;
            this.PutByte((byte)1, (ushort)2);
            this.PutByte((byte)1, (ushort)4);
        }

        /// <summary>
        /// Sets a value indicating whether Confirm.
        /// </summary>
        public bool Confirm
        {
            set
            {
                if (value)
                    this.PutByte((byte)0, (ushort)3);
                else
                    this.PutByte(byte.MaxValue, (ushort)3);
            }
        }

        /// <summary>
        /// Sets a value indicating whether Perform.
        /// </summary>
        public bool Perform
        {
            set
            {
                if (value)
                    this.PutByte((byte)0, (ushort)5);
                else
                    this.PutByte(byte.MaxValue, (ushort)5);
            }
        }
    }
}
