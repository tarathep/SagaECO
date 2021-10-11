using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 紅包袋 : Item
    {
        public 紅包袋()
        {
            //紅包袋
            Init(90000030, delegate(ActorPC pc)
            {
                pc.Gold += 2500;
                TakeItem(pc, 10049053, 1);
            });
        }
    }
}