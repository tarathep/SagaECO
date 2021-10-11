using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:汪汪(11001418) X:201 Y:89
namespace SagaScript.M10018103
{
    public class S11001418 : Event
    {
        public S11001418()
        {
            this.EventID = 11001418;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001418, 0, "汪汪!$R;", "汪汪");

            Say(pc, 11001417, 131, "這隻像狗的寵物叫「汪汪」，$R;" +
                                   "除了牠還有別的種類。$R;" +
                                   "$R根據職業，$R;" +
                                   "寵物在作戰時，也可以幫忙喔!!$R;", "寵物養殖研究員");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001418, 0, "汪汪!$R;", "汪汪");
        }  
    }
}
