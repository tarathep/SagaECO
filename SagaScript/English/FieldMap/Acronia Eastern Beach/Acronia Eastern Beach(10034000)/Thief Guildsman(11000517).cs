using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11000517 : Event
    {
        public S11000517()
        {
            this.EventID = 11000517;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            if (mask.Test(Job2X_03.The_third_question_was_answered_incorrectly))//_4A06)
            {
                if (mask.Test(Job2X_03.The_second_question_is_answered_correctly) && CountItem(pc, 10000350) >= 1)
                {
                    mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, false);
                    //_4A06 = false;
                    Say(pc, 131, "Friends! Do your best! $R;" +
                         "$P The hint I can give is [Yes, just like that] $R;");
                    return;
                }
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "I got 1 [Assassin Water 2]! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, false);
                    mask.SetValue(Job2X_03.The_second_question_is_answered_correctly, true);
                    mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "Friends! Do your best! $R;" +
                         "$P The hint I can give is [Yes, just like that] $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, true);
                //_4A70 = true;
                Say(pc, 131, "Your luggage is too much $R;" +
                     "Cannot give you $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_second_question_is_answered_correctly))//_4A02)
            {
                if (mask.Test(Job2X_03.The_second_question_is_answered_correctly) && CountItem(pc, 10000350) >= 1)
                {
                    mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, false);
                    //_4A06 = false;
                    Say(pc, 131, "Friends! Do your best! $R;" +
                         "$P The hint I can give is [Yes, just like that] $R;");
                    return;
                }
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "I got 1 [Assassin Water 2]! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, false);
                    mask.SetValue(Job2X_03.The_second_question_is_answered_correctly, true);
                    mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "Friends! Do your best! $R;" +
                         "$P The hint I can give is [Yes, just like that] $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, true);
                //_4A70 = true;
                Say(pc, 131, "Your luggage is too much $R;" +
                    "Cannot give you $R;");
                return;
            }
            if (mask.Test(Job2X_03.Did_not_get_the_Assassin_Water_2))//_4A70)
            {
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "I got 1 [Assassin Water 2]! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, false);
                    mask.SetValue(Job2X_03.The_second_question_is_answered_correctly, true);
                    mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "Friends! Do your best! $R;" +
                        "$P The hint I can give is [Yes, just like that] $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, true);
                //_4A70 = true;
                Say(pc, 131, "Your luggage is too much $R;" +
                    "Cannot give you $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_second_question_was_answered_incorrectly))//_4A05)
            {
                Say(pc, 131, "After listening to the prompt again $R;" +
                    "Come on again $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_first_question_is_answered_correctly))//_4A01)
            {
                Say(pc, 131, "This is the thieves’ examination room $R;" +
                    "$R better not want to help the person in the test $R;" +
                    "$P……$R;" +
                    "$P but other things $R;" +
                    "What do you think of curry? $R;");
                switch (Select(pc, "How to do it?", "", "I don't like curry so much", "Eat!", "Drink!"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, true);
                        //_4A05 = true;
                        Say(pc, 131, "Hurry back to $R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, true);
                        //_4A05 = true;
                        Say(pc, 131, "It's still far away...$R;");
                        break;
                    case 3:
                        Say(pc, 131, "Really?!$R;" +
                            "I like curry so much!! $R;" +
                            "$R will not hate eating curry every day! $R;" +
                            "It's better to drink...$R;" +
                            "$R...$R;" +
                            "$P, tell me a hint, $R;" +
                            "$R prompt is [Yes, just like that] $R;" +
                            "You can guess it $R;" +
                            "$P, take this and go $R;");
                        if (CheckInventory(pc, 10000350, 1))
                        {
                            GiveItem(pc, 10000350, 1);
                            Say(pc, 131, "I got 1 [Assassin Water 2]! $R;");
                            mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, false);
                            mask.SetValue(Job2X_03.The_second_question_is_answered_correctly, true);
                            mask.SetValue(Job2X_03.The_third_question_was_answered_incorrectly, false);
                            //_4A70 = false;
                            //_4A02 = true;
                            //_4A06 = false;
                            Say(pc, 131, "Friends! Do your best! $R;" +
                                "$P The hint I can give is [Yes, just like that] $R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_2, true);
                        //_4A70 = true;
                        Say(pc, 131, "Your luggage is too much $R;" +
                             "Cannot give you $R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.Assassin_transfer_begins))//_4A00)
            {
                Say(pc, 131, "What? Is the code? $R;" +
                    "$R what are you talking about? $R;");
                return;
            }

            if (JobBasic_03_mask.Test(JobBasic_03.Choose_to_become_a_scout))
            {
                Say(pc, 131, "Do you want to give up being a scout? $R;" +
                    "$R... Now you can give up although you can $R;" +
                    "Challenge again...$R;");
                switch (Select(pc, "How to do it?", "", "Don't give up!", "Give up..."))
                {
                    case 1:
                        break;
                    case 2:
                        JobBasic_03_mask.SetValue(JobBasic_03.Choose_to_become_a_scout, false);

                        SetHomePoint(pc, 10023400, 124, 3);

                        Warp(pc, 10023400, 124, 3);
                        break;
                }
                return;
            }
            Say(pc, 131, "This is the scont’ examination room $R;" +
                "$R better not want to help $R;" +
                "Person in the test $R;");
        }
    }
}