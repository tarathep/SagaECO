namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the GeneralBitFlags.
    /// </summary>
    [System.Flags]
    public enum GeneralBitFlags
    {
        /// <summary>
        /// Defines the Encrypted.
        /// </summary>
        Encrypted = 1,

        /// <summary>
        /// Defines the Method.
        /// </summary>
        Method = 6,

        /// <summary>
        /// Defines the Descriptor.
        /// </summary>
        Descriptor = 8,

        /// <summary>
        /// Defines the ReservedPKware4.
        /// </summary>
        ReservedPKware4 = 16, // 0x00000010

        /// <summary>
        /// Defines the Patched.
        /// </summary>
        Patched = 32, // 0x00000020

        /// <summary>
        /// Defines the StrongEncryption.
        /// </summary>
        StrongEncryption = 64, // 0x00000040

        /// <summary>
        /// Defines the Unused7.
        /// </summary>
        Unused7 = 128, // 0x00000080

        /// <summary>
        /// Defines the Unused8.
        /// </summary>
        Unused8 = 256, // 0x00000100

        /// <summary>
        /// Defines the Unused9.
        /// </summary>
        Unused9 = 512, // 0x00000200

        /// <summary>
        /// Defines the Unused10.
        /// </summary>
        Unused10 = 1024, // 0x00000400

        /// <summary>
        /// Defines the UnicodeText.
        /// </summary>
        UnicodeText = 2048, // 0x00000800

        /// <summary>
        /// Defines the EnhancedCompress.
        /// </summary>
        EnhancedCompress = 4096, // 0x00001000

        /// <summary>
        /// Defines the HeaderMasked.
        /// </summary>
        HeaderMasked = 8192, // 0x00002000

        /// <summary>
        /// Defines the ReservedPkware14.
        /// </summary>
        ReservedPkware14 = 16384, // 0x00004000

        /// <summary>
        /// Defines the ReservedPkware15.
        /// </summary>
        ReservedPkware15 = 32768, // 0x00008000
    }
}
