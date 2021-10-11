using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市司令室(50030000) NPC基本信息:操作員(13000164) X:2 Y:13
namespace SagaScript.M50030000
{
    public class S13000164 : Event
    {
        public S13000164()
        {
            this.EventID = 13000164;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.The_commander_ordered_the_firing_of_the_main_gun))
            {
                The_commander_ordered_the_firing_of_the_main_gun(pc);
                return;
            }

            Say(pc, 13000164, 135, "The exit is in front of the room! $R;" +
                                    "Quickly escape from here! Hurry! $R;", "Operator");
        }

        void The_commander_ordered_the_firing_of_the_main_gun(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.The_commander_ordered_the_firing_of_the_main_gun, true);

            Say(pc, 13000162, 131, "The anti-aircraft gun on the left! $R;" +
                                    "What are you doing! Give me a good job! $R;", "Commander");

            Say(pc, 13000163, 135, "The enemy's mech fortress, we are approaching urgently! $R;" +
                                   "$R attack output power is reduced by 30%! $R;" +
                                   "Can't resist anymore! $R;", "Operator");

            Say(pc, 13000164, 135, "The enemy has landed in the upper city area on the right! $R;" +
                                   "It seems that the line of defense has been breached! $R;", "Operator");

            Say(pc, 13000162, 131, "Reserve forces gather!!$R:" +
                                   "No matter what...$R;" +
                                   "Never let the enemy invade the central area. $R;" +
                                   "$P...Okay, remove the maneuver mode! $R;" +
                                   "$R gathers the combat power in the city, $R;" +
                                   "Launch the most powerful main gun! $R;" +
                                   "The target is the enemy's mech fortress [Mai Mai]. $R;", "Commander");

            Say(pc, 13000163, 135, "Commander...Commander...if...$R;" +
                                   "$R... If you lose motivation, $R;" +
                                   "What about Acropolis...$R;", "Operator");

            Say(pc, 13000162, 131, "Anyway, this can’t last long, $R;" +
                                   "$R will fight them to the death this time! $R;" +
                                   "Destroy those guys in one go! $R;", "Commander");

            Say(pc, 13000164, 135, "Know... Got it! $R;" +
                                   "Enter the main gun group! $R;", "Operator");

            Say(pc, 13000162, 131, "…!! What…!!$R;" +
                                   "$R, what do civilians want to do here? $R;" +
                                   "$P has ordered martial law! $R;" +
                                   "Let them avoid immediately! $R;", "Commander");
        } 
    }
}
