using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum World_01
    {
        //世界記錄

        //trash_can
        Using_the_trash_can_for_the_first_time = 0x1,

        //道具精製師
        The_hair_conditioner_synthesis_task_begins = 0x2,
        Complete_the_hair_conditioner_synthesis_task = 0x4,
        First_story_of_the_progress_of_the_refiner_of_tools = 0x8,

        //Appraiser
        First_dialogue_with_the_appraiser = 0x10,

        //六姬
        The_first_story_of_the_progress_of_Yorokujyo = 0x20,

        //弗朗西斯小姐
        Wing_decoration_synthesis_task_begins = 0x40,
        Wing_decoration_synthesis_task_completed = 0x80,
    }
}
