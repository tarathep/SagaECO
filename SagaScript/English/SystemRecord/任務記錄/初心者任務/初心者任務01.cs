using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Beginner_01
    {
        //初心者任務

        //離開阿高普路斯市
        The_commander_ordered_the_firing_of_the_main_gun = 0x1,
        Meet_Tita_again = 0x2,
        Transformed_into_a_slightly_Selmander = 0x4,
        Encounter_an_enemy = 0x8,
        Leaving_Acropolis = 0x10,

        //說明ECO的基本操作
        Have_already_had_the_first_conversation_with_Emil = 0x20,
        The_Emil_gives_the_Emil_badge = 0x40,
        Emil_gives_Emil_introduction_book = 0x80,

        //NPC_item_buying_and_selling_teaching
        NPC_item_buying_and_selling_teaching開始 = 0x100,
        Sales_of_chocolate_completed = 0x200,
        Apple_purchase_completed = 0x400,
        NPC_item_buying_and_selling_teaching_completed = 0x800,

        //說明ECO的戰鬥操作
        Have_had_the_first_conversation_with_Berial = 0x1000,
        貝利爾開始進行相關說明 = 0x2000,

        Have_already_conducted_combat_teaching_with_Belial = 0x4000,
        Combat_teaching_begins = 0x8000,
        HP_recovery_teaching_completed = 0x10000,
        Combat_teaching_completed = 0x20000,

        Already_had_skills_teaching_with_Belial = 0x40000,
        Skill_teaching_begins = 0x80000,
        Get_the_second_skill_stone = 0x100000,
        Get_the_third_skill_gem = 0x200000,
        Skill_teaching_completed = 0x400000,

        Belial_gives_the_novice_ribbon = 0x800000,

        //瑪莎說明ECO的飛空庭系統
        Have_already_had_the_first_conversation_with_Masha = 0x1000000,
        Found_something_under_the_bed = 0x2000000,

         //說明ECO的基本操作2
        埃米爾的蘋果 = 0x4000000,
        埃米爾的重物 = 0x8000000,
        埃米爾的木偶 = 0x10000000,
        埃米爾的桃子 = 0x20000000,
        埃米爾的刀 = 0x40000000,
    }
}
