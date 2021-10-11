namespace ICSharpCode.SharpZipLib.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="ScanFailureEventArgs" />.
    /// </summary>
    public class ScanFailureEventArgs : EventArgs
    {
        /// <summary>
        /// Defines the name_.
        /// </summary>
        private string name_;

        /// <summary>
        /// Defines the exception_.
        /// </summary>
        private Exception exception_;

        /// <summary>
        /// Defines the continueRunning_.
        /// </summary>
        private bool continueRunning_;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanFailureEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="e">The e<see cref="Exception"/>.</param>
        public ScanFailureEventArgs(string name, Exception e)
        {
            this.name_ = name;
            this.exception_ = e;
            this.continueRunning_ = true;
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name_;
            }
        }

        /// <summary>
        /// Gets the Exception.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return this.exception_;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ContinueRunning.
        /// </summary>
        public bool ContinueRunning
        {
            get
            {
                return this.continueRunning_;
            }
            set
            {
                this.continueRunning_ = value;
            }
        }
    }
}
