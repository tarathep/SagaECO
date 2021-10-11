using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:路利耶(11001401) X:223 Y:82
namespace SagaScript.M10018102
{
    public class S11001401 : Event
    {
        public S11001401()
        {
            this.EventID = 11001401;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001401, 131, "Hello!$R;" +
                                    "$R, let me teach you a few things about ECO$R;" +
                                    "Several programs! $R;", "Lurie");

            selection = Select(pc, "Which description do you want to hear?", "", "About the minimap", "About the viewpoint", "About the screenshot", "About the environment setting", "No need");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001401, 131, "The mini map is $R;" +
                                               "On the map of your current location, $R;" +
                                               "A small screen showing you and the location of the teleporter, $R;" +
                                               "It's a very convenient function! $R;" +
                                               "$P Click the [Map] button in the main window. $R;" +
                                               "$R can be set to display/not display the mini map. $R;" +
                                               "$P press the button on the small map window, $R;" +
                                               "You can also change the size of the minimap! $R;" +
                                               "$R has 1/4 area or the entire area, $R;" +
                                               "[World Map] can be displayed in the entire window. $R;" +
                                               "$P now describes the [point] you see on the map. $R;" +
                                               "The $R arrow indicates your position, $R;" +
                                               "[Red dot] is the location of the NPC, $R;" +
                                               "The [blue dot] indicates the location of the teleportation point. $R;" +
                                               "$P If there are players on the same team in the same area, $R;" +
                                               "The position of the players will also be displayed. $R;", "Lurie");
                        break;

                    case 2:
                        Say(pc, 11001401, 131, "Now explain the viewpoint in the game...$R;" +
                                               "And how to operate the camera! $R;" +
                                               "$P arbitrarily on the screen $R;" +
                                               "Right click and drag while moving and try it out! $R;" +
                                               "$P...$R;" +
                                               "The viewpoint of $P has changed? $R;" +
                                               "$R choose your favorite viewpoint to play the game. $R;" +
                                               "$P to add, $R;" +
                                               "Teach you a convenient technique! $R;" +
                                               "$P put the target on the ground, double click the right mouse button, $R;" +
                                               "The screen will face north. $R;" +
                                               "$R is suitable for, $R;" +
                                               "Used when you are confused about the direction of your position. $R;" +
                                               "$P is in [Environment Settings], $R;" +
                                               "Except for the north $R;" +
                                               "You can also set the screen to other directions! $R;" +
                                               "$R can be set as you like. $R;", "Lurie");
                        break;

                    case 3:
                        Say(pc, 11001401, 131, "When you want to take a picture of the game, $R;" +
                                               "Click the [PrintScreen] button on the keyboard. $R;" +
                                               "$R Click this to save the screen! $R;" +
                                               "$P[PrintScreen] screen storage location, $R;" +
                                               "It depends on everyone's computer settings. $R;" +
                                               "$R as long as you look at the designated location! $R;", "Lurie");
                        break;

                    case 4:
                        Say(pc, 11001401, 131, "In the [Environment Settings] window $R;" +
                                               "You can adjust the display or sound of the game. $R;" +
                                               "$P has a button at the top right of the screen, $R;" +
                                               "Click the third button from right to left, $R;" +
                                               "The [System] window will be displayed. $R;" +
                                               "$P...Can you see the window?$R;" +
                                               "$P can also operate when talking to me, $R;" +
                                               "You can also change the settings now. $R;" +
                                               "$P use the $R at the top right of the [System] window;" +
                                               "[Low] [Normal] [High], $R;" +
                                               "You can adjust the general settings of the screen. $R;" +
                                               "The picture processing speed is too slow in the $R game, $R;" +
                                               "Just adjust here and have a try! $R;" +
                                               "$P detailed setting method, $R;" +
                                               "Wait for you to be familiar with the game before telling you! $R;", "Lurie");
                        break;
                }

                selection = Select(pc, "Which description do you want to hear?", "", "About the minimap", "About the viewpoint", "About the screenshot", "About the environment setting", "No need");
            }
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001401, 131, "Have you heard all the words of Emil? $R;" +
                                    "It's best to listen to Emil's information first!", "Lurie");
        }  
    }
}
