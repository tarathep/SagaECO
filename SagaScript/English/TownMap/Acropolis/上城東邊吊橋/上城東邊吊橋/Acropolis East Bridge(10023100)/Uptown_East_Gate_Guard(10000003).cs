using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:上城東門守衛(10000003) X:222 Y:126
namespace SagaScript.M10023100
{
    public class S10000003 : Event
    {
        public S10000003()
        {
            this.EventID = 10000003;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];


            if (CountItem(pc, 10042801) >= 1)
            {
                if (pc.Gold < 100)
                {
                    if (pc.PossesionedActors.Count != 0)
                    {
                        PlaySound(pc, 2225, false, 100, 50);
                        Say(pc, 131, "咇!!!!$R;" +
                             "Forbidden...forbid $R;" +
                             "$R can't pass depending on the status $R;" +
                             "$P...$R;" +
                             "This is forbidden zone $R;" +
                             "In order to let go by through, $ R;" +
                             "A special checkpoint has been set up for the inspection basis $R;" +
                             "$P pass by relying on $R refers to the place that generally cannot be passed, $R;" +
                             "You can pass $R; in a dependent state;" +
                             "$This is a very convenient technique, $R;" +
                             "But...$R;" +
                             "$P...$R;" +
                             "Go back if you know it $R;");
                        return;
                    }
                    Warp(pc, 10023000, 217, 127);
                    return;
                }
                switch (Select(pc, "Welcome to Acropolis!", "", "Enter the upper city", "Thanks for your hard work", "Don't go in"))
                {
                    case 1:
                        if (pc.PossesionedActors.Count != 0)
                        {
                            PlaySound(pc, 2225, false, 100, 50);
                            Say(pc, 131, "咇!!!!$R;" +
                                "Forbidden...forbid $R;" +
                                "$R can't pass depending on the status $R;" +
                                "$P...$R;" +
                                "This is forbidden zone $R;" +
                                "In order to prevent from passing, $R;" +
                                "A special checkpoint has been set up for the inspection basis $R;" +
                                "$P pass by relying on $R refers to the place that generally cannot be passed, $R;" +
                                "You can pass $R; in a dependent state;" +
                                "$This is a very convenient technique, $R;" +
                                "But...$R;" +
                                "$P...$R;" +
                                "Go back if you know it $R;");
                            return;
                        }
                        Warp(pc, 10023000, 217, 127);
                        break;
                    case 2:
                        switch (Select(pc, "Know everything...", "", "Don't let people find out, I took out 100 gold coins", "Don't give me"))
                        {
                            case 1:
                                pc.Gold -= 100;
                                switch (Select(pc, "Which way to go?", "", "Pass the rear aisle of the east gate", "Pass the rear aisle of the west gate", "Pass the rear aisle of the south gate", "Pass the rear aisle of the north gate"))
                                {
                                    case 1:
                                        Warp(pc, 10023100, 224, 127);
                                        break;
                                    case 2:
                                        Warp(pc, 10023200, 31, 127);
                                        break;
                                    case 3:
                                        Warp(pc, 10023300, 128, 225);
                                        break;
                                    case 4:
                                        Warp(pc, 10023400, 127, 31);
                                        break;
                                }
                                break;
                            case 2:
                                break;
                        }
                        break;
                    case 3:
                        break;
                }
                return;
            }
            if (Knights_mask.Test(Knights.Tell_me_how_to_join_the_Knights) &&
                !Knights_mask.Test(Knights.Get_the_Uptown_Pass))
            {
                Say(pc, 131, "The way to enter the uptown city is a secret, $R;" +
                      "Don't tell anyone! $R;");
                return;
            }
            if (Knights_mask.Test(Knights.Consider_joining_the_Knights) &&
                !Knights_mask.Test(Knights.Tell_me_how_to_join_the_Knights) &&
                !Knights_mask.Test(Knights.Get_the_Uptown_Pass))
            {
                Say(pc, 131, "Do you really want to join the Knights of Mixed City? $R;" +
                     "$R, of course, we have to join our strongest Eastern Army! $R;" +
                     "$P...$R;" +
                     "Then tell you how to get the license $R;" +
                     "Go down from this ladder $R;" +
                     "It's Argopulus Down Town $R;" +
                     "Got it? $R;" +
                     "Walk slightly south from the center of the lower city $R;" +
                     "There is an elderly lady $R;" +
                     "$P she is known as $R;" +
                     "The elder of Dr. All Things in the Downtown City, $R;" +
                     "Find her! She will tell you something, $R;" +
                     "It will be helpful to you, remember not to be rude. $R;");
                Knights_mask.SetValue(Knights.Tell_me_how_to_join_the_Knights, true);
                return;
            }
            if (Knights_mask.Test(Knights.Was_told_not_to_join_the_Knights) &&
                !Knights_mask.Test(Knights.Tell_the_group_leader_the_reason_for_ignoring_you))
            {
                Say(pc, 131, "The sir does not see you? Of course $R;" +
                     "$R If you are very famous, maybe I will meet you, $R may be afraid that you are a spy in a certain country. $R;" +
                     "$P, if you want to increase your visibility here, $R must first promote your name. $R start with the simple ones $R;" +
                     "$P helps people, or is entrusted to handle some tasks, $R can increase the visibility $R;" +
                     "$R is waiting for you to become famous, even if it's just a little bit, of course the $R officer will meet you $R;" +
                     "$P because the Knights have been short of manpower $R;");
                Knights_mask.SetValue(Knights.Tell_the_group_leader_the_reason_for_ignoring_you, true);
                return;
            }
            if (!Knights_mask.Test(Knights.Get_the_Uptown_Pass) &&
                Knights_mask.Test(Knights.Informed_that_there_is_no_pass))
            {
                Say(pc, 131, "It's you again, really annoying $R;" +
                     "Here only holds $R in the upper city of Acropolis;" +
                     "Only people with a citizen certificate can enter A$R;" +
                     "$R wait for you to get the license, come on again $R;");
                return;
            }
            if (!Knights_mask.Test(Knights.Get_the_Uptown_Pass))
            {
                //WINDOWOPEN 8
                Say(pc, 131, "Welcome to the world's largest trading city $R;" +
                    "Argopoulos$R;" +
                    "The structure of this street in $P is a bit special, $R;" +
                    "$R people who come for the first time are easy to get lost, $R;" +
                    "Just give you a brief explanation $R;");
                switch (Select(pc, "Listen to the instructions?", "", "Give up", "I want to hear"))
                {
                    case 1:
                        Say(pc, 131, "Sorry, there is no uptown permit $R;" +
                            "Can't enter, please go back $R;");
                        Knights_mask.SetValue(Knights.Informed_that_there_is_no_pass, true);
                        break;
                    case 2:
                        Say(pc, 131, "Agopoulus is located on a huge lake $R;" +
                            "$R, east, west, south and north, there are huge suspension bridges $R;" +
                            "The place where you are standing is the east gate $R;" +
                            "There are also places on the south, west, and north where $R is similar to here $R;" +
                            "Looking at the map, you can know that in the city of $R, east, west, south and north are symmetrical $R;" +
                            "$P is also divided into two floors above ground and underground $R;" +
                            "$R in the door I am guarding $R;" +
                            "The wide streets are called Shangcheng $R;" +
                            "The underground is Xiacheng $R;" +
                            "$P how to get down town, right? $R;" +
                            "Go around from my right or left corner $R;" +
                            "You will see a ladder $R;" +
                            "Just go down the stairs $R;" +
                            "$P doesn't have a permit anyway, you can't get in the upper city $R;" +
                            "That means you can't get in $R;" +
                            "$R go, go back $R;");
                        Knights_mask.SetValue(Knights.Informed_that_there_is_no_pass, true);
                        break;
                }
                return;
            }
            if (pc.Gold < 100)
            {
                if (pc.PossesionedActors.Count != 0)
                {
                    PlaySound(pc, 2225, false, 100, 50);
                    Say(pc, 131, "咇!!!!$R;" +
                        "Forbidden...forbid $R;" +
                        "$R can't pass depending on the status $R;" +
                        "$P...$R;" +
                        "This is forbidden zone $R;" +
                        "In order to prevent Pingyi from passing, $R;" +
                        "A special checkpoint has been set up for the inspection basis $R;" +
                        "$P pass by relying on $R refers to the place that generally cannot be passed, $R;" +
                        "You can pass $R; in a dependent state;" +
                        "$This is a very convenient technique, $R;" +
                        "But...$R;" +
                        "$P...$R;" +
                        "Go back if you know it $R;");
                    return;
                }
                Warp(pc, 10023000, 217, 127);
                return;
            }
            switch (Select(pc, "Welcome to Acropolis!", "", "Enter the upper city", "Thanks for your hard work", "Don't go in"))
            {
                case 1:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        PlaySound(pc, 2225, false, 100, 50);
                        Say(pc, 131, "咇!!!!$R;" +
                            "Forbidden...forbid $R;" +
                            "$R can't pass depending on the status $R;" +
                            "$P...$R;" +
                            "This is forbidden zone $R;" +
                            "In order to prevent Pingyi from passing, $R;" +
                            "A special checkpoint has been set up for the inspection basis $R;" +
                            "$P pass by relying on $R refers to the place that generally cannot be passed, $R;" +
                            "You can pass $R; in a dependent state;" +
                            "$This is a very convenient technique, $R;" +
                            "But...$R;" +
                            "$P...$R;" +
                            "Go back if you know it $R;");
                        return;
                    }
                    Warp(pc, 10023000, 217, 127);
                    break;
                case 2:
                    switch (Select(pc, "Know everything...", "", "Don't let people find out, I took out 100 gold coins", "Don't give me"))
                    {
                        case 1:
                            pc.Gold -= 100;
                            switch (Select(pc, "Which way to go?", "", "Pass the rear aisle of the east gate", "Pass the rear aisle of the west gate", "Pass the rear aisle of the south gate", "Pass the rear aisle of the north gate"))
                            {
                                case 1:
                                    Warp(pc, 10023100, 224, 127);
                                    break;
                                case 2:
                                    Warp(pc, 10023200, 31, 127);
                                    break;
                                case 3:
                                    Warp(pc, 10023300, 128, 225);
                                    break;
                                case 4:
                                    Warp(pc, 10023400, 127, 31);
                                    break;
                            }
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 3:
                    break;
            }
        }
    }
}
