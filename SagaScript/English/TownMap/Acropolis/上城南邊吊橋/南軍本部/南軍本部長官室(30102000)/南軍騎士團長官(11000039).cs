﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102000
{
    public class S11000039 : Event
    {
        public S11000039()
        {
            this.EventID = 11000039;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (Knights_mask.Test(Knights.加入南軍騎士團))
            {
                if (!Knights_mask.Test(Knights.獲得騎士團皮盔甲) && pc.Fame > 29)
                {
                    Say(pc, 131, "看起來最近做的很認真阿$R;" +
                        "我也聽說過了$R;" +
                        "$R給您這個作為獎勵吧$R;" +
                        "$P這個是軍用盔甲$R;" +
                        "我想您會喜歡的$R;" +
                        "挺帥的！$R;");
                    GiveItem(pc, 60500000, 1);
                    if (CountItem(pc, 60500000) >= 1)
                    {
                        Say(pc, 131, "得到了『南軍皮盔甲』$R;");
                        Knights_mask.SetValue(Knights.獲得騎士團皮盔甲, true);
                        return;
                    }
                    Say(pc, 131, "…$R;" +
                        "$P行李太多了，不能給您阿$R;" +
                        "$R請扔掉或者減少道具後，再來吧$R;");
                    return;
                }
                Say(pc, 131, pc.Name + "?$R;" +
                    "來得好！$R;");
                //EVT1100003961
                switch (Select(pc, "怎麼辦呢？", "", "任務服務台", "什麼也不做"))
                {
                    case 1:
                        //GOTO EVT1100003913;
                        break;
                    case 2:
                        Say(pc, 131, "想執行任務嗎？$R;");
                        break;
                }
                return;
            }
            if (pc.Fame < 1)
            {
                Say(pc, 131, "我就是$R奧克魯尼亞混城騎士團南軍長官$R;" +
                    "我們軍隊是．．．$R;" +
                    "$P．．．？？$R;" +
                    "$P您到這個城市沒有多久吧？$R;" +
                    "$R對吧？$R逃不過我的眼睛的啦$R;" +
                    "$R我們軍隊雖然招募傭兵。$R但身份不明的人是不需要的$R;" +
                    "您首先在這個城市提高名氣後再來吧$R;");
                Knights_mask.SetValue(Knights.告知無法加入南軍, true);
                return;
            }
            if (!Knights_mask.Test(Knights.聽取南軍騎士團說明) && 
                !Knights_mask.Test(Knights.已經加入騎士團))
            {
                Say(pc, 131, "年輕人！$R我是奧克魯尼亞騎士團南團長官$R;" +
                    "格爾尼奥$R;" +
                    "我們騎士團需要您這樣的年輕人！$R;");
                switch (Select(pc, "怎麼辦呢？", "", "加入南軍", "聽一聽關於騎士團的故事", "不聽"))
                {
                    case 1:
                        switch (Select(pc, "真的想加入南軍嗎？", "", "入團", "不入團", "再想一想"))
                        {
                            case 1:
                                Say(pc, 131, "好！$R就是應該這樣！$R那麼就在這文件上簽名吧！$R;");
                                PlaySound(pc, 2030, false, 100, 50);
                                Say(pc, 131, "拿到文件了$R;");
                                switch (Select(pc, "想要簽名嗎？", "", "簽名", "還是放棄"))
                                {
                                    case 1:
                                        Say(pc, 131, "嗯…很好！$R;" +
                                            "$R現在開始$R您是我們奧克魯尼亞混城騎士團南軍$R見習軍團員啦$R;" +
                                            "$P先收下這個吧$R;");
                                        GiveItem(pc, 10041500, 1);
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "得到$R『阿伊恩薩烏斯騎士團證』$R;");
                                        Knights_mask.SetValue(Knights.加入南軍騎士團, true);
                                        Say(pc, 131, "這就是您的$R混城騎士團南軍團員的證明。$R;" +
                                            "$R同時是阿伊恩薩烏斯國的公民權，$R好好保管吧$R;" +
                                            "$P還有這是阿高普路斯市的通行證。$R;");
                                        GiveItem(pc, 10042800, 1);
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, 31, "得到$R『阿高普路斯通行證』$R;");
                                        Knights_mask.SetValue(Knights.Get_the_Uptown_Pass, true);
                                        Say(pc, 131, "不久我們軍隊會得到任務的。$R先認真鍛煉吧$R;" +
                                            "$R期待您的活躍表現唷。$R;");
                                        Knights_mask.SetValue(Knights.已經加入騎士團, true);
                                        break;
                                    case 2:
                                        Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                                break;
                            case 3:
                                Say(pc, 131, "是嗎？那麼我會等您的唷$R;" +
                                    "隨時來找我$R;");
                                Knights_mask.SetValue(Knights.Consider_joining_the_Knights, true);
                                break;
                        }
                        break;
                    case 2:
                        Say(pc, 131, "奧克魯尼亞混城騎士團$R是為了守護自由之都：阿高普路斯市$R;" +
                            "$R由世界的4大國家派遣的聯合軍，$R;" +
                            "$P南門由我們$R阿伊恩薩烏斯共和國管轄的$R;" +
                            "$R混城騎士團除了我們，$R還有東軍、西軍、北軍一共4個軍隊$R;" +
                            "最強的當然是我們南軍啦。$R;" +
                            "$P而且入團還有特權！$R入團者可以拿到$R阿伊恩薩烏斯公民權。$R;" +
                            "$R有了公民權可以在$R阿伊恩薩烏斯自由出入。$R阿伊恩薩烏斯出產的武器最強唷！$R;" +
                            "$P那麼！加入我們南軍？怎麼樣呢？$R;");
                        Knights_mask.SetValue(Knights.聽取騎士團說明, true);
                        Knights_mask.SetValue(Knights.聽取南軍騎士團說明, true);
                        switch (Select(pc, "想要加入嗎？", "", "入團", "不入團", "先想想"))
                        {
                            case 1:
                                Say(pc, 131, "哦！真的入團嗎！？$R;");
                                switch (Select(pc, "真的想加入南軍嗎？", "", "入團", "不入團", "再想一想"))
                                {
                                    case 1:
                                        Say(pc, 131, "好！$R就是應該這樣！$R那麼就在這文件上簽名吧！$R;");
                                        PlaySound(pc, 2030, false, 100, 50);
                                        Say(pc, 131, "拿到文件了$R;");
                                        switch (Select(pc, "想要簽名嗎？", "", "簽名", "還是放棄"))
                                        {
                                            case 1:
                                                Say(pc, 131, "嗯…很好！$R;" +
                                                    "$R現在開始$R您是我們奧克魯尼亞混城騎士團南軍$R見習軍團員啦$R;" +
                                                    "$P先收下這個吧$R;");
                                                GiveItem(pc, 10041500, 1);
                                                PlaySound(pc, 4006, false, 100, 50);
                                                Say(pc, 0, 131, "得到$R『阿伊恩薩烏斯騎士團證』$R;");
                                                Knights_mask.SetValue(Knights.加入南軍騎士團, true);
                                                Say(pc, 131, "這就是您的$R混城騎士團南軍團員的證明。$R;" +
                                                    "$R同時是阿伊恩薩烏斯國的公民權，$R好好保管吧$R;" +
                                                    "$P還有這是阿高普路斯市的通行證。$R;");
                                                GiveItem(pc, 10042800, 1);
                                                PlaySound(pc, 2040, false, 100, 50);
                                                Say(pc, 0, 31, "得到$R『阿高普路斯通行證』$R;");
                                                Knights_mask.SetValue(Knights.Get_the_Uptown_Pass, true);
                                                Say(pc, 131, "不久我們軍隊會得到任務的。$R先認真鍛煉吧$R;" +
                                                    "$R期待您的活躍表現唷。$R;");
                                                Knights_mask.SetValue(Knights.已經加入騎士團, true);
                                                break;
                                            case 2:
                                                Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                                                break;
                                        }
                                        break;
                                    case 2:
                                        Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                                        break;
                                    case 3:
                                        Say(pc, 131, "是嗎？那麼我會等您的唷$R;" +
                                            "隨時來找我$R;");
                                        Knights_mask.SetValue(Knights.Consider_joining_the_Knights, true);
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                                break;
                            case 3:
                                Say(pc, 131, "是嗎？那麼我會等您的唷$R;" +
                                    "隨時來找我$R;");
                                Knights_mask.SetValue(Knights.Consider_joining_the_Knights, true);
                                break;
                        }
                        break;
                    case 3:
                        Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                        break;
                }
                return;
            }
            if (!Knights_mask.Test(Knights.已經加入騎士團) &&
                Knights_mask.Test(Knights.聽取南軍騎士團說明))
            {
                Say(pc, 131, "原來是您阿$R;" +
                    "怎麼樣？$R是不是想參加我們的騎士團了？$R;");
                switch (Select(pc, "想要加入嗎？", "", "入團", "不入團", "先想想"))
                {
                    case 1:
                        Say(pc, 131, "哦！真的入團嗎！？$R;");
                        switch (Select(pc, "真的想加入南軍嗎？", "", "入團", "不入團", "再想一想"))
                        {
                            case 1:
                                Say(pc, 131, "好！$R就是應該這樣！$R那麼就在這文件上簽名吧！$R;");
                                PlaySound(pc, 2030, false, 100, 50);
                                Say(pc, 131, "拿到文件了$R;");
                                switch (Select(pc, "想要簽名嗎？", "", "簽名", "還是放棄"))
                                {
                                    case 1:
                                        Say(pc, 131, "嗯…很好！$R;" +
                                            "$R現在開始$R您是我們奧克魯尼亞混城騎士團南軍$R見習軍團員啦$R;" +
                                            "$P先收下這個吧$R;");
                                        GiveItem(pc, 10041500, 1);
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "得到$R『阿伊恩薩烏斯騎士團證』$R;");
                                        Knights_mask.SetValue(Knights.加入南軍騎士團, true);
                                        Say(pc, 131, "這就是您的$R混城騎士團南軍團員的證明。$R;" +
                                            "$R同時是阿伊恩薩烏斯國的公民權，$R好好保管吧$R;" +
                                            "$P還有這是阿高普路斯市的通行證。$R;");
                                        GiveItem(pc, 10042800, 1);
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, 31, "得到$R『阿高普路斯通行證』$R;");
                                        Knights_mask.SetValue(Knights.Get_the_Uptown_Pass, true);
                                        Say(pc, 131, "不久我們軍隊會得到任務的。$R先認真鍛煉吧$R;" +
                                            "$R期待您的活躍表現唷。$R;");
                                        Knights_mask.SetValue(Knights.已經加入騎士團, true);
                                        break;
                                    case 2:
                                        Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                                break;
                            case 3:
                                Say(pc, 131, "是嗎？那麼我會等您的唷$R;" +
                                    "隨時來找我$R;");
                                Knights_mask.SetValue(Knights.Consider_joining_the_Knights, true);
                                break;
                        }
                        break;
                    case 2:
                        Say(pc, 131, "是嗎？…我們軍隊非常強大阿，$R等什麼時候改變想法再來吧。$R;");
                        break;
                    case 3:
                        Say(pc, 131, "是嗎？那麼我會等您的唷$R;" +
                            "隨時來找我$R;");
                        Knights_mask.SetValue(Knights.Consider_joining_the_Knights, true);
                        break;
                }
                return;
            }
            Say(pc, 131, "我就是$R奧克魯尼亞混城騎士團南軍長官$R;" +
                "是我們軍隊中最強大的。$R;");
        }
    }
}