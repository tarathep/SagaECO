namespace SagaLib.VirtualFileSystem
{
    /// <summary>
    /// Defines the <see cref="VirtualFileSystemManager" />.
    /// </summary>
    public class VirtualFileSystemManager : Singleton<VirtualFileSystemManager>
    {
        /// <summary>
        /// Defines the fs.
        /// </summary>
        private IFileSystem fs;

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="type">The type<see cref="FileSystems"/>.</param>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Init(FileSystems type, string path)
        {
            if (this.fs != null)
                this.fs.Close();
            switch (type)
            {
                case FileSystems.Real:
                    this.fs = (IFileSystem)new RealFileSystem();
                    break;
                case FileSystems.LPK:
                    this.fs = (IFileSystem)new LPKFileSystem();
                    break;
            }
            return this.fs.Init(path);
        }

        /// <summary>
        /// Gets the FileSystem.
        /// </summary>
        public IFileSystem FileSystem
        {
            get
            {
                return this.fs;
            }
        }
    }
}
