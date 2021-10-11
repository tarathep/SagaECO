namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_INVITE" />.
    /// </summary>
    public class SSMG_RING_INVITE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_INVITE"/> class.
        /// </summary>
        public SSMG_RING_INVITE()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)6831;
        }

        /// <summary>
        /// Sets the CharID.
        /// </summary>
        public uint CharID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the CharName.
        /// </summary>
        public string CharName
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[8 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the RingName.
        /// </summary>
        public string RingName
        {
            set
            {
                byte num = this.GetByte((ushort)6);
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[8 + (int)num + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)(7U + (uint)num));
                this.PutBytes(bytes, (ushort)(8U + (uint)num));
            }
        }
    }
}
