using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S11111145 : Event
    {
        public S11111145()
        {
            this.EventID = 11111145;
        }

        public override void OnEvent(ActorPC pc)
        {
            //ERROR CANNOT  FIND sakmask model
            /*{
                BitMask<sakmask> mask = pc.CMask["sakmask"];
                if (mask.Test(sakmask.已經與Sak花進行第一次對話))
                {
                    mask.SetValue(sakmask.已經與Sak花進行第一次對話, false);
                    Say(pc, 11000405, 131, "你好，在這種地方也能遇到你呢。$R;" +
                                           "$R我是櫻之夢ECO的管理者 : 櫻の花，簡稱Sak花$R;" +
                                           "$R剛來這個世界，會很不習慣吧？$R;" +
                                           "$R不一樣的世界，不一樣的玩家，$R;" +
                                           "$R不一樣的任務，還有，你自己。$R;" +
                                           "$R但是，不要害怕，被選上的勇者啊！$R;" +
                                           "請用自己的手和心，$R;" +
                                           "$R開創出屬於你自己的奇幻旅程吧。$R;" +
                                           "$R我會一直在這裡，$R;" +
                                           "$R期待著你所編寫出的故事。$R;" +
                                           "$R多多指教，同時，$R;" +
                                           "$R歡迎你進入這個世界。$R;" +
                                           "$R在下城東方也有同為管理員之一的Sak夢$R;" +
                                           "$R去找找他吧。$R;" +
                                           "$R如果遇到甚麼問題$R;" +
                                           "$R隨時歡迎你回來找我，$R;" +
                                           "$R我會一直在這裡，$R;" +
                                           "$R期待著你的成長，$R;" +
                                           "$R你的未來！$R;", "SAK花");
                }
                else
                {
                    mask.SetValue(sakmask.已經與Sak花進行第一次對話, true);
                    Say(pc, 11000405, 131, "你好，好久不見，很高興你來找我，$R;" +
                                           "對這個新世界開始適應了嗎…？$R;" +
                                           "在這裡的生活還算順利吧？$R;" +
                                           "請加油啊，$R;" +
                                           "我會一直在這裡，期待著你所編寫出的故事。$R;" +
                                           "有甚麼需要我可以幫忙的？$R;", "SAK花");
                    switch (Select(pc, "有甚麼需要我幫忙？", "", "購買強化結晶", "購買子彈", "購買子彈", "購買子彈&藥品部", "購買箭矢", "點物兌換", "什麼都不做"))
                    {
                        case 1:
                            OpenShopBuy(pc, 416);
                            break;
                        case 2:
                            OpenShopBuy(pc, 417);
                            break;
                        case 3:
                            OpenShopBuy(pc, 418);
                            break;
                        case 4:
                            OpenShopBuy(pc, 419);
                            break;
                        case 5:
                            OpenShopBuy(pc, 412);
                            break;
                        case 6:
                            if (CountItem(pc, 21050122) >= 1 && CountItem(pc, 21050123) >= 1 && CountItem(pc, 21050124) >= 1 && CountItem(pc, 21050125) >= 1 && CountItem(pc, 21050126) >= 1 && CountItem(pc, 21050127) >= 1 && CountItem(pc, 21050128) >= 1)
                               {
                            Say(pc, 11000405, 131, "咦…你好像集齊了八種顏色的禮品印刻哦？兌換禮品嗎？$R;", "SAK花");
                            switch (Select(pc, "兌換禮品嗎？", "", "兌換", "不兌換"))
                            {
                                case 1:
                                   GiveRandomTreasure(pc, "item");
                                   TakeItem(pc, 21050122, 1);
                                   TakeItem(pc, 21050123, 1);
                                   TakeItem(pc, 21050124, 1);
                                   TakeItem(pc, 21050125, 1);
                                   TakeItem(pc, 21050126, 1);
                                   TakeItem(pc, 21050127, 1);
                                   TakeItem(pc, 21050128, 1);
                                   break; 
                            }
                               }
                            else
                            Say(pc, 11000405, 131, "你沒有集齊八種顏色的禮品印刻哦$R;", "SAK花");
                            break;
                    }
                }
            }*/
        }
    }
}