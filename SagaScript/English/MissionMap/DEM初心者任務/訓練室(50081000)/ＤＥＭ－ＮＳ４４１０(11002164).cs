using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50081000
{
    public class S11002164 : Event
    {
        public S11002164()
        {
            this.EventID = 11002164;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            if (!newbie.Test(DEMNewbie.介绍造型变换))
            {
                Say(pc, 131, "那麼…$R;" +
                "一邊移動一下身體，一邊$R;" +
                "測試你的性能吧。$R;" +
                "$P為我們DEM族鬥爭、$R;" +
                "必須變更「形態」$R;" +
                "$P在「人形形態」$R;" +
                "我們能裝備武器以及衣服$R;" +
                "但是我們發揮不了原來的能力。$R;" +
                "$P不過假如是「戰鬥形態」的話、$R;" +
                "就能發揮到最大威力。$R;" +
                "$P要變更「形態」的話、$R;" +
                "右方點擊自己$R;" +
                "選擇「フォームチェンジ」的話就可以了。$R;" +
                "$P那就會變成$R;" +
                "「戰鬥形態」。$R;", "ＤＥＭ－ＮＳ４４１０");
                newbie.SetValue(DEMNewbie.介绍造型变换, true);
                return;
            }
            if (!newbie.Test(DEMNewbie.给予改造部件))
            {
                if (pc.Form != DEM_FORM.MACHINA_FORM)
                {
                    Say(pc, 131, "那個形態不是戰鬥形態。$R;" +
                    "$P要變更「形態」的話、$R;" +
                    "右方點擊自己$R;" +
                    "選擇「フォームチェンジ」。$R;", "ＤＥＭ－ＮＳ４４１０");
                    return;
                }
                else
                {
                    Say(pc, 131, "唔..沒事的話倒不需要轉換形態$R;" +
                    "那麼就來解說一下武器轉換吧。$R;" +
                    "$P啊！好像還有些武裝還沒送到......$R;" +
                    "$R你想$R;" +
                    "近接攻撃型$R;" +
                    "遠距離攻撃型$R;" +
                    "哪一方面的類型好？$R;", "ＤＥＭ－ＮＳ４４１０");
                    switch (Select(pc, "哪一方面的類型好？", "", "近接攻撃型(未實裝)", "遠距離攻撃型"))
                    {
                        case 2:
                            Say(pc, 164, "是這樣嗎？$R;" +
                            "那麼就送你這個吧。$R;", "ＤＥＭ－ＮＳ４４１０");
                            GiveItem(pc, 85302500, 1);
                            GiveItem(pc, 81700000, 1);
                            Say(pc, 0, 65535, "ガンハンド$R;" +
                            "■SK-[ルーキーバレット]$R;" +
                            "獲得了。$R;", " ");
                            Say(pc, 131, "剛剛給你的、$R;" +
                            "是被稱為「手部零件」的$R;" +
                            "マシナフォーム使用裝備。$R;" +
                            "$Pそして今開いているウインドウは、$R;" +
                            "以及現在開著的視窗叫作「裝備視窗」。$R;" +
                            "$P在マシナフォーム、$R;" +
                            "頭部,身體,手腕,腿,背部$R;" +
                            "都能換上不同的零件。$R;" +
                            "$P那麼立刻把裝備裝上吧。$R;" +
                            "$R安裝了手腕部件。$R;" +
                            "$P把要更換的組件雙按兩下就可以換上了$R;", "ＤＥＭ－ＮＳ４４１０");
                            DEMParts(pc);
                            break;
                    }
                    newbie.SetValue(DEMNewbie.给予改造部件, true);
                    return;
                }
            }

            if (!pc.Inventory.Parts.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                Say(pc, 131, "剛剛給你的、$R;" +
                "是被稱為「手部零件」$R;" +
                "的マシナフォーム使用裝備。$R;" +
                "$P以及現在開著的視窗叫作、$R;" +
                "「裝備視窗」。$R;" +
                "$P在マシナフォーム、$R;" +
                "頭部,身體,手腕,腿,背部$R;" +
                "都能換上不同的零件。$R;" +
                "$P那麼立刻把裝備裝上吧。$R;" +
                "$R把要更換的組件雙按兩下就可以換上了$R;", "ＤＥＭ－ＮＳ４４１０");
                DEMParts(pc);
                return;
            }
            else
            {
                if (!newbie.Test(DEMNewbie.已经DEMIC改造完毕))
                {
                    Say(pc, 131, "好像成功裝上部件了$R;" +
                    "$P部件的變更、$R;" +
                    "因為如果安裝錯誤的話$R;" +
                    "是不能使用的。$R;" +
                    "$P那麼，進行下一個教學吧。$R;" +
                    "$P現在開著的視窗、$R;" +
                    "被稱為「DEMIC」。$R;" +
                    "$P剛剛給你的是強化容量的晶片、$R;" +
                    "但是不會使你變得強大。$R;" +
                    "$P為了使實際的性能有所提升、$R;" +
                    "必須要使用「晶片」。$R;" +
                    "$P在「DEMIC」裡可以設定強化晶片$R;" +
                    "以提高性能。$R;", "ＤＥＭ－ＮＳ４４１０");

                    Say(pc, 131, "對於晶片、$R;" +
                    "有提高基本狀態的「狀態晶片」、$R;" +
                    "有技能學習的「技能晶片」$R;" +
                    "$P以及總結全部技術整合而成的、$R;" +
                    "「極限晶片」$R;" +
                    "$P以上的三種「晶片」$R;" +
                    "在DEMIC面板可以、$R;" +
                    "自由安裝這些晶片$R;" +
                    "$R能夠改變成使用者喜愛的戰術取向。$R;" +
                    "$P超過了強化限界的話、$R;" +
                    "那就是DEMIC欄太少的緣故$R;" +
                    "如果晶片所佔的範圍較多，$R;" +
                    "$R你就要換上小一點的晶片或是進行升級、$R;" +
                    "大概是這樣了。$R;" +
                    "$P還有的是$R;" +
                    "使用DEMIC晶片功能是要扣除1EP作為資源費用$R;" +
                    "這樣說明的話$R;" +
                    "會較容易明白吧。$R;" +
                    "$P剛剛送給你的「強化晶片」$R;" +
                    "把它放進DEMIC吧$R;" +
                    "首先按左方下的「入替」按鍵。$R;" +
                    "$P然後會開放了晶片面板$R;" +
                    "然後換上你需要的晶片。$R;" +
                    "$P然後把它放到你喜愛的位置。$R;" +
                    "$R但是請注意所佔的格數、$R;" +
                    "當你決定好的時候，$R;" +
                    "$P按下「確定」就完成了整個DEMIC強化過程了$R;", "ＤＥＭ－ＮＳ４４１０");

                    pc.EP++;
                    DEMIC(pc);
                    newbie.SetValue(DEMNewbie.已经DEMIC改造完毕, true);
                }
                else
                {
                    if (!newbie.Test(DEMNewbie.要求去攻击靶子))
                    {
                        Say(pc, 131, "放在那裡的是測試物$R;" +
                        "試試攻擊它吧$R;" +
                        "$P把想攻擊的目標，$R;" +
                        "用滑鼠左鍵點擊它一下$R;" +
                        "攻撃処理を行ってくれる。$R;" +
                        "$他就會自動攻擊$R;" +
                        "另外，剛剛在DEMIC部份、$R;" +
                        "如果有裝上「技能晶片」的話，還可以使用技能$R;" +
                        "當你想使用技能時$R;" +
                        "打開技能視窗，雙擊滑鼠，$R;" +
                        "$P部屋の隅にテスト用の素体が$R;" +
                        "指定目標就能使用了。$R;" +
                        "測試用的目標已經準備好了$R;" +
                        "$P攻擊它吧、$R;" +
                        "$P當你認為足夠的話、$R;", "ＤＥＭ－ＮＳ４４１０");
                        newbie.SetValue(DEMNewbie.要求去攻击靶子, true);
                    }
                    else
                    {
                        Say(pc, 131, "已經可以嗎？$R;", "ＤＥＭ－ＮＳ４４１０");
                        if (Select(pc, "已經可以嗎？", "", "想再試一下", "沒有問題") == 2)
                        {
                            Say(pc, 131, "那麼立刻$R;" +
                            "上去實際戰場。。$R;" +
                            "$P正好ドミニオン和我們$R;" +
                            "有正在交戰的部隊。。$R;" +
                            "$R就去那裡混在一起和ドミニオン交戰吧$R;" +
                            "$P跟來吧。$R;", "ＤＥＭ－ＮＳ４４１０");
                            int oldMap = pc.CInt["Beginner_Map"];
                            pc.CInt["Beginner_Map"] = CreateMapInstance(50082000, 10023100, 250, 132);
                            Warp(pc, (uint)pc.CInt["Beginner_Map"], 58, 47);

                            DeleteMapInstance(oldMap);
                        }
                    }
                }
            }

        }
    }
}