using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:盜賊行會(30042000) NPC基本信息:Scout Master(11000017) X:3 Y:3
namespace SagaScript.M30042000
{
    public class S11000017 : Event
    {
        public S11000017()
        {
            this.EventID = 11000017;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            Say(pc, 11000017, 131, "welcome!$R;" +
                                   "This is the Scout' Guild $R;", "Scout Master");

            if (JobBasic_03_mask.Test(JobBasic_03.scout_successfully_transferred) &&
                !JobBasic_03_mask.Test(JobBasic_03.Has_been_transferred_to_a_scout))
            {
                scout_transfer_completed(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
            {
                if (JobBasic_03_mask.Test(JobBasic_03.Choose_to_become_a_scout) &&
                    !JobBasic_03_mask.Test(JobBasic_03.Has_been_transferred_to_a_scout))
                {
                    Scout_transfer_task(pc);
                    return;
                }
                else
                {
                    Introduction_to_Scout(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.SCOUT)
            {
                if (mask.Test(Job2X_03.The_first_question_was_answered_incorrectly))//_4A04)
                {
                    mask.SetValue(Job2X_03.The_first_question_was_answered_incorrectly, false);
                    //_4A04 = false;
                    Say(pc, 131, "The password is wrong? $R;" +
                         "There is no way $R;" +
                         "$R tell you again, you have to listen carefully $R;" +
                         "$P reminder is [Sky]!!! $R;" +
                         "Don't forget it again! $R;", "Scout Master");
                    return;
                }
                if (mask.Test(Job2X_03.Assassin_transfer_begins))//_4A00)
                {
                    Advanced_transfer_complete(pc);
                    return;
                }
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "Tell you to put on clothes quickly $R;", "Scout Master");
                    return;
                }

                Say(pc, 11000017, 131, pc.Name + "? right? $R;" +
                                        "What day is $R today? $R;", "Scout Master");
                //EVT1100001760
                switch (Select(pc, "What do you want to do?", "", "Task Desk", "Sell Entry Permit", "Sell Drugs", "I Want to Change Jobs", "Do nothing "))
                {
                    case 1:
                        HandleQuest(pc, 13);
                        break;
                    case 2:
                        Say(pc, 131, "Do you want to go to Aienzaus? $R;", "Scout Master");
                        OpenShopBuy(pc, 105);
                        break;
                    case 3:
                        Say(pc, 131, "Be careful! $R;", "Scout Master");
                        OpenShopBuy(pc, 153);
                        break;
                    case 4:
                        if (!mask.Test(Job2X_03.End_of_transfer))
                        {
                            Introduction_to_Advanced_transfer(pc);
                            return;
                        }
                        Say(pc, 131, "Cannot be transferred anymore $R;", "Scout Master");
                        break;
                    case 5:
                        break;
                }
                //EVENTEND

                switch (Select(pc, "What do you want to do?", "", "Task Service Desk", "Sell Entry Permit", "I want to change jobs", "Do nothing"))
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;
                }
            }
        }

        void Introduction_to_Scout(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            int selection;

            Say(pc, 11000017, 131, "I am the Scout Master who manages scouts. $R;" +
                                    "$P, you don't seem to belong to our guild's jurisdiction? $R;" +
                                    "$R...In this way...$R;" +
                                    "Do you want to be a [scout]? $R;", "Scout Master");

            selection = Select(pc, "What do you want to do?", "", "I want to be a [Scout]!", "What kind of profession is a [Scout]?", "Task Desk", "Do nothing");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        if (pc.Str > 9)
                        {
                            Say(pc, 11000017, 131, "Haha, do you want to be a [scout]? $R;" +
                                                    "$R wants to be a scout, $R;" +
                                                    "You have to pass the [test] first. $R;" +
                                                    "$P...Do you want to be tested with the determination to die? $R;", "Scout Master");

                            switch (Select(pc, "Accept the [test]?", "", "No problem", "No need"))
                            {
                                case 1:
                                    JobBasic_03_mask.SetValue(JobBasic_03.Choose_to_become_a_scout, true);

                                    Say(pc, 11000017, 131, "Find a way to dodge monsters, $R;" +
                                                            "Go back here unharmed. $R;", "Scout Master");

                                    SetHomePoint(pc, 10035000, 64, 175);

                                    Warp(pc, 10035000, 64, 175);
                                    break;

                                case 2:
                                    Say(pc, 11000017, 131, "Don't waste my time..., $R;" +
                                                            "Think carefully about it later. $R;", "Scout Master");
                                    break;
                            }
                        }
                        else
                        {
                            Say(pc, 11000017, 131, "After the strength reaches 10, $R;" +
                                                    "Come to me again! $R;", "Scout Master");
                        }
                        return;

                    case 2:
                        Say(pc, 11000017, 131, "The profession of thieves is more suitable for $R;" +
                                                "The physique of the Emil and Dominion! $R;" +
                                                "$R I will explain it to you carefully. $R;", "Scout Master");

                        switch (Select(pc, "Do you still want to listen?", "", "I want to listen", "Don't listen"))
                        {
                            case 1:
                                Say(pc, 11000017, 131, "[Thieves] are $R who use all means to accomplish the task;" +
                                                        "Slient Warrior. $R;" +
                                                        "$R even if the situation is not good for you, $R;" +
                                                        "You can also use the dark night as a protective color, $R;" +
                                                        "Kill the enemy by surprise. $R;" +
                                                        "If the strength of $P is higher, $R;" +
                                                        "Unconsciously, $R;" +
                                                        "You can kill the enemies one by one. $R;" +
                                                        "$P is more suitable for people with strong observation skills. $R;", "Scout Master");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000017, 131, "If you want to be a [scout], let me introduce you to the mission! $R;", "Scout Master");
                        break;
                }

                selection = Select(pc, "What do you want to do?", "", "I want to be a [scout]!", "What kind of profession is a [scout]?", "Task Desk", "Do nothing");
            }
        }

