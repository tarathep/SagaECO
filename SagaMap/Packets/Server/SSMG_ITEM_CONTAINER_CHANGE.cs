namespace SagaMap.Packets.Server
{
    using SagaDB.Item;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_CONTAINER_CHANGE" />.
    /// </summary>
    public class SSMG_ITEM_CONTAINER_CHANGE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_CONTAINER_CHANGE"/> class.
        /// </summary>
        public SSMG_ITEM_CONTAINER_CHANGE()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)2531;
        }

        /// <summary>
        /// Sets the InventorySlot.
        /// </summary>
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Result
        /// GAME_SMSG_ITEM_MOVEERR1,";存在しないアイテムです";
        /// GAME_SMSG_ITEM_MOVEERR2,";アイテム数が不足しています";
        /// GAME_SMSG_ITEM_MOVEERR3,";アイテムを移動することが出来ません";
        /// GAME_SMSG_ITEM_MOVEERR4,";憑依中は装備を解除することが出来ません";
        /// GAME_SMSG_ITEM_MOVEERR5,";これ以上アイテムを所持することはできません";
        /// GAME_SMSG_ITEM_MOVEERR6,";憑依者待機中は装備を解除することが出来ません";
        /// GAME_SMSG_ITEM_MOVEERR7,";トレード中はアイテムを移動出来ません";
        /// GAME_SMSG_ITEM_MOVEERR8,";イベント中はアイテムを移動できません";.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Target.
        /// </summary>
        public ContainerType Target
        {
            set
            {
                this.PutByte((byte)value, (ushort)7);
            }
        }
    }
}
