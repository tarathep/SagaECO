namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_COMBO_ATTACK_RESULT" />.
    /// </summary>
    public class SSMG_SKILL_COMBO_ATTACK_RESULT : Packet
    {
        /// <summary>
        /// Defines the combo.
        /// </summary>
        private byte combo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_COMBO_ATTACK_RESULT"/> class.
        /// </summary>
        /// <param name="combo">The combo<see cref="byte"/>.</param>
        public SSMG_SKILL_COMBO_ATTACK_RESULT(byte combo)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                this.data = new byte[27 + 4 * (int)combo + 6 * (int)combo + 4 * (int)combo];
            if (Singleton<Configuration>.Instance.Version >= Version.Saga9_2)
                this.data = new byte[27 + 4 * (int)combo + 12 * (int)combo + 4 * (int)combo];
            this.offset = (ushort)2;
            this.ID = (ushort)4002;
            this.combo = combo;
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                this.PutByte((byte)1, (ushort)(12 + (int)combo * 6 + (int)combo * 8));
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte((byte)1, (ushort)(12 + (int)combo * 12 + (int)combo * 8));
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
        /// Sets the TargetID.
        /// </summary>
        public List<SagaDB.Actor.Actor> TargetID
        {
            set
            {
                this.PutByte(this.combo, (ushort)6);
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutUInt(value[index].ActorID, (ushort)(7 + index * 4));
            }
        }

        /// <summary>
        /// Sets the AttackType.
        /// </summary>
        public ATTACK_TYPE AttackType
        {
            set
            {
                this.PutByte((byte)value, (ushort)(7 + (int)this.combo * 4));
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
                this.PutByte(this.combo, (ushort)(8 + (int)this.combo * 4));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutShort((short)hp[index], (ushort)(9 + (int)this.combo * 4 + index * 2));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(8 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutInt(hp[index], (ushort)(9 + (int)this.combo * 4 + index * 4));
        }

        /// <summary>
        /// The SetMP.
        /// </summary>
        /// <param name="mp">The mp<see cref="List{int}"/>.</param>
        public void SetMP(List<int> mp)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.PutByte(this.combo, (ushort)(9 + (int)this.combo * 4 + (int)this.combo * 2));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutShort((short)mp[index], (ushort)(10 + (int)this.combo * 4 + (int)this.combo * 2 + index * 2));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(9 + (int)this.combo * 4 + (int)this.combo * 4));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutInt(mp[index], (ushort)(10 + (int)this.combo * 4 + (int)this.combo * 4 + index * 4));
        }

        /// <summary>
        /// The SetSP.
        /// </summary>
        /// <param name="sp">The sp<see cref="List{int}"/>.</param>
        public void SetSP(List<int> sp)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.PutByte(this.combo, (ushort)(10 + (int)this.combo * 4 + (int)this.combo * 4));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutShort((short)sp[index], (ushort)(11 + (int)this.combo * 4 + (int)this.combo * 4 + index * 2));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(10 + (int)this.combo * 4 + (int)this.combo * 8));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutInt(sp[index], (ushort)(11 + (int)this.combo * 4 + (int)this.combo * 8 + index * 4));
        }

        /// <summary>
        /// The AttackFlag.
        /// </summary>
        /// <param name="flag">The flag<see cref="List{AttackFlag}"/>.</param>
        public void AttackFlag(List<AttackFlag> flag)
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.PutByte(this.combo, (ushort)(11 + (int)this.combo * 4 + (int)this.combo * 6));
                for (int index = 0; index < (int)this.combo; ++index)
                    this.PutUInt((uint)flag[index], (ushort)(12 + (int)this.combo * 4 + (int)this.combo * 6 + index * 4));
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.PutByte(this.combo, (ushort)(11 + (int)this.combo * 4 + (int)this.combo * 12));
            for (int index = 0; index < (int)this.combo; ++index)
                this.PutUInt((uint)flag[index], (ushort)(12 + (int)this.combo * 4 + (int)this.combo * 12 + index * 4));
        }

        /// <summary>
        /// Sets the Delay.
        /// </summary>
        public uint Delay
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutUInt(value, (ushort)(14 + (int)this.combo * 4 + (int)this.combo * 6 + (int)this.combo * 4));
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutUInt(value, (ushort)(14 + (int)this.combo * 4 + (int)this.combo * 12 + (int)this.combo * 4));
            }
        }

        /// <summary>
        /// Sets the Unknown.
        /// </summary>
        public uint Unknown
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutUInt(value, (ushort)(18 + (int)this.combo * 6 + (int)this.combo * 8));
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutUInt(value, (ushort)(18 + (int)this.combo * 12 + (int)this.combo * 8));
            }
        }
    }
}
