using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:埃米爾(11001400) X:195 Y:64
namespace SagaScript.M10018102
{
    public class S11001400 : Event
    {
        public S11001400()
        {
            this.EventID = 11001400;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Conversation_with_Emil_for_the_first_time(pc);
            }
            else
            {
                //埃米爾 = emil 
                Say(pc, 11001400, 131, "What, explain more? $R;", "Emil");
            }

            selection = Select(pc, "Which aspect of operation do you want to ask about?", "", "Talk/attack", "Move method", "End game method", "Move map method", "Other");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001400, 131, "I want to say hello to people in the village, $R;" +
                                               "Or when attacking the enemy, $R;" +
                                               "Just click the left mouse button on the target. $R;" +
                                               "$P...see you greeted me, $R;" +
                                               "Should you already know it? $R;" +
                                               "$P if the other person overlaps with others, $R;" +
                                               "Just click the right mouse button. $R;" +
                                               "$R will show a window with overlapping names, $R;" +
                                               "You can choose who to say hello in it! $R;", "Emil");
                        break;

                    case 2:
                        Say(pc, 11001400, 131, "When moving, click the left button on the moving target. $R;" +
                                               "$R If you keep clicking for a long time, $R;" +
                                               "It will automatically follow the direction of mouse movement. $R;" +
                                               "$P is difficult to operate when there are too many people. $R;" +
                                               "$R now use the arrow keys of the keyboard to move, $R;" +
                                               "It will be more convenient! $R;", "Emil");
                        break;

                    case 3:
                        Say(pc, 11001400, 131, "To end the game, $R;" +
                                               "Please press the ESC key in the upper left corner of the keyboard...$R;" +
                                               "$P If you press this button...$R;" +
                                               "Hey! Don't click now! $R;" +
                                               "$P then...$R;" +
                                               "$R[Return to game] or [Logout]$R;" +
                                               "You can choose between the two options of $R. $R;" +
                                               "$P pay attention, $R;" +
                                               "End the game during the dialogue, $R;" +
                                               "You will not hear the following dialogue! $R;" +
                                               "$P takes 5 seconds to log out, $R;" +
                                               "$R moves or is attacked by a monster at this time, $R;" +
                                               "Logout will be cancelled automatically! $R;" +
                                               "$P waiting for $R in the cold, hot or in the water;" +
                                               "Where the physical HP value gradually decreases, $R;" +
                                               "There is no way to log out! $R;", "Emil");
                        break;

