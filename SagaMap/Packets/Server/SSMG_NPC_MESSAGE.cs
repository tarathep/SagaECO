namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_MESSAGE" />.
    /// </summary>
    public class SSMG_NPC_MESSAGE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_MESSAGE"/> class.
        /// </summary>
        public SSMG_NPC_MESSAGE()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)1015;
        }

        /// <summary>
        /// The SetMessage.
        /// </summary>
        /// <param name="npcID">The npcID<see cref="uint"/>.</param>
        /// <param name="num">The num<see cref="byte"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="motion">The motion<see cref="ushort"/>.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        public void SetMessage(uint npcID, byte num, string message, ushort motion, string title)
        {
            this.PutUInt(npcID, (ushort)2);
            this.PutByte(num, (ushort)6);
            byte[] bytes1 = Global.Unicode.GetBytes(message);
            byte[] numArray1 = new byte[bytes1.Length + this.data.Length + 1];
            this.data.CopyTo((Array)numArray1, 0);
            this.data = numArray1;
            byte b = (byte)(bytes1.Length + 1);
            this.PutByte(b, (ushort)7);
            this.PutBytes(bytes1, (ushort)8);
            ushort index = (ushort)(8U + (uint)b);
            this.PutUShort(motion, index);
            byte[] bytes2 = Global.Unicode.GetBytes(title);
            byte[] numArray2 = new byte[bytes2.Length + this.data.Length + 1];
            this.data.CopyTo((Array)numArray2, 0);
            this.data = numArray2;
            this.PutByte((byte)(bytes2.Length + 1), (ushort)((uint)index + 2U));
            this.PutBytes(bytes2, (ushort)((uint)index + 3U));
        }
    }
}
