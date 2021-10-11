using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:微微(13000167) X:11 Y:19
namespace SagaScript.M50031000
{
    public class S13000167 : Event
    {
        public S13000167()
        {
            this.EventID = 13000167;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Meet_Tita_again))
            {
                Beginner_01_mask.SetValue(Beginner_01.Meet_Tita_again, true);

                Say(pc, 0, 0, "Hey! So you are here! $R;" +
                               "$R I'm sorry...$R;" +
                               "They ran away from me again. $R;" +
                               "$P you seem to be from [those guys] $R;" +
                               "Escape to [this era] in the dimensional seam I made. $R;" +
                               "The details of $P, let's talk about it later! $R;" +
                               "Get out! $R;" +
                               "The exit is at the end of the aisle! $R;" +
                               "$R [This era] of Acropolis...$R;" +
                               "It's almost destroyed...!$R;", "Tita");
            }
        }
    }
}
