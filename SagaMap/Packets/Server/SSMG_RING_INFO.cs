namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_INFO" />.
    /// </summary>
    public class SSMG_RING_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_INFO"/> class.
        /// </summary>
        public SSMG_RING_INFO()
        {
            this.data = new byte[28];
            this.offset = (ushort)2;
            this.ID = (ushort)6860;
        }

        /// <summary>
        /// The Ring.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="reason">The reason<see cref="SSMG_RING_INFO.Reason"/>.</param>
        public void Ring(SagaDB.Ring.Ring ring, SSMG_RING_INFO.Reason reason)
        {
            this.PutUInt(ring.ID, (ushort)2);
            byte[] bytes = Global.Unicode.GetBytes(ring.Name + "\0");
            byte[] numArray = new byte[28 + bytes.Length];
            byte length = (byte)bytes.Length;
            this.data.CopyTo((Array)numArray, 0);
            this.data = numArray;
            this.PutByte(length, (ushort)6);
            this.PutBytes(bytes, (ushort)7);
            this.PutByte((byte)2);
            this.PutInt((int)reason);
            this.PutUInt(0U);
            this.PutUInt(ring.Fame);
            this.PutInt(ring.MemberCount);
            this.PutInt(ring.MaxMemberCount);
        }

        /// <summary>
        /// Defines the Reason.
        /// </summary>
        public enum Reason
        {
            /// <summary>
            /// Defines the CREATE.
            /// </summary>
            CREATE,

            /// <summary>
            /// Defines the JOIN.
            /// </summary>
            JOIN,

            /// <summary>
            /// Defines the NONE.
            /// </summary>
            NONE,

            /// <summary>
            /// Defines the UPDATED.
            /// </summary>
            UPDATED,
        }
    }
}
