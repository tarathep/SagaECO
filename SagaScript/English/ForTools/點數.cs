using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 點數 : Item
    {
        public 點數()
        {
            //點數
            Init(1122, delegate(ActorPC pc)
            {
                pc.VShopPoints += 250;
                TakeItem(pc, 21050129, 1);
            });
        }
    }
}