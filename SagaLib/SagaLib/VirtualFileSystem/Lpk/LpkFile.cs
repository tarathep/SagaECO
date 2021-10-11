namespace SagaLib.VirtualFileSystem.Lpk
{
    using SagaLib.VirtualFileSystem.Lpk.LZMA;
    using SevenZip;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="LpkFile" />.
    /// </summary>
    public class LpkFile
    {
        /// <summary>
        /// Defines the key.
        /// </summary>
        private static byte[] key = Encoding.ASCII.GetBytes("1234567890123456");

        /// <summary>
        /// Defines the aes.
        /// </summary>
        private Rijndael aes = Rijndael.Create();

        /// <summary>
        /// Defines the infoBuf.
        /// </summary>
        private List<LpkFileInfo> infoBuf = new List<LpkFileInfo>();

        /// <summary>
        /// Defines the hashTable.
        /// </summary>
        private Dictionary<string, int> hashTable;

        /// <summary>
        /// Defines the fileStream.
        /// </summary>
        private Stream fileStream;

        /// <summary>
        /// Defines the hashSize.
        /// </summary>
        private int hashSize;

        /// <summary>
        /// Defines the hashOffset.
        /// </summary>
        private int hashOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="LpkFile"/> class.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public LpkFile(Stream stream)
        {
            this.fileStream = stream;
            this.aes.Mode = CipherMode.ECB;
            this.aes.KeySize = 128;
            this.aes.Padding = PaddingMode.None;
            if (stream.Length != 0L)
            {
                BinaryReader binaryReader = new BinaryReader(stream);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                if (binaryReader.ReadInt32() != 4935756)
                    throw new Exception("This is not a LPK Archive");
                try
                {
                    this.hashOffset = binaryReader.ReadInt32();
                    stream.Position = (long)this.hashOffset;
                    this.hashSize = binaryReader.ReadInt32();
                    this.hashTable = (Dictionary<string, int>)binaryFormatter.Deserialize((Stream)new MemoryStream(this.Decrypt(binaryReader.ReadBytes(this.hashSize))));
                }
                catch (Exception ex)
                {
                    throw new Exception("This Archive is corrupted and cannot be opened", ex);
                }
                if (this.hashTable == null)
                    throw new Exception("This Archive is corrupted and cannot be opened");
            }
            else
            {
                this.hashTable = new Dictionary<string, int>();
                BinaryWriter binaryWriter = new BinaryWriter(stream);
                binaryWriter.Write(4935756);
                this.hashSize = 0;
                this.hashOffset = 8;
                binaryWriter.Write(this.hashOffset);
                binaryWriter.Write(this.hashSize);
            }
            this.infoBuf = this.GetInfos();
        }

        /// <summary>
        /// The GetInfo.
        /// </summary>
        /// <param name="name">文件名.</param>
        /// <returns>元信息.</returns>
        public LpkFileInfo GetInfo(string name)
        {
            this.fileStream.Position = (long)this.hashTable[name];
            return new LpkFileInfo(this.fileStream)
            {
                Name = name
            };
        }

        /// <summary>
        /// The GetInfos.
        /// </summary>
        /// <returns>The <see cref="List{LpkFileInfo}"/>.</returns>
        private List<LpkFileInfo> GetInfos()
        {
            if (this.hashTable == null)
                return new List<LpkFileInfo>();
            List<LpkFileInfo> lpkFileInfoList = new List<LpkFileInfo>();
            foreach (string key in this.hashTable.Keys)
            {
                this.fileStream.Position = (long)this.hashTable[key];
                lpkFileInfoList.Add(new LpkFileInfo(this.fileStream)
                {
                    Name = key
                });
            }
            return lpkFileInfoList;
        }

        /// <summary>
        /// Gets the GetFileNames.
        /// </summary>
        public List<LpkFileInfo> GetFileNames
        {
            get
            {
                return this.infoBuf;
            }
        }

        /// <summary>
        /// The Exists.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <returns>.</returns>
        public bool Exists(string fileName)
        {
            return this.hashTable.ContainsKey(fileName);
        }

        /// <summary>
        /// Gets the FileCount.
        /// </summary>
        public int FileCount
        {
            get
            {
                return this.hashTable.Count;
            }
        }

        /// <summary>
        /// Gets the TotalSize.
        /// </summary>
        public long TotalSize
        {
            get
            {
                long num = 0;
                foreach (LpkFileInfo lpkFileInfo in this.infoBuf)
                    num += (long)lpkFileInfo.UncompressedSize;
                return num;
            }
        }

        /// <summary>
        /// Gets the TotalCompressedSize.
        /// </summary>
        public long TotalCompressedSize
        {
            get
            {
                long num = 0;
                foreach (LpkFileInfo lpkFileInfo in this.infoBuf)
                    num += (long)lpkFileInfo.FileSize;
                return num;
            }
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public void Close()
        {
            this.fileStream.Flush();
            this.fileStream.Close();
        }

        /// <summary>
        /// The AddFile.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <param name="inStream">要添加的文件的流.</param>
        public void AddFile(string fileName, Stream inStream)
        {
            this.AddFile(fileName, inStream, (ICodeProgress)null);
        }

        /// <summary>
        /// The AddFile.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <param name="inStream">要添加的文件的流.</param>
        /// <param name="progress">压缩进度回调对象.</param>
        public void AddFile(string fileName, Stream inStream, ICodeProgress progress)
        {
            lock (this.hashTable)
            {
                byte[] buffer = new byte[LpkFileInfo.Size * this.hashTable.Count];
                int hashOffset = this.hashOffset;
                BinaryWriter binaryWriter = new BinaryWriter(this.fileStream);
                if (this.hashTable.ContainsKey(fileName))
                    throw new ArgumentException("A file with this name(" + fileName + ") already exists!");
                uint num1 = LzmaHelper.CRC32(inStream);
                inStream.Position = 0L;
                uint length1 = (uint)inStream.Length;
                this.fileStream.Position = (long)(this.hashOffset + this.hashSize + 4);
                this.fileStream.Read(buffer, 0, LpkFileInfo.Size * this.hashTable.Count);
                this.hashTable.Add(fileName, 0);
                int length2 = this.HashBuffer.Length;
                this.fileStream.Position = (long)this.hashOffset;
                LzmaHelper.Compress(inStream, this.fileStream, progress);
                uint num2 = (uint)((ulong)this.fileStream.Position - (ulong)this.hashOffset);
                this.hashOffset = (int)this.fileStream.Position;
                this.fileStream.Position += (long)(length2 + 4);
                this.fileStream.Write(buffer, 0, buffer.Length);
                this.fileStream.Position = 4L;
                binaryWriter.Write(this.hashOffset);
                this.fileStream.Position = (long)(this.hashOffset + 4 + length2);
                string[] array = new string[this.hashTable.Count];
                this.hashTable.Keys.CopyTo(array, 0);
                foreach (string index1 in array)
                {
                    LpkFileInfo lpkFileInfo;
                    if (index1 != fileName)
                    {
                        Dictionary<string, int> hashTable;
                        string index2;
                        (hashTable = this.hashTable)[index2 = index1] = hashTable[index2] + (this.hashOffset - hashOffset + (length2 - this.hashSize));
                        this.fileStream.Position = (long)this.hashTable[index1];
                        lpkFileInfo = new LpkFileInfo(this.fileStream);
                        lpkFileInfo.HeaderOffset = (uint)this.hashTable[index1];
                    }
                    else
                    {
                        this.hashTable[index1] = 4 + this.hashOffset + length2 + (this.hashTable.Count - 1) * LpkFileInfo.Size;
                        lpkFileInfo = new LpkFileInfo();
                        lpkFileInfo.HeaderOffset = (uint)this.hashTable[index1];
                        lpkFileInfo.DataOffset = (uint)hashOffset;
                        lpkFileInfo.UncompressedSize = length1;
                        lpkFileInfo.CRC = num1;
                        lpkFileInfo.FileSize = num2;
                    }
                    lpkFileInfo.WriteToStream(this.fileStream);
                }
                this.fileStream.Position = (long)this.hashOffset;
                binaryWriter.Write(length2);
                binaryWriter.Write(this.Encrypt(this.HashBuffer));
                this.hashSize = length2;
                this.infoBuf = this.GetInfos();
            }
        }

        /// <summary>
        /// The OpenFile.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <returns>.</returns>
        public MemoryStream OpenFile(string fileName)
        {
            return this.OpenFile(fileName, (ICodeProgress)null);
        }

        /// <summary>
        /// The OpenFile.
        /// </summary>
        /// <param name="fileName">文件名.</param>
        /// <param name="progress">解压进度回调对象.</param>
        /// <returns>.</returns>
        public MemoryStream OpenFile(string fileName, ICodeProgress progress)
        {
            lock (this.hashTable)
            {
                MemoryStream memoryStream = new MemoryStream();
                this.fileStream.Position = (long)this.hashTable[fileName];
                LpkFileInfo lpkFileInfo = new LpkFileInfo(this.fileStream);
                this.fileStream.Position = (long)lpkFileInfo.DataOffset;
                try
                {
                    LzmaHelper.Decompress(this.fileStream, (Stream)memoryStream, (long)lpkFileInfo.FileSize, (long)lpkFileInfo.UncompressedSize, progress);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    throw new Exception(string.Format("File:{1} CRC({0:X}) error, file open failed!", (object)lpkFileInfo.CRC, (object)fileName));
                }
                memoryStream.Position = 0L;
                uint num = LzmaHelper.CRC32((Stream)memoryStream);
                if ((int)lpkFileInfo.CRC != (int)num)
                    throw new Exception(string.Format("CRC(Original:{0:X} Current:{1:X}) error, file open failed!", (object)lpkFileInfo.CRC, (object)num));
                memoryStream.Position = 0L;
                return memoryStream;
            }
        }

        /// <summary>
        /// The Encrypt.
        /// </summary>
        /// <param name="buff">The buff<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        internal byte[] Encrypt(byte[] buff)
        {
            byte[] numArray1 = new byte[buff.Length];
            int length = buff.Length % 1024;
            ICryptoTransform encryptor = this.aes.CreateEncryptor(LpkFile.key, new byte[16]);
            byte[] numArray2 = new byte[1024];
            for (int index = 0; index < buff.Length / 1024; ++index)
            {
                Array.Copy((Array)buff, index * 1024, (Array)numArray2, 0, 1024);
                encryptor.TransformBlock(numArray2, 0, 1024, numArray2, 0);
                Array.Copy((Array)numArray2, 0, (Array)numArray1, index * 1024, 1024);
            }
            Array.Copy((Array)buff, buff.Length - length, (Array)numArray1, buff.Length - length, length);
            return numArray1;
        }

        /// <summary>
        /// The Decrypt.
        /// </summary>
        /// <param name="buff">The buff<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        internal byte[] Decrypt(byte[] buff)
        {
            byte[] numArray1 = new byte[buff.Length];
            int length = buff.Length % 1024;
            ICryptoTransform decryptor = this.aes.CreateDecryptor(LpkFile.key, new byte[16]);
            byte[] numArray2 = new byte[1024];
            for (int index = 0; index < buff.Length / 1024; ++index)
            {
                Array.Copy((Array)buff, index * 1024, (Array)numArray2, 0, 1024);
                decryptor.TransformBlock(numArray2, 0, 1024, numArray2, 0);
                Array.Copy((Array)numArray2, 0, (Array)numArray1, index * 1024, 1024);
            }
            Array.Copy((Array)buff, buff.Length - length, (Array)numArray1, buff.Length - length, length);
            return numArray1;
        }

        /// <summary>
        /// Gets the HashBuffer.
        /// </summary>
        internal byte[] HashBuffer
        {
            get
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                binaryFormatter.Serialize((Stream)memoryStream, (object)this.hashTable);
                memoryStream.Flush();
                long num = 1024L - memoryStream.Length % 1024L;
                memoryStream.SetLength(memoryStream.Length + num);
                memoryStream.Close();
                return memoryStream.ToArray();
            }
        }
    }
}
