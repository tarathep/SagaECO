namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAT_PUBLIC" />.
    /// </summary>
    public class SSMG_CHAT_PUBLIC : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAT_PUBLIC"/> class.
        /// </summary>
        public SSMG_CHAT_PUBLIC()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)1001;
        }

        /// <summary>
        /// Sets the ActorID
        /// -1 : システムメッセージ(黄)
        /// 0 : 管理者メッセージ(桃)
        /// 1-9999 : PCユーザー
        /// 10000-30000 : ペット
        /// 他 : 飛空庭設置ペットなど.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Message.
        /// </summary>
        public string Message
        {
            set
            {
                if (value.Substring(value.Length - 1, 1) != "\0")
                    value += "\0";
                byte[] bytes = Global.Unicode.GetBytes(value);
                byte[] numArray = new byte[bytes.Length + 7];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
            }
        }
    }
}
