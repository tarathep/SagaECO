namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_NAME" />.
    /// </summary>
    public class SSMG_PARTY_NAME : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_NAME"/> class.
        /// </summary>
        public SSMG_PARTY_NAME()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)6617;
        }

        /// <summary>
        /// The Party.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        /// <param name="pc">The pc<see cref="SagaDB.Actor.Actor"/>.</param>
        public void Party(SagaDB.Party.Party party, SagaDB.Actor.Actor pc)
        {
            this.PutUInt(pc.ActorID, (ushort)2);
            byte[] bdata = party == null ? new byte[1] : Global.Unicode.GetBytes(party.Name + "\0");
            byte[] numArray = new byte[8 + bdata.Length];
            byte length = (byte)bdata.Length;
            this.data.CopyTo((Array)numArray, 0);
            this.data = numArray;
            this.PutByte(length, (ushort)6);
            this.PutBytes(bdata, (ushort)7);
        }
    }
}
