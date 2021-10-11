using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:弓手行會(30043000) NPC基本信息:弓手總管(11000018) X:3 Y:3
namespace SagaScript.M30043000
{
    public class S11000018 : Event
    {
        public S11000018()
        {
            this.EventID = 11000018;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            Say(pc, 11000018, 131, "This is the Guild of Archers...$R;" +
                                    "Haha! $R;", "Archer Master");

            if (JobBasic_04_mask.Test(JobBasic_04.Archer_changed_job_successfully) &&
                !JobBasic_04_mask.Test(JobBasic_04.Has_been_converted_to_archer))
            {
                Archer_transfer_completed(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
            {
                if (JobBasic_04_mask.Test(JobBasic_04.Choose_to_become_an_archer) &&
                    !JobBasic_04_mask.Test(JobBasic_04.Has_been_converted_to_archer))
                {
                    Archer_transfer_task(pc);
                    return;
                }
                else
                {
                    Introduction_to_Archers(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.ARCHER)
            {
                Say(pc, 131, pc.Name + "?$R;" +
                     "$R Haha $R;" +
                     "How are you doing? $R;");
                switch (Select(pc, "What do you do?", "", "I want to change jobs!", "Listen to adventurous opinions", "Buy something", "Sell entry permit", "Do nothing"))
                {
                    case 1:
                        Advanced_transfer(pc);
                        break;

                    case 2:
                        Say(pc, 131, "Isn’t the arrow hit rate very high? $R;" +
                             "In the case of $R, click on the monster and then $R;" +
                             "Try again. $R;" +
                             "$P if the red power meter is full, $R;" +
                             "The hit rate will increase $R;" +
                             "$R must try it $R;");
                        break;

                    case 3:
                        OpenShopBuy(pc, 67);
                        break;


                    case 4:
                        OpenShopBuy(pc, 105);
                        break;
                    case 5:
                        break;
                }
            }
        }

        void Introduction_to_Archers(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            int selection;

            Say(pc, 11000018, 131, "I am the master archer who manages the archers. $R;" +
                                    "$P Huh, are you a novice? $R;" +
                                    "$R, do you want to be an [archer]? $R;" +
                                    "Listen to my explanation first. $R;", "Archer Master");

            selection = Select(pc, "What do you want to do?", "", "I want to be an [archer]!", "What kind of profession is an [archer]?", "Task Desk", "Do nothing");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000018, 131, "Want to be an [archer]? $R;" +
                                               "$ like this, $R;" +
                                               "Then try to make a [self - made arrow]. $R;" +
                                               "$P let me know that you have the talent to become an [archer]. $R;", "Archer Master");

                        switch (Select(pc, "Accept it?", "", "No problem", "No problem"))
                        {
                            case 1:
                                JobBasic_04_mask.SetValue(JobBasic_04.Choose_to_become_an_archer, true);

                                Say(pc, 11000018, 131, "The material is one [ChockKo's Wings] + 1 [Branch]. $R;" +
                                                       "Look for a [Weaponsmith] to make [Arrows by Yourself], $R;" +
                                                       "Take me an [Arrow made by yourself]. $R;", "Archer Master");
                                break;

                            case 2:
                                Say(pc, 11000018, 131, "Really? $R;" +
                                                       "$R [archer] is a must to make the weapon you need. $R;", "Archer Master");
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000018, 131, "The profession of archer is more suitable for $R;" +
                                               "Emil and Dominion! $R;" +
                                               "$R, do you want to listen to it? $R;", "Archer Master");

                        switch (Select(pc, "Do you still want to listen?", "", "I want to listen", "Don't listen"))
                        {
                            case 1:
                                Say(pc, 11000018, 131, "[Archer] is a profession that uses arrows. $R;" +
                                                       "$R is good at long-range attacks, $R;" +
                                                       "So basically it is impossible to get hurt. $R;" +
                                                       "$P on the contrary, if you fight in close quarters, $R;" +
                                                       "It's not very popular. $R;" +
                                                       "$P but can become $R in the future;" +
                                                       "How about the [marksman] who uses a pistol! $R;" +
                                                       "$R, so now I have to bear it! $R;" +
                                                       "$P Archer's Guild is not like other professional clubs introducing missions. $R;" +
                                                       "$R so if you are looking for a job, $R;" +
                                                       "I'm going to [Cafe] $R;" +
                                                       "Or become the guard of the production department, $R;" +
                                                       "Come earn rewards! $R;", "Archer Master");
                                break;
                                
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000018, 131, "Become an [Archer] and I will introduce you to the mission. $R;", "Archer Master");
                        break;

                    case 4:
                        break;
                }

                selection = Select(pc, "What do you want to do?", "", "I want to be an [archer]", "What kind of profession is an [archer]?", "Task Desk", "Do nothing");
            } 
        }

        void Archer_transfer_task(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            if (!JobBasic_04_mask.Test(JobBasic_04.Archer_transfer_mission_completed))
            {
                Give_yourself_an_arrow(pc);
            }

            if (JobBasic_04_mask.Test(JobBasic_04.Archer_transfer_mission_completed) &&
                !JobBasic_04_mask.Test(JobBasic_04.Archer_changed_job_successfully))
            {
                Apply_to_become_an_archer(pc);
                return;
            }
        }

        void Give_yourself_an_arrow(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            if (CountItem(pc, 10026401) > 0)
            {
                Say(pc, 11000018, 131, "It is indeed an arrow made by yourself. $R;" +
                                        "$R, you are really amazing! $R;" +
                                        "$P I am looking forward to your future. $R;" +
                                        "$R Now that you have achieved the mission, $R;" +
                                        "From now on, you are the [Archer]. $R;", "Archer Master");

                switch (Select(pc, "Do you want to change to [Archer]?", "", "Change to [Archer]", "Forget it"))
                {
                    case 1:
                        JobBasic_04_mask.SetValue(JobBasic_04.Archer_transfer_mission_completed, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10026401, 1);
                        Say(pc, 0, 0, "Hand over the [I made the arrow]! $R;", "");
                        break;

                    case 2:
                        Say(pc, 11000018, 131, "If your mind changes, come and talk to me again. $R;", "Archer Master");
                        break;
                }
            }
            else
            {
                Say(pc, 11000018, 131, "The material is one [Cocko Wing] + 1 [Branch]. $R;" +
                                        "Look for a [Weaponsmith] to make [Arrows by Yourself], $R;" +
                                        "Take me an [Arrow made by yourself]. $R;", "Archer Master");
            }
        }

        void Apply_to_become_an_archer(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            Say(pc, 11000018, 131, "Then! I will give you the $R symbolizing [Archer];" +
                                     "[Archer's Emblem]! $R;", "Archer Master");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_04_mask.SetValue(JobBasic_04.Archer_changed_job_successfully, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000018, 131, "…$R;" +
                                       "$P is great! $R;" +
                                       "You have a beautiful coat of arms imprinted on your body. $R;" +
                                       "$R from now on, $R;" +
                                       "You will become our [archer] on behalf of us. $R;", "Archer Master");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.ARCHER);

                Say(pc, 0, 0, "You have been transferred to [Archer]! $R;", "");

                Say(pc, 11000018, 131, "There is a small gift for you! $R;" +
                                       "After putting on your clothes, talk to me again. $R;" +
                                       "$R and don't forget to pack your luggage! $R;", "Archer Master");
            }
            else
            {
                Say(pc, 11000018, 131, "The coat of arms is imprinted on the skin, $R;" +
                                       "Take off the equipment first. $R;", "Archer Master");
            }
        }

