using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_01
    {
        //劍士
        Choose_to_become_a_swordsman = 0x1,
        Swordsman_transfer_task完成 = 0x2,
        Swordsman_changed_job_successfully = 0x4,
        Has_been_transferred_to_a_swordsman = 0x8,
    }
}
