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
    public class S11001034 : Event
    {
        public S11001034()
        {
            this.EventID = 11001034;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001033, 131, "在吧，師母，聽說過嗎？$R;");
            Say(pc, 11001034, 131, "哎呀，$R;" +
                "是真的嗎？$R;" +
                "$R再說一遍吧。$R;");
        }
    }
}