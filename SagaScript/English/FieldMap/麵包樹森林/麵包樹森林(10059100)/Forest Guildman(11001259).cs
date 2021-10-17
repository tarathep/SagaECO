using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059100
{
    public class S11001259 : Event
    {
        public S11001259()
        {
            this.EventID = 11001259;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "อา! $R เหนื่อยใช่มั้ย?");
            switch (Select(pc, "เข้าไปพักผ่อน", "", "อืม! แน่นอน", "ไม่จำเป็น"))
            {
                case 1:
                    if (pc.Gold >= 360)
                    {
                        pc.Gold -= 360;
                        Say(pc, 0, 131, "360G ได้รับเงินแล้ว", " ");
                        Fade(pc, FadeType.Out, FadeEffect.Black);
                        Wait(pc, 3000);
                        Warp(pc, 10059101, 233, 35);
                        // Fade(pc, FadeType.Out, FadeEffect.Black);
                    }
                    else
                        Say(pc, 131, "เงินไม่พอ.");
                    break;

            }
        }
    }
}