using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:埃米爾(11001414) X:195 Y:64
namespace SagaScript.M10018103
{
    public class S11001414 : Event
    {
        public S11001414()
        {
            this.EventID = 11001414;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Conversation_with_Emil_for_the_first_time(pc);
            }
            else
            {
                Say(pc, 11001414, 131, "什麼，再說明一下?$R;", "埃米爾");
            }
            
            selection = Select(pc, "想問關於哪方面的操作呢?", "", "說話/攻擊", "移動方法", "結束遊戲方法", "移動地圖的方法", "其他");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001414, 131, "想跟村裡的人打招呼，$R;" +
                                               "或攻擊敵人的時候，$R;" +
                                               "對目標點擊滑鼠左鍵就可以囉。$R;" +
                                               "$P…看您跟我打招呼了，$R;" +
                                               "應該已經知道了吧?$R;" +
                                               "$P如果對方跟其他人重疊時，$R;" +
                                               "就請點擊滑鼠右鍵吧。$R;" +
                                               "$R將會出現重疊人名的視窗，$R;" +
                                               "在裡面可以選擇跟誰打招呼唷!$R;", "埃米爾");
                        break;

                    case 2:
                        Say(pc, 11001414, 131, "移動時，在移動目標上點擊左鍵即可。$R;" +
                                               "$R如果長時間持續點擊，$R;" +
                                               "就會自動跟著滑鼠移動方向移動。$R;" +
                                               "$P人太多的時候操作困難。$R;" +
                                               "$R這時用鍵盤的方向鍵移動，$R;" +
                                               "會比較方便唷!$R;", "埃米爾");
                        break;

                    case 3:
                        Say(pc, 11001414, 131, "要結束遊戲的話，$R;" +
                                               "請按鍵盤左上角ESC鍵…$R;" +
                                               "$P如果按這鍵…$R;" +
                                               "喂! 別現在點擊!$R;" +
                                               "$P然後…$R;" +
                                               "$R「返回遊戲」或「登出」$R;" +
                                               "$R兩個選項中做出選擇即可。$R;" +
                                               "$P注意一點，$R;" +
                                               "對話時結束遊戲，$R;" +
                                               "會聽不到後面的對話唷!$R;" +
                                               "$P登出需要5秒鐘的時間，$R;" +
                                               "$R這時移動或被魔物攻擊的話，$R;" +
                                               "會自動取消登出喔!$R;" +
                                               "$P在寒冷、炎熱或是在水裡等$R;" +
                                               "體質HP值漸漸減少的地方，$R;" +
                                               "是沒辦法登出的喔!$R;", "埃米爾");
                        break;

