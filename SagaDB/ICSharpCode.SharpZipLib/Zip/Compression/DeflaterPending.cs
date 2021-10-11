namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    /// <summary>
    /// Defines the <see cref="DeflaterPending" />.
    /// </summary>
    public class DeflaterPending : PendingBuffer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeflaterPending"/> class.
        /// </summary>
        public DeflaterPending()
      : base(65536)
        {
        }
    }
}
