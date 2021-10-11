﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:街上的男孩(11000049) X:128 Y:156
namespace SagaScript.M10024000
{
    public class S11000049 : Event
    {
        public S11000049()
        {
            this.EventID = 11000049;
        }

        public override void OnEvent(ActorPC pc)
        {

            if (CountItem(pc, 10001080) > 0)
            {
                萬聖節(pc);
            }

            蒐集魔法書的碎片(pc);
        }

        void 蒐集魔法書的碎片(ActorPC pc)
        {
            BitMask<Badge_01> Badge_01_mask = new BitMask<Badge_01>(pc.CMask["Badge_01"]);                                                                              //任務：蒐集魔法書的碎片

            if (!Badge_01_mask.Test(Badge_01.已經與街上的男孩進行第一次對話))
            {
                初次與街上的男孩進行對話(pc);
                return;
            }

            if (CountItem(pc, 10019900) >= 10)
            {
                if (CheckInventory(pc, 10009500, 1))
                {
                    if (pc.Gender == PC_GENDER.MALE)
                    {
                        Say(pc, 11000049, 131, "大哥哥，真的很謝謝你!$R;" +
                                               "$R用10張『撕破的魔法書』$R;" +
                                               "完成了魔法書一頁。$R;" +
                                               "$P作為答謝，這個送給你吧!$R;" +
                                               "$R很像閃耀的星星吧!!$R;" +
                                               "$P我還有很多的，$R;" +
                                               "所以這個送給大哥哥吧。$R;", "街上的男孩");

                        TakeItem(pc, 10019900, 10);
                        GiveItem(pc, 10009500, 1);
                        Say(pc, 0, 0, "得到『銅徽章』!$R;", " ");
                    }
                    else
                    {
                        Say(pc, 11000049, 131, "大姊姊，真的很謝謝你!$R;" +
                                               "$R用10張『撕破的魔法書』$R;" +
                                               "完成了魔法書一頁。$R;" +
                                               "$P作為答謝，這個送給你吧!$R;" +
                                               "$R很像閃耀的星星吧!!$R;" +
                                               "$P我還有很多的，$R;" +
                                               "所以這個送給大姊姊吧。$R;", "街上的男孩");

                        TakeItem(pc, 10019900, 10);
                        GiveItem(pc, 10009500, 1);
                        Say(pc, 0, 0, "得到『銅徽章』!$R;", " ");
                    }
                }
                else
                {
                    Say(pc, 11000049, 131, "哇，竟然是『撕破的魔法書』!$R;" +
                                           "$R唔哇哇，是真的啊!$R;" +
                                           "$P我會好好答謝您的，$R;" +
                                           "請把行李減輕以後再來吧!$R;", "街上的男孩");
                }
            }
            else
            {
                Say(pc, 11000049, 131, "10張『撕破的魔法書』$R;" +
                                       "等於魔法書1頁的分量。$R;" +
                                       "$R…嘿嘿，$R;" +
                                       "如果能一次蒐集10張拿來給我的話，$R;" +
                                       "就太好了…$R;", "街上的男孩");
            }
        }

