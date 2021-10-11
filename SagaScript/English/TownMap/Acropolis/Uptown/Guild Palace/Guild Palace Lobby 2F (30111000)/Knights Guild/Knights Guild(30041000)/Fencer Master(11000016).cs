using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:騎士行會(30041000) NPC基本信息:騎士總管(11000016) X:3 Y:3
namespace SagaScript.M30041000
{
    public class S11000016 : Event
    {
        public S11000016()
        {
            this.EventID = 11000016;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            Say(pc, 11000016, 131, "Welcome to the Fencer Guild. $R;", "Fencer Master");

            if (JobBasic_02_mask.Test(JobBasic_02.Fencer_transferred_successfully) &&
                !JobBasic_02_mask.Test(JobBasic_02.Has_been_transferred_to_Cavaliers))
            {
                Knight_transfer_completed(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
            {
                if (JobBasic_02_mask.Test(JobBasic_02.Choosing_to_become_a_fancer) &&
                    !JobBasic_02_mask.Test(JobBasic_02.Has_been_transferred_to_Cavaliers))
                {
                    Fancer_transfer_task(pc);
                    return;
                }
                else
                {
                    FancerIntroduction(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.FENCER)
            {
                Say(pc, 11000016, 131, pc.Name + "啊~!$R;" +
                                        "$R Long time no see! $R;", "Fancer Master");
                switch (Select(pc, "What do you want to do?", "", "Sell entry permit", "I want to change jobs", "Do nothing"))
                {
                    case 1:
                        OpenShopBuy(pc, 105);
                        break;
                    case 2:
                        Advanced_transfer(pc);
                        break;
                    case 3:
                        break;
                }
            }
        }

        void FancerIntroduction(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            int selection;

            Say(pc, 11000016, 131, "I am the Fancer Master who manages the fancer. $R;" +
                                    "$R, you are still a novice! $R;" +
                                    "Hehe...$R;" +
                                    "$P if you don’t have a profession you want to do, $R;" +
                                    "Do you want to be a fancer? $R;" +
                                    "$R listen to my opinion first! $R;", "Fancer Master");

            selection = Select(pc, "What do you want to do?", "", "I want to be a [Fancer]!", "What kind of profession is [Fancer]?", "Task Desk", "Do nothing");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000016, 131, "Want to be a [Fancer]? $R;" +
                                                 "I'm glad you said that. $R;" +
                                                 "$R but do you really have this ability? $R;" +
                                                 "Test your strength first! $R;", "Fancer Master");

                        switch (Select(pc, "Accept the [test]?", "", "No problem", "No need"))
                        {
                            case 1:
                                JobBasic_02_mask.SetValue(JobBasic_02.Choosing_to_become_a_fancer, true);

                                Say(pc, 11000016, 131, "There is a monster to the west of here, named [Killer Bee]. $R;" +
                                                         "It looks like a bee. $R;" +
                                                         "The task of $R is to kill the Killer Bee. $R;" +
                                                         "$P, don't forget!! $R;" +
                                                         "Also get the [Bee Sting] $R;" +
 
                                                         "As evidence of defeat! $R;" +
                                                         "$P If you can really defeat the [Killer Bee], $R;" +
                                                         "So you can become a fancer. $R;", "Fancer Master");
                                break;

                            case 2:
                                Say(pc, 11000016, 131, "Forget it..., $R;" +
                                                         "Think about it again. $R;", "Fancer Master");
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000016, 131, "[Fancer] is a more suitable class for $R;" +
                                               "The physique of the Emil and Dominion! $R;" +
                                               "$R continue to listen? $R;", "Fancer Master");

                        switch (Select(pc, "Do you still want to listen?", "", "I want to listen", "Don't listen"))
                        {
                            case 1:
                                Say(pc, 11000016, 131, "[Fancer] are mainly gorgeous warriors who use rapiers and spear. $R;" +
                                                       "$R Fancer’s sharp stab attack is $R;" +
                                                       "No one can avoid it. $R;" +
                                                       "It's a pity that $P has relatively low collection and handling capabilities, $R;" +
                                                       "Not suitable for one person to act alone. $R;" +
                                                       "$P fancer whose heart is infected by dark power, $R;" +
                                                       "I heard that other powers can be mastered. $R;" +
                                                       "$P Fancer's Guild, it is not yet open to accept missions. $R;" +
                                                       "$R So if you are looking for a job, go to the [Cafe] $R;" +
                                                       "Or become the guard of the producer, $R;" +
                                                       "Come and earn rewards! $R;", "Fancer Master");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000016, 131, "Wait for you to become a glorious [Fancer], and then find me to ration your job. $R;", "Fancer Master");
                        break;

                    case 4:
                        break;
                }

                selection = Select(pc, "What do you want to do?", "", "I want to be a [Fancer]!", "What kind of profession is [Fancer]?", "Task Desk", "Do nothing");
            } 
        }

        void Fancer_transfer_task(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            if (!JobBasic_02_mask.Test(JobBasic_02.Fancer_transfer_task_completed))
            {
                Poisonous_needle_given_to_bees(pc);
            }

            if (JobBasic_02_mask.Test(JobBasic_02.Fancer_transfer_task_completed) &&
                !JobBasic_02_mask.Test(JobBasic_02.Fencer_transferred_successfully))
            {
                Apply_for_conversion_to_Knight(pc);
                return;
            }
        }

        void Poisonous_needle_given_to_bees(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            if (CountItem(pc, 10035200) > 0)
            {
                Say(pc, 11000016, 131, "Wow! It's really a [Bee Sting]. $R;" +
                                        "You are really amazing. $R;" +
                                        "$R I am looking forward to your future. $R;" +
                                        "$P Now that you have achieved the mission, $R;" +
                                        "From now on, you are the [Fancer]! $R;", "Knight Manager");

                switch (Select(pc, "Do you want to change to [Fancer]?", "", "Change to [Fancer]", "Forget it"))
                {
                    case 1:
                        JobBasic_02_mask.SetValue(JobBasic_02.Fancer_transfer_task_completed, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10035200, 1);
                        Say(pc, 0, 0, "Handed over the [Bee Sting]. $R;", "");
                        break;

                    case 2:
                        Say(pc, 11000016, 131, "Don't you want to be a knight? $R;" +
                                               "$R hey! $R;" +
                                               "Is there a time like this? $R;" +
                                               "$P can't help it, $R;" +
                                               "If your mind changes, come and talk to me again. $R;", "Fancer Master");
                        break;
                }
            }
            else
            {
                Say(pc, 11000016, 131, "There is a monster to the west of here, named [Killer Bee]. $R;" +
                                       "It looks like a bee. $R;" +
                                       "The task of $R is to kill the Killer Bee. $R;" +
                                       "$P, don't forget!! $R;" +
                                       "Also get the [Bee Sting] $R;" +
                                       "As evidence of defeat! $R;" +
                                       "$P If you can really defeat the [Killer Bee], $R;" +
                                       "So you can become a knight. $R;", "Fancer Master");
            }
        }

        void Apply_for_conversion_to_Knight(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            Say(pc, 11000016, 131, "Then I will give you the $R symbolizing [Fancer];" +
                                    "[Fancer's coat of arms]. $R;", "Fancer Master");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_02_mask.SetValue(JobBasic_02.Fencer_transferred_successfully, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000016, 131, "…$R;" +
                                        "$P is great! $R;" +
                                        "You have a beautiful coat of arms imprinted on your body. $R;" +
                                        "$R from now on, $R;" +
                                        "You will become our [Fancer] on behalf of us. $R;", "Fancer Master");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.FENCER);

                Say(pc, 0, 0, "You have been transferred to [Knight]! $R;", "");

                Say(pc, 11000016, 131, "There is a small gift for you! $R;" +
                                       "After putting on your clothes, talk to me again. $R;" +
                                       "$R and don’t forget to pack your luggage! $R;", "Fancer Master");
            }
            else
            {
                Say(pc, 11000016, 131, "The coat of arms is imprinted on the skin, $R;" +
                                       "Take off the equipment first. $R;", "Fancer Master");
            }
        }

