namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_ACTIVE_FLOOR" />.
    /// </summary>
    public class SSMG_SKILL_ACTIVE_FLOOR : Packet
    {
        /// <summary>
        /// Defines the combo.
        /// </summary>
        private byte combo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_ACTIVE_FLOOR"/> class.
        /// </summary>
        /// <param name="combo">The combo<see cref="byte"/>.</param>
        public SSMG_SKILL_ACTIVE_FLOOR(byte combo)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                this.data = new byte[18 + 4 * (int)combo + 6 * (int)combo + 4 * (int)combo];
            if (Singleton<Configuration>.Instance.Version >= Version.Saga9_2)
                this.data = new byte[18 + 4 * (int)combo + 12 * (int)combo + 4 * (int)combo];
            this.offset = (ushort)2;
            this.ID = (ushort)5005;
            this.combo = combo;
            this.PutByte((byte)1, (ushort)4);
        }

        /// <summary>
        /// Sets the SkillID.
        /// </summary>
        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, (ushort)2);
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
                {
                    if (value[index] != null)
                        this.PutUInt(value[index].ActorID, (ushort)(11 + index * 4));
                    else
                        this.PutUInt(uint.MaxValue, (ushort)(11 + index * 4));
                }
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)(11 + (int)this.combo * 4));
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)(12 + (int)this.combo * 4));
            }
        }

        /// <summary>
        /// The SetHP.
        /// </summary>
        /// <param name="hp">The hp<see cref="List{int}"/>.</param>
        public void SetHP(List<int> hp)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.PutByte(this.combo, (ushort)(13 + (int)this.combo * 4));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutShort((short)hp[index], (ushort)(14 + (int)this.combo * 4 + index * 2));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(13 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutInt(hp[index], (ushort)(14 + (int)this.combo * 4 + index * 4));
        }

        /// <summary>
        /// The SetMP.
        /// </summary>
        /// <param name="mp">The mp<see cref="List{int}"/>.</param>
        public void SetMP(List<int> mp)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.PutByte(this.combo, (ushort)(14 + (int)this.combo * 4 + (int)this.combo * 2));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutShort((short)mp[index], (ushort)(15 + (int)this.combo * 4 + (int)this.combo * 2 + index * 2));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(14 + (int)this.combo * 4 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutInt(mp[index], (ushort)(15 + (int)this.combo * 4 + (int)this.combo * 4 + index * 4));
        }

        /// <summary>
        /// The SetSP.
        /// </summary>
        /// <param name="sp">The sp<see cref="List{int}"/>.</param>
        public void SetSP(List<int> sp)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.PutByte(this.combo, (ushort)(15 + (int)this.combo * 4 + (int)this.combo * 4));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutShort((short)sp[index], (ushort)(16 + (int)this.combo * 4 + (int)this.combo * 4 + index * 2));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(15 + (int)this.combo * 4 + (int)this.combo * 8));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutInt(sp[index], (ushort)(16 + (int)this.combo * 4 + (int)this.combo * 8 + index * 4));
        }

        /// <summary>
        /// The AttackFlag.
        /// </summary>
        /// <param name="flag">The flag<see cref="List{AttackFlag}"/>.</param>
        public void AttackFlag(List<AttackFlag> flag)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.PutByte(this.combo, (ushort)(16 + (int)this.combo * 4 + (int)this.combo * 6));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutUInt((uint)flag[index], (ushort)(17 + (int)this.combo * 4 + (int)this.combo * 6 + index * 4));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(16 + (int)this.combo * 4 + (int)this.combo * 12));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutUInt((uint)flag[index], (ushort)(17 + (int)this.combo * 4 + (int)this.combo * 12 + index * 4));
        }

        /// <summary>
        /// Sets the SkillLv.
        /// </summary>
        public byte SkillLv
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutByte(value, (ushort)(17 + (int)this.combo * 4 + (int)this.combo * 6 + (int)this.combo * 4));
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutByte(value, (ushort)(17 + (int)this.combo * 4 + (int)this.combo * 12 + (int)this.combo * 4));
            }
        }
    }
}
