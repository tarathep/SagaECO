using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:trash_can
namespace SagaScript.M10023100
{
    public class S12000014 : trash_can 
    {
        public S12000014() 
        { 
            this.EventID = 12000014; 
        } 
    }

    public class S12000016 : trash_can 
    {
        public S12000016() 
        { 
            this.EventID = 12000016; 
        } 
    }
}
