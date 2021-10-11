namespace ICSharpCode.SharpZipLib.Checksums
{
    /// <summary>
    /// Defines the <see cref="IChecksum" />.
    /// </summary>
    public interface IChecksum
    {
        /// <summary>
        /// Gets the Value.
        /// </summary>
        long Value { get; }

        /// <summary>
        /// The Reset.
        /// </summary>
        void Reset();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        void Update(int value);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        void Update(byte[] buffer);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        void Update(byte[] buffer, int offset, int count);
    }
}
