﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:正在工作的泰迪(11000726) X:62 Y:210
namespace SagaScript.M10071000
{
    public class S11000726 : Event
    {
        public S11000726()
        {
            this.EventID = 11000726;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> mask = pc.CMask["Tinyis_Land_01"];

            if (pc.Account.GMLevel >= 100)
            {
                if (Select(pc, "去ECO城嗎？", "", "去", "不去") == 1)
                {
                    NPCMove(pc, 11000726, 62, 210, 0, 7, 111, 200);
                    ShowEffect(pc, 11000726, 5314);
                    NPCMove(pc, 11000717, 62, 210, 100, 7, 111, 65535);
                    NPCMove(pc, 11000718, 62, 210, 100, 7, 111, 65535);
                    NPCMove(pc, 11000738, 62, 210, 100, 7, 111, 65535);
                    Wait(pc, 990);
                    ShowEffect(pc, 4011);
                    ShowEffect(pc, 11000726, 5314); 
                    Wait(pc, 990);
                    Warp(pc, 11027000, 252, 7);
                    NPCShow(pc, 11001833);
                }
                return;
            }

            if (pc.Fame > 6)
            {
                if (!mask.Test(Tinyis_Land_01.Been_to_ECO_City))
                {
                    Say(pc, 0, 65535, "ねえ、知ってる？$R;", "タイニー1号");
                    switch (Select(pc, "知ってる？", "", "知らない", "何を？"))
                    {
                        case 1:
                            Say(pc, 0, 65535, "レインボーはねぇ、実は道なんだ。$R;", "タイニー2号");

                            Say(pc, 0, 65535, "近所のコンビニに続く道。$R;" +
                            "$R世界の裏側の国に続く道。$R;" +
                            "$R君の未来へと続く道。$R;", "タイニー3号");

                            Say(pc, 0, 65535, "君が望めば$R;" +
                            "本当に、どこにでも行けるんだ。$R;", "タイニー4号");

                            Say(pc, 0, 65535, "実は、この空には$R;" +
                            "レインボーがい～っぱい！$R;" +
                            "$R……みえないけどね。$R;", "タイニー1号");

                            Say(pc, 0, 65535, "クスクスッ！$R;" +
                            "$Rじゃあさ！$R;" +
                            "レインボーの交差点って知ってる？$R;", "タイニー2号");
                            break;
                        case 2:

                            Say(pc, 0, 65535, "レインボーはねぇ、実は道なんだ。$R;", "タイニー2号");

                            Say(pc, 0, 65535, "近所のコンビニに続く道。$R;" +
                            "$R世界の裏側の国に続く道。$R;" +
                            "$R君の未来へと続く道。$R;", "タイニー3号");

                            Say(pc, 0, 65535, "君が望めば$R;" +
                            "本当に、どこにでも行けるんだ。$R;", "タイニー4号");

                            Say(pc, 0, 65535, "実は、この空には$R;" +
                            "レインボーがい～っぱい！$R;" +
                            "$R……みえないけどね。$R;", "タイニー1号");

                            Say(pc, 0, 65535, "クスクスッ！$R;" +
                            "$Rじゃあさ！$R;" +
                            "レインボーの交差点って知ってる？$R;", "タイニー2号");
                            break;
                    }

                    switch (Select(pc, "知ってる？", "", "知らない", "知ってる"))
                    {
                        case 1:
                            Say(pc, 0, 65535, "レインボーが交差する場所。$R;" +
                             "ここではない、どこでもない場所。$R;", "タイニー3号");

                            Say(pc, 0, 65535, "中継地点みたいなもんかな？$R;", "タイニー4号");

                            Say(pc, 0, 65535, "えー、違うでしょ！？$R;", "タイニー1号");

                            Say(pc, 0, 65535, "フフフッ！$R;" +
                            "ねえ、行ってみたい？$R;" +
                            "レインボーが交差するあの場所へ！$R;", "タイニー2号");

                            Say(pc, 0, 65535, "あったりまえだよ！$R;" +
                            "ねえ！？$R;", "タイニー3号");
                            break;
                        case 2:
                            Say(pc, 0, 65535, "フフフッ！$R;" +
                            "ねえ、行ってみたい？$R;" +
                            "レインボーが交差するあの場所へ！$R;", "タイニー2号");

                            Say(pc, 0, 65535, "あったりまえだよ！$R;" +
                            "ねえ！？$R;", "タイニー3号");
                            break;
                    }
                }
                else
                {
                    Say(pc, 0, 65535, "フフフッ！$R;" +
                   "ねえ、行ってみたい？$R;" +
                   "レインボーが交差するあの場所へ！$R;", "タイニー2号");

                    Say(pc, 0, 65535, "あったりまえだよ！$R;" +
                    "ねえ！？$R;", "タイニー3号");
                }

                switch (Select(pc, "どうする？", "", "行かない", "もちろん、いってみたい！"))
                {
                    case 1:
                        Say(pc, 0, 65535, "あれー？$R;", "タイニー4号");
                        break;
                    case 2:
                        mask.SetValue(Tinyis_Land_01.Been_to_ECO_City, true);
                        Say(pc, 0, 65535, "よーしっ！$R;" +
                        "じゃあ、みんな、いくぜ～っ！！$R;", "タイニー1号");
                        Wait(pc, 660);

                        Say(pc, 0, 65535, "こーこーろーをーあーわーせーてー$R;" +
                        "ちーかーらーをーひーとーつーにー！$R;" +
                        "$Rまわせまわせまわせまわせ～～～！$R;", "タイニー1号");
                        NPCMove(pc, 11000726, 62, 210, 0, 7, 111, 200);
                        ShowEffect(pc, 11000726, 5314);
                        NPCMove(pc, 11000717, 62, 210, 100, 7, 111, 65535);
                        NPCMove(pc, 11000718, 62, 210, 100, 7, 111, 65535);
                        NPCMove(pc, 11000738, 62, 210, 100, 7, 111, 65535);
                        Wait(pc, 990);
                        ShowEffect(pc, 4011);
                        ShowEffect(pc, 11000726, 5314);
                        Wait(pc, 990);
                        Warp(pc, 11027000, 252, 7);
                        NPCShow(pc, 11001833);
                        break;
                }
                
            }
            Say(pc, 0, 0, "哼唷……哼唷……$R;" +
                          "$R哼唷……哼唷……$R;", "正在工作的泰迪");
        }
    }
}




