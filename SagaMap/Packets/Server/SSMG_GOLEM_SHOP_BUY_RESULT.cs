namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_BUY_RESULT" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_BUY_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_BUY_RESULT"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_BUY_RESULT()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)6190;
        }

        /// <summary>
        /// Sets the BoughtItems.
        /// </summary>
        public Dictionary<uint, GolemShopItem> BoughtItems
        {
            set
            {
                this.data = new byte[12 + value.Count * 6];
                this.offset = (ushort)2;
                this.ID = (ushort)6190;
                uint s = 0;
                this.PutByte((byte)value.Count, (ushort)10);
                this.PutByte((byte)value.Count, (ushort)(11 + value.Count * 4));
                int num = 0;
                foreach (uint key in value.Keys)
                {
                    this.PutUInt(key, (ushort)(11 + num * 4));
                    this.PutUShort(value[key].Count, (ushort)(12 + value.Count * 4 + num * 2));
                    s += (uint)value[key].Count * value[key].Price;
                    ++num;
                }
                this.PutUInt(s, (ushort)6);
            }
        }
    }
}
