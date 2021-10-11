namespace SagaLogin.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_WRP_LIST" />.
    /// </summary>
    public class SSMG_WRP_LIST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_WRP_LIST"/> class.
        /// </summary>
        public SSMG_WRP_LIST()
        {
            this.data = new byte[9];
            this.ID = (ushort)371;
        }

        /// <summary>
        /// Sets the RankingList.
        /// </summary>
        public List<ActorPC> RankingList
        {
            set
            {
                byte[][] numArray1 = new byte[value.Count][];
                byte[] numArray2 = new byte[value.Count];
                byte[] numArray3 = new byte[value.Count];
                byte[] numArray4 = new byte[value.Count];
                int[] numArray5 = new int[value.Count];
                uint[] numArray6 = new uint[value.Count];
                int index = 0;
                foreach (ActorPC actorPc in value)
                {
                    numArray1[index] = Global.Unicode.GetBytes(actorPc.Name);
                    numArray2[index] = actorPc.DominionLevel;
                    numArray3[index] = actorPc.DominionJobLevel;
                    numArray4[index] = (byte)actorPc.Job;
                    numArray5[index] = actorPc.WRP;
                    numArray6[index] = index != 0 ? (index >= 10 ? 0U : 1U) : 10U;
                    ++index;
                }
                int num = 0;
                foreach (byte[] numArray7 in numArray1)
                    num += numArray7.Length;
                this.data = new byte[9 + 16 * value.Count + num];
                this.ID = (ushort)371;
                this.offset = (ushort)2;
                this.PutByte((byte)value.Count);
                for (int s = 1; s <= value.Count; ++s)
                    this.PutInt(s);
                this.PutByte((byte)value.Count);
                foreach (byte[] bdata in numArray1)
                {
                    this.PutByte((byte)bdata.Length);
                    this.PutBytes(bdata);
                }
                this.PutByte((byte)value.Count);
                foreach (byte b in numArray2)
                    this.PutByte(b);
                this.PutByte((byte)value.Count);
                foreach (byte b in numArray3)
                    this.PutByte(b);
                this.PutByte((byte)value.Count);
                foreach (byte b in numArray4)
                    this.PutByte(b);
                this.PutByte((byte)value.Count);
                foreach (int s in numArray5)
                    this.PutInt(s);
                this.PutByte((byte)value.Count);
                foreach (uint s in numArray6)
                    this.PutUInt(s);
            }
        }
    }
}
