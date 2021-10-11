namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Defines the JoinRes.
    /// </summary>
    public enum JoinRes
    {
        /// <summary>
        /// Defines the RECRUIT_DELETED.
        /// </summary>
        RECRUIT_DELETED = -7,

        /// <summary>
        /// Defines the SELF.
        /// </summary>
        SELF = -6,

        /// <summary>
        /// Defines the ALREADY_IN_PARTY.
        /// </summary>
        ALREADY_IN_PARTY = -5,

        /// <summary>
        /// Defines the TARGET_OFFLINE.
        /// </summary>
        TARGET_OFFLINE = -4,

        /// <summary>
        /// Defines the PARTY_FULL.
        /// </summary>
        PARTY_FULL = -3,

        /// <summary>
        /// Defines the REJECTED.
        /// </summary>
        REJECTED = -2,

        /// <summary>
        /// Defines the DB_ERROR.
        /// </summary>
        DB_ERROR = -1,

        /// <summary>
        /// Defines the OK.
        /// </summary>
        OK = 0,
    }
}
