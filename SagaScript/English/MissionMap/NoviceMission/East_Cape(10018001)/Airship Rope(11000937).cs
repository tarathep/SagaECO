using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:東方海角(10018001) NPC基本信息:飛空庭繩子(11000937) X:32 Y:50
namespace SagaScript.M10018001
{
    public class S11000937 : Event
    {
        public S11000937()
        {
            this.EventID = 11000937;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            Say(pc, 11000936, 131, "That's the rope connecting the flying garden, $R" +
                                    "Go up and look inside. $R;" +
                                    "If $P wants to end the guidance, $R;" +
                                    "I can send you to the city, what do you want to do? $R;", "Masha");

            switch (Select(pc, "Go to the Flying Garden?", "", "Go to the Flying Garden", "Go to the Next Guidance", "End Guidance", "Not ready yet"))
            {
                case 1:
                    Say(pc, 11000936, 131, "Let’s go then! $R;" +
                                           "After $R enters, I will tell you more carefully. $R;", "Masha");

                    x = (byte)Global.Random.Next(7, 7);
                    y = (byte)Global.Random.Next(12, 12);

                    Warp(pc, 30201000, x, y);
                    break;

                case 2:
                    Say(pc, 11000936, 131, "Do you know the information about [Flying Sky Court]? $R;" +
                                            "$R then, $R;" +
                                            "Use [Flying Garden] to take you to the next location! $R;", "Masha");

                    x = (byte)Global.Random.Next(171, 174);
                    y = (byte)Global.Random.Next(100, 103);

                    Warp(pc, 100250001, x, y);
                    break;

                case 3:
                    Say(pc, 11000936, 131, "Got it, $R;" +
                                             "Send you to the city? $R;" +
                                             "$R; [Titus] is on the bridge, $R;" +
                                             "Ask him for the rest! $R;" +
                                             "$P, go on! $R;", "Masha");

                    x = (byte)Global.Random.Next(18, 29);
                    y = (byte)Global.Random.Next(124, 130);

                    Warp(pc, 10025001, x, y);
                    break;

                case 4:
                    break;
            }
        }
    }
}
