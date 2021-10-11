namespace SagaLib
{
    using System;

    /// <summary>
    /// Defines the base class of a network packet. Packets are send back and forth between the client
    /// and server. Different types of packets are used for different purposes. The general packet structure
    /// is: PACKET_SIZE (2 bytes), PACKET_ID (2 bytes), PACKET_DATA (x bytes). The id bytes are considered to
    /// be part of the data bytes.
    /// The size bytes are unencrypted, but the id bytes and all data following are encrypted.
    /// </summary>
    [Serializable]
    public class Packet
    {
        /// <summary>
        /// The size of the packet is equal to the number of data bytes plus 2 bytes for the message id plus 2 bytes for the size..
        /// </summary>
        public uint size;

        /// <summary>
        /// The data bytes (note: these include the id bytes and the size bytes).
        /// </summary>
        public byte[] data;

        /// <summary>
        /// Our current offset in the data array. After creation this will be set to 4 (the first
        /// non ID data byte).
        /// </summary>
        public ushort offset;

        /// <summary>
        /// If true, the data byte array will be cloned before it gets encrypted.
        /// Set it to "true" if you want to send the packet multiple times..
        /// </summary>
        public bool doNotEncryptBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="length">Length of the data bytes.</param>
        public Packet(uint length)
        {
            this.size = length;
            this.data = new byte[length];
            this.offset = (ushort)4;
            this.doNotEncryptBuffer = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        public Packet()
        {
            this.size = 0U;
            this.offset = (ushort)4;
            this.doNotEncryptBuffer = false;
        }

        /// <summary>
        /// Check to see if a given size is ok for a certain packet.
        /// </summary>
        /// <param name="size">Size to compare with.</param>
        /// <returns>true: size is ok. false: size is not ok.</returns>
        public virtual bool SizeIsOk(uint size)
        {
            if (this.isStaticSize())
            {
                if ((int)size == (int)this.size)
                    return true;
            }
            else if (size >= this.size)
                return true;
            return false;
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public ushort ID
        {
            get
            {
                return this.GetUShort((ushort)0);
            }
            set
            {
                this.PutUShort(value, (ushort)0);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>.</returns>
        public virtual Packet New()
        {
            return new Packet();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="Client"/>.</param>
        public virtual void Parse(Client client)
        {
        }

        /// <summary>
        /// Write the data length to the first 2 bytes of the packet.
        /// </summary>
        public void SetLength()
        {
            byte[] bytes = BitConverter.GetBytes((uint)(this.data.Length - 4));
            this.data[0] = bytes[3];
            this.data[1] = bytes[2];
            this.data[2] = bytes[1];
            this.data[3] = bytes[0];
        }

        /// <summary>
        /// The isStaticSize.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool isStaticSize()
        {
            return true;
        }

        /// <summary>
        /// The GetString.
        /// </summary>
        /// <param name="index">Index of the string.</param>
        /// <returns>String representation.</returns>
        public string GetString(ushort index)
        {
            ushort num;
            for (num = index; (uint)num < this.size; ++num)
            {
                if (this.data[(int)num] == (byte)0 && this.data[(int)num + 1] == (byte)0)
                {
                    if (((int)num - (int)index) % 2 != 0)
                    {
                        ++num;
                        break;
                    }
                    break;
                }
            }
            this.offset = (ushort)((uint)num + 2U);
            return Global.Unicode.GetString(this.data, (int)index, (int)num - (int)index);
        }

        /// <summary>
        /// The GetString.
        /// </summary>
        /// <returns>String representation.</returns>
        public string GetString()
        {
            return this.GetString(this.offset);
        }

        /// <summary>
        /// The GetStringFixedSize.
        /// </summary>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        /// <param name="size">The size<see cref="ushort"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetStringFixedSize(ushort index, ushort size)
        {
            if ((int)index + (int)size > this.data.Length)
                return "OUT_OF_RANGE";
            this.offset += size;
            return Global.Unicode.GetString(this.data, (int)index, (int)size);
        }

        /// <summary>
        /// The GetStringFixedSize.
        /// </summary>
        /// <param name="size">The size<see cref="ushort"/>.</param>
        /// <returns>String representation.</returns>
        public string GetStringFixedSize(ushort size)
        {
            return this.GetStringFixedSize(this.offset, size);
        }

        /// <summary>
        /// The PutString.
        /// </summary>
        /// <param name="s">String to insert.</param>
        /// <param name="index">Index at which to insert.</param>
        public void PutString(string s, ushort index)
        {
            Global.Unicode.GetBytes(s).CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + (uint)(s.Length * 2));
        }

        /// <summary>
        /// The PutString.
        /// </summary>
        /// <param name="s">String to insert.</param>
        public void PutString(string s)
        {
            this.PutString(s, this.offset);
        }

        /// <summary>
        /// The GetByte.
        /// </summary>
        /// <param name="index">Index of the byte.</param>
        /// <returns>Byte at the index.</returns>
        public byte GetByte(ushort index)
        {
            this.offset = (ushort)((uint)index + 1U);
            return this.data[(int)index];
        }

        /// <summary>
        /// The GetByte.
        /// </summary>
        /// <returns>The byte.</returns>
        public byte GetByte()
        {
            return this.data[(int)this.offset++];
        }

        /// <summary>
        /// The PutByte.
        /// </summary>
        /// <param name="b">Byte to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutByte(byte b, ushort index)
        {
            this.data[(int)index] = b;
            this.offset = (ushort)((uint)index + 1U);
        }

        /// <summary>
        /// The PutByte.
        /// </summary>
        /// <param name="b">Byte to insert.</param>
        public void PutByte(byte b)
        {
            this.data[(int)this.offset++] = b;
        }

        /// <summary>
        /// The GetUShort.
        /// </summary>
        /// <param name="index">Index of the ushort.</param>
        /// <returns>The ushort value at the index.</returns>
        public ushort GetUShort(ushort index)
        {
            this.offset = (ushort)((uint)index + 2U);
            return BitConverter.ToUInt16(new byte[2]
            {
        this.data[(int) index + 1],
        this.data[(int) index]
            }, 0);
        }

        /// <summary>
        /// The GetUShort.
        /// </summary>
        /// <returns>The ushort value at the offset.</returns>
        public ushort GetUShort()
        {
            return this.GetUShort(this.offset);
        }

        /// <summary>
        /// The PutUShort.
        /// </summary>
        /// <param name="s">Ushort to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutUShort(ushort s, ushort index)
        {
            byte[] numArray = new byte[2];
            byte[] bytes = BitConverter.GetBytes(s);
            Array.Reverse((Array)bytes);
            bytes.CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + 2U);
        }

        /// <summary>
        /// The PutUShort.
        /// </summary>
        /// <param name="s">.</param>
        public void PutUShort(ushort s)
        {
            this.PutUShort(s, this.offset);
        }

        /// <summary>
        /// The GetShort.
        /// </summary>
        /// <param name="index">Index of the short.</param>
        /// <returns>The short value at the index.</returns>
        public short GetShort(ushort index)
        {
            this.offset = (ushort)((uint)index + 2U);
            return BitConverter.ToInt16(new byte[2]
            {
        this.data[(int) index + 1],
        this.data[(int) index]
            }, 0);
        }

        /// <summary>
        /// The GetShort.
        /// </summary>
        /// <returns>The short value at the offset.</returns>
        public short GetShort()
        {
            return this.GetShort(this.offset);
        }

        /// <summary>
        /// The PutShort.
        /// </summary>
        /// <param name="s">Short to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutShort(short s, ushort index)
        {
            byte[] numArray = new byte[2];
            byte[] bytes = BitConverter.GetBytes(s);
            Array.Reverse((Array)bytes);
            bytes.CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + 2U);
        }

        /// <summary>
        /// The PutShort.
        /// </summary>
        /// <param name="s">Short to insert.</param>
        public void PutShort(short s)
        {
            this.PutShort(s, this.offset);
        }

        /// <summary>
        /// The GetBytes.
        /// </summary>
        /// <param name="count">Number of bytes to get.</param>
        /// <param name="index">Indec from where to get bytes.</param>
        /// <returns>Byte array.</returns>
        public byte[] GetBytes(ushort count, ushort index)
        {
            this.offset = (ushort)((uint)index + (uint)count);
            byte[] numArray = new byte[(int)count];
            if ((int)index + (int)count <= this.data.Length)
            {
                for (ushort index1 = 0; (int)index1 < (int)count; ++index1)
                    numArray[(int)index1] = this.data[(int)index + (int)index1];
            }
            return numArray;
        }

        /// <summary>
        /// Get a certain amount of bytes from the current offset.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Byte array.</returns>
        public byte[] GetBytes(ushort count)
        {
            return this.GetBytes(this.offset, count);
        }

        /// <summary>
        /// Put some given bytes at a given position in the data array.
        /// </summary>
        /// <param name="bdata">bytes to add to the data array.</param>
        /// <param name="index">position to add the bytes to.</param>
        public void PutBytes(byte[] bdata, ushort index)
        {
            this.offset = (ushort)((uint)index + (uint)bdata.Length);
            if ((int)index + bdata.Length > this.data.Length)
                return;
            for (ushort index1 = 0; (int)index1 < bdata.Length; ++index1)
                this.data[(int)index + (int)index1] = bdata[(int)index1];
        }

        /// <summary>
        /// Put some given bytes at the current offset in the data array.
        /// </summary>
        /// <param name="bdata">bytes to add to the data array.</param>
        public void PutBytes(byte[] bdata)
        {
            this.PutBytes(bdata, this.offset);
        }

        /// <summary>
        /// The GetInt.
        /// </summary>
        /// <param name="index">Index of the int.</param>
        /// <returns>The int value at the index.</returns>
        public int GetInt(ushort index)
        {
            this.offset = (ushort)((uint)index + 4U);
            return BitConverter.ToInt32(new byte[4]
            {
        this.data[(int) index + 3],
        this.data[(int) index + 2],
        this.data[(int) index + 1],
        this.data[(int) index]
            }, 0);
        }

        /// <summary>
        /// The GetInt.
        /// </summary>
        /// <returns>The int value at the offset.</returns>
        public int GetInt()
        {
            return this.GetInt(this.offset);
        }

        /// <summary>
        /// The PutInt.
        /// </summary>
        /// <param name="s">Int to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutInt(int s, ushort index)
        {
            byte[] numArray = new byte[4];
            byte[] bytes = BitConverter.GetBytes(s);
            Array.Reverse((Array)bytes);
            bytes.CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + 4U);
        }

        /// <summary>
        /// The PutInt.
        /// </summary>
        /// <param name="s">Int to insert.</param>
        public void PutInt(int s)
        {
            this.PutInt(s, this.offset);
        }

        /// <summary>
        /// The GetUInt.
        /// </summary>
        /// <param name="index">Index of the uint.</param>
        /// <returns>The uint value at the index.</returns>
        public uint GetUInt(ushort index)
        {
            this.offset = (ushort)((uint)index + 4U);
            return BitConverter.ToUInt32(new byte[4]
            {
        this.data[(int) index + 3],
        this.data[(int) index + 2],
        this.data[(int) index + 1],
        this.data[(int) index]
            }, 0);
        }

        /// <summary>
        /// The GetUInt.
        /// </summary>
        /// <returns>The uint value at the offset.</returns>
        public uint GetUInt()
        {
            return this.GetUInt(this.offset);
        }

        /// <summary>
        /// The PutUInt.
        /// </summary>
        /// <param name="s">uint to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutUInt(uint s, ushort index)
        {
            byte[] numArray = new byte[4];
            byte[] bytes = BitConverter.GetBytes(s);
            Array.Reverse((Array)bytes);
            bytes.CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + 4U);
        }

        /// <summary>
        /// The PutUInt.
        /// </summary>
        /// <param name="s">uint to insert.</param>
        public void PutUInt(uint s)
        {
            this.PutUInt(s, this.offset);
        }

        /// <summary>
        /// The PutLong.
        /// </summary>
        /// <param name="s">The s<see cref="long"/>.</param>
        public void PutLong(long s)
        {
            this.PutLong(s, this.offset);
        }

        /// <summary>
        /// The PutLong.
        /// </summary>
        /// <param name="s">The s<see cref="long"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        public void PutLong(long s, ushort index)
        {
            byte[] numArray = new byte[8];
            byte[] bytes = BitConverter.GetBytes(s);
            Array.Reverse((Array)bytes);
            bytes.CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + 8U);
        }

        /// <summary>
        /// The PutULong.
        /// </summary>
        /// <param name="s">The s<see cref="ulong"/>.</param>
        public void PutULong(ulong s)
        {
            this.PutULong(s, this.offset);
        }

        /// <summary>
        /// The PutULong.
        /// </summary>
        /// <param name="s">The s<see cref="ulong"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        public void PutULong(ulong s, ushort index)
        {
            byte[] numArray = new byte[8];
            byte[] bytes = BitConverter.GetBytes(s);
            Array.Reverse((Array)bytes);
            bytes.CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + 8U);
        }

        /// <summary>
        /// The GetULong.
        /// </summary>
        /// <returns>The <see cref="ulong"/>.</returns>
        public ulong GetULong()
        {
            return this.GetULong(this.offset);
        }

        /// <summary>
        /// The GetULong.
        /// </summary>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        /// <returns>The <see cref="ulong"/>.</returns>
        public ulong GetULong(ushort index)
        {
            this.offset = (ushort)((uint)index + 8U);
            byte[] numArray = new byte[4]
            {
        this.data[(int) index + 7],
        this.data[(int) index + 6],
        this.data[(int) index + 5],
        this.data[(int) index + 4]
            };
            numArray[4] = this.data[(int)index + 3];
            numArray[5] = this.data[(int)index + 2];
            numArray[6] = this.data[(int)index + 1];
            numArray[7] = this.data[(int)index];
            return BitConverter.ToUInt64(numArray, 0);
        }

        /// <summary>
        /// The GetLong.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        public long GetLong()
        {
            return this.GetLong(this.offset);
        }

        /// <summary>
        /// The GetLong.
        /// </summary>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public long GetLong(ushort index)
        {
            this.offset = (ushort)((uint)index + 8U);
            byte[] numArray = new byte[4]
            {
        this.data[(int) index + 7],
        this.data[(int) index + 6],
        this.data[(int) index + 5],
        this.data[(int) index + 4]
            };
            numArray[4] = this.data[(int)index + 3];
            numArray[5] = this.data[(int)index + 2];
            numArray[6] = this.data[(int)index + 1];
            numArray[7] = this.data[(int)index];
            return BitConverter.ToInt64(numArray, 0);
        }

        /// <summary>
        /// The GetFloat.
        /// </summary>
        /// <param name="index">Index of the float.</param>
        /// <returns>The float value at the index.</returns>
        public float GetFloat(ushort index)
        {
            this.offset = (ushort)((uint)index + 4U);
            return BitConverter.ToSingle(this.data, (int)index);
        }

        /// <summary>
        /// The GetFloat.
        /// </summary>
        /// <returns>The float value at the offset.</returns>
        public float GetFloat()
        {
            return this.GetFloat(this.offset);
        }

        /// <summary>
        /// The PutFloat.
        /// </summary>
        /// <param name="s">Float to insert.</param>
        /// <param name="index">Index to insert at.</param>
        public void PutFloat(float s, ushort index)
        {
            BitConverter.GetBytes(s).CopyTo((Array)this.data, (int)index);
            this.offset = (ushort)((uint)index + 4U);
        }

        /// <summary>
        /// Put the given float at the current offset in the data.
        /// </summary>
        /// <param name="s">Float to insert.</param>
        public void PutFloat(float s)
        {
            this.PutFloat(s, this.offset);
        }

        /// <summary>
        /// The DumpData.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string DumpData()
        {
            string str = "";
            for (int index = 0; index < this.data.Length; ++index)
            {
                str += string.Format("{0:X2} ", (object)this.data[index]);
                if ((index + 1) % 16 == 0 && index != 0)
                    str += "\r\n";
            }
            return str;
        }
    }
}
