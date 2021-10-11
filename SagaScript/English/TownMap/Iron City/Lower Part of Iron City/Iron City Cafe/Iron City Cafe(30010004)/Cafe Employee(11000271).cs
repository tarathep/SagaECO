using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010004
{
    public class S11000271 : Event
    {
        public S11000271()
        {
            this.EventID = 11000271;

            this.notEnoughQuestPoint = "Come and see. $R;" +
                                       "$R is fine now, need to help $R;" +
                                       "It's better to take a risk first $R;";
            this.leastQuestPoint = 1;
            this.questFailed = "……$P failed? $R;" +
                               "$R must be sad $R;" +
                               "I don't know what to say, $R;" +
                               "$P can't do anything this time $R;" +
                               "Success next time! You know? $R;";
            this.alreadyHasQuest = "Did the task go well? $R;";
            this.gotNormalQuest = "Then please $R;" +
                                  "$R mission is over $R;" +
                                  "Come and tell me again $R;";
            this.gotTransportQuest = "Have you collected all of them? $R;";
            this.questCompleted = "Thank you. $R;" +
                                  "The task of $R is successful. Get paid for $R. $R;";
            this.transport = "Oh oh... have you received it all?;";
            this.questCanceled = "I thought if it was you $R;" +
                                 "It will be done...$R;";
            this.questTooEasy = "This may be too easy for you. $R;" +
                                "$R is okay? $R;";
            this.questTooHard = "It may be too difficult for you...$R;" +
                                "Can you? $R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "Welcome to the second shop of the cafe!", "", "Buy something", "Sell something", "Task desk", "Do nothing"))
            {
                case 1:
                    OpenShopBuy(pc, 4);
                    Say(pc, 111, "Come and play again! $R;");
                    break;
                case 2:
                    OpenShopSell(pc, 4);
                    Say(pc, 111, "Come and play again! $R;");
                    break;
                case 3:
                    HandleQuest(pc, 6);
                    break;
                case 4:
                    Say(pc, 111, "Come and play again! $R;");
                    break;
            }
        }
    }
}