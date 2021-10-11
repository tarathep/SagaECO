using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:微微(13000169) X:11 Y:19
namespace SagaScript.M50031000
{
    public class S13000169 : Event
    {
        public S13000169()
        {
            this.EventID = 13000169;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Encounter_an_enemy))
            {
                Beginner_01_mask.SetValue(Beginner_01.Encounter_an_enemy, true);

                Say(pc, 0, 0, "Is it the enemy?!$R;" +
                               "How can I be hacked here! $R;" +
                               "$R calm down...$R;" +
                               "You are now a puppet, $R;" +
                               "You can beat those guys! $R;", "Tita");
            }
        }
    }
}
