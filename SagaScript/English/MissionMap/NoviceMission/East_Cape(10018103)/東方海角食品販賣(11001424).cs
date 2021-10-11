using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:東方海角食品販賣(11001424) X:203 Y:98
namespace SagaScript.M10018103
{
    public class S11001424 : Event
    {
        public S11001424()
        {
            this.EventID = 11001424;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            if (!Beginner_01_mask.Test(Beginner_01.NPC_item_buying_and_selling_teaching_completed))
            {
                NPC_item_buying_and_selling_teaching(pc);
                return;
            }

            switch (Select(pc, "Want to listen again?", "", "Listen again", "No need"))
            {
                case 1:
                    Say(pc, 11001424, 131, "This time will be briefly explained. $R;" +
                                           "When $P wants to sell an item, select [Sell something]. $R;" +
                                           "$R will display the transaction window, $R;" +
                                           "Move the item you want to sell to the right, $R;" +
                                           "Click [Sell]. $R;" +
                                           "$R is done like this! $R;" +
                                           "When $P wants to buy, choose [buy something]! $R;" +
                                           "$R will display the transaction window, $R;" +
                                           "Move the item you want to buy to the left, $R;" +
                                           "Click [Buy.] $R;" +
                                           "$R so you can buy the goods you want! $R;" +
                                           "$P sell the acquired items, $R;" +
                                           "Use those money to buy equipment. $R;" +
                                           "$R so good, come on! $R;", "East Cape Food Vendor");
                    break;

                case 2:
                    break;
            }
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001424, 131, "Have you heard Emil's explanation yet? $R;" +
                                   "$R, let's listen to it first! $R;", "East Cape Food Vendor");
        }

        void NPC_item_buying_and_selling_teaching(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Sales_of_chocolate_completed))
            {
                NPC物品販賣教學(pc);
                return;
            }

            if (!Beginner_01_mask.Test(Beginner_01.Apple_purchase_completed))
            {
                NPC物品購買教學(pc);
                return;
            }

            Beginner_01_mask.SetValue(Beginner_01.NPC_item_buying_and_selling_teaching_completed, true);

            Say(pc, 11001424, 131, "買東西也不難吧?$R;" +
                                   "$P交易這樣就算結束了，可以做到吧?$R;" +
                                   "$P把取得的道具賣掉，就有錢囉，$R;" +
                                   "這樣就可以購買其他裝備了。$R;" +
                                   "$R那麼加油吧!$R;", "東方海角食品販賣");
        }   

        void NPC物品販賣教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.NPC_item_buying_and_selling_teaching開始) &&
                !Beginner_01_mask.Test(Beginner_01.Sales_of_chocolate_completed))
            {
                Say(pc, 11001424, 131, "您好!$R;" +
                                       "$R這裡是食品店，$R;" +
                                       "想知道買賣物品的方法嗎?$R;", "東方海角食品販賣");

                switch (Select(pc, "聽一聽買賣物品的方法，如何?", "", "我要聽! 我要聽!", "不用了"))
                {
                    case 1:
                        Beginner_01_mask.SetValue(Beginner_01.NPC_item_buying_and_selling_teaching開始, true);

                        GiveItem(pc, 10009200, 1);

                        Say(pc, 0, 0, "得到『巧克力』!$R;", " ");

                        Say(pc, 11001424, 131, "先教您賣道具的方法吧!$R;" +
                                               "$R試試賣一下我給您的『巧克力』吧!$R;" +
                                               "$P把想賣的道具移到右邊視窗，$R;" +
                                               "再點一下「賣」鍵。$R;" +
                                               "$R很簡單吧?$R;", "東方海角食品販賣");
                        break;

                    case 2:
                        return;
                }
            }

            OpenShopSell(pc, 188);

            if (CountItem(pc, 10009200) == 0)
            {
                Beginner_01_mask.SetValue(Beginner_01.Sales_of_chocolate_completed, true);
            }
        }

        void NPC物品購買教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Apple_purchase_completed) &&
                !Beginner_01_mask.Test(Beginner_01.NPC_item_buying_and_selling_teaching_completed))
            {
                Say(pc, 11001424, 131, "賣東西不難吧?$R;" +
                                       "$R身上數量多的時候，會有視窗顯示，$R;" +
                                       "問您想賣幾個唷!$R;" +
                                       "$P嗯…那麼現在換買道具看看吧?$R;" +
                                       "$R用剛才得到的錢，在我這裡買$R;" +
                                       "『蘋果』試試看吧。$R;" +
                                       "$P把想買的道具移到左邊視窗，$R;" +
                                       "點擊「購買」鍵即可。$R;" +
                                       "$R很簡單吧?$R;", "東方海角食品販賣");
            }

            OpenShopBuy(pc, 188);

            if (CountItem(pc, 10002800) > 0)
            {
                Beginner_01_mask.SetValue(Beginner_01.Apple_purchase_completed, true);
            }
        }
    }
}
