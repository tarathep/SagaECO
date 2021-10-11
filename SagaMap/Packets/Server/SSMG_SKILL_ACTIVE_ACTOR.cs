namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_ACTIVE_ACTOR" />.
    /// </summary>
    public class SSMG_SKILL_ACTIVE_ACTOR : Packet
    {
        /// <summary>
        /// Defines the combo.
        /// </summary>
        private byte combo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_ACTIVE_ACTOR"/> class.
        /// </summary>
        /// <param name="combo">The combo<see cref="byte"/>.</param>
        public SSMG_SKILL_ACTIVE_ACTOR(byte combo)
        {
            this.data = new byte[11 + 4 * (int)combo + 6 * (int)combo + 4 * (int)combo];
            this.offset = (ushort)2;
            this.ID = (ushort)5040;
            this.combo = combo;
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the AffectedID.
        /// </summary>
        public List<SagaDB.Actor.Actor> AffectedID
        {
            set
            {
                this.PutByte(this.combo, (ushort)6);
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutUInt(value[index].ActorID, (ushort)(7 + index * 4));
            }
        }

        /// <summary>
        /// The SetHP.
        /// </summary>
        /// <param name="hp">The hp<see cref="List{int}"/>.</param>
        public void SetHP(List<int> hp)
        {
            this.PutByte(this.combo, (ushort)(7 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort((short)hp[index], (ushort)(8 + (int)this.combo * 4 + index * 2));
        }

        /// <summary>
        /// The SetMP.
        /// </summary>
        /// <param name="mp">The mp<see cref="List{int}"/>.</param>
        public void SetMP(List<int> mp)
        {
            this.PutByte(this.combo, (ushort)(8 + (int)this.combo * 4 + (int)this.combo * 2));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort((short)mp[index], (ushort)(9 + (int)this.combo * 4 + (int)this.combo * 2 + index * 2));
        }

        /// <summary>
        /// The SetSP.
        /// </summary>
        /// <param name="sp">The sp<see cref="List{int}"/>.</param>
        public void SetSP(List<int> sp)
        {
            this.PutByte(this.combo, (ushort)(9 + (int)this.combo * 4 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutShort((short)sp[index], (ushort)(10 + (int)this.combo * 4 + (int)this.combo * 4 + index * 2));
        }

        /// <summary>
        /// The AttackFlag.
        /// </summary>
        /// <param name="flag">The flag<see cref="List{AttackFlag}"/>.</param>
        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(this.combo, (ushort)(10 + (int)this.combo * 4 + (int)this.combo * 6));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutUInt((uint)flag[index], (ushort)(11 + (int)this.combo * 4 + (int)this.combo * 6 + index * 4));
        }
    }
}
