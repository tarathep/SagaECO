using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054000
{
    public class S11000795 : Event
    {
        public S11000795()
        {
            this.EventID = 11000795;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JJJRFlags> mask = pc.AMask["JJJR"];
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];
            BitMask<AYEFlags> mask_0 = new BitMask<AYEFlags>(pc.CMask["AYE"]);

            if (!mask_0.Test(AYEFlags.The_first_conversation_with_the_cabinetmaker))//_6a06)
            {
                mask_0.SetValue(AYEFlags.The_first_conversation_with_the_cabinetmaker, true);
                //_6a06 = true;
                Say(pc, 131, "Guest! $R;" +
                      "Should this be your first time here? $R;" +
                      "$R, I'm making the flying house props $R;" +
                      "The most advanced furniture craftsman $R;" +
                      "If you like these things, $R will patronize often! $R;");
                switch (Select(pc, "How do you want to do it?", "", "Buy furniture", "Do nothing"))
                {
                    case 1:
                        OpenShopBuy(pc, 155);
                        break;
                }
                return;
            }
            //if (_Xa29 && !_Xb17 && CountItem(pc, 10022700) >= 1)
            if (fgarden.Test(FGarden.Get_the_key_to_the_sky_court)
                && !mask.Test(JJJRFlags.Get_the_hut)
                && CountItem(pc, 10022700) >= 1)
            {
                Say(pc, 131, "Wow, that's the [Flying Garden Key]. $R;" +
                    "$R then means to have the Flying Garden $R;" +
                    "I'm so young now, so capable! $R;" +
                    "When I was your age $R;" +
                    "Still wandering aimlessly $R;" +
                    "The young people nowadays are amazing $R;" +
                    "$P then my big brother $R;" +
                    "Just give some gifts to my capable brother $R;" +
                    "$R is a gift $R;" +
                    "But it's not a random little thing $R;" +
                    "$P haha, give you a gift $R;" +
                    "That's one [hut]!! $R;");
                if (CheckInventory(pc, 30001200, 1))
                {
                    Say(pc, 131, "The [hut] is quite heavy, can it be moved? $R;");
                    switch (Select(pc, "How do you want to do it?", "", "First tidy things up", "Receive cabin"))
                    {
                        case 1:
                            break;
                        case 2:
                            mask.SetValue(JJJRFlags.Get_the_hut, true);
                            GiveItem(pc, 30001200, 1);
                            Say(pc, 131, "To rebuild, demolish the house or buy furniture $R;" +
                                "Welcome to our shop! $R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "Get [Shack] $R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "Go and reduce your luggage, come on again. $R;");
                return;
            }
            Say(pc, 131, "Oh furniture, don’t you look at the furniture? $R;" +
                "The price is still the same as 20 years ago! $R;");
            switch (Select(pc, "How do you want to do it?", "", "Buy furniture", "Do nothing"))
            {
                case 1:
                    OpenShopBuy(pc, 155);
                    break;
            }
        }
    }
}