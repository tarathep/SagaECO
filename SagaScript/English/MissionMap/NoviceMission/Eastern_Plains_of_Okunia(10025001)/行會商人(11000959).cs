using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:GuildMerchant(11000959) X:40 Y:135
namespace SagaScript.M10025001
{
    public class S11000959 : Event
    {
        public S11000959()
        {
            this.EventID = 11000959;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000959, 131, "是初心者啊!$R;" +
                                   "我是做保管個人財產生意的人。$R;" +
                                   "$P嗯，我能告訴您的只有兩三點…$R;" +
                                   "$P使用道具或技能的時候，$R;" +
                                   "設置在「快捷鍵視窗」的話，$R;" +
                                   "會比較方便喔!$R;" +
                                   "$P快捷鍵視窗，$R;" +
                                   "就是「F1」或「F2」旁邊，$R;" +
                                   "長長的視窗呀!$R;" +
                                   "$P把道具拉到空格裡試試看吧!$R;" +
                                   "就這麼簡單，快捷鍵就設置完成了。$R;" +
                                   "$P那麼 按「F1」鍵「F2」鍵，$R;" +
                                   "就可以使用已設置的道具了。$R;" +
                                   "$R也可以設置$R;" +
                                   "「技能圖示」和「表情圖示」。$R;" +
                                   "$P實際冒險的時候，一定要使用看看啊!$R;" +
                                   "$P啊! 還有一個!$R;" +
                                   "主視窗右上方的鍵，已經按過了?$R;" +
                                   "$R按一次看看吧?$R;" +
                                   "$P主視窗的形態會變吧?$R;" +
                                   "$R按右上方的鍵，$R;" +
                                   "主視窗旁邊長長的光條，$R;" +
                                   "就會移到遊戲畫面的最下方喔!$R;" +
                                   "$P再一次按右邊底部的鍵，$R;" +
                                   "就會回到視窗狀態，$R;" +
                                   "按照自己的喜好選擇使用吧!$R;", "GuildMerchant");
        }
    }
}
