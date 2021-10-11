namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_WARE_PUT_RESULT" />.
    /// </summary>
    public class SSMG_ITEM_WARE_PUT_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_WARE_PUT_RESULT"/> class.
        /// </summary>
        public SSMG_ITEM_WARE_PUT_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)2558;
        }

        /// <summary>
        /// Sets the Result
        /// 0
        /// 成功
        /// -1～-8
        /// GAME_SMSG_WAREHOUSE_ERR1,";倉庫を開けていません";
        /// GAME_SMSG_WAREHOUSE_ERR2,";指定されたアイテムは存在しません";
        /// GAME_SMSG_WAREHOUSE_ERR3,";指定された数量が不正です";
        /// GAME_SMSG_WAREHOUSE_ERR4,";倉庫のアイテム数が上限を超えてしまうためキャンセルされました";
        /// GAME_SMSG_WAREHOUSE_ERR5,";キャラのアイテム数が100個を超えてしまうためキャンセルされました";
        /// GAME_SMSG_WAREHOUSE_ERR6,";イベントアイテムは預けられません";
        /// GAME_SMSG_WAREHOUSE_ERR7,";指定した格納場所は使用できません";
        /// GAME_SMSG_WAREHOUSE_ERR8,";変身中のマリオネットは預ける事ができません";
        /// それ以外
        /// GAME_SMSG_WAREHOUSE_ERR99,";倉庫移動に失敗しました";.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutInt(value, (ushort)2);
            }
        }
    }
}
