using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:汪汪(11001404) X:201 Y:89
namespace SagaScript.M10018102
{
    public class S11001404 : Event
    {
        public S11001404()
        {
            this.EventID = 11001404;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001404, 0, "Wow!$R;", "Wow");

            Say(pc, 11001403, 131, "This dog-like pet is called [Puppy], $R;" +
                                   "There are other types besides it. $R;" +
                                   "$R according to occupation, $R;" +
                                   "When pets are fighting, you can also help!!$R;", "Pet Training Center");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001404, 0, "Wow!$R;", "Wow");
        }  
    }
}
