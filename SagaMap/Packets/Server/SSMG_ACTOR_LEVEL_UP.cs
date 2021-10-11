namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_LEVEL_UP" />.
    /// </summary>
    public class SSMG_ACTOR_LEVEL_UP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_LEVEL_UP"/> class.
        /// </summary>
        public SSMG_ACTOR_LEVEL_UP()
        {
            if (Singleton<Configuration>.Instance.Version >= Version.Saga10)
                this.data = new byte[41];
            else
                this.data = new byte[25];
            this.offset = (ushort)2;
            this.ID = (ushort)575;
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
        /// Sets the Level.
        /// </summary>
        public byte Level
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the JobLevel.
        /// </summary>
        public byte JobLevel
        {
            set
            {
                this.PutByte(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the ExpPerc.
        /// </summary>
        public int ExpPerc
        {
            set
            {
                this.PutInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the JExpPerc.
        /// </summary>
        public int JExpPerc
        {
            set
            {
                this.PutInt(value, (ushort)12);
            }
        }

        /// <summary>
        /// Sets the Exp.
        /// </summary>
        public long Exp
        {
            set
            {
                this.PutLong(value, (ushort)16);
            }
        }

        /// <summary>
        /// Sets the JExp.
        /// </summary>
        public long JExp
        {
            set
            {
                this.PutLong(value, (ushort)24);
            }
        }

        /// <summary>
        /// Sets the StatusPoints.
        /// </summary>
        public short StatusPoints
        {
            set
            {
                this.PutShort(value, (ushort)32);
            }
        }

        /// <summary>
        /// Sets the SkillPoints.
        /// </summary>
        public short SkillPoints
        {
            set
            {
                this.PutShort(value, (ushort)34);
            }
        }

        /// <summary>
        /// Sets the SkillPoints2X.
        /// </summary>
        public short SkillPoints2X
        {
            set
            {
                this.PutShort(value, (ushort)36);
            }
        }

        /// <summary>
        /// Sets the SkillPoints2T.
        /// </summary>
        public short SkillPoints2T
        {
            set
            {
                this.PutShort(value, (ushort)48);
            }
        }

        /// <summary>
        /// Sets the LvType.
        /// </summary>
        public byte LvType
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version >= Version.Saga10)
                    this.PutByte(value, (ushort)40);
                else
                    this.PutByte(value, (ushort)24);
            }
        }
    }
}
