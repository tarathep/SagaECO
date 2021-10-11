namespace SagaLib.VirtualFileSystem.Lpk
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="LpkFileInfo" />.
    /// </summary>
    public class LpkFileInfo
    {
        /// <summary>
        /// Defines the headerOffset.
        /// </summary>
        private uint headerOffset;

        /// <summary>
        /// Defines the fileSize.
        /// </summary>
        private uint fileSize;

        /// <summary>
        /// Defines the uncompressedSize.
        /// </summary>
        private uint uncompressedSize;

        /// <summary>
        /// Defines the dataOffset.
        /// </summary>
        private uint dataOffset;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the crc.
        /// </summary>
        private uint crc;

        /// <summary>
        /// Gets or sets the HeaderOffset.
        /// </summary>
        public uint HeaderOffset
        {
            get
            {
                return this.headerOffset;
            }
            set
            {
                this.headerOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the DataOffset.
        /// </summary>
        public uint DataOffset
        {
            get
            {
                return this.dataOffset;
            }
            set
            {
                this.dataOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the FileSize.
        /// </summary>
        public uint FileSize
        {
            get
            {
                return this.fileSize;
            }
            set
            {
                this.fileSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the UncompressedSize.
        /// </summary>
        public uint UncompressedSize
        {
            get
            {
                return this.uncompressedSize;
            }
            set
            {
                this.uncompressedSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the CRC.
        /// </summary>
        public uint CRC
        {
            get
            {
                return this.crc;
            }
            set
            {
                this.crc = value;
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets the Size.
        /// </summary>
        public static int Size
        {
            get
            {
                return 16;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LpkFileInfo"/> class.
        /// </summary>
        public LpkFileInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LpkFileInfo"/> class.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public LpkFileInfo(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            this.headerOffset = (uint)stream.Position;
            this.dataOffset = binaryReader.ReadUInt32() ^ 265851106U;
            this.fileSize = binaryReader.ReadUInt32() ^ 852870806U;
            this.uncompressedSize = binaryReader.ReadUInt32() ^ 511060806U;
            this.crc = binaryReader.ReadUInt32() ^ 987654321U;
        }

        /// <summary>
        /// The WriteToStream.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void WriteToStream(Stream stream)
        {
            BinaryWriter binaryWriter = new BinaryWriter(stream);
            stream.Position = (long)this.headerOffset;
            binaryWriter.Write(this.dataOffset ^ 265851106U);
            binaryWriter.Write(this.fileSize ^ 852870806U);
            binaryWriter.Write(this.uncompressedSize ^ 511060806U);
            binaryWriter.Write(this.crc ^ 987654321U);
        }
    }
}
