namespace SagaMap.Packets.Server
{
    using SagaLib;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_RECRUIT" />.
    /// </summary>
    public class SSMG_COMMUNITY_RECRUIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_RECRUIT"/> class.
        /// </summary>
        public SSMG_COMMUNITY_RECRUIT()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)7071;
        }

        /// <summary>
        /// Sets the Type.
        /// </summary>
        public RecruitmentType Type
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Page.
        /// </summary>
        public int Page
        {
            set
            {
                this.PutInt(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the MaxPage.
        /// </summary>
        public int MaxPage
        {
            set
            {
                this.PutInt(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the Entries.
        /// </summary>
        public List<Recruitment> Entries
        {
            set
            {
                byte[] numArray1 = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo((Array)numArray1, 0);
                this.data = numArray1;
                this.PutByte((byte)value.Count, (ushort)11);
                for (int index = 0; index < value.Count; ++index)
                    this.PutUInt(value[index].Creator.CharID, (ushort)(12 + index * 4));
                byte[] numArray2 = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo((Array)numArray2, 0);
                this.data = numArray2;
                this.PutByte((byte)value.Count, (ushort)(12 + value.Count * 4));
                for (int index = 0; index < value.Count; ++index)
                    this.PutUInt(value[index].Creator.MapID, (ushort)(13 + value.Count * 4 + index * 4));
                byte[] numArray3 = new byte[this.data.Length + value.Count + 1];
                this.data.CopyTo((Array)numArray3, 0);
                this.data = numArray3;
                this.PutByte((byte)value.Count, (ushort)(13 + value.Count * 8));
                for (int index = 0; index < value.Count; ++index)
                {
                    if (value[index].Creator.Party != null)
                        this.PutByte((byte)value[index].Creator.Party.MemberCount, (ushort)(14 + value.Count * 8 + index));
                    else
                        this.PutByte((byte)0, (ushort)(14 + value.Count * 8 + index));
                }
                byte[][] numArray4 = new byte[value.Count][];
                int num1 = 0;
                for (int index = 0; index < value.Count; ++index)
                {
                    numArray4[index] = Global.Unicode.GetBytes(value[index].Creator.Name);
                    num1 += numArray4[index].Length + 1;
                }
                byte[] numArray5 = new byte[this.data.Length + num1 + 1];
                this.data.CopyTo((Array)numArray5, 0);
                this.data = numArray5;
                this.PutByte((byte)value.Count, (ushort)(14 + value.Count * 9));
                int num2 = 0;
                for (int index = 0; index < value.Count; ++index)
                {
                    this.PutByte((byte)numArray4[index].Length, (ushort)(15 + value.Count * 9 + num2));
                    this.PutBytes(numArray4[index], (ushort)(16 + value.Count * 9 + num2));
                    num2 += numArray4[index].Length + 1;
                }
                byte[][] numArray6 = new byte[value.Count][];
                int num3 = 0;
                for (int index = 0; index < value.Count; ++index)
                {
                    numArray6[index] = Global.Unicode.GetBytes(value[index].Title);
                    num3 += numArray6[index].Length + 1;
                }
                byte[] numArray7 = new byte[this.data.Length + num3 + 1];
                this.data.CopyTo((Array)numArray7, 0);
                this.data = numArray7;
                this.PutByte((byte)value.Count);
                for (int index = 0; index < value.Count; ++index)
                {
                    this.PutByte((byte)numArray6[index].Length);
                    this.PutBytes(numArray6[index]);
                }
                byte[][] numArray8 = new byte[value.Count][];
                int num4 = 0;
                for (int index = 0; index < value.Count; ++index)
                {
                    numArray8[index] = Global.Unicode.GetBytes(value[index].Content);
                    num4 += numArray8[index].Length + 1;
                }
                byte[] numArray9 = new byte[this.data.Length + num4 + 1];
                this.data.CopyTo((Array)numArray9, 0);
                this.data = numArray9;
                this.PutByte((byte)value.Count);
                for (int index = 0; index < value.Count; ++index)
                {
                    this.PutByte((byte)numArray8[index].Length);
                    this.PutBytes(numArray8[index]);
                }
            }
        }
    }
}
