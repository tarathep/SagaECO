namespace ICSharpCode.SharpZipLib.GZip
{
    /// <summary>
    /// Defines the <see cref="GZipConstants" />.
    /// </summary>
    public sealed class GZipConstants
    {
        /// <summary>
        /// Defines the GZIP_MAGIC.
        /// </summary>
        public const int GZIP_MAGIC = 8075;

        /// <summary>
        /// Defines the FTEXT.
        /// </summary>
        public const int FTEXT = 1;

        /// <summary>
        /// Defines the FHCRC.
        /// </summary>
        public const int FHCRC = 2;

        /// <summary>
        /// Defines the FEXTRA.
        /// </summary>
        public const int FEXTRA = 4;

        /// <summary>
        /// Defines the FNAME.
        /// </summary>
        public const int FNAME = 8;

        /// <summary>
        /// Defines the FCOMMENT.
        /// </summary>
        public const int FCOMMENT = 16;

        /// <summary>
        /// Prevents a default instance of the <see cref="GZipConstants"/> class from being created.
        /// </summary>
        private GZipConstants()
        {
        }
    }
}
