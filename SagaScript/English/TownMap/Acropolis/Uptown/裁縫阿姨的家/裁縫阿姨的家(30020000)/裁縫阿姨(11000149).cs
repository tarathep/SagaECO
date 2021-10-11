using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Item;
//所在地圖:裁縫阿姨的家(30020000) NPC基本信息:裁縫阿姨(11000149) X:3 Y:1
namespace SagaScript.M30020000
{
    public class S11000149 : Event
    {
        public S11000149()
        {
            this.EventID = 11000149;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];


            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被詢問犯人的事) &&
                !Neko_04_cmask.Test(Neko_04.被告知犯人是小孩))
            {
                Neko_04_cmask.SetValue(Neko_04.被告知犯人是小孩, true); 
                Say(pc, 0, 131, "桃子!$R這位奶奶是誰啊?$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "什麼?$R綠子!什麼奶奶啊!!$R;", "凱堤（桃）");
                Say(pc, 131, "奶奶?$R叫我奶奶?$R;" +
                    "$R嗯嗯…叫奶奶!!$R;");
                Say(pc, 0, 131, "咪 咪 喵…$R;", "凱堤（桃）");
                Say(pc, 131, "那個就那樣了$R…你又被凱堤纏著了?$R;" +
                    "$R是人太好還是太傻啊$R;");
                Say(pc, 0, 131, "還是…那樣啊(嘆息)$R;");
                Say(pc, 0, 131, "咪咪!咪咪喵!喵!$R;" +
                    "$R咪咪!咪咪喵!喵!$R;");
                Say(pc, 131, "嗯嗯!這樣啊!$R;" +
                    "$R…雖然不知道是什麼意思$R但好像是在說「犯人是小孩!」$R;" +
                    "$P…到底什麼意思啊?$R;");
                Say(pc, 0, 131, "犯人是小孩…??$R;");
                return;
            }
            //*/
            if (Neko_02_cmask.Test(Neko_02.藍任務失敗))
            {
                Neko_02_cmask.SetValue(Neko_02.藍任務失敗, false);
                Say(pc, 131, "…快把「原始」停止，小貓的靈魂…$R真可憐啊$R;" +
                    "$R不要那麼傷心了，不是你的錯啊$R;" +
                    "$R你只是做了一件$R不管是誰一定要做的事情而已$R;" +
                    "$R那凱堤也總有一天會理解的$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍) &&
                !Neko_02_cmask.Test(Neko_02.得到三角巾) &&
                CountItem(pc, 10043700) >= 1 &&
                CountItem(pc, 10021300) >= 1 &&
                CountItem(pc, 10019800) >= 1)
            {
                switch (Select(pc, "要不要委託製作『裁縫阿姨的三角巾』", "", "委託", "放棄"))
                {
                    case 1:
                        Neko_02_cmask.SetValue(Neko_02.得到三角巾, true);
                        TakeItem(pc, 10043700, 1);
                        TakeItem(pc, 10021300, 1);
                        TakeItem(pc, 10019800, 1);
                        GiveItem(pc, 10017904, 1);
                        Say(pc, 131, "得到『裁縫阿姨的三角巾』$R;");
                        Say(pc, 131, "可以的話，快阻止「原始」的復活吧$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.獲知原始的事情) &&
                !Neko_02_cmask.Test(Neko_02.得到藍))
            {
                Say(pc, 131, "可以的話，快阻止「原始」的復活吧$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.聽取建議) &&
                !Neko_02_cmask.Test(Neko_02.獲知原始的事情))
            {

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        藍(pc);
                        return;
                    }
                }
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話) &&
                !Neko_02_cmask.Test(Neko_02.得知維修方法))
            {
                Say(pc, 131, "修理故障的活動木偶的話$R凱堤也會離開活動木偶回來的$R;" +
                    "$R如果是唐卡人說不定會告訴您$R修理活動木偶得方法$R;" +
                    "$R工匠會打扮成道具Appraiser的模樣$R;" +
                    "不知道會不會在阿高普路斯呢~$R;");
                判斷(pc);
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.藍任務開始) && 
                !Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話))
            {

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900 ||
                        pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Neko_02_cmask.SetValue(Neko_02.與裁縫阿姨第一次對話, true);
                        Say(pc, 131, "哎喲！凱堤好吵啊！$R;" +
                            "$R怎麼了?$R;");
                        Say(pc, 0, 131, "喵!!咪咪!!喵…$R;", "凱堤(桃子)");
                        Say(pc, 131, "真是真是!$R;" +
                            "是嗎?這樣啊?$R不論怎樣我都會給您做啊$R;");
                        Say(pc, 131, "…??$R;");
                        Say(pc, 131, "你的凱堤好像發現了朋友$R;" +
                            "$P可能把出故障的活動木偶$R當作了主人$P憑依的狀態下怎樣都無法脫離啊$R;");
                        Say(pc, 0, 131, "喵~~~$R;", "凱堤(桃子)");
                        Say(pc, 131, "修理故障的活動木偶的話$R凱堤也會離開活動木偶回來的$R;" +
                            "$R如果是唐卡人說不定會告訴您$R修理活動木偶得方法$R;" +
                            "$R唐卡是活動木偶和飛空庭的$R最大生産國$R;" +
                            "$R可是真的奇怪啊…$R為什麼“凱堤”$R會在沒有生命的活動木偶上…$R;" +
                            "$R難道…?不是…不會是的$R;");
                        Say(pc, 131, "…?$R;");
                        return;
                    }
                }
            }
            


            if (!Neko_01_amask.Test(Neko_01.Peach_mission_completed) &&
                Neko_01_cmask.Test(Neko_01.再次與祭祀對話) &&
                !Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾))
            {
                Say(pc, 131, "哎呀!$R好可愛的寵物啊!$R;" +
                    "$P坐在肩膀上…$R好…好…心情好像很好啊$R;");
                Say(pc, 0, 131, "咪~嗷$R;", "");
                Say(pc, 131, "…??$R;");
                if (!Neko_01_amask.Test(Neko_01.Peach_mission_completed) &&
                    Neko_01_cmask.Test(Neko_01.再次與祭祀對話) &&
                    !Neko_01_cmask.Test(Neko_01.得到裁縫阿姨的三角巾) &&
                    CountItem(pc, 10043700) >= 1 &&
                    CountItem(pc, 10021300) >= 1 &&
                    CountItem(pc, 10019800) >= 1)
                {
                    Say(pc, 131, "哎呀!$R;" +
                        "那『棉緞帶』還有『布』和『線』！$R;" +
                        "$P只要集齊這三樣$R就可以給那傢伙製作漂亮的禮物了$R;" +
                        "$R怎麼樣?要製作嗎?$R;");
                    switch (Select(pc, "要製作嗎？", "", "要", "不要"))
                    {
                        case 1:
                            Neko_01_cmask.SetValue(Neko_01.得到裁縫阿姨的三角巾, true);
                            TakeItem(pc, 10043700, 1);
                            TakeItem(pc, 10021300, 1);
                            TakeItem(pc, 10019800, 1);
                            GiveItem(pc, 10017904, 1);
                            Say(pc, 131, "得到『裁縫阿姨的三角巾』$R;");
                            Say(pc, 0, 131, "咪-咪-喵$R;");
                            Say(pc, 131, "好可愛啊…看來心情很好啊$R太好了$R;" +
                                "$P哎呀！你的小貓不見了？$R真是，怎麼搞得?$R;" +
                                "$R這麼可愛…$R;" +
                                "$R…對了!$R我奶奶說小貓喜歡「溫暖的光」$R;" +
                                "$P給那小貓溫暖的光的話$R會不會能看到牠的樣子呢？$R怎麼樣?$R;");
                            Say(pc, 0, 131, "喵~$R;");
                            break;
                        case 2:
                            Say(pc, 131, "真是…沒辦法啊…$R下次再來吧$R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "真的可惜啊...$R只要有材料，可以給牠製作漂亮的禮物呢…$R;");
                return;
            }
            /*
            if (!_0b12)
            {
                判斷(pc);
                return;
            }
            */
            判斷(pc);
        }

        void 判斷(ActorPC pc)
        {
            BitMask<Puppet_02> Puppet_02_mask = pc.CMask["Puppet_02"];
            if (Puppet_02_mask.Test(Puppet_02.要求製作泰迪))
            {
                木偶泰迪(pc);
                return;
            }
            if (CountItem(pc, 10020208) >= 1)
            {
                Say(pc, 131, "呃?你拿著的那個東西$R;" +
                    "好像是『縫製玩偶的布』啊$R;" +
                    "$R…原來是這樣啊$R;" +
                    "$P這是能動的玩偶$R;" +
                    "『活動木偶泰迪』的材料!$R;" +
                    "$R只要你願意我可以給你製作喔$R;");
                switch (Select(pc, "要不要請他幫忙呢?", "", "要", "不要"))
                {
                    case 1:
                        Say(pc, 131, "不要著急啦，還差幾樣材料$R;" +
                            "$R一個是『棉花』還有一個$R;" +
                            "是『奧拉克妮線』$R;" +
                            "最後是3枝『針』$R;" +
                            "$P但是那針不是一般的針$R;" +
                            "$P是需要得裁縫之神守護的$R;" +
                            "特殊針3支$R;" +
                            "$P用什麼辦法可以弄到那種針嗎?$R;" +
                            "$R那個我不太清楚$R;" +
                            "我從來不外出的$R;" +
                            "$P無論如何找一找試試看吧$R;" +
                            "我會在這裡等您的$R;");
                        Puppet_02_mask.SetValue(Puppet_02.要求製作泰迪, true);
                        break;
                    case 2:
                        普通販賣(pc);
                        break;
                }
                return;
            }
            普通販賣(pc);
        }

        void 普通販賣(ActorPC pc)
        {
            switch (Select(pc, "想做什麼呢?", "", "買男性服裝", "買女性服裝", "賣東西", "委託裁縫", "委託烹調", "什麼都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 19);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 3:
                    OpenShopSell(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 4:
                    Synthese(pc, 2054, 5);
                    break;
                case 5:
                    Synthese(pc, 2040, 5);
                    break;
                case 6:
                    break;
            }
        }

        void 藍(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];

            Neko_02_cmask.SetValue(Neko_02.獲知原始的事情, true);
            Say(pc, 131, "還是那樣了!?$R;" +
                "$R真是大事啊…!$R;");
            Say(pc, 0, 131, "…!?$R;", " ");
            Say(pc, 131, "平復一下心情後好好聽啊$R;" +
                "$R那活動木偶塔依$R不是你認識的活動木偶$R;" +
                "$P那塔依是「原始」$R;");
            Say(pc, 0, 131, "…原始…?$R;", "凱堤(桃子)");
            Say(pc, 131, "是啊~$R;" +
                "$P活動木偶塔依$R是根據機械文明時代的設計圖$R在身上貼上洋鐵後製作而成的$R自動戰鬥兵器的複製品$R;" +
                "$P以現在的技術$R無法發揮它原來的性能$R所以只能當作活動木偶來用$R;");
            Say(pc, 131, "也可以說是在塔依中$R真的難得擁有「原來的性能」的$R;" +
                "$P把那個叫「原始」$R擁有人工智能的「原始」$R聽說自己一個也可以瞬間$R把一個村落毁掉$R;" +
                "$P萬一「原始」出現的話$R技術人員會把那個活動木偶停止後$R立即銷毁$R;" +
                "$P萬一以前的「破壞命令」$R還殘餘在電子頭腦裡的話$R就大事不妙了$R;");
            Say(pc, 0, 131, "…$R;", " ");
            Say(pc, 131, "要停止「原始」啓動啊…$R;" +
                "$R現在開始告訴你停止塔依的方法$R;" +
                "$R塔依的背下面有個修理箱$R;" +
                "$R把它打開，裡面有黃色插頭$R把那個拔掉就可以了$R;" +
                "$P那樣的話「原始」會$R馬上停止活動的$R;" +
                "$P「原始」自己恢復的話$R到時候就沒辦法了$R;" +
                "$R可以的話盡快阻止「原始」復活啊$R;");

            if (CountItem(pc, 10017902) >= 1)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                    {
                        Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                        Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                            "$P那可不行!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;");
                        Say(pc, 0, 131, "…救救藍$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子!?$R;", "凱堤(桃子)");
                        Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                            "$R…現在就剩下我們兩個$R;" +
                            "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子…$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;" +
                            "知道了$R雖然不能肯定…$R;" +
                            "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                            "『棉緞帶』、『布』和『線』$R;" +
                            "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                            "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                            "$R現在好了嗎?$R;");
                        Say(pc, 0, 131, "喵$R;");
                        return;
                    }
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                    Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                    Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                        "$P那可不行!!$R;", "凱堤(桃子)");
                    Say(pc, 131, "…$R;");
                    Say(pc, 131, "…$R;" +
                        "知道了$R雖然不能肯定…$R;" +
                        "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                        "『棉緞帶』、『布』和『線』$R;" +
                        "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                        "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                        "$R現在好了嗎?$R;");
                    Say(pc, 0, 131, "喵$R;");
                    return;
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                {
                    Say(pc, 0, 131, "…救一下藍$R;", "凱堤(綠子)");
                    Say(pc, 131, "?…$R是草綠色的凱堤…$R這真是沒辦法…$R;");
                    Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                        "$R…現在就剩下我們兩個$R;" +
                        "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                    Say(pc, 131, "…$R;");
                    Say(pc, 131, "…$R;" +
                        "知道了$R雖然不能肯定…$R;" +
                        "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                        "『棉緞帶』、『布』和『線』$R;" +
                        "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                        "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                        "$R現在好了嗎?$R;");
                    Say(pc, 0, 131, "喵$R;");
                    return;
                }
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017902)
                    {
                        Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                        Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                            "$P那可不行!!$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;");
                        Say(pc, 0, 131, "…救救藍$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子!?$R;", "凱堤(桃子)");
                        Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                            "$R…現在就剩下我們兩個$R;" +
                            "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                        Say(pc, 0, 131, "綠子…$R;", "凱堤(桃子)");
                        Say(pc, 131, "…$R;" +
                            "知道了$R雖然不能肯定…$R;" +
                            "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                            "『棉緞帶』、『布』和『線』$R;" +
                            "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                            "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                            "$R現在好了嗎?$R;");
                        Say(pc, 0, 131, "喵$R;");
                        return;
                    }
                }
            }
            if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                    "$P那可不行!!$R;", "凱堤(桃子)");
                Say(pc, 131, "…$R;");
                Say(pc, 0, 131, "…救救藍$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "綠子!?$R;", "凱堤(桃子)");
                Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                    "$R…現在就剩下我們兩個$R;" +
                    "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                Say(pc, 0, 131, "綠子…$R;", "凱堤(桃子)");
                Say(pc, 131, "…$R;" +
                    "知道了$R雖然不能肯定…$R;" +
                    "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                    "『棉緞帶』、『布』和『線』$R;" +
                    "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                    "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                    "$R現在好了嗎?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
            if (CountItem(pc, 10017900) >= 1)
            {
                Say(pc, 0, 131, "稍等!!$R;", "凱堤(桃子)");
                Say(pc, 131, "?…$R是粉紅色的凱堤…$R這真是沒辦法…$R;");
                Say(pc, 0, 131, "不過…$R那樣的話…藍…也會消失啊!!$R;" +
                    "$P那可不行!!$R;", "凱堤(桃子)");
                Say(pc, 131, "…$R;");
                Say(pc, 131, "…$R;" +
                    "知道了$R雖然不能肯定…$R;" +
                    "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                    "『棉緞帶』、『布』和『線』$R;" +
                    "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                    "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                    "$R現在好了嗎?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "…救一下藍$R;", "凱堤(綠子)");
                Say(pc, 131, "?…$R是草綠色的凱堤…$R這真是沒辦法…$R;");
                Say(pc, 0, 131, "…求求您$R朋友們都在戰爭中死掉了$R;" +
                    "$R…現在就剩下我們兩個$R;" +
                    "$R…就剩下我們兩個$R;", "凱堤(綠子)");
                Say(pc, 131, "…$R;");
                Say(pc, 131, "…$R;" +
                    "知道了$R雖然不能肯定…$R;" +
                    "$P可以拿跟以前一樣的材料過來嗎?$R;" +
                    "『棉緞帶』、『布』和『線』$R;" +
                    "$P給您製作跟那凱堤一樣的$R三角頭巾吧$R;" +
                    "$P只好相信凱堤會憑依在$R我製作的三角頭巾上$R;" +
                    "$R現在好了嗎?$R;");
                Say(pc, 0, 131, "喵$R;");
                return;
            }
        }

        void 木偶泰迪(ActorPC pc)
        {
            switch (Select(pc, "什麼事情啊?", "", "買男性服裝", "買女性服裝", "賣東西", "委託裁縫", "委託烹調", "製作活動木偶泰迪", "委託烹調"))
            {
                case 1:
                    OpenShopBuy(pc, 19);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 2:
                    OpenShopBuy(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 3:
                    OpenShopSell(pc, 20);
                    Say(pc, 131, "再來玩吧$R;");
                    break;
                case 4:
                    Synthese(pc, 2054, 5);
                    break;
                case 5:
                    Synthese(pc, 2040, 5);
                    break;
                case 6:
                    if (CountItem(pc, 10020208) >= 1 &&
                        CountItem(pc, 10019701) >= 1 &&
                        CountItem(pc, 10019702) >= 1 &&
                        CountItem(pc, 10019703) >= 1 &&
                        CountItem(pc, 10024002) >= 1 &&
                        CountItem(pc, 10019600) >= 1)
                    {
                        Say(pc, 131, "材料都弄齊了!$R;" +
                            "現在開始就是我做的事了$R;" +
                            "交給我吧!$R;");
                        Say(pc, 131, "給了他『縫製玩偶的布』$R;" +
                            "給了他『奧拉克妮線』$R;" +
                            "給了他『棉花』$R;" +
                            "給了他『早晨的針』$R;" +
                            "給了他『白天的針』$R;" +
                            "給了他『夜晚的針』$R;");

                        Fade(pc, FadeType.Out, FadeEffect.Black);
                        Wait(pc, 1000);
                        Wait(pc, 1000);
                        Fade(pc, FadeType.In, FadeEffect.Black);
                        Say(pc, 131, "好了!$R;" +
                            "我嘔心瀝血的傑作$R;" +
                            "$R很可愛吧?$R;" +
                            "好好珍惜使用吧！$R;");
                        PlaySound(pc, 4006, false, 100, 50);
                        Say(pc, 131, "得到了『活動木偶泰迪』$R;");
                        TakeItem(pc, 10020208, 1);
                        TakeItem(pc, 10019701, 1);
                        TakeItem(pc, 10019702, 1);
                        TakeItem(pc, 10019703, 1);
                        TakeItem(pc, 10024002, 1);
                        TakeItem(pc, 10019600, 1);
                        GiveItem(pc, 10022000, 1);
                        return;
                    }
                    Say(pc, 131, "活動木偶泰迪的材料是$R;" +
                        "『縫製玩偶的布』$R;" +
                        "『棉花』$R;" +
                        "『奧拉克妮線』$R;" +
                        "『針』$R;" +
                        "$P針如果不是得裁縫之神守護的$R;" +
                        "特殊針，是不行的阿$R;" +
                        "$R裁縫之神化成蝴蝶的樣子$R;" +
                        "注視著這個世界呢$R;" +
                        "$P無論怎麼樣，你找找看吧$R;" +
                        "我會等著的!$R;");
                    木偶泰迪(pc);
                    break;
                case 7:
                    break;
            }
        }

        void 萬聖節(ActorPC pc)
        {

            //EVT1100014953
            switch (Select(pc, "怎麼做好呢？", "", "就那樣打招呼", "不給糖就搗蛋！"))
            {
                case 1:
                    判斷(pc);
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
                            Say(pc, 131, "呵呵呵$R;" +
                                "$R小妖精長的好可愛阿$R;" +
                                "來！給你餅乾，不許淘氣啊$R;");
                            if (CheckInventory(pc, 10009300, 1))
                            {
                                //_0b12 = true;
                                GiveItem(pc, 10009300, 1);
                                return;
                            }
                            return;
                        }
                    }
                    Say(pc, 131, "嗯…打扮後再來吧$R;" +
                        "到時候我會給你餅乾的$R;");
                    break;
            }
        }
    }
}
