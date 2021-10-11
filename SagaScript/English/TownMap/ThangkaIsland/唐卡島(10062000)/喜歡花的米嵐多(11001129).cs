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
    public class S11001129 : Event
    {
        public S11001129()
        {
            this.EventID = 11001129;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];
            if (Neko_06_amask.Test(Neko_06.GetApricots) &&
                !Neko_06_amask.Test(Neko_06.結束對話))
            {
                Say(pc, 131, "まあ、あんた。$R意識が戻ったのかい？$R;" +
                "$Rよかったよ、一時はどうなることかと…$R;", "お花が好きなミランダ");
                Neko_06_amask.SetValue(Neko_06.結束對話, true);
                return;
            }
            if (Neko_06_amask.Test(Neko_06.Kyoko_mission_begins) &&
                !Neko_06_amask.Test(Neko_06.GetApricots) &&
                Neko_06_cmask.Test(Neko_06.恢復身體))
            {
                if (pc.PossesionedActors.Count == 0)
                {
                    pc.CInt["Neko_06_Map_01"] = CreateMapInstance(50035000, 10062000, 130, 180);
                    Warp(pc, (uint)pc.CInt["Neko_06_Map_01"], 4, 7);
                }
                else
                {
                    Say(pc, 131, "啊…誰在憑依啊?$R;" +
                        "$R這裡不可以帶陌生人來的!哦$R;");
                }
                return;
            }
            if (Neko_06_amask.Test(Neko_06.Kyoko_mission_begins) &&
                !Neko_06_amask.Test(Neko_06.GetApricots) &&
                Neko_06_cmask.Test(Neko_06.Learn_how_to_recover))
            {
                Say(pc, 131, "……な、な、な、なかに、中に！$R;" +
                "$Rひ、ひっひっ人が眼をむいてってっ！！$R;", "お花が好きなミランダ");

                Say(pc, 0, 131, "（ま、まずい。抜け殻に気がつかれた。$R;" +
                "$Rいそいで元に戻らなきゃ…！）$R;", " ");
                if (pc.PossesionedActors.Count == 0)
                {
                    pc.CInt["Neko_06_Map_02"] = CreateMapInstance(50036000, 10062000, 130, 180);
                    Warp(pc, (uint)pc.CInt["Neko_06_Map_02"], 4, 7);
                }
                else
                {
                    Say(pc, 131, "啊…誰在憑依啊?$R;" +
                        "$R這裡不可以帶陌生人來的!哦$R;");
                }
                return;
            }

            if (Neko_06_amask.Test(Neko_06.Kyoko_mission_begins) &&
                !Neko_06_amask.Test(Neko_06.GetApricots) &&
                !Neko_06_cmask.Test(Neko_06.與杏子對話))
            {
                Neko_06_cmask.SetValue(Neko_06.与喜歡花的米嵐多对话, true);
                Say(pc, 131, "ホントに困ったわねぇ……ブツブツ$R;" +
                "$P……あら？$R;" +
                "$Rまあ！？あなたが壷を引き取りに？$R;" +
                "$P…そうなのよ、$R壷の中に閉じ込めたのはいいんだけど$Rやっぱりおっかなくってさ。$R;" +
                "$R助かるわぁ、$R早く持って行ってくださいな。$R;", "お花が好きなミランダ");
                if (Select(pc, "部屋に入る？", "", "入る", "まだ入らない") == 1)
                {
                    if (pc.PossesionedActors.Count == 0)
                    {
                        pc.CInt["Neko_06_Map_01"] = CreateMapInstance(50035000, 10062000, 130, 180);
                        Warp(pc, (uint)pc.CInt["Neko_06_Map_01"], 4, 7);
                    }
                    else
                    {
                        Say(pc, 131, "啊…誰在憑依啊?$R;" +
                            "$R這裡不可以帶陌生人來的!哦$R;"); 
                    }
                }
                return;
            }
            Say(pc, 131, "這花是我種植的唷$R;");
        }
    }
}