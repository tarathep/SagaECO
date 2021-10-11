namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_PET_SELECT" />.
    /// </summary>
    public class SSMG_NPC_PET_SELECT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_PET_SELECT"/> class.
        /// </summary>
        public SSMG_NPC_PET_SELECT()
        {
            this.data = new byte[9];
            this.offset = (ushort)2;
            this.ID = (ushort)4810;
        }

        /// <summary>
        /// Sets the Type.
        /// </summary>
        public SSMG_NPC_PET_SELECT.SelType Type
        {
            set
            {
                this.PutInt((int)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Pets.
        /// </summary>
        public List<SagaDB.Item.Item> Pets
        {
            set
            {
                byte[][] numArray1 = new byte[value.Count][];
                uint[] numArray2 = new uint[value.Count];
                byte num = 0;
                for (int index = 0; index < numArray1.Length; ++index)
                {
                    numArray1[index] = Global.Unicode.GetBytes(value[index].BaseData.name);
                    num += (byte)(numArray1[index].Length + 1);
                    numArray2[index] = value[index].Slot;
                }
                byte[] numArray3 = new byte[9 + value.Count * 8 + (int)num];
                this.data.CopyTo((Array)numArray3, 0);
                this.data = numArray3;
                this.offset = (ushort)6;
                this.PutByte((byte)numArray2.Length);
                for (int index = 0; index < value.Count; ++index)
                    this.PutUInt(numArray2[index]);
                this.PutByte((byte)numArray1.Length);
                for (int index = 0; index < value.Count; ++index)
                {
                    this.PutByte((byte)numArray1[index].Length);
                    this.PutBytes(numArray1[index]);
                }
                this.PutByte((byte)value.Count);
            }
        }

        /// <summary>
        /// Defines the SelType.
        /// </summary>
        public enum SelType
        {
            /// <summary>
            /// Defines the Recover.
            /// </summary>
            Recover,

            /// <summary>
            /// Defines the Rebirth.
            /// </summary>
            Rebirth,

            /// <summary>
            /// Defines the None.
            /// </summary>
            None,
        }
    }
}
