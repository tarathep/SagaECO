namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_RECRUIT_REQUEST" />.
    /// </summary>
    public class SSMG_COMMUNITY_RECRUIT_REQUEST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_RECRUIT_REQUEST"/> class.
        /// </summary>
        public SSMG_COMMUNITY_RECRUIT_REQUEST()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)7085;
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
                byte[] numArray = new byte[7 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
            }
        }
    }
}
