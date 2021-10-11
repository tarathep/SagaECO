namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ZipHelperStream" />.
    /// </summary>
    internal class ZipHelperStream : Stream
    {
        /// <summary>
        /// Defines the isOwner_.
        /// </summary>
        private bool isOwner_;

        /// <summary>
        /// Defines the stream_.
        /// </summary>
        private Stream stream_;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipHelperStream"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public ZipHelperStream(string name)
        {
            this.stream_ = (Stream)new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            this.isOwner_ = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipHelperStream"/> class.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public ZipHelperStream(Stream stream)
        {
            this.stream_ = stream;
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsStreamOwner.
        /// </summary>
        public bool IsStreamOwner
        {
            get
            {
                return this.isOwner_;
            }
            set
            {
                this.isOwner_ = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanRead.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return this.stream_.CanRead;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanSeek.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return this.stream_.CanSeek;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanTimeout.
        /// </summary>
        public override bool CanTimeout
        {
            get
            {
                return this.stream_.CanTimeout;
            }
        }

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public override long Length
        {
            get
            {
                return this.stream_.Length;
            }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.stream_.Position;
            }
            set
            {
                this.stream_.Position = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanWrite.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return this.stream_.CanWrite;
            }
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public override void Flush()
        {
            this.stream_.Flush();
        }

        /// <summary>
        /// The Seek.
        /// </summary>
        /// <param name="offset">The offset<see cref="long"/>.</param>
        /// <param name="origin">The origin<see cref="SeekOrigin"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream_.Seek(offset, origin);
        }

        /// <summary>
        /// The SetLength.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public override void SetLength(long value)
        {
            this.stream_.SetLength(value);
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
            return this.stream_.Read(buffer, offset, count);
        }

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream_.Write(buffer, offset, count);
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            Stream stream = this.stream_;
            this.stream_ = (Stream)null;
            if (!this.isOwner_ || stream == null)
                return;
            this.isOwner_ = false;
            stream.Close();
        }

        /// <summary>
        /// The WriteLocalHeader.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        /// <param name="patchData">The patchData<see cref="EntryPatchData"/>.</param>
        private void WriteLocalHeader(ZipEntry entry, EntryPatchData patchData)
        {
            CompressionMethod compressionMethod = entry.CompressionMethod;
            bool flag1 = true;
            bool flag2 = false;
            this.WriteLEInt(67324752);
            this.WriteLEShort(entry.Version);
            this.WriteLEShort(entry.Flags);
            this.WriteLEShort((int)(byte)compressionMethod);
            this.WriteLEInt((int)entry.DosTime);
            if (flag1)
            {
                this.WriteLEInt((int)entry.Crc);
                if (entry.LocalHeaderRequiresZip64)
                {
                    this.WriteLEInt(-1);
                    this.WriteLEInt(-1);
                }
                else
                {
                    this.WriteLEInt(entry.IsCrypted ? (int)entry.CompressedSize + 12 : (int)entry.CompressedSize);
                    this.WriteLEInt((int)entry.Size);
                }
            }
            else
            {
                if (patchData != null)
                    patchData.CrcPatchOffset = this.stream_.Position;
                this.WriteLEInt(0);
                if (patchData != null)
                    patchData.SizePatchOffset = this.stream_.Position;
                if (entry.LocalHeaderRequiresZip64 && flag2)
                {
                    this.WriteLEInt(-1);
                    this.WriteLEInt(-1);
                }
                else
                {
                    this.WriteLEInt(0);
                    this.WriteLEInt(0);
                }
            }
            byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
            if (array.Length > (int)ushort.MaxValue)
                throw new ZipException("Entry name too long.");
            ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
            if (entry.LocalHeaderRequiresZip64 && (flag1 || flag2))
            {
                zipExtraData.StartNewEntry();
                if (flag1)
                {
                    zipExtraData.AddLeLong(entry.Size);
                    zipExtraData.AddLeLong(entry.CompressedSize);
                }
                else
                {
                    zipExtraData.AddLeLong(-1L);
                    zipExtraData.AddLeLong(-1L);
                }
                zipExtraData.AddNewEntry(1);
                if (!zipExtraData.Find(1))
                    throw new ZipException("Internal error cant find extra data");
                if (patchData != null)
                    patchData.SizePatchOffset = (long)zipExtraData.CurrentReadIndex;
            }
            else
                zipExtraData.Delete(1);
            byte[] entryData = zipExtraData.GetEntryData();
            this.WriteLEShort(array.Length);
            this.WriteLEShort(entryData.Length);
            if (array.Length > 0)
                this.stream_.Write(array, 0, array.Length);
            if (entry.LocalHeaderRequiresZip64 && flag2)
                patchData.SizePatchOffset += this.stream_.Position;
            if (entryData.Length <= 0)
                return;
            this.stream_.Write(entryData, 0, entryData.Length);
        }

        /// <summary>
        /// The LocateBlockWithSignature.
        /// </summary>
        /// <param name="signature">The signature<see cref="int"/>.</param>
        /// <param name="endLocation">The endLocation<see cref="long"/>.</param>
        /// <param name="minimumBlockSize">The minimumBlockSize<see cref="int"/>.</param>
        /// <param name="maximumVariableData">The maximumVariableData<see cref="int"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
        {
            long num1 = endLocation - (long)minimumBlockSize;
            if (num1 < 0L)
                return -1;
            long num2 = Math.Max(num1 - (long)maximumVariableData, 0L);
            while (num1 >= num2)
            {
                this.Seek(num1--, SeekOrigin.Begin);
                if (this.ReadLEInt() == signature)
                    return this.Position;
            }
            return -1;
        }

        /// <summary>
        /// The WriteZip64EndOfCentralDirectory.
        /// </summary>
        /// <param name="noOfEntries">The noOfEntries<see cref="long"/>.</param>
        /// <param name="sizeEntries">The sizeEntries<see cref="long"/>.</param>
        /// <param name="centralDirOffset">The centralDirOffset<see cref="long"/>.</param>
        public void WriteZip64EndOfCentralDirectory(long noOfEntries, long sizeEntries, long centralDirOffset)
        {
            long position = this.stream_.Position;
            this.WriteLEInt(101075792);
            this.WriteLELong(44L);
            this.WriteLEShort(45);
            this.WriteLEShort(45);
            this.WriteLEInt(0);
            this.WriteLEInt(0);
            this.WriteLELong(noOfEntries);
            this.WriteLELong(noOfEntries);
            this.WriteLELong(sizeEntries);
            this.WriteLELong(centralDirOffset);
            this.WriteLEInt(117853008);
            this.WriteLEInt(0);
            this.WriteLELong(position);
            this.WriteLEInt(1);
        }

        /// <summary>
        /// The WriteEndOfCentralDirectory.
        /// </summary>
        /// <param name="noOfEntries">The noOfEntries<see cref="long"/>.</param>
        /// <param name="sizeEntries">The sizeEntries<see cref="long"/>.</param>
        /// <param name="startOfCentralDirectory">The startOfCentralDirectory<see cref="long"/>.</param>
        /// <param name="comment">The comment<see cref="byte[]"/>.</param>
        public void WriteEndOfCentralDirectory(long noOfEntries, long sizeEntries, long startOfCentralDirectory, byte[] comment)
        {
            if (noOfEntries >= (long)ushort.MaxValue || startOfCentralDirectory >= (long)uint.MaxValue || sizeEntries >= (long)uint.MaxValue)
                this.WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
            this.WriteLEInt(101010256);
            this.WriteLEShort(0);
            this.WriteLEShort(0);
            if (noOfEntries >= (long)ushort.MaxValue)
            {
                this.WriteLEUshort(ushort.MaxValue);
                this.WriteLEUshort(ushort.MaxValue);
            }
            else
            {
                this.WriteLEShort((int)(short)noOfEntries);
                this.WriteLEShort((int)(short)noOfEntries);
            }
            if (sizeEntries >= (long)uint.MaxValue)
                this.WriteLEUint(uint.MaxValue);
            else
                this.WriteLEInt((int)sizeEntries);
            if (startOfCentralDirectory >= (long)uint.MaxValue)
                this.WriteLEUint(uint.MaxValue);
            else
                this.WriteLEInt((int)startOfCentralDirectory);
            int num = comment != null ? comment.Length : 0;
            if (num > (int)ushort.MaxValue)
                throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", (object)num));
            this.WriteLEShort(num);
            if (num <= 0)
                return;
            this.Write(comment, 0, comment.Length);
        }

        /// <summary>
        /// The ReadLEShort.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadLEShort()
        {
            int num1 = this.stream_.ReadByte();
            if (num1 < 0)
                throw new EndOfStreamException();
            int num2 = this.stream_.ReadByte();
            if (num2 < 0)
                throw new EndOfStreamException();
            return num1 | num2 << 8;
        }

        /// <summary>
        /// The ReadLEInt.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadLEInt()
        {
            return this.ReadLEShort() | this.ReadLEShort() << 16;
        }

        /// <summary>
        /// The ReadLELong.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        public long ReadLELong()
        {
            return (long)(uint)this.ReadLEInt() | (long)this.ReadLEInt() << 32;
        }

        /// <summary>
        /// The WriteLEShort.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void WriteLEShort(int value)
        {
            this.stream_.WriteByte((byte)(value & (int)byte.MaxValue));
            this.stream_.WriteByte((byte)(value >> 8 & (int)byte.MaxValue));
        }

        /// <summary>
        /// The WriteLEUshort.
        /// </summary>
        /// <param name="value">The value<see cref="ushort"/>.</param>
        public void WriteLEUshort(ushort value)
        {
            this.stream_.WriteByte((byte)((uint)value & (uint)byte.MaxValue));
            this.stream_.WriteByte((byte)((uint)value >> 8));
        }

        /// <summary>
        /// The WriteLEInt.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void WriteLEInt(int value)
        {
            this.WriteLEShort(value);
            this.WriteLEShort(value >> 16);
        }

        /// <summary>
        /// The WriteLEUint.
        /// </summary>
        /// <param name="value">The value<see cref="uint"/>.</param>
        public void WriteLEUint(uint value)
        {
            this.WriteLEUshort((ushort)(value & (uint)ushort.MaxValue));
            this.WriteLEUshort((ushort)(value >> 16));
        }

        /// <summary>
        /// The WriteLELong.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public void WriteLELong(long value)
        {
            this.WriteLEInt((int)value);
            this.WriteLEInt((int)(value >> 32));
        }

        /// <summary>
        /// The WriteLEUlong.
        /// </summary>
        /// <param name="value">The value<see cref="ulong"/>.</param>
        public void WriteLEUlong(ulong value)
        {
            this.WriteLEUint((uint)(value & (ulong)uint.MaxValue));
            this.WriteLEUint((uint)(value >> 32));
        }

        /// <summary>
        /// The WriteDataDescriptor.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int WriteDataDescriptor(ZipEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            int num1 = 0;
            if ((entry.Flags & 8) != 0)
            {
                this.WriteLEInt(134695760);
                this.WriteLEInt((int)entry.Crc);
                int num2 = num1 + 8;
                if (entry.LocalHeaderRequiresZip64)
                {
                    this.WriteLELong(entry.CompressedSize);
                    this.WriteLELong(entry.Size);
                    num1 = num2 + 16;
                }
                else
                {
                    this.WriteLEInt((int)entry.CompressedSize);
                    this.WriteLEInt((int)entry.Size);
                    num1 = num2 + 8;
                }
            }
            return num1;
        }

        /// <summary>
        /// The ReadDataDescriptor.
        /// </summary>
        /// <param name="zip64">The zip64<see cref="bool"/>.</param>
        /// <param name="data">The data<see cref="DescriptorData"/>.</param>
        public void ReadDataDescriptor(bool zip64, DescriptorData data)
        {
            if (this.ReadLEInt() != 134695760)
                throw new ZipException("Data descriptor signature not found");
            data.Crc = (long)this.ReadLEInt();
            if (zip64)
            {
                data.CompressedSize = this.ReadLELong();
                data.Size = this.ReadLELong();
            }
            else
            {
                data.CompressedSize = (long)this.ReadLEInt();
                data.Size = (long)this.ReadLEInt();
            }
        }
    }
}
