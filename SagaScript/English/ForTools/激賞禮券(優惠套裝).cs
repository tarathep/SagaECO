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
            //�E��§�� 1�u�f�M��
            Init(1101, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050100, 6);
                TakeItem(pc, 21050101, 1);
            });

            //�E��§�� 2�u�f�M��
            Init(1103, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050102, 6);
                TakeItem(pc, 21050103, 1);
            });

            //�E��§�� 3�u�f�M��
            Init(1105, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050104, 6);
                TakeItem(pc, 21050105, 1);
            });

            //�E��§�� 4�u�f�M��
            Init(1107, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050106, 6);
                TakeItem(pc, 21050107, 1);
            });

            //�E��§�� 5�u�f�M��
            Init(1109, delegate(ActorPC pc)
            {
                GiveItem(pc, 21050108, 6);
                TakeItem(pc, 21050109, 1);
            });
        }
    }
}