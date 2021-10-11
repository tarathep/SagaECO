using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
namespace SagaScript.M10060000
{
    public class S11000064 : GuildMerchant
    {
        public S11000064()
        {
            Init(11000064, 175, 100, WarehousePlace.Morg);
        }
    }
}