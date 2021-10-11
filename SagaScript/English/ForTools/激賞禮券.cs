using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 激賞禮券 : Item
    {
        public 激賞禮券()
        {
            //激賞禮券 1
            Init(1100, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "LOTTO1");
                TakeItem(pc, 21050100, 1);
            });

            //激賞禮券 2
            Init(1102, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "LOTTO2");
                TakeItem(pc, 21050102, 1);
            });

            //激賞禮券 3
            Init(1104, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "LOTTO3");
                TakeItem(pc, 21050104, 1);
            });

            //激賞禮券 4
            Init(1106, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "LOTTO4");
                TakeItem(pc, 21050106, 1);
            });

            //激賞禮券 5
            Init(1108, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "LOTTO5");
                TakeItem(pc, 21050108, 1);
            });
        }
    }
}