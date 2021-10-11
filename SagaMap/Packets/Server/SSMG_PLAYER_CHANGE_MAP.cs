namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_CHANGE_MAP" />.
    /// </summary>
    public class SSMG_PLAYER_CHANGE_MAP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_CHANGE_MAP"/> class.
        /// </summary>
        public SSMG_PLAYER_CHANGE_MAP()
        {
            this.data = new byte[17];
            this.offset = (ushort)2;
            this.ID = (ushort)4605;
            this.DungeonDir = (byte)4;
            this.DungeonX = byte.MaxValue;
            this.DungeonY = byte.MaxValue;
        }

        /// <summary>
        /// Sets the MapID.
        /// </summary>
        public uint MapID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the Dir.
        /// </summary>
        public byte Dir
        {
            set
            {
                this.PutByte(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the DungeonDir.
        /// </summary>
        public byte DungeonDir
        {
            set
            {
                this.PutByte(value, (ushort)9);
            }
        }

        /// <summary>
        /// Sets the DungeonX.
        /// </summary>
        public byte DungeonX
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the DungeonY.
        /// </summary>
        public byte DungeonY
        {
            set
            {
                this.PutByte(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets a value indicating whether FGTakeOff.
        /// </summary>
        public bool FGTakeOff
        {
            set
            {
                if (!value)
                    return;
                this.PutByte((byte)1, (ushort)16);
            }
        }
    }
}
