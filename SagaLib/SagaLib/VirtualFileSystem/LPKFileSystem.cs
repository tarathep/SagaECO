namespace SagaLib.VirtualFileSystem
{
    using SagaLib.VirtualFileSystem.Lpk;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="LPKFileSystem" />.
    /// </summary>
    public class LPKFileSystem : IFileSystem
    {
        /// <summary>
        /// Defines the lpk.
        /// </summary>
        private LpkFile lpk;

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Init(string path)
        {
            try
            {
                this.lpk = new LpkFile((Stream)new FileStream(path, FileMode.Open, FileAccess.Read));
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// The OpenFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public Stream OpenFile(string path)
        {
            path = path.Replace("/", "\\");
            if (this.lpk.Exists(path))
                return (Stream)this.lpk.OpenFile(path);
            throw new IOException("Cannot find file:" + path);
        }

        /// <summary>
        /// The SearchFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="pattern">The pattern<see cref="string"/>.</param>
        /// <returns>The <see cref="string[]"/>.</returns>
        public string[] SearchFile(string path, string pattern)
        {
            return this.SearchFile(path, pattern, SearchOption.AllDirectories);
        }

        /// <summary>
        /// The SearchFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="pattern">The pattern<see cref="string"/>.</param>
        /// <param name="option">The option<see cref="SearchOption"/>.</param>
        /// <returns>The <see cref="string[]"/>.</returns>
        public string[] SearchFile(string path, string pattern, SearchOption option)
        {
            List<LpkFileInfo> getFileNames = this.lpk.GetFileNames;
            List<string> stringList = new List<string>();
            if (path.Substring(path.Length - 1) != "/" && path.Substring(path.Length - 1) != "\\")
                path += "\\";
            path = path.Replace("/", "\\");
            pattern = pattern.Replace("*", "\\w*");
            foreach (LpkFileInfo lpkFileInfo in getFileNames)
            {
                if (lpkFileInfo.Name.StartsWith(path))
                {
                    string[] strArray = lpkFileInfo.Name.Replace(path, "").Split('\\');
                    if ((option != SearchOption.TopDirectoryOnly || strArray.Length <= 1) && Regex.IsMatch(strArray[strArray.Length - 1], pattern, RegexOptions.IgnoreCase))
                        stringList.Add(lpkFileInfo.Name);
                }
            }
            return stringList.ToArray();
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public void Close()
        {
            this.lpk.Close();
        }
    }
}
