using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:飛空庭的房間(30202000) NPC基本信息:飛天鼠(11000939) X:7 Y:6
namespace SagaScript.M30202000
{
    public class S11000939 : Event
    {
        public S11000939()
        {
            this.EventID = 11000939;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Found_something_under_the_bed))
            {
                Found_something_under_the_bed(pc);
                return;
            }

            Say(pc, 11000939, 0, "…$R;", "Sky Squirrel");
        }

        void Found_something_under_the_bed(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.Found_something_under_the_bed, true);

            Say(pc, 11000939, 0, "…$R;" +
                                 "$R (under the bed) $R;", "Sky Squirrel");

            Say(pc, 0, 0, "Hey?$R;" +
                          "Is there anything under the bed? $R;", "");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 10001250, 1);
            Say(pc, 0, 0, "Get the [synthesis failed item]! $R;", "");
        } 
    }
}
