using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:歷奇號(11001409) X:222 Y:61
namespace SagaScript.M10018102
{
    public class S11001409 : Event
    {
        public S11001409()
        {
            this.EventID = 11001409;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001409, 500, "Um...$R;", "Carrier Ricky");

            Say(pc, 11001408, 131, "Ah, I'm sorry, maybe I saw a strange face $R;" +
                                   "So I'm a little scared $R;" +
                                   "$R this kid is called Adventure, it’s my partner $R;" +
                                   "$P is the best for the businessman, $R;" +
                                   "See the person with this guy, $R;" +
                                   "Just talk to him $R;" +
                                   "$R may give you a hint $R;", "Mr. Ricky");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001409, 0, "Um...$R;", "Carrier Ricky");
        }  
    }
}
