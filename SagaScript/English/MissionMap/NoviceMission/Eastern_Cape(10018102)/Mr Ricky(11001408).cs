using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:利基先生(11001408) X:220 Y:65
namespace SagaScript.M10018102
{
    public class S11001408 : Event
    {
        public S11001408()
        {
            this.EventID = 11001408;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001408, 131, "Hey, did everything go well? $R;" +
                                   "$R here is my father's shop, $R;" +
                                   "Next time you come to Dongfang Cape, you must come! $R;" +
                                   "Did you see the sign of the $P store? $R;" +
                                   "Most of the stores of $R merchants have such signs. $R;" +
                                   "$P like this to the city where I went for the first time, $R;" +
                                   "You can also know what stores those are right away. $R;" +
                                   "$P, look at the shop on the right also has a sign! $R;" +
                                   "$R That is [Item Refiner] $R;" +
                                   "And the sign of [Appraiser]. $R;" +
                                   "$P [Weapon Shop] and [Antique Shop] $R;" +
                                   "There are also designated signs! $R;" +
                                   "$R go to Acropolis to confirm, $R;" +
                                   "It will be helpful for your journey. $R;", "Mr. Ricky");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001408, 131, "Have you not said hello to Emil? $R;" +
                                    "Go to Emil first! $R;", "Mr. Ricky");
        }  
    }
}
