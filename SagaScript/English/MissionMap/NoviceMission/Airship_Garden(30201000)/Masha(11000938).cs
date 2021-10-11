using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:飛空庭的庭院(30201000) NPC基本信息:瑪莎(11000938) X:9 Y:10
namespace SagaScript.M30201000
{
    public class S11000938 : Event
    {
        public S11000938()
        {
            this.EventID = 11000938;
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

            Say(pc, 11000938, 131, "Welcome to the Airship Garden! $R;" +
                                   "$P Flying Sky Garden is a home and garden flying in the sky! $R;" +
                                   "$R is in the ECO world, $R;" +
                                   "You can own your own house and garden. $R;" +
                                   "$P Feikong Garden is made by the craftsmen of [Tongka]. $R;" +
                                   "$R is a little difficult to collect parts...$R;" +
                                   "$P is right! $R;" +
                                   "There is a craftsman in a village in $R, look for it, $R;" +
                                   "Others are pretty good, $R;" +
                                   "Some parts will be distributed for free every week! $R;" +
                                   "$P heard somewhere in the south...$R;" +
                                   "$P flying in the sky! $R;" +
                                   "$R is in the ECO world, $R;" +
                                   "Very important means of transportation! $R;" +
                                   "$P to [Morg City] $R;" +
                                   "You must take the Flying Garden! $R;" +
                                   "The $R channel is managed by the army and guilds. $R;" +
                                   "$P Airship Garden owned by ordinary people $R;" +
                                   "It can only be used as a house and a garden! $R;" +
                                   "The $R house is built, you can decorate it as you like! $R;" +
                                   "You can let your friends in, $R;" +
                                   "Or designate some people to enter, $R;" +
                                   "You can also choose not to open. $R;" +
                                   "$P everyone follows their own hobbies. $R;" +
                                   "Can present a unique style! $R;" +
                                   "$R and, $R;" +
                                   "Feikongting can only be returned at the designated location! $R;" +
                                   "$R can only be used in the [airport] dedicated to the Airship Garden, $R;" +
                                   "It can be returned! $R;" +
                                   "$P is actually not allowed to be returned here. $R;" +
                                   "$R made an exception just to show you! $R;" +
                                   "$P go into the house and take a look? $R;" +
                                   "$R if you think about the next stage, $R;" +
                                   "Just tell me! $R;", "Masha");
        }

        void 與瑪莎進行第二次對話(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);
            BitMask<Beginner_02> Beginner_02_mask =pc.CMask["Beginner_02"];

            byte x, y;
            if (!Beginner_02_mask.Test(Beginner_02.Get_chocolate_chip_cookies_and_juice))
            {
                Beginner_02_mask.SetValue(Beginner_02.Get_chocolate_chip_cookies_and_juice, true);
                Say(pc, 11000938, 131, "Here are biscuits! $R;" +
                                        "Eat and rest at the same time! $R;", "Marsha");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10009450, 1);
                GiveItem(pc, 10001600, 1);
                Say(pc, 0, 131, "Get [Chocolate Chip Cookies] and [Juice]! $R;", "");
            }
            if (Beginner_01_mask.Test(Beginner_01.Found_something_under_the_bed))
            {
                ShowEffect(pc, 11000938, 4520);
                Wait(pc, 990);

                Say(pc, 11000938, 131, "Ahhhhh!!$R;" +
                                       "$R this is the [synthesis failure]?? $R;" +
                                       "Where did this come from? $R;" +
                                       "$R what? Flying mouse found it? $R;" +
                                       "$P alas...$R;" +
                                       "$P is found and there is no way. $R;" +
                                       "$R cooking or synthesis, sometimes it fails! $R;" +
                                       "What happened at that time... is the [synthetic failure]! $R;", "Masha");

                ShowEffect(pc, 11000938, 4507);
                Wait(pc, 990);

                Say(pc, 11000938, 131, "It is not often failed, $R;" +
                                       "Just I~~~ you!!$R;", "Masha");

                Say(pc, 11000938, 134, "Woo...$R;" +
                                       "$R originally wanted to take it to the antique store by myself. $R;" +
                                       "$P now go to the next stage? $R;", "Masha");
            }

            switch (Select(pc, "Go to the next stage?", "", "Go to the next stage", "Tell me information about Feikongyuan again", "Resting for a while"))
            {
                case 1:
                    Say(pc, 11000938, 131, "Then go now! $R;" +
                                           "$P but...$R;" +
                                           "$R from the place just now to the city, $R;" +
                                           "It takes some time! $R;" +
                                           "$P so I use Fei Kongting $R;" +
                                           "Send you there! $R;" +
                                           "$P is actually not allowed to use the Sky Court. $R;" +
                                           "$R but to help beginners, it should be no problem! $R;" +
                                           "$P then let's go! $R;", "Masha");

                    PlaySound(pc, 2438, false, 100, 50);
                    Wait(pc, 1980);

                    x = (byte)Global.Random.Next(171, 174);
                    y = (byte)Global.Random.Next(100, 103);

                    Warp(pc, 10025001, x, y);
                    break;

                case 2:
                    Say(pc, 11000938, 131, "The Flying Garden is made by the craftsmen of [Tongka]! $R;" +
                                           "$R is a little difficult to collect parts...$R;" +
                                           "$P is right! $R;" +
                                           "There is a craftsman in a village in $R, look for it, $R;" +
                                           "Others are pretty good, $R;" +
                                           "Some parts will be distributed for free every week! $R;" +
                                           "$P heard somewhere in the south...$R;" +
                                           "$P flying in the sky! $R;" +
                                           "$R is in the ECO world, $R;" +
                                           "Very important means of transportation! $R;" +
                                           "$P to [Morg City] $R;" +
                                           "You must take the Flying Garden! $R;" +
                                           "The $R channel is managed by the army and guilds. $R;" +
                                           "$P Airship Garden owned by ordinary people $R;" +
                                           "It can only be used as a house and a garden! $R;" +
                                           "The $R house is built, you can decorate it as you like! $R;" +
                                           "You can let your friends in, $R;" +
                                           "Or designate some people to enter, $R;" +
                                           "You can also choose not to open. $R;" +
                                           "$P everyone follows their own hobbies. $R;" +
                                           "Can present a unique style! $R;" +
                                           "$R and, $R;" +
                                           "Feikongting can only be returned at the designated location! $R;" +
                                           "$R can only be used in the [airport] dedicated to the Airship Garden, $R;" +
                                           "It can be returned! $R;" +
                                           "$P is actually not allowed to be returned here. $R;" +
                                           "$R made an exception just to show you! $R;" +
                                           "$P go into the house and take a look? $R;" +
                                           "$R if you think about the next stage, $R;" +
                                           "Just tell me! $R;", "Masha");
                    break;

                case 3:
                    break;
            }
        }
    }
}
