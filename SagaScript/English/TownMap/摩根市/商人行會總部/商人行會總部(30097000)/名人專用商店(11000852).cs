﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30097000
{
    public class S11000852 : Event
    {
        public S11000852()
        {
            this.EventID = 11000852;

        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_6a69)
            {
                Say(pc, 131, "客人…客人…$R;" +
                    "這個月的銷售情況不太好$R;" +
                    "$R能不能買點東西呢？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "看袋子", "看防具", "賣東西", "什麼也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 183);
                        break;
                    case 2:
                        OpenShopBuy(pc, 200);
                        break;
                    case 3:
                        OpenShopSell(pc, 183);
                        break;
                    case 4:
                        break;
                }
                Say(pc, 131, "太謝謝了$R;");
            }
            */

            int a = pc.Level * 1000;
            if (pc.Gold < a)
            {
                Say(pc, 131, "哎呀…要借錢呀？$R;" +
                    "$P啊？…不是？$R;" +
                    "$R哎呀…對不起$R;" +
                    "看您的錢包有點輕，所以$R;" +
                    "對不起，我搞錯了…$R;");
                return;
            }
            switch (Select(pc, "welcome", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 179);
                    Say(pc, 131, "哎呀！客人！$R;" +
                        "這麼快就看完了？$R;" +
                        "$R這邊的東西很適合客人您的唷$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "好了", "什麼東西？"))
                    {
                        case 1:
                            Say(pc, 131, "謝謝$R;" +
                                "下次再光臨唷$R;");
                            break;
                        case 2:
                            OpenShopBuy(pc, 179);
                            break;
                    }
                    break;
                case 2:
                    OpenShopSell(pc, 179);
                    Say(pc, 131, "謝謝$R;" +
                        "下次再光臨唷$R;");
                    break;
                case 3:
                    break;
            }
        }
    }
}