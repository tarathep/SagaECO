﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:ItemTicketExchange(樣版)(12001061) X:38 Y:195
namespace SagaScript.M10071000
{
    public class S12001061 : Event
    {
        public S12001061()
        {
            this.EventID = 12001061;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "發生故障。$R;" +
                           "$P……$R;" +
                           "仔細看了一下，$R;" +
                           "原來有燒焦了的痕跡。$R;", " ");
        }
    }
}




