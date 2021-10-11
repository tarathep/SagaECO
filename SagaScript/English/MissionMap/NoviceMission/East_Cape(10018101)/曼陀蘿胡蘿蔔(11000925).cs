using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:曼陀蘿胡蘿蔔(11000925) X:204 Y:90
namespace SagaScript.M10018101
{
    public class S11000925 : Event
    {
        public S11000925()
        {
            this.EventID = 11000925;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11000925, 131, "…♪$R;", "曼陀蘿胡蘿蔔");

            Say(pc, 11000923, 131, "這隻寵物叫「曼陀蘿胡蘿蔔」。$R;" +
                                   "$R養起來十分費勁，$R;" +
                                   "但對跟植物有關的職業來說，$R;" +
                                   "是很有幫助唷!$R;", "寵物養殖研究員");         
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11000925, 131, "…♪$R;", "曼陀蘿胡蘿蔔");
        } 
    }
}
