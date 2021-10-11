using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:劍士行會(30040000) NPC基本信息:劍士總管(11000015) X:3 Y:3
namespace SagaScript.M30040000
{
    public class S11000015 : Event
    {
        public S11000015()
        {
            this.EventID = 11000015;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            Say(pc, 11000015, 131, "Welcome to the Swordsman's Guild. $R;", "Swordsman Master");

            if (JobBasic_01_mask.Test(JobBasic_01.Swordsman_changed_job_successfully) &&
                !JobBasic_01_mask.Test(JobBasic_01.Has_been_transferred_to_a_swordsman))
            {
                Swordsman_transfer_completed(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE )
            {
                if (JobBasic_01_mask.Test(JobBasic_01.Choose_to_become_a_swordsman) &&
                    !JobBasic_01_mask.Test(JobBasic_01.Has_been_transferred_to_a_swordsman))
                {
                    Swordsman_transfer_task(pc);
                    return;
                }
                else
                {
                    Swordsman_introduction(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.SWORDMAN)
            {
                Say(pc, 11000015, 131, "This is not" + pc.Name + "Is it?!$R;" +
                                         "$R came well, $R;" +
                                         "What can I do today? $R;", "Swordsman Master");

                switch (Select(pc, "What should I do?", "", "Task Service Desk", "I want to change jobs", "Sell Entry Permit", "Do nothing"))
                {
                    case 1:
                        Say(pc, 0, 0, "$R has not been implemented yet;", "");
                        break;
                    case 2:
                        Advanced_transfer(pc);
                        break;
                    case 3:
                        OpenShopBuy(pc, 105);
                        break;
                    case 4:
                        break;
                }
            }
        }

        void Swordsman_introduction(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            int selection;

            Say(pc, 11000015, 131, "I am the swordsman in charge of the swordsmen. $R;" +
                                   "$P, you don't seem to belong to our guild's jurisdiction? $R;" +
                                   "$R then...$R;" +
                                   "Do you want to be a [Swordsman]? $R;", "Swordsman Master");

            selection = Select(pc, "What do you want to do?", "", "I want to be a [Swordsman]!", "What kind of profession is [Swordsman]?", "Task Desk", "Nothing do");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000015, 131, "Want to be a [Swordsman]? $R;" +
                                               "$R, you seem to have some potential, $R;" +
                                               "Test your strength first! $R;", "Swordsman Master");

                        switch (Select(pc, "Accept the [test]?", "", "No problem", "No need"))
                        {
                            case 1:
                                if (pc.Str > 9)
                                {
                                    Say(pc, 11000015, 131, "Don't worry, the task is very simple. $R;" +
                                                           "$P has a monster named [Bawoo], $R;" +
                                                           "It looks like a vicious dog. $R;" +
                                                           "The task is to knock down [Bawoo]. $R;" +
                                                           "$P, don't forget!! $R;" +
                                                           "You have to get [meat] as proof of defeat! $R;" +
                                                           "$R so you can become a swordsman. $R;", "Swordsman Master");

                                    switch (Select(pc, "Accept the [test]?", "", "No problem", "No need"))
                                    {
                                        case 1:
                                            JobBasic_01_mask.SetValue(JobBasic_01.Choose_to_become_a_swordsman, true);

                                            Say(pc, 11000015, 131, "……$R;" +
                                                                   "$P is fine, $R;" +
                                                                   "I'll wait for you to come back. $R;", "Swordsman Master");
                                            break;

                                        case 2:
                                            Say(pc, 11000015, 131, "Swordsmen are brave representatives, $R;" +
                                                                   "Come back with courage. $R;", "Swordsman Master");
                                            break;
                                    }
                                }
                                else
                                {
                                    Say(pc, 11000015, 131, "You still need a little strength if you want to be a swordsman! $R;" +
                                                           "After the strength of $P reaches 10, come find me again. $R;", "Swordsman Master");
                                }
                                break;

                            case 2:
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000015, 131, "The profession of swordsman is more suitable for $R;" +
                                               "The physique of the Emil and Daumini! $R;" +
                                               "$R judges the nature of the profession, $R;" +
                                               "Whether it suits your race is very important, $R;" +
                                               "Do you still want to listen to it? $R;", "Swordsman Master");

                        switch (Select(pc, "Do you still want to listen?", "", "I want to listen", "Don't listen"))
                        {
                            case 1:
                                Say(pc, 11000015, 131, "[Swordsmen] are mainly warriors who use swords and shields! $R;" +
                                                       "Of course, you can also use other weapons. $R;" +
                                                       "$R Swordsman’s greatest charm, $R;" +
                                                       "The attack power is very high. $R;" +
                                                       "Of course, $P has a high defense. $R;" +
                                                       "This will become the shield of the team, $R;" +
                                                       "Not only can you fight, but you can also protect your teammates! $R;" +
                                                       "It's a pity that $P has relatively low collection and handling capabilities, $R;" +
                                                       "Not suitable for one person to act alone. $R;" +
                                                       "$R is a cooperation with peers, $R;" +
                                                       "It will be a brilliant career. $R;", "Swordsman Master");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000015, 131, "If you want to take the task here, $R;" +
                                               "First of all, we must have some conditions. $R;" +
                                               "As for what conditions does $P have? $R;" +
                                               "$R waits for you to become [Swordsman], $R;" +
                                               "I'll tell you again. $R;", "Swordsman Master");
                        break;
                }

                selection = Select(pc, "What do you want to do?", "", "I want to be a [Swordsman]!", "What kind of profession is [Swordsman]?", "Task Desk", "Nothing do");
            } 
        }

