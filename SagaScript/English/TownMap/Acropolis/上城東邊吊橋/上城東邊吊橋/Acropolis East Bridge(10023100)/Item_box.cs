using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:ItemBox
namespace SagaScript.M10023100
{
    public class S12001004 : Event
    {
        public S12001004()
        {
            this.EventID = 12001004;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, 131, "What powerful props will there be? $R;" +
                "It's the ItemBox that people are looking forward to! $R;");
            /*if(//ME.ITEMSLOT_EMPTY <1) Check if there is space on the player
            {
                 Call(EVT1200100402);
             return;
            }*/
            switch (Select(pc, "What badge to put?", "", "1 copper badge", "Not interested"))
            {
                case 1:
                    if (CountItem(pc, 10009500) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "What kind of handle do you want?", "", "[Lovely]", "[Handsome]", "[Practical]"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_A1");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "Get props! $R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_A2");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "Get props! $R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "COPPER_A3");
                                TakeItem(pc, 10009500, 1);
                                Say(pc, 0, 131, "Get props! $R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "There is no copper badge $R;");
                    break;
                case 2:
                    break;
            }
        }
    }

    public class S12001005 : Event
    {
        public S12001005()
        {
            this.EventID = 12001005;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);
            Say(pc, 0, 131, "What powerful props will there be? $R;" +
                 "It's the ItemBox that people are looking forward to! $R;");
            /*if(//ME.ITEMSLOT_EMPTY <1) Check if there is space on the player
            {
                 Call(EVT1200100402);
             return;
            }*/
            switch (Select(pc, "What badge to put?", "", "1 silver badge", "not interested"))
            {
                case 1:
                    if (CountItem(pc, 10009600) >= 1)
                    {
                        PlaySound(pc, 2060, false, 100, 50);
                        switch (Select(pc, "What kind of handle do you want?", "", "[Lovely]", "[Handsome]", "[Practical]"))
                        {
                            case 1:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "SILVER_C1");
                                TakeItem(pc, 10009600, 1);
                                Say(pc, 0, 131, "Get props! $R;");
                                break;
                            case 2:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "SILVER_C2");
                                TakeItem(pc, 10009600, 1);
                                Say(pc, 0, 131, "Get props! $R;");
                                break;
                            case 3:
                                PlaySound(pc, 2429, false, 100, 50);
                                Wait(pc, 1000);
                                GiveRandomTreasure(pc, "SILVER_C3");
                                TakeItem(pc, 10009600, 1);
                                Say(pc, 0, 131, "Get props! $R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 0, 131, "There is no silver badge $R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
