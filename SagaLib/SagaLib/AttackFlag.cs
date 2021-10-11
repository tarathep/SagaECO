namespace SagaLib
{
    /// <summary>
    /// Defines the AttackFlag.
    /// </summary>
    public enum AttackFlag
    {
        /// <summary>
        /// Defines the NONE.
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Defines the HP_DAMAGE.
        /// </summary>
        HP_DAMAGE = 1,

        /// <summary>
        /// Defines the MP_DAMAGE.
        /// </summary>
        MP_DAMAGE = 2,

        /// <summary>
        /// Defines the SP_DAMAGE.
        /// </summary>
        SP_DAMAGE = 4,

        /// <summary>
        /// Defines the NO_DAMAGE.
        /// </summary>
        NO_DAMAGE = 8,

        /// <summary>
        /// Defines the HP_HEAL.
        /// </summary>
        HP_HEAL = 17, // 0x00000011

        /// <summary>
        /// Defines the MP_HEAL.
        /// </summary>
        MP_HEAL = 34, // 0x00000022

        /// <summary>
        /// Defines the SP_HEAL.
        /// </summary>
        SP_HEAL = 68, // 0x00000044

        /// <summary>
        /// Defines the UNKNOWN1.
        /// </summary>
        UNKNOWN1 = 128, // 0x00000080

        /// <summary>
        /// Defines the CRITICAL.
        /// </summary>
        CRITICAL = 256, // 0x00000100

        /// <summary>
        /// Defines the MISS.
        /// </summary>
        MISS = 512, // 0x00000200

        /// <summary>
        /// Defines the AVOID.
        /// </summary>
        AVOID = 1024, // 0x00000400

        /// <summary>
        /// Defines the AVOID2.
        /// </summary>
        AVOID2 = 2048, // 0x00000800

        /// <summary>
        /// Defines the GUARD.
        /// </summary>
        GUARD = 4096, // 0x00001000

        /// <summary>
        /// Defines the ATTACK_EFFECT.
        /// </summary>
        ATTACK_EFFECT = 8192, // 0x00002000

        /// <summary>
        /// Defines the DIE.
        /// </summary>
        DIE = 16384, // 0x00004000

        /// <summary>
        /// Defines the UNKNOWN2.
        /// </summary>
        UNKNOWN2 = 65536, // 0x00010000

        /// <summary>
        /// Defines the UNKNOWN3.
        /// </summary>
        UNKNOWN3 = 4194304, // 0x00400000

        /// <summary>
        /// Defines the UNKNOWN4.
        /// </summary>
        UNKNOWN4 = 8388608, // 0x00800000

        /// <summary>
        /// Defines the UNKNOWN5.
        /// </summary>
        UNKNOWN5 = 16777216, // 0x01000000

        /// <summary>
        /// Defines the UNKNOWN6.
        /// </summary>
        UNKNOWN6 = 33554432, // 0x02000000
    }
}
