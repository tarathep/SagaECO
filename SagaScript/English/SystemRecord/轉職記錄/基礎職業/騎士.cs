using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_02
    {
        //騎士
        Choosing_to_become_a_fancer = 0x1,
        Fancer_transfer_task_completed = 0x2,
        Fencer_transferred_successfully = 0x4,
        Has_been_transferred_to_Cavaliers = 0x8,
    }
}
