using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class ItemBox : Event
    {
        byte badge01, badge02;
        string prize01, prize02;

        public ItemBox()
        {

        }

        protected void Init(uint eventID, byte badge01, string prize01, byte badge02, string prize02)
        {
            this.EventID = eventID;
            this.badge01 = badge01;
            this.prize01 = prize01;
            this.badge02 = badge02;
            this.prize02 = prize02;
        }

        public override void OnEvent(ActorPC pc)
        {
            string badge_01, badge_02;
            string[] badge = new string[] { "Emil badge", "Bronze badge", "Silver badge", "Gold badge" };

            badge_01 = badge[badge01];
            badge_02 = badge[badge02];

            PlaySound(pc, 2559, false, 100, 50);

            Say(pc, 0, 65535, "What powerful props will there be? $R;" +
                              "It's really an expected item box! $R;", "");

            switch (Select(pc, "What badge do you want to put?", "", badge_01, badge_02, "not interested"))
            {
                case 1:
                    //The item box function has not been implemented yet
                    Say(pc, 0, 65535, "$R has not been implemented yet;", "");

                    Say(pc, 0, 65535, "No bronze badge! $R;", "");
                    break;

                case 2:
                    //The item box function has not been implemented yet
                    Say(pc, 0, 65535, "$R has not been implemented yet;", "");

                    Say(pc, 0, 65535, "No Emil badge! $R;", "");
                    break;

                case 3:
                    break;
            }
        }
    }
}
