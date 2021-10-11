using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11000346 : Event
    {
        public S11000346()
        {
            this.EventID = 11000346;
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "If you want to take an exam, complete other tasks first $R;" +
     "Need the remaining task points [1] $R;";
            this.gotNormalQuest = "The [Mokugyou/Snakehead Fish] living in the 5th floor underground in the Territory Cave $R;" +
    "It is a monster of water attribute $R;" +
    "So the [fire Arrow] is the most effective! $R;" +
    "Then! Come back safely! $R;";
            this.questFailed = "Failed?! What a shame...$R;" +
    "Then challenge again next time! $R;";
            this.questCompleted = "Yeah~hahahaha!!$R;" +
    "Awesome! $R;" +
    "Handsome passed the training! $R;" +
    "$R, give a reward $R;" +
    "Give the reward to the Archer Master, and you will become [Striker] $R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (!mask.Test(DHAFlags.Miss_Pamela_first_conversation))
            {
                mask.SetValue(DHAFlags.Miss_Pamela_first_conversation, true);
                Say(pc, 131, "Oh ha ha ha ha!!$R;" +
                     "I am Lady Pamela, Rose Striker$R;");
                return;
            }

            if (!mask.Test(DHAFlags.Get_pets) && pc.JobBasic == PC_JOB.ARCHER)
            {
                Get_pets(pc);
                return;
            }

            if (Job2X_04_mask.Test(Job2X_04.Advanced_transfer_start) && !Job2X_04_mask.Test(Job2X_04.Advanced_transfer_ends))
            {
                Advanced_transfer(pc);
                return;
            }

            Say(pc, 131, "Oh ha ha ha ha!!$R;" +
                 "I am Lady Pamela, Rose Striker$R;");

        }

        void Get_pets(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);

            Say(pc, 131, "I don't know what is different from usual $R;");
            switch (Select(pc, "How to do it?", "", "Let's observe first", "Talk as usual"))
            {
                case 1:
                    Say(pc, 131, "Hehe, it’s easy to be lonely♪$R;" +
                        "Okay, okay♪ $R;" +
                        "$R grow up quickly to become an adult~?$R;" +
                        "Ah~ so cute!! $R;");
                    switch (Select(pc, "How to do it?", "", "Indifferent to it", "What to do"))
                    {
                        case 1:
                            break;

                        case 2:
                            ShowEffect(pc, 11000346, 4501);
                            Say(pc, 376, "Ah! $R;" +
                                "$R you guys! Go and stay aside! $R;");
                            Say(pc, 131, "……$R;" +
                                "$P...haha...$R;" +
                                "What's the matter? $R;");
                            switch (Select(pc, "How to do it?", "", "What is hidden?", "What I see will be kept secret"))
                            {
                                case 1:
                                    Say(pc, 131, "Nothing is $R;" +
                                        "If there is nothing to do, go somewhere else $R;");
                                    break;

                                case 2:
                                    Say(pc, 131, "That...that! Hold on! $R;" +
                                        "$R seems to have misunderstood something... there is nothing $R;");

                                    Select(pc, "How to do it?", "", "Just smile", "See the other side");

                                    Say(pc, 131, "That...that is...listen it! $R;" +
                                        "$R I'm just looking for being $R;" +
                                        "These pet owners are $R;" +
                                        "$P is separated from mom, very poor $R;" +
                                        "$R saw crying in the grass, so I was protecting $R;" +
                                        "Isn't it right to be an adventurer? $R;" +
                                        "$P Ah! That's right! $R;" +
                                        "$R do you want to be the master of these guys? $R;");

                                    switch (Select(pc, "How to do it?", "", "Become the owner of Rock Bird Egg", "Become the owner of Little White Wolf"))
                                    {
                                        case 1:
                                            mask.SetValue(DHAFlags.Get_pets, true);
                                            GiveItem(pc, 10050800, 1);
                                            Say(pc, 131, "……$R;" +
                                                "$P children must be happy $R;" +
                                                "In case something happens, you can come back anytime...$R;" +
                                                "$P knows?? $R;" +
                                                "$R makes this unfortunate $R;" +
                                                "I will never forgive! $R;");
                                            Say(pc, 131, "Become the owner of [Rock Bird Egg]! $R;");
                                            break;

                                        case 2:
                                            mask.SetValue(DHAFlags.Get_pets, true);
                                            GiveItem(pc, 10049900, 1);
                                            Say(pc, 131, "……$R;" +
                                                "$P children must be happy $R;" +
                                                "In case something happens, you can come back anytime...$R;" +
                                                "$P got it?? $R;" +
                                                "$R makes this unfortunate $R;" +
                                                "I will never forgive! $R;");
                                            Say(pc, 131, "Become the owner of [Little White Wolf]! $R;");
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 131, "Oh ha ha ha ha!!$R;" +
                         "I am Lady Pamela, Rose Striker$R;");
                    break;
            }
        }

        void Advanced_transfer(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (Job2X_04_mask.Test(Job2X_04.Advanced_transfer_start) && pc.Job == PC_JOB.ARCHER)
            {

                if (CountItem(pc, 10020751) >= 1)
                {
                    Say(pc, 131, "Please go to the Archer Master. $R;");
                    return;
                }

                if (Job2X_04_mask.Test(Job2X_04.The_trial_begins))//_3a58)
                {
                    if (pc.Quest != null)
                    {
                        if (pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
                        {
                            HandleQuest(pc, 24);
                            return;
                        }
                    }


                    switch (Select(pc, "Do you want to take an exam?", "", "Wait a moment!", "Yes! Want to take an exam"))
                    {
                        case 1:
                            break;
                        case 2:
                            HandleQuest(pc, 24);
                            break;
                    }
                    return;
                }

                if (Job2X_04_mask.Test(Job2X_04.Trial_of_the_Hunter))//_3a57)
                {
                    Say(pc, 131, "Have you gotten the Striker's trial? $R;");
                    switch (Select(pc, "Ready to start?", "", "Wait!", "Yes! Please start!"))
                    {
                        case 1:
                            break;

                        case 2:
                            if (pc.Quest != null)
                            {
                                Say(pc, 131, "If you want to take an exam, other tasks are over $R;" +
                                    "Come here again $R;");
                                return;
                            }

                            HandleQuest(pc, 24);
                            if (pc.Quest != null)
                            {
                                if (pc.Quest.ID == 10031100)
                                {
                                    Job2X_04_mask.SetValue(Job2X_04.The_trial_begins, true);
                                }
                            }
                            break;
                    }
                    return;
                }

                if (Job2X_04_mask.Test(Job2X_04.Collect_Fire_Arrow))
                {
                    if (Job2X_04_mask.Test(Job2X_04.Collect_Fire_Arrow) && CountItem(pc, 10026500) >= 100)
                    {
                        Job2X_04_mask.SetValue(Job2X_04.Trial_of_the_Hunter, true);
                        //_3a57 = true;
                        Say(pc, 131, "Have you got 100? $R;" +
                            "Then count it! $R;" +
                            "$P one, two, three, four, $R five, six, seven, eight $R;" +
                            "Nine, ten, eleven, twelve, $R thirteen, fourteen, $R fifteen, sixteen $R;" +
                            "$P17, 18, $R19, 20$R;" +
                            "Twenty-one, twenty-two, $R twenty-three, twenty-four $R;" +
                            "$P twenty-five, twenty-six, $R twenty-seven, twenty-eight $R;" +
                            "Twenty-nine, thirty, $R thirty-one, thirty-two $R;" +
                            "$P thirty-three, thirty-four, $R thirty-five, thirty-six $R;" +
                            "Thirty-seven, thirty-eight, $R thirty-nine, forty $R;" +
                            "$P forty-one, forty-two, $R forty-three, forty-four $R;" +
                            "Forty-five, forty-six, $R forty-seven, forty-eight $R;" +
                            "$P forty-nine, fifty, $R fifty-one, fifty-two $R;" +
                            "Fifty-three, fifty-four, $R fifty-five, fifty-six $R;" +
                            "$P fifty-seven, fifty-eight, $R fifty-nine, sixty $R;" +
                            "Sixty-one, sixty-two, $R sixty-three, sixty-four $R;" +
                            "$P sixty five, sixty six, $R sixty seven, sixty eight $R;" +
                            "Sixty-nine, seventy, $R seventy-one, seventy-two $R;" +
                            "$P seventy three, seventy four, $R seventy five, seventy six $R;" +
                            "Seventy seven, seventy-eight, $R seventy nine, eighty $R;" +
                            "$P eighty one, eighty two, $R eighty three, eighty four $R;" +
                            "Eighty five, eighty six, $R eighty seven, eighty eight $R;" +
                            "$P eighty nine, ninety, $R ninety one, ninety two $R;" +
                            "Ninety three, ninety four, $R ninety five, ninety six $R;" +
                            "$P97$R;" +
                            "Ninety-eight $R;" +
                            "Ninety-nine...$R;" +
                            "$P one hundred...$R;" +
                            "Um... a hundred is not wrong $R;" +
                            "$R hehe... I won't be sloppy $R;");
                        Say(pc, 131, "The next thing you need is [Healing Potion] $R;" +
                            "But counting is troublesome $R;" +
                            "Prepare to see it yourself in the future $R;" +
                            "$P don't know what to do? $R;" +
                            "Really... why did you collect the Arrow of Fire $R;" +
                            "Don't even know? $R;" +
                            "$R was collected in order to knock back the monsters with bows and arrows! $R;" +
                            "$P will accept the use of attribute bows $R next time;" +
                            "Attack training! $R;" +
                            "$R attack the weakness of the monster's attributes, $R;" +
                            "$R that can cause more damage than usual;" +
                            "$P carries all bows and arrows with $R;" +
                            "Change various attributes according to the monster $R;" +
                            "The best at fighting is the hunter! $R;" +
                            "$R hahahaha!$R;" +
                            "$P In order to receive training, come back when you are ready $R;" +
                            "$R will wait here! $R;");
                        return;
                    }
                    Say(pc, 131, "First start with $R;" +
                         "As an archer, collect bows and arrows! $R;" +
                         "$R collect 100 [Fire Arrows]! $R;");
                    switch (Select(pc, "Do you know how to start?", "", "Of course", "Don't know"))
                    {
                        case 1:
                            break;
                        case 2:
                            Say(pc, 131, "Oh hahaha!! Then please ask! $R;" +
                                "$R Fire Arrow $R only to the [flame spirit] in the desert $R;" +
                                "If you bring [Fire Summoning Stone] and [Arrow] in the past, $R;" +
                                "He will help make $R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "Oh hahaha!!$R;" +
                     "I am Lady Pamela the Rose Striker $R;" +
                     "You just want to be a Striker? $R;" +
                     pc.Name + "Really?!$R;" +
                     "$P I am the person in charge of Striker transfer $R;" +
                     "Then carry out the transfer test! $R;" +
                     "Are you mentally prepared? $R;");
                switch (Select(pc, "Are you ready?", "", "Wait!", "Yes! Please start"))
                {
                    case 1:
                        break;
                    case 2:
                        if (Job2X_04_mask.Test(Job2X_04.Collect_Fire_Arrow) && CountItem(pc, 10026500) >= 100)
                        {
                            Job2X_04_mask.SetValue(Job2X_04.Trial_of_the_Hunter, true);
                            //_3a57 = true;
                            Say(pc, 131, "Have you got 100? $R;" +
                                "Then count it! $R;" +
                                "$P one, two, three, four, $R five, six, seven, eight $R;" +
                                "Nine, ten, eleven, twelve, $R thirteen, fourteen, $R fifteen, sixteen $R;" +
                                "$P17, 18, $R19, 20$R;" +
                                "Twenty-one, twenty-two, $R twenty-three, twenty-four $R;" +
                                "$P twenty-five, twenty-six, $R twenty-seven, twenty-eight $R;" +
                                "Twenty-nine, thirty, $R thirty-one, thirty-two $R;" +
                                "$P thirty-three, thirty-four, $R thirty-five, thirty-six $R;" +
                                "Thirty-seven, thirty-eight, $R thirty-nine, forty $R;" +
                                "$P forty-one, forty-two, $R forty-three, forty-four $R;" +
                                "Forty-five, forty-six, $R forty-seven, forty-eight $R;" +
                                "$P forty-nine, fifty, $R fifty-one, fifty-two $R;" +
                                "Fifty-three, fifty-four, $R fifty-five, fifty-six $R;" +
                                "$P fifty-seven, fifty-eight, $R fifty-nine, sixty $R;" +
                                "Sixty-one, sixty-two, $R sixty-three, sixty-four $R;" +
                                "$P sixty five, sixty six, $R sixty seven, sixty eight $R;" +
                                "Sixty-nine, seventy, $R seventy-one, seventy-two $R;" +
                                "$P seventy three, seventy four, $R seventy five, seventy six $R;" +
                                "Seventy seven, seventy-eight, $R seventy nine, eighty $R;" +
                                "$P eighty one, eighty two, $R eighty three, eighty four $R;" +
                                "Eighty five, eighty six, $R eighty seven, eighty eight $R;" +
                                "$P eighty nine, ninety, $R ninety one, ninety two $R;" +
                                "Ninety three, ninety four, $R ninety five, ninety six $R;" +
                                "$P97$R;" +
                                "Ninety-eight $R;" +
                                "Ninety-nine...$R;" +
                                "$P one hundred...$R;" +
                                "Um... a hundred is not wrong $R;" +
                                "$R hehe... I won't be sloppy $R;");
                            Say(pc, 131, "The next thing you need is [Healing Potion] $R;" +
                                "But counting is troublesome $R;" +
                                "Prepare to see it yourself in the future $R;" +
                                "$P don't know what to do? $R;" +
                                "Really... why did you collect the Arrow of Fire $R;" +
                                "Don't even know? $R;" +
                                "$R was collected in order to knock back the monsters with bows and arrows! $R;" +
                                "$P will accept the use of attribute bows $R next time;" +
                                "Attack training! $R;" +
                                "$R attack the weakness of the monster's attributes, $R;" +
                                "$R that can cause more damage than usual;" +
                                "$P carries all bows and arrows with $R;" +
                                "Change various attributes according to the monster $R;" +
                                "The best at fighting is the hunter! $R;" +
                                "$R hahahaha!$R;" +
                                "$P In order to receive training, come back when you are ready $R;" +
                                "$R will wait here! $R;");
                            return;
                        }
                        Job2X_04_mask.SetValue(Job2X_04.Collect_Fire_Arrow, true);
                        //_3a56 = true;
                        Say(pc, 131, "First start with $R;" +
                             "As an archer, collect bows and arrows! $R;" +
                             "$R collect 100 [Fire Arrows]! $R;");
                        switch (Select(pc, "Do you know how to start?", "", "Of course", "Don't know"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 131, "Oh hahaha!! Then please ask! $R;" +
                                    "$R Fire Arrow $R only to the [flame spirit] in the desert $R;" +
                                    "If you bring pFire Summoning Stone[ and [Arrow] in the past, $R;" +
                                    "He will help make $R;");
                                break;
                        }
                        break;
                }
                return;
            }
            Say(pc, 131, "Oh ha ha ha ha!!$R;" +
                 "I am Miss Pamela, Rose Striker$R;");
        }
    }
}
