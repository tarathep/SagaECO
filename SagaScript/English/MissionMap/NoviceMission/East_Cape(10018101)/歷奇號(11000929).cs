using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:歷奇號(11000929) X:222 Y:61
namespace SagaScript.M10018101
{
    public class S11000929 : Event
    {
        public S11000929()
        {
            this.EventID = 11000929;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11000929, 500, "唔…$R;", "歷奇號");

            Say(pc, 11001408, 131, "啊，對不起，可能是看到陌生的臉孔$R;" +
                                   "所以有點害怕$R;" +
                                   "$R這小子叫歷奇號，是我的搭檔唷$R;" +
                                   "$P對商人來說這個小子最棒了，$R;" +
                                   "看到帶著這個傢伙的人，$R;" +
                                   "就跟他說話吧$R;" +
                                   "$R可能會給您提示的呀$R;", "利基先生");
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11000929, 0, "唔…$R;", "歷奇號");
        }  
    }
}
