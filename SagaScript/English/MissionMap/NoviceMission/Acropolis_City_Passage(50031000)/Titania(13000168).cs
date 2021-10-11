using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:微微(13000168) X:11 Y:19
namespace SagaScript.M50031000
{
    public class S13000168 : Event
    {
        public S13000168()
        {
            this.EventID = 13000168;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Transformed_into_a_slightly_Selmander))
            {
                Beginner_01_mask.SetValue(Beginner_01.Transformed_into_a_slightly_Selmander, true);

                Say(pc, 0, 0, "Ah... wait a minute!!$R;" +
                               "There is a flame in front of $R... It's dangerous to go on like this... $R;" +
                               "$P This is the [Skill Stone], $R;" +
                               "Use its power, $R;" +
                               "You can protect your body with flames. $R;", "Tita");

                Say(pc, 0, 0, "Slightly throw the strange-shaped stone into the air, $R;" +
                              "Then chant the spell. $R;" +
                              "$R……??$R;" +
                              "Flashing light......?!$R;", "");

                ActivateMarionette(pc, 20040301);
                Heal(pc);
                ShowEffect(pc, 8015);
                Say(pc, 0, 0, "Transformed into a [Selmander]! $R;", "");

                Say(pc, 0, 0, "The body of the active puppet Selmander, $R;" +
                              "Can withstand heat and flames. $R;" +
                              "$R fast! Go through the aisle quickly! $R;", "Tita");
            }
        }
    }
}
