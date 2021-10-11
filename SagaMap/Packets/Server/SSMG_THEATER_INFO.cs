namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_THEATER_INFO" />.
    /// </summary>
    public class SSMG_THEATER_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_THEATER_INFO"/> class.
        /// </summary>
        public SSMG_THEATER_INFO()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)6800;
        }

        /// <summary>
        /// Sets the MessageType.
        /// </summary>
        public SSMG_THEATER_INFO.Type MessageType
        {
            set
            {
                this.PutUInt((uint)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Message.
        /// </summary>
        public string Message
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

        /// <summary>
        /// Defines the Type.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Defines the MESSAGE.
            /// </summary>
            MESSAGE = 10, // 0x0000000A

            /// <summary>
            /// Defines the MOVIE_ADDRESS.
            /// </summary>
            MOVIE_ADDRESS = 20, // 0x00000014

            /// <summary>
            /// Defines the STOP_BGM.
            /// </summary>
            STOP_BGM = 31, // 0x0000001F

            /// <summary>
            /// Defines the PLAY.
            /// </summary>
            PLAY = 40, // 0x00000028
        }
    }
}
