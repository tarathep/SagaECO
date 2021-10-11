using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:trash_can
namespace SagaScript.M10023400
{
    public class S12000013 : trash_can 
    {
        public S12000013() 
        { 
            this.EventID = 12000013; 
        } 
    }

    public class S12000015 : trash_can 
    {
        public S12000015() 
        { 
            this.EventID = 12000015; 
        } 
    }
}