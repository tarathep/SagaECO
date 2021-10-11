namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_INFO" />.
    /// </summary>
    public class SSMG_PARTY_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_INFO"/> class.
        /// </summary>
        public SSMG_PARTY_INFO()
        {
            this.data = new byte[13];
            this.offset = (ushort)2;
            this.ID = (ushort)6620;
        }

        /// <summary>
        /// The Party.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void Party(SagaDB.Party.Party party, ActorPC pc)
        {
            this.PutUInt(party.ID, (ushort)2);
            byte[] bytes = Global.Unicode.GetBytes(party.Name + "\0");
            byte[] numArray = new byte[13 + bytes.Length];
            byte length = (byte)bytes.Length;
            this.data.CopyTo((Array)numArray, 0);
            this.data = numArray;
            this.PutByte(length, (ushort)6);
            this.PutBytes(bytes, (ushort)7);
            if (party.Leader == pc)
                this.PutByte((byte)1, (ushort)(7U + (uint)length));
            else
                this.PutByte((byte)0, (ushort)(7U + (uint)length));
            this.PutByte(party.IndexOf(pc), (ushort)(8U + (uint)length));
            this.PutInt(party.MemberCount, (ushort)(9U + (uint)length));
        }
    }
}
