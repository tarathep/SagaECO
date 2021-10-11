using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:曼陀蘿胡蘿蔔(11001405) X:204 Y:90
namespace SagaScript.M10018102
{
    public class S11001405 : Event
    {
        public S11001405()
        {
            this.EventID = 11001405;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001405, 131, "…♪$R;", "Mandra carrot");

            Say(pc, 11001403, 131, "This pet is called [Mandra carrot]. $R;" +
                                   "$R is very hard to raise, $R;" +
                                   "But for occupations related to plants, $R;" +
                                   "It is very helpful! $R;", "Pet Training Center");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001405, 131, "…♪$R;", "Mandra carrot");
        } 
    }
}
