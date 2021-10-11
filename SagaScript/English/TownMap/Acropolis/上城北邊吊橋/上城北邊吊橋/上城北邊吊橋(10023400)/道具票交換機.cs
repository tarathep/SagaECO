using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城北邊吊橋(10023400) NPC基本信息:ItemTicketExchange
namespace SagaScript.M10023400
{
    public class S12001014 : ItemTicketExchange
    {
        public S12001014()
        {
            this.EventID = 12001014;
        }
    }

    public class S12001015 : ItemTicketExchange
    {
        public S12001015()
        {
            this.EventID = 12001015;
        }
    }
}
