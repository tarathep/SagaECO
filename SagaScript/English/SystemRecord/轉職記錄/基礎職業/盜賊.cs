using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_03
    {
        //盜賊
        Choose_to_become_a_scout = 0x1,
        Scout_transfer_task_completed = 0x2,
        scout_successfully_transferred = 0x4,
        Has_been_transferred_to_a_scout = 0x8,
    }
}
