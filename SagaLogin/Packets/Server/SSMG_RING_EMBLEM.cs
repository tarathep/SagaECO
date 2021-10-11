namespace SagaLogin.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_EMBLEM" />.
    /// </summary>
    public class SSMG_RING_EMBLEM : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_EMBLEM"/> class.
        /// </summary>
        public SSMG_RING_EMBLEM()
        {
            this.data = new byte[16];
            this.ID = (ushort)266;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the RingID.
        /// </summary>
        public uint RingID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Result2.
        /// </summary>
        public byte Result2
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Data.
        /// </summary>
        public byte[] Data
        {
            set
            {
                byte[] numArray = new byte[20 + value.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)253, (ushort)11);
                this.PutInt(value.Length, (ushort)12);
                this.PutBytes(value, (ushort)16);
            }
        }

        /// <summary>
        /// Sets the UpdateTime.
        /// </summary>
        public DateTime UpdateTime
        {
            set
            {
                uint totalSeconds = (uint)(value - new DateTime(1970, 1, 1)).TotalSeconds;
                if (this.GetByte((ushort)11) == (byte)253)
                {
                    int num = this.GetInt((ushort)12);
                    this.PutUInt(totalSeconds, (ushort)(16 + num));
                }
                else
                    this.PutUInt(totalSeconds, (ushort)12);
            }
        }
    }
}
