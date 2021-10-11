namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_VSHOP_INFO" />.
    /// </summary>
    public class SSMG_VSHOP_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_VSHOP_INFO"/> class.
        /// </summary>
        public SSMG_VSHOP_INFO()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)1616;
        }

        /// <summary>
        /// Sets the Point.
        /// </summary>
        public uint Point
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Comment.
        /// </summary>
        public string Comment
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[11 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)10);
                this.PutBytes(bytes, (ushort)11);
            }
        }
    }
}
