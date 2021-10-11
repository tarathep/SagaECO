using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;
using SagaDB.Item;
//所在地圖:咖啡館(30010000) NPC基本信息:咖啡館老闆(11000002) X:1 Y:0
namespace SagaScript.M30010000
{
    public class S11000002 : Event
    {
        public S11000002()
        {
            this.EventID = 11000002;

            //任務服務台相關設定
            this.leastQuestPoint = 1;

            this.alreadyHasQuest = "任務進行的還順利嗎?$R;";

            this.gotNormalQuest = "那就拜託了。$R;" +
                                  "$R等任務完成以後，再來找我吧。$R;";

            this.gotTransportQuest = "是阿，道具太重了吧?$R;" +
                                     "$R所以不能一次傳送的話，$R;" +
                                     "分成幾次給我也可以的。$R;";

            this.questCompleted = "真是辛苦了。$R;" +
                                  "$R恭喜你，任務完成了。$R;" +
                                  "$P來! 收下報酬吧!$R;";

            this.transport = "哦哦…全部都收集好了嗎?$R;";

            this.questCanceled = "嗯…如果是你，我相信你能做到的，$R;" +
                                 "很期待呢……$R;";

            this.questFailed = "……$R;" +
                               "$P失敗了?$R;" +
                               "$R真是鬧了大事，$R;" +
                               "不知道該說什麼好啊?$R;" +
                               "$P這次實在沒辦法了，$R;" +
                               "下次一定要成功啊!$R;" +
                               "$R知道了吧?$R;";

            this.notEnoughQuestPoint = "嗯…$R;" +
                                       "$R現在沒有要特別拜託的事情啊，$R;" +
                                       "再去冒險怎麼樣?$R;";

            this.questTooEasy = "唔…但是對你來說，$R;" +
                                "說不定是太簡單的任務。$R;" +
                                "$R那樣也沒關係嘛?$R;";

            this.questTooHard = "這對你來說有點困難啊?$R;" +
                                "$R這樣也沒關係嘛?$R;"; 
        }
        
