namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_USE" />.
    /// </summary>
    public class SSMG_ITEM_USE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_USE"/> class.
        /// </summary>
        public SSMG_ITEM_USE()
        {
            this.data = new byte[25];
            this.offset = (ushort)2;
            this.ID = (ushort)2501;
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
        /// Sets the result.
        /// </summary>
        public short result
        {
            set
            {
                this.PutShort(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Form_ActorId.
        /// </summary>
        public uint Form_ActorId
        {
            set
            {
                this.PutUInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the Cast.
        /// </summary>
        public uint Cast
        {
            set
            {
                this.PutUInt(value, (ushort)12);
            }
        }

        /// <summary>
        /// Sets the To_ActorID.
        /// </summary>
        public uint To_ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)16);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)20);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)21);
            }
        }

        /// <summary>
        /// Sets the SkillID.
        /// </summary>
        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, (ushort)22);
            }
        }

        /// <summary>
        /// Sets the SkillLV.
        /// </summary>
        public byte SkillLV
        {
            set
            {
                this.PutByte(value, (ushort)24);
            }
        }
    }
}
