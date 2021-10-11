namespace SagaMap.Packets.Server
{
    using SagaDB.Quests;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_LIST" />.
    /// </summary>
    public class SSMG_QUEST_LIST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_LIST"/> class.
        /// </summary>
        public SSMG_QUEST_LIST()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)6500;
        }

        /// <summary>
        /// Sets the Quests.
        /// </summary>
        public List<QuestInfo> Quests
        {
            set
            {
                byte[][] numArray1 = new byte[value.Count][];
                int num1 = 0;
                int num2 = 0;
                foreach (QuestInfo questInfo in value)
                {
                    byte[] bytes = Global.Unicode.GetBytes(questInfo.Name);
                    numArray1[num1++] = bytes;
                    num2 += bytes.Length + 1;
                }
                byte[] numArray2 = new byte[7 + value.Count * 10 + num2];
                this.data.CopyTo((Array)numArray2, 0);
                this.data = numArray2;
                this.PutByte((byte)value.Count, (ushort)2);
                this.PutByte((byte)value.Count, (ushort)(3 + value.Count * 4));
                this.PutByte((byte)value.Count, (ushort)(4 + value.Count * 5));
                this.PutByte((byte)value.Count, (ushort)(5 + num2 + value.Count * 5));
                this.PutByte((byte)value.Count, (ushort)(6 + num2 + value.Count * 9));
                int index = 0;
                int num3 = 0;
                foreach (QuestInfo questInfo in value)
                {
                    this.PutUInt(questInfo.ID, (ushort)(3 + index * 4));
                    this.PutByte((byte)questInfo.QuestType, (ushort)(4 + value.Count * 4 + index));
                    int length = (int)(byte)numArray1[index].Length;
                    int num4 = 5;
                    int num5 = num3;
                    int num6 = num5 + 1;
                    int num7 = (int)(ushort)(num4 + num5 + value.Count * 5);
                    this.PutByte((byte)length, (ushort)num7);
                    this.PutBytes(numArray1[index], (ushort)(5 + num6 + value.Count * 5));
                    num3 = num6 + numArray1[index].Length;
                    this.PutInt(questInfo.TimeLimit, (ushort)(6 + num2 + value.Count * 5 + index * 4));
                    this.PutByte(questInfo.MinLevel, (ushort)(7 + num2 + value.Count * 9 + index));
                    ++index;
                }
            }
        }
    }
}
