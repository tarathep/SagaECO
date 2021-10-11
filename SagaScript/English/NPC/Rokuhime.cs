using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

using SagaDB.Quests;

namespace SagaScript
{
    public abstract class 六姬 : Event
    {
        public 六姬()
        {
            this.questFailed = "How did it take so long! $R;" +
                "I'm tired of waiting $R;" +
                "The task of $R failed! $R;";
            this.notEnoughQuestPoint = "Can't accept the task right now";
            this.leastQuestPoint = 1;
            this.gotNormalQuest = "The item is too heavy to be removed at one time $R;" +
                "Just move in batches $R$P;" +
                "The type of task that can be undertaken here is $R;" +
                "The crusade mission based on the team 唷$R;" +
                "$R as long as you and your teammate have accepted the same task $R;" +
                "The number of hunting monsters will be calculated together, not bad $R;";
            this.questCompleted = "Hehe...$R;" +
                "I am very happy to get your help $R;" +
                "$R, please accept the payment $R;";
            this.alreadyHasQuest = "What? Give up halfway?";
            this.questCanceled = "……";
        }

        public override void  OnQuestUpdate(ActorPC pc, Quest quest)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            World_01_mask.SetValue(World_01.The_first_story_of_the_progress_of_Yorokujyo, false);
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.The_first_story_of_the_progress_of_Yorokujyo))
            {
                World_01_mask.SetValue(World_01.The_first_story_of_the_progress_of_Yorokujyo, true);

                Say(pc, 131, "I like the number 6 very much! $R;" +
                              "$R6 names, 6 gold coins...$R;" +
                              "And...$R;" +
                              "$P as long as the task is completed perfectly, $R;" +
                              "I will reward you! $R;" +
                              "$R but now I want you to hunt 666 monsters, $R;" +
                              "It might not be easy for you? $R;" +
                              "$P But I am not an unreasonable person? $R;" +
                              "If you cooperate with a teammate, $R;" +
                              "I will also admit that you have completed the task. $R;" +
                              "$R as long as you and your teammate take the same task, $R;" +
                              "The number of hunted monsters will also be calculated together, $R;" +
                              "How is it, not bad? $R;" +
                              "$P Do you want to challenge the mission? $R;", "Rokuhime");
            }
            else
            {
                HandleQuest(pc, 19);
            }
        }
    }
}
