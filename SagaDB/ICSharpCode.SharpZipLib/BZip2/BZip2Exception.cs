namespace ICSharpCode.SharpZipLib.BZip2
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="BZip2Exception" />.
    /// </summary>
    [Serializable]
    public class BZip2Exception : SharpZipBaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BZip2Exception"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected BZip2Exception(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BZip2Exception"/> class.
        /// </summary>
        public BZip2Exception()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BZip2Exception"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public BZip2Exception(string message)
          : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BZip2Exception"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public BZip2Exception(string message, Exception exception)
          : base(message, exception)
        {
        }
    }
}
