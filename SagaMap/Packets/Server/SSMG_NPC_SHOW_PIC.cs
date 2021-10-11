namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SHOW_PIC" />.
    /// </summary>
    public class SSMG_NPC_SHOW_PIC : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SHOW_PIC"/> class.
        /// </summary>
        public SSMG_NPC_SHOW_PIC()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)1660;
        }

        /// <summary>
        /// Sets the Path.
        /// </summary>
        public string Path
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[this.data.Length + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)2);
                this.PutBytes(bytes, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Unknown.
        /// </summary>
        public int Unknown
        {
            set
            {
                byte num = this.GetByte((ushort)2);
                this.PutInt(value, (ushort)(3U + (uint)num));
            }
        }

        /// <summary>
        /// Sets the Unknown2.
        /// </summary>
        public int Unknown2
        {
            set
            {
                byte num = this.GetByte((ushort)2);
                this.PutInt(value, (ushort)(7U + (uint)num));
            }
        }

        /// <summary>
        /// Sets the Title.
        /// </summary>
        public string Title
        {
            set
            {
                byte num = this.GetByte((ushort)2);
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[this.data.Length + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)(11U + (uint)num));
                this.PutBytes(bytes, (ushort)(12U + (uint)num));
            }
        }
    }
}
