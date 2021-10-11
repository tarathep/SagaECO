using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class RewardVoucher : Item
    {
        public RewardVoucher()
        {
            //¿E½àÂ§¨é 1Àu´f®M¸Ë
            Init(1101, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050100, 6);
                TakeItem(pc, 21050101, 1);
            });

            //¿E½àÂ§¨é 2Àu´f®M¸Ë
            Init(1103, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050102, 6);
                TakeItem(pc, 21050103, 1);
            });

            //¿E½àÂ§¨é 3Àu´f®M¸Ë
            Init(1105, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050104, 6);
                TakeItem(pc, 21050105, 1);
            });

            //¿E½àÂ§¨é 4Àu´f®M¸Ë
            Init(1107, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050106, 6);
                TakeItem(pc, 21050107, 1);
            });

            //¿E½àÂ§¨é 5Àu´f®M¸Ë
            Init(1109, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050108, 6);
                TakeItem(pc, 21050109, 1);
            });
        }
    }
}