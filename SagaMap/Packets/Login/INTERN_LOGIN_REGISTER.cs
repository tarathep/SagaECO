namespace SagaMap.Packets.Login
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="INTERN_LOGIN_REGISTER" />.
    /// </summary>
    public class INTERN_LOGIN_REGISTER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="INTERN_LOGIN_REGISTER"/> class.
        /// </summary>
        public INTERN_LOGIN_REGISTER()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)65520;
        }

        /// <summary>
        /// Sets the Password.
        /// </summary>
        public string Password
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value);
                this.PutByte((byte)bytes.Length, (ushort)2);
                byte[] numArray = new byte[this.data.Length + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutBytes(bytes, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the HostedMaps.
        /// </summary>
        public List<uint> HostedMaps
        {
            set
            {
                ushort index1 = (ushort)(3U + (uint)this.GetByte((ushort)2));
                byte[] bytes = Global.Unicode.GetBytes(Singleton<Configuration>.Instance.Host);
                this.PutByte((byte)bytes.Length, index1);
                byte[] numArray1 = new byte[this.data.Length + bytes.Length];
                this.data.CopyTo((Array)numArray1, 0);
                this.data = numArray1;
                this.PutBytes(bytes, (ushort)((uint)index1 + 1U));
                ushort index2 = (ushort)((int)index1 + 1 + bytes.Length);
                this.PutInt(Singleton<Configuration>.Instance.Port, index2);
                byte[] numArray2 = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo((Array)numArray2, 0);
                this.data = numArray2;
                this.PutByte((byte)value.Count, (ushort)((uint)index2 + 4U));
                for (int index3 = 0; index3 < value.Count; ++index3)
                    this.PutUInt(value[index3], (ushort)((int)index2 + 5 + index3 * 4));
            }
        }
    }
}
