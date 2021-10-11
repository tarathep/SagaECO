﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50084000
{
    public class S11002201 : Event
    {
        public S11002201()
        {
            this.EventID = 11002201;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "從次元斷層裡跳進去嗎？$R;", "");
            if (Select(pc, "跳進去？", "", "不跳進去", "跳進去") == 2)
            {
                PlaySound(pc, 3161, false, 100, 50);
                Wait(pc, 990);
                Wait(pc, 990);
                
                int oldMap = pc.CInt["Beginner_Map"];
                pc.CInt["Beginner_Map"] = CreateMapInstance(50085000, 10023100, 250, 132);
                Warp(pc, (uint)pc.CInt["Beginner_Map"], 5, 5);
                
            }
        }
    }
}