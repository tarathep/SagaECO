namespace ICSharpCode.SharpZipLib.Core
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="PathFilter" />.
    /// </summary>
    public class PathFilter : IScanFilter
    {
        /// <summary>
        /// Defines the nameFilter_.
        /// </summary>
        private NameFilter nameFilter_;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathFilter"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="string"/>.</param>
        public PathFilter(string filter)
        {
            this.nameFilter_ = new NameFilter(filter);
        }

        /// <summary>
        /// The IsMatch.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool IsMatch(string name)
        {
            bool flag = false;
            if (name != null)
                flag = this.nameFilter_.IsMatch(name.Length > 0 ? Path.GetFullPath(name) : "");
            return flag;
        }
    }
}
