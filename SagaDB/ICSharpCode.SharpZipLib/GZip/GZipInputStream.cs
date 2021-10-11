namespace ICSharpCode.SharpZipLib.GZip
{
    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip.Compression;
    using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="GZipInputStream" />.
    /// </summary>
    public class GZipInputStream : InflaterInputStream
    {
        /// <summary>
        /// Defines the crc.
        /// </summary>
        protected Crc32 crc = new Crc32();

        /// <summary>
        /// Defines the eos.
        /// </summary>
        protected bool eos;

        /// <summary>
        /// Defines the readGZIPHeader.
        /// </summary>
        private bool readGZIPHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipInputStream"/> class.
        /// </summary>
        /// <param name="baseInputStream">The baseInputStream<see cref="Stream"/>.</param>
        public GZipInputStream(Stream baseInputStream)
      : this(baseInputStream, 4096)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipInputStream"/> class.
        /// </summary>
        /// <param name="baseInputStream">The baseInputStream<see cref="Stream"/>.</param>
        /// <param name="size">The size<see cref="int"/>.</param>
        public GZipInputStream(Stream baseInputStream, int size)
      : base(baseInputStream, new Inflater(true), size)
        {
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!this.readGZIPHeader)
                this.ReadHeader();
            if (this.eos)
                return 0;
            int count1 = base.Read(buffer, offset, count);
            if (count1 > 0)
                this.crc.Update(buffer, offset, count1);
            if (this.inf.IsFinished)
                this.ReadFooter();
            return count1;
        }

        /// <summary>
        /// The ReadHeader.
        /// </summary>
        private void ReadHeader()
        {
            Crc32 crc32 = new Crc32();
            int num1 = this.baseInputStream.ReadByte();
            if (num1 < 0)
                throw new EndOfStreamException("EOS reading GZIP header");
            crc32.Update(num1);
            if (num1 != 31)
                throw new GZipException("Error GZIP header, first magic byte doesn't match");
            int num2 = this.baseInputStream.ReadByte();
            if (num2 < 0)
                throw new EndOfStreamException("EOS reading GZIP header");
            if (num2 != 139)
                throw new GZipException("Error GZIP header,  second magic byte doesn't match");
            crc32.Update(num2);
            int num3 = this.baseInputStream.ReadByte();
            if (num3 < 0)
                throw new EndOfStreamException("EOS reading GZIP header");
            if (num3 != 8)
                throw new GZipException("Error GZIP header, data not in deflate format");
            crc32.Update(num3);
            int num4 = this.baseInputStream.ReadByte();
            if (num4 < 0)
                throw new EndOfStreamException("EOS reading GZIP header");
            crc32.Update(num4);
            if ((num4 & 224) != 0)
                throw new GZipException("Reserved flag bits in GZIP header != 0");
            for (int index = 0; index < 6; ++index)
            {
                int num5 = this.baseInputStream.ReadByte();
                if (num5 < 0)
                    throw new EndOfStreamException("EOS reading GZIP header");
                crc32.Update(num5);
            }
            if ((num4 & 4) != 0)
            {
                for (int index = 0; index < 2; ++index)
                {
                    int num5 = this.baseInputStream.ReadByte();
                    if (num5 < 0)
                        throw new EndOfStreamException("EOS reading GZIP header");
                    crc32.Update(num5);
                }
                if (this.baseInputStream.ReadByte() < 0 || this.baseInputStream.ReadByte() < 0)
                    throw new EndOfStreamException("EOS reading GZIP header");
                int num6 = this.baseInputStream.ReadByte();
                int num7 = this.baseInputStream.ReadByte();
                if (num6 < 0 || num7 < 0)
                    throw new EndOfStreamException("EOS reading GZIP header");
                crc32.Update(num6);
                crc32.Update(num7);
                int num8 = num6 << 8 | num7;
                for (int index = 0; index < num8; ++index)
                {
                    int num5 = this.baseInputStream.ReadByte();
                    if (num5 < 0)
                        throw new EndOfStreamException("EOS reading GZIP header");
                    crc32.Update(num5);
                }
            }
            if ((num4 & 8) != 0)
            {
                int num5;
                while ((num5 = this.baseInputStream.ReadByte()) > 0)
                    crc32.Update(num5);
                if (num5 < 0)
                    throw new EndOfStreamException("EOS reading GZIP header");
                crc32.Update(num5);
            }
            if ((num4 & 16) != 0)
            {
                int num5;
                while ((num5 = this.baseInputStream.ReadByte()) > 0)
                    crc32.Update(num5);
                if (num5 < 0)
                    throw new EndOfStreamException("EOS reading GZIP header");
                crc32.Update(num5);
            }
            if ((num4 & 2) != 0)
            {
                int num5 = this.baseInputStream.ReadByte();
                if (num5 < 0)
                    throw new EndOfStreamException("EOS reading GZIP header");
                int num6 = this.baseInputStream.ReadByte();
                if (num6 < 0)
                    throw new EndOfStreamException("EOS reading GZIP header");
                if ((num5 << 8 | num6) != ((int)crc32.Value & (int)ushort.MaxValue))
                    throw new GZipException("Header CRC value mismatch");
            }
            this.readGZIPHeader = true;
        }

        /// <summary>
        /// The ReadFooter.
        /// </summary>
        private void ReadFooter()
        {
            byte[] buffer = new byte[8];
            int length = this.inf.RemainingInput;
            if (length > 8)
                length = 8;
            Array.Copy((Array)this.inputBuffer.RawData, this.inputBuffer.RawLength - this.inf.RemainingInput, (Array)buffer, 0, length);
            int count = 8 - length;
            while (count > 0)
            {
                int num = this.baseInputStream.Read(buffer, 8 - count, count);
                if (num <= 0)
                    throw new EndOfStreamException("EOS reading GZIP footer");
                count -= num;
            }
            int num1 = (int)buffer[0] & (int)byte.MaxValue | ((int)buffer[1] & (int)byte.MaxValue) << 8 | ((int)buffer[2] & (int)byte.MaxValue) << 16 | (int)buffer[3] << 24;
            if (num1 != (int)this.crc.Value)
                throw new GZipException("GZIP crc sum mismatch, theirs \"" + (object)num1 + "\" and ours \"" + (object)(int)this.crc.Value);
            if ((this.inf.TotalOut & (long)uint.MaxValue) != (long)(uint)((int)buffer[4] & (int)byte.MaxValue | ((int)buffer[5] & (int)byte.MaxValue) << 8 | ((int)buffer[6] & (int)byte.MaxValue) << 16 | (int)buffer[7] << 24))
                throw new GZipException("Number of bytes mismatch in footer");
            this.eos = true;
        }
    }
}