        void 初次與街上的男孩進行對話(ActorPC pc)
        {
            BitMask<Badge_01> Badge_01_mask = new BitMask<Badge_01>(pc.CMask["Badge_01"]);                                                                              //任務：蒐集魔法書的碎片

            if (pc.Gender == PC_GENDER.MALE)
            {
                Say(pc, 11000049, 131, "大哥哥，你好!$R;" +
                                       "$P大哥哥是專門討伐魔物的冒險家吧?$R;" +
                                       "$P我有一個小小的困擾，$R;" +
                                       "$R請大哥哥幫幫我吧!!$R;", "街上的男孩");

                switch (Select(pc, "拜託，幫忙我一下啦!!", "", "好的好的…知道了", "我討厭小孩子"))
                {
                    case 1:
                        Badge_01_mask.SetValue(Badge_01.已經與街上的男孩進行第一次對話, true);

                        Say(pc, 11000049, 131, "大哥哥，謝謝你!$R;" +
                                               "$P那個，我不小心弄丟了東西。$R;" +
                                               "$R是本好幾千頁的『魔法書』，$R;" +
                                               "是老師交給我的重要東西。$R;" +
                                               "$P到這個世界的時候，$R;" +
                                               "因為發生了一些特別狀況，$R;" +
                                               "所以都散掉了…$R;" +
                                               "$R被老師知道的話，$R;" +
                                               "肯定會狠狠的懲罰我的。$R;" +
                                               "$P大哥哥，拜託你了!$R;" +
                                               "$R幫我找一下『撕破的魔法書』吧!$R;", "街上的男孩");
                        break;

                    case 2:
                        Say(pc, 11000049, 131, "…嗚咽。$R;", "街上的男孩");
                        break;
                }
            }
            else
            {
                Say(pc, 11000049, 131, "大姊姊，你好!$R;" +
                                       "$P大姊姊是專門討伐魔物的冒險家吧?$R;" +
                                       "$P我有一個小小的困擾，$R;" +
                                       "$R請大姊姊幫幫我吧!!$R;", "街上的男孩");

                switch (Select(pc, "拜託，幫忙我一下啦!!", "", "好的好的…知道了", "我討厭小孩子"))
                {
                    case 1:
                        Badge_01_mask.SetValue(Badge_01.已經與街上的男孩進行第一次對話, true);

                        Say(pc, 11000049, 131, "大姊姊，謝謝你!$R;" +
                                               "$P那個，我不小心弄丟了東西。$R;" +
                                               "$R是本好幾千頁的『魔法書』，$R;" +
                                               "是老師交給我的重要東西。$R;" +
                                               "$P到這個世界的時候，$R;" +
                                               "因為發生了一些特別狀況，$R;" +
                                               "所以都散掉了…$R;" +
                                               "$R被老師知道的話，$R;" +
                                               "肯定會狠狠的懲罰我的。$R;" +
                                               "$P大姊姊，拜託你了!$R;" +
                                               "$R幫我找一下『撕破的魔法書』吧!$R;", "街上的男孩");
                        break;

                    case 2:
                        Say(pc, 11000049, 131, "…嗚咽。$R;", "街上的男孩");
                        break;
                }
            }
        }

        void 萬聖節(ActorPC pc)
        {
            Say(pc, 131, "お化けなんていないもん！$R;" +
            "いないったら、いないんだもん！$R;", "街の男の子");
            switch (Select(pc, "何をする？", "", "普通に話しかける", "『秘密のキャンディ』を渡す"))
            {
                case 1:
                    蒐集魔法書的碎片(pc);
                    break;
                case 2:
                    Say(pc, 131, "これ、僕に？$R;" +
                    "わーい、ありがとう！$R;" +
                    "$Rお化けから身を守る$R;" +
                    "不思議なキャンディ嬉しいな！$R;", "街の男の子");
                    TakeItem(pc, 10001080, 1);
                    Say(pc, 131, "お礼にいいものあげる！$R;", "街の男の子");
                    switch (Select(pc, "想要哪一樣禮物呢?", "", "南瓜頭", "南瓜紋中統襪♀", "南瓜花圃"))
                    {
                        case 1:
                            GiveItem(pc, 50024357, 1);
                            Say(pc, 11000049, 131, "作為答謝送你我親手做的『南瓜頭』!$R;" +
                                                   "$R把這個戴在頭上的話，$R;" +
                                                   "像鬼怪那樣的魔物都不敢打擾你的!$R;" +
                                                   "$P…應該是吧…$R;", "街上的男孩");
                            break;
                        case 2:
                            GiveItem(pc, 50011250, 1);
                            Say(pc, 11000049, 131, "作為答謝送給你『南瓜紋中統襪♀』!$R;", "街上的男孩");
                            break;
                        case 3:
                            GiveItem(pc, 31160200, 1);
                            Say(pc, 11000049, 131, "作為答謝送給你『南瓜花圃』!$R;", "街上的男孩");
                            break;
                    }
                    break;
            }
        }
    }
}