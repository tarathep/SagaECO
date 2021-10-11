using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:武器製造所(30013000) NPC基本信息:武器製作所 店員(11000159) X:3 Y:1
namespace SagaScript.M30013000
{
    public class S11000159 : Event
    {
        public S11000159()
        {
            this.EventID = 11000159;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "Hmm! This is the weapon factory $R;" +
                 "$R here can use the materials you collected $R to smelt into iron to make weapons $R;" +
                 "Does $R have any weapons needed? $R;","Weaponsmith");

            switch (Select(pc, "What do you want to do?", "", "Receive to make weapons", "Receive to make armor", "Receive to smelt metal", "Receive to make [bows and arrows]", "Do nothing"))
            {
                case 1:
                    switch (Select(pc, "What do you want to make?", "", "Make a weapon", "Make a wand", "Make a bow", "Give up"))
                    {
                        case 1:
                            Synthese(pc, 2010, 10);
                            break;
                        case 2:
                            Synthese(pc, 2021, 5);
                            break;
                        case 3:
                            Synthese(pc, 2034, 5);
                            break;
                    }
                    break;
                case 2:
                    Synthese(pc, 2017, 5);
                    break;
                case 3:
                    Synthese(pc, 2051, 3);
                    break;
                case 4:
                    Synthese(pc, 2035, 5);
                    break;
            }

        }
    }
}
