namespace SagaMap.Packets.Server
{
    using SagaDB.BBS;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_BBS_PAGE_INFO" />.
    /// </summary>
    public class SSMG_COMMUNITY_BBS_PAGE_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_BBS_PAGE_INFO"/> class.
        /// </summary>
        public SSMG_COMMUNITY_BBS_PAGE_INFO()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)6921;
        }

        /// <summary>
        /// Sets the Posts.
        /// </summary>
        public List<Post> Posts
        {
            set
            {
                byte[][] numArray1 = new byte[value.Count][];
                byte[][] numArray2 = new byte[value.Count][];
                byte[][] numArray3 = new byte[value.Count][];
                int index1 = 0;
                int num = 0;
                foreach (Post post in value)
                {
                    numArray1[index1] = Global.Unicode.GetBytes(post.Name + "\0");
                    numArray2[index1] = Global.Unicode.GetBytes(post.Title + "\0");
                    numArray3[index1] = Global.Unicode.GetBytes(post.Content + "\0");
                    if (numArray3[index1].Length >= 253)
                        num += 4;
                    num += numArray1[index1].Length + numArray2[index1].Length + numArray3[index1].Length + 3;
                    ++index1;
                }
                byte[] numArray4 = new byte[10 + num + 4 * value.Count];
                this.data.CopyTo((Array)numArray4, 0);
                this.data = numArray4;
                this.PutByte((byte)value.Count, (ushort)6);
                for (int index2 = 0; index2 < value.Count; ++index2)
                    this.PutUInt((uint)(value[index2].Date - new DateTime(1970, 1, 1)).TotalSeconds);
                this.PutByte((byte)value.Count);
                for (int index2 = 0; index2 < value.Count; ++index2)
                {
                    this.PutByte((byte)numArray1[index2].Length);
                    this.PutBytes(numArray1[index2]);
                }
                this.PutByte((byte)value.Count);
                for (int index2 = 0; index2 < value.Count; ++index2)
                {
                    this.PutByte((byte)numArray2[index2].Length);
                    this.PutBytes(numArray2[index2]);
                }
                this.PutByte((byte)value.Count);
                for (int index2 = 0; index2 < value.Count; ++index2)
                {
                    if (numArray3[index2].Length < 253)
                    {
                        this.PutByte((byte)numArray3[index2].Length);
                        this.PutBytes(numArray3[index2]);
                    }
                    else
                    {
                        this.PutByte((byte)253);
                        this.PutInt(numArray3[index2].Length);
                        this.PutBytes(numArray3[index2]);
                    }
                }
            }
        }
    }
}
