using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class Appraiser : Event
    {
        public Appraiser()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.First_dialogue_with_the_appraiser))
            {
                Dialogue_with_the_appraiser_for_the_first_time(pc);
                return;
            }

            Identify(pc);
        }

        void Dialogue_with_the_appraiser_for_the_first_time(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            World_01_mask.SetValue(World_01.First_dialogue_with_the_appraiser, true);

            Say(pc, 131, "Hello! I am an [Appraiser]. $R;" +
                          "$R has a saying that the old horse knows the way. $R;" +
                          "$P I just use my long life experience, $R;" +
                          "Come to [identify] unidentified items. $R;" +
                          "When $P finds an unidentified item, $R;" +
                          "Bring me here. $R;", "Appraiser");
        }
    }
}
