using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:帕斯特國境警備員(11001402) X:235 Y:77
namespace SagaScript.M10018102
{
    public class S11001402 : Event
    {
        public S11001402()
        {
            this.EventID = 11001402;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001402, 131, "Hello!$R;" +
                                   "$R this bridge connects to Fareast City, $R;" +
                                   "But it can't pass now! $R;" +
                                   "$P open perspective of Fareast City! $R;" +
                                   "The expansive view of $R is spectacular!! $R;" +
                                   "If you want to go, improve your strength first! $R;" +
                                   "$P Whoops! $R;" +
                                   "Are you still not sure about this world? $R;" +
                                   "The world of $R consists of a big continent $R;" +
                                   "[Oclunia Mainland] and $R;" +
                                   "There are many islands around you! $R;" +
                                   "$P The central part of the Acronia continent is [Acropolis], $R;" +
                                   "$R go there first to increase your strength! $R;" +
                                   "$P here is the east side of mainland Acronia, $R;" +
                                   "It's called [East Cape.] $R;" +
                                   "$R in the small map window$R;" +
                                   "Watch the map of the entire continent, $R;" +
                                   "You can find your current location right away! $R;" +
                                   "Do you know how to use the $P minimap? $R;" +
                                   "Go to the Dominion girl over there, $R;" +
                                   "She will tell you carefully. $R;" +
                                   "$P looks at the map of the entire continent, $R;" +
                                   "Listen to her explanation! $R;" +
                                   "The bridge next to $R $R;" +
                                   "It is to connect the mainland of Acronia $R;" +
                                   "And the bridge of [Fareast City]. $R;" +
                                   "$P $R from the Acronia continent;" +
                                   "North is [Norton Island], $R;" +
                                   "South of $R is [South Iron Vocano], $R;" +
                                   "The southwest of $R is [Morg City.] $R;" +
                                   "After the strength of $P increases, go and see one by one! $R;", "Fareast Border Guard");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001402, 131, "Do you want to talk to Emil so soon? $R;", "Fareast Border Guard");
        }  
    }
}
