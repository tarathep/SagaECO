﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:出差的咖啡館店員(11000989) X:118 Y:135
namespace SagaScript.M10014000
{
    public class S11000989 : Event
    {
        public S11000989()
        {
            this.EventID = 11000989;
        }

        public override void OnEvent(ActorPC pc)
        {
            //Say(pc, 11000989, 302, "我是多麼♪$R;" +
            //                       "希望擁有屬於自己的出差咖啡館本店$R;" +
            //                       "的美麗動人的少女喔♪$R;" +
            //                       "$R要向著自己的夢想努力♪$R;", "出差的咖啡館店員");
            Say(pc, 302, "welcome～～♪$R;", "咖啡館店員");
            Say(pc, 302, "本店是奧克魯尼亞上空１０００ｍ的「天空咖啡廳」$R入場500G;", "咖啡館店員");
            switch (Select(pc, "要進去嗎？", "", "進去", "不進去"))
            {
                case 1:
                    if (pc.Gold >= 500)
                    {
                        pc.Gold -= 500;
                        Say(pc, 0, 131, "付500G。$R;", " ");
                        Say(pc, 302, "恩。$R;" +
                        "客人一名！$R;", "咖啡館店員");
                        Warp(pc, 20081000, 11, 21);
                    }
                    else
                    {
                        Say(pc, 302, "對不起你身上沒錢～～♪$R;", "咖啡館店員");
                    }
                    break;
            }
        }
    }
}
