namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_ACTOR_APPEAR" />.
    /// </summary>
    public class SSMG_GOLEM_ACTOR_APPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_ACTOR_APPEAR"/> class.
        /// </summary>
        public SSMG_GOLEM_ACTOR_APPEAR()
        {
            this.data = new byte[30];
            this.offset = (ushort)2;
            this.ID = (ushort)6100;
        }

        /// <summary>
        /// Sets the PictID.
        /// </summary>
        public uint PictID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the Speed.
        /// </summary>
        public ushort Speed
        {
            set
            {
                this.PutUShort(value, (ushort)12);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public byte Dir
        {
            set
            {
                this.PutByte(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the GolemID.
        /// </summary>
        public uint GolemID
        {
            set
            {
                this.PutUInt(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the GolemType.
        /// </summary>
        public GolemType GolemType
        {
            set
            {
                this.PutByte((byte)value, (ushort)19);
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
                byte[] numArray = new byte[30 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)20);
                this.PutBytes(bytes, (ushort)21);
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
                byte num = this.GetByte((ushort)20);
                byte[] numArray = new byte[30 + (int)num + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)(21U + (uint)num));
                this.PutBytes(bytes, (ushort)(22U + (uint)num));
            }
        }

        /// <summary>
        /// Sets the Unknown.
        /// </summary>
        public uint Unknown
        {
            set
            {
                byte num1 = this.GetByte((ushort)20);
                byte num2 = (byte)((uint)num1 + (uint)this.GetByte((ushort)(21U + (uint)num1)));
                this.PutUInt(value, (ushort)(22U + (uint)num2));
                this.PutUInt(value, (ushort)(26U + (uint)num2));
            }
        }
    }
}
