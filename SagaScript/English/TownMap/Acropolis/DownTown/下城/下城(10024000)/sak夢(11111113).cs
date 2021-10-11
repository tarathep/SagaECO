using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S11111113 : Event
    {
        public S11111113()
        {
            this.EventID = 11111113;

            this.leastQuestPoint = 1;

            this.alreadyHasQuest = "任務進行的還順利嗎?$R;";

            this.gotNormalQuest = "那就拜託了。$R;" +
                                  "$R等任務完成以後，再來找我吧。$R;";

            this.gotTransportQuest = "是阿，道具太重了吧?$R;" +
                                     "$R所以不能一次傳送的話，$R;" +
                                     "分成幾次給我也可以的。$R;";

            this.questCompleted = "真是辛苦了。$R;" +
                                  "$R恭喜你，任務完成了。$R;" +
                                  "$P來! 收下報酬吧!$R;";

            this.transport = "哦哦…全部都收集好了嗎?$R;";

            this.questCanceled = "嗯…如果是你，我相信你能做到的，$R;" +
                                 "很期待呢……$R;";

            this.questFailed = "……$R;" +
                               "$P失敗了?$R;" +
                               "$R真是鬧了大事，$R;" +
                               "不知道該說什麼好啊?$R;" +
                               "$P這次實在沒辦法了，$R;" +
                               "下次一定要成功啊!$R;" +
                               "$R知道了吧?$R;";

            this.notEnoughQuestPoint = "嗯…$R;" +
                                       "$R現在沒有要特別拜託的事情啊，$R;" +
                                       "再去冒險怎麼樣?$R;";

            this.questTooEasy = "唔…但是對你來說，$R;" +
                                "說不定是太簡單的任務。$R;" +
                                "$R那樣也沒關係嘛?$R;";

            this.questTooHard = "這對你來說有點困難啊?$R;" +
                                "$R這樣也沒關係嘛?$R;"; 
        }

        public override void OnEvent(ActorPC pc)
        {
                Say(pc, 131, "首先，我們十分歡迎你來到這個世界，$R;" +
                             "如Sak花所說，我是同為櫻之夢ECO的管理者 - 櫻の夢，簡稱Sak夢，$R;" +
                             "ECO吸引人的地方，除了可愛的人物做型，與別不同的世界觀之外，$R;" +
                             "還有一種特色，就是擁有各式各樣的任務供玩家接恰。$R;" +
                             "喜歡拿武器砍魔物？喜歡從魔物上收集物品？喜歡當個粗勞的跑腿？$R;" +
                             "當然，這些付出，並不是無代價的，報酬其實也相當的豐富。$R;" +
                             "想接更為特別的任務的話，隨時歡迎你過來找我。$R;" +
                             "我會在這裡，發放出一些與別不同的任務供你接恰。$R;", "SAK夢");
            switch (Select(pc, "做什麼呢？", "", "任務服務台", "什麼也不做"))
            {
                case 1:
                    HandleQuest(pc, 73);
                    break;
                case 2:
                    break;
            }
        }
    }
}
