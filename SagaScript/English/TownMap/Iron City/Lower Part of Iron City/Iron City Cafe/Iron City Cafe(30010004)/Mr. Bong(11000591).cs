using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Quests;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010004
{
    public class S11000591 : Event
    {
        public S11000591()
        {
            this.EventID = 11000591;
            this.alreadyHasQuest = "Did the task go well? $R;";
            this.gotNormalQuest = "Then please $R;" +
                "$R wait for the end of the task, come to me again;";
            this.questCompleted = "It's really hard work $R;" +
                "$R mission is successful! Come on! Get paid!;";
            this.questCanceled = "Well... if it's you, I believe you can do it $R;" +
                "I am looking forward to...;";
            this.questFailed = "Failed $R;" +
                "Your ability is just like this? $R;";
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "Um...$R;" +
                "$R has nothing special to ask for now $R;" +
                "How about taking another risk? $R;";
            this.questTooEasy = "Um...but for you $R;" +
                "Maybe it is too simple $R;" +
                "Is it okay with $R? $R;";
        }

        /*
        public override void OnQuestUpdate(ActorPC pc, Quest quest)
        {
            if (pc.Quest.ID == 10031000 && pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
            {
                HandleQuest(pc, 23);
                return;
            }
        }
        */

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];
            BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];


            if (CountItem(pc, 10020600) >= 1)
            {
                Say(pc, 131, "Hold the certificate $R;" +
                     "Just go back to Acropolis $R;" +
                     "$R worked hard $R;");
                return;
            }
            
            if (Job2X_01_mask.Test(Job2X_01.Transfer_completed) || Job2X_02_mask.Test(Job2X_02.Transfer_completed))//_3A37 || _3A38)
            {
                Say(pc, 131, "What? $R;" +
                      "Want to get my task? $R;");
                Say(pc, 131, "My task $R;" +
                    "$R for [Team];" +
                    "If there is someone in the $R team $R;" +
                    "If you get the same task $R;" +
                    "The number of monsters that can be repelled by sharing $R;");

                switch (Select(pc, "What to do?", "", "Task", "Do nothing"))
                {
                    case 1:
                        //HandleQuest(pc, 23);
                        break;

                    case 2:
                        break;
                }
                return;
            }
            
            if (Job2X_01_mask.Test(Job2X_01.Give_black_vinegar) || Job2X_02_mask.Test(Job2X_02.Give_ice_can))//_3A36)
            {
                Say(pc, 131, "My task $R;" +
                     "$R for [Team];" +
                     "If there is someone in the $R team $R;" +
                     "If you get the same task $R;" +
                     "The number of monsters that can be repelled by sharing $R;");

                switch (Select(pc, "What to do?", "", "Task", "Do nothing"))
                {
                    case 1:
                        HandleQuest(pc, 23);
                        break;

                    case 2:
                        break;
                }
                return;
            }

            if (Job2X_01_mask.Test(Job2X_01.Collect_black_vinegar))//_3A34)
            {

                if (CountItem(pc, 10033910) >= 1)
                {
                    TakeItem(pc, 10033910, 1);
                    Say(pc, 131, "Hehe, did you bring it? $R;" +
                         "Now you can use $R;" +
                         "$P wants to write you a certification letter $R;" +
                         "Wait a minute...$R;" +
                         "$P...$R;" +
                         "$P...$R;" +
                         "$P Oh, forget it! $R;" +
                         "Give you the certificate $R;" +
                         "Report to me $R;" +
                         "Please confirm $R;" +
                         "$P will give you a task $R;" +
                         "Can you say it again? $R;");
                    Job2X_01_mask.SetValue(Job2X_01.Give_black_vinegar, true);
                    //_3A36 = true;
                    return;
                }

                Say(pc, 131, "Is [Black Vinegar] not ready yet? $R;");
                return;
            }
            
            if (Job2X_02_mask.Test(Job2X_02.Collect_ice_cans))//_3A35)
            {

                if (CountItem(pc, 10033904) >= 1)
                {
                    TakeItem(pc, 10033904, 1);
                    Say(pc, 131, "Got it? $R;" +
                         "Now relax $R;" +
                         "$P wants to write you a certification letter $R;" +
                         "Wait a minute...$R;" +
                         "$P...$R;" +
                         "$P...$R;" +
                         "$P Oh, forget it! $R;" +
                         "Give you the certificate $R;" +
                         "Report to me $R;" +
                         "Please confirm $R;" +
                         "$P will give you a task $R;" +
                         "Say it again, okay? $R;");
                    Job2X_02_mask.SetValue(Job2X_02.Give_ice_can, true);
                    //_3A36 = true;
                    return;
                }

                Say(pc, 131, "Is [Canned Ice] not ready yet? $R;");
                return;
            }

            if (Job2X_01_mask.Test(Job2X_01.Advanced_transfer_starts))//_3A32)
            {
                Say(pc, 131, "Hehe, $R;" +
                    "Want to get a certificate from me $R;" +
                    "Become a [Blademaster]? $R;");

                switch (Select(pc, "Do you want to be a light warrior?", "", "What is a light warrior?", "Well, I want to be a light warrior", "don't"))
                {
                    case 1:
                        Say(pc, 131, "[Blademaster] is more aggressive than swordsman $R;" +
                            "It's a profession that makes perfect skills with weapons, $R;" +
                            "If you change your job to become [Blademaster], $R;" +
                            "The $R you have accumulated as a swordsman;" +
                            "Professional LV will return to [1]$R;" +
                            "You have to think about it $R;");
                        break;

                    case 2:
                        Say(pc, 131, "You have to bring [black vinegar] to write it $R;" +
                             "Otherwise, your hands keep shaking and you can't write the certificate $R;" +
                             "$R then, please $R;");
                        Job2X_01_mask.SetValue(Job2X_01.Collect_black_vinegar, true);
                        //_3A34 = true;
                        break;

                    case 3:
                        Say(pc, 131, "Really? $R;");
                        break;
                }
                return;
            }
            
            if (Job2X_02_mask.Test(Job2X_02.Advanced_transfer_start))//_3A33)
            {
                Say(pc, 131, "Hehe! $R;" +
                    "Want to get a certificate from me $R;" +
                    "Become a [Knight]? $R;");

                switch (Select(pc, "Do you want to be a Knight?", "", "What is a Knight...?", "Well, I want to be a Knight", "Don't"))
                {
                    case 1:
                        Say(pc, 131, "The defensive power of [Knight] is much stronger than that of knights. $R;" +
                            "It's an outstanding career guarding our army. $R;" +
                            "If you change your job to become a [Knight], $R;" +
                            "The $R you have accumulated as a fancer;" +
                            "Professional LV will return to [1]$R;" +
                            "You have to think about it $R;");
                        break;

                    case 2:
                        Say(pc, 131, "Then can I bring [canned ice]? $R;" +
                            "Otherwise it's too hot to write the certificate $R;" +
                            "$R then, please $R;");
                        Job2X_02_mask.SetValue(Job2X_02.Collect_ice_cans, true);
                        //_3A35 = true;
                        break;

                    case 3:
                        Say(pc, 131, "…$R;");
                        break;
                }
                return;
            }

            Say(pc, 131, "This is still the best $R;" +
                 "Hehe$R;");

        }
    }
}
