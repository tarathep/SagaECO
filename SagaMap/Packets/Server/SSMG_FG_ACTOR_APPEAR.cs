namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_ACTOR_APPEAR" />.
    /// </summary>
    public class SSMG_FG_ACTOR_APPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_ACTOR_APPEAR"/> class.
        /// </summary>
        /// <param name="type">The type<see cref="byte"/>.</param>
        public SSMG_FG_ACTOR_APPEAR(byte type)
        {
            this.data = new byte[25];
            this.offset = (ushort)2;
            if (type == (byte)1)
                this.ID = (ushort)7151;
            else
                this.ID = (ushort)7171;
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the PictID.
        /// </summary>
        public uint PictID
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public short X
        {
            set
            {
                this.PutShort(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public short Y
        {
            set
            {
                this.PutShort(value, (ushort)16);
            }
        }

        /// <summary>
        /// Sets the Z.
        /// </summary>
        public short Z
        {
            set
            {
                this.PutShort(value, (ushort)18);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public ushort Dir
        {
            set
            {
                this.PutUShort(value, (ushort)20);
            }
        }

        /// <summary>
        /// Sets the Motion.
        /// </summary>
        public ushort Motion
        {
            set
            {
                this.PutUShort(value, (ushort)22);
            }
        }

        /// <summary>
        /// Sets the Name.
        /// </summary>
        public string Name
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[25 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)24);
                this.PutBytes(bytes, (ushort)25);
            }
        }
    }
}
