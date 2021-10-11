using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001870 : Event
    {
        public S11001870()
        {
            this.EventID = 11001870;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "いやぁ、あなたも$R;" +
            "この「キリオン」を見に来たのですか？$R;" +
            "$Rこのキリオン、タイタニア世界を$R;" +
            "象徴する、神話上の生物なんですよ。$R;", "物知りデバイ");
        }


    }
}


