using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class InfiniteCorridorExit : Event
    {
        public InfiniteCorridorExit()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "Return to the entrance?", "", "Don't go back", "Go back"))
            {
                case 1:
                    break;
                case 2:
                    Warp(pc, 10042000, 80, 134);
                    break;
            }
        }
    }
}