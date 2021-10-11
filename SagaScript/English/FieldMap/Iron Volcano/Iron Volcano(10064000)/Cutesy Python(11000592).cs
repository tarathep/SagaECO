using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S11000592 : Event
    {
        public S11000592()
        {
            this.EventID = 11000592;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];
            
            if (mask.Test(Job2X_03.The_fourth_question_is_answered_correctly))//_4A08)
            {
                if (mask.Test(Job2X_03.The_fourth_question_is_answered_correctly) && CountItem(pc, 10000352) >= 1)
                {
                    Say(pc, 131, "Let’s take a look, $R;" +
                         "Is the drug already collected? $R;" +
                         "$R go back as soon as possible? $R;" +
                         "$P be careful$R;");
                    return;
                }
                if (CheckInventory(pc, 10000352, 1))
                {
                    GiveItem(pc, 10000352, 1);
                    Say(pc, 131, "Get a $R for [Assassin Water 4];");
                     Say(pc, 131, "This is also one of the practice! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_4, false);
                    //_4A72 = false;
                    //_4A08 = true;
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_4, true);
                //_4A72 = true;
                Say(pc, 131, "...too much luggage, come back after finishing $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_fourth_question_was_answered_incorrectly))//_4A07)
            {
                Say(pc, 131, "…$R;" +
                     "$P...$R;" +
                     "$R go back to listen again. $R;");
                return;
            }
            if (mask.Test(Job2X_03.Did_not_get_the_Assassin_Water_4))//_4A72)
            {
                if (CheckInventory(pc, 10000352, 1))
                {
                    GiveItem(pc, 10000352, 1);
                    Say(pc, 131, "Get a $R for [Assassin Water 4];");
                     Say(pc, 131, "This is also one of the practice! $R;");
                    mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_4, false);
                    //_4A72 = false;
                    //_4A08 = true;
                    return;
                }
                mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_4, true);
                //_4A72 = true;
                Say(pc, 131, "...too much luggage, come back after finishing $R;");
                return;
            }
            if (mask.Test(Job2X_03.The_first_question_is_answered_correctly) &&
                mask.Test(Job2X_03.The_second_question_is_answered_correctly) &&
                mask.Test(Job2X_03.The_third_question_is_answered_correctly))//_4A03 && _4A02 && _4A01)
            {
                Say(pc, 131, "…$R;" +
                    "$P...$R;" +
                    "$R is so hot...$R;" +
                    "$P...$R;" +
                    "$P, it's so hot $R;" +
                    "Is there no way? $R;");
                switch (Select(pc, "What to do?", "", "Norton is cooler", "Just take off your clothes", "Mind calm and cool naturally"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, true);
                        //_4A07 = true;
                        Say(pc, 131, "…$R;" +
                            "$P...$R;" +
                            "$R is not cool there, $R;" +
                            "It's cold $R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.The_fourth_question_was_answered_incorrectly, true);
                        //_4A07 = true;
                        Say(pc, 131, "…$R;" +
                            "$P...$R;" +
                            "$R, are you crazy? $R;");
                        break;
                    case 3:
                        Say(pc, 131, "…$R;" +
                            "$P...$R;" +
                            "$R Calm and cool naturally? $R;" +
                            "What are you talking about? $R;" +
                            "$P take this! $R;");
                        if (CheckInventory(pc, 10000352, 1))
                        {
                            GiveItem(pc, 10000352, 1);
                            Say(pc, 131, "Get a $R for [Assassin Water 4];");
                            Say(pc, 131, "This is also one of the practice! $R;");
                            mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_4, false);
                            mask.SetValue(Job2X_03.The_fourth_question_is_answered_correctly, true);
                            //_4A72 = false;
                            //_4A08 = true;
                            return;
                        }
                        mask.SetValue(Job2X_03.Did_not_get_the_Assassin_Water_4, true);
                        //_4A72 = true;
                        Say(pc, 131, "…too much luggage, come back after finishing $R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.Assassin_transfer_begins))//_4A00)
            {
                Say(pc, 131, "…$R;" +
                    "$P...$R;" +
                    "$R is so annoying...$R;");
                return;
            }

            Say(pc, 131, "…$R;" +
                "$P...$R;" +
                "$R is so hot...$R;");

        }
    }
}