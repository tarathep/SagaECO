using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class WRPranking : Event
    {
        public WRPranking()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
        	
             ShowUI(pc, UIType.WRPRanking);
        			
        		
        }
    }
}