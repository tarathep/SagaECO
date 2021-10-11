namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_BUY_SETUP" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_BUY_SETUP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_BUY_SETUP"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_BUY_SETUP()
        {
            this.data = new byte[14];
            this.offset = (ushort)2;
            this.ID = (ushort)6171;
            this.MaxItemCount = (byte)32;
        }

        /// <summary>
        /// Sets the Unknown.
        /// </summary>
        public uint Unknown
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the MaxItemCount.
        /// </summary>
        public byte MaxItemCount
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the BuyLimit.
        /// </summary>
        public uint BuyLimit
        {
            set
            {
                this.PutUInt(value, (ushort)7);
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
                byte[] numArray = new byte[14 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)11);
                this.PutBytes(bytes, (ushort)12);
            }
        }
    }
}
