using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:裝備技術人員(11001411) X:195 Y:69
namespace SagaScript.M10018102
{
    public class S11001411 : Event
    {
        public S11001411()
        {
            this.EventID = 11001411;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_already_had_the_first_conversation_with_Emil))
            {
                Haven_t_spoken_to_Emil_yet(pc);
                return;
            }

            Say(pc, 11001411, 131, "Good...$R;" +
                                    "$R? Hello, are you a novice? $R;" +
                                    "$P I belong to the Machinists' Guild. $R;" +
                                    "$R is here to confirm the state of the machine. $R;" +
                                    "$P Oh? Do you have anything to call me? $R;", "Equipment technician");

            selection = Select(pc, "What do you want to ask?", "", "How to use ItemBox", "How to use ItemTicketExchange", "Nothing I want to ask");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11001411, 131, "ItemBox$R;" +
                                               "$R as long as there is [Emil badge] or $R;" +
                                               "[Bronze badge] can be used! $R;" +
                                               "$P after clicking on the machine, $R;" +
                                               "Select the badge you want to use. $R;" +
                                               "The badge that $P wants to use must be decided before tax exchange. $R;" +
                                               "After the $P tax is exchanged, there will be items! $R;" +
                                               "As for what will happen to $R, I don't know before the tax change! $R;", "Equipment technician");
                        break;

                    case 2:
                        Say(pc, 11001411, 131, "As for ItemTicketExchange, $R;" +
                                               "First click on this machine, $R;" +
                                               "Then select [Item Ticket Exchange]. $R;" +
                                               "$P need to enter the number, $R;" +
                                               "Enter the correct [Item Number] in the input window. $R;" +
                                               "$R please remember to confirm, otherwise you won't be able to get the props. $R;" +
                                               "$P can be entered slowly, $R;" +
                                               "But it must be correct! $R;" +
                                               "After entering $P, click [Confirm], $R;" +
                                               "You can get the props. $R;" +
                                               "Props obtained by $R $R;" +
                                               "You can confirm in the [Item Window]!! $R;", "Equipment Technician");
                        break;
                }

                selection = Select(pc, "What do you want to ask?", "", "How to use ItemBox", "How to use ItemTicketExchange", "Nothing to ask");
            }
        }

        void Haven_t_spoken_to_Emil_yet(ActorPC pc)
        {
            Say(pc, 11001411, 131, "Have you said hello to Emil? $R;" +
                                   "Better talk to him first! $R;", "Equipment technician");
        } 
    }
}
