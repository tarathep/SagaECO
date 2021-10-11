using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaLib;
namespace SagaScript
{
    public class BOX : Event
    {
        public BOX()
        {
            this.EventID = 110;
            this.alreadyHasQuest = "任務順利嗎？$R;";
            this.gotNormalQuest = "那拜託了$R;" +
                "$R等任務結束後，再來找我吧;";
            this.gotTransportQuest = "是阿，道具太重了吧$R;" +
                "所以不能一次傳送的話$R;" +
                "分幾次給就可以！;";
            this.questCompleted = "真是辛苦了$R;" +
                "$R任務成功了$R來！收報酬吧！;";
            this.transport = "哦哦…全部收來了嗎？;";
            this.questCanceled = "嗯…如果是你，我相信你能做到的$R;" +
                "很期待呢……;";
            this.questFailed = "……$P失敗了？$R;" +
                "$R客人真是出大事了$R;" +
                "該說什麼好啊?$R;" +
                "$P這次實在沒辦法了$R;" +
                "可下次一定要成功啊！知道了吧？;";
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "嗯…$R;" +
                "$R現在沒有要特別拜託的事情啊$R;" +
                "再去冒險怎麼樣？$R;";
            this.questTooEasy = "唔…但是對你來說$R;" +
                "說不定是太簡單的事情$R;" +
                "$R那樣也沒關係嘛？$R;";
            AddGoods(10000103, 10000102, 10000108, 10034104, 10026400, 61010000, 50001954, 50070050, 50062100, 50003352, 50010351, 60010051);
            BuyLimit = 1000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            TT的万能BOX(pc);
        }

