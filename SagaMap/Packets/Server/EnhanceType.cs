namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Defines the EnhanceType.
    /// </summary>
    public enum EnhanceType
    {
        /// <summary>
        /// Defines the HP.
        /// </summary>
        HP = 0,

        /// <summary>
        /// Defines the MP.
        /// </summary>
        MP = 1,

        /// <summary>
        /// Defines the SP.
        /// </summary>
        SP = 2,

        /// <summary>
        /// Defines the Atk.
        /// </summary>
        Atk = 3,

        /// <summary>
        /// Defines the MAtk.
        /// </summary>
        MAtk = 4,

        /// <summary>
        /// Defines the Def.
        /// </summary>
        Def = 5,

        /// <summary>
        /// Defines the MDef.
        /// </summary>
        MDef = 6,

        /// <summary>
        /// Defines the Cri.
        /// </summary>
        Cri = 13, // 0x0000000D

        /// <summary>
        /// Defines the AvoidCri.
        /// </summary>
        AvoidCri = 14, // 0x0000000E
    }
}
