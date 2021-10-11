namespace SevenZip
{
    /// <summary>
    /// Defines the <see cref="ISetDecoderProperties" />.
    /// </summary>
    public interface ISetDecoderProperties
    {
        /// <summary>
        /// The SetDecoderProperties.
        /// </summary>
        /// <param name="properties">The properties<see cref="byte[]"/>.</param>
        void SetDecoderProperties(byte[] properties);
    }
}
