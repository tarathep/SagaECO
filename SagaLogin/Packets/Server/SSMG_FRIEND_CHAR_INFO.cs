namespace SagaLogin.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaLogin.Network.Client;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_CHAR_INFO" />.
    /// </summary>
    public class SSMG_FRIEND_CHAR_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_CHAR_INFO"/> class.
        /// </summary>
        public SSMG_FRIEND_CHAR_INFO()
        {
            this.data = new byte[20];
            this.ID = (ushort)220;
        }

        /// <summary>
        /// Sets the ActorPC.
        /// </summary>
        public ActorPC ActorPC
        {
            set
            {
                this.PutUInt(value.CharID, (ushort)2);
                byte[] bytes = Global.Unicode.GetBytes(value.Name + "\0");
                byte[] numArray = new byte[20 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                byte length = (byte)bytes.Length;
                this.PutByte(length, (ushort)6);
                this.PutBytes(bytes, (ushort)7);
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                {
                    this.PutByte((byte)4, (ushort)(7U + (uint)length));
                    this.PutUShort((ushort)value.Job, (ushort)(8U + (uint)length));
                    this.PutUShort((ushort)value.Level, (ushort)(10U + (uint)length));
                    this.PutUShort((ushort)value.CurrentJobLevel, (ushort)(12U + (uint)length));
                }
                else
                {
                    this.PutUShort((ushort)value.Job, (ushort)(7U + (uint)length));
                    this.PutUShort((ushort)value.Level, (ushort)(9U + (uint)length));
                    this.PutUShort((ushort)value.CurrentJobLevel, (ushort)(11U + (uint)length));
                }
            }
        }

        /// <summary>
        /// Sets the MapID.
        /// </summary>
        public uint MapID
        {
            set
            {
                byte num = this.GetByte((ushort)6);
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutUInt(value, (ushort)(14U + (uint)num));
                else
                    this.PutUInt(value, (ushort)(13U + (uint)num));
            }
        }

        /// <summary>
        /// Sets the Status.
        /// </summary>
        public CharStatus Status
        {
            set
            {
                byte num = this.GetByte((ushort)6);
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutByte((byte)value, (ushort)(18U + (uint)num));
                else
                    this.PutByte((byte)value, (ushort)(17U + (uint)num));
            }
        }

        /// <summary>
        /// Sets the Comment.
        /// </summary>
        public string Comment
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                {
                    byte num = this.GetByte((ushort)6);
                    byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                    byte[] numArray = new byte[20 + (int)num + bytes.Length];
                    this.data.CopyTo((Array)numArray, 0);
                    this.data = numArray;
                    this.PutByte((byte)bytes.Length, (ushort)(19U + (uint)num));
                    this.PutBytes(bytes, (ushort)(20U + (uint)num));
                }
                else
                {
                    byte num = this.GetByte((ushort)6);
                    byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                    byte[] numArray = new byte[19 + (int)num + bytes.Length];
                    this.data.CopyTo((Array)numArray, 0);
                    this.data = numArray;
                    this.PutByte((byte)bytes.Length, (ushort)(18U + (uint)num));
                    this.PutBytes(bytes, (ushort)(19U + (uint)num));
                }
            }
        }
    }
}
