using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018001) NPC基本信息:初階冒險者(11000933) X:62 Y:59
namespace SagaScript.M10018001
{
    public class S11000933 : Event
    {
        public S11000933()
        {
            this.EventID = 11000933;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000933, 0, "There are too many things that I don't understand, $R;" +
                                  "You must study hard! $R;" +
                                  "$R do you want to listen together? $R;", "Apprentice Adventurer");
        }
    }
}