        void Knight_transfer_completed(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_02_mask.SetValue(JobBasic_02.Has_been_transferred_to_Cavaliers, true);
                Say(pc, 11000016, 131, "This is the [Phantom Mask] $R;" +
                                        "It is a face accessory that only a knight can wear. $R;" +
                                        "$R, you must cherish it! $R;", "Fancer Master");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50040400, 1);
                Say(pc, 0, 0, "Get [Phantom Mask]! $R;", "");

                LearnSkill(pc, 2138);
                Say(pc, 0, 0, "Learned [Flying Spear]! R;", "");
            }
            else
            {
                Say(pc, 11000016, 131, "Speak to me after putting on your clothes first.", "Fancer Master");
            }
        }

        void Advanced_transfer(ActorPC pc)
        {
            BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];

            if (Job2X_02_mask.Test(Job2X_02.Transfer_completed))//_3A38)
            {

                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "Really! $R;" +
                          "Please dress neatly! $R;");
                    return;
                }

                Say(pc, 131, "It's still not good with your strength, $R;" +
                    "It's better to accumulate more experience. $R;");
                return;
            }

            if (pc.Job == PC_JOB.FENCER && pc.JobLevel1 > 29)
            {

                if (CountItem(pc, 10020600) >= 1)
                {
                    Say(pc, 131, "Very good, now that you have obtained the certification, $R will let you change your job. $R;" +
                          "$R from now on, $R you will become the envy of everyone [Knight] $R;");
                    Advanced_transfer_selection(pc);
                    return;
                }

                if (Job2X_02_mask.Test(Job2X_02.Advanced_transfer_start))//_3A33)
                {
                    Say(pc, 131, "Just go to the $R [Mr. Bong] in [Iron City] $R;" +
                          "If you get the certificate, $R;" +
                          "Just admit that you are a [Knight]. $R;");
                    return;
                }

                Say(pc, 131, "Haha, you have grown a lot $R;" +
                     "$R I think it's time for you to start from [Fancer] $R;" +
                     "Transfer to [Knight], right? $R;");

                Say(pc, 131, "Just go to the $R [Mr. Bong] in [Iron City] $R;" +
                    "If you get the certificate, $R;" +
                    "Just admit that you are a [Knight]. $R;");
                Job2X_02_mask.SetValue(Job2X_02.Advanced_transfer_start, true);
                //_3A33 = true;
                return;
            }

            if (pc.Inventory.Equipments.Count == 0)
            {
                Say(pc, 131, "Really! $R;" +
                     "Please dress neatly! $R;");
                return;
            }

            Say(pc, 131, "It's still not good with your strength, $R;" +
                "It's better to accumulate more experience. $R;");
        }

        void Advanced_transfer_selection(ActorPC pc)
        {
            BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];

            switch (Select(pc, "Really change job?", "", "I want to be a Knight", "Listen to the notes about Knight", "Forget it"))
            {
                case 1:
                    Say(pc, 131, "Then I will imprint the $R which symbolizes the Knight;" +
                        "[Knight Crest] $R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        Say(pc, 131, "I will confirm with you one last time, $R;" +
                            "Did you really decide to change your job? $R;");

                        switch (Select(pc, "Really change job?", "", "Change job", "Don't change job"))
                        {
                            case 1:
                                TakeItem(pc, 10020600, 1);
                                Job2X_02_mask.SetValue(Job2X_02.Transfer_completed, true);
                                //_3A38 = true;
                                ChangePlayerJob(pc, PC_JOB.KNIGHT);
                                pc.JEXP = 0;
                                //PARAM ME.JOB = 13
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 4000);
                                Say(pc, 131, "…$R;" +
                                     "$P haha, you have a beautiful coat of arms imprinted on your body. $R;" +
                                     "$R from now on, $R you will become our [Knight] on behalf of us. $R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "You have been transferred to [Knight]. $R;");
                                break;

                            case 2:
                                Say(pc, 131, "Have you never thought about becoming a Knight? $R;" +
                                    "$R...$R;" +
                                    "Okay, then you can think about it. $R;");
                                break;
                        }
                        return;
                    }

                    Say(pc, 131, "If the defense is too high, the coat of arms will not be branded $R;" +
                          "Please change into light clothes and come again. $R;");
                    break;

                case 2:
                    Say(pc, 131, "If you become a [Knight], $R professional LV will become 1.$R;" +
                        "But the $R you had before the transfer;" +
                        "$R skills and skill points will not disappear. $R;" +
                        "$P also has skills that cannot be learned before the transfer, $R;" +
                        "You can't learn after the transfer. $R;" +
                        "For example, if the occupation level is 30, if you change your job, $R;" +
                        "$R Skills above 30 level before transfer $R;" +
                        "You can't learn, please pay attention. $R;" +
                        "Think carefully before changing your job for $P! $R;");

                    Advanced_transfer_selection(pc);
                    break;

                case 3:
                    Say(pc, 131, "Have you never thought about becoming a Knight? $R;" +
                         "$R...$R;" +
                         "Okay, then you can think about it. $R;");
                    break;
            }
        }
    }
}
