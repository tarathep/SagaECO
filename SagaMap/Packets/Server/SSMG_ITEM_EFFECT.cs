namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_EFFECT" />.
    /// </summary>
    public class SSMG_ITEM_EFFECT : Packet
    {
        /// <summary>
        /// Defines the combo.
        /// </summary>
        private byte combo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_EFFECT"/> class.
        /// </summary>
        /// <param name="combo">The combo<see cref="byte"/>.</param>
        public SSMG_ITEM_EFFECT(byte combo)
        {
            this.data = new byte[21 + 4 * (int)combo + 6 * (int)combo + 4 * (int)combo];
            this.offset = (ushort)2;
            this.ID = (ushort)2504;
            this.combo = combo;
            this.PutByte((byte)1, (ushort)4);
        }

        /// <summary>
        /// Sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the AffectedID.
        /// </summary>
        public List<SagaDB.Actor.Actor> AffectedID
        {
            set
            {
                this.PutByte(this.combo, (ushort)10);
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutUInt(value[index].ActorID, (ushort)(11 + index * 4));
            }
        }

        /// <summary>
        /// The SetHP.
        /// </summary>
        /// <param name="hp">The hp<see cref="short[]"/>.</param>
        public void SetHP(short[] hp)
        {
            this.PutByte(this.combo, (ushort)(11 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort(hp[index], (ushort)(12 + (int)this.combo * 4 + index * 2));
        }

        /// <summary>
        /// The SetMP.
        /// </summary>
        /// <param name="mp">The mp<see cref="short[]"/>.</param>
        public void SetMP(short[] mp)
        {
            this.PutByte(this.combo, (ushort)(12 + (int)this.combo * 4 + (int)this.combo * 2));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort(mp[index], (ushort)(13 + (int)this.combo * 4 + (int)this.combo * 2 + index * 2));
        }

        /// <summary>
        /// The SetSP.
        /// </summary>
        /// <param name="sp">The sp<see cref="short[]"/>.</param>
        public void SetSP(short[] sp)
        {
            this.PutByte(this.combo, (ushort)(13 + (int)this.combo * 4 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort(sp[index], (ushort)(14 + (int)this.combo * 4 + (int)this.combo * 4 + index * 2));
        }

        /// <summary>
        /// The AttackFlag.
        /// </summary>
        /// <param name="flag">The flag<see cref="List{AttackFlag}"/>.</param>
        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(this.combo, (ushort)(14 + (int)this.combo * 4 + (int)this.combo * 6));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutUInt((uint)flag[index], (ushort)(15 + (int)this.combo * 4 + (int)this.combo * 6 + index * 4));
        }
    }
}
