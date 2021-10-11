namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SELECT" />.
    /// </summary>
    public class SSMG_NPC_SELECT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SELECT"/> class.
        /// </summary>
        public SSMG_NPC_SELECT()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)1540;
        }

        /// <summary>
        /// The SetSelect.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="confirm">The confirm<see cref="string"/>.</param>
        /// <param name="options">The options<see cref="string[]"/>.</param>
        /// <param name="canCancel">The canCancel<see cref="bool"/>.</param>
        public void SetSelect(string title, string confirm, string[] options, bool canCancel)
        {
            if (title != "" && title.Substring(title.Length - 1) != "\0")
                title += "\0";
            if (confirm != "")
            {
                if (confirm.Substring(confirm.Length - 1) != "\0")
                    confirm += "\0";
            }
            else
                confirm = "\0";
            for (int index1 = 0; index1 < options.Length; ++index1)
            {
                if (options[index1].Substring(options[index1].Length - 1) != "\0")
                {
                    string[] strArray;
                    uint index2;
                    (strArray = options)[(int)(index2 = (uint)index1)] = strArray[index2] + "\0";
                }
            }
            byte[] bytes1 = Global.Unicode.GetBytes(title);
            byte[] bytes2 = Global.Unicode.GetBytes(confirm);
            byte[][] numArray = new byte[options.Length][];
            int num = 0;
            for (int index = 0; index < options.Length; ++index)
            {
                numArray[index] = Global.Unicode.GetBytes(options[index]);
                num += numArray[index].Length + 1;
            }
            this.data = new byte[num + (bytes1.Length + 1) + (bytes2.Length + 1) + 8];
            this.ID = (ushort)1540;
            this.offset = (ushort)2;
            this.PutByte((byte)bytes1.Length);
            this.PutBytes(bytes1);
            this.PutByte((byte)options.Length);
            foreach (byte[] bdata in numArray)
            {
                this.PutByte((byte)bdata.Length);
                this.PutBytes(bdata);
            }
            this.PutByte((byte)bytes2.Length);
            if (bytes2.Length != 0)
                this.PutBytes(bytes2);
            if (!canCancel)
                return;
            this.PutByte((byte)1);
        }
    }
}
