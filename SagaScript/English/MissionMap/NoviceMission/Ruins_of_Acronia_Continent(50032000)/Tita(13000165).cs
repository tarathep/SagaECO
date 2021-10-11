using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞大陸廢墟(50032000) NPC基本信息:微微(13000165) X:100 Y:25
namespace SagaScript.M50032000
{
    public class S13000165 : Event
    {
        public S13000165()
        {
            this.EventID = 13000165;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (pc.CInt["Country"])
            {
                case 0:
                    Tasks_for_Novices_in_Taiwan(pc);
                    break;

                case 1:
                    Novice_missions_in_Japan(pc);
                    break;
            }
        }

        void Tasks_for_Novices_in_Taiwan(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = pc.CMask["Beginner_01"];

            byte x, y;

            if (pc.CInt["NewQuestHack"] == 1)
            {
                Say(pc, 131, "Why are you still here?? $R;");
                return;
            }
            if (Beginner_01_mask.Test(Beginner_01.Leaving_Acropolis))
            {
                Say(pc, 13000165, 132, "Are you injured? $R;" +
                                       "$R Fortunately...$R;" +
                                       "$R here is...$R;" +
                                       "A few hundred years ago, the mainland of Acronia, $R;" +
                                       "$R is destroying the world of Emil's $R;" +
                                       "The last battle...$R;", "Tita");

                Say(pc, 13000165, 131, "After the war, $R;" +
                                       "Emil World Long $R;" +
                                       "In a state of chaos and stagnation. $R;" +
                                       "$P don’t worry, $R;" +
                                       "$R you will welcome the $R of [our age];" +
                                       "Acropolis City, $R;" +
                                       "It will be peaceful and prosperous again. $R;", "Tita");

                Say(pc, 13000165, 132, "But...the danger has not completely disappeared. $R;" +
                                       "$R [Those guys]...$R;" +
                                       "The aggressor's offense is still going on! $R;" +
                                       "$P defeat the threat of [those guys]...$R;" +
                                       "$R don't worry, you will definitely do it!! $R;", "Tita");

                Say(pc, 13000165, 132, "It won’t take long...$R;" +
                                       "Dangerous luck will come here. $R;" +
                                       "$R come... I will send you to $R;" +
                                       "Our original era. $R;" +
                                       "$R See you next time!! $R;" +
                                       "$P is now during the event, $R;" +
                                       "If you choose [Continue listening to the story] $R;" +
                                       "$P I will give you an unexpected surprise! $R;", "Tita");

                switch (Select(pc, "What to do?", "", "Continue to listen to the story", "Go directly to Acropolis"))
                {
                    case 1:
                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10066307, 1);

                        Say(pc, 13000165, 131, "The present for you now, $R;" +
                                                "It is the $R that allows you to match your next world;" +
                                                "Invaders, a small gift specially given to you. $R;" +
                                                "$P don't drop it on the ground! $R;" +
                                                "$R if you don’t need it anymore, $R;" +
                                                "Please drop it in the nearby trash can. $R;", "Tita");

                        pc.CInt["NewQuestHack"] = 1;
                        pc.QuestRemaining += 5;
                        x = (byte)Global.Random.Next(199, 204);
                        y = (byte)Global.Random.Next(64, 68);
                        Warp(pc, 10018102, x, y);
                        break;

                    case 2:
                        pc.CInt["NewQuestHack"] = 1;
                        pc.QuestRemaining += 5;
                        x = (byte)Global.Random.Next(245, 250);
                        y = (byte)Global.Random.Next(126, 131);
                        Warp(pc, 10023100, x, y);
                        break;
                }
            }
        }

        void Novice_missions_in_Japan(ActorPC pc)
        {
            Say(pc, 132, "どこもお怪我はありませんか？$R;" +
            "$Rそう、よかった…。$R;" +
            "$Pここは…$R数百年前のアクロニア大陸。$R;" +
            "$Rエミルの世界を破壊しつくした$Rあの戦争の…最後の戦いの時……。$R;", "ティタ");

            Say(pc, 131, "戦争のあとエミルの世界は$R長い混乱と停滞の時代が続きました。$R;" +
            "$P心配しないで、$R;" +
            "$Rあなたがこれから向かう$R「あたしたちの時代」のアクロポリスは$R平和と繁栄を取り戻していますわ。$R;", "ティタ");

            Say(pc, 132, "ただ…脅威が$R完全に去ったわけではありません。$R;" +
            "$R「彼ら」…次元侵略者の侵攻は$R今でも深く静かに進行しています。$R;" +
            "$P本当の脅威を聡明な心で照らし出して。$R;" +
            "$R大丈夫、$Rあなたならきっと出来ますわ。$R;", "ティタ");

            Say(pc, 132, "もうすぐここにも$R恐ろしい瘴気が降ってきます。$R;" +
            "$Rさあ…もとの、あたしたちの時代に$Rティタがお送りしますわ。$R;" +
            "$R遠からずまたお目にかかりましょうね。$R;", "ティタ");

            ShowEffect(pc, 4023);
            Wait(pc, 1980);

            if (pc.Race == PC_RACE.EMIL)
            {
                Warp(pc, 30090002, 2, 2);
                pc.QuestRemaining += 5;
            }

            if (pc.Race == PC_RACE.TITANIA)
            {
                Warp(pc, 30140000, 12, 15);
                pc.QuestRemaining += 5;
            }

            if (pc.Race == PC_RACE.DOMINION)
            {
                Warp(pc, 30141000, 11, 17);
                pc.QuestRemaining += 5;
            }
        }
    }
}
