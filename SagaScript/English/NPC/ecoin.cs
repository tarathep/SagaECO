using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class ecoin : Event
    {
        public ecoin()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "Do you have any questions?!", "", "No need", "『ecoin』buy", "Deposit money in the bank", "Withdraw money from the bank"))
            {
                case 2:
                    Say(pc, 131, "$CR " + pc.Name + " $CDYou now have$R;" +
                    "$CR" + pc.ECoin + "$CD ecoin.$R;", "ecoin counter");

                    Say(pc, 131, "ecoin Exchange cannot exceed 999999$R;" +
                    "Otherwise it cannot be exchanged. $R;" +
                    "$R$CR1$CD ecoin need $CR100$CDgold$R;" +
                    "How many pieces do I need to buy?$R;", "ecoin counter");
                    string temp = InputBox(pc, "Enter the number of pieces to be purchased", InputType.Bank);
                    if (temp != "")
                    {
                        uint ecop = uint.Parse(temp);
                        Say(pc, 131, "Buy" + temp + "coin$R;" +
                       "Need $CR" + temp + "00$CDgold. $R;", "ecoin counter");
                        if (Select(pc, "What to do?", "", "Buy", "Don't buy") == 1)
                        {
                            if (pc.Gold >= (ecop * 100))
                            {
                                pc.Gold -= (int)(ecop * 100);
                                pc.ECoin += ecop;
                                Say(pc, 131, "Purchased $R;" +
                                "$CR" + temp + "$CD coins.$R;", "ecoin counter");
                            }
                            else
                            {
                                Say(pc, 131, "Not enough money...$R;", "ecoin counter");
                            }
                        }
                    }
                    break;
                case 3:
                    BankDeposit(pc);
                    break;

                case 4:
                    BankWithdraw(pc);
                    break;
            }
            Say(pc, 11001724, 131, "Then please continue $R;" +
            "Enjoy ECO City! $R;", "ecoin counter");
        }
    }
}