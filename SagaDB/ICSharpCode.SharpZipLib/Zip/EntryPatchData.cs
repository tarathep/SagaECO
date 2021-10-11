namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the <see cref="EntryPatchData" />.
    /// </summary>
    internal class EntryPatchData
    {
        /// <summary>
        /// Defines the sizePatchOffset_.
        /// </summary>
        private long sizePatchOffset_;

        /// <summary>
        /// Defines the crcPatchOffset_.
        /// </summary>
        private long crcPatchOffset_;

        /// <summary>
        /// Gets or sets the SizePatchOffset.
        /// </summary>
        public long SizePatchOffset
        {
            get
            {
                return this.sizePatchOffset_;
            }
            set
            {
                this.sizePatchOffset_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the CrcPatchOffset.
        /// </summary>
        public long CrcPatchOffset
        {
            get
            {
                return this.crcPatchOffset_;
            }
            set
            {
                this.crcPatchOffset_ = value;
            }
        }
    }
}