        public override void OnEvent(ActorPC pc)
        {
            BitMask<Valentine_Day_00> Valentine_Day_00_mask = pc.CMask["Valentine_Day_00"];                                                                             //活動：情人節
            BitMask<White_Valentine_Day_00> White_Valentine_Day_00_mask = pc.CMask["White_Valentine_Day_00"];                                                           //活動：白色情人節
            BitMask<Halloween_00> Halloween_00_mask = pc.CMask["Halloween_00"];                                                                                         //活動：萬聖節
            BitMask<Hunting_Mushroom_00> Hunting_Mushroom_00_mask = pc.CMask["Hunting_Mushroom_00"];                                                                    //活動：狩獵蘑菇
            BitMask<Emil_Letter> Emil_Letter_mask = pc.CMask["Emil_Letter"];                                                                                            //任務：埃米爾介紹書
            BitMask<Last_Words> Last_Words_mask = pc.CMask["Last_Words"];                                                                                               //任務：古魯杜的遺言

            Valentine_Day_00_mask.SetValue(Valentine_Day_00.情人節活動期間, false);                                                                                     //情人節活動期間                開/關
            White_Valentine_Day_00_mask.SetValue(White_Valentine_Day_00.白色情人節活動期間, false);                                                                     //白色情人節活動期間            開/關
            Halloween_00_mask.SetValue(Halloween_00.萬聖節活動期間, false);                                                                                             //萬聖節活動期間                開/關
            Hunting_Mushroom_00_mask.SetValue(Hunting_Mushroom_00.狩獵蘑菇活動期間, false);                                                                             //狩獵蘑菇活動期間              開/關

            if (Valentine_Day_00_mask.Test(Valentine_Day_00.情人節活動期間) &&
                pc.Fame > 10)
            {
                情人節(pc);
            }

            if (White_Valentine_Day_00_mask.Test(White_Valentine_Day_00.白色情人節活動期間) &&
                pc.Fame > 10)
            {
                白色情人節(pc);
            }

            if (Halloween_00_mask.Test(Halloween_00.萬聖節活動期間))
            {
                萬聖節(pc);
            }

            if (Hunting_Mushroom_00_mask.Test(Hunting_Mushroom_00.狩獵蘑菇活動期間) &&
                pc.Fame > 10)
            {
                狩獵蘑菇(pc);
            }

            if (!Emil_Letter_mask.Test(Emil_Letter.埃米爾介紹書任務完成) &&
                CountItem(pc, 10043081) > 0)
            {
                埃米爾介紹書(pc);
                return;
            }

            if (!Last_Words_mask.Test(Last_Words.已經與古魯杜的女兒們進行對話) &&
                pc.Level >= 35 &&
                pc.Fame >= 20)
            {
                古魯杜的遺言(pc);
                return;
            }

            switch (Select(pc, "要不要喝一杯?", "", "買東西", "賣東西", "任務服務台", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    break;
            }            
        }

        void 情人節(ActorPC pc)
        {
            if (pc.Fame > 10)
            {
                咖啡館的情人節禮物(pc);
                return;
            }
        }

        void 咖啡館的情人節禮物(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            switch (Select(pc, "要不要喝一杯?", "", "買東西", "賣東西", "任務服務台", "交換『咖啡館的禮券』", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    if (!Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                        !Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                    {
                        交換情人節禮物(pc);
                        return;
                    }
                    else
                    {
                        PlaySound(pc, 2041, false, 100, 50);

                        Say(pc, 11000002, 131, "你已經交換過了嗎?$R;" +
                                               "$R如果有剩下的票，$R;" +
                                               "可以送給朋友喔。$R;", "咖啡館老闆");                    
                    }
                    break;

                case 5:
                    break;
            }
        }

        void 交換情人節禮物(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            if(CountItem(pc, 10048002) >= 15)
            {
                if (!Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                    !Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                {
                    switch (Select(pc, "想要交換哪一樣禮物呢?", "", "交換『心花牆紙』", "交換『心花地板』"))
                    {
                        case 1:
                            交換心花牆紙(pc);
                            return;
                            
                        case 2:
                            交換心花地板(pc);
                            return;
                    }
                }
               
                if (!Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                    Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                {
                    交換心花牆紙(pc);
                    return;
                }

                if (Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花牆紙) &&
                    !Valentine_Day_01_mask.Test(Valentine_Day_01.交換心花地板))
                {
                    交換心花地板(pc);
                    return;
                }
            }
            else
            {
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 0, 65535, "『咖啡館的禮券』數量不足。$R;", " ");

                Say(pc, 11000002, 131, "活動期間在「咖啡館」承接『情人節系列』任務，$R;" +
                                       "任務成功的話，$R;" +
                                       "就可以得到『咖啡館的禮券』。$R;" +
                                       "$R收集15張『咖啡館的禮券』的話，$R;" +
                                       "就可以交換漂亮的禮物喔。$R;", "咖啡館老闆");            
            }
        }

        void 交換心花牆紙(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R這不是『咖啡館的禮券』嗎?$R;" +
                                   "$P要用15張『咖啡館的禮券』$R;" +
                                   "來交換『心花牆紙』嗎?$R;" +
                                   "$R雖然之前就講過了，$R;" +
                                   "但還是要提醒您一下，$R;" +
                                   "因為禮物有點重，$R;" +
                                   "所以要注意一下啊。$R;", "咖啡館老闆");

            switch(Select(pc, "怎麼辦呢?", "", "不交換", "交換"))
            {
                case 1:
                    break;

                case 2:
                    Valentine_Day_01_mask.SetValue(Valentine_Day_01.交換心花牆紙, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 30040012, 1);
                    Say(pc, 0, 65535, "得到『心花牆紙』!$R;", " ");

                    Say(pc, 11000002, 131, "平時總是在麻煩你，$R;" +
                                           "真的非常感謝你呀。$R;" +
                                           "$R以後也會繼續期待你的表現!$R;", "咖啡館老闆");
                    break;
            }
        }

        void 交換心花地板(ActorPC pc)
        {
            BitMask<Valentine_Day_01> Valentine_Day_01_mask = pc.CMask["Valentine_Day_01"];                                                                             //活動：咖啡館的情人節禮物(情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R這不是『咖啡館的禮券』嗎?$R;" +
                                   "$P要用15張『咖啡館的禮券』$R;" +
                                   "來交換『心花地板』嗎?$R;" +
                                   "$R雖然之前就講過了，$R;" +
                                   "但還是要提醒您一下，$R;" +
                                   "因為禮物有點重，$R;" +
                                   "所以要注意一下啊。$R;", "咖啡館老闆");

            switch(Select(pc, "怎麼辦呢?", "", "不交換", "交換"))
            {
                case 1:
                    break;

                case 2:
                    Valentine_Day_01_mask.SetValue(Valentine_Day_01.交換心花牆紙, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 30050113, 1);
                    Say(pc, 0, 65535, "得到『心花牆紙』!$R;", " ");

                    Say(pc, 11000002, 131, "平時總是在麻煩你，$R;" +
                                           "真的非常感謝你呀。$R;" +
                                           "$R以後也會繼續期待你的表現!$R;", "咖啡館老闆");
                    break;
            }
        }

        void 白色情人節(ActorPC pc)
        {
            if (pc.Fame > 10)
            {
                咖啡館的白色情人節禮物(pc);
                return;
            }
        }

        void 咖啡館的白色情人節禮物(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            switch (Select(pc, "要不要喝一杯?", "", "買東西", "賣東西", "任務服務台", "交換『咖啡館的禮券』", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    if (!White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                        !White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                    {
                        交換白色情人節禮物(pc);
                        return;
                    }
                    else
                    {
                        PlaySound(pc, 2041, false, 100, 50);

                        Say(pc, 11000002, 131, "你已經交換過了嗎?$R;" +
                                               "$R如果有剩下的票，$R;" +
                                               "可以送給朋友喔。$R;", "咖啡館老闆");
                    }
                    break;

                case 5:
                    break;
            }
        }

        void 交換白色情人節禮物(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            if (CountItem(pc, 10048002) >= 15)
            {
                if (!White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                    !White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                {
                    switch (Select(pc, "想要交換哪一樣禮物呢?", "", "交換『小地毯 (白色情人節)』", "交換『大地毯 (白色情人節)』"))
                    {
                        case 1:
                            交換小地毯(pc);
                            return;

                        case 2:
                            交換大地毯(pc);
                            return;
                    }
                }

                if (!White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                    White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                {
                    交換小地毯(pc);
                    return;
                }

                if (White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換小地毯) &&
                    !White_Valentine_Day_01_mask.Test(White_Valentine_Day_01.交換大地毯))
                {
                    交換大地毯(pc);
                    return;
                }
            }
            else
            {
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 0, 65535, "『咖啡館的禮券』數量不足。$R;", " ");

                Say(pc, 11000002, 131, "活動期間在「咖啡館」承接『白色情人節系列』任務，$R;" +
                                       "任務成功的話，$R;" +
                                       "就可以得到『咖啡館的禮券』。$R;" +
                                       "$R收集15張『咖啡館的禮券』的話，$R;" +
                                       "就可以交換漂亮的禮物喔。$R;", "咖啡館老闆");
            }
        }

        void 交換小地毯(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R這不是『咖啡館的禮券』嗎?$R;" +
                                   "$P要用15張『咖啡館的禮券』$R;" +
                                   "來交換『小地毯 (白色情人節)』嗎?$R;" +
                                   "$R雖然之前就講過了，$R;" +
                                   "但還是要提醒您一下，$R;" +
                                   "因為禮物有點重，$R;" +
                                   "所以要注意一下啊。$R;", "咖啡館老闆");

            switch (Select(pc, "怎麼辦呢?", "", "不交換", "交換"))
            {
                case 1:
                    break;

                case 2:
                    White_Valentine_Day_01_mask.SetValue(White_Valentine_Day_01.交換小地毯, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 31130027, 1);
                    Say(pc, 0, 65535, "得到『小地毯 (白色情人節)』!$R;", " ");

                    Say(pc, 11000002, 131, "平時總是在麻煩你，$R;" +
                                           "真的非常感謝你呀。$R;" +
                                           "$R以後也會繼續期待你的表現!$R;", "咖啡館老闆");
                    break;
            }
        }

        void 交換大地毯(ActorPC pc)
        {
            BitMask<White_Valentine_Day_01> White_Valentine_Day_01_mask = pc.CMask["White_Valentine_Day_01"];                                                           //活動：咖啡館的白色情人節禮物(白色情人節)

            Say(pc, 11000002, 131, "哦哦!$R;" +
                                   "$R這不是『咖啡館的禮券』嗎?$R;" +
                                   "$P要用15張『咖啡館的禮券』$R;" +
                                   "來交換『大地毯 (白色情人節)』嗎?$R;" +
                                   "$R雖然之前就講過了，$R;" +
                                   "但還是要提醒您一下，$R;" +
                                   "因為禮物有點重，$R;" +
                                   "所以要注意一下啊。$R;", "咖啡館老闆");

            switch (Select(pc, "怎麼辦呢?", "", "不交換", "交換"))
            {
                case 1:
                    break;

                case 2:
                    White_Valentine_Day_01_mask.SetValue(White_Valentine_Day_01.交換大地毯, true);

                    TakeItem(pc, 10048002, 15);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 31130109, 1);
                    Say(pc, 0, 65535, "得到『大地毯 (白色情人節)』!$R;", " ");

                    Say(pc, 11000002, 131, "平時總是在麻煩你，$R;" +
                                           "真的非常感謝你呀。$R;" +
                                           "$R以後也會繼續期待你的表現!$R;", "咖啡館老闆");
                    break;
            }
        }

        void 萬聖節(ActorPC pc)
        {
            BitMask<Halloween_03> Halloween_03_mask = pc.CMask["Halloween_03"];                                                                                         //活動：搗蛋糖(萬聖節)

            if (!Halloween_03_mask.Test(Halloween_03.已經向咖啡館老闆索取搗蛋糖))
            {
                搗蛋糖任務(pc);
                return;
            }
        }

        void 搗蛋糖任務(ActorPC pc)
        {
            BitMask<Halloween_03> Halloween_03_mask = pc.CMask["Halloween_03"];                                                                                         //活動：搗蛋糖(萬聖節)

            switch (Select(pc, "想要怎麼做呢?", "", "跟往常一樣打招呼", "不給糖，就搗蛋!!"))
            {
                case 1:
                    break;

                case 2:
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50025800 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024350 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024351 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024352 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024353 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024354 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024355 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024356 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024357 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50024358 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022500 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022600 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022700 ||
                            pc.Inventory.Equipments[EnumEquipSlot.HEAD].ItemID == 50022800)
                        {
                            Say(pc, 11000002, 131, "啊…是妖怪嗎?$R;" +
                                                   "$R喲!$R;" +
                                                   "不要做惡作劇，我給你糖果。$R;", "咖啡館老闆");

                            if (CheckInventory(pc, 10009200, 1))
                            {
                                Halloween_03_mask.SetValue(Halloween_03.已經向咖啡館老闆索取搗蛋糖, true);

                                GiveItem(pc, 10009200, 1);
                            }
                            else
                            {
                                PlaySound(pc, 2041, false, 100, 50);

                                Say(pc, 11000002, 131, "……$R;" +
                                                       "$P行李太多，沒辦法給您報酬啊?!$R;" +
                                                       "$R可以把不要的道具扔掉一些，$R;" +
                                                       "或者是減少點行李以後，再來吧。$R;", "咖啡館老闆");                        
                            }
                        }
                    }
                    else
                    {
                        Say(pc, 11000002, 131, "啊…這樣不行的啊!$R;" +
                                               "回去打扮以後，再過來吧。$R;", "咖啡館老闆");                    
                    }
                    break;
            }
        }

