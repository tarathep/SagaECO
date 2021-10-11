namespace ICSharpCode.SharpZipLib.Zip
{
    using System;

    /// <summary>
    /// Defines the <see cref="RawTaggedData" />.
    /// </summary>
    public class RawTaggedData : ITaggedData
    {
        /// <summary>
        /// Defines the tag_.
        /// </summary>
        protected short tag_;

        /// <summary>
        /// Defines the data_.
        /// </summary>
        private byte[] data_;

        /// <summary>
        /// Initializes a new instance of the <see cref="RawTaggedData"/> class.
        /// </summary>
        /// <param name="tag">The tag<see cref="short"/>.</param>
        public RawTaggedData(short tag)
        {
            this.tag_ = tag;
        }

        /// <summary>
        /// Gets or sets the TagID.
        /// </summary>
        public short TagID
        {
            get
            {
                return this.tag_;
            }
            set
            {
                this.tag_ = value;
            }
        }

        /// <summary>
        /// The SetData.
        /// </summary>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetData(byte[] data, int offset, int count)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            this.data_ = new byte[count];
            Array.Copy((Array)data, offset, (Array)this.data_, 0, count);
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetData()
        {
            return this.data_;
        }

        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        public byte[] Data
        {
            get
            {
                return this.data_;
            }
            set
            {
                this.data_ = value;
            }
        }
    }
}
