using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:汪汪(11000924) X:201 Y:89
namespace SagaScript.M10018101
{
    public class S11000924 : Event
    {
        public S11000924()
        {
            this.EventID = 11000924;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11000924, 0, "汪汪!$R;", "汪汪");

            Say(pc, 11000923, 131, "這隻像狗的寵物叫「汪汪」，$R;" +
                                   "除了牠還有別的種類。$R;" +
                                   "$R根據職業，$R;" +
                                   "寵物在作戰時，也可以幫忙喔!!$R;", "寵物養殖研究員");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11000924, 0, "汪汪!$R;", "汪汪");
        }  
    }
}
