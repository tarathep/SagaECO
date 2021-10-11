namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_THEATER_SCHEDULE" />.
    /// </summary>
    public class SSMG_THEATER_SCHEDULE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_THEATER_SCHEDULE"/> class.
        /// </summary>
        public SSMG_THEATER_SCHEDULE()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)6811;
        }

        /// <summary>
        /// Sets the Index.
        /// </summary>
        public int Index
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the TicketItem.
        /// </summary>
        public uint TicketItem
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Time.
        /// </summary>
        public string Time
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[12 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)10);
                this.PutBytes(bytes, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the Title.
        /// </summary>
        public string Title
        {
            set
            {
                byte num = this.GetByte((ushort)10);
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[12 + (int)num + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)(11U + (uint)num));
                this.PutBytes(bytes, (ushort)(12U + (uint)num));
            }
        }
    }
}
