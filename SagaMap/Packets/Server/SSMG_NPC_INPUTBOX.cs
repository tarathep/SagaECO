namespace SagaMap.Packets.Server
{
    using SagaLib;
    using SagaMap.Scripting;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_INPUTBOX" />.
    /// </summary>
    public class SSMG_NPC_INPUTBOX : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_INPUTBOX"/> class.
        /// </summary>
        public SSMG_NPC_INPUTBOX()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)1535;
        }

        /// <summary>
        /// Sets the Title.
        /// </summary>
        public string Title
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[7 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)2);
                this.PutBytes(bytes, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Type.
        /// </summary>
        public InputType Type
        {
            set
            {
                byte num = this.GetByte((ushort)2);
                this.PutInt((int)value, (ushort)(3U + (uint)num));
            }
        }
    }
}
