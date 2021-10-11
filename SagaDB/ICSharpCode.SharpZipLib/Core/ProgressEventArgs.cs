namespace ICSharpCode.SharpZipLib.Core
{
    using System;

    /// <summary>
    /// Defines the <see cref="ProgressEventArgs" />.
    /// </summary>
    public class ProgressEventArgs : EventArgs
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
        /// Defines the processed_.
        /// </summary>
        private long processed_;

        /// <summary>
        /// Defines the target_.
        /// </summary>
        private long target_;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="processed">The processed<see cref="long"/>.</param>
        /// <param name="target">The target<see cref="long"/>.</param>
        public ProgressEventArgs(string name, long processed, long target)
        {
            this.name_ = name;
            this.processed_ = processed;
            this.target_ = target;
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

        /// <summary>
        /// Gets the PercentComplete.
        /// </summary>
        public float PercentComplete
        {
            get
            {
                if (this.target_ <= 0L)
                    return 0.0f;
                return (float)((double)this.processed_ / (double)this.target_ * 100.0);
            }
        }

        /// <summary>
        /// Gets the Processed.
        /// </summary>
        public long Processed
        {
            get
            {
                return this.processed_;
            }
        }

        /// <summary>
        /// Gets the Target.
        /// </summary>
        public long Target
        {
            get
            {
                return this.target_;
            }
        }
    }
}
