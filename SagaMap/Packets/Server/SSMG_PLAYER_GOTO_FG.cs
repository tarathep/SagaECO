namespace SagaMap.Packets.Server
{
    using SagaDB.FGarden;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_GOTO_FG" />.
    /// </summary>
    public class SSMG_PLAYER_GOTO_FG : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_GOTO_FG"/> class.
        /// </summary>
        public SSMG_PLAYER_GOTO_FG()
        {
            this.data = new byte[57];
            this.offset = (ushort)2;
            this.ID = (ushort)7140;
            this.PutByte((byte)9, (ushort)9);
            this.PutByte((byte)9, (ushort)46);
            this.PutByte((byte)1, (ushort)56);
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
        /// Sets the Equiptments.
        /// </summary>
        public Dictionary<FGardenSlot, uint> Equiptments
        {
            set
            {
                for (int index = 0; index < 8; ++index)
                    this.PutUInt(value[(FGardenSlot)index], (ushort)(10 + index * 4));
            }
        }
    }
}
