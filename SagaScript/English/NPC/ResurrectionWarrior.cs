using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class ResurrectionWarrior : Event
    {
        uint mapID;
        byte x, y;

        public ResurrectionWarrior()
        {
        
        }

        protected void Init(uint eventID, uint mapID, byte x, byte y)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x = x;
            this.y = y;
        }

        public override void OnEvent(ActorPC pc)
        {
            try
            {
                string oldSave, newSave;

                oldSave = GetMapName(pc.SaveMap);
                newSave = GetMapName(mapID);

                if (oldSave == "")
                {
                    SetHomePoint(pc, mapID, x, y);

                    Say(pc, 131, "The storage point is set to $R;" +
                                 "『" + newSave + "』! $R;", "Resurrection Warrior");
                    return;
                }
                Say(pc, 131, "The current storage point is $R;" +
                             "『" + oldSave + "』!$R;", "Resurrection Warrior");

                switch (Select(pc, "Are you sure you want to store it here?", "", "Do not change", "I want to store it here"))
                {
                    case 1:
                        break;

                    case 2:
                        SetHomePoint(pc, mapID, x, y);

                        Say(pc, 131, "The storage point is set to $R;" +
                                     "『" + newSave + "』! $R;", "Resurrection Warrior");
                        break;
                }
            }
            catch
            {
                SetHomePoint(pc, mapID, x, y);

                Say(pc, 131, "The storage point is set to $R;" +
                             "『" + GetMapName(mapID) + "』!$R;", "Resurrection Warrior");
            }
        }
    }
}
