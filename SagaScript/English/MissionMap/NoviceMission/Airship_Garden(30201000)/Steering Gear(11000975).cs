using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:飛空庭的庭院(30201000) NPC基本信息:舵輪(11000975) X:7 Y:13
namespace SagaScript.M30201000
{
    public class S11000975 : Event
    {
        public S11000975()
        {
            this.EventID = 11000975;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000938, 131, "That is the [Steering Gear] of the Flying Garden! $R;" +
                                    "$R come down from the flying garden, $R;" +
                                    "Or used when transforming the flying garden. $R;" +
                                    "$R don't worry, $R;" +
                                    "Except for me, no one will be allowed to operate. $R;", "Masha");
        }
    }
}
