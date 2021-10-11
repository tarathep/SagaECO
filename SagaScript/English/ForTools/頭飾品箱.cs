using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class S1110 : Event
    {
        public S1110()
        {
            this.EventID = 1110;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要抽哪一類髮飾品？", "", "可愛的髮簪", "爆炸頭假髮和頭上小貓", "好看的頭帶", "學生戴的飾物", "戰國武將的帽子", "好看的頭巾", "サムライのリボン"))
            {
                case 1:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head1");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 2:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head2");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 3:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head3");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 4:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head4");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 5:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head5");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 6:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head6");
                TakeItem(pc, 21050110, 1);
                }
                    break;
                case 7:
                if (CountItem(pc, 21050110) > 0)
                {
                GiveRandomTreasure(pc, "head7");
                TakeItem(pc, 21050110, 1);
                }               
                    break;
            }
        }
    }
}