                    case 4:
                        Say(pc, 11001414, 131, "想移動到下一幅地圖時，$R;" +
                                               "看到藍色光柱了嗎?$R;" +
                                               "這是「傳送點」，$R;" +
                                               "踩到它就會進入下一個地圖唷!$R;" +
                                               "$P同樣的，進入店裡時踩傳送點即可。$R;" +
                                               "$P如果沒有光柱，$R;" +
                                               "傳送點可能是隱藏的。$R;" +
                                               "$R如果覺得可能有的話，$R;" +
                                               "就踩一踩試試看，$R;" +
                                               "可能有新發現唷!$R;", "埃米爾");
                        break;
                }

                selection = Select(pc, "想問關於哪方面的操作呢?", "", "說話/攻擊", "移動方法", "結束遊戲方法", "移動地圖的方法", "其他");
            }

            if (!Beginner_01_mask.Test(Beginner_01.The_Emil_gives_the_Emil_badge))
            {
                The_Emil_gives_the_Emil_badge(pc);
                return;
            }

            Say(pc, 11001414, 131, "那麼說明一下傳送點吧!$R;" +
                                   "$R只要點擊那個傳送點，$R;" +
                                   "就可以進入下一階段唷!$R;" +
                                   "$P跟著路走就是阿高普路斯市了!$R;" +
                                   "$R放心，$R;" +
                                   "只要參照小地圖來走，就不會迷路啦。$R;", "埃米爾");          
        }

        void Conversation_with_Emil_for_the_first_time(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            byte x, y;

            Beginner_01_mask.SetValue(Beginner_01.Have_already_had_the_first_conversation_with_Emil, true);

            Say(pc, 11001414, 131, "您好!$R;" +
                                   "是否第一次來到這裡呢?$R;" +
                                   "$R我叫埃米爾，$R;" +
                                   "是負責幫助來到$R;" +
                                   "ECO世界的初心者們。$R;" +
                                   "$P準備好了，就開始個人指導吧…$R;", "埃米爾");

            switch (Select(pc, "要進行指導嗎?", "", "進行指導", "直接返回遊戲"))
            {
                case 1:
                    Say(pc, 11001414, 131, "首先，歡迎來到ECO世界!$R;" +
                                           "$R現在，您的眼前即將展開魔幻新世界。$R;" +
                                           "$P那麼開始教您一些基本操作方法吧!$R;", "埃米爾");
                    break;

                case 2:
                    Say(pc, 11001414, 131, "真是的，以後再也聽不到說明喔!$R;", "埃米爾");

                    switch (Select(pc, "真的不聽說明嗎?", "", "聽完說明在走", "不聽也行!"))
                    {
                        case 1:
                            Say(pc, 11001414, 131, "那就開始吧!$R;", "埃米爾");

                            Say(pc, 11001414, 131, "首先，歡迎來到ECO世界!$R;" +
                                                   "$R現在，您的眼前即將展開魔幻新世界。$R;" +
                                                   "$P那麼開始教您一些基本操作方法吧!$R;", "埃米爾");
                            break;

                        case 2:
                            Beginner_01_mask.SetValue(Beginner_01.Emil_gives_Emil_introduction_book, true);

                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10043081, 1);
                            Say(pc, 0, 0, "得到『埃米爾介紹書』!$R;", " ");

                            Say(pc, 11001414, 131, "知道了，最後還有一點。$R;" +
                                                   "$P到達阿高普路斯市後，$R;" +
                                                   "最好去「咖啡館」或者「咖啡館分店」一趟。$R;" +
                                                   "$P「咖啡館」在「阿高普路斯市」的$R;" +
                                                   "「下城」東部喔!$R;" +
                                                   "$R「咖啡館分店」在「阿高普路斯市」的$R;" +
                                                   "東、南、西、北部平原的中央附近唷!$R;" +
                                                   "$P我有給您一份介紹書，$R;" +
                                                   "在道具視窗中確認一下吧?$R;" +
                                                   "$P「阿高普路斯市」是一個很大的城市喔!$R;" +
                                                   "迷了路可以利用「響導機械人」。$R;" +
                                                   "$R「響導機械人」是個有點奇怪的機器人，$R;" +
                                                   "「阿高普路斯市」裡到處都有，很容易找到的。$R;", "埃米爾");

                            Say(pc, 11001414, 131, "那麼送您到「阿高普路斯市」吧!$R;", "埃米爾");

                            x = (byte)Global.Random.Next(245, 250);
                            y = (byte)Global.Random.Next(126, 131);

                            Warp(pc, 10023100, x, y);
                            return;
                    }
                    break;
            }
        }

        void The_Emil_gives_the_Emil_badge(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.The_Emil_gives_the_Emil_badge, true);

            Say(pc, 11001414, 131, "還有…$R;" +
                                   "$R通過前面的傳送點，$R;" +
                                   "就會進入下一張地圖，$R;" +
                                   "沿著路走，$R;" +
                                   "就會有「關卡」唷!$R;" +
                                   "$R我的同伴，是道米尼男孩。$R;" +
                                   "$P他會教您很多關於戰鬥的東西唷!$R;" +
                                   "$R那麼，祝您好運!$R;", "埃米爾");

            Say(pc, 11001414, 131, "這是歡送您的禮物!$R;", "埃米爾");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10009550, 1);
            Say(pc, 0, 0, "得到『埃米爾徽章』!$R;", " ");

            Say(pc, 11001414, 131, "把這個徽章放到叫「ItemBox」的$R;" +
                                   "粉紅色機器就可以交換到$R;" +
                                   "各種有用的道具唷!$R;" +
                                   "$P試試將徽章$R;" +
                                   "放到我前面的「ItemBox」吧!$R;", "埃米爾");

            Say(pc, 11001414, 131, "對了!$R;" +
                                   "$R走之前，先到村落裡走走吧!$;" +
                                   "會聽到很多情報唷!$R;", "埃米爾");
        }   
    }
}
