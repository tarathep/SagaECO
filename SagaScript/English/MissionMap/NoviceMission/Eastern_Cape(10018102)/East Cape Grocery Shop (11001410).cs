using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:東方海角食品販賣(11001410) X:203 Y:98
namespace SagaScript.M10018102
{
    public class S11001410 : Event
    {
        public S11001410()
        {
            this.EventID = 11001410;
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
                    Say(pc, 11001410, 131, "This time will be briefly explained. $R;" +
                                           "When $P wants to sell an item, select [Sell something]. $R;" +
                                           "$R will display the transaction window, $R;" +
                                           "Move the item you want to sell to the right, $R;" +
                                           "Click [Sell]. $R;" +
                                           "$R is done like this! $R;" +
                                           "When $P wants to buy, choose [buy something]! $R;" +
                                           "$R will display the transaction window, $R;" +
                                           "Move the item you want to buy to the left, $R;" +
                                           "Click [Buy] $R;" +
                                           "$R so you can buy the goods you want! $R;" +
                                           "$P sell the acquired items, $R;" +
                                           "Use those money to buy equipment. $R;" +
                                           "$R so good, come on! $R;", "East Cape Grocery Shop");
                    break;
                    
                case 2:
                    break;
            }
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001410, 131, "Have you not heard Emil's explanation yet? $R;" +
                                    "$R, let's listen to it first! $R;", "East Cape Grocery Shop");
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

            Say(pc, 11001410, 131, "It's not difficult to buy things, right? $R;" +
                                     "The $P transaction is over, can it be done? $R;" +
                                     "$P sell the acquired items and you will have money, $R;" +
                                     "So you can purchase other equipment. $R;" +
                                     "$R, so come on! $R;", "East Cape Grocery Shop");
        }   

        void NPC物品販賣教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.NPC_item_buying_and_selling_teaching開始) &&
                !Beginner_01_mask.Test(Beginner_01.Sales_of_chocolate_completed))
            {
                Say(pc, 11001410, 131, "Hello!$R;" +
                                         "$R here is a food store, $R;" +
                                         "Want to know how to buy and sell items? $R;", "East Cape Grocery Shop");

                switch (Select(pc, "Listen to the method of buying and selling items, how about?", "", "I want to listen! I want to listen!", "No need"))
                {
                    case 1:
                        Beginner_01_mask.SetValue(Beginner_01.NPC_item_buying_and_selling_teaching開始, true);

                        GiveItem(pc, 10009200, 1);

                        Say(pc, 0, 0, "Get [Chocolate]! $R;", "");

                        Say(pc, 11001410, 131, "Teach you how to sell items first! $R;" +
                                               "$R try to sell the [chocolate] I gave you! $R;" +
                                               "$P moves the item you want to sell to the right window, $R;" +
                                               "Tap the [Sell] button again. $R;" +
                                               "$R is simple, isn't it? $R;", "East Cape Grocery Shop");
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
                Say(pc, 11001410, 131, "Selling things is not difficult, right? $R;" +
                                        "When there is a large amount of $R, there will be a window display, $R;" +
                                        "Ask how many do you want to sell! $R;" +
                                        "$P...then change to buy props now, right? $R;" +
                                        "$R use the money just got, buy $R from me;" +
                                        "[Apple] give it a try. $R;" +
                                        "$P moves the item you want to buy to the left window, $R;" +
                                        "Click the [Buy] button. $R;" +
                                        "$R is simple, isn't it? $R;", "East Cape Grocery Shop");
            }

            OpenShopBuy(pc, 188);

            if (CountItem(pc, 10002800) > 0)
            {
                Beginner_01_mask.SetValue(Beginner_01.Apple_purchase_completed, true);
            }
        }
    }
}