        void Archer_transfer_completed(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_04_mask.SetValue(JobBasic_04.Has_been_converted_to_archer, true);

                Say(pc, 11000018, 131, "This is the [practice bow] and [waist quiver] for you. $R;" +
                                        "$R, you must do your best! $R;", "Archer Master");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 60090050, 1);
                GiveItem(pc, 50070400, 1);
                Say(pc, 0, 0, "Get [practice bow] and [waist quiver]! $R;", "");

                LearnSkill(pc, 2035);
                Say(pc, 0, 0, "Learned [Throwing Weapon Making]! R;", "");
            }
            else
            {
                Say(pc, 11000018, 131, "Speak to me after you put on your clothes.", "The Archer");
            }
        }

        void Advanced_transfer(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (CountItem(pc, 10020751) >= 1 && Job2X_04_mask.Test(Job2X_04.Advanced_transfer_start) && pc.Job == PC_JOB.ARCHER)
            {
                Say(pc, 131, "Come on, how is the exam? $R;" +
                     "$P haha! It's the [Hunter Certification]. $R;" +
                     "$R from now on, $R you will become the envy of everyone [hunter] $R;");
                Advanced_transfer_selection(pc);
                return;
            }

            if (Job2X_04_mask.Test(Job2X_04.Advanced_transfer_start) && pc.Job == PC_JOB.ARCHER)
            {
                Say(pc, 131, "Striker’s transfer test is in $R;" +
                     "[Acronia Eastern Beach]. $R;" +
                     "$R to the caravan tent near the east coast $R;" +
                     "Find [Lady Pamela], $R;" +
                     "Take the transfer test with her. $R;" +
                     "$P get the [License (Quest)] from her $R;" +
                     "Just admit that you are a [Striker]. $R;");
                return;
            }

            if (pc.JobLevel1 > 29 && pc.Job == PC_JOB.ARCHER)
            {
                Job2X_04_mask.SetValue(Job2X_04.Advanced_transfer_start, true);
                //_3a55 = true;
                Say(pc, 131, "You finally meet the conditions for challenging advanced occupations $R;" +
                     "$P is right. $R;" +
                     "$R is also transferred from [Archer] to [Striker]. $R;");
                return;
            }

            Say(pc, 131, "No, $R;" +
                 "With your strength, it is too reluctant to transfer, $R;" +
                 "Let’s get experience first. $R;");
        }

        void Advanced_transfer_selection(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            switch (Select(pc, "Really change job?", "", "I want to be a Striker", "Listen to the notes about Striker", "Forget it"))
            {
                case 1:
                    Say(pc, 131, "Then imprint this $R symbolizing Striker for you;" +
                        "『Striker Emblem』$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        TakeItem(pc, 10020751, 1);
                        ChangePlayerJob(pc, PC_JOB.STRIKER);
                        pc.JEXP = 0;
                        //PARAM ME.JOB = 33
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "…$R;" +
                             "$P is great, $R;" +
                             "You have a beautiful coat of arms imprinted on your body. $R;" +
                             "$R from now on, $R you will become our [Striker] on behalf of us. $R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "You have been transferred to [Striker]. $R;");
                        Job2X_04_mask.SetValue(Job2X_04.Advanced_transfer_ends, true);
                        return;
                    }
                    Say(pc, 131, "…$R;" +
                        "If the defense is too high, the coat of arms will not be branded $R;" +
                        "Please change into light clothes and come again. $R;");
                    break;
                case 2:
                    Say(pc, 131, "I must tell you clearly first. $R;" +
                        "If you become a [Striker], $R professional LV will become 1. $R;" +
                        "The skills of the $P archer can also be learned after the transfer. $R;" +
                        "$R, but there is one thing to pay attention to, $R you have to listen carefully. $R;" +
                        "$P [Skill Points] is completely separate from the class level $R. $R;" +
                        "$R Skill points gained when learning archer skills $R;" +
                        "Only when the profession is an archer can you accumulate $R;" +
                        "Although $P will not disappear after transfer, $R;" +
                        "But the archer skill points will no longer increase $R;" +
                        "$P is the same as skill points, $R;" +
                        "The archer's skill learning level $R will not rise after the transfer. $R;" +
                        "$P means that $R is in addition to the skills you are learning now, $R;" +
                        "I can't learn later. $R;" +
                        "If $R still has skills that you want to learn, $R should change your job after you finish learning $R;");
                    Advanced_transfer_selection(pc);
                    break;
                case 3:
                    break;
            }
        }
    }
}
