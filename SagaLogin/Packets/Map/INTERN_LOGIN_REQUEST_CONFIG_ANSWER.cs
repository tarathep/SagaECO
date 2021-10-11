namespace SagaLogin.Packets.Map
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Defines the <see cref="INTERN_LOGIN_REQUEST_CONFIG_ANSWER" />.
    /// </summary>
    public class INTERN_LOGIN_REQUEST_CONFIG_ANSWER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="INTERN_LOGIN_REQUEST_CONFIG_ANSWER"/> class.
        /// </summary>
        public INTERN_LOGIN_REQUEST_CONFIG_ANSWER()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)65522;
        }

        /// <summary>
        /// Sets a value indicating whether AuthOK.
        /// </summary>
        public bool AuthOK
        {
            set
            {
                if (value)
                    this.PutByte((byte)1, (ushort)2);
                else
                    this.PutByte((byte)0, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the StartupSetting.
        /// </summary>
        public Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting> StartupSetting
        {
            set
            {
                MemoryStream memoryStream = new MemoryStream();
                new BinaryFormatter().Serialize((Stream)memoryStream, (object)value);
                memoryStream.Flush();
                byte[] numArray = new byte[8L + memoryStream.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutUInt((uint)memoryStream.Length, (ushort)3);
                this.PutBytes(memoryStream.ToArray(), (ushort)7);
            }
        }
    }
}
