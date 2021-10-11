namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CSMG_TRADE_ITEM" />.
    /// </summary>
    public class CSMG_TRADE_ITEM : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_TRADE_ITEM"/> class.
        /// </summary>
        public CSMG_TRADE_ITEM()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the InventoryID.
        /// </summary>
        public List<uint> InventoryID
        {
            get
            {
                List<uint> uintList = new List<uint>();
                byte num = this.GetByte((ushort)2);
                for (int index = 0; index < (int)num; ++index)
                    uintList.Add(this.GetUInt((ushort)(3 + index * 4)));
                return uintList;
            }
        }

        /// <summary>
        /// Gets the Count.
        /// </summary>
        public List<ushort> Count
        {
            get
            {
                List<ushort> ushortList = new List<ushort>();
                byte num = this.GetByte((ushort)2);
                for (int index = 0; index < (int)num; ++index)
                    ushortList.Add(this.GetUShort((ushort)(4 + (int)num * 4 + index * 2)));
                return ushortList;
            }
        }

        /// <summary>
        /// Gets the Gold.
        /// </summary>
        public uint Gold
        {
            get
            {
                return this.GetUInt((ushort)(4 + (int)this.GetByte((ushort)2) * 6));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_TRADE_ITEM();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnTradeItem(this);
        }
    }
}
