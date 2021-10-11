namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_ACTIVE_SELF" />.
    /// </summary>
    public class SSMG_ITEM_ACTIVE_SELF : Packet
    {
        /// <summary>
        /// Defines the combo.
        /// </summary>
        private byte combo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_ACTIVE_SELF"/> class.
        /// </summary>
        /// <param name="combo">The combo<see cref="byte"/>.</param>
        public SSMG_ITEM_ACTIVE_SELF(byte combo)
        {
            this.data = new byte[17 + 4 * (int)combo + 6 * (int)combo + 4 * (int)combo];
            this.offset = (ushort)2;
            this.ID = (ushort)2504;
            this.combo = combo;
            this.PutByte((byte)1, (ushort)6);
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
                this.PutUInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the AffectedID.
        /// </summary>
        public List<SagaDB.Actor.Actor> AffectedID
        {
            set
            {
                this.PutByte(this.combo, (ushort)12);
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutUInt(value[index].ActorID, (ushort)(13 + index * 4));
            }
        }

        /// <summary>
        /// The SetHP.
        /// </summary>
        /// <param name="hp">The hp<see cref="List{int}"/>.</param>
        public void SetHP(List<int> hp)
        {
            this.PutByte(this.combo, (ushort)(13 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort((short)hp[index], (ushort)(14 + (int)this.combo * 4 + index * 2));
        }

        /// <summary>
        /// The SetMP.
        /// </summary>
        /// <param name="mp">The mp<see cref="List{int}"/>.</param>
        public void SetMP(List<int> mp)
        {
            this.PutByte(this.combo, (ushort)(14 + (int)this.combo * 4 + (int)this.combo * 2));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort((short)mp[index], (ushort)(15 + (int)this.combo * 4 + (int)this.combo * 2 + index * 2));
        }

        /// <summary>
        /// The SetSP.
        /// </summary>
        /// <param name="sp">The sp<see cref="List{int}"/>.</param>
        public void SetSP(List<int> sp)
        {
            this.PutByte(this.combo, (ushort)(15 + (int)this.combo * 4 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort((short)sp[index], (ushort)(16 + (int)this.combo * 4 + (int)this.combo * 4 + index * 2));
        }

        /// <summary>
        /// The AttackFlag.
        /// </summary>
        /// <param name="flag">The flag<see cref="List{AttackFlag}"/>.</param>
        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(this.combo, (ushort)(16 + (int)this.combo * 4 + (int)this.combo * 6));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutUInt((uint)flag[index], (ushort)(17 + (int)this.combo * 4 + (int)this.combo * 6 + index * 4));
        }
    }
}
