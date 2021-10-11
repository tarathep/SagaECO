using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Map;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public class S1001 : Event
    {
        public S1001()
        {
            this.EventID = 1001;
        }

        public override void OnEvent(ActorPC pc)
        {
                       if (CheckMapFlag(pc.SaveMap, MapFlags.Dominion) != CheckMapFlag(pc.MapID, MapFlags.Dominion))
            {
                Say(pc, 131, "抱歉,您當前的世界跟儲存的世界不同,無法使用次元傳送服務!", " ");
                return;
            }
            if (CheckMapFlag(pc.MapID, MapFlags.Dominion))
            {
                Say(pc, 131, "現在處於不可使用次元印刻的惡界請利用通天之塔回到人界再使用次元印刻吧!", " ");
            }
            else
            {
                switch (Select(pc, "次元", "", "上城東邊吊橋", "各城市傳送", "各升級地點傳送", "NPC", "PVP鬥技場", "衝天塔", "瑪衣瑪衣島", "不用了"))
                {
                    case 1:
                        if (CountItem(pc, 21050001) >= 1)
                        {
                            TakeItem(pc, 21050001, 1);
                            Warp(pc, 10023100, 247, 126);
                        }
                        break;
                    case 2:
                        switch (Select(pc, "城市傳送", "", "帕斯特市", "唐卡島", "摩根市", "諾頓海濱長廊", "上城東邊吊橋", "上城南邊吊橋", "上城西邊吊橋", "上城北邊吊橋", "東方海角", "帕斯特街道(飛空庭匠人)", "諾頓海濱長廊", "軍艦島", "謎之團要塞", "阿伊恩市", "取消"))
                        {
                            case 1:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10057000, 9, 123);
                                }
                                break;
                            case 2:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10062000, 71, 201);
                                }
                                break;
                            case 3:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10060000, 47, 138);
                                }
                                break;
                            case 4:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10065000, 52, 127);
                                }
                                break;
                            case 5:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10023100, 239, 127);
                                }
                                break;
                            case 6:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10023300, 127, 245);
                                }
                                break;
                            case 7:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10023200, 10, 127);
                                }
                                break;
                            case 8:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10023400, 127, 9);
                                }
                                break;
                            case 9:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10018100, 202, 68);
                                }
                                break;
                            case 10:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10017000, 148, 210);
                                }
                                break;
                            case 11:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10065000, 52, 195);
                                }
                                break;
                            case 12:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10035000, 245, 2);
                                }
                                break;
                            case 13:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10054000, 186, 138);
                                }
                                break;
                            case 14:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10063100, 123, 147);
                                }
                                break;
                            case 15:
                                break;
                        }
                        break;
                    case 3:
                        switch (Select(pc, "升級專區", "", "奧克魯尼亞叢林", "瑞路斯山道", "北方海角", "北方中央山脈", "永恆之北部極地", "奧克魯尼亞東海岸", "不死皇城", "鬼之寢岩", "南方海角", "奧克魯尼亞北部平原", "奧克魯尼亞東部平原", "奧克魯尼亞西部平原", "奧克魯尼亞南部平原", "果樹森林", "殺人蜂山路", "不死島嶼", "南部地牢3F", "南部地牢2F", "南部地牢1F", "馬克特碼頭", "東部地牢", "冰結の坑道Ｂ１Ｆ", "北部地牢", "東方地牢", "光の塔 １Ｆ", "大陸之洞穴Ｂ１Ｆ", "取消"))
                        {

                            case 1:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10015000, 152, 143);
                                }
                                break;
                            case 2:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10003000, 81, 249);
                                }
                                break;
                            case 3:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10001000, 84, 35);
                                }
                                break;
                            case 4:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10050000, 54, 253);
                                }
                                break;
                            case 5:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10051000, 92, 127);
                                }
                                break;
                            case 6:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10034000, 4, 222);
                                }
                                break;
                            case 7:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10069000, 145, 208);
                                }
                                break;
                            case 8:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10061000, 188, 251);
                                }
                                break;
                            case 9:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10046000, 197, 251);
                                }
                                break;
                            case 10:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10014000, 128, 252);
                                }
                                break;
                            case 11:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10025000, 4, 127);
                                }
                                break;
                            case 12:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10022000, 252, 128);
                                }
                                break;
                            case 13:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10031000, 157, 252);
                                }
                                break;
                            case 14:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10030000, 217, 250);
                                }
                                break;
                            case 15:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10020000, 252, 158);
                                }
                                break;
                            case 16:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 10019000, 253, 169);
                                }
                                break;
                            case 17:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20022000, 128, 129);
                                }
                                break;
                            case 18:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20021000, 124, 144);
                                }
                                break;
                            case 19:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20020000, 127, 129);
                                }
                                break;
                            case 20:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20023000, 150, 44);
                                }
                                break;
                            case 21:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20090000, 95, 2);
                                }
                                break;
                            case 22:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20010000, 63, 12);
                                }
                                break;
                            case 23:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20014000, 241, 12);
                                }
                                break;
                            case 24:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20090000, 112, 253);
                                }
                                break;
                            case 25:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20140000, 37, 52);
                                }
                                break;
                            case 26:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20000000, 63, 23);
                                }
                                break;
                            case 27:
                                break;
                        }
                        break;
                    case 4:
                        switch (Select(pc, "NPC", "", "情人教堂", "聖女的家", "遺跡廣場", "賢者之家", "民家(瑪瑪)", "取消"))
                        {
                            case 1:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 30122000, 11, 20);
                                }
                                break;
                            case 2:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 30090001, 4, 6);
                                }
                                break;
                            case 3:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 20080012, 25, 25);
                                }
                                break;
                            case 4:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 30022000, 3, 3);
                                }
                                break;
                            case 5:
                                if (CountItem(pc, 21050001) >= 1)
                                {
                                    TakeItem(pc, 21050001, 1);
                                    Warp(pc, 30020001, 3, 3);
                                }
                                break;
                        }
                        break;
                    case 5:
                        if (CountItem(pc, 21050001) >= 1)
                        {
                            TakeItem(pc, 21050001, 1);
                            Warp(pc, 20080000, 25, 25);
                        }
                        break;
                    case 6:
                        if (CountItem(pc, 21050001) >= 1)
                        {
                            TakeItem(pc, 21050001, 1);
                            Warp(pc, 10058000, 127, 160);
                        }
                        break;
                    case 7:
                        if (CountItem(pc, 21050001) >= 1)
                        {
                        TakeItem(pc, 21050001, 1);
                        Warp(pc, 10059000, 68, 147);
                        }
                        break;
                    case 8:
                        break;
                }
            }
                    }
            }
        }

