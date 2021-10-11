namespace SevenZip
{
    /// <summary>
    /// Provides the fields that represent properties idenitifiers for compressing.
    /// </summary>
    public enum CoderPropID
    {
        /// <summary>
        /// Defines the DictionarySize.
        /// </summary>
        DictionarySize = 1024, // 0x00000400

        /// <summary>
        /// Defines the UsedMemorySize.
        /// </summary>
        UsedMemorySize = 1025, // 0x00000401

        /// <summary>
        /// Defines the Order.
        /// </summary>
        Order = 1026, // 0x00000402

        /// <summary>
        /// Defines the PosStateBits.
        /// </summary>
        PosStateBits = 1088, // 0x00000440

        /// <summary>
        /// Defines the LitContextBits.
        /// </summary>
        LitContextBits = 1089, // 0x00000441

        /// <summary>
        /// Defines the LitPosBits.
        /// </summary>
        LitPosBits = 1090, // 0x00000442

        /// <summary>
        /// Defines the NumFastBytes.
        /// </summary>
        NumFastBytes = 1104, // 0x00000450

        /// <summary>
        /// Defines the MatchFinder.
        /// </summary>
        MatchFinder = 1105, // 0x00000451

        /// <summary>
        /// Defines the NumPasses.
        /// </summary>
        NumPasses = 1120, // 0x00000460

        /// <summary>
        /// Defines the Algorithm.
        /// </summary>
        Algorithm = 1136, // 0x00000470

        /// <summary>
        /// Defines the MultiThread.
        /// </summary>
        MultiThread = 1152, // 0x00000480

        /// <summary>
        /// Defines the EndMarker.
        /// </summary>
        EndMarker = 1168, // 0x00000490
    }
}
