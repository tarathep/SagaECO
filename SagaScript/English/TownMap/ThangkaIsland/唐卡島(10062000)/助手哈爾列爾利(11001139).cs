﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001139 : Event
    {
        public S11001139()
        {
            this.EventID = 11001139;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "嗯?您也想在這裡工作嗎？$R;");
                return;
            }
            Say(pc, 131, "主人！！！$R;" +
                "這個可以放這裡嗎？$R;");
            Say(pc, 11001025, 131, "啊，是嗎$R;" +
                "多謝！！$R;");
        }
    }
}