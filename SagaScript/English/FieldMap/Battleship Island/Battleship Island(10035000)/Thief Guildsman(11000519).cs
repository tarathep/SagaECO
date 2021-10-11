using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000519 : Event
    {
        public S11000519()
        {
            this.EventID = 11000519;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];



            if (mask.Test(Job2X_03.The_second_question_was_answered_incorrectly))//_4A05)
            {
                if (mask.Test(Job2X_03.The_first_question_is_answered_correctly) && CountItem(pc, 10000309) >= 1)
                {
                    mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, false);
                    //_4A05 = false;
                    Say(pc, 131, "I heard that this exam will take some time! $R;" +
                         "You have to do it anyway $R;" +
                         "$P The reminder I gave is [Don't think of it as food] $R;");
                     return;
                }
                if (CheckInventory(pc, 10000309, 1))
                {
                    GiveItem(pc, 10000309, 1);
                    Say(pc, 131, "Got the [Assassin Water1] $R;");

                   mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, false);
                    mask.SetValue(Job2X_03.The_first_question_is_answered_correctly, true);
                    mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, false);
                    //_4A69 = false;
                    //_4A01 = true;
                    //_4A05 = false;
                    Say(pc, 131, "I heard that this exam will take some time! $R;" +
                         "You have to do it anyway $R;" +
                         "$P The reminder I gave is [Don't think of it as food] $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, true);
                //_4A69 = true;
                Say(pc, 131, "Your luggage is too much $R;" +
                     "Cannot give you $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_first_question_is_answered_correctly))//_4A01)
            {
                if (mask.Test(Job2X_03.The_first_question_is_answered_correctly) && CountItem(pc, 10000309) >= 1)
                {
                    mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, false);
                    //_4A05 = false;
                    Say(pc, 131, "I heard that this exam will take some time! $R;" +
                         "You have to do it anyway $R;" +
                         "$P The reminder I gave is [Don't think of it as food] $R;");
                    return;
                }
                if (CheckInventory(pc, 10000309, 1))
                {
                    GiveItem(pc, 10000309, 1);
                    Say(pc, 131, "Got the [Assassin Water1] $R;");
 
                      mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, false);
                    mask.SetValue(Job2X_03.The_first_question_is_answered_correctly, true);
                    //_4A69 = false;
                    //_4A01 = true;
                    mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, false);
                    //_4A05 = false;
                    Say(pc, 131, "I heard that this exam will take some time! $R;" +
                        "You have to do it anyway $R;" +
                        "$P The reminder I gave is [Don't think of it as food] $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, true);
                //_4A69 = true;
                Say(pc, 131, "Your luggage is too much $R;" +
                     "Cannot give you $R;");
                return;
            }
            if (mask.Test(Job2X_03.Did_not_get_the_Assassin_Water_1))//_4A69)
            {
                if (CheckInventory(pc, 10000309, 1))
                {
                    GiveItem(pc, 10000309, 1);
                    Say(pc, 131, "Got the [Assassin Water1] $R;");

                   mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, false);
                    mask.SetValue(Job2X_03.The_first_question_is_answered_correctly, true);
                    //_4A69 = false;
                    //_4A01 = true;
                    mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, false);
                    //_4A05 = false;
                    Say(pc, 131, "I heard that this exam will take some time! $R;" +
                         "You have to do it anyway $R;" +
                         "$P The reminder I gave is [Don't think of it as food] $R;");
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, true);
                //_4A69 = true;
                Say(pc, 131, "Your luggage is too much $R;" +
                     "Cannot give you $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_first_question_was_answered_incorrectly))//_4A04)
            {
                Say(pc, 131, "Go and listen to the prompt $R;" +
                    "How about coming here again? $R;");
                return;
            }
            if (mask.Test(Job2X_03.Assassin_transfer_begins))//_4A00)
            {
                Say(pc, 131, "This is the thieves’ examination room $R;" +
                    "$R better not want to help the person in the test $R;" +
                    "$P……$R;" +
                    "Does $P have any thoughts of visiting Morg City? $R;");
                switch (Select(pc, "How to do it?", "", "What is Morg City?", "Fly into the sky", "No...I didn't think about it"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.The_first_question_was_answered_incorrectly, true);
                        //_4A04 = true;
                        Say(pc, 131, "Do you really want to do it? $R;");
                        break;
                    case 2:
                        Say(pc, 131, "Hmm... That's right $R;" +
                            "$P then let me give the hint $R;" +
                            "The $R reminder is [Don't think of it as food] $R;" +
                            "$P! Almost forgot $R;");
                        if (CheckInventory(pc, 10000309, 1))
                        {
                            GiveItem(pc, 10000309, 1);
                            Say(pc, 131, "Got the [Assassin Water1] $R;");
                            mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, false);
                            mask.SetValue(Job2X_03.The_first_question_is_answered_correctly, true);
                            //_4A69 = false;
                            //_4A01 = true;
                            mask.SetValue(Job2X_03.The_second_question_was_answered_incorrectly, false);
                            //_4A05 = false;
                            Say(pc, 131, "I heard that this exam will take some time! $R;" +
                                "You have to do it anyway $R;" +
                                "$P The reminder I gave is [Don't think of it as food] $R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_1, true);
                        //_4A69 = true;
                        Say(pc, 131, "Your luggage is too much $R;" +
                             "Cannot give you $R;");
                        break;
                    case 3:
                        mask.SetValue(Job2X_03.The_first_question_was_answered_incorrectly, true);
                        //_4A04 = true;
                        Say(pc, 131, "It turns out to be a guy who has no dreams...$R;");
                        break;
                }
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
            Say(pc, 131, "This is the thieves’ examination room $R;" +
                "$R better not want to help $R;" +
                "Person in the test $R;");
        }
    }
}