using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Job2X_03
    {
        //刺客
        Assassin_transfer_begins = 0x1,//_4A00
        //軍艦島
        The_first_question_is_answered_correctly = 0x2,//_4A01
        The_first_question_was_answered_incorrectly = 0x4,//_4A04
        Did_not_get_the_Assassin_Water_1 = 0x8,//_4A69
        //奧克魯尼亞東海岸
        The_second_question_is_answered_correctly = 0x10,//_4A02
        The_second_question_was_answered_incorrectly = 0x20,//_4A05
        Did_not_get_the_Assassin_Water_2 = 0x40,//_4A70
        //北方海角
        The_third_question_is_answered_correctly = 0x80,//_4A03
        The_third_question_was_answered_incorrectly = 0x100,//_4A06
        Did_not_get_the_Assassin_Water_3 = 0x200,//_4A71
        //鐵火山
        The_fourth_question_is_answered_correctly = 0x400,///_4A08
        The_fourth_question_was_answered_incorrectly = 0x800,//_4A07
        Did_not_get_the_Assassin_Water_4 = 0x1000,//_4A72
        //中央
        Return_the_internal_drug_to_the_assassin = 0x2000,//_4A25
        Defense_is_too_high = 0x4000,
        End_of_transfer = 0x8000,
    }
}
