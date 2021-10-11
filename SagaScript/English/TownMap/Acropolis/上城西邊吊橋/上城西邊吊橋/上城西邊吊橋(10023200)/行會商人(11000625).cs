using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:GuildMerchant(11000625) X:16 Y:122
namespace SagaScript.M10023200
{
    public class S11000625 : GuildMerchant
    {
        public S11000625()
        {
            Init(11000625, 1, 0, WarehousePlace.Acropolis);
        }
    }
}
