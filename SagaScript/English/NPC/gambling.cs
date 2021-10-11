using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class gambling : Event
    {
        public gambling()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            switch (Select(pc, "How to play?", "", "『『ecoin』", "『』Gold", "『』Don’t bet"))
            {
                case 1:
                    if (pc.ECoin > 1)
                    {
                        Say(pc, 131, "$CR " + pc.Name + " $CD you have $R;" +
                      "$CR" + pc.ECoin + "$CD个ecoin$R;", "");
                        switch (Select(pc, "What are you playing?", "", "Seal", "Unused", "Don't bet"))
                        {
                            case 1:
                                if (pc.ECoin > 5000)
                                {
                                    if (Select(pc, "Pick it", "", "Pick", "Don't pull") == 1)
                                    {
                                        pc.ECoin -= 5000;
                                        if (SagaLib.Global.Random.Next(0, 99) < 20)
                                        {
                                            GiveRandomTreasure(pc, "100ky");
                                            return;
                                        }

                                        Say(pc, 131, "Welcome again, you have $R;" +
                                         "$CR" + pc.ECoin + "$CD个ecoin$R;", "Banker");
                                        return;
                                    }

                                }
                                else
                                {
                                    Say(pc, 131, "ecoin is not enough, you have $R;" +
                                      "$CR" + pc.ECoin + "$CD个ecoin$R;", "Banker");
                                }
                                return;
                        }
                    }
                    Say(pc, 0, 131, "$R is required to play mini games;" +
                    "Prepare more than $CR1$CD of ecoin coins. $R;" +
                    "$R first go to the $CRecoin counter $CD$R;" +
                    "Buy enough ecoin coins. $R;", "Banker");

                    return;
                case 2:
                    switch (Select(pc, "What to play?", "", "Draw", "Dice", "Don't bet"))
                    {
                        case 1:
                            if (pc.Gold > 99999)
                            {
                                pc.Gold -= 100000;
                                if (SagaLib.Global.Random.Next(0, 99) < 20)
                                {
                                    GiveRandomTreasure(pc, "kuji14");
                                    return;
                                }
                                Say(pc, 131, "RP can't do it♪$R;");
                            }
                            else
                            {
                                Say(pc, 131, "Not enough gold coins♪$R;");
                            }
                            break;
                        case 2:

                            switch (Select(pc, "Select", "", "1 point 100,000", "2 points 500,000", "3 points 1 million", "45 points 5 million", "5 points 10 million", "6 Point 50 million", "Don't bet"))
                            {
                                case 1:
                                    if (pc.Gold > 99999)
                                    {
                                        pc.Gold -= 100000;
                                        selection = Global.Random.Next(1, 6);
                                        switch (selection)
                                        {
                                            case 1:
                                                ShowEffect(pc, 4523);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Congratulations...! $R;", "");
                                                GiveItem(pc, 10009551, 1);
                                                break;
                                            case 2:
                                                ShowEffect(pc, 4524);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");
                                                break;
                                            case 3:
                                                ShowEffect(pc, 4525);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");
                                                break;
                                            case 4:
                                                ShowEffect(pc, 4526);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");
                                                break;
                                            case 5:
                                                ShowEffect(pc, 4527);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");
                                                break;
                                            case 6:
                                                ShowEffect(pc, 4528);
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 131, "Not enough gold coins♪$R;");
                                    }
                                    break;
                                case 2:
                                    if (pc.Gold > 499999)
                                    {
                                        pc.Gold -= 500000;
                                        selection = Global.Random.Next(1, 3);
                                        switch (selection)
                                        {
                                            case 1:
                                                if (SagaLib.Global.Random.Next(0, 99) < 20)
                                                {
                                                    ShowEffect(pc, 4524);//2 points
                                                    Wait(pc, 660);
                                                    GiveRandomTreasure(pc, "RPG");
                                                    Say(pc, 0, 131, "Congratulations...! $R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4523);//1 point
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");
                                                break;
                                            case 2:
                                                if (SagaLib.Global.Random.Next(0, 99) < 50)
                                                {
                                                    ShowEffect(pc, 4525);//3 points
                                                    Wait(pc, 660);
                                                    Say(pc, 0, 131, "Regret...! $R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4526);//4 points
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");

                                                break;
                                            case 3:
                                                if (SagaLib.Global.Random.Next(0, 99) < 50)
                                                {
                                                    ShowEffect(pc, 4527);//5 points
                                                    Wait(pc, 660);
                                                    Say(pc, 0, 131, "Regret...! $R;", "");
                                                    return;
                                                }
                                                ShowEffect(pc, 4528);//6 points
                                                Wait(pc, 660);
                                                Say(pc, 0, 131, "Regret...! $R;", "");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Say(pc, 131, "Not enough gold coins♪$R;");
                                    }
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                case 5:
                                    break;
                                case 6:
                                    break;
                            }


                            break;
                    }
                    return;
            }
        }
    }
}