namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;

    /// <summary>
    /// Defines the <see cref="DeflaterConstants" />.
    /// </summary>
    public class DeflaterConstants
    {
        /// <summary>
        /// Defines the MAX_BLOCK_SIZE.
        /// </summary>
        public static int MAX_BLOCK_SIZE = Math.Min((int)ushort.MaxValue, 65531);

        /// <summary>
        /// Defines the GOOD_LENGTH.
        /// </summary>
        public static int[] GOOD_LENGTH = new int[10]
    {
      0,
      4,
      4,
      4,
      4,
      8,
      8,
      8,
      32,
      32
    };

        /// <summary>
        /// Defines the MAX_LAZY.
        /// </summary>
        public static int[] MAX_LAZY = new int[10]
    {
      0,
      4,
      5,
      6,
      4,
      16,
      16,
      32,
      128,
      258
    };

        /// <summary>
        /// Defines the NICE_LENGTH.
        /// </summary>
        public static int[] NICE_LENGTH = new int[10]
    {
      0,
      8,
      16,
      32,
      16,
      32,
      128,
      128,
      258,
      258
    };

        /// <summary>
        /// Defines the MAX_CHAIN.
        /// </summary>
        public static int[] MAX_CHAIN = new int[10]
    {
      0,
      4,
      8,
      32,
      16,
      32,
      128,
      256,
      1024,
      4096
    };

        /// <summary>
        /// Defines the COMPR_FUNC.
        /// </summary>
        public static int[] COMPR_FUNC = new int[10]
    {
      0,
      1,
      1,
      1,
      1,
      2,
      2,
      2,
      2,
      2
    };

        /// <summary>
        /// Defines the DEBUGGING.
        /// </summary>
        public const bool DEBUGGING = false;

        /// <summary>
        /// Defines the STORED_BLOCK.
        /// </summary>
        public const int STORED_BLOCK = 0;

        /// <summary>
        /// Defines the STATIC_TREES.
        /// </summary>
        public const int STATIC_TREES = 1;

        /// <summary>
        /// Defines the DYN_TREES.
        /// </summary>
        public const int DYN_TREES = 2;

        /// <summary>
        /// Defines the PRESET_DICT.
        /// </summary>
        public const int PRESET_DICT = 32;

        /// <summary>
        /// Defines the DEFAULT_MEM_LEVEL.
        /// </summary>
        public const int DEFAULT_MEM_LEVEL = 8;

        /// <summary>
        /// Defines the MAX_MATCH.
        /// </summary>
        public const int MAX_MATCH = 258;

        /// <summary>
        /// Defines the MIN_MATCH.
        /// </summary>
        public const int MIN_MATCH = 3;

        /// <summary>
        /// Defines the MAX_WBITS.
        /// </summary>
        public const int MAX_WBITS = 15;

        /// <summary>
        /// Defines the WSIZE.
        /// </summary>
        public const int WSIZE = 32768;

        /// <summary>
        /// Defines the WMASK.
        /// </summary>
        public const int WMASK = 32767;

        /// <summary>
        /// Defines the HASH_BITS.
        /// </summary>
        public const int HASH_BITS = 15;

        /// <summary>
        /// Defines the HASH_SIZE.
        /// </summary>
        public const int HASH_SIZE = 32768;

        /// <summary>
        /// Defines the HASH_MASK.
        /// </summary>
        public const int HASH_MASK = 32767;

        /// <summary>
        /// Defines the HASH_SHIFT.
        /// </summary>
        public const int HASH_SHIFT = 5;

        /// <summary>
        /// Defines the MIN_LOOKAHEAD.
        /// </summary>
        public const int MIN_LOOKAHEAD = 262;

        /// <summary>
        /// Defines the MAX_DIST.
        /// </summary>
        public const int MAX_DIST = 32506;

        /// <summary>
        /// Defines the PENDING_BUF_SIZE.
        /// </summary>
        public const int PENDING_BUF_SIZE = 65536;

        /// <summary>
        /// Defines the DEFLATE_STORED.
        /// </summary>
        public const int DEFLATE_STORED = 0;

        /// <summary>
        /// Defines the DEFLATE_FAST.
        /// </summary>
        public const int DEFLATE_FAST = 1;

        /// <summary>
        /// Defines the DEFLATE_SLOW.
        /// </summary>
        public const int DEFLATE_SLOW = 2;
    }
}
