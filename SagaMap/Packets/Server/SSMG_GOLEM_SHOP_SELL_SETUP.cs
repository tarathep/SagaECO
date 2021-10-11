namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_SELL_SETUP" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_SELL_SETUP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_SELL_SETUP"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_SELL_SETUP()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)6121;
            this.MaxItemCount = (byte)100;
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
        /// Sets the Comment.
        /// </summary>
        public string Comment
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[10 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)7);
                this.PutBytes(bytes, (ushort)8);
            }
        }
    }
}
