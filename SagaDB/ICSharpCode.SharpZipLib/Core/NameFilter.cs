namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="NameFilter" />.
    /// </summary>
    public class NameFilter : IScanFilter
    {
        /// <summary>
        /// Defines the filter_.
        /// </summary>
        private string filter_;

        /// <summary>
        /// Defines the inclusions_.
        /// </summary>
        private ArrayList inclusions_;

        /// <summary>
        /// Defines the exclusions_.
        /// </summary>
        private ArrayList exclusions_;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameFilter"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="string"/>.</param>
        public NameFilter(string filter)
        {
            this.filter_ = filter;
            this.inclusions_ = new ArrayList();
            this.exclusions_ = new ArrayList();
            this.Compile();
        }

        /// <summary>
        /// The IsValidExpression.
        /// </summary>
        /// <param name="expression">The expression<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidExpression(string expression)
        {
            bool flag = true;
            try
            {
                Regex regex = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            catch (ArgumentException ex)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// The IsValidFilterExpression.
        /// </summary>
        /// <param name="toTest">The toTest<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidFilterExpression(string toTest)
        {
            if (toTest == null)
                throw new ArgumentNullException(nameof(toTest));
            bool flag = true;
            try
            {
                string[] strArray = NameFilter.SplitQuoted(toTest);
                for (int index = 0; index < strArray.Length; ++index)
                {
                    if (strArray[index] != null && strArray[index].Length > 0)
                    {
                        Regex regex = new Regex(strArray[index][0] != '+' ? (strArray[index][0] != '-' ? strArray[index] : strArray[index].Substring(1, strArray[index].Length - 1)) : strArray[index].Substring(1, strArray[index].Length - 1), RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// The SplitQuoted.
        /// </summary>
        /// <param name="original">The original<see cref="string"/>.</param>
        /// <returns>The <see cref="string[]"/>.</returns>
        public static string[] SplitQuoted(string original)
        {
            char ch = '\\';
            char[] array = new char[1] { ';' };
            ArrayList arrayList = new ArrayList();
            if (original != null && original.Length > 0)
            {
                int index = -1;
                StringBuilder stringBuilder = new StringBuilder();
                while (index < original.Length)
                {
                    ++index;
                    if (index >= original.Length)
                        arrayList.Add((object)stringBuilder.ToString());
                    else if ((int)original[index] == (int)ch)
                    {
                        ++index;
                        if (index >= original.Length)
                            throw new ArgumentException("Missing terminating escape character", nameof(original));
                        stringBuilder.Append(original[index]);
                    }
                    else if (Array.IndexOf<char>(array, original[index]) >= 0)
                    {
                        arrayList.Add((object)stringBuilder.ToString());
                        stringBuilder.Length = 0;
                    }
                    else
                        stringBuilder.Append(original[index]);
                }
            }
            return (string[])arrayList.ToArray(typeof(string));
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.filter_;
        }

        /// <summary>
        /// The IsIncluded.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsIncluded(string name)
        {
            bool flag = false;
            if (this.inclusions_.Count == 0)
            {
                flag = true;
            }
            else
            {
                foreach (Regex inclusion in this.inclusions_)
                {
                    if (inclusion.IsMatch(name))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// The IsExcluded.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsExcluded(string name)
        {
            bool flag = false;
            foreach (Regex exclusion in this.exclusions_)
            {
                if (exclusion.IsMatch(name))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// The IsMatch.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsMatch(string name)
        {
            return this.IsIncluded(name) && !this.IsExcluded(name);
        }

        /// <summary>
        /// The Compile.
        /// </summary>
        private void Compile()
        {
            if (this.filter_ == null)
                return;
            string[] strArray = NameFilter.SplitQuoted(this.filter_);
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index] != null && strArray[index].Length > 0)
                {
                    bool flag = strArray[index][0] != '-';
                    string pattern = strArray[index][0] != '+' ? (strArray[index][0] != '-' ? strArray[index] : strArray[index].Substring(1, strArray[index].Length - 1)) : strArray[index].Substring(1, strArray[index].Length - 1);
                    if (flag)
                        this.inclusions_.Add((object)new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
                    else
                        this.exclusions_.Add((object)new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
                }
            }
        }
    }
}
