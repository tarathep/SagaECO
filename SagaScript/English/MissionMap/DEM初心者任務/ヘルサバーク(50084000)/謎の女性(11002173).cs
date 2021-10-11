using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50084000
{
    public class S11002173 : Event
    {
        public S11002173()
        {
            this.EventID = 11002173;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);

            if (!newbie.Test(DEMNewbie.第一次跟迷之女性说话))
            {
                NPCMove(pc, 11002173, -10050, -10550, 315, MoveType.NONE);
                Say(pc, 131, "咦..？那傢伙找到新的東西....$R;" +
                "$P雖然是DEM族卻被DEM追捕著...$R;" +
                "$P最近....、$R;" +
                "聽說了很多「例外」的事情。$R;" +
                "$R嘛...對我來說都是好事...$R;", "謎の女性");

                if (Select(pc, "……", "", "謎の女性", "道米尼少女") == 2)
                {
                    Say(pc, 131, "道米尼少女的事情？$R;" +
                    "$P如果是這樣的話、$R;" +
                    "它的心應該是出故障了。$R;" +
                    "$P如果是你的話$R;" +
                    "作為道米尼的盟友$R;" +
                    "守護她吧...。$R;", "謎の女性");
                    NPCMove(pc, 11002173, -10050, -10550, 135, MoveType.NONE);
                    Say(pc, 111, "但是..這個次元斷層...不太正常...$R;" +
                    "$P前方那個發光的區域，就是次元斷層。$R;" +
                    "經常都會發生時間傾斜。$R;" +
                    "$P傾斜到什麼地方都可以前往...$R;" +
                    "但是去到的地方是不知道的$R;" +
                    "$P在這個世界上...$R;" +
                    "$P你是很幸運.....$R;" +
                    "$P你想作為DEM而活的話、$R;" +
                    "就跳下去，$R;" +
                    "$P要麼留在這等死。$R;" +
                    "如果你想通過自己的道路前進...$R;" +
                    "就跳進這個次元斷層...$R;" +
                    "$R就算不知道是什麼地方都好....。$R;" +
                    "$P選擇權還是在你身上。$R;" +
                    "$R自由是很可貴的....。$R;", "謎の女性");
                }
            }
            else
            {
                Say(pc, 111, "……。$R;", "謎の女性");
                Say(pc, 0, 65535, "女性尖銳的目光...$R;" +
                "正注視著這個強大的旋禍…。$R;", " ");
            }
        }
    }
}