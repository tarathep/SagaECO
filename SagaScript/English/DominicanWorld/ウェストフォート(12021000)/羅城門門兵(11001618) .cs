﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001618 : Event
    {
        public S11001618()
        {
            this.EventID = 11001618;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "若は、ここを動くなって言ったけど$R;" +
            "心配だな……。$R;", "羅城門門兵");
           }

           }
                        
                }
            
            
        
     
    