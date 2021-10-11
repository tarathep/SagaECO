using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30203003
{
    public class S11002162 : Event
    {
        public S11002162()
        {
            this.EventID = 11002162;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, pc.Name + "小朋友$R;" +
            "第一次見面嗎？;" +
            "您好！$R;", "微微");

            Say(pc, 132, "您好，我叫微微。$R;" +
            "我是タイタニア第3氏族的天使。$R;" +
            "$R很榮幸見到你。$R;" +
            "$P我作為你夢裡的人...我必須告訴你知$R;" +
            "現在已經醒了！$R;" +
            "$P不要太擔心...$R;" +
            "$P因為還有世界...$R;" +
            "是你的朋友......$R;", "ティタ");
            Wait(pc, 990);

            pc.CInt["Beginner_Map"] = CreateMapInstance(50080000, 10023100, 250, 132);

            Warp(pc, (uint)pc.CInt["Beginner_Map"], 26, 25);
        }
    }
}