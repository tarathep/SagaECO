namespace ICSharpCode.SharpZipLib.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="ScanEventArgs" />.
    /// </summary>
    public class ScanEventArgs : EventArgs
    {
        /// <summary>
        /// Defines the continueRunning_.
        /// </summary>
        private bool continueRunning_ = true;

        /// <summary>
        /// Defines the name_.
        /// </summary>
        private string name_;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public ScanEventArgs(string name)
        {
            this.name_ = name;
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
