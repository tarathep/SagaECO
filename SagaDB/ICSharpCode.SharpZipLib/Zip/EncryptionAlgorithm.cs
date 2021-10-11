namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the EncryptionAlgorithm.
    /// </summary>
    public enum EncryptionAlgorithm
    {
        /// <summary>
        /// Defines the None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Defines the PkzipClassic.
        /// </summary>
        PkzipClassic = 1,

        /// <summary>
        /// Defines the Des.
        /// </summary>
        Des = 26113, // 0x00006601

        /// <summary>
        /// Defines the RC2.
        /// </summary>
        RC2 = 26114, // 0x00006602

        /// <summary>
        /// Defines the TripleDes168.
        /// </summary>
        TripleDes168 = 26115, // 0x00006603

        /// <summary>
        /// Defines the TripleDes112.
        /// </summary>
        TripleDes112 = 26121, // 0x00006609

        /// <summary>
        /// Defines the Aes128.
        /// </summary>
        Aes128 = 26126, // 0x0000660E

        /// <summary>
        /// Defines the Aes192.
        /// </summary>
        Aes192 = 26127, // 0x0000660F

        /// <summary>
        /// Defines the Aes256.
        /// </summary>
        Aes256 = 26128, // 0x00006610

        /// <summary>
        /// Defines the RC2Corrected.
        /// </summary>
        RC2Corrected = 26370, // 0x00006702

        /// <summary>
        /// Defines the Blowfish.
        /// </summary>
        Blowfish = 26400, // 0x00006720

        /// <summary>
        /// Defines the Twofish.
        /// </summary>
        Twofish = 26401, // 0x00006721

        /// <summary>
        /// Defines the RC4.
        /// </summary>
        RC4 = 26625, // 0x00006801

        /// <summary>
        /// Defines the Unknown.
        /// </summary>
        Unknown = 65535, // 0x0000FFFF
    }
}
