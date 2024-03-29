﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:塔妮亞世界的幻影(30204000) NPC基本信息:微微(13000177) X:15 Y:20
namespace SagaScript.M30204000
{
    public class S13000177 : Event
    {
        public S13000177()
        {
            this.EventID = 13000177;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            pc.CInt["Country"] = 0;

            Say(pc, 13000177, 131, "You seem to be... a little confused? $R;" +
                                    "$R is your first time here? $R;", "Tita");

            Say(pc, 13000177, 132, "I am Tita, $R;" +
                                    "The archangel of the third family of Titania. $R;" +
                                    "$P is your dreamland here! $R;" +
                                    "$R Titania's World$R;" +
                                    "Have blue sky $R;" +
                                    "It's a beautiful place. $R;", "Tita");

            Say(pc, 13000177, 131, "You don't need to worry about it, $R;" +
                                    "$R let me lead you to the world of ECO! $R;", "Tita");

            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 5607);
            Wait(pc, 2000);

            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 5607);
            Wait(pc, 1000);

            PlaySound(pc, 2122, false, 100, 50);
            ShowEffect(pc, 5607);
            Wait(pc, 2000);

            Say(pc, 13000177, 131, "Ah!!$R;" +
                                    "$R...this is...$R;" +
                                    "This is the electromagnetic wave of [those guys]...!?$R;", "Tita");

            ShowEffect(pc, 4023);
            Wait(pc, 2000);

            pc.CInt["Beginner_Map"] = CreateMapInstance(50030000, 10023100, 250, 132);

            x = (byte)Global.Random.Next(2, 9);
            y = (byte)Global.Random.Next(2, 4);

            Warp(pc, (uint)pc.CInt["Beginner_Map"], x, y);
        }
    }
}
