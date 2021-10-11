namespace SevenZip.Compression.LZMA
{
    using SevenZip.Compression.LZ;
    using SevenZip.Compression.RangeCoder;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Decoder" />.
    /// </summary>
    public class Decoder : ICoder, ISetDecoderProperties
    {
        /// <summary>
        /// Defines the m_OutWindow.
        /// </summary>
        private OutWindow m_OutWindow = new OutWindow();

        /// <summary>
        /// Defines the m_RangeDecoder.
        /// </summary>
        private SevenZip.Compression.RangeCoder.Decoder m_RangeDecoder = new SevenZip.Compression.RangeCoder.Decoder();

        /// <summary>
        /// Defines the m_IsMatchDecoders.
        /// </summary>
        private BitDecoder[] m_IsMatchDecoders = new BitDecoder[(192)];

        /// <summary>
        /// Defines the m_IsRepDecoders.
        /// </summary>
        private BitDecoder[] m_IsRepDecoders = new BitDecoder[(12)];

        /// <summary>
        /// Defines the m_IsRepG0Decoders.
        /// </summary>
        private BitDecoder[] m_IsRepG0Decoders = new BitDecoder[(12)];

        /// <summary>
        /// Defines the m_IsRepG1Decoders.
        /// </summary>
        private BitDecoder[] m_IsRepG1Decoders = new BitDecoder[(12)];

        /// <summary>
        /// Defines the m_IsRepG2Decoders.
        /// </summary>
        private BitDecoder[] m_IsRepG2Decoders = new BitDecoder[(12)];

        /// <summary>
        /// Defines the m_IsRep0LongDecoders.
        /// </summary>
        private BitDecoder[] m_IsRep0LongDecoders = new BitDecoder[(192)];

        /// <summary>
        /// Defines the m_PosSlotDecoder.
        /// </summary>
        private BitTreeDecoder[] m_PosSlotDecoder = new BitTreeDecoder[(4)];

        /// <summary>
        /// Defines the m_PosDecoders.
        /// </summary>
        private BitDecoder[] m_PosDecoders = new BitDecoder[(114)];

        /// <summary>
        /// Defines the m_PosAlignDecoder.
        /// </summary>
        private BitTreeDecoder m_PosAlignDecoder = new BitTreeDecoder(4);

        /// <summary>
        /// Defines the m_LenDecoder.
        /// </summary>
        private Decoder.LenDecoder m_LenDecoder = new Decoder.LenDecoder();

        /// <summary>
        /// Defines the m_RepLenDecoder.
        /// </summary>
        private Decoder.LenDecoder m_RepLenDecoder = new Decoder.LenDecoder();

        /// <summary>
        /// Defines the m_LiteralDecoder.
        /// </summary>
        private Decoder.LiteralDecoder m_LiteralDecoder = new Decoder.LiteralDecoder();

        /// <summary>
        /// Defines the m_DictionarySize.
        /// </summary>
        private uint m_DictionarySize;

        /// <summary>
        /// Defines the m_DictionarySizeCheck.
        /// </summary>
        private uint m_DictionarySizeCheck;

        /// <summary>
        /// Defines the m_PosStateMask.
        /// </summary>
        private uint m_PosStateMask;

        /// <summary>
        /// Defines the _solid.
        /// </summary>
        private bool _solid;

        /// <summary>
        /// Initializes a new instance of the <see cref="Decoder"/> class.
        /// </summary>
        public Decoder()
        {
            this.m_DictionarySize = uint.MaxValue;
            for (int index = 0; index < 4; ++index)
                this.m_PosSlotDecoder[index] = new BitTreeDecoder(6);
        }

        /// <summary>
        /// The SetDictionarySize.
        /// </summary>
        /// <param name="dictionarySize">The dictionarySize<see cref="uint"/>.</param>
        private void SetDictionarySize(uint dictionarySize)
        {
            if ((int)this.m_DictionarySize == (int)dictionarySize)
                return;
            this.m_DictionarySize = dictionarySize;
            this.m_DictionarySizeCheck = Math.Max(this.m_DictionarySize, 1U);
            this.m_OutWindow.Create(Math.Max(this.m_DictionarySizeCheck, 4096U));
        }

        /// <summary>
        /// The SetLiteralProperties.
        /// </summary>
        /// <param name="lp">The lp<see cref="int"/>.</param>
        /// <param name="lc">The lc<see cref="int"/>.</param>
        private void SetLiteralProperties(int lp, int lc)
        {
            if (lp > 8)
                throw new InvalidParamException();
            if (lc > 8)
                throw new InvalidParamException();
            this.m_LiteralDecoder.Create(lp, lc);
        }

        /// <summary>
        /// The SetPosBitsProperties.
        /// </summary>
        /// <param name="pb">The pb<see cref="int"/>.</param>
        private void SetPosBitsProperties(int pb)
        {
            if (pb > 4)
                throw new InvalidParamException();
            uint numPosStates = (uint)(1 << pb);
            this.m_LenDecoder.Create(numPosStates);
            this.m_RepLenDecoder.Create(numPosStates);
            this.m_PosStateMask = numPosStates - 1U;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        /// <param name="outStream">The outStream<see cref="Stream"/>.</param>
        private void Init(Stream inStream, Stream outStream)
        {
            this.m_RangeDecoder.Init(inStream);
            this.m_OutWindow.Init(outStream, this._solid);
            for (uint index1 = 0; index1 < 12U; ++index1)
            {
                for (uint index2 = 0; index2 <= this.m_PosStateMask; ++index2)
                {
                    uint num = (index1 << 4) + index2;
                    this.m_IsMatchDecoders[num].Init();
                    this.m_IsRep0LongDecoders[num].Init();
                }
                this.m_IsRepDecoders[index1].Init();
                this.m_IsRepG0Decoders[index1].Init();
                this.m_IsRepG1Decoders[index1].Init();
                this.m_IsRepG2Decoders[index1].Init();
            }
            this.m_LiteralDecoder.Init();
            for (uint index = 0; index < 4U; ++index)
                this.m_PosSlotDecoder[index].Init();
            for (uint index = 0; index < 114U; ++index)
                this.m_PosDecoders[index].Init();
            this.m_LenDecoder.Init();
            this.m_RepLenDecoder.Init();
            this.m_PosAlignDecoder.Init();
        }

        /// <summary>
        /// The Code.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        /// <param name="outStream">The outStream<see cref="Stream"/>.</param>
        /// <param name="inSize">The inSize<see cref="long"/>.</param>
        /// <param name="outSize">The outSize<see cref="long"/>.</param>
        /// <param name="progress">The progress<see cref="ICodeProgress"/>.</param>
        public void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress)
        {
            this.Init(inStream, outStream);
            Base.State state = new Base.State();
            state.Init();
            uint distance = 0;
            uint num1 = 0;
            uint num2 = 0;
            uint num3 = 0;
            ulong num4 = 0;
            ulong num5 = (ulong)outSize;
            if (num4 < num5)
            {
                if (this.m_IsMatchDecoders[(state.Index << 4)].Decode(this.m_RangeDecoder) != 0U)
                    throw new DataErrorException();
                state.UpdateChar();
                this.m_OutWindow.PutByte(this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, 0U, (byte)0));
                ++num4;
            }
            while (num4 < num5)
            {
                uint posState = (uint)num4 & this.m_PosStateMask;
                if (this.m_IsMatchDecoders[((state.Index << 4) + posState)].Decode(this.m_RangeDecoder) == 0U)
                {
                    byte prevByte = this.m_OutWindow.GetByte(0U);
                    this.m_OutWindow.PutByte(state.IsCharState() ? this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, (uint)num4, prevByte) : this.m_LiteralDecoder.DecodeWithMatchByte(this.m_RangeDecoder, (uint)num4, prevByte, this.m_OutWindow.GetByte(distance)));
                    state.UpdateChar();
                    ++num4;
                }
                else
                {
                    uint len;
                    if (this.m_IsRepDecoders[state.Index].Decode(this.m_RangeDecoder) == 1U)
                    {
                        if (this.m_IsRepG0Decoders[state.Index].Decode(this.m_RangeDecoder) == 0U)
                        {
                            if (this.m_IsRep0LongDecoders[((state.Index << 4) + posState)].Decode(this.m_RangeDecoder) == 0U)
                            {
                                state.UpdateShortRep();
                                this.m_OutWindow.PutByte(this.m_OutWindow.GetByte(distance));
                                ++num4;
                                continue;
                            }
                        }
                        else
                        {
                            uint num6;
                            if (this.m_IsRepG1Decoders[state.Index].Decode(this.m_RangeDecoder) == 0U)
                            {
                                num6 = num1;
                            }
                            else
                            {
                                if (this.m_IsRepG2Decoders[state.Index].Decode(this.m_RangeDecoder) == 0U)
                                {
                                    num6 = num2;
                                }
                                else
                                {
                                    num6 = num3;
                                    num3 = num2;
                                }
                                num2 = num1;
                            }
                            num1 = distance;
                            distance = num6;
                        }
                        len = this.m_RepLenDecoder.Decode(this.m_RangeDecoder, posState) + 2U;
                        state.UpdateRep();
                    }
                    else
                    {
                        num3 = num2;
                        num2 = num1;
                        num1 = distance;
                        len = 2U + this.m_LenDecoder.Decode(this.m_RangeDecoder, posState);
                        state.UpdateMatch();
                        uint num6 = this.m_PosSlotDecoder[Base.GetLenToPosState(len)].Decode(this.m_RangeDecoder);
                        if (num6 >= 4U)
                        {
                            int NumBitLevels = (int)(num6 >> 1) - 1;
                            uint num7 = (uint)((2 | (int)num6 & 1) << NumBitLevels);
                            distance = num6 >= 14U ? num7 + (this.m_RangeDecoder.DecodeDirectBits(NumBitLevels - 4) << 4) + this.m_PosAlignDecoder.ReverseDecode(this.m_RangeDecoder) : num7 + BitTreeDecoder.ReverseDecode(this.m_PosDecoders, (uint)((int)num7 - (int)num6 - 1), this.m_RangeDecoder, NumBitLevels);
                        }
                        else
                            distance = num6;
                    }
                    if ((ulong)distance >= (ulong)this.m_OutWindow.TrainSize + num4 || distance >= this.m_DictionarySizeCheck)
                    {
                        if (distance != uint.MaxValue)
                            throw new DataErrorException();
                        break;
                    }
                    this.m_OutWindow.CopyBlock(distance, len);
                    num4 += (ulong)len;
                }
                progress?.SetProgress((long)num4, 0L);
            }
            this.m_OutWindow.Flush();
            this.m_OutWindow.ReleaseStream();
            this.m_RangeDecoder.ReleaseStream();
        }

        /// <summary>
        /// The SetDecoderProperties.
        /// </summary>
        /// <param name="properties">The properties<see cref="byte[]"/>.</param>
        public void SetDecoderProperties(byte[] properties)
        {
            if (properties.Length < 5)
                throw new InvalidParamException();
            int lc = (int)properties[0] % 9;
            int num = (int)properties[0] / 9;
            int lp = num % 5;
            int pb = num / 5;
            if (pb > 4)
                throw new InvalidParamException();
            uint dictionarySize = 0;
            for (int index = 0; index < 4; ++index)
                dictionarySize += (uint)properties[1 + index] << index * 8;
            this.SetDictionarySize(dictionarySize);
            this.SetLiteralProperties(lp, lc);
            this.SetPosBitsProperties(pb);
        }

        /// <summary>
        /// The Train.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Train(Stream stream)
        {
            this._solid = true;
            return this.m_OutWindow.Train(stream);
        }

        /// <summary>
        /// Defines the <see cref="LenDecoder" />.
        /// </summary>
        private class LenDecoder
        {
            /// <summary>
            /// Defines the m_Choice.
            /// </summary>
            private BitDecoder m_Choice = new BitDecoder();

            /// <summary>
            /// Defines the m_Choice2.
            /// </summary>
            private BitDecoder m_Choice2 = new BitDecoder();

            /// <summary>
            /// Defines the m_LowCoder.
            /// </summary>
            private BitTreeDecoder[] m_LowCoder = new BitTreeDecoder[(16)];

            /// <summary>
            /// Defines the m_MidCoder.
            /// </summary>
            private BitTreeDecoder[] m_MidCoder = new BitTreeDecoder[(16)];

            /// <summary>
            /// Defines the m_HighCoder.
            /// </summary>
            private BitTreeDecoder m_HighCoder = new BitTreeDecoder(8);

            /// <summary>
            /// Defines the m_NumPosStates.
            /// </summary>
            private uint m_NumPosStates;

            /// <summary>
            /// The Create.
            /// </summary>
            /// <param name="numPosStates">The numPosStates<see cref="uint"/>.</param>
            public void Create(uint numPosStates)
            {
                for (uint numPosStates1 = this.m_NumPosStates; numPosStates1 < numPosStates; ++numPosStates1)
                {
                    this.m_LowCoder[numPosStates1] = new BitTreeDecoder(3);
                    this.m_MidCoder[numPosStates1] = new BitTreeDecoder(3);
                }
                this.m_NumPosStates = numPosStates;
            }

            /// <summary>
            /// The Init.
            /// </summary>
            public void Init()
            {
                this.m_Choice.Init();
                for (uint index = 0; index < this.m_NumPosStates; ++index)
                {
                    this.m_LowCoder[index].Init();
                    this.m_MidCoder[index].Init();
                }
                this.m_Choice2.Init();
                this.m_HighCoder.Init();
            }

            /// <summary>
            /// The Decode.
            /// </summary>
            /// <param name="rangeDecoder">The rangeDecoder<see cref="SevenZip.Compression.RangeCoder.Decoder"/>.</param>
            /// <param name="posState">The posState<see cref="uint"/>.</param>
            /// <returns>The <see cref="uint"/>.</returns>
            public uint Decode(SevenZip.Compression.RangeCoder.Decoder rangeDecoder, uint posState)
            {
                if (this.m_Choice.Decode(rangeDecoder) == 0U)
                    return this.m_LowCoder[posState].Decode(rangeDecoder);
                uint num = 8;
                return this.m_Choice2.Decode(rangeDecoder) != 0U ? num + 8U + this.m_HighCoder.Decode(rangeDecoder) : num + this.m_MidCoder[posState].Decode(rangeDecoder);
            }
        }

        /// <summary>
        /// Defines the <see cref="LiteralDecoder" />.
        /// </summary>
        private class LiteralDecoder
        {
            /// <summary>
            /// Defines the m_Coders.
            /// </summary>
            private Decoder.LiteralDecoder.Decoder2[] m_Coders;

            /// <summary>
            /// Defines the m_NumPrevBits.
            /// </summary>
            private int m_NumPrevBits;

            /// <summary>
            /// Defines the m_NumPosBits.
            /// </summary>
            private int m_NumPosBits;

            /// <summary>
            /// Defines the m_PosMask.
            /// </summary>
            private uint m_PosMask;

            /// <summary>
            /// The Create.
            /// </summary>
            /// <param name="numPosBits">The numPosBits<see cref="int"/>.</param>
            /// <param name="numPrevBits">The numPrevBits<see cref="int"/>.</param>
            public void Create(int numPosBits, int numPrevBits)
            {
                if (this.m_Coders != null && this.m_NumPrevBits == numPrevBits && this.m_NumPosBits == numPosBits)
                    return;
                this.m_NumPosBits = numPosBits;
                this.m_PosMask = (uint)((1 << numPosBits) - 1);
                this.m_NumPrevBits = numPrevBits;
                uint num = (uint)(1 << this.m_NumPrevBits + this.m_NumPosBits);
                this.m_Coders = new Decoder.LiteralDecoder.Decoder2[num];
                for (uint index = 0; index < num; ++index)
                    this.m_Coders[index].Create();
            }

            /// <summary>
            /// The Init.
            /// </summary>
            public void Init()
            {
                uint num = (uint)(1 << this.m_NumPrevBits + this.m_NumPosBits);
                for (uint index = 0; index < num; ++index)
                    this.m_Coders[index].Init();
            }

            /// <summary>
            /// The GetState.
            /// </summary>
            /// <param name="pos">The pos<see cref="uint"/>.</param>
            /// <param name="prevByte">The prevByte<see cref="byte"/>.</param>
            /// <returns>The <see cref="uint"/>.</returns>
            private uint GetState(uint pos, byte prevByte)
            {
                return (uint)((((int)pos & (int)this.m_PosMask) << this.m_NumPrevBits) + ((int)prevByte >> 8 - this.m_NumPrevBits));
            }

            /// <summary>
            /// The DecodeNormal.
            /// </summary>
            /// <param name="rangeDecoder">The rangeDecoder<see cref="SevenZip.Compression.RangeCoder.Decoder"/>.</param>
            /// <param name="pos">The pos<see cref="uint"/>.</param>
            /// <param name="prevByte">The prevByte<see cref="byte"/>.</param>
            /// <returns>The <see cref="byte"/>.</returns>
            public byte DecodeNormal(SevenZip.Compression.RangeCoder.Decoder rangeDecoder, uint pos, byte prevByte)
            {
                return this.m_Coders[this.GetState(pos, prevByte)].DecodeNormal(rangeDecoder);
            }

            /// <summary>
            /// The DecodeWithMatchByte.
            /// </summary>
            /// <param name="rangeDecoder">The rangeDecoder<see cref="SevenZip.Compression.RangeCoder.Decoder"/>.</param>
            /// <param name="pos">The pos<see cref="uint"/>.</param>
            /// <param name="prevByte">The prevByte<see cref="byte"/>.</param>
            /// <param name="matchByte">The matchByte<see cref="byte"/>.</param>
            /// <returns>The <see cref="byte"/>.</returns>
            public byte DecodeWithMatchByte(SevenZip.Compression.RangeCoder.Decoder rangeDecoder, uint pos, byte prevByte, byte matchByte)
            {
                return this.m_Coders[this.GetState(pos, prevByte)].DecodeWithMatchByte(rangeDecoder, matchByte);
            }

            /// <summary>
            /// Defines the <see cref="Decoder2" />.
            /// </summary>
            private struct Decoder2
            {
                /// <summary>
                /// Defines the m_Decoders.
                /// </summary>
                private BitDecoder[] m_Decoders;

                /// <summary>
                /// The Create.
                /// </summary>
                public void Create()
                {
                    this.m_Decoders = new BitDecoder[768];
                }

                /// <summary>
                /// The Init.
                /// </summary>
                public void Init()
                {
                    for (int index = 0; index < 768; ++index)
                        this.m_Decoders[index].Init();
                }

                /// <summary>
                /// The DecodeNormal.
                /// </summary>
                /// <param name="rangeDecoder">The rangeDecoder<see cref="SevenZip.Compression.RangeCoder.Decoder"/>.</param>
                /// <returns>The <see cref="byte"/>.</returns>
                public byte DecodeNormal(SevenZip.Compression.RangeCoder.Decoder rangeDecoder)
                {
                    uint num = 1;
                    do
                    {
                        num = num << 1 | this.m_Decoders[num].Decode(rangeDecoder);
                    }
                    while (num < 256U);
                    return (byte)num;
                }

                /// <summary>
                /// The DecodeWithMatchByte.
                /// </summary>
                /// <param name="rangeDecoder">The rangeDecoder<see cref="SevenZip.Compression.RangeCoder.Decoder"/>.</param>
                /// <param name="matchByte">The matchByte<see cref="byte"/>.</param>
                /// <returns>The <see cref="byte"/>.</returns>
                public byte DecodeWithMatchByte(SevenZip.Compression.RangeCoder.Decoder rangeDecoder, byte matchByte)
                {
                    uint num1 = 1;
                    do
                    {
                        uint num2 = (uint)((int)matchByte >> 7 & 1);
                        matchByte <<= 1;
                        uint num3 = this.m_Decoders[((uint)(1 + (int)num2 << 8) + num1)].Decode(rangeDecoder);
                        num1 = num1 << 1 | num3;
                        if ((int)num2 != (int)num3)
                        {
                            while (num1 < 256U)
                                num1 = num1 << 1 | this.m_Decoders[num1].Decode(rangeDecoder);
                            break;
                        }
                    }
                    while (num1 < 256U);
                    return (byte)num1;
                }
            }
        }
    }
}
