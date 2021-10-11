using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:喜歡小狗的女孩(11001407) X:201 Y:90
namespace SagaScript.M10018102
{
    public class S11001407 : Event
    {
        public S11001407()
        {
            this.EventID = 11001407;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            //尚未插入♪表情
            Say(pc, 11001407, 0, "Too cute~♪$R;" +
                                  "$R Do you have anything to do with me? $R;", "Puppy Loving Girl");

            switch (Select(pc, "What are you looking at?", "", "That [♪] is...?", "Nothing"))
            {
                case 1:
                    Say(pc, 11001407, 0, "This is the [emoji]! $R;" +
                                         "$R open the [emoji] window, $R;" +
                                         "You can simply use it. $R;" +
                                         "The larger chat window next to $P $R;" +
                                         "Did you see it? $R;" +
                                         "$R wants to open the [emoji] window, $R;" +
                                         "Just click on $R; on the right side of this window" +
                                         "Smile icon. $R;" +
                                         "$P in the [Expression] and [Action] windows, $R;" +
                                         "Double-click the icon you want to use. $R;" +
                                         "$P is very simple~♪$R;" +
                                         "$R when talking to a friend, $R;" +
                                         "Use various emoticons $R;" +
                                         "It will be more interesting! $R;", "Puppy Loving Girl");
                    break;

                case 2:
                    break;
            }
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001407, 0, "Too cute!!$R;" +
                                 "$R(...No one seems to be the surrounding situation.)$R;", "Puppy Loving Girl");
        }  
    }
}
