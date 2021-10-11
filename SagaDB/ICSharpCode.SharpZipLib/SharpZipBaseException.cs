namespace ICSharpCode.SharpZipLib
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="SharpZipBaseException" />.
    /// </summary>
    [Serializable]
    public class SharpZipBaseException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpZipBaseException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected SharpZipBaseException(SerializationInfo info, StreamingContext context)
      : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpZipBaseException"/> class.
        /// </summary>
        public SharpZipBaseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpZipBaseException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public SharpZipBaseException(string message)
      : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpZipBaseException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public SharpZipBaseException(string message, Exception innerException)
      : base(message, innerException)
        {
        }
    }
}
