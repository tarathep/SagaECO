namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="NTTaggedData" />.
    /// </summary>
    public class NTTaggedData : ITaggedData
    {
        /// <summary>
        /// Defines the lastAccessTime_.
        /// </summary>
        private DateTime lastAccessTime_ = DateTime.FromFileTime(0L);

        /// <summary>
        /// Defines the lastModificationTime_.
        /// </summary>
        private DateTime lastModificationTime_ = DateTime.FromFileTime(0L);

        /// <summary>
        /// Defines the createTime_.
        /// </summary>
        private DateTime createTime_ = DateTime.FromFileTime(0L);

        /// <summary>
        /// Gets the TagID.
        /// </summary>
        public short TagID
        {
            get
            {
                return 10;
            }
        }

        /// <summary>
        /// The SetData.
        /// </summary>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetData(byte[] data, int index, int count)
        {
            using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
            {
                using (ZipHelperStream zipHelperStream = new ZipHelperStream((Stream)memoryStream))
                {
                    zipHelperStream.ReadLEInt();
                    while (zipHelperStream.Position < zipHelperStream.Length)
                    {
                        int num1 = zipHelperStream.ReadLEShort();
                        int num2 = zipHelperStream.ReadLEShort();
                        if (num1 == 1)
                        {
                            if (num2 < 24)
                                break;
                            this.lastModificationTime_ = DateTime.FromFileTime(zipHelperStream.ReadLELong());
                            this.lastAccessTime_ = DateTime.FromFileTime(zipHelperStream.ReadLELong());
                            this.createTime_ = DateTime.FromFileTime(zipHelperStream.ReadLELong());
                            break;
                        }
                        zipHelperStream.Seek((long)num2, SeekOrigin.Current);
                    }
                }
            }
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetData()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ZipHelperStream zipHelperStream = new ZipHelperStream((Stream)memoryStream))
                {
                    zipHelperStream.IsStreamOwner = false;
                    zipHelperStream.WriteLEInt(0);
                    zipHelperStream.WriteLEShort(1);
                    zipHelperStream.WriteLEShort(24);
                    zipHelperStream.WriteLELong(this.lastModificationTime_.ToFileTime());
                    zipHelperStream.WriteLELong(this.lastAccessTime_.ToFileTime());
                    zipHelperStream.WriteLELong(this.createTime_.ToFileTime());
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// The IsValidValue.
        /// </summary>
        /// <param name="value">The value<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidValue(DateTime value)
        {
            bool flag = true;
            try
            {
                value.ToFileTimeUtc();
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Gets or sets the LastModificationTime.
        /// </summary>
        public DateTime LastModificationTime
        {
            get
            {
                return this.lastModificationTime_;
            }
            set
            {
                if (!NTTaggedData.IsValidValue(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.lastModificationTime_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the CreateTime.
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return this.createTime_;
            }
            set
            {
                if (!NTTaggedData.IsValidValue(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.createTime_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the LastAccessTime.
        /// </summary>
        public DateTime LastAccessTime
        {
            get
            {
                return this.lastAccessTime_;
            }
            set
            {
                if (!NTTaggedData.IsValidValue(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.lastAccessTime_ = value;
            }
        }
    }
}
