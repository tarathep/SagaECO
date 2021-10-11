using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10024000
{
    public class S11001975 : Event
    {
        public S11001975()
        {
            this.EventID = 11001975;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Iris_1> Iris_1_mask = new BitMask<Iris_1>(pc.CMask["Iris_1"]);
            //int selection;
            if (Iris_1_mask.Test(Iris_1.第一次对话后))
            {
                Say(pc, 131, "啊！辛苦你了$R;" +
                "去研究所？$R;", "壊し屋バキア");
                if (Select(pc, "去研究所？", "", "去", "不去") == 1)
                {
                    Warp(pc, 30166000, 9, 16);
                    return;
                }
                Say(pc, 131, "不去。$R;" +
                "偶然都來一下嘛、$R;" +
                "都會很歡迎你的$R;", "壊し屋バキア");

            }
        }
    }
}