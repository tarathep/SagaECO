using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:飛空庭的房間(30202000) NPC基本信息:操作盤(11000940) X:8 Y:14
namespace SagaScript.M30202000
{
    public class S11000940 : Event
    {
        public S11000940()
        {
            this.EventID = 11000940;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "This is the [Control Panel] of Fei Kongting! $R;" +
                           "$R uses this [Control Panel] $R on the Flying Court;" +
                           "And the [steering wheel] outside, $R;" +
                           "You can change the pattern on the wall or the ground! $R;" +
                           "$P Thangka craftsmen sell $R;" +
                           "Floor, wallpaper, furniture and other props! $R;" +
                           "$R and there are some furniture, $R;" +
                           "It is produced by skills possessed by certain occupations. $R;" +
                           "If there is a flying garden in $P, $R;" +
                           "Please make a unique and personalized flying garden! $R;", "");
        }
    }
}
