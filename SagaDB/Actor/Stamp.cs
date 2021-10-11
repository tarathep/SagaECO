namespace SagaDB.Actor
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Stamp" />.
    /// </summary>
    public class Stamp
    {
        /// <summary>
        /// Defines the stamps.
        /// </summary>
        private Dictionary<StampGenre, BitMask<StampSlot>> stamps = new Dictionary<StampGenre, BitMask<StampSlot>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Stamp"/> class.
        /// </summary>
        public Stamp()
        {
            this.stamps.Add(StampGenre.Special, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Pururu, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Field, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Coast, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Wild, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Cave, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Snow, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Colliery, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.Northan, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.IronSouth, new BitMask<StampSlot>());
            this.stamps.Add(StampGenre.SouthDungeon, new BitMask<StampSlot>());
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.stamps = (Dictionary<StampGenre, BitMask<StampSlot>>)null;
        }


        public BitMask<StampSlot> this[StampGenre genre]
        {
            get
            {
                return this.stamps[genre];
            }
        }
    }
}
