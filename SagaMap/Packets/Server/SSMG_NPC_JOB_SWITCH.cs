namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_JOB_SWITCH" />.
    /// </summary>
    public class SSMG_NPC_JOB_SWITCH : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_JOB_SWITCH"/> class.
        /// </summary>
        public SSMG_NPC_JOB_SWITCH()
        {
            this.data = new byte[16];
            this.offset = (ushort)2;
            this.ID = (ushort)700;
        }

        /// <summary>
        /// Sets the Job.
        /// </summary>
        public PC_JOB Job
        {
            set
            {
                this.PutUShort((ushort)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the LevelReduced.
        /// </summary>
        public byte LevelReduced
        {
            set
            {
                this.PutByte(value, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the Level.
        /// </summary>
        public byte Level
        {
            set
            {
                this.PutByte(value, (ushort)5);
            }
        }

        /// <summary>
        /// Sets the LevelItem.
        /// </summary>
        public uint LevelItem
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the ItemCount.
        /// </summary>
        public uint ItemCount
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the PossibleReserveSkills.
        /// </summary>
        public ushort PossibleReserveSkills
        {
            set
            {
                this.PutUShort(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the PossibleSkills.
        /// </summary>
        public List<SagaDB.Skill.Skill> PossibleSkills
        {
            set
            {
                byte[] numArray = new byte[18 + 3 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count, (ushort)16);
                this.PutByte((byte)value.Count, (ushort)(17 + 2 * value.Count));
                int num = 0;
                foreach (SagaDB.Skill.Skill skill in value)
                {
                    this.PutUShort((ushort)skill.ID, (ushort)(17 + 2 * num));
                    this.PutByte(skill.Level, (ushort)(18 + 2 * value.Count + num));
                    ++num;
                }
            }
        }
    }
}
