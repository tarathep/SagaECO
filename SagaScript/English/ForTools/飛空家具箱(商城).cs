using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class S1112 : Event
    {
        public S1112()
        {
            this.EventID = 1112;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要抽哪一類家俱？", "", "小屋", "地板", "牆紙", "推進器/飛翔帆"))
            {
                case 1:
                if (CountItem(pc, 21050112) > 0)
                {
                GiveRandomTreasure(pc, "home1");
                TakeItem(pc, 21050112, 1);
                }
                    break;
                case 2:
                if (CountItem(pc, 21050112) > 0)
                {
                GiveRandomTreasure(pc, "home2");
                TakeItem(pc, 21050112, 1);
                }
                    break;
                case 3:
                if (CountItem(pc, 21050112) > 0)
                {
                GiveRandomTreasure(pc, "home3");
                TakeItem(pc, 21050112, 1);
                }
                    break;
                case 4:
                if (CountItem(pc, 21050112) > 0)
                {
                GiveRandomTreasure(pc, "home4");
                TakeItem(pc, 21050112, 1);
                }
                    break;
            }
        }
    }
}