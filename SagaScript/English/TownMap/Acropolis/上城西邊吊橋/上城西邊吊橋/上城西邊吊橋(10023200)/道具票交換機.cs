using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:ItemTicketExchange
namespace SagaScript.M10023200
{
    public class S12001012 : ItemTicketExchange
    {
        public S12001012()
        {
            this.EventID = 12001012;
        }
    }

    public class S12001013 : ItemTicketExchange
    {
        public S12001013()
        {
            this.EventID = 12001013;
        }
    }
}
