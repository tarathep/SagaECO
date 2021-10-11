namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_WAREHOUSE" />.
    /// </summary>
    public class SSMG_GOLEM_WAREHOUSE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_WAREHOUSE"/> class.
        /// </summary>
        public SSMG_GOLEM_WAREHOUSE()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)6131;
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Title.
        /// </summary>
        public string Title
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[8 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)7);
                this.PutBytes(bytes, (ushort)8);
            }
        }
    }
}