        void Swordsman_transfer_task(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (!JobBasic_01_mask.Test(JobBasic_01.Swordsman_transfer_task完成))
            {
                Give_the_Meat_of_BaWoo(pc);
            }

            if (JobBasic_01_mask.Test(JobBasic_01.Swordsman_transfer_task完成) &&
                !JobBasic_01_mask.Test(JobBasic_01.Swordsman_changed_job_successfully))
            {
                Apply_to_become_a_swordsman(pc);
                return;
            }
        }

        void Give_the_Meat_of_BaWoo(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (CountItem(pc, 10006300) > 0)
            {
                Say(pc, 11000015, 131, "Wow!! I really brought the [meat], $R;" +
                                         "It seems that you did a good job. $R;" +
                                         "$R I am looking forward to your future. $R;" +
                                         "$P Now that you have achieved the mission, $R;" +
                                         "From now on, you are the [Swordsman]! $R;", "Swordsman Manager");

                switch (Select(pc, "Do you want to change to [Swordsman]?", "", "Change to [Swordsman]", "Forget it"))
                {
                    case 1:
                        JobBasic_01_mask.SetValue(JobBasic_01.Swordsman_transfer_task完成, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10006300, 1);
                        Say(pc, 0, 0, "Hand over the [meat]! $R;", "");
                        break;

                    case 2:
                        Say(pc, 11000015, 131, "Think carefully and come back. $R;", "Swordsman Master");
                        break;
                }
            }
            else
            {
                Say(pc, 11000015, 131, "In the [Northern Plains of Okrunia] $R;" +
                                       "On the [Reluth Mountain Road] going up. $R;" +
                                       "There are a lot of [Bawoos] inhabiting $R, $R;" +
                                       "But [Bawoos] is very strong! $R;" +
                                       "It is recommended to find friends to help fight~! $R;", "Swordsman Master Manager");
            }
        }

        void Apply_to_become_a_swordsman(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            Say(pc, 11000015, 131, "Then tattoo you $R representing [Swordsman];" +
                                    "[Swordsman's Emblem]. $R;", "Swordsman Master");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_01_mask.SetValue(JobBasic_01.Swordsman_changed_job_successfully, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000015, 131, "…$R;" +
                                       "$P is great, $R;" +
                                       "You have a beautiful coat of arms imprinted on your body. $R;" +
                                       "$R from now on, $R;" +
                                       "You have become a [Swordsman]. $R;", "Swordsman Manager");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SWORDMAN);

                Say(pc, 0, 0, "You have been transferred to [Swordsman]! $R;", "");

