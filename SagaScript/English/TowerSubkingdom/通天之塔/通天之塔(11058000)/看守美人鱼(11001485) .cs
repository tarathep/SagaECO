using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001485 : Event
    {
        public S11001485()
        {
            this.EventID = 11001485;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "300年前、タイタニアの王が$R;" +
            "この扉を封印し……$R;" +
            "我々ごとこの海域を封印したのだ。$R;" +
            "$Rそれ以来、タイタニアが$R;" +
            "この島に来たことはない……。$R;", "見張りのマーメイド");

            Say(pc, 131, "すまぬ。$R;" +
            "お前が悪いわけではないのだろうが$R;" +
            "タイタニアを憎む気持ちが抑えられぬ。$R;" +
            "$Rあっちに行ってくれ。$R;", "見張りのマーメイド");
        }
    }
}




