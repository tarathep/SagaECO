using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:路利耶(11000921) X:223 Y:82
namespace SagaScript.M10018101
{
    public class S11000921 : Event
    {
        public S11000921()
        {
            this.EventID = 11000921;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11000921, 131, "您好!$R;" +
                                   "$R我來教您幾個關於方便玩ECO$R;" +
                                   "的幾個程序喔!$R;", "路利耶");

            selection = Select(pc, "想聽哪個說明呢?", "", "關於小地圖", "關於視點", "關於截圖", "關於環境設定", "不用了");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000921, 131, "小地圖是$R;" +
                                               "在您目前的位置地圖上，$R;" +
                                               "顯示您和傳送點位置的小畫面，$R;" +
                                               "是個非常方便的功能喔!$R;" +
                                               "$P點擊主視窗的「地圖」鍵。$R;" +
                                               "$R可以設定顯示/不顯示小地圖。$R;" +
                                               "$P按小地圖視窗的按鍵，$R;" +
                                               "還可以變更小地圖的大小呢!$R;" +
                                               "$R有1/4區域或整個區域，$R;" +
                                               "「世界地圖」可以在整個視窗顯示。$R;" +
                                               "$P現在說明您在地圖上看到的「點」。$R;" +
                                               "$R箭頭表示自己的位置，$R;" +
                                               "「紅點」是NPC的位置，$R;" +
                                               "「藍點」表示傳送點的位置。$R;" +
                                               "$P如果同一地區裡有同隊的隊員，$R;" +
                                               "隊員的位置也會顯示出來唷。$R;", "路利耶");
                        break;

                    case 2:
                        Say(pc, 11000921, 131, "現在說明遊戲裡的視點…$R;" +
                                               "及照相機的操作方法吧!$R;" +
                                               "$P任意在畫面上$R;" +
                                               "點擊右鍵一邊拖一邊移動試試看吧!$R;" +
                                               "$P…$R;" +
                                               "$P視點變了吧?$R;" +
                                               "$R選擇自己喜歡的視點進行遊戲即可。$R;" +
                                               "$P補充一下，$R;" +
                                               "多教您一個方便的技巧唷!$R;" +
                                               "$P將目標放在地面雙擊滑鼠右鍵，$R;" +
                                               "畫面就會朝向北邊。$R;" +
                                               "$R適合用於，$R;" +
                                               "當您搞不清自己位置的方向時使用。$R;" +
                                               "$P在「環境設定」裡，$R;" +
                                               "除了北邊$R;" +
                                               "也可以把畫面設定成別的方向喔!$R;" +
                                               "$R您可以按照喜好隨意設定的。$R;", "路利耶");
                        break;

                    case 3:
                        Say(pc, 11000921, 131, "想把遊戲畫面拍下時，$R;" +
                                               "點擊鍵盤上的「PrintScreen」鍵即可。$R;" +
                                               "$R點擊這個就可以保存畫面喔!$R;" +
                                               "$P「PrintScreen」畫面儲存的地方，$R;" +
                                               "根據每個人的電腦設定而不同。$R;" +
                                               "$R只要您看一下指定位置就清楚了!$R;", "路利耶");
                        break;

                    case 4:
                        Say(pc, 11000921, 131, "在「環境設定」視窗$R;" +
                                               "可以調整遊戲的顯示或聲音。$R;" +
                                               "$P螢幕右上方有按鍵，$R;" +
                                               "點擊從右數到左第三個鍵，$R;" +
                                               "會顯示「系統」視窗。$R;" +
                                               "$P…能看到視窗嗎?$R;" +
                                               "$P與我對話的時候也可以操作，$R;" +
                                               "現在也可以變更設定。$R;" +
                                               "$P用「系統」視窗右上方的$R;" +
                                               "「低」「普通」「高」，$R;" +
                                               "可以調整螢幕的一般設定。$R;" +
                                               "$R遊戲中覺得畫面處理速度過慢，$R;" +
                                               "就調整這裡試試看吧!$R;" +
                                               "$P詳細的設定方法，$R;" +
                                               "等您熟悉遊戲後再跟您說吧!$R;", "路利耶");
                        break;
                }

                selection = Select(pc, "想聽哪個說明呢?", "", "關於小地圖", "關於視點", "關於截圖", "關於環境設定", "不用了");                
            }
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11000921, 131, "埃米爾的話都聽過了嗎?$R;" +
                                   "最好先聽聽埃米爾的情報唷!", "路利耶");
        }  
    }
}
