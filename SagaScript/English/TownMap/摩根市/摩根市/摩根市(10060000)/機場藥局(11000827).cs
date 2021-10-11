﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000827 : Event
    {
        public S11000827()
        {
            this.EventID = 11000827;

        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有什麼要幫忙的嗎？", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 170);
                    break;
                case 2:
                    OpenShopSell(pc, 170);
                    break;
                case 3:
                    break;
            }
        }
    }
}