        void 狩獵蘑菇(ActorPC pc)
        {
            switch (Select(pc, "要不要喝一杯?", "", "買東西", "賣東西", "任務服務台", "學做『蘑菇料理』", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    break;

                case 2:
                    OpenShopSell(pc, 4);
                    break;

                case 3:
                    HandleQuest(pc, 6);
                    break;

                case 4:
                    學習蘑菇料理(pc);
                    break;

                case 5:
                    break;
            }
        }

        void 學習蘑菇料理(ActorPC pc)
        {
            Say(pc, 11000002, 131, "現在要教你『蘑菇料理』的做法，$R;" +
                                   "你想學哪一種?$R;", "咖啡館老闆");

            switch (Select(pc, "想要了解哪種烹調方法呢?", "", "蘑菇湯", "蘑菇包", "蘑菇咖喱", "蘑菇大雜燴", "沒興趣"))
            {
                case 1:
                    Say(pc, 11000002, 131, "烹調『蘑菇湯』，$R;" +
                                           "需要有『蘑菇』、『野菜汁』，$R;" +
                                           "這兩種材料。$R;", "咖啡館老闆");
                    break;

                case 2:
                    Say(pc, 11000002, 131, "烹調『蘑菇包』，$R;" +
                                           "需要有『蘑菇』、『麵包樹果實』、『鹽』，$R;" +
                                           "這三種材料。$R;", "咖啡館老闆");
                    break;

                case 3:
                    Say(pc, 11000002, 131, "烹調『蘑菇咖喱』，$R;" +
                                           "需要有『蘑菇』、『肉』、『礦泉水』$R;" +
                                           "以及『香辛料』，$R;" +
                                           "這四種材料。$R;", "咖啡館老闆");
                    break;

                case 4:
                    Say(pc, 11000002, 131, "烹調『蘑菇大雜燴』，$R;" +
                                           "需要有『蘑菇』、『高級肉』、『礦泉水』$R;" +
                                           "以及『奶油』，$R;" +
                                           "這四種材料。$R;", "咖啡館老闆");
                    break;

                case 5:
                    Say(pc, 11000002, 131, "那一樣…?$R;", "咖啡館老闆");
                    break;
            }
        }

        void 埃米爾介紹書(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = pc.CMask["Emil_Letter"];                                                                                            //任務：埃米爾介紹書

            int selection;

            Emil_Letter_mask.SetValue(Emil_Letter.埃米爾介紹書任務開始, true);

            Say(pc, 11000002, 131, "啊?!$R;" +
                                   "那個莫非是『埃米爾介紹書』?$R;" +
                                   "$R是嗎? 來的正好!$R;" +
                                   "$P您是初心者吧?$R;" +
                                   "$R是第一次來這裡，很辛苦吧?$R;" +
                                   "我請客! 您隨便吃吧!$R;", "咖啡館老闆");

            PlaySound(pc, 2040, false, 100, 50);
            Heal(pc);
            Say(pc, 0, 65535, "招待了親自做的蛋糕和紅茶！$R;", " ");

            Say(pc, 11000002, 131, "您沒有想過要挑戰看看任務?$R;", "咖啡館老闆");

            selection = Select(pc, "想要挑戰任務嗎?", "", "有什麼樣的任務?", "挑戰任務", "放棄");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        任務種類詳細解說(pc);
                        break;

                    case 2:
                        擊退皮露露(pc);
                        break;

                    case 3:
                        break;
                }

                selection = Select(pc, "想要挑戰任務嗎?", "", "有什麼樣的任務?", "挑戰任務", "放棄");
            }
        }

        void 任務種類詳細解說(ActorPC pc)
        {
            int selection;

            Say(pc, 11000002, 131, "任務的要求幾乎都很簡單喔!$R;" +
                                   "$R「咖啡館」除了賣糧食外，$R;" +
                                   "也會介紹一些工作給冒險者唷!$R;" +
                                   "$P久而久之，口碑越來越好了，$R;" +
                                   "所以在「阿高普路斯市」周圍，$R;" +
                                   "開了許多分店。$R;" +
                                   "$R出差的時後，一定要去看看啊。$R;" +
                                   "$P任務的內容主要是以$R;" +
                                   "「擊退魔物」、「收集/搬運道具」等。$R;" +
                                   "$R當然會根據任務的內容，$R;" +
                                   "給予您等值的報酬!$R;" +
                                   "$P工作內容不同，執行方式也不同。$R;" +
                                   "$R想聽詳細的說明嗎?$R;", "咖啡館老闆");

            selection = Select(pc, "想聽什麼說明呢?", "", "任務的注意事項", "關於「擊退任務」", "關於「收集任務」", "關於「搬運任務」", "什麼也不聽");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000002, 131, "成功完成任務的話，$R;" +
                                               "可以得到相對應的經驗值和報酬。$R;" +
                                               "$R但托付的任務並不是很多，$R;" +
                                               "有時候會供過於求，不太平衡啊!$R;" +
                                               "$P所以工作是有次數限制的。$R;" +
                                               "$R真是非常抱歉，因為還有別的冒險者，$R;" +
                                               "所以沒辦法只好這樣，請您諒解呀!$R;" +
                                               "$P除此之外，$R;" +
                                               "為了避免有人承接任務卻沒有回報，$R;" +
                                               "所以任務都定了時限呀!$R;" +
                                               "$R規定時間內沒有完成任務，$R;" +
                                               "就會當作任務失敗唷!$R;" +
                                               "這個任務，就會給別的冒險者了。$R;" +
                                               "$P剩餘的任務點數和任務所剩時間，$R;" +
                                               "可以在「任務視窗」確認喔。$R;" +
                                               "$R盡量不要失敗，$R;" +
                                               "請您努力吧!$R;", "咖啡館老闆");
                        break;

                    case 2:
                        Say(pc, 11000002, 131, "「擊退任務」就是要在指定的區域，$R;" +
                                               "抓到指定數量的魔物。$R;" +
                                               "$P例如：$R;" +
                                               "擊退「奧克魯尼亞東方平原」的$R;" +
                                               "5隻「皮露露」。$R;" +
                                               "$R接受這樣的任務時，$R;" +
                                               "只要抓住指定區域的5隻「皮露露」，$R;" +
                                               "任務就算成功了唷!$R;" +
                                               "$P其他地方的「皮露露」，$R;" +
                                               "並不會列入計算的。請多留意呀!$R;" +
                                               "$P委託內容和完成進度等，$R;" +
                                               "可以在「任務視窗」隨時確認唷!$R;" +
                                               "$R執行任務時，只要打開這個視窗，$R;" +
                                               "就可以隨時確認，很方便吧?$R;" +
                                               "$P任務成功後，要記得回報，$R;" +
                                               "這樣才可以拿到報酬喔。$R;" +
                                               "$R關於「報酬」，$R;" +
                                               "可以在任何附近的「任務服務台」拿到，$R;" +
                                               "所以只要到附近的「服務台」就可以了。$R;", "咖啡館老闆");
                        break;

                    case 3:
                        Say(pc, 11000002, 131, "「收集任務」就是收集指定道具的任務唷!$R;" +
                                               "$P如果接到收集3個『杰利科』的任務。$R;" +
                                               "$R只要想盡辦法收集3個『杰利科』，$R;" +
                                               "就算任務完成了。$R;" +
                                               "$P收集完以後，$R;" +
                                               "把道具拿到「任務服務台」就可以了。$R;" +
                                               "$R接受「收集任務」時，$R;" +
                                               "選擇「任務服務台」$R;" +
                                               "就會顯示交易視窗喔!$R;" +
                                               "$P把收集的道具，$R;" +
                                               "從道具視窗移到交易視窗的左邊，$R;" +
                                               "$R點擊『確認』再點擊『交易』，$R;" +
                                               "道具就交易到「服務台」了。$R;" +
                                               "$P交易指定數量後，$R;" +
                                               "任務就算成功了。$R;" +
                                               "$R如果道具太重，$R;" +
                                               "一次交易不了，可以分批送出喔。$R;" +
                                               "$P我會清點交易的道具的。$R;" +
                                               "$R我不會算錯啦，盡管放心!!$R;", "咖啡館老闆");
                        break;

                    case 4:
                        Say(pc, 11000002, 131, "「搬運任務」是從委託人那裡取得道具，$R;" +
                                               "然後轉交給收件人的任務唷!$R;" +
                                               "$P例如：$R;" +
                                               "在「下城」的$R;" +
                                               "「咖啡館的麥當娜」那裡$R;" +
                                               "取得4個『杰利科』。$R;" +
                                               "$R然後拿給「咖啡館」的$R;" +
                                               "「咖啡館老闆」。$R;" +
                                               "$P接到這樣的任務的話，$R;" +
                                               "只要從店外的「麥當娜」那裡取得4個『杰利科』，$R;" +
                                               "把道具轉交給我，就算成功了。$R;" +
                                               "$P要給予運送道具，$R;" +
                                               "只要跟相關的人交談就可以了。$R;" +
                                               "$R任務成功以後，$R;" +
                                               "就跟「擊退任務」一樣，$R;" +
                                               "到「任務服務台」，拿取報酬就可以了。$R;", "咖啡館老闆");
                        break;
                }

                selection = Select(pc, "想聽什麼說明呢?", "", "任務的注意事項", "關於「擊退任務」", "關於「收集任務」", "關於「搬運任務」", "什麼也不聽");
            }
        }

        void 擊退皮露露(ActorPC pc)
        {
            BitMask<Emil_Letter> Emil_Letter_mask = pc.CMask["Emil_Letter"];                                                                                            //任務：埃米爾介紹書

            Say(pc, 11000002, 131, "最近「阿高普路斯市」周圍，$R;" +
                                   "出現了很多「皮露露」，$R;" +
                                   "能不能擊退呢?$R;" +
                                   "$R「皮露露」是像布丁的天藍色魔物。$R;" +
                                   "$P在任務清單選擇任務後，$R;" +
                                   "點擊『確認』，就可以接受任務了。$R;" +
                                   "$R那麼，您想挑戰嗎?$R;", "咖啡館老闆");

            switch (Select(pc, "想怎麼做呢?", "", "挑戰任務", "再聽一次說明", "放棄"))
            {
                case 1:
                    if (pc.QuestRemaining > 0)
                    {
                        Emil_Letter_mask.SetValue(Emil_Letter.埃米爾介紹書任務完成, true);

                        TakeItem(pc, 10043081, 1);

                        HandleQuest(pc, 1);
                    }
                    else
                    {
                        Say(pc, 11000002, 131, "真是的，任務點數竟然是『0』呀!!$R;" +
                                               "$R只好下次再來吧。$R;", "咖啡館老闆");
                    }
                    break;

                case 2:
                    擊退皮露露(pc);
                    break;

                case 3:
                    break;
            }
        }

        void 古魯杜的遺言(ActorPC pc)
        {
            BitMask<Last_Words> Last_Words_mask = pc.CMask["Last_Words"];                                                                                               //任務：古魯杜的遺言

            if (!Last_Words_mask.Test(Last_Words.古魯杜的遺言任務開始))
            {
                Say(pc, 11000002, 131, "哦哦! " + pc.Name + "呀!$R;" +
                                       "$R不好意思，$R;" +
                                       "有件事情想拜託你幫忙。$R;" +
                                       "$P工作的內容是$R;" +
                                       "擔任『遺囑的見證人』。$R;" +
                                       "$R是不是有點無理啊?$R;" +
                                       "$P但像你一樣會守口如瓶，$R;" +
                                       "讓人信賴的冒險者不多啊?$R;" +
                                       "$P任務的報酬是『1萬金幣』。$R;" +
                                       "怎麼樣? 要接受委託嗎?$R;", "咖啡館老闆");

                switch (Select(pc, "怎麼辦呢?", "", "但是現在很忙啊…", "接受委託"))
                {
                    case 1:

                        Say(pc, 11000002, 131, "這樣啊?$R;" +
                                               "$R那還真是可惜，$R;" +
                                               "如果改變主意的話，$R;" +
                                               "請再跟我說吧。$R;", "咖啡館老闆");
                        break;

                    case 2:
                        Last_Words_mask.SetValue(Last_Words.古魯杜的遺言任務開始, true);

                        Say(pc, 11000002, 131, "真是感謝啊…$R;" +
                                               "這下令我放下心頭上的大石了。$R;" +
                                               "$P這是作為訂金的『3000金幣』。$R;" +
                                               "$R委託完成後，$R;" +
                                               "會給予剩於的金幣的。$R;" +
                                               "$P世界著名的「古魯杜先生」的$R;" +
                                               "「遺囑」打開的那天，$R;" +
                                               "去當「見證人」就可以了。$R;" +
                                               "$P他家在「摩根島」上，$R;" +
                                               "是位於城市東邊的建築物。$R;" +
                                               "$R準備好就出發前往那裡吧!$R;" +
                                               "$P他的女兒們都還在這座城市。$R;" +
                                               "$R要出發前去「摩根島」前，$R;" +
                                               "最好是去跟她們說說話吧。$R;" +
                                               "$R她們分別叫「鉑金」、「銀」以及「翡翠」。$R;" +
                                               "$P在「劇場」附近做生意，$R;" +
                                               "三人一組的商人就是她們。$R;" +
                                               "$R因為都是站著的，$R;" +
                                               "所以很容易找到的。$R;" +
                                               "$P這個是『摩根入國許可證』，$R;" +
                                               "在進入『摩根島』的時候會用到的。$R;" +
                                               "$R要好好保管喔!$R;", "咖啡館老闆");

                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10041900, 1);
                        pc.Gold += 3000;
                        Say(pc, 0, 65535, "得到『3000金幣』!$R;", " ");
                        Say(pc, 0, 65535, "得到『摩根入國許可證』!$R;", " ");
                        break;
                }
            }
            else
            {
                Say(pc, 11000002, 131, "「古魯杜先生」的女兒，$R;" +
                                       "她們在「劇場」附近做生意，$R;" +
                                       "去跟她們說說話吧!$R;", "咖啡館老闆");
            }
        }
    }
}