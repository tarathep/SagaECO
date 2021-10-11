namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the <see cref="TestStatus" />.
    /// </summary>
    public class TestStatus
    {
        /// <summary>
        /// Defines the file_.
        /// </summary>
        private ZipFile file_;

        /// <summary>
        /// Defines the entry_.
        /// </summary>
        private ZipEntry entry_;

        /// <summary>
        /// Defines the entryValid_.
        /// </summary>
        private bool entryValid_;

        /// <summary>
        /// Defines the errorCount_.
        /// </summary>
        private int errorCount_;

        /// <summary>
        /// Defines the bytesTested_.
        /// </summary>
        private long bytesTested_;

        /// <summary>
        /// Defines the operation_.
        /// </summary>
        private TestOperation operation_;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStatus"/> class.
        /// </summary>
        /// <param name="file">The file<see cref="ZipFile"/>.</param>
        public TestStatus(ZipFile file)
        {
            this.file_ = file;
        }

        /// <summary>
        /// Gets the Operation.
        /// </summary>
        public TestOperation Operation
        {
            get
            {
                return this.operation_;
            }
        }

        /// <summary>
        /// Gets the File.
        /// </summary>
        public ZipFile File
        {
            get
            {
                return this.file_;
            }
        }

        /// <summary>
        /// Gets the Entry.
        /// </summary>
        public ZipEntry Entry
        {
            get
            {
                return this.entry_;
            }
        }

        /// <summary>
        /// Gets the ErrorCount.
        /// </summary>
        public int ErrorCount
        {
            get
            {
                return this.errorCount_;
            }
        }

        /// <summary>
        /// Gets the BytesTested.
        /// </summary>
        public long BytesTested
        {
            get
            {
                return this.bytesTested_;
            }
        }

        /// <summary>
        /// Gets a value indicating whether EntryValid.
        /// </summary>
        public bool EntryValid
        {
            get
            {
                return this.entryValid_;
            }
        }

        /// <summary>
        /// The AddError.
        /// </summary>
        internal void AddError()
        {
            ++this.errorCount_;
            this.entryValid_ = false;
        }

        /// <summary>
        /// The SetOperation.
        /// </summary>
        /// <param name="operation">The operation<see cref="TestOperation"/>.</param>
        internal void SetOperation(TestOperation operation)
        {
            this.operation_ = operation;
        }

        /// <summary>
        /// The SetEntry.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        internal void SetEntry(ZipEntry entry)
        {
            this.entry_ = entry;
            this.entryValid_ = true;
            this.bytesTested_ = 0L;
        }

        /// <summary>
        /// The SetBytesTested.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        internal void SetBytesTested(long value)
        {
            this.bytesTested_ = value;
        }
    }
}