        void Scout_transfer_task(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            if (!JobBasic_03_mask.Test(JobBasic_03.Scout_transfer_task_completed))
            {
                Return_safely_to_the_Scouts_Guild(pc);
            }

            if (JobBasic_03_mask.Test(JobBasic_03.Scout_transfer_task_completed) &&
                !JobBasic_03_mask.Test(JobBasic_03.scout_successfully_transferred))
            {
                Apply_for__a_job_as_a_scout(pc);
                return;
            }

        }

        void Return_safely_to_the_Scouts_Guild(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            Say(pc, 11000017, 131, "I'm back. $R;" +
                                    "$P...$R;" +
                                    "Thanks all the way. $R;" +
                                    "$R Scout's true random is to avoid fighting, you should have a deep understanding. $R;" +
                                    "Don't forget it! $R;" +
                                    "$P, do you want to change your job to a [Scout]? $R;", "Scout Master");

            switch (Select(pc, "Do you want to change to [Scout]?", "", "Change to [Scout]", "Forget it"))
            {
                case 1:
                    JobBasic_03_mask.SetValue(JobBasic_03.Scout_transfer_task_completed, true);
                    break;

                case 2:
                    Say(pc, 11000017, 131, "Consider again!$R;", "Scout Master");
                    break;
            }
        }

        void Apply_for__a_job_as_a_scout(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            Say(pc, 11000017, 131, "Then tattoo you $R, which symbolizes [scout];" +
                                    "『Rogue Emblem』! $R;", "Scout Master");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_03_mask.SetValue(JobBasic_03.scout_successfully_transferred, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000017, 131, "…$R;" +
                                        "$P is great, $R;" +
                                        "You have a beautiful coat of arms imprinted on your body. $R;" +
                                        "$R from now on, $R;" +
                                        "You will become our [scout] on behalf of us. $R;", "Scout Master");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SCOUT);

                Say(pc, 0, 0, "You have been transferred to a [scout]! $R;", "Scout Master");

