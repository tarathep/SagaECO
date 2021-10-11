namespace SevenZip
{
    using System;

    /// <summary>
    /// The exception that is thrown when the value of an argument is outside the allowable range.
    /// </summary>
    internal class InvalidParamException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidParamException"/> class.
        /// </summary>
        public InvalidParamException()
      : base("Invalid Parameter")
        {
        }
    }
}
