using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000257 : Event
    {
        public S11000257()
        {
            this.EventID = 11000257;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            if (mask.Test(Job2X_03.The_fourth_question_was_answered_incorrectly))//_4A07)
            {
                if (mask.Test(Job2X_03.The_third_question_is_answered_correctly) && CountItem(pc, 10000351) >= 1)
                {
                    mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, false);
                    //_4A07 = false;
                    Say(pc, 135, "Don't worry! The hint I can give is $R;" +
                         "『The way to cool down』! $R;");
                    return;
                }
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "Get 1 [Assassin Water 3]! $R;");
                     Say(pc, 135, "The hint I can give is $R;" +
                         "『The way to cool down』! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, false);
                    mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "Don't worry! The hint I can give is $R;" +
                         "『The way to cool down』! $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, true);
                //_4A71 = true;
                Say(pc, 135, "Your luggage is too full $R;" +
                    "I can't give you an item $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_third_question_is_answered_correctly))//_4A03)
            {
                if (mask.Test(Job2X_03.The_third_question_is_answered_correctly) && CountItem(pc, 10000351) >= 1)
                {
                    mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, false);
                    //_4A07 = false;
                    Say(pc, 135, "Don't worry! The hint I can give is $R;" +
                         "『The way to cool down』! $R;");
                    return;
                }
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "Get 1 [Assassin Water 3]! $R;");
                     Say(pc, 135, "The hint I can give is $R;" +
                         "『The way to cool down』! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, false);
                    mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "Don't worry! The hint I can give is $R;" +
                        "『The way to cool down』! $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, true);
                //_4A71 = true;
                Say(pc, 135, "Your luggage is too full $R;" +
                     "I can't give you an item $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_third_question_was_answered_incorrectly))//_4A06)
            {
                Say(pc, 135, "Please listen to the prompt again and come back! $R;");
                return;
            }
            if (mask.Test(Job2X_03.Did_not_get_the_Assassin_Water_3))//_4A71)
            {
                if (CheckInventory(pc, 10000351, 1))
                {
                    GiveItem(pc, 10000351, 1);
                    Say(pc, 131, "Get 1 [Assassin Water 3]! $R;");
 
                      Say(pc, 135, "The hint I can give is $R;" +
                          "『The way to cool down』! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, false);
                    mask.SetValue(Job2X_03.The_third_question_is_answered_correctly, true);
                    mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, false);
                    //_4A71 = false;
                    //_4A03 = true;
                    //_4A07 = false;
                    Say(pc, 135, "Don't worry! The hint I can give is $R;" +
                         "『The way to cool down』! $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, true);
                //_4A71 = true;
                Say(pc, 135, "Your luggage is too full $R;" +
                    "I can't give you an item $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_first_question_is_answered_correctly) && mask.Test(Job2X_03.The_second_question_is_answered_correctly))//_4A02 && _4A01)
            {
                Say(pc, 135, "Hey hey...Is it finally to me? $R;" +
                    "$P come on! Please feel free, you are welcome! $R;" +
                    "How have you been recently? $R;");
                switch (Select(pc, "what do you want to answer?", "", "just like that", "busy", "delicate"))
                {
                    case 1:
                        Say(pc, 135, "Okay, it means YES $R;" +
                            "$R just take this and go $R;");
                        if (CheckInventory(pc, 10000351, 1))
                        {
                            GiveItem(pc, 10000351, 1);
                            Say(pc, 131, "Get 1 [Assassin Water 3]! $R;");
                            Say(pc, 135, "The hint I can give is $R;" +
                                "『The way to cool down』! $R;");
                            mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, false);
                            mask.SetValue(Job2X_03.The_third_question_is_answered_correctly, true);
                            mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, false);
                            //_4A71 = false;
                            //_4A03 = true;
                            //_4A07 = false;
                            Say(pc, 135, "Don't worry! The hint I can give is $R;" +
                                "『The way to cool down』! $R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_3, true);
                        //_4A71 = true;
                        Say(pc, 135, "Your luggage is too full $R;" +
                             "I can't give you an item $R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, true);
                        //_4A06 = true;
                        Say(pc, 135, "Whatever it is, take it easy! $R;");
                        break;
                    case 3:
                        mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, true);
                        //_4A06 = true;
                        Say(pc, 135, "Um $R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.Assassin_transfer_begins))//_4A00)
            {
                Say(pc, 135, "Huh? Password? $R;" +
                     "What do you mean? $R;");
                return;
            }

            Say(pc, 135, "Hey hey...$R;" +
                "Will it be difficult for $R to enter Norton Kingdom? $R;");
            switch (Select(pc, "Want to buy that?", "", "Buy", "Don't buy"))
            {
                case 1:
                    OpenShopBuy(pc, 81);
                    Say(pc, 135, "Oh! Thank you! $R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
