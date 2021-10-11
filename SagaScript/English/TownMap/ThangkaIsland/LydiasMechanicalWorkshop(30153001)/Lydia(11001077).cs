using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30153001
{
    public class S11001077 : Event
    {
        public S11001077()
        {
            this.EventID = 11001077;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "Oh, it's true, why not? $R;" +
                 "What's wrong with $R? $R;" +
                 "It's a mess. $R;" +
                 "Ah, I'm so angry. $R;" +
                 "$P wow, ah, $R;" +
                 "Can't hear, can't hear. $R;" +
                 "$R is angry!!!!!! $R;");
        }
    }
}