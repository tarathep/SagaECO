namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_PUT_ERROR" />.
    /// </summary>
    public class SSMG_ITEM_PUT_ERROR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_PUT_ERROR"/> class.
        /// </summary>
        public SSMG_ITEM_PUT_ERROR()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)2001;
        }

        /// <summary>
        /// Sets the ErrorID
        /// GAME_SMSG_ITEM_DROPERR2,";存在しないアイテムです";
        /// GAME_SMSG_ITEM_DROPERR3,";イベントアイテムなので捨てることが出来ません";
        /// GAME_SMSG_ITEM_DROPERR4,";変身中のマリオネットは捨てることが出来ません";
        /// GAME_SMSG_ITEM_DROPERR5,";起動中のゴーレムは捨てることが出来ません";
        /// GAME_SMSG_ITEM_DROPERR6,";ゴーレムショップに出品中のアイテムは捨てることが出来ません";
        /// GAME_SMSG_ITEM_DROPERR7,";行動不能時はアイテムを捨てる事が出来ません";
        /// GAME_SMSG_ITEM_DROPERR8,";トレード中はアイテムを捨てる事が出来ません";
        /// GAME_SMSG_ITEM_DROPERR9,";使用中のアイテムを捨てる事はできません";
        /// GAME_SMSG_ITEM_DROPERR10,";イベント中はアイテムを捨てることが出来ません";
        /// GAME_SMSG_ITEM_DROPERR11,";装備中のアイテムは捨てることが出来ません";
        /// GAME_SMSG_ITEM_DROPERR15,"ゲストID期間中はアイテムをドロップすることができません。アイテムを捨てたい場合は、アイテムウィンドウにあるゴミ箱のアイコンをクリックしてください"
        /// GAME_SMSG_ITEM_DROPERR16_0,"「あ、あわわわわ･･･それは捨てることはできませんよ！」"
        /// GAME_SMSG_ITEM_DROPERR16_1,"「ギルド商人までお持ちください。」"
        /// GAME_SMSG_ITEM_DROPERR17,"レンタルアイテムは捨てることが出来ません"
        /// GAME_SMSG_ITEM_DROPERR18,"ＤＥＭ強化チップを捨てることが出来ません".
        /// </summary>
        public int ErrorID
        {
            set
            {
                this.PutByte((byte)value, (ushort)6);
            }
        }
    }
}
