namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_SYSTEM_MESSAGE" />.
    /// </summary>
    public class SSMG_SYSTEM_MESSAGE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SYSTEM_MESSAGE"/> class.
        /// </summary>
        public SSMG_SYSTEM_MESSAGE()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)1010;
        }

        /// <summary>
        /// Sets the Message.
        /// </summary>
        public SSMG_SYSTEM_MESSAGE.Messages Message
        {
            set
            {
                this.PutUShort((ushort)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the Messages.
        /// </summary>
        public enum Messages
        {
            /// <summary>
            /// Defines the GAME_SMSG_RECV_SAVEPOINT_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SAVEPOINT_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SHORTOFMONEY_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SHORTOFMONEY_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SHORTOFDEPOSIT_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SHORTOFDEPOSIT_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_WAREHOUSECROWDED_TEXT.
            /// </summary>
            GAME_SMSG_RECV_WAREHOUSECROWDED_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_POSTUREBLOW_TEXT.
            /// </summary>
            GAME_SMSG_RECV_POSTUREBLOW_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_POSTURESLASH_TEXT.
            /// </summary>
            GAME_SMSG_RECV_POSTURESLASH_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_POSTURESTAB_TEXT.
            /// </summary>
            GAME_SMSG_RECV_POSTURESTAB_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_POSTURETHROW_TEXT.
            /// </summary>
            GAME_SMSG_RECV_POSTURETHROW_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_POSTUREERROR_TEXT.
            /// </summary>
            GAME_SMSG_RECV_POSTUREERROR_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_SUCCESS_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_SUCCESS_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_FAILED_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_FAILED_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_SKILLNOTFOUND_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_SKILLNOTFOUND_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_SLOT_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_SLOT_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_LEVEL_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_LEVEL_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_RACE_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_RACE_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_SEX_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_SEX_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_JOBLV_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_JOBLV_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_MASTERY_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_MASTERY_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_ELEMENT_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_ELEMENT_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_NEEDSKILL_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_NEEDSKILL_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_NOTLEARNSKILL_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_NOTLEARNSKILL_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_ALREADY_LEARNED_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_ALREADY_LEARNED_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SKILLLEARN_JOB_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SKILLLEARN_JOB_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SHOP_CARDNOTFOUND_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SHOP_CARDNOTFOUND_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SHOP_SHORTOFMONEY_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SHOP_SHORTOFMONEY_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SHOP_SHORTOFDEPOSIT_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SHOP_SHORTOFDEPOSIT_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_BANK_DEPOSITMAX_TEXT.
            /// </summary>
            GAME_SMSG_RECV_BANK_DEPOSITMAX_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_BANK_MONEY_MAX_TEXT.
            /// </summary>
            GAME_SMSG_RECV_BANK_MONEY_MAX_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_WAREHOUSE_FULL_TEXT.
            /// </summary>
            GAME_SMSG_RECV_WAREHOUSE_FULL_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_PLACE_NOT_USE_TEXT.
            /// </summary>
            GAME_SMSG_RECV_PLACE_NOT_USE_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_CAPACITY_OVER_TEXT.
            /// </summary>
            GAME_SMSG_RECV_CAPACITY_OVER_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_PAYLOAD_OVER_TEXT.
            /// </summary>
            GAME_SMSG_RECV_PAYLOAD_OVER_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_TRANCER_CAPACITY_OVER_TEXT.
            /// </summary>
            GAME_SMSG_RECV_TRANCER_CAPACITY_OVER_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_TRANCER_PAYLOAD_OVER_TEXT.
            /// </summary>
            GAME_SMSG_RECV_TRANCER_PAYLOAD_OVER_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_IDENTIFY_BY_STACK_TEXT.
            /// </summary>
            GAME_SMSG_RECV_IDENTIFY_BY_STACK_TEXT,

            /// <summary>
            /// Defines the GAME_SMSG_RECV_SHOP_ITEMMAX_TEXT.
            /// </summary>
            GAME_SMSG_RECV_SHOP_ITEMMAX_TEXT,
        }
    }
}
