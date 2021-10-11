namespace ICSharpCode.SharpZipLib.GZip
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="GZipException" />.
    /// </summary>
    [Serializable]
    public class GZipException : SharpZipBaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GZipException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected GZipException(SerializationInfo info, StreamingContext context)
      : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipException"/> class.
        /// </summary>
        public GZipException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public GZipException(string message)
      : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public GZipException(string message, Exception innerException)
      : base(message, innerException)
        {
        }
    }
}
