using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_04
    {
        //弓手
        Choose_to_become_an_archer = 0x1,
        Archer_transfer_mission_completed = 0x2,
        Archer_changed_job_successfully = 0x4,
        Has_been_converted_to_archer = 0x8,
    }
}
