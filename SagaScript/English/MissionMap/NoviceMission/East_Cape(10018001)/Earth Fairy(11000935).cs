using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018001) NPC基本信息:大地之精靈(11000935) X:43 Y:95
namespace SagaScript.M10018001
{
    public class S11000935 : Event
    {
        public S11000935()
        {
            this.EventID = 11000935;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000935, 131, "Hello!$R;" +
                                   "$R I am Earth Fairy, $R;" +
                                   "It's the spirit of the earth! $R;" +
                                   "What's the matter with $P? $R;", "Earth Spirit");

            selection = Select(pc, "What do you want to ask?", "", "What is [element]", "What are you doing?", "Do not ask anything");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000935, 131, "Is that so? $R;" +
                                               "$R I am the spirit in charge of the power of [earth]. $R;" +
                                               "Does $P know the [earth element]? $R;" +
                                               "$P has various [elements] in this world. $R;" +
                                               "$P has fire/water/wind/soil $R;" +
                                               "There are light and dark elements! $R;" +
                                               "Most of the $P elements are highlighted in the region. $R;" +
                                               "$R is occasionally also on monsters, weapons or armors. $R;" +
                                               "$P Hot Place$R;" +
                                               "[Fire] has a strong element, $R;" +
                                               "$R cold place$R;" +
                                               "[Water] has a strong element $R;" +
                                               "$P windy place$R;" +
                                               "[Wind] has a strong element, $R;" +
                                               "$R green shade place$R;" +
                                               "[Soil] has strong elements. $R; " +
                                               "$P has no power relationship between them. $R;" +
                                               "$R but each element has a special occupation, $R;" +
                                               "You can ask them directly. $R;", "Earth Fairy");
                        break;

                    case 2:
                        Say(pc, 11000935, 131, "Around the elves in charge of elements, $R;" +
                                               "There will be [crystals] with the same elements! $R;" +
                                               "$P look for it when you come next time? $R;" +
                                               "$P can be used from the crystal, $R;" +
                                               "The extracted [Summon Stone], $R;" +
                                               "Come to infuse [some] elements! $R;" +
                                               "$P, if you need it, please feel free to come over. $R;" +
                                               "$P is in charge of sprites with other elements, $R;" +
                                               "Also has the same ability! $R;", "Earth Fairy");
                        break;

                    case 3:
                        Say(pc, 11000935, 131, "Really? $R;" +
                                               "If you need it, come here anytime! $R;", "Earth Fairy");
                        return;
                }

                selection = Select(pc, "What do you want to ask?", "", "What is [elf]", "What are you doing?", "Do not ask anything");
            }
        }
    }
}
