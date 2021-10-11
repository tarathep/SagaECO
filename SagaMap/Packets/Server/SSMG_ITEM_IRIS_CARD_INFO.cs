namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_IRIS_CARD_INFO" />.
    /// </summary>
    public class SSMG_ITEM_IRIS_CARD_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_IRIS_CARD_INFO"/> class.
        /// </summary>
        public SSMG_ITEM_IRIS_CARD_INFO()
        {
            this.data = new byte[9];
            this.offset = (ushort)2;
            this.ID = (ushort)2517;
        }

        /// <summary>
        /// Sets the Item.
        /// </summary>
        public SagaDB.Item.Item Item
        {
            set
            {
                this.PutUInt(value.Slot, (ushort)2);
                byte[] numArray = new byte[9 + 4 * (int)value.CurrentSlot];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte(value.CurrentSlot, (ushort)6);
                for (int index = 0; index < (int)value.CurrentSlot; ++index)
                {
                    if (index < value.Cards.Count)
                        this.PutUInt(value.Cards[index].ID);
                    else
                        this.PutUInt(0U);
                }
                this.PutShort((short)value.CurrentSlot);
            }
        }
    }
}
