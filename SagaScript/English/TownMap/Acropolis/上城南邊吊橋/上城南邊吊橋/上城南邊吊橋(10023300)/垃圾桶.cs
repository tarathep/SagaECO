using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:trash_can
namespace SagaScript.M10023300
{
    public class S12000010 : trash_can 
    {
        public S12000010() 
        { 
            this.EventID = 12000010; 
        } 
    }

    public class S12000012 : trash_can 
    {
        public S12000012() 
        { 
            this.EventID = 12000012; 
        } 
    }
}
