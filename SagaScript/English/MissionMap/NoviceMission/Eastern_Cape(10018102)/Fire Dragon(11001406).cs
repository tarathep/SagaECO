using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:火紅德拉古(11001406) X:202 Y:88
namespace SagaScript.M10018102
{
    public class S11001406 : Event
    {
        public S11001406()
        {
            this.EventID = 11001406;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001406, 0, "…$R;", "Fire Dragon");

            Say(pc, 11001403, 131, "This pet is called [Fire Dragon], $R;" +
                                   "It's a kind of [riding pet]! $R;" +
                                   "$R moves very fast, $R;" +
                                   "So it's very convenient to go far away! $R;" +
                                   "$P heard that it is in Norton Island in the northern part of mainland Acronia, $R;" +
                                   "I found its egg!! $R;", "Pet Training Center");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001406, 0, "…$R;", "Fire Dragon");
        }  
    }
}
