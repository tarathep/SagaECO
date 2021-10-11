using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:trash_can
namespace SagaScript.M10022000
{
    public class S12000003 : trash_can
    {
        public S12000003()
        {
            this.EventID = 12000003;
        }
    }

    public class S12000004 : trash_can
    {
        public S12000004()
        {
            this.EventID = 12000004;
        }
    }
}
