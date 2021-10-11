namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_ATTACK_RESULT" />.
    /// </summary>
    public class SSMG_SKILL_ATTACK_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_ATTACK_RESULT"/> class.
        /// </summary>
        public SSMG_SKILL_ATTACK_RESULT()
        {
            if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
            {
                this.data = new byte[29];
                this.offset = (ushort)2;
                this.ID = (ushort)4001;
            }
            if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                return;
            this.data = new byte[35];
            this.offset = (ushort)2;
            this.ID = (ushort)4001;
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
        public uint TargetID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the AttackType.
        /// </summary>
        public ATTACK_TYPE AttackType
        {
            set
            {
                this.PutByte((byte)value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the HP.
        /// </summary>
        public int HP
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutShort((short)value, (ushort)11);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutInt(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the MP.
        /// </summary>
        public int MP
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutShort((short)value, (ushort)13);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutInt(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the SP.
        /// </summary>
        public int SP
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutShort((short)value, (ushort)15);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutInt(value, (ushort)19);
            }
        }

        /// <summary>
        /// Sets the AttackFlag.
        /// </summary>
        public AttackFlag AttackFlag
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutUInt((uint)value, (ushort)17);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutUInt((uint)value, (ushort)23);
            }
        }

        /// <summary>
        /// Sets the Delay.
        /// </summary>
        public uint Delay
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version <= Version.Saga9)
                    this.PutUInt(value, (ushort)21);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutUInt(value, (ushort)27);
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
                    this.PutUInt(value, (ushort)25);
                if (Singleton<Configuration>.Instance.Version < Version.Saga9_2)
                    return;
                this.PutUInt(value, (ushort)31);
            }
        }
    }
}
