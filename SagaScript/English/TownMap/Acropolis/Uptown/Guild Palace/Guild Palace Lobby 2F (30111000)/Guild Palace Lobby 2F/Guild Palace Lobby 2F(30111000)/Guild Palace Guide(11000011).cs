using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:行會宮殿2樓(30111000) NPC基本信息:行會宮殿嚮導(11000011) X:10 Y:16
namespace SagaScript.M30111000
{
    public class S11000011 : Event
    {
        public S11000011()
        {
            this.EventID = 11000011;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000011, 131, "Hello, this is the second floor of the Guild Palace! $R;" +
                                     "The north of $P is the archer's room. $R;" +
                                     "To the east is the room of the Swordsman Master. $R;" +
                                     "To the south is the room of the Scout Master. $R;" +
                                     "There is the room of the knight master in the west. $R;", "Guide to the Guild Palace");
        }
    }
}
