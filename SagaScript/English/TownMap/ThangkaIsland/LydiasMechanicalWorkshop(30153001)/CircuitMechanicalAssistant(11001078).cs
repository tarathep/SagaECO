using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30153001
{
    public class S11001078 : Event
    {
        public S11001078()
        {
            this.EventID = 11001078;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10012300) >= 1)
            {
                Say(pc, 131, "Give me that [green peridot] $R;");
                switch (Select(pc, "What to do?", "", "Don't give it", "Give it to him"))
                {
                    case 1:
                        break;
                    case 2:
                        if (!CheckInventory(pc, 10030100, 1))
                        {
                            Say(pc, 131, "Thank you so much $R;" +
                                "I don't know what to say $R;" +
                                "$R is not a valuable thing, $R;" +
                                "This is my heart, please accept it. $R;" +
                                "$R, there are too many luggages $R;");
                            return;
                        }
                        Say(pc, 131, "Thank you so much $R;" +
                            "Thank you sincerely, $R;" +
                            "$R this is my replacement part, $R doesn't know if it fits, $R;" +
                            "If you can, please accept it. $R;");
                        switch (Select(pc, "Which component is good?", "", "Engine", "Tayi model", "Ultra-microcomputer"))
                        {
                            case 1:
                                GiveItem(pc, 10030100, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "Got the [engine] $R;");
                                break;
                            case 2:
                                GiveItem(pc, 10030002, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "Get the [Tayi Model] $R;");
                                break;
                            case 3:
                                GiveItem(pc, 10030200, 1);
                                TakeItem(pc, 10012300, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "Got the [Ultra - microcomputer] $R;");
                                break;
                        }
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 131, "Please, please $R;" +
                      "$R gem...Yes! $R;" +
                      "Even if there are only gems, the mood might get better. $R;" +
                      "$P is good even for cheap gems. $R;" +
                      "$R emerald green peridot... by the way!! $R;" +
                      "Can you give me emerald peridot? $R;");
                return;
            }
            Say(pc, 131, "trembling......$R;");
        }
    }
}