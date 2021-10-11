namespace ICSharpCode.SharpZipLib.Zip
{
    using System;

    /// <summary>
    /// Defines the <see cref="KeysRequiredEventArgs" />.
    /// </summary>
    public class KeysRequiredEventArgs : EventArgs
    {
        /// <summary>
        /// Defines the fileName.
        /// </summary>
        private string fileName;

        /// <summary>
        /// Defines the key.
        /// </summary>
        private byte[] key;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeysRequiredEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public KeysRequiredEventArgs(string name)
        {
            this.fileName = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeysRequiredEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="keyValue">The keyValue<see cref="byte[]"/>.</param>
        public KeysRequiredEventArgs(string name, byte[] keyValue)
        {
            this.fileName = name;
            this.key = keyValue;
        }

        /// <summary>
        /// Gets the FileName.
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public byte[] Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }
    }
}
