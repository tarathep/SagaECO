namespace SagaMap.Packets.Server
{
    using SagaDB.Quests;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_QUEST_DETAIL" />.
    /// </summary>
    public class SSMG_QUEST_DETAIL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_QUEST_DETAIL"/> class.
        /// </summary>
        public SSMG_QUEST_DETAIL()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)6546;
        }

        /// <summary>
        /// The SetDetail.
        /// </summary>
        /// <param name="type">任务类型.</param>
        /// <param name="name">任务名字.</param>
        /// <param name="mapid1">未知地图ID.</param>
        /// <param name="mapid2">传送任务起始地图.</param>
        /// <param name="mapid3">传送任务目标地图.</param>
        /// <param name="info1">未知NPC名.</param>
        /// <param name="info2">传送任务起始NPC.</param>
        /// <param name="info3">传送任务目标NPC.</param>
        /// <param name="unk1">击退任务怪物地图1.</param>
        /// <param name="unk2">击退任务怪物地图2.</param>
        /// <param name="unk3">击退任务怪物地图3.</param>
        /// <param name="item1">物品或怪物ID.</param>
        /// <param name="item2">物品或怪物ID.</param>
        /// <param name="item3">物品或怪物ID.</param>
        /// <param name="amount1">物品或怪物数量.</param>
        /// <param name="amount2">物品或怪物数量.</param>
        /// <param name="amount3">物品或怪物数量.</param>
        /// <param name="time">剩余时间.</param>
        /// <param name="unk4">.</param>
        public void SetDetail(QuestType type, string name, uint mapid1, uint mapid2, uint mapid3, string info1, string info2, string info3, uint unk1, uint unk2, uint unk3, uint item1, uint item2, uint item3, uint amount1, uint amount2, uint amount3, int time, uint unk4)
        {
            byte[] bytes1 = Global.Unicode.GetBytes(name + "\0");
            byte[] bytes2 = Global.Unicode.GetBytes(info1);
            byte[] bytes3 = Global.Unicode.GetBytes(info2);
            byte[] bytes4 = Global.Unicode.GetBytes(info3);
            byte[] numArray = new byte[68 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length];
            this.data.CopyTo((Array)numArray, 0);
            this.data = numArray;
            this.PutByte((byte)type, (ushort)2);
            this.PutByte((byte)bytes1.Length, (ushort)3);
            this.PutBytes(bytes1, (ushort)4);
            this.PutByte((byte)3, (ushort)(4 + bytes1.Length));
            this.PutUInt(mapid1, (ushort)(5 + bytes1.Length));
            this.PutUInt(mapid2, (ushort)(9 + bytes1.Length));
            this.PutUInt(mapid3, (ushort)(13 + bytes1.Length));
            this.PutByte((byte)3, (ushort)(17 + bytes1.Length));
            this.PutByte((byte)bytes2.Length, (ushort)(18 + bytes1.Length));
            this.PutBytes(bytes2, (ushort)(19 + bytes1.Length));
            this.PutByte((byte)bytes3.Length, (ushort)(19 + bytes1.Length + bytes2.Length));
            this.PutBytes(bytes3, (ushort)(20 + bytes1.Length + bytes2.Length));
            this.PutByte((byte)bytes4.Length, (ushort)(20 + bytes1.Length + bytes2.Length + bytes3.Length));
            this.PutBytes(bytes4, (ushort)(21 + bytes1.Length + bytes2.Length + bytes3.Length));
            this.PutByte((byte)3, (ushort)(21 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(unk1, (ushort)(22 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(unk2, (ushort)(26 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(unk3, (ushort)(30 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutByte((byte)3, (ushort)(34 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(item1, (ushort)(35 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(item2, (ushort)(39 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(item3, (ushort)(43 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutByte((byte)3, (ushort)(47 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(amount1, (ushort)(48 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(amount2, (ushort)(52 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(amount3, (ushort)(56 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutInt(time, (ushort)(60 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
            this.PutUInt(unk4, (ushort)(64 + bytes1.Length + bytes2.Length + bytes3.Length + bytes4.Length));
        }
    }
}
