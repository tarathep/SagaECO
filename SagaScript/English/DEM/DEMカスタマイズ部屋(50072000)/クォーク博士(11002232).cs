using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

using SagaLib;
namespace SagaScript.M50072000
{
    public class S11002232 : Event
    {
        public S11002232()
        {
            this.EventID = 11002232;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);

            Say(pc, 131, "啊,你來了。$R;" + 
            "要什麼嗎？$R;", "クォーク博士");
            switch (Select(pc, "想怎麼辦？", "", "成本限制提升", "部件定制", "ＤＥＭＩＣ", "購買部件", "購買晶片", "EP電池(未實裝)", "零件強化(未實裝)", "零件融合(未實裝)", "什麼都沒有"))
            {
                case 1:
                    DEMCL(pc);
                    break;
                case 2:
                    DEMParts(pc);
                    break;
                case 3:
                    DEMIC(pc);
                    break;
                case 4:
                    Say(pc, 131, "購買零件。$R;" +
                    "$R因為資金困難的緣故..$R;" +
                    "因此會貴一點$R;" +
                    "那麼。$R;" +
                    "$P你想購買、$R;" +
                    "什麼部件？$R;", "クォーク博士");
                    switch (Select(pc, "……。", "", "購買手臂部件", "購買其他部件", "購入しない"))
                    {
                        case 1:
                            OpenShopBuy(pc, 292);
                            break;
                        case 2:
                            OpenShopBuy(pc, 293);
                            break;
                    }
                    break;
                case 5:
                    DEMChipShop(pc);
                    break;
            }
        }
    }
}
