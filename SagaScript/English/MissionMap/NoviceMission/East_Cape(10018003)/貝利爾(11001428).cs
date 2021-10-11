using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018003) NPC基本信息:貝利爾(11001428) X:136 Y:68
namespace SagaScript.M10018003
{
    public class S11001428 : Event
    {
        public S11001428()
        {
            this.EventID = 11001428;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_had_the_first_conversation_with_Berial))
            {
                Conversation_with_Belial_for_the_first_time(pc);
            }
            else
            {
                Say(pc, 11001428, 131, "哦?$R;" +
                                       "$R有什麼要問的嗎?$R;", "貝利爾");
            }

            if (Beginner_01_mask.Test(Beginner_01.Combat_teaching_begins) &&
                !Beginner_01_mask.Test(Beginner_01.Combat_teaching_completed))
            {
                戰鬥教學(pc);
                return;
            }

            if (Beginner_01_mask.Test(Beginner_01.Skill_teaching_begins) &&
                !Beginner_01_mask.Test(Beginner_01.Skill_teaching_completed))
            {
                技能教學(pc);
                return;
            }

            selection = Select(pc, "想聽哪個說明呢?", "", "「道具」使用方法", "「戰鬥」方法", "「技能」使用方法", "關於「獎勵點數」", "沒有想要問的");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001428, 131, "說明道具的使用方法之前，$R;" +
                                               "先來看看基本操作吧!$R;", "貝利爾");

                        Say(pc, 11001428, 131, "「道具」、「技能」；「裝備」$R;" +
                                               "視窗打開了嗎?$R;" +
                                               "$R這三種都可以在主視窗做開關唷!$R;" +
                                               "$P跟我說話的時候，$R;" +
                                               "也可以一邊操作，$R;" +
                                               "先練練吧!$R;" +
                                               "$P…$R;" +
                                               "$P不行?$R;" +
                                               "$P現在介紹$R;" +
                                               "「道具」和「裝備」視窗吧!$R;" +
                                               "$P我要說明的是$R;" +
                                               "關於「裝備」和「消耗道具」。$R;" +
                                               "$P「裝備」是$R;" +
                                               "武器、防具、裝飾品$R;" +
                                               "等道。具$R;" +
                                               "$R想佩帶這些時，$R;" +
                                               "只要雙擊道具就可以了。$R;" +
                                               "$P大部分的裝備佩帶後，$R;" +
                                               "外貌是會改變的唷!$R;" +
                                               "$R收集好看的裝備，$R;" +
                                               "也是一件快樂的事情呀!$R;" +
                                               "$P要確認是否裝置正確，$R;" +
                                               "打開「裝備」視窗即可。$R;" +
                                               "$R自己佩帶的裝備$R;" +
                                               "一眼就可以看出來了。$R;" +
                                               "$P當需要道具詳細說明時，$R;" +
                                               "對道具圖示點擊滑鼠右鍵。$R;" +
                                               "$R就可以確認，$R;" +
                                               "攻擊/防禦/道具裝備時的效果等。$R;" +
                                               "$P下一個是「消耗道具」。$R;" +
                                               "$P消耗道具是$R;" +
                                               "為了得到某種效果而使用的道具。$R;" +
                                               "$R簡單的說就是恢復道具啦!$R;" +
                                               "$P基本上，雙擊就可以使用囉。$R;" +
                                               "$R如果不能使用的話，$R;" +
                                               "可能是「材料道具」或「貴重物品」。$R;" +
                                               "$P有關於材料道具的消息，$R;" +
                                               "「道具精製師」很清楚唷!$R;" +
                                               "$R他一直在「阿高普路斯市」前，$R;" +
                                               "有時間就去找他吧!$R;", "貝利爾");
                        break;

                    case 2:
                        if (!Beginner_01_mask.Test(Beginner_01.Have_already_conducted_combat_teaching_with_Belial))
                        {
                            Beginner_01_mask.SetValue(Beginner_01.Have_already_conducted_combat_teaching_with_Belial, true);
                            Beginner_01_mask.SetValue(Beginner_01.Combat_teaching_begins, true);

                            Say(pc, 11001428, 131, "透過實際作戰，最容易瞭解了，$R;" +
                                                   "但要先告訴您一些注意事項唷!$R;" +
                                                   "主視窗的「HP」「MP」「SP」$R;" +
                                                   "這三個數值。$R;" +
                                                   "$P在戰鬥時一定要注意。$R;" +
                                                   "$P特別是HP值達到0，就死亡了。$R;" +
                                                   "$P使用「技能」，$R;" +
                                                   "「MP」值和「SP」值就會減少呀!$R;" +
                                                   "$R即使數值跌至0也沒問題，$R;" +
                                                   "但是就不能有效地戰鬥囉。$R;" +
                                                   "$P戰鬥時要點擊敵人，$R;" +
                                                   "$R那麼就帶著配劍，$R;" +
                                                   "跟「微型皮露露」打一仗吧!$R;" +
                                                   "$P您應該有配劍吧?$R;" +
                                                   "裝備好後就出來練習吧!$R;" +
                                                   "$R只要在道具視窗雙擊配劍就可以裝上。$R;" +
                                                   "$P對付這些傢伙不用費太多力氣，$R;" +
                                                   "他們很弱，不是很強呀!$R;", "貝利爾");
                            return;
                        }
                        else
                        {
                            Say(pc, 11001428, 131, "還想聽關於戰鬥嗎? 現在做簡單說明。$R;" +
                                                   "$P戰鬥時，要注意以下幾點唷!$R;" +
                                                   "主視窗的「HP」「MP」「SP」$R;" +
                                                   "這三個數值。$R;" +
                                                   "$P戰鬥時一定要注意。$R;" +
                                                   "$P特別是HP值達到0，就死亡了。$R;" +
                                                   "$P使用「技能」，$R;" +
                                                   "「MP」值和「SP」值就會減少呀!$R;" +
                                                   "$R即使數值跌至0也沒問題，$R;" +
                                                   "但是就不能有效地戰鬥囉。$R;" +
                                                   "$P先告訴您，$R;" +
                                                   "戰鬥中失去的HP值的恢復方法吧!$R;" +
                                                   "$R要恢復HP，可以使用道具，$R;" +
                                                   "也可以透過「坐下」來恢復。$R;" +
                                                   "$P只要點擊「Insert」鍵就可以坐下。$R;" +
                                                   "$P若您再點擊一次，$R;" +
                                                   "或做其他動作的話，就會自動解除。$R;" +
                                                   "$P還有，在城市裡或在帳篷裡，$R;" +
                                                   "會自動恢復喔!$R;" +
                                                   "$R既不用花錢又有效呀$!R;", "貝利爾");
                        }
                        break;

                    case 3:
                        if (!Beginner_01_mask.Test(Beginner_01.Already_had_skills_teaching_with_Belial))
                        {
                            Beginner_01_mask.SetValue(Beginner_01.Already_had_skills_teaching_with_Belial, true);

                            Say(pc, 11001428, 131, "那麼現在為您解釋「技能」吧!$R;" +
                                                   "$P「初心者」時，只能普通攻擊。$R;" +
                                                   "$R轉職後，才可以使用各種技能唷!$R;" +
                                                   "$P怎麼樣，想試試嗎?$R;", "貝利爾");

                            switch (Select(pc, "想要試一下技能嗎?", "", "想試試", "聽說明就夠了"))
                            {
                                case 1:
                                    Beginner_01_mask.SetValue(Beginner_01.Skill_teaching_begins, true);

                                    PlaySound(pc, 2040, false, 100, 50);
                                    GiveItem(pc, 20050006, 1);
                                    Say(pc, 0, 0, "取得了技能石『力量牆』$R;", " ");

                                    Say(pc, 11001428, 131, "剛才給您的叫技能石，$R;" +
                                                           "是只能使用一次技能的道具呀!$R;" +
                                                           "$P使用這石頭就可以$R;" +
                                                           "體驗「力量提升」的技能喔!$R;" +
                                                           "$R這是可以馬上提高$R;" +
                                                           "原有潛在力的技能，$R;" +
                                                           "主要用於自己或隊員。$R;" +
                                                           "$P使用這技能，$R;" +
                                                           "跟魔物打一架試試看吧!$R;", "貝利爾");
                                    return;

                                case 2:
                                    Say(pc, 11001428, 131, "技能指的是，$R;" +
                                                           "消耗SP值和MP值$R;" +
                                                           "來使用的「技術」和「魔法」唷!$R;", "貝利爾");

                                    Say(pc, 11001428, 131, "您現在可以打開「技能視窗」，$R;" +
                                                           "熟悉或使用技能。$R;" +
                                                           "$P想要知道效果，$R;" +
                                                           "可以點擊「技能視窗」的圖示進行確認。$R;" +
                                                           "還有，有一些技能，$R;" +
                                                           "不配戴特定裝備，就不能使用唷!$R;" +
                                                           "$R學習技能前，先確認一下吧!$R;" +
                                                           "$P使用技能時，$R;" +
                                                           "一定要注意MP值跟SP值喔!$R;" +
                                                           "$P恢復方法和HP值一樣，$R;" +
                                                           "可以坐著恢復，$R;" +
                                                           "也可以使用道具，$R;" +
                                                           "或者是在城市/帳篷裡自動恢復唷!$R;", "貝利爾");
                                    break;
                            }
                        }
                        else 
                        {
                            Say(pc, 11001428, 131, "想再聽一遍嗎?$R;" +
                                                   "$R第二次就不能試驗道具囉。$R;" +
                                                   "那就做個簡單的說明吧!$R;" +
                                                   "技能指的是，消耗SP值跟MP值，$R;" +
                                                   "來使用的「技術」和「魔法」唷!$R;", "貝利爾");

                            Say(pc, 11001428, 131, "您現在可以打開「技能視窗」，$R;" +
                                                   "熟悉或使用技能。$R;" +
                                                   "$P想要知道效果，$R;" +
                                                   "可以點擊「技能視窗」的圖示進行確認。$R;" +
                                                   "還有，有一些技能，$R;" +
                                                   "不配戴特定裝備，就不能使用唷!$R;" +
                                                   "$R學習技能前，先確認一下吧!$R;" +
                                                   "$P使用技能時，$R;" +
                                                   "一定要注意MP值跟SP值喔!$R;" +
                                                   "$P恢復方法和HP值一樣，$R;" +
                                                   "可以坐著恢復，$R;" +
                                                   "也可以使用道具，$R;" +
                                                   "或者是在城市/帳篷裡自動恢復唷!$R;", "貝利爾");
                        }
                        break;

                    case 4:
                        Say(pc, 11001428, 131, "「獎勵點數」是提高自己$R;" +
                                               "各種能力值的非常重要的點數唷!$R;" +
                                               "$P先暫時打開狀態視窗吧!$R;" +
                                               "$P打開了嗎?$R;" +
                                               "$P視窗右下方有個小的「+」鍵對吧?$R;" +
                                               "$R點擊一下吧!$R;" +
                                               "$P在這裡可以分配「獎勵點數」。$R;" +
                                               "$R點一下閃爍的「△」，會提升數值，$R;" +
                                               "上升的數值就會變成藍色。$R;" +
                                               "$P千萬不要立刻點擊 「確認」啊!$R;" +
                                               "$R點擊「確認」後，$R;" +
                                               "就不能再修改了，$R;" +
                                               "所以要想清楚了再分配呀!$R;" +
                                               "$P想修改的話，請點擊「取消」或$R;" +
                                               "「獎勵點數」視窗旁邊的「-」，$R;" +
                                               "就會回到開始狀態唷!$R;" +
                                               "$P這個數值對將來影響很大，$R;" +
                                               "現在先不要分配吧!$R;" +
                                               "$P在升級的時候，$R;" +
                                               "您會自動獲得「獎勵點數」唷!$R;" +
                                               "$R不好好分配能力值，是不行的，$R;" +
                                               "現在還需要增強實力呢!$R;", "貝利爾");
                        break;
                }

                selection = Select(pc, "想聽哪個說明呢?", "", "「道具」使用方法", "「戰鬥」方法", "「技能」使用方法", "關於「獎勵點數」", "沒有想要問的");
            }
            

            if (!Beginner_01_mask.Test(Beginner_01.Belial_gives_the_novice_ribbon))
            {
                Belial_gives_the_novice_ribbon(pc);
            }

            Say(pc, 11001428, 131, "下一個地圖，瑪莎會告訴您的。$R;" +
                                   "$P她是個埃米爾族的女孩，$R;" +
                                   "雖然有點麻煩呀!$R;" +
                                   "$R剛剛說的不要告訴瑪莎喔!$R;" +
                                   "$R瑪莎會告訴您有關$R;" +
                                   "「飛空庭」的消息唷!$R;" +
                                   "$P順著這條路一直往前走，$R;" +
                                   "她就在前面，$R;" +
                                   "一邊走一邊看著小地圖就不會迷路的。$R;" +
                                   "$P哎呀，對了!$R;" +
                                   "$R路上遇到其他人，也跟他們聊聊吧!$R;" +
                                   "$R他們會告訴您這個世界的歷史，$R;" +
                                   "或系統之類的喔!$R;" +
                                   "$P要是走錯路也不要擔心。$R;" +
                                   "$R剛才，聽路利耶說過吧?$R;" +
                                   "小地圖的紅點就表示NPC的位置。$R;" +
                                   "$P迷路時，$R;" +
                                   "往那個點走過去就可以了。$R;" +
                                   "$R那麼加油吧!!$R;", "貝利爾");
        }

        void Conversation_with_Belial_for_the_first_time(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01. Have_had_the_first_conversation_with_Berial, true);

            byte x, y;

            Say(pc, 11001428, 131, "很高興第一次見到您，$R;" +
                                   "我叫貝利爾。$R;" +
                                   "$R從埃米爾那裡受了委託，$R;" +
                                   "為剛來的人介紹各種事情唷!$R;" +
                                   "$P說來話長…$R;" +
                                   "沒關係嗎?$R;", "貝利爾");

            switch (Select(pc, "您要從哪開始聽?", "", "我想要從頭開始聽!", "跳過戰鬥指導部分", "到指導的最後部分"))
            {
                case 1:
                    Say(pc, 11001428, 131, "好了嗎? 那麼開始吧!$R;" +
                                           "$R我只告訴您基本的概念，$R;" +
                                           "其他的您自己修煉吧!$R;", "貝利爾");
                    break;

                case 2:
                    Say(pc, 11001428, 131, "哦? 是嗎?$R;" +
                                           "跟我沒關係呢…$R;", "貝利爾");

                    switch (Select(pc, "要聽說明嗎?", "", "我想聽喔", "到下一個階段"))
                    {
                        case 1:
                            Say(pc, 11001428, 131, "好了嗎? 那麼開始吧!$R;" +
                                                   "$R我只告訴您基本的概念，$R;" +
                                                   "其他的您自己修煉吧!$R;", "貝利爾");
                            break;

                        case 2:
                            Say(pc, 11001428, 131, "是這樣嗎?$R;" +
                                                   "下一個地圖，瑪莎會告訴您的。$R;" +
                                                   "$P她是個埃米爾族的女孩，$R;" +
                                                   "雖然有點麻煩呀!$R;" +
                                                   "$R剛剛說的不要告訴瑪莎喔!$R;" +
                                                   "$R瑪莎會告訴您有關$R;" +
                                                   "「飛空庭」的消息唷!$R;" +
                                                   "$P順著這條路一直往前走，$R;" +
                                                   "她就在前面，$R;" +
                                                   "一邊走一邊看著小地圖就不會迷路的。$R;" +
                                                   "$P哎呀，對了!$R;" +
                                                   "$R路上遇到其他人，也跟他們聊聊吧!$R;" +
                                                   "$R他們會告訴您這個世界的歷史，$R;" +
                                                   "或系統之類的喔!$R;" +
                                                   "$P要是走錯路也不要擔心。$R;" +
                                                   "$R剛才，聽路利耶說過吧?$R;" +
                                                   "小地圖的紅點就表示NPC的位置。$R;" +
                                                   "$P迷路時，$R;" +
                                                   "往那個點走過去就可以了。$R;" +
                                                   "$R那麼加油吧!!$R;", "貝利爾");
                            break;
                    }
                    break;

                case 3:
                    Say(pc, 11001428, 131, "是嗎?$R;" +
                                           "我無所謂。$R;", "貝利爾");

                    switch (Select(pc, "要繼續聽說明嗎?", "", "繼續聽", "放棄"))
                    {
                        case 1:
                            Say(pc, 11001428, 131, "好了嗎? 那麼開始吧!$R;" +
                                                   "$R我只告訴您基本的概念，$R;" +
                                                   "其他的您自己修煉吧!$R;", "貝利爾");
                            break;

                        case 2:
                            Say(pc, 11001428, 131, "我知道了$R;" +
                                                   "$R我就送您到「阿高普路斯市」吧!$R;" +
                                                   "$P不會說我在趕您走吧?$R;" +
                                                   "$P提多在前面…$R;" +
                                                   "問他就可以了，$R;" +
                                                   "$R那麼加油吧!$R;", "貝利爾");

                            x = (byte)Global.Random.Next(18, 29);
                            y = (byte)Global.Random.Next(124, 130);

                            Warp(pc, 10025001, x, y);
                            break;
                    }
                    break;
            }
        }     

        void 戰鬥教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.HP_recovery_teaching_completed))
            {
                Beginner_01_mask.SetValue(Beginner_01.HP_recovery_teaching_completed, true);

                Say(pc, 11001428, 131, "大概知道是什麼意思了吧?$R;" +
                                       "當您HP值下降，就吃這個吧!$R;", "貝利爾");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10006350, 1);
                Say(pc, 0, 0, "得到「燻肉」!$R;", " ");
            }
            else
            {
                Beginner_01_mask.SetValue(Beginner_01.Combat_teaching_completed, true);

                Say(pc, 11001428, 131, "HP值恢復了吧?$R;" +
                                       "$R要恢復HP，可以使用道具，$R;" +
                                       "也可以透過「坐下」來恢復。$R;" +
                                       "$P只要點擊「Insert」鍵就可以坐下。$R;" +
                                       "$P若您再點擊一次，$R;" +
                                       "或做其他動作的話，就會自動解除。$R;" +
                                       "$P還有，在城市裡或在帳篷裡，$R;" +
                                       "會自動恢復喔!$R;" +
                                       "$R既不用花錢又有效呀!$R;", "貝利爾");
            }
        }

        void 技能教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Get_the_second_skill_stone))
            {
                Beginner_01_mask.SetValue(Beginner_01.Get_the_second_skill_stone, true);

                Say(pc, 11001428, 131, "怎麼樣?$R;" +
                                       "攻擊力量是不是變強大了?$R;", "貝利爾");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 20050005, 1);
                Say(pc, 0, 0, "取得了技能石『火石』$R;", " ");

                Say(pc, 11001428, 131, "第二種技能是「烈焰焚城」喔!$R;" +
                                       "$R就是其中一種「魔法」技能，$R;" +
                                       "用來催化世界上存在的各種力量。$R;" +
                                       "$P選擇魔法師系列的職業，$R;" +
                                       "就可以學到唷!$R;" +
                                       "$P使用方法是雙擊技能石，$R;" +
                                       "浮標就會變成魔杖形狀，$R;" +
                                       "然後點擊施放技能的地方。$R;" +
                                       "$R時間控制並不容易，$R;" +
                                       "但還是試試吧!$R;", "貝利爾");
                return;
            }

            if (!Beginner_01_mask.Test(Beginner_01.Get_the_third_skill_gem))
            {
                Beginner_01_mask.SetValue(Beginner_01.Get_the_third_skill_gem, true);

                Say(pc, 11001428, 131, "怎麼樣? 使用華麗的技能，$R;" +
                                       "很爽吧?$R;", "貝利爾");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 20050001, 1);
                Say(pc, 0, 0, "取得了技能石『旋風劍』$R;", " ");

                Say(pc, 11001428, 131, "第三種技能是「旋風劍」喔!$R;" +
                                       "$P使用這個技能，$R;" +
                                       "會給魔物非常強烈的打擊呀!$R;" +
                                       "$R這種叫「技術」的技能，$R;" +
                                       "主要是給戰士系列職業學的技能唷!$R;" +
                                       "$P這種技能需要透過劍來施放。$R;" +
                                       "$R沒有劍的人就不能使用，要注意唷!$R;" +
                                       "$P需要詳細說明時，$R;" +
                                       "就在技能圖示點擊右鍵吧!$R;" +
                                       "$R使用條件以及效果都有詳細說明。$R;" +
                                       "$P被敵人包圍時，$R;" +
                                       "使用這種技能非常有效呀!$R;" +
                                       "$R試一下吧!$R;", "貝利爾");
                return;
            }
            else
            {
                Beginner_01_mask.SetValue(Beginner_01.Skill_teaching_completed, true);

                Say(pc, 11001428, 131, "有沒有喜歡的技能呢?$R;" +
                                       "試驗技能已經完畢囉。$R;" +
                                       "$P各職業使用技能，$R;" +
                                       "都會消耗MP值和SP值喔!$R;" +
                                       "$R所以不僅HP值，$R;" +
                                       "同時也要留意MP值跟SP值呀!$R;" +
                                       "$P恢復方法和HP值一樣，$R;" +
                                       "可以坐著恢復，也可以使用道具，$R;" +
                                       "也可以在城市或帳篷裡自動恢復唷!$R;", "貝利爾");
            }
        }

        void Belial_gives_the_novice_ribbon(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.Belial_gives_the_novice_ribbon, true);

            Say(pc, 11001428, 131, "基本的概念，$R;" +
                                   "現在大概清楚了吧?$R;" +
                                   "$P對了，還有這個給您…$R;", "貝利爾");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 50053600, 1);
            Say(pc, 0, 0, "得到『初心者緞帶』!$R;", " ");

            Say(pc, 11001428, 131, "給您的『初心者緞帶』，$R;" +
                                   "看名稱就知道是初心者用的裝備。$R;" +
                                   "$R裝備『初心者緞帶』時，$R;" +
                                   "只要雙擊道具就可以了，$R;" +
                                   "以後再帶吧!$R;" +
                                   "$P只要帶著這個，$R;" +
                                   "別人一定會幫忙的唷!$R;" +
                                   "$P…對了，$R;" +
                                   "$P等您成長強大後。$R;" +
                                   "看到有其他人帶著它，$R;" +
                                   "就可以知道那人需要幫忙了喔!$R;" +
                                   "$R到時去幫幫他吧，$R;" +
                                   "剛開始冒險的初心者，$R;" +
                                   "心中總有點不安吧?$R;", "貝利爾");
        }  
    }
}