                Say(pc, 11000017, 131, "Speak to me after putting on your clothes first. $R;" +
                                       "There is a small gift for you! $R;" +
                                       "$R, you go to pack your luggage first, then come to me. $R;", "Scout Master");
            }
            else
            {
                Say(pc, 11000017, 131, "The coat of arms is imprinted on the skin, $R;" +
                                       "Take off the equipment first. $R;", "Scout Master");
            }
        }

        void scout_transfer_completed(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_03_mask.SetValue(JobBasic_03.Has_been_transferred_to_a_scout, true);

                Say(pc, 11000017, 131, "Give you the [Secret Mask], $R;" +
                                        "$R, just practice with your heart. $R;", "Scout Master");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50040200, 1);
                Say(pc, 0, 0, "Get the [Secret Mask]! $R;", "Scout Master");

                LearnSkill(pc, 2042);
                Say(pc, 0, 0, "Learn to [invisibility]! $R;", "Scout Master");
            }
            else
            {
                Say(pc, 11000017, 131, "Speak to me after putting on your clothes first. $R;", "Scout Master");
            }
        }

        void Introduction_to_Advanced_transfer(ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];
            //EVT1100001761
            Say(pc, 131, "If it is you, it may be possible to succeed $R;" +
                 "How about $R, do you want to be an assassin? $R;", "Scout Master");
            //EVT1100001762
            switch (Select(pc, "What do you do?", "", "I want to be an [Assassin]!", "What kind of profession is an [Assassin]?", "Do nothing"))
            {
                case 1:
                    if (pc.JobLevel1 > 29)
                    {
                        Say(pc, 131, "Do you want to be an assassin? $R;" +
                            "$R first test your strength! $R;", "Scout Master");
                        switch (Select(pc, "Do you want to test your strength?", "", "Of course you must", "Forget it."))
                        {
                            case 1:
                                mask.SetValue(Job2X_03.Assassin_transfer_begins, true);
                                //_4A00 = true;
                                Say(pc, 131, "Hahahaha!! $R;" +
                                    "Why are you always so serious? $R;" +
                                    "$R don't worry, do it now! $R;" +
                                    "$P now look for four drug $R;" +
                                    "$P The people holding the Drug are everywhere on the acronia $R;" +
                                    "One of $P may be near the coast $R;" +
                                    "$P the other three, one in a cold place $R;" +
                                    "One in a hot place $R;" +
                                    "The last place of $R is on the western island. $R;" +
                                    "The important thing about $P is that even if you find someone who takes the Drug, $R;" +
                                    "But if you don't know the password, you still can't get the Drug $R for $R;" +
                                    "$P give you some hints $R;" +
                                    "$R prompt is [Sky]$R;" +
                                    "$P hehe, I don’t know who ordered $R for this prompt;" +
                                    "$P go now! $R;", "Scout Master");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "If you want to be an assassin, strength is very important. $R;" +
                        "$R first go to improve [strength], then come again. $R;" +
                        "I'll talk about the others later. $R;", "Scout Master");
                    break;
                case 2:
                    Say(pc, 131, "Like thieves, this profession is more suitable for $R;" +
                        "The physique of the Emil and Dominion tribe, $R;" +
                        "$R I will explain to you slowly...$R;", "Scout Master");
                    switch (Select(pc, "Listen to it?", "", "I want to listen", "Don't listen"))
                    {
                        case 1:
                            Say(pc, 131, "The assassin will never give up until he reaches the goal, $R;" +
                                "Is a tragic soldier, $R;" +
                                "$R assassins can hide themselves in the dark$R;" +
                                "Kill the enemy instantly $R;" +
                                "$P uses special props and poisons $R;" +
                                "Also has excellent talent $R;" +
                                "It is a profession that is more suitable for people with high insights $R;", "Scout Master");
                            //GOTO EVT1100001762
                            break;
                    }
                    break;
            }
        }

        void Advanced_transfer_complete(ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            if (mask.Test(Job2X_03.Defense_is_too_high) ||
                mask.Test(Job2X_03.Return_the_internal_drug_to_the_assassin))
            {
                switch (Select(pc, "Really change job?", "", "I want to be an assassin", "Forget it."))
                {
                    case 1:
                        Say(pc, 131, "Then I will brand you $R, which symbolizes the assassin;" +
                            "[Assassin's Emblem] $R;", "Scout Master");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            mask.SetValue(Job2X_03.End_of_transfer, true);
                            mask.SetValue(Job2X_03.Assassin_transfer_begins, false);
                            mask.SetValue(Job2X_03.The_first_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.The_second_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.The_third_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.The_fourth_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.Defense_is_too_high, false);
                            mask.SetValue(Job2X_03.Return_the_internal_drug_to_the_assassin, false);
                            ChangePlayerJob(pc, PC_JOB.ASSASSIN);
                            pc.JEXP = 0;
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P is great, $R;" +
                                "You have a beautiful coat of arms imprinted on your body. $R;" +
                                "$R from now on, $R you will become our [Assassin] on behalf of us. $R;", "Scout Master");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "You have been transferred to [Assassin]. $R;", "Scout Master");
                            Say(pc, 131, "Now put on your clothes first $R;" +
                                "$P looks forward to you doing something in the future $R;", "Scout Master");
                            return;
                        }
                        mask.SetValue(Job2X_03.Defense_is_too_high, true);
                        Say(pc, 131, "If the defense is too high, the coat of arms will not be branded $R;" +
                            "Please change into light clothes and come again. $R;", "Scout Master");
                        break;
                    case 2:
                        Say(pc, 131, "Are you confused about the future? $R;" +
                            "$R stabilize your mind first, and come to me after you calm down. $R;", "Scout Master");
                        break;
                }
                return;
            }

            if (CountItem(pc, 10000309) >= 1 &&
                CountItem(pc, 10000350) >= 1 &&
                CountItem(pc, 10000351) >= 1 &&
                CountItem(pc, 10000352) >= 1)
            {
                mask.SetValue(Job2X_03.Return_the_internal_drug_to_the_assassin, true);
                //_4A25 = true;
                Say(pc, 131, "Come on $R;" +
                    "$R looks fine. $R;" +
                    "$P...$R;" +
                    "$P everything went well, it's great! $R;" +
                    "$R, you seem to have taken the [Assassin Water] back, $R;" +
                    "Right? $R;", "Scout Master");
                TakeItem(pc, 10000309, 1);
                TakeItem(pc, 10000350, 1);
                TakeItem(pc, 10000351, 1);
                TakeItem(pc, 10000352, 1);
                Say(pc, 131, "Hand over [Assassin Water 1] $R;" +
                    "Hand over [Assassin Water 2] $R;" +
                    "Hand over [Assassin Water 3] $R;" +
                    "Hand over [Assassin Water 4] $R;", "Scout Master");
                Say(pc, 131, "Is it too simple? $R;" +
                    "$P You can survive under any conditions $R;" +
                    "$R looks like you are qualified to be an assassin $R;" +
                    "Does $P really want to be an assassin? $R;", "Scout Master");
                //EVT1100001774
                switch (Select(pc, "Really change job?", "", "I want to be an assassin", "Forget it."))
                {
                    case 1:
                        Say(pc, 131, "Then I will brand you $R, which symbolizes the assassin;" +
                            "[Assassin's Emblem] $R;", "Scout Master");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            mask.SetValue(Job2X_03.End_of_transfer, true);
                            mask.SetValue(Job2X_03.Assassin_transfer_begins, false);
                            mask.SetValue(Job2X_03.The_first_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.The_second_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.The_third_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.The_fourth_question_is_answered_correctly, false);
                            mask.SetValue(Job2X_03.Defense_is_too_high, false);
                            mask.SetValue(Job2X_03.Return_the_internal_drug_to_the_assassin, false);
                            //_4A10 = true;
                            //_4A00 = false;
                            //_4A01 = false;
                            //_4A02 = false;
                            //_4A03 = false;
                            //_4A08 = false;
                            //_4A09 = false;
                            //_4A25 = false;
                            ChangePlayerJob(pc, PC_JOB.ASSASSIN);
                            pc.JEXP = 0;
                            //PARAM ME.JOB = 23
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P is great, $R;" +
                                "You have a beautiful coat of arms imprinted on your body. $R;" +
                                "$R from now on, $R you will become our [Assassin] on behalf of us. $R;", "Scout Master");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "You have been transferred to [Assassin]. $R;", "Scout Master");
                            Say(pc, 131, "Now put on your clothes first $R;" +
                                "$P looks forward to you doing something in the future $R;", "Scout Master");
                            return;
                        }
                        mask.SetValue(Job2X_03.Defense_is_too_high, true);
                        //_4A09 = true;
                        Say(pc, 131, "If the defense is too high, the coat of arms will not be branded $R;" +
                            "Please change into light clothes and come again. $R;", "Scout Master");
                        break;
                    case 2:
                        Say(pc, 131, "Are you confused about the future? $R;" +
                            "$R stabilize your mind first, and come to me after you calm down. $R;", "Scout Master");
                        break;
                }
                return;
            }
            Say(pc, 131, "Huh? $R;" +
                             "Isn't all collected yet? $R;" +
                             "Don't think about going back until $R is all set! $R;", "Scout Master");
        }
    }
}
