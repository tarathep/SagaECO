using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50080000
{
    public class S11002163 : Event
    {
        public S11002163()
        {
            this.EventID = 11002163;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            if (!newbie.Test(DEMNewbie.领取EP))
            {
                do
                {
                    Say(pc, 131, "起來了嗎？、$R;" +
                    "MS-67$R;" +
                    "$P為了使你更進一步認識自己$R;" +
                    "你需要教育。$R;" +
                    "$P我被把你的教育委托了$R;" +
                    "型號為「ＤＥＭ－ＮＳ４４１０」的同伴。$R;" +
                    "$R首先先介紹我們的種族$R;" +
                    "留心聽吧。$R;", "ＤＥＭ－ＮＳ４４１０");

                    Say(pc, 131, "我們這個地方的居民$R;" +
                    "是被稱為「ＤＥＭ」的人型武器。$R;" +
                    "$P被「マザー」統一起來把我們的意志、$R;" +
                    "命令攻擊這個地方。$R;" +
                    "$P道米尼跟有生命的種族、$R;" +
                    "對我們而言都是敵人。$R;" +
                    "$P除此之外、$R;" +
                    "還有埃米爾族和塔尼亞族、$R;" +
                    "這些認識到道米尼的種族。$R;" +
                    "$R到這裡能理解嗎？$R;", "ＤＥＭ－ＮＳ４４１０");
                }
                while (Select(pc, "怎麼辦？", "", "再聽一次", "聽下一句") != 2);

                pc.EP++;
                newbie.SetValue(DEMNewbie.领取EP, true);
            }
            if (pc.CL < 10)
            {
                Say(pc, 131, "好。$R;" +
                "$P那麼你就立刻接受簡單的訓練$R;" +
                "並且馬上到戰場來。$R;" +
                "$R那就是製造你的理由、$R;" +
                "生存的理由。$R;" +
                "$P…在這之前、$R;" +
                "先告訴你成長狀態的教學$R;" +
                "$P現在打開的視窗、$R;" +
                "是「成本限制視窗」。$R;" +
                "$P我們不是活動體。$R;" +
                "$R因此，就算會成長卻依然會有限制。$R;" +
                "我們是用著一種叫「EP」$R;" +
                "$P的東西來讓我們成長。$R;" +
                "消費「EP」。$R;" +
                "$P就能讓自己的能力上升、$R;" +
                "$R當「成本限制」達到一定程度時$R;" +
                "$R你自己就會升級。$R;" +
                "$P還有的是「DEMIC」欄$R;" +
                "以後再說明吧。$R;" +
                "$P使用了的「EP」$R;" +
                "會提高「成長界限」。$R;" +
                "$P按「EP消費」按鍵、$R;" +
                "輸入想消耗的EP數量。$R;" +
                "$R確認好後，按確認$R;" +
                "能力就會上升。$R;", "ＤＥＭ－ＮＳ４４１０");

                DEMCL(pc);
                return;
            }
            else
            {
                Say(pc, 131, "成本限制$R;" +
                "好象上升了。$R;" +
                "$P那就準備下一個解說吧。$R;" +
                "$R前往下一個地方吧。$R;", "ＤＥＭ－ＮＳ４４１０");
                int oldMap = pc.CInt["Beginner_Map"];
                pc.CInt["Beginner_Map"] = CreateMapInstance(50081000, 10023100, 250, 132);
                LoadSpawnFile(pc.CInt["Beginner_Map"], "DB/Spawns/50081000.xml");
                Warp(pc, (uint)pc.CInt["Beginner_Map"], 10, 14);

                DeleteMapInstance(oldMap);
            }
        }
    }
}