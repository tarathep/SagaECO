﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M21193000
{
    public class S11001789 : Event
    {
        public S11001789()
        {
            this.EventID = 11001789;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            if (pc.Quest != null)
            {
                if (pc.Quest.ID == 25200004 && pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
                {
                    ECOchen_mask.SetValue(ECOchen.打倒一次龙, true);
                    ECOchen_mask.SetValue(ECOchen.打到本气龙后, true);
                    HandleQuest(pc, 67);
                    return;
                }
                if (pc.Quest.ID == 25200003 && pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
                {
                    ECOchen_mask.SetValue(ECOchen.打倒一次龙, true);
                    ECOchen_mask.SetValue(ECOchen.打到放水龙后, true);
                    HandleQuest(pc, 66);
                    return;
                }
            }
            if (ECOchen_mask.Test(ECOchen.打到本气龙后))
            {
                古武器真解放(pc);
                return;
            }
            if (ECOchen_mask.Test(ECOchen.打到放水龙后) && 
                (CountItem(pc, 10067900) > 0 ||
                    CountItem(pc, 10068000) > 0 ||
                    CountItem(pc, 10068100) > 0 ||
                    CountItem(pc, 10068200) > 0 ||
                    CountItem(pc, 10068300) > 0 ||
                    CountItem(pc, 10068400) > 0 ||
                    CountItem(pc, 10068500) > 0 ||
                    CountItem(pc, 10068600) > 0 ||
                    CountItem(pc, 10068700) > 0 ||
                    CountItem(pc, 10068800) > 0 ||
                    CountItem(pc, 10068900) > 0 ||
                    CountItem(pc, 10069100) > 0 ||
                    CountItem(pc, 10069200) > 0 ||
                    CountItem(pc, 10069300) > 0 ||
                    CountItem(pc, 10069400) > 0 ||
                    CountItem(pc, 10069500) > 0 ||
                    CountItem(pc, 10069600) > 0))
            {
                古武器解放(pc);
                return;
                /*
                else
                {
                    Say(pc, 0, "......$R;" +
                    "还有什么事情么？$R;" +
                    ".......$R;", "埃米爾龍");
                    return;
                }//*/
            }
            if (ECOchen_mask.Test(ECOchen.第一次和埃米尔龙对话))
            {
                if (CountItem(pc, 10067600) > 0 ||
                    CountItem(pc, 10067700) > 0 ||
                    CountItem(pc, 10067800) > 0)
                {

                    Say(pc, 0, "哦？$R;" +
                    "想解放古代武器的力量？$R;" +
                    "做好覺悟了么？$R;", "埃米爾龍");
                    switch (Select(pc, "如何呢？", "", "挑戰", "不挑戰"))
                    {
                        case 1:
                            HandleQuest(pc, 67);
                            return;
                    }
                    return;
                }
                Say(pc, 0, "哦？$R;" +
                "準備好了么？$R;" +
                "$R話說在前頭、我可是很強的哦？$R;" +
                "做好覺悟了么？$R;", "埃米爾龍");
                switch (Select(pc, "如何呢？", "", "挑戰", "不挑戰"))
                {
                    case 1:
                        if (pc.Level >= 84)
                        {
                            HandleQuest(pc, 66);
                            return;
                        }
                        Say(pc, 0, "嗯？$R;" +
                        "你、比想像中的還弱啊？$R;" +
                        "$R多去鍛煉下再來吧。$R;", "埃米爾龍");
                        break;
                    case 2:
                        Say(pc, 0, "怎麼、不舒服么！$R;" +
                        "$R改變主意的話再來哦！$R;" +
                        "$R我實在閑得沒辦法。$R;", "埃米爾龍");
                        break;

                }
            }
            else
            {
                第一次对话(pc);
                return;
            }

        }

        void 第一次对话(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            Say(pc, 0, "…嗯？$R;" +
            "$R哦、正常的人類能達到這里還$R;" +
            "真是少見啊！$R;" +
            "$P話這麼說、你還有點非比尋常。$R;" +
            "$R是如何到這裡來的呢。$R;" +
            "$P嘛,這種事情怎麼都無所謂。$R;" +
            "$R我在這裡也有很長一段時間了、$R;" +
            "實在是很閑。$R;" +
            "$P如何？$R;" +
            "陪我打發下時間怎麼樣？$R;" +
            "$P玩法很簡單！$R;" +
            "和我戰鬥就行！$R;" +
            "$R當然、對手是人類的話我會$R;" +
            "放水的、放心吧！$R;" +
            "$P對了、如果戰勝我的話、$R;" +
            "就送你貴重的道具當禮物。$R;" +
            "$P準備好了的話、$R;" +
            "再來找我說話吧！$R;", "埃米爾龍");
            ECOchen_mask.SetValue(ECOchen.第一次和埃米尔龙对话, true);
            return;
        }

        void 古武器真解放(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            if (CountItem(pc, 10067600) > 0)
            {
                if (Select(pc, "想解放武器的力量？", "", "是", "不了") == 1)
                {
                    TakeItem(pc, 10067600, 1);//古の両手剣
                    GiveItem(pc, 60021500, 1);//終刃・黒狼
                    ECOchen_mask.SetValue(ECOchen.打到本气龙后, false);
                }
                return;
            }
            if (CountItem(pc, 10067700) > 0)
            {
                if (Select(pc, "想解放武器的力量？", "", "是", "不了") == 1)
                {
                    TakeItem(pc, 10067700, 1);//古の片手槍
                    GiveItem(pc, 60061700, 1);//聖竜槍・タイタニア
                    ECOchen_mask.SetValue(ECOchen.打到本气龙后, false);
                }
                return;
            }
            if (CountItem(pc, 10067800) > 0)
            {
                if (Select(pc, "想解放武器的力量？", "", "是", "不了") == 1)
                {
                    TakeItem(pc, 10067800, 1);//古の両手槍
                    GiveItem(pc, 60061800, 1);//闇竜槍・ドミニオン
                    ECOchen_mask.SetValue(ECOchen.打到本气龙后, false);
                }
                return;
            }
        }
            
        void 古武器解放(ActorPC pc)
        {
        	BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            Say(pc, 0, "嗯？$R;" +
            "你们打算要我帮你们解开武器的封印？$R;" +
            "$R好吧不过有个条件$R;" +
            "拿10000ecoin来我就帮你$R;", "埃米爾龍");
            if (pc.ECoin >= 10000)
            {
                if (Select(pc, "想解放武器的力量？", "", "是", "不了") == 1)
                {

                    if (CountItem(pc, 10067900) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10067900, 1);//古の片手剣
                        GiveItem(pc, 60024600, 1);//霊刀・布都御魂剣
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068000) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068000, 1);//古の細剣
                        GiveItem(pc, 60024600, 1);//霊刀・布都御魂剣
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068100) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068100, 1);//古の短剣
                        GiveItem(pc, 60024800, 1);//黒剣・カルンウェナン
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068200) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068200, 1);//古の爪
                        GiveItem(pc, 60024900, 1);//覇王爪・フェンリル
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068300) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068300, 1);//古の片手斧
                        GiveItem(pc, 60052200, 1);//魔斧・コキュートス
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068400) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068400, 1);//古の両手斧
                        GiveItem(pc, 60052300, 1);//冥獄斧・デストロイヤー
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068500) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068500, 1);//古の片手棒
                        GiveItem(pc, 60043900, 1);//神槌・ミョルニル
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068600) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068600, 1);//古の両手棒
                        GiveItem(pc, 60044000, 1);//轟戦旗・ファフニール
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068700) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068700, 1);//古の杖
                        GiveItem(pc, 60071700, 1);//宝杖・レッドルナ
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068800) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068800, 1);//古の本
                        GiveItem(pc, 60084600, 1);//禁書・エイボン
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10068900) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10068900, 1);//古の弓
                        GiveItem(pc, 60092800, 1);//太陽弓・シャールンガ
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10069000) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10069000, 1);//古の弩
                        GiveItem(pc, 60092900, 1);//轟砲弓・オティヌス
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10069100) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10069100, 1);//古の銃
                        GiveItem(pc, 60093000, 1);//魔神銃・アキシオン
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10069200) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10069200, 1);//古のライフル
                        GiveItem(pc, 60093200, 1);//光砲・エンジェルハイロゥ
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10069300) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10069300, 1);//古の鞭
                        GiveItem(pc, 61030200, 1);//刻鞭・シワコアトル
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10069400) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10069400, 1);//古の楽器
                        GiveItem(pc, 61040600, 1);//真絃・ソウルゲイザー
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10069500) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10069500, 1);//古の投擲槍
                        GiveItem(pc, 61011200, 200);//天鳴槍・ゲイボルグ
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                    if (CountItem(pc, 10069600) > 0)
                    {
                        pc.ECoin -= 10000;
                        TakeItem(pc, 10069600, 1);//古のカード
                        GiveItem(pc, 61050057, 200);//影札・ブラックジャック
                        ECOchen_mask.SetValue(ECOchen.打到放水龙后, false);
                        return;
                    }
                }
            }
            else
            {
                Say(pc, 0, "$Pecoin好像不够呢$R;", "埃米爾龍");
            }
        }
    }
}