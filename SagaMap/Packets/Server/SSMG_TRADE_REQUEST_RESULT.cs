namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_TRADE_REQUEST_RESULT" />.
    /// </summary>
    public class SSMG_TRADE_REQUEST_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_TRADE_REQUEST_RESULT"/> class.
        /// </summary>
        public SSMG_TRADE_REQUEST_RESULT()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)2571;
        }

        /// <summary>
        /// Sets the Result
        /// GAME_SMSG_TRADE_REQERR1,";トレード中です";
        /// GAME_SMSG_TRADE_REQERR2,";イベント中です";
        /// GAME_SMSG_TRADE_REQERR3,";相手がトレード中です";
        /// GAME_SMSG_TRADE_REQERR4,";相手がイベント中です";
        /// GAME_SMSG_TRADE_REQERR5,";相手が見つかりません";
        /// GAME_SMSG_TRADE_REQERR6,";トレードを断られました";
        /// GAME_SMSG_TRADE_REQERR7,";ゴーレムショップ起動中です";
        /// GAME_SMSG_TRADE_REQERR8,";相手がゴーレムショップ起動中です";
        /// GAME_SMSG_TRADE_REQERR9,";憑依中です";
        /// GAME_SMSG_TRADE_REQERR10,";相手が憑依中です";
        /// GAME_SMSG_TRADE_REQERR11,";相手のトレード設定が不許可になっています";
        /// GAME_SMSG_TRADE_REQERR12,";トレードを行える状態ではありません";
        /// GAME_SMSG_TRADE_REQERR13,";トレード相手との距離が離れすぎています";.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }
    }
}
