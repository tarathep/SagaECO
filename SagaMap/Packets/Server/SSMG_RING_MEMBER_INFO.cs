namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_MEMBER_INFO" />.
    /// </summary>
    public class SSMG_RING_MEMBER_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_MEMBER_INFO"/> class.
        /// </summary>
        public SSMG_RING_MEMBER_INFO()
        {
            this.data = new byte[17];
            this.offset = (ushort)2;
            this.ID = (ushort)6862;
        }

        /// <summary>
        /// The Member.
        /// </summary>
        /// <param name="value">The value<see cref="ActorPC"/>.</param>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        public void Member(ActorPC value, SagaDB.Ring.Ring ring)
        {
            int s = ring == null ? -1 : ring.IndexOf(value);
            this.PutInt(s, (ushort)2);
            this.PutUInt(value.CharID);
            byte[] bytes = Global.Unicode.GetBytes(value.Name + "\0");
            byte[] numArray = new byte[17 + bytes.Length];
            this.data.CopyTo((Array)numArray, 0);
            this.data = numArray;
            this.PutByte((byte)bytes.Length);
            this.PutBytes(bytes);
            this.PutByte((byte)value.Race);
            this.PutByte((byte)value.Gender);
            if (s == -1)
                return;
            this.PutInt(ring.Rights[s].Value);
        }
    }
}
