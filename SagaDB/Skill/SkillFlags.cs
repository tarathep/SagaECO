namespace SagaDB.Skill
{
    /// <summary>
    /// Defines the SkillFlags.
    /// </summary>
    public enum SkillFlags
    {
        /// <summary>
        /// Defines the NONE.
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Defines the NOT_EXIST.
        /// </summary>
        NOT_EXIST = 1,

        /// <summary>
        /// Defines the MAGIC.
        /// </summary>
        MAGIC = 2,

        /// <summary>
        /// Defines the PHYSIC.
        /// </summary>
        PHYSIC = 4,

        /// <summary>
        /// Defines the PARTY_ONLY.
        /// </summary>
        PARTY_ONLY = 8,

        /// <summary>
        /// Defines the ATTACK.
        /// </summary>
        ATTACK = 16, // 0x00000010

        /// <summary>
        /// Defines the CAN_HAS_TARGET.
        /// </summary>
        CAN_HAS_TARGET = 32, // 0x00000020

        /// <summary>
        /// Defines the SUPPORT.
        /// </summary>
        SUPPORT = 64, // 0x00000040

        /// <summary>
        /// Defines the HOLY.
        /// </summary>
        HOLY = 128, // 0x00000080

        /// <summary>
        /// Defines the DEAD_ONLY.
        /// </summary>
        DEAD_ONLY = 512, // 0x00000200

        /// <summary>
        /// Defines the KIT_RELATED.
        /// </summary>
        KIT_RELATED = 1024, // 0x00000400

        /// <summary>
        /// Defines the NO_POSSESSION.
        /// </summary>
        NO_POSSESSION = 2048, // 0x00000800

        /// <summary>
        /// Defines the NOT_BEEN_POSSESSED.
        /// </summary>
        NOT_BEEN_POSSESSED = 4096, // 0x00001000

        /// <summary>
        /// Defines the HEART_SKILL.
        /// </summary>
        HEART_SKILL = 8192, // 0x00002000
    }
}
