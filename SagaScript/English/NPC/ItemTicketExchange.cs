using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class ItemTicketExchange : Event
    {
        public ItemTicketExchange()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2559, false, 100, 50);

            switch (Select(pc, "Welcome", "", "Exchange item ticket", "Check how to use", "Do nothing"))
            {
                case 1:
                    Say(pc, 0, 65535, "Wait a minute! $R;" +
                                      "Confirm $R; before exchange" +
                                      "Weight/volume/quantity held, $R;" +
                                      "Exchange in the lightest state. $R;" +
                                      "$R because of weight/volume/quantity held $R;" +
                                      "Items that exceed the standard and cannot be accepted, $R;" +
                                      "The item ticket will not be restored, $R;" +
                                      "Please pay attention! $R;", "");

                    //The input window has not been created yet
                    Say(pc, 0, 65535, "$R has not been implemented yet;", "");

                    Say(pc, 0, 65535, "The input number is incorrect!$R;" +
                                      "$R please enter the number again. $R;", "");
                    break;

                case 2:
                    Say(pc, 0, 65535, "[Item Ticket Exchange] uses [Item Ticket], $R;" +
                                      "The machine that exchanges the items written on the ticket. $R;" +
                                      "$P put the item ticket you have, $R;" +
                                      "Push in the red arrow above, $R;" +
                                      "Be careful not to bend, just put it in. $R;" +
                                      "$P and enter the [number] printed on the ticket, $R;" +
                                      "Then in the glass box in the machine, $R;" +
                                      "Just receive the props. $R;", "");
                    break;

                case 3:
                    break;
            }
        }
    }
}