                    case 4:
                        Say(pc, 11001400, 131, "When you want to move to the next map, $R;" +
                                               "See the blue beam of light? $R;" +
                                               "This is the [Teleport Point], $R;" +
                                               "Step on it and you will enter the next map! $R;" +
                                               "Same for $P, just step on the teleport point when entering the store. $R;" +
                                               "$P if there is no beam of light, $R;" +
                                               "The teleporter may be hidden. $R;" +
                                               "$R if it is possible, $R;" +
                                               "Just step on it and try it out, $R;" +
                                               "There may be new discoveries! $R;", "Emil");
                        break;
                }

                selection = Select(pc, "Which aspect of operation do you want to ask about?", "", "Talk/attack", "Move method", "End game method", "Move map method", "Other");
            }

            if (!Beginner_01_mask.Test(Beginner_01.The_Emil_gives_the_Emil_badge))
            {
                The_Emil_gives_the_Emil_badge(pc);
                return;
            }

            Say(pc, 11001400, 131, "Then explain the teleport point! $R;" +
                                     "$R just click on that transfer point, $R;" +
                                     "You can enter the next stage! $R;" +
                                     "$P follows the road and is the city of Acropolis! $R;" +
                                     "$R don't worry, $R;" +
                                     "As long as you refer to the minimap, you won't get lost. $R;", "Emil");
        }

        void Conversation_with_Emil_for_the_first_time(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            byte x, y;

            Beginner_01_mask.SetValue(Beginner_01.Have_already_had_the_first_conversation_with_Emil, true);

            Say(pc, 11001400, 131, "Hello!$R;" +
                                   "Is this the first time here? $R;" +
                                   "$R, my name is Emil, $R;" +
                                   "Is responsible for helping to come to $R;" +
                                   "Novices in the ECO world. $R;" +
                                   "$P is ready, let's start personal guidance...$R;", "Emil");

            switch (Select(pc, "Do you want to guide?", "", "Guide", "Go back to the game directly"))
            {
                case 1:
                    Say(pc, 11001400, 131, "First of all, welcome to the ECO world! $R;" +
                                           "$R Now, a new magical world is about to unfold before your eyes. $R;" +
                                           "$P then start to teach you some basic operation methods! $R;", "Emil");
                    break;

                case 2:
                    Say(pc, 11001400, 131, "Really, I will never hear the explanation again! $R;", "Emil");

                    switch (Select(pc, "Really don't listen to the instructions?", "", "After listening to the instructions, I'm going", "You don't have to listen to it!"))
                    {
                        case 1:
                            Say(pc, 11001400, 131, "Let’s start! $R;", "Emil");

                            Say(pc, 11001400, 131, "First of all, welcome to the ECO world! $R;" +
                                                   "$R Now, a new magical world is about to unfold before your eyes. $R;" +
                                                   "$P then start to teach you some basic operation methods! $R;", "Emil");
                            break;

                        case 2:
                            Beginner_01_mask.SetValue(Beginner_01.Emil_gives_Emil_introduction_book, true);

                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10043081, 1);
                            Say(pc, 0, 0, "Get the [Emile Introduction Book]! $R;", "");

                            Say(pc, 11001400, 131, "I see, there is one more point at the end. $R;" +
                                                   "$P after arriving in Acropolis, $R;" +
                                                   "It's best to go to a [coffee shop] or a [coffee shop branch]. $R;" +
                                                   "$P [Cafe] in [Acropolis City] $R;" +
                                                   "East of [Xiacheng]! $R;" +
                                                   "$R [Cafe Branch] in [Acropolis City] $R;" +
                                                   "Near the center of the East, South, West, and Northern Plains! $R;" +
                                                   "$P I have an introduction letter for you, $R;" +
                                                   "Confirm in the item window? $R;" +
                                                   "$P [Acropolis] is a big city! $R;" +
                                                   "If you get lost, you can use the [Sound Guiding Robot.] $R;" +
                                                   "$R [Sound Guiding Robot] is a strange robot, $R;" +
                                                   "Acropolis City is everywhere, and it's easy to find. $R;", "Emil");

                            Say(pc, 11001400, 131, "Then send you to [Acropolis]! $R;", "Emil");

                            x = (byte)Global.Random.Next(245, 250);
                            y = (byte)Global.Random.Next(126, 131);

                            Warp(pc, 10023100, x, y);
                            return;
                    }
                    break;
            }
        }

        void The_Emil_gives_the_Emil_badge(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.The_Emil_gives_the_Emil_badge, true);

            Say(pc, 11001400, 131, "and...$R;" +
                                   "$R passes through the previous teleportation point, $R;" +
                                   "It will enter the next map, $R;" +
                                   "Go along the road, $R;" +
                                   "There will be a [level]! $R;" +
                                   "$R, my companion, is a Dominion boy. $R;" +
                                   "$P he will teach you a lot about fighting! $R;" +
                                   "$R Well, good luck! $R;", "Emil");

            Say(pc, 11001400, 131, "This is a farewell gift! $R;", "Emil");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10009550, 1);
            Say(pc, 0, 0, "Get the [Emil Badge]! $R;", "");

            Say(pc, 11001400, 131, "Put this badge in $R called [ItemBox];" +
                                   "The pink machine can be exchanged to $R;" +
                                   "Various useful items! $R;" +
                                   "$P try to put the badge $R;" +
                                   "Put it on the [ItemBox] in front of me! $R;", "Emil");

            Say(pc, 11001400, 131, "Yes! $R;" +
                                   "Before you leave $R, go to the village first! $;" +
                                   "You will hear a lot of information! $R;", "Emil");
        }   
    }
}
