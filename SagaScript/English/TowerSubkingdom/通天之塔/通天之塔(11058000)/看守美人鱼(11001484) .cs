using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001484 : Event
    {
        public S11001484()
        {
            this.EventID = 11001484;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "この扉が開いたときに$R;" +
            "現れるのは悪魔か化け物か。$R;" +
            "$R……そう聞いていたのだが$R;" +
            "まさか、タイタニアが現れるとは$R;" +
            "予想だにしなかったな。$R;", "見張りのマーメイド");
        }
    }
}




