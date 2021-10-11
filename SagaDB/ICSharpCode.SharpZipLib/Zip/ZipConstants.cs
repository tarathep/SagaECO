namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="ZipConstants" />.
    /// </summary>
    public sealed class ZipConstants
    {
        /// <summary>
        /// Defines the defaultCodePage.
        /// </summary>
        private static int defaultCodePage = Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage;

        /// <summary>
        /// Defines the VersionMadeBy.
        /// </summary>
        public const int VersionMadeBy = 45;

        /// <summary>
        /// Defines the VERSION_MADE_BY.
        /// </summary>
        [Obsolete("Use VersionMadeBy instead")]
        public const int VERSION_MADE_BY = 45;

        /// <summary>
        /// Defines the VersionStrongEncryption.
        /// </summary>
        public const int VersionStrongEncryption = 50;

        /// <summary>
        /// Defines the VERSION_STRONG_ENCRYPTION.
        /// </summary>
        [Obsolete("Use VersionStrongEncryption instead")]
        public const int VERSION_STRONG_ENCRYPTION = 50;

        /// <summary>
        /// Defines the VersionZip64.
        /// </summary>
        public const int VersionZip64 = 45;

        /// <summary>
        /// Defines the LocalHeaderBaseSize.
        /// </summary>
        public const int LocalHeaderBaseSize = 30;

        /// <summary>
        /// Defines the LOCHDR.
        /// </summary>
        [Obsolete("Use LocalHeaderBaseSize instead")]
        public const int LOCHDR = 30;

        /// <summary>
        /// Defines the Zip64DataDescriptorSize.
        /// </summary>
        public const int Zip64DataDescriptorSize = 24;

        /// <summary>
        /// Defines the DataDescriptorSize.
        /// </summary>
        public const int DataDescriptorSize = 16;

        /// <summary>
        /// Defines the EXTHDR.
        /// </summary>
        [Obsolete("Use DataDescriptorSize instead")]
        public const int EXTHDR = 16;

        /// <summary>
        /// Defines the CentralHeaderBaseSize.
        /// </summary>
        public const int CentralHeaderBaseSize = 46;

        /// <summary>
        /// Defines the CENHDR.
        /// </summary>
        [Obsolete("Use CentralHeaderBaseSize instead")]
        public const int CENHDR = 46;

        /// <summary>
        /// Defines the EndOfCentralRecordBaseSize.
        /// </summary>
        public const int EndOfCentralRecordBaseSize = 22;

        /// <summary>
        /// Defines the ENDHDR.
        /// </summary>
        [Obsolete("Use EndOfCentralRecordBaseSize instead")]
        public const int ENDHDR = 22;

        /// <summary>
        /// Defines the CryptoHeaderSize.
        /// </summary>
        public const int CryptoHeaderSize = 12;

        /// <summary>
        /// Defines the CRYPTO_HEADER_SIZE.
        /// </summary>
        [Obsolete("Use CryptoHeaderSize instead")]
        public const int CRYPTO_HEADER_SIZE = 12;

        /// <summary>
        /// Defines the LocalHeaderSignature.
        /// </summary>
        public const int LocalHeaderSignature = 67324752;

        /// <summary>
        /// Defines the LOCSIG.
        /// </summary>
        [Obsolete("Use LocalHeaderSignature instead")]
        public const int LOCSIG = 67324752;

        /// <summary>
        /// Defines the SpanningSignature.
        /// </summary>
        public const int SpanningSignature = 134695760;

        /// <summary>
        /// Defines the SPANNINGSIG.
        /// </summary>
        [Obsolete("Use SpanningSignature instead")]
        public const int SPANNINGSIG = 134695760;

        /// <summary>
        /// Defines the SpanningTempSignature.
        /// </summary>
        public const int SpanningTempSignature = 808471376;

        /// <summary>
        /// Defines the SPANTEMPSIG.
        /// </summary>
        [Obsolete("Use SpanningTempSignature instead")]
        public const int SPANTEMPSIG = 808471376;

        /// <summary>
        /// Defines the DataDescriptorSignature.
        /// </summary>
        public const int DataDescriptorSignature = 134695760;

        /// <summary>
        /// Defines the EXTSIG.
        /// </summary>
        [Obsolete("Use DataDescriptorSignature instead")]
        public const int EXTSIG = 134695760;

        /// <summary>
        /// Defines the CENSIG.
        /// </summary>
        [Obsolete("Use CentralHeaderSignature instead")]
        public const int CENSIG = 33639248;

        /// <summary>
        /// Defines the CentralHeaderSignature.
        /// </summary>
        public const int CentralHeaderSignature = 33639248;

        /// <summary>
        /// Defines the Zip64CentralFileHeaderSignature.
        /// </summary>
        public const int Zip64CentralFileHeaderSignature = 101075792;

        /// <summary>
        /// Defines the CENSIG64.
        /// </summary>
        [Obsolete("Use Zip64CentralFileHeaderSignature instead")]
        public const int CENSIG64 = 101075792;

        /// <summary>
        /// Defines the Zip64CentralDirLocatorSignature.
        /// </summary>
        public const int Zip64CentralDirLocatorSignature = 117853008;

        /// <summary>
        /// Defines the ArchiveExtraDataSignature.
        /// </summary>
        public const int ArchiveExtraDataSignature = 117853008;

        /// <summary>
        /// Defines the CentralHeaderDigitalSignature.
        /// </summary>
        public const int CentralHeaderDigitalSignature = 84233040;

        /// <summary>
        /// Defines the CENDIGITALSIG.
        /// </summary>
        [Obsolete("Use CentralHeaderDigitalSignaure instead")]
        public const int CENDIGITALSIG = 84233040;

        /// <summary>
        /// Defines the EndOfCentralDirectorySignature.
        /// </summary>
        public const int EndOfCentralDirectorySignature = 101010256;

        /// <summary>
        /// Defines the ENDSIG.
        /// </summary>
        [Obsolete("Use EndOfCentralDirectorySignature instead")]
        public const int ENDSIG = 101010256;

        /// <summary>
        /// Gets or sets the DefaultCodePage.
        /// </summary>
        public static int DefaultCodePage
        {
            get
            {
                return ZipConstants.defaultCodePage;
            }
            set
            {
                ZipConstants.defaultCodePage = value;
            }
        }

        /// <summary>
        /// The ConvertToString.
        /// </summary>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ConvertToString(byte[] data, int count)
        {
            if (data == null)
                return string.Empty;
            return Encoding.ASCII.GetString(data, 0, count);
        }

        /// <summary>
        /// The ConvertToString.
        /// </summary>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ConvertToString(byte[] data)
        {
            if (data == null)
                return string.Empty;
            return ZipConstants.ConvertToString(data, data.Length);
        }

        /// <summary>
        /// The ConvertToStringExt.
        /// </summary>
        /// <param name="flags">The flags<see cref="int"/>.</param>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ConvertToStringExt(int flags, byte[] data, int count)
        {
            if (data == null)
                return string.Empty;
            if ((flags & 2048) != 0)
                return Encoding.UTF8.GetString(data, 0, count);
            return ZipConstants.ConvertToString(data, count);
        }

        /// <summary>
        /// The ConvertToStringExt.
        /// </summary>
        /// <param name="flags">The flags<see cref="int"/>.</param>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ConvertToStringExt(int flags, byte[] data)
        {
            if (data == null)
                return string.Empty;
            if ((flags & 2048) != 0)
                return Encoding.UTF8.GetString(data, 0, data.Length);
            return ZipConstants.ConvertToString(data, data.Length);
        }

        /// <summary>
        /// The ConvertToArray.
        /// </summary>
        /// <param name="str">The str<see cref="string"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public static byte[] ConvertToArray(string str)
        {
            if (str == null)
                return new byte[0];
            return Encoding.GetEncoding(ZipConstants.DefaultCodePage).GetBytes(str);
        }

        /// <summary>
        /// The ConvertToArray.
        /// </summary>
        /// <param name="flags">The flags<see cref="int"/>.</param>
        /// <param name="str">The str<see cref="string"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public static byte[] ConvertToArray(int flags, string str)
        {
            if (str == null)
                return new byte[0];
            if ((flags & 2048) != 0)
                return Encoding.UTF8.GetBytes(str);
            return ZipConstants.ConvertToArray(str);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ZipConstants"/> class from being created.
        /// </summary>
        private ZipConstants()
        {
        }
    }
}
