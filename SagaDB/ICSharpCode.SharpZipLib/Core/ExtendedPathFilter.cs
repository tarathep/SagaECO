namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ExtendedPathFilter" />.
    /// </summary>
    public class ExtendedPathFilter : PathFilter
    {
        /// <summary>
        /// Defines the maxSize_.
        /// </summary>
        private long maxSize_ = long.MaxValue;

        /// <summary>
        /// Defines the minDate_.
        /// </summary>
        private DateTime minDate_ = DateTime.MinValue;

        /// <summary>
        /// Defines the maxDate_.
        /// </summary>
        private DateTime maxDate_ = DateTime.MaxValue;

        /// <summary>
        /// Defines the minSize_.
        /// </summary>
        private long minSize_;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedPathFilter"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="string"/>.</param>
        /// <param name="minSize">The minSize<see cref="long"/>.</param>
        /// <param name="maxSize">The maxSize<see cref="long"/>.</param>
        public ExtendedPathFilter(string filter, long minSize, long maxSize)
      : base(filter)
        {
            this.MinSize = minSize;
            this.MaxSize = maxSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedPathFilter"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="string"/>.</param>
        /// <param name="minDate">The minDate<see cref="DateTime"/>.</param>
        /// <param name="maxDate">The maxDate<see cref="DateTime"/>.</param>
        public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate)
      : base(filter)
        {
            this.MinDate = minDate;
            this.MaxDate = maxDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedPathFilter"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="string"/>.</param>
        /// <param name="minSize">The minSize<see cref="long"/>.</param>
        /// <param name="maxSize">The maxSize<see cref="long"/>.</param>
        /// <param name="minDate">The minDate<see cref="DateTime"/>.</param>
        /// <param name="maxDate">The maxDate<see cref="DateTime"/>.</param>
        public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate)
      : base(filter)
        {
            this.MinSize = minSize;
            this.MaxSize = maxSize;
            this.MinDate = minDate;
            this.MaxDate = maxDate;
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
                FileInfo fileInfo = new FileInfo(name);
                flag = this.MinSize <= fileInfo.Length && this.MaxSize >= fileInfo.Length && this.MinDate <= fileInfo.LastWriteTime && this.MaxDate >= fileInfo.LastWriteTime;
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

        /// <summary>
        /// Gets or sets the MinDate.
        /// </summary>
        public DateTime MinDate
        {
            get
            {
                return this.minDate_;
            }
            set
            {
                if (value > this.maxDate_)
                    throw new ArgumentOutOfRangeException(nameof(value), "Exceeds MaxDate");
                this.minDate_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxDate.
        /// </summary>
        public DateTime MaxDate
        {
            get
            {
                return this.maxDate_;
            }
            set
            {
                if (this.minDate_ > value)
                    throw new ArgumentOutOfRangeException(nameof(value), "Exceeds MinDate");
                this.maxDate_ = value;
            }
        }
    }
}
