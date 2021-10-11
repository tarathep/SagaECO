using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
//所在地圖:埃米爾族的夢中(30203000) NPC基本信息:微微(13000176) X:15 Y:20
namespace SagaScript.M30203000
{
    public class S13000176 : Event
    {
        public S13000176()
        {
            this.EventID = 13000176;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            pc.CInt["Country"] = 0;

            Say(pc, 13000176, 131, "You seem to be... a little confused? $R;" +
                                    "$R is your first time here? $R;", "Tita");

            Say(pc, 13000176, 132, "I am Tita, $R;" +
                                   "The archangel of the third family of Titania. $R;" +
                                   "$P is your dreamland here! $R;" +
                                   "$R Emil’s world is $R;" +
                                   "A very shining star $R;" +
                                   "It's a beautiful place. $R;", "Tita");

            Say(pc, 13000176, 131, "You don't need to worry, $R;" +
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

            Say(pc, 13000176, 131, "Ah!!$R;" +
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
