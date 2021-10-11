namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_RESERVE_LIST" />.
    /// </summary>
    public class SSMG_SKILL_RESERVE_LIST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_RESERVE_LIST"/> class.
        /// </summary>
        public SSMG_SKILL_RESERVE_LIST()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)558;
        }

        /// <summary>
        /// Sets the Skills.
        /// </summary>
        public List<SagaDB.Skill.Skill> Skills
        {
            set
            {
                this.data = new byte[4 + 3 * value.Count];
                this.ID = (ushort)558;
                this.PutByte((byte)value.Count, (ushort)2);
                this.PutByte((byte)value.Count, (ushort)(3 + 2 * value.Count));
                for (int index = 0; index < value.Count; ++index)
                {
                    this.PutUShort((ushort)value[index].ID, (ushort)(3 + 2 * index));
                    this.PutByte(value[index].Level, (ushort)(4 + 2 * value.Count + index));
                }
            }
        }
    }
}
