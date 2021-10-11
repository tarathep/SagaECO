using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50082000
{
    public class S11002165 : Event
    {
        public S11002165()
        {
            this.EventID = 11002165;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            Say(pc, 131, "如果先從這裡前進的話$R;" +
            "應該能見朋友。$R;" +
            "$R聽說已經趕了敵人的事了…$R;" +
            "為念頭援助吧。$R;", "ＤＥＭ－ＮＳ４４１０");

        }
    }
}