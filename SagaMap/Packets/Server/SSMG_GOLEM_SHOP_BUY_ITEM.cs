namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_BUY_ITEM" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_BUY_ITEM : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_BUY_ITEM"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_BUY_ITEM()
        {
            this.data = new byte[329];
            this.offset = (ushort)2;
            this.ID = (ushort)6182;
            this.PutByte((byte)32, (ushort)2);
            this.PutByte((byte)32, (ushort)131);
            this.PutByte((byte)32, (ushort)196);
        }

        /// <summary>
        /// Sets the Items.
        /// </summary>
        public Dictionary<uint, GolemShopItem> Items
        {
            set
            {
                int num = 0;
                foreach (GolemShopItem golemShopItem in value.Values)
                {
                    this.PutUInt(golemShopItem.ItemID, (ushort)(3 + num * 4));
                    this.PutUShort(golemShopItem.Count, (ushort)(132 + num * 2));
                    this.PutUInt(golemShopItem.Price, (ushort)(197 + num * 4));
                    ++num;
                }
            }
        }

        /// <summary>
        /// Sets the BuyLimit.
        /// </summary>
        public uint BuyLimit
        {
            set
            {
                this.PutUInt(value, (ushort)325);
            }
        }
    }
}
