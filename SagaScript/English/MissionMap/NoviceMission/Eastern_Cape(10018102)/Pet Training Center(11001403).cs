using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:寵物養殖研究員(11001403) X:203 Y:88
namespace SagaScript.M10018102
{
    public class S11001403 : Event
    {
        public S11001403()
        {
            this.EventID = 11001403;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001403, 131, "Hello, $R;" +
                                     "Are you interested in these kids? $R;", "Pet Training Center");

            selection = Select(pc, "Do you want to hear about pets?", "", "Listen to pets' instructions", "Listen to the precautions about keeping pets", "Don't listen now");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001403, 131, "In ECO, you can also keep pets $R;" +
                                               "$P has many other pets besides here $R;" +
                                               "$R how about adventuring with your favorite pet? $R;" +
                                               "$P with pets $R;" +
                                               "Can improve specific ability $R;" +
                                               "$R or fight together in battle $R;" +
                                               "$P also has [riding pets] that can be ridden $R;" +
                                               "Fire Dragon next to $R and $R;" +
                                               "[Crawling Reptile Latin] is [riding pet] $R;" +
                                               "Finally there is a pet named [Kaiti] $R;" +
                                               "Have you seen $P when talking to Emil? $R;" +
                                               "It's the kid floating next to Emil $R;" +
                                               "It's called [Kaiti] $R;" +
                                               "There are other pets, look for them yourself $R;", "Pet Training Center");
                        break;

                    case 2:
                        Say(pc, 11001403, 131, "Keeping pets is also related to occupation $R;" +
                                               "$R can make it attack $R;" +
                                               "But you cannot use the pet's [skill] $R;" +
                                               "$R...there are other situations $R;" +
                                               "$P But, I want to be with it, $R;" +
                                               "Cultivating feelings is the most important $R;" +
                                               "[My favorite pet] is the best $R;" +
                                               "$R raise it with feelings $R;" +
                                               "$P A pet raised with emotion $R;" +
                                               "Be stronger in [battle], $R;" +
                                               "$P still has that situation $R;" +
                                               "$R has suitable places to keep pets, $R;" +
                                               "Please refer to the instructions when raising $R;", "Pet Training Center");
                        break;
                }

                selection = Select(pc, "Do you want to hear about pets?", "", "Listen to pets' instructions", "Listen to the precautions about keeping pets", "Don't listen now");
            }
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001403, 131, "What? Have you heard Emil's explanation? $R;", "Pet Training Center");
        } 
    }
}
