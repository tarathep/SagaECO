namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ExtendedUnixData" />.
    /// </summary>
    public class ExtendedUnixData : ITaggedData
    {
        /// <summary>
        /// Defines the modificationTime_.
        /// </summary>
        private DateTime modificationTime_ = new DateTime(1970, 1, 1);

        /// <summary>
        /// Defines the lastAccessTime_.
        /// </summary>
        private DateTime lastAccessTime_ = new DateTime(1970, 1, 1);

        /// <summary>
        /// Defines the createTime_.
        /// </summary>
        private DateTime createTime_ = new DateTime(1970, 1, 1);

        /// <summary>
        /// Defines the flags_.
        /// </summary>
        private ExtendedUnixData.Flags flags_;

        /// <summary>
        /// Gets the TagID.
        /// </summary>
        public short TagID
        {
            get
            {
                return 21589;
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
                    this.flags_ = (ExtendedUnixData.Flags)zipHelperStream.ReadByte();
                    if ((this.flags_ & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags)0 && count >= 5)
                        this.modificationTime_ = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, zipHelperStream.ReadLEInt(), 0)).ToLocalTime();
                    if ((this.flags_ & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags)0)
                        this.lastAccessTime_ = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, zipHelperStream.ReadLEInt(), 0)).ToLocalTime();
                    if ((this.flags_ & ExtendedUnixData.Flags.CreateTime) == (ExtendedUnixData.Flags)0)
                        return;
                    this.createTime_ = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, zipHelperStream.ReadLEInt(), 0)).ToLocalTime();
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
                    zipHelperStream.WriteByte((byte)this.flags_);
                    if ((this.flags_ & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags)0)
                    {
                        int totalSeconds = (int)(this.modificationTime_.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
                        zipHelperStream.WriteLEInt(totalSeconds);
                    }
                    if ((this.flags_ & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags)0)
                    {
                        int totalSeconds = (int)(this.lastAccessTime_.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
                        zipHelperStream.WriteLEInt(totalSeconds);
                    }
                    if ((this.flags_ & ExtendedUnixData.Flags.CreateTime) != (ExtendedUnixData.Flags)0)
                    {
                        int totalSeconds = (int)(this.createTime_.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
                        zipHelperStream.WriteLEInt(totalSeconds);
                    }
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
            return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
        }

        /// <summary>
        /// Gets or sets the ModificationTime.
        /// </summary>
        public DateTime ModificationTime
        {
            get
            {
                return this.modificationTime_;
            }
            set
            {
                if (!ExtendedUnixData.IsValidValue(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.flags_ |= ExtendedUnixData.Flags.ModificationTime;
                this.modificationTime_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the AccessTime.
        /// </summary>
        public DateTime AccessTime
        {
            get
            {
                return this.lastAccessTime_;
            }
            set
            {
                if (!ExtendedUnixData.IsValidValue(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.flags_ |= ExtendedUnixData.Flags.AccessTime;
                this.lastAccessTime_ = value;
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
                if (!ExtendedUnixData.IsValidValue(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.flags_ |= ExtendedUnixData.Flags.CreateTime;
                this.createTime_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the Include.
        /// </summary>
        private ExtendedUnixData.Flags Include
        {
            get
            {
                return this.flags_;
            }
            set
            {
                this.flags_ = value;
            }
        }

        /// <summary>
        /// Defines the Flags.
        /// </summary>
        [System.Flags]
        public enum Flags : byte
        {
            /// <summary>
            /// Defines the ModificationTime.
            /// </summary>
            ModificationTime = 1,

            /// <summary>
            /// Defines the AccessTime.
            /// </summary>
            AccessTime = 2,

            /// <summary>
            /// Defines the CreateTime.
            /// </summary>
            CreateTime = 4,
        }
    }
}