                Say(pc, 11000015, 131, "Speak to me after putting on your clothes first. $R;" +
                                       "There is a small gift for you! $R;" +
                                       "$R, you go and pack your luggage first, then come to me. $R;", "Swordsman Master");
            }
            else
            {
                Say(pc, 11000015, 131, "The coat of arms is imprinted on the skin, $R;" +
                                       "Take off your equipment first. $R;", "Swordsman Master");
            }
        }

        void Swordsman_transfer_completed(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_01_mask.SetValue(JobBasic_01.Has_been_transferred_to_a_swordsman, true);

                Say(pc, 11000015, 131, "Give you the [Swordsman Medal], $R;" +
                                         "$R uses [Swordsman Medal] to represent the honor of swordsman. $R;" +
                                         "Good luck. $R;", "Swordsman Master");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50051300, 1);
                Say(pc, 0, 0, "Get the [Swordsman Medal]! $R;", "");

                LearnSkill(pc, 2115);
                Say(pc, 0, 0, "Learned [Quick Slash Combo]! $R;", "");
            }
            else
            {
                Say(pc, 11000015, 131, "Speak to me after putting on your clothes first. $R;", "Swordsman Master");
            }
        }

        void Advanced_transfer(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];

            if (Job2X_01_mask.Test(Job2X_01.Transfer_completed))//_3A37)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "Wear your clothes!! $R;");
                    return;
                }
                Say(pc, 131, "Can't change job now, $R;" +
                    "It's better to accumulate experience first. $R;");
                return;
            }

            if (CountItem(pc, 10020600) >= 1)
            {
                Say(pc, 131, "Very good, now that you have obtained the certification, $R will let you change your job. $R;" +
                    "$R from now on, $R you will become the [Blademaster] everyone envy $R;");
                Advanced_transfer_selection(pc);
                return;
            }

            if (pc.Inventory.Equipments.Count == 0)
            {
                Say(pc, 131, "衣服要穿好啊！！！$R;");
                return;
            }

            if (Job2X_01_mask.Test(Job2X_01.Advanced_transfer_starts))//_3A32)
            {
                Say(pc, 131, "Just go to the $R [Mr. Bong] in [Iron City] $R;" +
                     "If you get the certificate, $R;" +
                     "Just admit that you are a [Blademaster]. $R;");
                return;
            }

            if (pc.Job == PC_JOB.SWORDMAN && pc.JobLevel1 > 29)
            {
                Say(pc, 131, "You finally meet the conditions for challenging advanced occupations $R;" +
                    "That is to change from a swordsman to a Blademaster. $R;");

                Say(pc, 131, "Just go to the $R [Mr. Bong] in [Iron City] $R;" +
                    "If you get the certificate, $R;" +
                    "Just admit that you are a [Blademaster]. $R;");
                Job2X_01_mask.SetValue(Job2X_01.Advanced_transfer_starts, true);
                //_3A32 = true;
                return;
            }

            Say(pc, 131, "You have not yet met the requirements for applying for a job change. $R;" +
                "Start with the career of a swordsman and slowly develop your strength. $R;");
        }

        void Advanced_transfer_selection(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];

            switch (Select(pc, "Do you really want to transfer?", "", "I want to be a Blademaster", "listen to the precautions about Blademasters", "let's forget it"))
            {
                case 1:
                    Say(pc, 131, "Then I will imprint this $R which symbolizes the warrior of light;" +
                        "『Blademaster Emblem』$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        Say(pc, 131, "I will confirm with you one last time, $R;" +
                            "Did you really decide to change your job? $R;");
                        switch (Select(pc, "Really change job?", "", "Become a Blademaster", "Forget it"))
                        {
                            case 1:
                                TakeItem(pc, 10020600, 1);
                                Job2X_01_mask.SetValue(Job2X_01.Transfer_completed, true);
                                //_3A37 = true;
                                ChangePlayerJob(pc, PC_JOB.BLADEMASTER);
                                pc.JEXP = 0;
                                //PARAM ME.JOB = 3
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 4000);
                                Say(pc, 131, "…$R;" +
                                     "$P is great, $R;" +
                                     "You have a beautiful coat of arms imprinted on your body. $R;" +
                                     "$R from now on, $R you will become our [Blademaster] on behalf of us. $R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "You have been transferred to [Blademaster]. $R;");
                                break;
                            case 2:
                                Say(pc, 131, "It seems you don’t want to change your job yet? $R;" +
                                    "I think so, such a big decision $R needs time to think carefully $R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "If the defense is too high, the coat of arms will not be branded $R;" +
                          "Please change into light clothes and come again. $R;");
                    break;
                case 2:
                    Say(pc, 131, "If you become a [Blademaster], $R professional LV will become 1. $R;" +
                        "But the $R you had before the transfer;" +
                        "$R skills and skill points will not disappear. $R;" +
                        "$P also has skills that cannot be learned before the transfer, $R;" +
                        "You can't learn after the transfer. $R;" +
                        "For example, if the occupation level is 30, if you change your job, $R;" +
                        "$R Skills above level 30 before transfer $R;" +
                        "You can't learn, please pay attention. $R;");
                    Advanced_transfer_selection(pc);
                    break;
                case 3:
                    Say(pc, 131, "It seems you don’t want to change your job yet? $R;" +
                         "I think so, such a big decision $R needs time to think carefully $R;");
                    break;
            }
        }
    }
}
