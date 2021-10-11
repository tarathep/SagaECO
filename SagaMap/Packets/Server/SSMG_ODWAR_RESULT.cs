namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ODWAR_RESULT" />.
    /// </summary>
    public class SSMG_ODWAR_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ODWAR_RESULT"/> class.
        /// </summary>
        public SSMG_ODWAR_RESULT()
        {
            this.data = new byte[16];
            this.offset = (ushort)2;
            this.ID = (ushort)7045;
            this.PutByte((byte)1, (ushort)2);
        }

        /// <summary>
        /// Sets a value indicating whether Win.
        /// </summary>
        public bool Win
        {
            set
            {
                if (!value)
                    return;
                this.PutByte((byte)1, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the EXP.
        /// </summary>
        public uint EXP
        {
            set
            {
                this.PutUInt(value, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the JEXP.
        /// </summary>
        public uint JEXP
        {
            set
            {
                this.PutUInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the CP.
        /// </summary>
        public uint CP
        {
            set
            {
                this.PutUInt(value, (ushort)12);
            }
        }
    }
}
