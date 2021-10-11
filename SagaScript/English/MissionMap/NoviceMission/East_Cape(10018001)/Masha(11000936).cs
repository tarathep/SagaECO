using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018001) NPC基本信息:瑪莎(11000936) X:33 Y:49
namespace SagaScript.M10018001
{
    public class S11000936 : Event
    {
        public S11000936()
        {
            this.EventID = 11000936;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000936, 131, "Hello, my name is Masha. $R;" +
                                    "$R is entrusted by Emil, $R;" +
                                    "Responsible for imparting some knowledge to beginners. $R;" +
                                    "$P now introduce [Flying Garden]! $R;" +
                                    "$P is ready, $R;" +
                                    "Just click on the [Flying Garden Rope] next to me. $R;" +
                                    "$R now let me introduce [Flying Garden]! $R;", "Masha");
        }
    }
}
