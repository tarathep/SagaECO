﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M32003001
{
    public class S11001720 : Event
    {
        public S11001720()
        {
            this.EventID = 11001720;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "う、うわぁぁぁっ！！$R;" +
            "て、てて敵が攻めてきた！？$R;" +
            "$Rだ、誰か助けて～！$R;", "攻防戦受付・グルー");

        }

    }

}
            
            
        
     
    