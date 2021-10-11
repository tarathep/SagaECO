using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:trash_can
namespace SagaScript.M10023200
{
    public class S12000006 : trash_can 
    {
        public S12000006() 
        { 
            this.EventID = 12000006; 
        } 
    }

    public class S12000008 : trash_can 
    {
        public S12000008() 
        { 
            this.EventID = 12000008; 
        } 
    }
}
