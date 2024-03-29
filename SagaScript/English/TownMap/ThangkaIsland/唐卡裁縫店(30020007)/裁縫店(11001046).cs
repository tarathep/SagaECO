﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30020007
{
    public class S11001046 : Event
    {
        public S11001046()
        {
            this.EventID = 11001046;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "welcome！", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 208);
                    break;
                case 2:
                    OpenShopSell(pc, 208);
                    break;
                case 3:
                    break;
            }
        }
    }
}