namespace SagaMap.Packets.Server
{
    using SagaDB.Npc;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SHOP_BUY" />.
    /// </summary>
    public class SSMG_NPC_SHOP_BUY : Packet
    {
        /// <summary>
        /// Defines the num.
        /// </summary>
        private int num;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SHOP_BUY"/> class.
        /// </summary>
        /// <param name="num">The num<see cref="int"/>.</param>
        public SSMG_NPC_SHOP_BUY(int num)
        {
            this.data = new byte[16 + num * 4];
            this.offset = (ushort)2;
            this.ID = (ushort)1555;
            this.num = num;
        }

        /// <summary>
        /// Sets the Rate.
        /// </summary>
        public uint Rate
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Goods.
        /// </summary>
        public List<uint> Goods
        {
            set
            {
                if (value.Count <= 12)
                    this.PutByte((byte)value.Count, (ushort)6);
                else
                    this.PutByte((byte)12, (ushort)6);
                for (int index = 0; index < value.Count; ++index)
                    this.PutUInt(value[index], (ushort)(7 + index * 4));
            }
        }

        /// <summary>
        /// Sets the Gold.
        /// </summary>
        public uint Gold
        {
            set
            {
                this.PutUInt(value, (ushort)(7 + this.num * 4));
            }
        }

        /// <summary>
        /// Sets the Bank.
        /// </summary>
        public uint Bank
        {
            set
            {
                this.PutUInt(value, (ushort)(11 + this.num * 4));
            }
        }

        /// <summary>
        /// Sets the Type.
        /// </summary>
        public ShopType Type
        {
            set
            {
                this.PutByte((byte)value, (ushort)(15 + this.num * 4));
            }
        }
    }
}
