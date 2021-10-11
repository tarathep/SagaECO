namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the <see cref="DescriptorData" />.
    /// </summary>
    public class DescriptorData
    {
        /// <summary>
        /// Defines the size.
        /// </summary>
        private long size;

        /// <summary>
        /// Defines the compressedSize.
        /// </summary>
        private long compressedSize;

        /// <summary>
        /// Defines the crc.
        /// </summary>
        private long crc;

        /// <summary>
        /// Gets or sets the CompressedSize.
        /// </summary>
        public long CompressedSize
        {
            get
            {
                return this.compressedSize;
            }
            set
            {
                this.compressedSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        public long Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        /// <summary>
        /// Gets or sets the Crc.
        /// </summary>
        public long Crc
        {
            get
            {
                return this.crc;
            }
            set
            {
                this.crc = value & (long)uint.MaxValue;
            }
        }
    }
}
