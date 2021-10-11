namespace SevenZip.Compression.LZMA
{
    /// <summary>
    /// Defines the <see cref="Base" />.
    /// </summary>
    internal abstract class Base
    {
        /// <summary>
        /// Defines the kNumRepDistances.
        /// </summary>
        public const uint kNumRepDistances = 4;

        /// <summary>
        /// Defines the kNumStates.
        /// </summary>
        public const uint kNumStates = 12;

        /// <summary>
        /// Defines the kNumPosSlotBits.
        /// </summary>
        public const int kNumPosSlotBits = 6;

        /// <summary>
        /// Defines the kDicLogSizeMin.
        /// </summary>
        public const int kDicLogSizeMin = 0;

        /// <summary>
        /// Defines the kNumLenToPosStatesBits.
        /// </summary>
        public const int kNumLenToPosStatesBits = 2;

        /// <summary>
        /// Defines the kNumLenToPosStates.
        /// </summary>
        public const uint kNumLenToPosStates = 4;

        /// <summary>
        /// Defines the kMatchMinLen.
        /// </summary>
        public const uint kMatchMinLen = 2;

        /// <summary>
        /// Defines the kNumAlignBits.
        /// </summary>
        public const int kNumAlignBits = 4;

        /// <summary>
        /// Defines the kAlignTableSize.
        /// </summary>
        public const uint kAlignTableSize = 16;

        /// <summary>
        /// Defines the kAlignMask.
        /// </summary>
        public const uint kAlignMask = 15;

        /// <summary>
        /// Defines the kStartPosModelIndex.
        /// </summary>
        public const uint kStartPosModelIndex = 4;

        /// <summary>
        /// Defines the kEndPosModelIndex.
        /// </summary>
        public const uint kEndPosModelIndex = 14;

        /// <summary>
        /// Defines the kNumPosModels.
        /// </summary>
        public const uint kNumPosModels = 10;

        /// <summary>
        /// Defines the kNumFullDistances.
        /// </summary>
        public const uint kNumFullDistances = 128;

        /// <summary>
        /// Defines the kNumLitPosStatesBitsEncodingMax.
        /// </summary>
        public const uint kNumLitPosStatesBitsEncodingMax = 4;

        /// <summary>
        /// Defines the kNumLitContextBitsMax.
        /// </summary>
        public const uint kNumLitContextBitsMax = 8;

        /// <summary>
        /// Defines the kNumPosStatesBitsMax.
        /// </summary>
        public const int kNumPosStatesBitsMax = 4;

        /// <summary>
        /// Defines the kNumPosStatesMax.
        /// </summary>
        public const uint kNumPosStatesMax = 16;

        /// <summary>
        /// Defines the kNumPosStatesBitsEncodingMax.
        /// </summary>
        public const int kNumPosStatesBitsEncodingMax = 4;

        /// <summary>
        /// Defines the kNumPosStatesEncodingMax.
        /// </summary>
        public const uint kNumPosStatesEncodingMax = 16;

        /// <summary>
        /// Defines the kNumLowLenBits.
        /// </summary>
        public const int kNumLowLenBits = 3;

        /// <summary>
        /// Defines the kNumMidLenBits.
        /// </summary>
        public const int kNumMidLenBits = 3;

        /// <summary>
        /// Defines the kNumHighLenBits.
        /// </summary>
        public const int kNumHighLenBits = 8;

        /// <summary>
        /// Defines the kNumLowLenSymbols.
        /// </summary>
        public const uint kNumLowLenSymbols = 8;

        /// <summary>
        /// Defines the kNumMidLenSymbols.
        /// </summary>
        public const uint kNumMidLenSymbols = 8;

        /// <summary>
        /// Defines the kNumLenSymbols.
        /// </summary>
        public const uint kNumLenSymbols = 272;

        /// <summary>
        /// Defines the kMatchMaxLen.
        /// </summary>
        public const uint kMatchMaxLen = 273;

        /// <summary>
        /// The GetLenToPosState.
        /// </summary>
        /// <param name="len">The len<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public static uint GetLenToPosState(uint len)
        {
            len -= 2U;
            if (len < 4U)
                return len;
            return 3;
        }

        /// <summary>
        /// Defines the <see cref="State" />.
        /// </summary>
        public struct State
        {
            /// <summary>
            /// Defines the Index.
            /// </summary>
            public uint Index;

            /// <summary>
            /// The Init.
            /// </summary>
            public void Init()
            {
                this.Index = 0U;
            }

            /// <summary>
            /// The UpdateChar.
            /// </summary>
            public void UpdateChar()
            {
                if (this.Index < 4U)
                    this.Index = 0U;
                else if (this.Index < 10U)
                    this.Index -= 3U;
                else
                    this.Index -= 6U;
            }

            /// <summary>
            /// The UpdateMatch.
            /// </summary>
            public void UpdateMatch()
            {
                this.Index = this.Index < 7U ? 7U : 10U;
            }

            /// <summary>
            /// The UpdateRep.
            /// </summary>
            public void UpdateRep()
            {
                this.Index = this.Index < 7U ? 8U : 11U;
            }

            /// <summary>
            /// The UpdateShortRep.
            /// </summary>
            public void UpdateShortRep()
            {
                this.Index = this.Index < 7U ? 9U : 11U;
            }

            /// <summary>
            /// The IsCharState.
            /// </summary>
            /// <returns>The <see cref="bool"/>.</returns>
            public bool IsCharState()
            {
                return this.Index < 7U;
            }
        }
    }
}
