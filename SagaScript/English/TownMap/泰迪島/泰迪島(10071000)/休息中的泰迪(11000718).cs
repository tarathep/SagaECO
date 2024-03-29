﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:休息中的泰迪(11000718) X:56 Y:214
namespace SagaScript.M10071000
{
    public class S11000718 : Event
    {
        public S11000718()
        {
            this.EventID = 11000718;
        }

        public override void OnEvent(ActorPC pc)
        {
                    if (CountItem(pc, 10050300) >= 2)
                    {
                        Say(pc, 131, "咦…你持有我最喜愛的泰迪徽章$R;" +
                            "$P我用這個奇怪的蛋和你交換3個可以嗎?$R;", "休息中的泰迪");
                switch (Select(pc, "蛋…", "", "火紅色的", "火紅色的", "沒什麼"))
                {
                    case 1:
                        TakeItem(pc, 10050300, 3);
                        GiveItem(pc, 10050852, 1);
                        break;
                    case 2:
                        TakeItem(pc, 10050300, 3);
                        GiveItem(pc, 10050851, 1);
                        break;
                }
                    }
                                    else
                        {
            Say(pc, 11000718, 131, "唉，休息一會兒吧。$R;", "休息中的泰迪");
                        }
        }
    }
}




