using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:GuildMerchant(11000624) X:239 Y:133
namespace SagaScript.M10023100
{
    public class S11000624 : GuildMerchant
    {
        public S11000624()
        {
            Init(11000624, 1, 0, WarehousePlace.Acropolis);
        }
    }
}