        void TT的万能BOX(ActorPC pc)
        {
            Say(pc, 0, "现在是声望值:" + pc.Fame);

            switch (Select(pc, "TT的測試BOX", "", "咖啡馆任务", "购物", "移动速度", "倉庫(中央)", "千姬", "GM", "发型", "HH", "没事"))
            {
                case 1:
                    HandleQuest(pc, 6);
                    return;
                case 2:
                    OpenShopBuy(pc);
                    return;
                case 3:
                    switch (Select(pc, "移动速度", "", "基本速度(420)", "2倍速度(840)", "没事"))
                    {
                        case 1:
                            pc.Speed = 420;
                            return;
                        case 2:
                            pc.Speed = 840;
                            return;
                    }
                    return;
                case 4:
                    OpenWareHouse(pc, SagaDB.Item.WarehousePlace.Acropolis);
                    return;
                case 5:
                    switch (Select(pc, "千姬", "", "任务", "飞空艇1", "飞空艇2", "没事"))
                    {
                        case 1:
                            HandleQuest(pc, 31);
                            return;
                        case 2:
                            pc.CInt["Beginner_Map"] = CreateMapInstance(50011000, 10023000, 95, 165);

                            LoadSpawnFile(pc.CInt["Beginner_Map"], "./DB/Spawns/50011000.xml");

                            Warp(pc, (uint)pc.CInt["Beginner_Map"], 7, 11);
                            return;
                        case 3:
                            pc.CInt["Beginner_Map"] = CreateMapInstance(50012000, 10023000, 95, 165);

                            Warp(pc, (uint)pc.CInt["Beginner_Map"], 7, 11);
                            return;
                    }
                    return;
                case 6:
                    switch (Select(pc, "GM用工具", "", "传送", "洗点", "合成", "技能点", "声望", "没事"))
                    {
                        case 1:
                            根目录(pc);
                            return;
                        case 2:
                            switch (Select(pc, "哪个呢", "", "一转技能", "二转技能", "属性点", "没事"))
                            {
                                case 1:
                                    ResetSkill(pc, 1);
                                    Say(pc, 0, "洗点成功");
                                    break;
                                case 2:
                                    ResetSkill(pc, 2);
                                    Say(pc, 0, "洗点成功");
                                    break;
                                case 3:
                                    ResetStatusPoint(pc);
                                    Say(pc, 0, "洗点成功");
                                    break;
                            }
                            return;
                        case 3:
                            合成(pc);
                            return;
                        case 4:
                            switch (Select(pc, "接受哪種技能點數呀？", "", "暫時想一想", "基本職業", "專門職業", "技術職業"))
                            {
                                case 1:
                                    break;
                                case 2:
                                    Wait(pc, 1000);
                                    PlaySound(pc, 3087, false, 100, 50);
                                    ShowEffect(pc, 4131);
                                    Wait(pc, 1000);
                                    SkillPointBonus(pc, 1);
                                    Say(pc, 131, "基本職業的技能點數上升1了$R;");
                                    break;
                                case 3:
                                    Wait(pc, 1000);
                                    PlaySound(pc, 3087, false, 100, 50);
                                    ShowEffect(pc, 4131);
                                    Wait(pc, 1000);
                                    SkillPointBonus2T(pc, 1);
                                    Say(pc, 131, "專門職業的技能點數上升1了$R;");
                                    break;
                                case 4:
                                    Wait(pc, 1000);
                                    PlaySound(pc, 3087, false, 100, 50);
                                    ShowEffect(pc, 4131);
                                    Wait(pc, 1000);
                                    SkillPointBonus2X(pc, 1);
                                    Say(pc, 131, "技術職業的技能點數上升1了$R;");
                                    break;
                            }
                            return;
                        case 5:
                            pc.Fame += uint.Parse(InputBox(pc, "增加多少声望?", InputType.ItemCode));
                            return;

                    }
                    return;
                case 7:
                    byte count;
                    switch (Select(pc, "？", "", "HairStyle", "Wig", "放棄"))
                    {
                        case 1:
                            count = byte.Parse(InputBox(pc, "輸入", InputType.ItemCode));
                            pc.Wig = (byte)count;
                            return;
                        case 2:
                            count = byte.Parse(InputBox(pc, "輸入", InputType.ItemCode));
                            pc.HairStyle = (byte)count;
                            return;
                    }
                    return;
                case 8:

                    int[] a = { 60002951, 60101351, 60101451, 60101551, 60101651, 60101751, 60101851, 60101951, 60102051, 60102151, 60102251, 60102351, 60102451, 60102551, 60102651, 60102751, 60102851, 60102951, 60103051, 60103151, 60103251, 60103351, 60103451, 60103551 };
                    int[] b = { 60002950, 60101350, 60101450, 60101550, 60101650, 60101750, 60101850, 60101950, 60102050, 60102150, 60102250, 60102350, 60102450, 60102550, 60102650, 60102750, 60102850, 60102950, 60103050, 60103150, 60103250, 60103350, 60103450, 60103550 };

                    for (int i = 0; i < a.Length; i++)
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].ItemID == a[i])
                        {
                            Say(pc, 131, i.ToString());
                            //te = i;
                            break;
                        }
                    }
                    return;
                case 9:
                    return;
            }
        }

        void 根目录(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "公共集合場所(GM房間)", "特殊", "唐卡島・瑪衣瑪衣島→(SAGA4)", "阿高普路斯市(SAGA0)", "奧克魯尼亞大陸(SAGA0)", "帕斯特島(SAGA3)", "摩根島→(SAGA4)", "諾頓島(SAGA1)", "薩烏斯火山島(SAGA2)", "地牢", "放棄"))
            {
                case 1:
                    Warp(pc, 40000000, 2, 2);
                    break;
                case 2:
                    特殊(pc);
                    break;
                case 3:
                    唐卡島AND瑪衣瑪衣島(pc);
                    break;
                case 4:
                    吊桥(pc);
                    break;
                case 5:
                    野外0(pc);
                    break;
                case 6:
                    帕斯特島(pc);
                    break;
                case 7:
                    摩根(pc);
                    break;
                case 8:
                    switch (Select(pc, "想去哪呢？", "", "諾頓市", "北方高原", "北方中央山脈（往城市）", "北方中央山脈（往地牢）", "北方中央山脈（哪邊也不是）", "永遠的北方邊界（北邊）", "永遠的北方邊界（南邊）", "愛伊斯島", "回去"))
                    {
                        case 1:
                            中央(pc);
                            break;
                        case 2:
                            Warp(pc, 10049000, 146, 251);
                            break;
                        case 3:
                            Warp(pc, 10050000, 52, 251);
                            break;
                        case 4:
                            Warp(pc, 10050001, 226, 134);
                            break;
                        case 5:
                            Warp(pc, 10050000, 194, 251);
                            break;
                        case 6:
                            Warp(pc, 10051000, 251, 37);
                            break;
                        case 7:
                            Warp(pc, 10051000, 251, 119);
                            break;
                        case 8:
                            Warp(pc, 10051100, 62, 150);
                            break;
                        case 9:
                            根目录(pc);
                            break;
                    }
                    break;
                case 9:
                    薩烏斯火山島(pc);
                    break;
                case 10:
                    洞(pc);
                    break;
                case 11:

                    break;
            }
        }

        void 吊桥(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "東邊吊橋", "西邊吊橋", "南邊吊橋", "北邊吊橋", "上城建築內部", "上城", "下城建築内部", "下城", "回去"))
            {
                case 1:
                    Warp(pc, 10023100, 252, 126);
                    break;
                case 2:
                    Warp(pc, 10023200, 2, 126);
                    break;
                case 3:
                    Warp(pc, 10023300, 126, 252);
                    break;
                case 4:
                    Warp(pc, 10023400, 126, 2);
                    break;
                case 5:
                    中央1(pc);
                    break;
                case 6:
                    switch (Select(pc, "想去哪呢？", "", "行會宮殿", "白聖堂", "黑聖堂", "門（東邊）", "門（西邊）", "門（南邊）", "門（北邊）", "髮型屋", "回去"))
                    {
                        case 1:
                            Warp(pc, 10023000, 126, 93);
                            break;
                        case 2:
                            Warp(pc, 10023000, 163, 126);
                            break;
                        case 3:
                            Warp(pc, 10023000, 90, 126);
                            break;
                        case 4:
                            Warp(pc, 10023000, 217, 126);
                            break;
                        case 5:
                            Warp(pc, 10023000, 37, 126);
                            break;
                        case 6:
                            Warp(pc, 10023000, 126, 218);
                            break;
                        case 7:
                            Warp(pc, 10023000, 126, 37);
                            break;
                        case 8:
                            Warp(pc, 50029000, 10, 10);
                            break;
                        case 9:
                            吊桥(pc);
                            break;
                    }
                    break;
                case 7:
                    中央0(pc);
                    break;
                case 8:
                    switch (Select(pc, "想去哪呢？", "", "東邊階梯下面酒吧", "西邊階梯下面古董商店前", "南邊階梯下面武器商店", "北邊階梯下面占卜店", "劇場1前", "劇場2前", "劇場3前", "劇場4前", "回去"))
                    {
                        case 1:
                            Warp(pc, 10024000, 207, 126);
                            break;
                        case 2:
                            Warp(pc, 10024000, 48, 126);
                            break;
                        case 3:
                            Warp(pc, 10024000, 126, 207);
                            break;
                        case 4:
                            Warp(pc, 10024000, 126, 48);
                            break;
                        case 5:
                            Warp(pc, 10024000, 143, 110);
                            break;
                        case 6:
                            Warp(pc, 10024000, 110, 143);
                            break;
                        case 7:
                            Warp(pc, 10024000, 143, 143);
                            break;
                        case 8:
                            Warp(pc, 10024000, 110, 110);
                            break;
                        case 9:
                            吊桥(pc);
                            break;
                    }
                    break;
                case 9:
                    根目录(pc);
                    break;
            }
        }

        void 中央1(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "行會宮殿", "白聖堂", "黑聖堂", "騎士團宮殿", "裁縫阿姨的家", "寶石商的家", "賢子首領的家", "預備", "回去"))
            {
                case 1:
                    行会宫殿(pc);
                    break;
                case 2:
                    Warp(pc, 30120000, 10, 21);
                    break;
                case 3:
                    Warp(pc, 30121000, 10, 21);
                    break;
                case 4:
                    switch (Select(pc, "想去哪呢？", "", "東軍入口", "東軍長官室", "西軍入口", "西軍長官室", "南軍入口", "南軍長官室", "北軍入口", "北軍長官室", "回去"))
                    {
                        case 1:
                            Warp(pc, 30030000, 2, 5);
                            break;
                        case 2:
                            Warp(pc, 30100000, 4, 14);
                            break;
                        case 3:
                            Warp(pc, 30031000, 2, 5);
                            break;
                        case 4:
                            Warp(pc, 30101000, 4, 14);
                            break;
                        case 5:
                            Warp(pc, 30032000, 2, 5);
                            break;
                        case 6:
                            Warp(pc, 30102000, 4, 14);
                            break;
                        case 7:
                            Warp(pc, 30033000, 2, 5);
                            break;
                        case 8:
                            Warp(pc, 30103000, 4, 14);
                            break;
                        case 9:
                            中央1(pc);
                            break;
                    }
                    break;
                case 5:
                    Warp(pc, 30020000, 3, 5);
                    break;
                case 6:
                    Warp(pc, 30021000, 3, 5);
                    break;
                case 7:
                    Warp(pc, 30022000, 3, 5);
                    break;
                case 9:
                    吊桥(pc);
                    break;
            }
        }

        void 中央0(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "東邊階梯下面酒吧", "西邊階梯下面古董商店前", "南邊階梯下面武器商店", "南邊階梯下面武器製造所", "北邊階梯下面占卜店", "解答士", "劇場内", "預備", "回去"))
            {
                case 1:
                    Warp(pc, 30010000, 3, 5);
                    break;
                case 2:
                    Warp(pc, 30014000, 3, 5);
                    break;
                case 3:
                    Warp(pc, 30012000, 3, 5);
                    break;
                case 4:
                    Warp(pc, 30013000, 3, 5);
                    break;
                case 5:
                    Warp(pc, 30001000, 2, 4);
                    break;
                case 6:
                    Warp(pc, 30002000, 3, 5);
                    break;
                case 7:
                    switch (Select(pc, "想去哪呢？", "", "劇場1樓門廊", "劇場2樓門廊", "劇場3樓門廊", "劇場4樓門廊", "劇場1", "劇場2", "劇場3", "劇場4", "回去"))
                    {
                        case 1:
                            Warp(pc, 30180000, 8, 16);
                            break;
                        case 2:
                            Warp(pc, 30181000, 8, 16);
                            break;
                        case 3:
                            Warp(pc, 30182000, 8, 16);
                            break;
                        case 4:
                            Warp(pc, 30183000, 8, 16);
                            break;
                        case 5:
                            Warp(pc, 30190000, 10, 20);
                            break;
                        case 6:
                            Warp(pc, 30194000, 10, 20);
                            break;
                        case 7:
                            Warp(pc, 30192000, 10, 20);
                            break;
                        case 8:
                            Warp(pc, 30196000, 10, 20);
                            break;
                        case 9:
                            中央0(pc);
                            break;
                    }
                    break;
                case 9:
                    吊桥(pc);
                    break;
            }
        }

        void 行会宫殿(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "1樓門廊", "2樓門廊", "3樓門廊", "4樓門廊", "5樓門廊", "2樓-3樓的閣房", "4樓-5樓的閣房", "預備", "回去"))
            {
                case 1:
                    Warp(pc, 30110000, 12, 14);
                    break;
                case 2:
                    Warp(pc, 30111000, 12, 14);
                    break;
                case 3:
                    Warp(pc, 30112000, 12, 14);
                    break;
                case 4:
                    switch (Select(pc, "想去哪呢？", "", "普通", "任务"))
                    {
                        case 1:
                            Warp(pc, 30113000, 12, 14);
                            break;
                        case 2:
                            Warp(pc, 30113001, 12, 14);
                            break;
                    }
                    break;
                case 5:
                    Warp(pc, 30114000, 12, 14);
                    break;
                case 6:
                    switch (Select(pc, "想去哪呢？", "", "2樓劍士行會", "2樓騎士行會", "2樓盜賊行會", "2樓弓手行會", "3樓魔法系行會", "3樓鐵匠行會", "3樓機械神匠行會", "農夫行會", "回去"))
                    {
                        case 1:
                            Warp(pc, 30040000, 3, 6);
                            break;
                        case 2:
                            Warp(pc, 30041000, 3, 6);
                            break;
                        case 3:
                            Warp(pc, 30042000, 3, 6);
                            break;
                        case 4:
                            Warp(pc, 30043000, 3, 6);
                            break;
                        case 5:
                            Warp(pc, 30044000, 3, 6);
                            break;
                        case 6:
                            Warp(pc, 30045000, 3, 6);
                            break;
                        case 7:
                            Warp(pc, 30046000, 3, 6);
                            break;
                        case 8:
                            Warp(pc, 30047000, 3, 6);
                            break;
                        case 9:
                            行会宫殿(pc);
                            break;
                    }
                    break;
                case 7:
                    switch (Select(pc, "想去哪呢？", "", "4樓鍊金術師行會", "4樓活動木偶術師行會", "4樓冒險家行會", "4樓商人行會", "5樓天使族代理人房間", "5樓惡魔族代理人房間", "5樓少數民族代理人房間", "5樓謎語團種族的房間", "回去"))
                    {
                        case 1:
                            Warp(pc, 30048000, 3, 6);
                            break;
                        case 2:
                            Warp(pc, 30049000, 3, 6);
                            break;
                        case 3:
                            Warp(pc, 30050000, 3, 6);
                            break;
                        case 4:
                            Warp(pc, 30051000, 3, 6);
                            break;
                        case 5:
                            Warp(pc, 30052000, 3, 6);
                            break;
                        case 6:
                            Warp(pc, 30053000, 3, 6);
                            break;
                        case 7:
                            Warp(pc, 30054000, 3, 6);
                            break;
                        case 8:
                            Warp(pc, 30055000, 3, 6);
                            break;
                        case 9:
                            行会宫殿(pc);
                            break;
                    }
                    break;
                case 9:
                    中央1(pc);
                    break;
            }
        }

        void 野外0(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "平原東方", "平原西方", "平原南方", "平原北方", "東域的村落", "荒廢礦村", "北域的村落", "南域的村落", "回去"))
            {
                case 1:
                    野外(pc);
                    break;
                case 2:
                    switch (Select(pc, "想去哪呢？", "", "奧克魯尼亞西方平原", "殺人蜂山路", "不死之島海邊", "不死之島", "摩斯科懸崖", "軍艦島", "伸手可及的地方→", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10022000, 252, 126);
                            break;
                        case 2:
                            Warp(pc, 10020000, 252, 158);
                            break;
                        case 3:
                            Warp(pc, 10019000, 110, 111);
                            break;
                        case 4:
                            Warp(pc, 10019100, 93, 104);
                            break;
                        case 5:
                            Warp(pc, 10028000, 40, 1);
                            break;
                        case 6:
                            Warp(pc, 10035000, 244, 1);
                            break;
                        case 7:
                            switch (Select(pc, "想去哪呢？", "", "殺人蜂之路的小屋", "蜜蜂家前", "預備", "預備", "預備", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 10020000, 103, 73);
                                    break;
                                case 2:
                                    Warp(pc, 10020000, 182, 48);
                                    break;
                                case 9:
                                    野外0(pc);
                                    break;
                            }
                            break;
                        case 8:

                            break;
                        case 9:
                            野外0(pc);
                            break;
                    }
                    break;
                case 3:
                    switch (Select(pc, "想去哪呢？", "", "奧克魯尼亞南方平原", "果樹園", "大草原沙漠(北邊)", "大草原沙漠(南邊)", "烏特那江下游（平原方向）", "烏特那江下游（下游方向）", "南域（東邊）", "南域（西邊）", "回去"))
                    {
                        case 1:
                            Warp(pc, 10031000, 126, 1);
                            break;
                        case 2:
                            Warp(pc, 10030000, 192, 1);
                            break;
                        case 3:
                            Warp(pc, 10042000, 217, 1);
                            break;
                        case 4:
                            Warp(pc, 10042000, 79, 134);
                            break;
                        case 5:
                            Warp(pc, 10043000, 154, 2);
                            break;
                        case 6:
                            Warp(pc, 10043000, 1, 226);
                            break;
                        case 7:
                            Warp(pc, 10046000, 199, 1);
                            break;
                        case 8:
                            Warp(pc, 10046000, 56, 2);
                            break;
                        case 9:
                            野外0(pc);
                            break;
                    }
                    break;
                case 4:
                    switch (Select(pc, "想去哪呢？", "", "奥克魯尼亞北方平原", "北域", "新路寶雪原（入口）", "新路寶雪原（地牢）", "新路寶的山路（北邊）", "新路寶的山路（南邊）", "新路寶的岔路口（北邊）", "新路寶的岔路口（南邊）", "回去"))
                    {
                        case 1:
                            Warp(pc, 10014000, 126, 252);
                            break;
                        case 2:
                            Warp(pc, 10001000, 194, 252);
                            break;
                        case 3:
                            Warp(pc, 10002000, 252, 30);
                            break;
                        case 4:
                            Warp(pc, 10002000, 33, 220);
                            break;
                        case 5:
                            Warp(pc, 10003000, 2, 31);
                            break;
                        case 6:
                            Warp(pc, 10003000, 77, 252);
                            break;
                        case 7:
                            Warp(pc, 10004000, 193, 2);
                            break;
                        case 8:
                            Warp(pc, 10004000, 2, 209);
                            break;
                        case 9:
                            野外0(pc);
                            break;
                    }
                    break;
                case 5:
                    Warp(pc, 10018100, 194, 64);
                    break;
                case 6:
                    Warp(pc, 10035000, 54, 175);
                    break;
                case 7:
                    Warp(pc, 10001000, 96, 21);
                    break;
                case 8:
                    Warp(pc, 10046000, 150, 221);
                    break;
                case 9:
                    根目录(pc);
                    break;
            }
        }

        void 野外(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "奧克魯尼亞東方平原", "奧克魯尼亞叢林", "帕斯特島", "農夫牧場", "東域", "烏特納湖", "奧克魯尼亞東海岸", "伸手可及的地方→", "回去"))
            {
                case 1:
                    Warp(pc, 10025000, 1, 126);
                    break;
                case 2:
                    Warp(pc, 10015000, 202, 252);
                    break;
                case 3:
                    Warp(pc, 10017000, 2, 76);
                    break;
                case 4:
                    Warp(pc, 10017001, 133, 67);
                    break;
                case 5:
                    Warp(pc, 10018000, 2, 40);
                    break;
                case 6:
                    Warp(pc, 10032000, 61, 2);
                    break;
                case 7:
                    Warp(pc, 10034000, 1, 223);
                    break;
                case 8:
                    switch (Select(pc, "想去哪呢？", "", "烏特那湖沙漠", "光明精靈", "農夫牧場負責人前", "預備", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10032000, 136, 111);
                            break;
                        case 2:
                            Warp(pc, 10034000, 38, 111);
                            break;
                        case 3:
                            Warp(pc, 10017000, 133, 67);
                            break;
                        case 9:
                            野外(pc);
                            break;
                    }
                    break;
                case 9:
                    野外0(pc);
                    break;
            }
        }

        void 中央(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "城市入口", "宮殿", "女王禮賓室", "魔法行會總部", "大導師的房間", "商店東邊", "商店西邊", "預備", "回去"))
            {
                case 1:
                    Warp(pc, 10065000, 49, 195);
                    break;
                case 2:
                    Warp(pc, 10065000, 50, 21);
                    break;
                case 3:
                    Warp(pc, 10067000, 36, 61);
                    break;
                case 4:
                    Warp(pc, 10065000, 59, 80);
                    break;
                case 5:
                    Warp(pc, 30164000, 8, 16);
                    break;
                case 6:
                    switch (Select(pc, "想去哪呢？", "", "酒吧1", "武器商店", "占卜店", "商人1", "鑑定師", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 30010001, 3, 5);
                            break;
                        case 2:
                            Warp(pc, 30012001, 3, 5);
                            break;
                        case 3:
                            Warp(pc, 30001001, 2, 4);
                            break;
                        case 4:
                            Warp(pc, 30060001, 4, 7);
                            break;
                        case 5:
                            Warp(pc, 30011001, 3, 5);
                            break;
                        case 9:
                            中央(pc);
                            break;
                    }
                    break;
                case 7:
                    switch (Select(pc, "想去哪呢？", "", "鑑定師2", "商人2", "裁縫店", "寶石商", "酒吧2", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 30011002, 3, 5);
                            break;
                        case 2:
                            Warp(pc, 30060002, 4, 7);
                            break;
                        case 3:
                            Warp(pc, 30020002, 3, 5);
                            break;
                        case 4:
                            Warp(pc, 30021001, 3, 5);
                            break;
                        case 5:
                            Warp(pc, 0010002, 3, 5);
                            break;
                        case 9:
                            中央(pc);
                            break;
                    }
                    break;
                case 9:
                    switch (Select(pc, "想去哪呢？", "", "諾頓市", "北方高原", "北方中央山脈（往城市）", "北方中央山脈（往地牢）", "北方中央山脈（哪邊也不是）", "永遠的北方邊界（北邊）", "永遠的北方邊界（南邊）", "愛伊斯島", "回去"))
                    {
                        case 1:
                            中央(pc);
                            break;
                        case 2:
                            Warp(pc, 10049000, 146, 251);
                            break;
                        case 3:
                            Warp(pc, 10050000, 52, 251);
                            break;
                        case 4:
                            Warp(pc, 10050001, 226, 134);
                            break;
                        case 5:
                            Warp(pc, 10050000, 194, 251);
                            break;
                        case 6:
                            Warp(pc, 10051000, 251, 37);
                            break;
                        case 7:
                            Warp(pc, 10051000, 251, 119);
                            break;
                        case 8:
                            Warp(pc, 10051100, 62, 150);
                            break;
                        case 9:
                            根目录(pc);
                            break;
                    }
                    break;
            }
        }

        void 洞(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "大陸的洞窟", "凍結的耕道", "荒廢礦坑", "北部地牢", "無限回廊", "PVP地圖", "南部地牢", "蜂之故鄉", "回去"))
            {
                case 1:
                    switch (Select(pc, "大陸的洞窟", "", "大陸的洞窟B1F", "大陸的洞窟B2Ｆ", "大陸的洞窟B3Ｆ", "大陸的洞窟B4Ｆ", "大陸的洞窟B5Ｆ", "瑪歐斯的故鄉", "大陸的洞窟", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20000000, 62, 23);
                            break;
                        case 2:
                            Warp(pc, 20001000, 105, 106);
                            break;
                        case 3:
                            Warp(pc, 20002000, 21, 62);
                            break;
                        case 4:
                            Warp(pc, 20003000, 105, 62);
                            break;
                        case 5:
                            Warp(pc, 20004000, 62, 101);
                            break;
                        case 6:
                            //Warp(pc,); EVT10000336;
                            break;
                        case 7:
                            Warp(pc, 10034000, 36, 56);
                            break;
                        case 8:
                            break;
                        case 9:
                            洞(pc);
                            break;
                    }
                    break;
                case 2:
                    switch (Select(pc, "凍結的耕道", "", "凍結的耕道", "凍結的道", "凍結的耕道", "永恆沼澤之門", "凍結的耕道入口", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20010000, 62, 11);
                            break;
                        case 2:
                            Warp(pc, 20011000, 12, 108);
                            break;
                        case 3:
                            Warp(pc, 20012000, 113, 64);
                            break;
                        case 4:
                            Warp(pc, 20013000, 62, 67);
                            break;
                        case 5:
                            Warp(pc, 10002000, 49, 221);
                            break;
                        case 9:
                            洞(pc);
                            break;
                    }
                    break;
                case 3:
                    switch (Select(pc, "荒廢礦坑", "", "荒廢礦坑Ｂ1Ｆ", "荒廢礦坑Ｂ2Ｆ", "荒廢礦坑Ｂ3Ｆ", "荒廢礦坑入口", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20030000, 62, 10);
                            break;
                        case 2:
                            Warp(pc, 20031000, 62, 113);
                            break;
                        case 3:
                            Warp(pc, 20032000, 106, 113);
                            break;
                        case 4:
                            Warp(pc, 10035000, 43, 192);
                            break;
                        case 9:
                            洞(pc);
                            break;
                    }
                    break;
                case 4:
                    switch (Select(pc, "想去哪呢？", "", "北部地牢入口", "伊戈路前", "預備", "預備", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20014000, 241, 12);
                            break;
                        case 2:
                            Warp(pc, 20014000, 14, 160);
                            break;
                        case 9:
                            洞(pc);
                            break;
                    }
                    break;
                case 5:
                    switch (Select(pc, "想去哪呢？", "", "無限回廊B1樓", "無限回廊B5樓", "無限回廊B10樓", "無限回廊B15樓", "無限回廊B20樓", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20070000, 22, 83);
                            break;
                        case 2:
                            Warp(pc, 20070004, 22, 83);
                            break;
                        case 3:
                            Warp(pc, 20071009, 22, 83);
                            break;
                        case 4:
                            Warp(pc, 20070014, 22, 83);
                            break;
                        case 5:
                            Warp(pc, 20071019, 22, 83);
                            break;
                        case 9:
                            洞(pc);
                            break;
                    }
                    break;
                case 6:
                    switch (Select(pc, "想去哪呢？", "", "缺省競技場", "東軍團門廊", "西軍團門廊", "南軍團門廊", "北軍團門廊", "花隊vs岩石隊", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20080000, 23, 10);
                            break;
                        case 2:
                            Warp(pc, 20080007, 21, 21);
                            break;
                        case 3:
                            Warp(pc, 20080008, 21, 21);
                            break;
                        case 4:
                            Warp(pc, 20080009, 21, 21);
                            break;
                        case 5:
                            Warp(pc, 20080010, 21, 21);
                            break;
                        case 6:
                            Warp(pc, 20080011, 21, 21);
                            break;
                        case 9:
                            洞(pc);
                            break;
                    }
                    break;
                case 7:
                    switch (Select(pc, "南部地牢", "", "南部地牢Ｂ1Ｆ", "南部地牢Ｂ2Ｆ", "南部地牢Ｂ3Ｆ", "馬克特碼頭", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20020000, 99, 247);
                            break;
                        case 2:
                            Warp(pc, 20021000, 179, 73);
                            break;
                        case 3:
                            Warp(pc, 20022000, 126, 241);
                            break;
                        case 4:
                            Warp(pc, 20023000, 127, 251);
                            break;
                        case 9:
                            洞(pc);
                            break;
                    }
                    break;
                case 8:
                    Warp(pc, 20004002, 105, 101);
                    break;
                case 9:
                    根目录(pc);
                    break;
            }
        }

        void 唐卡島AND瑪衣瑪衣島(ActorPC pc)
        {
            switch (Select(pc, "唐卡島・瑪衣瑪衣島", "", "唐卡市入口", "唐卡市（房間）→", "唐卡市（房外）→", "瑪衣瑪衣島入口", "瑪衣瑪衣島部分→", "瑪衣瑪衣島地牢入口", "瑪衣瑪衣島地牢内→", "瑪衣瑪衣島地牢通路→", "回去"))
            {
                case 1:
                    Warp(pc, 10062000, 190, 42);
                    break;
                case 2:
                    唐卡市房間(pc);
                    break;
                case 3:
                    switch (Select(pc, "唐卡市（房屋）", "", "唐卡機場", "研究所前的廣場", "匠人小巷", "唐卡政務大樓前", "皮諾湧泉廣場", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10062000, 190, 42);
                            break;
                        case 2:
                            Warp(pc, 10062000, 179, 143);
                            break;
                        case 3:
                            Warp(pc, 10062000, 131, 28);
                            break;
                        case 4:
                            Warp(pc, 10062000, 92, 150);
                            break;
                        case 5:
                            Warp(pc, 10062000, 81, 201);
                            break;
                        case 9:
                            唐卡島AND瑪衣瑪衣島(pc);
                            break;
                    }
                    break;
                case 4:
                    Warp(pc, 10059000, 68, 147);
                    break;
                case 5:
                    switch (Select(pc, "瑪衣瑪衣島", "", "瑪衣瑪衣遺跡上", "猴麵包樹叢林（白天）", "猴麵包樹叢林（黑天）", "猴麵包樹叢林（陰天）", "晚上的瑪衣瑪衣遺跡周圍", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10059000, 92, 92);
                            break;
                        case 2:
                            Warp(pc, 10059100, 234, 35);
                            break;
                        case 3:
                            Warp(pc, 10059101, 234, 35);
                            break;
                        case 4:
                            Warp(pc, 10059102, 234, 35);
                            break;
                        case 5:
                            Warp(pc, 10059001, 103, 52);
                            break;
                        case 9:
                            根目录(pc);
                            break;
                    }
                    break;
                case 6:
                    Warp(pc, 20164000, 125, 125);
                    break;
                case 7:
                    switch (Select(pc, "瑪衣瑪衣地牢", "", "開始", "結束", "小房間1", "小房間2", "小房間3", "大房間1", "大房間2", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 60001000, 24, 24);
                            break;
                        case 2:
                            Warp(pc, 61004000, 83, 76);
                            break;
                        case 3:
                            Warp(pc, 63006000, 40, 40);
                            break;
                        case 4:
                            Warp(pc, 63007000, 27, 27);
                            break;
                        case 5:
                            Warp(pc, 63008000, 19, 19);
                            break;
                        case 6:
                            Warp(pc, 62002000, 47, 47);
                            break;
                        case 7:
                            Warp(pc, 62003000, 56, 56);
                            break;
                        case 8:

                            break;
                        case 9:
                            唐卡島AND瑪衣瑪衣島(pc);
                            break;
                    }
                    break;
                case 8:
                    switch (Select(pc, "瑪衣瑪衣地牢", "", "直綫長通路", "往右拐的通路", "往左拐的通路", "T字交叉路", "交叉路", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 64008000, 8, 56);
                            break;
                        case 2:
                            Warp(pc, 64009000, 24, 28);
                            break;
                        case 3:
                            Warp(pc, 64010000, 31, 23);
                            break;
                        case 4:
                            Warp(pc, 64011000, 37, 17);
                            break;
                        case 5:
                            Warp(pc, 64012000, 43, 41);
                            break;
                        case 9:
                            唐卡島AND瑪衣瑪衣島(pc);
                            break;
                    }
                    break;
                case 9:
                    根目录(pc);
                    break;
            }
        }

        void 唐卡市房間(ActorPC pc)
        {
            switch (Select(pc, "唐卡市（房間)", "", "活動木偶第1研究所", "活動木偶第2研究所", "活動木偶術師本部", "機械工程師本部", "多爾斯軍本部", "唐卡政務大樓", "個人工作室・民房→", "商店→", "回去"))
            {
                case 1:
                    Warp(pc, 30154000, 6, 15);
                    break;
                case 2:
                    Warp(pc, 30154001, 6, 15);
                    break;
                case 3:
                    Warp(pc, 30156000, 7, 16);
                    break;
                case 4:
                    Warp(pc, 30157000, 6, 13);
                    break;
                case 5:
                    Warp(pc, 30155000, 6, 11);
                    break;
                case 6:
                    Warp(pc, 30151000, 6, 13);
                    break;
                case 7:
                    switch (Select(pc, "個人工房・民房", "", "雷奧的活動木偶工作室", "莉塔的活動木偶工作室", "卡米羅的機械工場", "利迪亞的機械工場", "唐卡", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 30152000, 6, 11);
                            break;
                        case 2:
                            Warp(pc, 30152001, 6, 11);
                            break;
                        case 3:
                            Warp(pc, 30153000, 6, 11);
                            break;
                        case 4:
                            Warp(pc, 30153001, 6, 11);
                            break;
                        case 5:
                            Warp(pc, 30150000, 4, 7);
                            break;
                        case 9:
                            唐卡市房間(pc);
                            break;
                    }
                    break;
                case 8:
                    switch (Select(pc, "個人工房", "", "唐卡鑑定師", "唐卡商店（下坡）", "唐卡商店（上坡）", "唐卡寶石商", "唐卡秘密商店", "唐卡裁縫店", "唐卡武器商店", "唐卡古董商店", "唐卡咖啡館", "回去"))
                    {
                        case 1:
                            Warp(pc, 30011006, 3, 5);
                            break;
                        case 2:
                            Warp(pc, 30060007, 4, 7);
                            break;
                        case 3:
                            Warp(pc, 30060008, 4, 7);
                            break;
                        case 4:
                            Warp(pc, 30021005, 3, 5);
                            break;
                        case 5:
                            Warp(pc, 30002003, 3, 5);
                            break;
                        case 6:
                            Warp(pc, 30020007, 3, 5);
                            break;
                        case 7:
                            Warp(pc, 30012005, 3, 5);
                            break;
                        case 8:
                            Warp(pc, 30014003, 3, 5);
                            break;
                        case 9:
                            Warp(pc, 30010010, 3, 5);
                            break;
                        case 10:
                            唐卡市房間(pc);
                            break;
                    }
                    break;
                case 9:
                    唐卡島AND瑪衣瑪衣島(pc);
                    break;
            }
        }

        void 薩烏斯火山島(ActorPC pc)
        {
            switch (Select(pc, "薩烏斯火山島", "", "阿伊恩市", "鬼魂安息處（北邊）", "鬼魂安息處（南邊）", "阿伊恩薩烏斯街道", "鐵火山(北邊)", "鐵火山(南邊)", "避難所", "預備", "回去"))
            {
                case 1:
                    阿伊恩市内(pc);
                    break;
                case 2:
                    Warp(pc, 10061000, 144, 2);
                    break;
                case 3:
                    Warp(pc, 10061000, 26, 251);
                    break;
                case 4:
                    Warp(pc, 10063000, 161, 156);
                    break;
                case 5:
                    Warp(pc, 10064000, 26, 2);
                    break;
                case 6:
                    Warp(pc, 10064000, 2, 207);
                    break;
                case 7:
                    switch (Select(pc, "上層的商店", "", "避難所1", "避難所2", "避難所", "預備", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 30080000, 3, 6);
                            break;
                        case 2:
                            Warp(pc, 30080001, 3, 6);
                            break;
                        case 3:
                            Warp(pc, 30080002, 3, 6);
                            break;
                        case 9:
                            switch (Select(pc, "上層房間内", "", "商店", "合同大厦門廊", "議會門廊", "共軍本部門廊", "共軍長官室", "傭兵軍本部門廊", "傭兵軍長官室", "預備", "回去"))
                            {
                                case 1:
                                    switch (Select(pc, "上樓的商店", "", "上層裁縫室", "上層寶石商店", "上層商店", "上層鑑定師前", "上層酒吧", "上層富翁之家", "預備", "預備", "回去"))
                                    {
                                        case 1:
                                            Warp(pc, 30020003, 3, 5);
                                            break;
                                        case 2:
                                            Warp(pc, 30021002, 3, 5);
                                            break;
                                        case 3:
                                            Warp(pc, 30060003, 4, 7);
                                            break;
                                        case 4:
                                            Warp(pc, 30011003, 3, 5);
                                            break;
                                        case 5:
                                            Warp(pc, 30010003, 3, 5);
                                            break;
                                        case 6:
                                            Warp(pc, 30072000, 4, 7);
                                            break;

                                        case 9:
                                            switch (Select(pc, "上層房間内", "", "商店", "合同大厦門廊", "議會門廊", "共軍本部門廊", "共軍長官室", "傭兵軍本部門廊", "傭兵軍長官室", "預備", "回去"))
                                            {
                                                case 1:
                                                    阿伊恩市内(pc);
                                                    break;
                                                case 2:
                                                    Warp(pc, 30079000, 9, 19);
                                                    break;
                                                case 3:
                                                    Warp(pc, 30078000, 9, 17);
                                                    break;
                                                case 4:
                                                    Warp(pc, 30074000, 4, 8);
                                                    break;
                                                case 5:
                                                    Warp(pc, 30076000, 5, 10);
                                                    break;
                                                case 6:
                                                    Warp(pc, 30073000, 4, 8);
                                                    break;
                                                case 7:
                                                    Warp(pc, 30075000, 5, 10);
                                                    break;
                                                case 8:

                                                    break;
                                                case 9:
                                                    阿伊恩市内(pc);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 2:
                                    Warp(pc, 30079000, 9, 19);
                                    break;
                                case 3:
                                    Warp(pc, 30078000, 9, 17);
                                    break;
                                case 4:
                                    Warp(pc, 30074000, 4, 8);
                                    break;
                                case 5:
                                    Warp(pc, 30076000, 5, 10);
                                    break;
                                case 6:
                                    Warp(pc, 30073000, 4, 8);
                                    break;
                                case 7:
                                    Warp(pc, 30075000, 5, 10);
                                    break;
                                case 8:

                                    break;
                                case 9:
                                    阿伊恩市内(pc);
                                    break;
                            }
                            break;
                    }
                    break;
                case 8:

                    break;
                case 9:
                    根目录(pc);
                    break;
            }
        }

        void 阿伊恩市内(ActorPC pc)
        {
            switch (Select(pc, "阿伊恩市内", "", "阿伊恩市入口", "上層", "上層房間内", "下層", "下層房間内", "預備", "預備", "預備", "回去"))
            {
                case 1:
                    Warp(pc, 10063100, 139, 156);
                    break;
                case 2:
                    switch (Select(pc, "上層", "", "合同大厦", "議會", "共軍本部", "傭兵軍本部", "富翁家前", "上層鑑定師", "寶石商店", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10063100, 125, 103);
                            break;
                        case 2:
                            Warp(pc, 10063100, 50, 103);
                            break;
                        case 3:
                            Warp(pc, 10063100, 43, 71);
                            break;
                        case 4:
                            Warp(pc, 10063100, 47, 155);
                            break;
                        case 5:
                            Warp(pc, 10063100, 130, 132);
                            break;
                        case 6:
                            Warp(pc, 10063100, 121, 170);
                            break;
                        case 7:
                            Warp(pc, 10063100, 137, 177);
                            break;
                        case 8:

                            break;
                        case 9:
                            阿伊恩市内(pc);
                            break;
                    }
                    break;
                case 3:
                    switch (Select(pc, "上層房間内", "", "商店", "合同大厦門廊", "議會門廊", "共軍本部門廊", "共軍長官室", "傭兵軍本部門廊", "傭兵軍長官室", "預備", "回去"))
                    {
                        case 1:
                            switch (Select(pc, "上樓的商店", "", "上層裁縫室", "上層寶石商店", "上層商店", "上層鑑定師前", "上層酒吧", "上層富翁之家", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 30020003, 3, 5);
                                    break;
                                case 2:
                                    Warp(pc, 30021002, 3, 5);
                                    break;
                                case 3:
                                    Warp(pc, 30060003, 4, 7);
                                    break;
                                case 4:
                                    Warp(pc, 30011003, 3, 5);
                                    break;
                                case 5:
                                    Warp(pc, 30010003, 3, 5);
                                    break;
                                case 6:
                                    Warp(pc, 30072000, 4, 7);
                                    break;
                                case 7:

                                    break;
                                case 8:

                                    break;
                                case 9:
                                    switch (Select(pc, "上層房間内", "", "商店", "合同大厦門廊", "議會門廊", "共軍本部門廊", "共軍長官室", "傭兵軍本部門廊", "傭兵軍長官室", "預備", "回去"))
                                    {
                                        case 1:
                                            阿伊恩市内(pc);
                                            break;
                                        case 2:
                                            Warp(pc, 30079000, 9, 19);
                                            break;
                                        case 3:
                                            Warp(pc, 30078000, 9, 17);
                                            break;
                                        case 4:
                                            Warp(pc, 30074000, 4, 8);
                                            break;
                                        case 5:
                                            Warp(pc, 30076000, 5, 10);
                                            break;
                                        case 6:
                                            Warp(pc, 30073000, 4, 8);
                                            break;
                                        case 7:
                                            Warp(pc, 30075000, 5, 10);
                                            break;
                                        case 8:

                                            break;
                                        case 9:
                                            阿伊恩市内(pc);
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 2:
                            Warp(pc, 30079000, 9, 19);
                            break;
                        case 3:
                            Warp(pc, 30078000, 9, 17);
                            break;
                        case 4:
                            Warp(pc, 30074000, 4, 8);
                            break;
                        case 5:
                            Warp(pc, 30076000, 5, 10);
                            break;
                        case 6:
                            Warp(pc, 30073000, 4, 8);
                            break;
                        case 7:
                            Warp(pc, 30075000, 5, 10);
                            break;
                        case 8:

                            break;
                        case 9:
                            阿伊恩市内(pc);
                            break;
                    }
                    break;
                case 4:
                    switch (Select(pc, "下層", "", "南邊荒廢礦坑入口", "鐵廠", "北邊階梯(冶鍊所旁)", "北邊階梯(阿伊恩薩烏斯旁)", "大工廠", "南邊階梯(工廠旁)", "南邊階梯(公園旁)", "動力控制室", "回去"))
                    {
                        case 1:
                            Warp(pc, 10066000, 197, 80);
                            break;
                        case 2:
                            Warp(pc, 10066000, 154, 60);
                            break;
                        case 3:
                            Warp(pc, 10066000, 111, 68);
                            break;
                        case 4:
                            Warp(pc, 10066000, 51, 68);
                            break;
                        case 5:
                            Warp(pc, 10066000, 154, 145);
                            break;
                        case 6:
                            Warp(pc, 10066000, 113, 138);
                            break;
                        case 7:
                            Warp(pc, 10066000, 49, 137);
                            break;
                        case 8:
                            Warp(pc, 10066000, 175, 102);
                            break;
                        case 9:
                            阿伊恩市内(pc);
                            break;
                    }
                    break;
                case 5:
                    switch (Select(pc, "下層房間内", "", "鐵工所", "古董商店", "武器製造所", "武器商店", "大工廠", "下層鑑定師", "下層酒吧", "下層商店", "回去"))
                    {
                        case 1:
                            Warp(pc, 30082000, 10, 20);
                            break;
                        case 2:
                            Warp(pc, 30014001, 3, 5);
                            break;
                        case 3:
                            Warp(pc, 30013001, 3, 5);
                            break;
                        case 4:
                            Warp(pc, 30012002, 3, 5);
                            break;
                        case 5:
                            Warp(pc, 30081000, 10, 20);
                            break;
                        case 6:
                            Warp(pc, 30011004, 3, 5);
                            break;
                        case 7:
                            Warp(pc, 30010004, 3, 5);
                            break;
                        case 8:
                            Warp(pc, 30060004, 4, 7);
                            break;
                        case 9:
                            阿伊恩市内(pc);
                            break;
                    }
                    break;

                case 9:
                    薩烏斯火山島(pc);
                    break;
            }
        }

        void 帕斯特島(ActorPC pc)
        {
            switch (Select(pc, "帕斯特島", "", "帕斯特島入口（沒有許可証）", "帕斯特市", "平原", "東方地牢", "不死之城", "預備", "預備", "預備", "回去"))
            {
                case 1:
                    Warp(pc, 10056000, 3, 80);
                    break;
                case 2:
                    switch (Select(pc, " 帕斯特", "", "城市東邊(地牢方向)", "城市東邊(草原方向)", "城市南邊(榖倉方向)", "城市北邊(海盜團圍牆方向)", "房間", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10057000, 251, 114);
                            break;
                        case 2:
                            Warp(pc, 10057000, 2, 138);
                            break;
                        case 3:
                            Warp(pc, 10057000, 134, 251);
                            break;
                        case 4:
                            Warp(pc, 10057000, 166, 2);
                            break;
                        case 5:
                            switch (Select(pc, "帕斯特市的房間", "", "農家的房間(不可進入)", "教室(不可進入)", "辦公大樓祠堂門廊", "傭兵團門廊", "農夫行會總部門廊(不可進入)", "農夫行會會長室(不可進入)", "馬廄(左側上)", "馬廄(右側上)", "回去"))
                            {
                                case 1:
                                    Warp(pc, 20100000, 22, 11);
                                    break;
                                case 2:
                                    Warp(pc, 20103000, 1, 1);
                                    break;
                                case 3:
                                    Warp(pc, 30130000, 6, 9);
                                    break;
                                case 4:
                                    Warp(pc, 30131000, 5, 9);
                                    break;
                                case 5:
                                    Warp(pc, 10069000, 160, 177);
                                    break;
                                case 6:
                                    Warp(pc, 20106000, 2, 32);
                                    break;
                                case 7:
                                    Warp(pc, 30134000, 4, 15);
                                    break;
                                case 8:
                                    Warp(pc, 30134001, 4, 15);
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 9:
                            帕斯特島(pc);
                            break;
                    }
                    break;
                case 3:
                    switch (Select(pc, "平原", "", "帕斯特草原(東域方向)", "帕斯特草原(往城市)", "帕斯特草原(圍牆方向)", "海盜島的圍牆(往城市)", "榖倉地帶(往城市)", "榖倉地帶(不死之城方向)", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10056000, 3, 80);
                            break;
                        case 2:
                            Warp(pc, 10056000, 251, 140);
                            break;
                        case 3:
                            Warp(pc, 10056000, 207, 2);
                            break;
                        case 4:
                            Warp(pc, 10054001, 166, 251);
                            break;
                        case 5:
                            Warp(pc, 10068000, 134, 2);
                            break;
                        case 6:
                            Warp(pc, 10068000, 194, 214);
                            break;
                        case 9:
                            帕斯特島(pc);
                            break;
                    }
                    break;
                case 4:
                    switch (Select(pc, "東方地牢", "", "一號地牢入口", "脫離的地點", "地牢", "開始地點", "三號地牢東方水源", "正常地點", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20090000, 111, 251);
                            break;
                        case 2:
                            Warp(pc, 20090000, 34, 66);
                            break;
                        case 3:
                            Warp(pc, 20091000, 92, 251);
                            break;
                        case 4:
                            Warp(pc, 20091000, 186, 225);
                            break;
                        case 5:
                            Warp(pc, 20092000, 122, 251);
                            break;
                        case 6:
                            Warp(pc, 20092000, 168, 32);
                            break;
                        case 9:
                            帕斯特島(pc);
                            break;
                    }
                    break;
                case 5:
                    switch (Select(pc, "不死之城 1", "", "不死之城入口", "到下一頁", "A棟（右側兵士地區）", "B棟（？地區）", "C棟（官廳地區）", "D棟（王座）", "E棟（右側兵士地區）", "F棟（宿舍地區）", "回去"))
                    {
                        case 1:
                            Warp(pc, 10069000, 143, 209);
                            break;
                        case 2:
                            switch (Select(pc, "不死之城 2", "", "G棟（大宴會場區）", "H棟（聖堂地區）", "I棟（倉庫地區）", "J棟（玄関門廊地區）", "預備", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    switch (Select(pc, "G棟（門廊地區）", "", "G棟", "G棟", "G棟", "預備", "預備", "預備", "預備", "預備", "回去"))
                                    {
                                        case 1:
                                            Warp(pc, 20120000, 7, 26);
                                            break;
                                        case 2:
                                            Warp(pc, 20121000, 28, 49);
                                            break;
                                        case 3:
                                            Warp(pc, 20122000, 7, 36);
                                            break;
                                        case 9:
                                            帕斯特島(pc);
                                            break;
                                    }
                                    break;
                                case 2:
                                    switch (Select(pc, "H棟 （聖堂地區）", "", "H棟", "H棟", "H棟聖子之間", "H棟聖堂", "預備", "預備", "預備", "預備", "回去"))
                                    {
                                        case 1:

                                            break;
                                        case 2:
                                            Warp(pc, 20123000, 1, 19);
                                            break;
                                        case 3:
                                            Warp(pc, 20124000, 23, 1);
                                            break;
                                        case 4:
                                            Warp(pc, 20125000, 23, 47);
                                            break;
                                        case 9:
                                            帕斯特島(pc);
                                            break;
                                    }
                                    break;
                                case 3:
                                    switch (Select(pc, "I棟 （倉庫地區）", "", "I棟", "I棟", "I棟", "預備", "預備", "預備", "預備", "預備", "回去"))
                                    {
                                        case 1:
                                            Warp(pc, 20126000, 22, 1);
                                            break;
                                        case 2:
                                            Warp(pc, 20127000, 11, 1);
                                            break;
                                        case 3:
                                            Warp(pc, 20128000, 1, 22);
                                            break;
                                        case 9:
                                            帕斯特島(pc);
                                            break;
                                    }
                                    break;
                                case 4:
                                    switch (Select(pc, "J棟 （玄關廳地區）", "", "J棟", "J棟", "預備", "預備", "預備", "預備", "預備", "預備", "回去"))
                                    {
                                        case 1:
                                            Warp(pc, 20129000, 7, 43);
                                            break;
                                        case 2:
                                            Warp(pc, 20092000, 108, 77);
                                            break;
                                        case 9:
                                            帕斯特島(pc);
                                            break;
                                    }
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 3:
                            switch (Select(pc, "A棟（右側兵士地區）", "", "A棟", "A棟", "A棟", "預備", "預備", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 20100000, 1, 7);
                                    break;
                                case 2:
                                    Warp(pc, 20101000, 1, 1);
                                    break;
                                case 3:
                                    Warp(pc, 20102000, 1, 22);
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 4:
                            switch (Select(pc, "B棟（？地區）", "", "B棟1樓", "B棟2樓", "預備", "預備", "預備", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 20103000, 1, 11);
                                    break;
                                case 2:
                                    Warp(pc, 20104000, 1, 1);
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 5:
                            switch (Select(pc, "C棟（官廳地區）", "", "C棟", "C棟", "C棟", "預備", "預備", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 20105000, 2, 32);
                                    break;
                                case 2:
                                    Warp(pc, 20106000, 15, 16);
                                    break;
                                case 3:
                                    Warp(pc, 20107000, 7, 1);
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 6:
                            switch (Select(pc, "D棟（王座地區）", "", "D棟1樓休息室1", "D棟2樓休息室2", "D棟3樓休息室3", "D棟4樓王座之間", "D棟5樓專用室", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 20108000, 7, 21);
                                    break;
                                case 2:
                                    Warp(pc, 20109000, 19, 21);
                                    break;
                                case 3:
                                    Warp(pc, 20110000, 19, 1);
                                    break;
                                case 4:
                                    Warp(pc, 20111000, 31, 21);
                                    break;
                                case 5:
                                    Warp(pc, 20112000, 1, 14);
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 7:
                            switch (Select(pc, "E棟（左側兵士地區）", "", "E棟", "E棟", "E棟", "預備", "預備", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 20113000, 23, 6);
                                    break;
                                case 2:
                                    Warp(pc, 20114000, 1, 1);
                                    break;
                                case 3:
                                    Warp(pc, 20115000, 23, 1);
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 8:
                            switch (Select(pc, "F棟（宿舍地區）", "", "F棟", "F棟", "F棟", "F棟", "預備", "預備", "預備", "預備", "回去"))
                            {
                                case 1:
                                    Warp(pc, 20116000, 18, 30);
                                    break;
                                case 2:
                                    Warp(pc, 20117000, 1, 30);
                                    break;
                                case 3:
                                    Warp(pc, 20118000, 14, 28);
                                    break;
                                case 4:
                                    Warp(pc, 20119000, 14, 1);
                                    break;
                                case 9:
                                    帕斯特島(pc);
                                    break;
                            }
                            break;
                        case 9:
                            帕斯特島(pc);
                            break;
                    }
                    break;
                case 9:
                    根目录(pc);
                    break;
            }
        }

        void 摩根(ActorPC pc)
        {
            switch (Select(pc, "摩根", "", "摩根市", "摩根市詳細→", "光之塔(C塔頂)", "光之塔(地樓", "光之塔(A塔頂)", "光之塔詳細→", "預備", "預備", "回去"))
            {
                case 1:
                    Warp(pc, 10060000, 228, 171);
                    break;
                case 2:
                    摩根市(pc);
                    break;
                case 3:
                    Warp(pc, 20146000, 199, 142);
                    break;
                case 4:
                    Warp(pc, 30099002, 7, 15);
                    break;
                case 5:
                    Warp(pc, 20163000, 142, 98);
                    break;
                case 6:
                    光之塔(pc);
                    break;
                case 7:
                    根目录(pc);
                    break;
                case 8:
                    根目录(pc);
                    break;
                case 9:
                    根目录(pc);
                    break;
            }
        }

        void 摩根市(ActorPC pc)
        {
            switch (Select(pc, "摩根市", "", "東邊機場(奧克魯尼亞方向)", "西邊機場(光之塔方向)", "房間→", "商店→", "預備", "預備", "預備", "預備", "回去"))
            {
                case 1:
                    Warp(pc, 10060000, 228, 171);
                    break;
                case 2:
                    Warp(pc, 10060000, 49, 129);
                    break;
                case 3:
                    switch (Select(pc, "摩根市(房間)", "", "摩根政府大樓", "摩根傭兵軍", "轉職行會分店", "商人行會分店", "荒廢礦坑監督室", "礦工的房間", "冶鍊所1", "冶鍊所3", "回去"))
                    {
                        case 1:
                            Warp(pc, 10060000, 139, 68);
                            break;
                        case 2:
                            Warp(pc, 10060000, 157, 91);
                            break;
                        case 3:
                            Warp(pc, 10060000, 155, 68);
                            break;
                        case 4:
                            Warp(pc, 10060000, 81, 152);
                            break;
                        case 5:
                            Warp(pc, 10060000, 105, 175);
                            break;
                        case 6:
                            Warp(pc, 10060000, 97, 101);
                            break;
                        case 7:
                            Warp(pc, 10060000, 109, 97);
                            break;
                        case 8:
                            Warp(pc, 10060000, 119, 186);
                            break;
                        case 9:
                            摩根市(pc);
                            break;
                    }
                    break;
                case 4:
                    switch (Select(pc, "摩根市(商店)", "", "露天咖啡店", "高級咖啡店", "摩根古董商店", "摩根裁縫店", "摩根寶石商", "摩根武器商店", "黑市武器商", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10060000, 149, 163);
                            break;
                        case 2:
                            Warp(pc, 10060000, 127, 78);
                            break;
                        case 3:
                            Warp(pc, 10060000, 170, 91);
                            break;
                        case 4:
                            Warp(pc, 10060000, 151, 130);
                            break;
                        case 5:
                            Warp(pc, 10060000, 167, 130);
                            break;
                        case 6:
                            Warp(pc, 10060000, 139, 149);
                            break;
                        case 7:
                            Warp(pc, 10060000, 145, 124);
                            break;
                        case 8:
                            根目录(pc);
                            break;
                        case 9:
                            摩根市(pc);
                            break;
                    }
                    break;
                case 5:
                    根目录(pc);
                    break;
                case 6:
                    根目录(pc);
                    break;
                case 7:
                    根目录(pc);
                    break;
                case 8:
                    根目录(pc);
                    break;
                case 9:
                    摩根(pc);
                    break;
            }
        }

        void 光之塔(ActorPC pc)
        {
            switch (Select(pc, "光之塔", "", "主層→", "Ａ棟→", "Ｂ棟→", "Ｃ棟（頂樓）", "外部階梯", "高架橋下", "4F", "預備", "回去"))
            {
                case 1:
                    switch (Select(pc, "主層", "", "1Ｆ", "2Ｆ", "3Ｆ", "4Ｆ", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20140000, 37, 52);
                            break;
                        case 2:
                            Warp(pc, 20141000, 69, 10);
                            break;
                        case 3:
                            Warp(pc, 20142000, 65, 79);
                            break;
                        case 4:
                            Warp(pc, 20143000, 62, 11);
                            break;
                        case 5:
                            根目录(pc);
                            break;
                        case 6:
                            根目录(pc);
                            break;
                        case 7:
                            根目录(pc);
                            break;
                        case 8:
                            根目录(pc);
                            break;
                        case 9:
                            光之塔(pc);
                            break;
                    }
                    break;
                case 2:
                    光塔A棟(pc);
                    break;
                case 3:
                    switch (Select(pc, "B棟", "", "9Ｆ（頂樓）", "8Ｆ（Ａ棟連接道路）", "7Ｆ", "6Ｆ", "5Ｆ", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20146000, 129, 156);
                            break;
                        case 2:
                            Warp(pc, 20152000, 44, 8);
                            break;
                        case 3:
                            Warp(pc, 20150000, 44, 42);
                            break;
                        case 4:
                            Warp(pc, 20148000, 15, 8);
                            break;
                        case 5:
                            Warp(pc, 20145000, 15, 42);
                            break;
                        case 6:
                            根目录(pc);
                            break;
                        case 7:
                            根目录(pc);
                            break;
                        case 8:
                            根目录(pc);
                            break;
                        case 9:
                            光之塔(pc);
                            break;
                    }
                    break;
                case 4:
                    Warp(pc, 20146000, 181, 130);
                    break;
                case 5:
                    Warp(pc, 20163000, 148, 106);
                    break;
                case 6:
                    Warp(pc, 20146000, 148, 106);
                    break;
                case 7:
                    Warp(pc, 20146000, 173, 92);
                    break;
                case 8:
                    根目录(pc);
                    break;
                case 9:
                    摩根市(pc);
                    break;
            }
        }

        void 光塔A棟(ActorPC pc)
        {
            switch (Select(pc, "A棟", "", "高層（12~19F）", "低層（5~11F）", "預備", "預備", "預備", "預備", "預備", "預備", "回去"))
            {
                case 1:
                    switch (Select(pc, "A棟高層（12~19F）", "", "19Ｆ（頂樓）", "18Ｆ", "17Ｆ", "16Ｆ", "15Ｆ（外部階梯連接道路）", "14Ｆ（死胡同）", "13Ｆ", "12Ｆ（外部階梯連接道路）", "回去"))
                    {
                        case 1:
                            Warp(pc, 20163000, 142, 98);
                            break;
                        case 2:
                            Warp(pc, 20162000, 23, 40);
                            break;
                        case 3:
                            Warp(pc, 20161000, 14, 6);
                            break;
                        case 4:
                            Warp(pc, 20154002, 15, 42);
                            break;
                        case 5:
                            Warp(pc, 20159000, 15, 8);
                            break;
                        case 6:
                            Warp(pc, 20154001, 15, 6);
                            break;
                        case 7:
                            Warp(pc, 20157000, 15, 44);
                            break;
                        case 8:
                            Warp(pc, 20156000, 15, 6);
                            break;
                        case 9:
                            光塔A棟(pc);
                            break;
                    }
                    break;
                case 2:
                    switch (Select(pc, "A棟低層（5~11F）", "", "11Ｆ", "10Ｆ", "09Ｆ", "08Ｆ（Ｂ棟連接道路）", "07Ｆ", "06Ｆ", "05Ｆ", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20155000, 15, 8);
                            break;
                        case 2:
                            Warp(pc, 20154000, 15, 42);
                            break;
                        case 3:
                            Warp(pc, 20153000, 15, 8);
                            break;
                        case 4:
                            Warp(pc, 20151000, 15, 42);
                            break;
                        case 5:
                            Warp(pc, 20149000, 15, 8);
                            break;
                        case 6:
                            Warp(pc, 20147000, 15, 6);
                            break;
                        case 7:
                            Warp(pc, 20144000, 15, 8);
                            break;
                        case 8:
                            根目录(pc);
                            break;
                        case 9:
                            光塔A棟(pc);
                            break;
                    }
                    break;
                case 3:
                    根目录(pc);
                    break;
                case 4:
                    根目录(pc);
                    break;
                case 5:
                    根目录(pc);
                    break;
                case 6:
                    根目录(pc);
                    break;
                case 7:
                    根目录(pc);
                    break;
                case 8:
                    根目录(pc);
                    break;
                case 9:
                    光之塔(pc);
                    break;
            }
        }

        void 特殊(ActorPC pc)
        {
            switch (Select(pc, "想去哪呢？", "", "營運者調查室(GM房間)", "接待室(GM房間)", "個人指導地圖", "納特咖啡店地圖", "告示板", "塔妮亞傳送室", "道米尼傳送室", "泰迪島", "得菩提島", "回去"))
            {
                case 1:
                    switch (Select(pc, "想去哪呢？", "", "營運團調查室1(GM房間)", "營運團調查室2(GM房間)", "營運團調查室3(GM房間)", "營運團調查室4(GM房間)", "營運團調查室5(GM房間)", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 20080001, 24, 24);
                            break;
                        case 2:
                            Warp(pc, 20080002, 24, 24);
                            break;
                        case 3:
                            Warp(pc, 20080003, 24, 24);
                            break;
                        case 4:
                            Warp(pc, 20080004, 24, 24);
                            break;
                        case 5:
                            Warp(pc, 20080005, 24, 24);
                            break;
                        case 9:
                            特殊(pc);
                            break;
                    }
                    break;
                case 2:
                    Warp(pc, 30100001, 4, 14);
                    break;
                case 3:
                    switch (Select(pc, "想去哪呢？", "", "講解室", "講解室", "講解室", "講解室", "講解室", "講解室", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 10014002, 78, 4);
                            break;
                        case 2:
                            Warp(pc, 10014012, 126, 197);
                            break;
                        case 3:
                            Warp(pc, 10014022, 126, 197);
                            break;
                        case 4:
                            Warp(pc, 10014032, 126, 197);
                            break;
                        case 5:
                            Warp(pc, 10014042, 126, 197);
                            break;
                        case 6:
                            Warp(pc, 10014052, 126, 197);
                            break;
                        case 9:
                            特殊(pc);
                            break;
                    }
                    break;
                case 4:
                    Warp(pc, 20081000, 11, 21);
                    break;
                case 5:
                    switch (Select(pc, "想去哪呢？", "", "公共告示板", "公共告示板（1回EVT1000路）", "軍團", "個體", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 30060001, 4, 7);
                            break;
                        case 2:
                            Warp(pc, 10065000, 65, 146);
                            break;
                        case 3:
                            Warp(pc, 30011001, 3, 5);
                            break;
                        case 4:
                            Warp(pc, 10065000, 65, 132);
                            break;
                        case 9:
                            根目录(pc);
                            break;
                    }
                    break;
                case 6:
                    switch (Select(pc, "想去哪呢？", "", "塔妮亞傳送室1", "塔妮亞傳送室2", "塔妮亞傳送室3", "預備", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 30140000, 9, 14);
                            break;
                        case 2:
                            Warp(pc, 30140001, 9, 14);
                            break;
                        case 3:
                            Warp(pc, 30140002, 9, 14);
                            break;
                        case 9:
                            特殊(pc);
                            break;
                    }
                    break;
                case 7:
                    switch (Select(pc, "想去哪呢？", "", "道米尼傳送室1", "道米尼傳送室2", "道米尼傳送室3", "預備", "預備", "預備", "預備", "預備", "回去"))
                    {
                        case 1:
                            Warp(pc, 30141000, 9, 14);
                            break;
                        case 2:
                            Warp(pc, 30141001, 9, 14);
                            break;
                        case 3:
                            Warp(pc, 30141002, 9, 14);
                            break;
                        case 9:
                            根目录(pc);
                            break;
                    }
                    break;
                case 8:
                    Warp(pc, 10071000, 244, 82);
                    break;
                case 9:
                    Warp(pc, 10071001, 171, 46);
                    break;
                case 10:
                    根目录(pc);
                    break;
            }
        }

        void 合成(ActorPC pc)
        {
            switch (Select(pc, "歡迎!想要什麽呢？", "", "合成(之外)", "合成(裝備品系列)", "合成(維修工具)", "放棄"))
            {
                case 1:
                    switch (Select(pc, "合成(之外)", "", "回去", "2009精製道具", "2051冶鍊高鍍金屬", "2020木材加工", "2039機械工學", "2040烹調", "2022製作藥品", "2038製作活動木偶", "812製作捲軸", "預備"))
                    {
                        case 1:
                            合成(pc);
                            break;
                        case 2:
                            Synthese(pc, 2009, 3);
                            break;
                        case 3:
                            Synthese(pc, 2051, 3);
                            break;
                        case 4:
                            Synthese(pc, 2020, 3);
                            break;
                        case 5:
                            Synthese(pc, 2039, 1);
                            break;
                        case 6:
                            Synthese(pc, 2040, 5);
                            break;
                        case 7:
                            Synthese(pc, 2022, 5);
                            break;
                        case 8:
                            Synthese(pc, 2038, 1);
                            break;
                        case 9:
                            Synthese(pc, 812, 1);
                            break;
                        case 10:
                            break;
                    }
                    break;
                case 2:
                    switch (Select(pc, "合成(裝備品系列)", "", "回去", "2035製作投擲武器", "2018製作裝飾品", "2010製作武器", "2017製作防具", "2034製作弓", "2054縫製", "2021製作魔杖", "預備", "預備"))
                    {
                        case 1:
                            合成(pc);
                            break;
                        case 2:
                            Synthese(pc, 2035, 5);
                            break;
                        case 3:
                            Synthese(pc, 2018, 5);
                            break;
                        case 4:
                            Synthese(pc, 2010, 10);
                            break;
                        case 5:
                            Synthese(pc, 2017, 5);
                            break;
                        case 6:
                            Synthese(pc, 2034, 5);
                            break;
                        case 7:
                            Synthese(pc, 2054, 5);
                            break;
                        case 8:
                            Synthese(pc, 2021, 5);
                            break;
                        case 9:
                            break;
                        case 10:
                            break;
                    }
                    break;
                case 3:
                    switch (Select(pc, "合成(維修工具)", "", "回去", "『製作金屬製品處理工具箱』", "『製作裝飾品維修工具箱』", "『製作服裝修補工具箱』", "『製作木製品維修工具箱』", "『製作皮革製品修補工具箱』", "『製作機械維修工具箱』", "預備", "預備", "預備"))
                    {
                        case 1:
                            合成(pc);
                            break;
                        case 2:
                            Synthese(pc, 2083, 2);
                            break;
                        case 3:
                            Synthese(pc, 2084, 2);
                            break;
                        case 4:
                            Synthese(pc, 2085, 2);
                            break;
                        case 5:
                            Synthese(pc, 2086, 2);
                            break;
                        case 6:
                            Synthese(pc, 2087, 2);
                            break;
                        case 7:
                            Synthese(pc, 2088, 2);
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                        case 10:
                            break;
                    }
                    break;
            }
        }
    }
}
