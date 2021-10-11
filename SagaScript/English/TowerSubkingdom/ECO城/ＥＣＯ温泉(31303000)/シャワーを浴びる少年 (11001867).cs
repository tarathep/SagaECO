using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001867 : Event
    {
        public S11001867()
        {
            this.EventID = 11001867;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 0, "うぅ、ゴーグルを持ってくるんだった。$R;", "シャワーを浴びる少年");
        }


    }
}


