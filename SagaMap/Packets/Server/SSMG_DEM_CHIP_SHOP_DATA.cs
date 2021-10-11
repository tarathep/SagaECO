namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_CHIP_SHOP_DATA" />.
    /// </summary>
    public class SSMG_DEM_CHIP_SHOP_DATA : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_CHIP_SHOP_DATA"/> class.
        /// </summary>
        public SSMG_DEM_CHIP_SHOP_DATA()
        {
            this.data = new byte[15];
            this.offset = (ushort)2;
            this.ID = (ushort)1594;
        }

        /// <summary>
        /// Sets the EXP.
        /// </summary>
        public uint EXP
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the JEXP.
        /// </summary>
        public uint JEXP
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Description.
        /// </summary>
        public string Description
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value + "\0");
                byte[] numArray = new byte[15 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bytes.Length, (ushort)14);
                this.PutBytes(bytes, (ushort)15);
            }
        }
    }
}
