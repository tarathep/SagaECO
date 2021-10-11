namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_START" />.
    /// </summary>
    public class SSMG_TRADE_START : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_START"/> class.
        /// </summary>
        public SSMG_TRADE_START()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)2575;
        }

        /// <summary>
        /// The SetPara.
        /// </summary>
        /// <param name="name">.</param>
        /// <param name="type">00だと人間? 01だとNPC.</param>
        public void SetPara(string name, int type)
        {
            byte[] bytes = Global.Unicode.GetBytes(name + "\0");
            byte[] numArray = new byte[7 + bytes.Length];
            this.data.CopyTo((Array)numArray, 0);
            this.data = numArray;
            this.PutByte((byte)bytes.Length, (ushort)2);
            this.PutBytes(bytes, (ushort)3);
            this.PutInt(type, (ushort)(3 + bytes.Length));
        }
    }
}
