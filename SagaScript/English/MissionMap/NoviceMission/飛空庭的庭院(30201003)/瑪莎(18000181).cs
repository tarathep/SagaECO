﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30201003
{
    public class S18000181 : Event
    {
        public S18000181()
        {
            this.EventID = 18000181;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Masha))
            {
                與瑪莎進行第一次對話(pc);
                return;
            }
            else
            {
                與瑪莎進行第二次對話(pc);
                return;
            }
        }

        void 與瑪莎進行第一次對話(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.Have_already_had_the_first_conversation_with_Masha, true);

            Say(pc, 159, "あたしの飛空庭にようこそ！$R;" +
"あたしの名前はマーシャ！$R;" +
"よろしくね！$R;" +
"$Pエミルの話長くなかった？$R;" +
"エミルったら親切丁寧に$R;" +
"教えるのはいいんだけど$R;" +
"話が長いのよねぇ。$R;" +
"$Pそういえば新米冒険者さんは$R;" +
"飛空庭ははじめて？$R;" +
"$P飛空庭っていうのは$R;" +
"簡単に言うと、呼んで字の如く$R;" +
"空飛ぶお庭のことね。$R;" +
"この世界では、あなただけの庭と家を$R;" +
"持つことができるのよ！$R;" +
"家を建てれば、部屋の中も自由に$R;" +
"コーディネイトできるわよ♪$R;" +
"$Pお友達に遊びに来てもらうことだって$R;" +
"できるし、一人だけでも$R;" +
"使うこともできるわ。$R;" +
"$P飛空庭は「トンカ」の職人さんが$R;" +
"つくっているの。$R;" +
"部品を集めるのがちょっと$R;" +
"大変かもしれないけど。$R;" +
"$Pあ、そうだ！$R;" +
"街に行ったら、職人さんの居場所を$R;" +
"聞いてみるといいわ。$R;" +
"とっても気さくな人で、部品を無料で$R;" +
"毎週配ってるって話よ。$R;" +
"$P飛空庭があれば何かと便利よ♪$R;" +
"留められる場所は限られているけど$R;" +
"色々な所に移動できるし。$R;" +
"$Pとはいえ、それはこの世界だけで、$R;" +
"他の世界にいけるわけじゃ$R;" +
"ないんだけどね。$R;", "マーシャ");
            Say(pc, 131, "そういえば、知ってる？$R;" +
"あたし達エミルとは違って、$R;" +
"タイタニアやドミニオンは、$R;" +
"一定の年齢に達すると、$R;" +
"このエミルの世界にやってくるの。$R;" +
"$Pタイタニアの人達は、何か大事な$R;" +
"「使命」を探すためにやってくる……$R;" +
"って聞いたわ。$R;" +
"でも、その使命がなんなのかとか、$R;" +
"タイタニアの世界のこととか、$R;" +
"記憶は封印されているんですって。$R;" +
"$Pだから、タイタニアの世界は$R;" +
"いまだに謎が多くて、詳しいことは$R;" +
"分かっていないのよ。$R;" +
"$Pドミニオンの人達は、戦いを求めて$R;" +
"この世界にやってくる……って$R;" +
"言われているけど、それは必ずしも$R;" +
"正確ではないわね。$R;" +
"$Pドミニオンの友達から聞いたんだけど$R;" +
"ドミニオンの世界は、はるか昔から$R;" +
"ずっと戦争が続いているらしいの。$R;" +
"他の世界からの侵略者は強力で、$R;" +
"それに対抗する力を身につけるため$R;" +
"ドミニオンはこの世界に来るみたい。$R;" +
"$Pどっちもいろいろな人がいるわ。$R;" +
"もちろん、あたし達エミルにもね。$R;" +
"$Pだから、他の種族の人とも、$R;" +
"仲良くしてあげてね。$R;" +
"$Rということで、改めてよろしくね。$R;" +
"あたしとも仲良くしてよね♪$R;", "マーシャ");
            Say(pc, 131, "それじゃあ、アクロポリス到着まで$R;" +
            "少し時間かかるから$R;" +
            "家の中とか見てってね♪$R;", "マーシャ");
        }

        void 與瑪莎進行第二次對話(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);
            BitMask<Beginner_02> Beginner_02_mask = pc.CMask["Beginner_02"];
            if (CountItem(pc, 10063700) >= 1)
            {
                Say(pc, 131, "『マーシャと話す』を選んでみて！$R;", "マーシャ");
            }
            else
            {
                if (!Beginner_02_mask.Test(Beginner_02.Get_chocolate_chip_cookies_and_juice))
                {
                    Beginner_02_mask.SetValue(Beginner_02.Get_chocolate_chip_cookies_and_juice, true);
                    Say(pc, 131, "はいっ、お菓子の準備ができたよ！$R;" +
                    "少し休んでいってね。$R;", "マーシャ");
                    ShowEffect(pc, 18000181, 4520);
                    Wait(pc, 990);

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10009450, 1);
                    GiveItem(pc, 10001600, 1);
                }


                if (Beginner_01_mask.Test(Beginner_01.Found_something_under_the_bed))
                {
                    ShowEffect(pc, 11000938, 4520);
                    Wait(pc, 990);

                    Say(pc, 131, "あぁぁぁ！！$R;" +
    "$Rご、『合成失敗物』…！！$R;" +
    "それ…どこで！？$R;" +
    "え、モモが見つけた！？$R;" +
    "$P……はぁ～。$R;" +
    "$P見つかったなら仕方ないか。$R;" +
    "料理や合成は、失敗することも$R;" +
    "あるのよ。$R;" +
    "『合成失敗物』はその時にできる……の。$R;", "マーシャ");
                    ShowEffect(pc, 18000181, 4507);
                    Wait(pc, 990);

                    Say(pc, 131, "た、たまに！$R;" +
                    "たま～～～～～にだからね！！$R;", "マーシャ");

                    Say(pc, 134, "うぅっ…$R;" +
                    "見つからないように、あとで$R;" +
                    "ジャンク屋さんに持って行く$R;" +
                    "つもりだったのに……。$R;", "マーシャ");

                    Say(pc, 131, "さて、それじゃあ、$R;" +
                    "そろそろ$R;" +
                    "アクロポリスシティに向けて$R;" +
                    "どーんっと飛ばしていくわよ！$R;" +
                    "$Pっとその前に、$R;" +
                    "はい、これどうぞ！$R;", "マーシャ");

                    Say(pc, 131, "それは、$R;" +
                    "マーシャホットラインって言って$R;" +
                    "いつでも、どこでもあたしと$R;" +
                    "お話ができる便利アイテムなの！$R;" +
                    "$Pこれから、冒険に出て、$R;" +
                    "困ったことや、何をすればいいのか$R;" +
                    "わからなくなったら$R;" +
                    "マーシャホットラインを使ってね♪$R;" +
                    "$Pどこに行けばいいのか、$R;" +
                    "何をすればいいのか$R;" +
                    "アドバイスをするからね！$R;" +
                    "さらに、もう一つの機能として$R;" +
                    "マルチチャンネルがあるわ！$R;" +
                    "$Pマルチチャンネルは、$R;" +
                    "他の冒険者さんに質問をするための$R;" +
                    "「初心者掲示板」や$R;" +
                    "冒険に関していろんなヒントが書かれた$R;" +
                    "「ヘルプ機能」があるわ！$R;" +
                    "$Pまぁ、エミルじゃないけど$R;" +
                    "「習うより慣れろ！」だね！$R;" +
                    "さっそくそれを使って$R;" +
                    "『マーシャと話す』を選んでみて！$R;", "マーシャ");
                    GiveItem(pc, 10063700, 1);

                }
            }
        }
    }
}
namespace SagaScript.庭外
{
    public class S10001315 : Event
    {
        public S10001315()
        {
            this.EventID = 10001315;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 30202002, 9, 14);
        }
    }
}