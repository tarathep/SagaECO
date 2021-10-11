namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the CompressionMethod.
    /// </summary>
    public enum CompressionMethod
    {
        /// <summary>
        /// Defines the Stored.
        /// </summary>
        Stored = 0,

        /// <summary>
        /// Defines the Deflated.
        /// </summary>
        Deflated = 8,

        /// <summary>
        /// Defines the Deflate64.
        /// </summary>
        Deflate64 = 9,

        /// <summary>
        /// Defines the BZip2.
        /// </summary>
        BZip2 = 11, // 0x0000000B

        /// <summary>
        /// Defines the WinZipAES.
        /// </summary>
        WinZipAES = 99, // 0x00000063
    }
}
