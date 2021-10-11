using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
namespace SagaScript.M30060000
{
    public class S11000060 : GuildMerchant
    {
        public S11000060()
        {
            Init(11000060, 22, 100, WarehousePlace.FarEast);
        }
    }
}
