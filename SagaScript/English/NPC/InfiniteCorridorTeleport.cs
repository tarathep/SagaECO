using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class InfiniteCorridorTeleport : Event
    {
        uint ItemID;
        uint mapID;
        byte x1, x2, y1, y2;

        public InfiniteCorridorTeleport()
        {

        }

        protected void Init(uint eventID, uint ItemID, uint mapID, byte x1, byte y1, byte x2, byte y2)
        {
            this.EventID = eventID;
            this.ItemID = ItemID;
            this.mapID = mapID;
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;

        }

        public override void OnEvent(ActorPC pc)
        {
            int a = pc.PossesionedActors.Count + 1;
            int b = CountItem(pc, ItemID);
            if (a <= b)
            {
                int x = Global.Random.Next(x1, x2);
                int y = Global.Random.Next(y1, y2);
                TakeItem(pc, ItemID, (ushort)b);
                Warp(pc, mapID, (byte)x, (byte)y);
                return;
            }
            Say(pc, 131, "The teleporter did not activate $R;" +
                   "$P may be insufficient items $R;");
        }
    }
}
