namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="NameAndSizeFilter" />.
    /// </summary>
    [Obsolete("Use ExtendedPathFilter instead")]
    public class NameAndSizeFilter : PathFilter
    {
        /// <summary>
        /// Defines the maxSize_.
        /// </summary>
        private long maxSize_ = long.MaxValue;

        /// <summary>
        /// Defines the minSize_.
        /// </summary>
        private long minSize_;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameAndSizeFilter"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="string"/>.</param>
        /// <param name="minSize">The minSize<see cref="long"/>.</param>
        /// <param name="maxSize">The maxSize<see cref="long"/>.</param>
        public NameAndSizeFilter(string filter, long minSize, long maxSize)
      : base(filter)
        {
            this.MinSize = minSize;
            this.MaxSize = maxSize;
        }

        /// <summary>
        /// The IsMatch.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsMatch(string name)
        {
            bool flag = base.IsMatch(name);
            if (flag)
            {
                long length = new FileInfo(name).Length;
                flag = this.MinSize <= length && this.MaxSize >= length;
            }
            return flag;
        }

        /// <summary>
        /// Gets or sets the MinSize.
        /// </summary>
        public long MinSize
        {
            get
            {
                return this.minSize_;
            }
            set
            {
                if (value < 0L || this.maxSize_ < value)
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.minSize_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxSize.
        /// </summary>
        public long MaxSize
        {
            get
            {
                return this.maxSize_;
            }
            set
            {
                if (value < 0L || this.minSize_ > value)
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.maxSize_ = value;
            }
        }
    }
}
