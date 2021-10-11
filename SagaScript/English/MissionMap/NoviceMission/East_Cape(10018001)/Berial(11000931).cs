using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018001) NPC基本信息:貝利爾(11000931) X:136 Y:68
namespace SagaScript.M10018001
{
    public class S11000931 : Event
    {
        public S11000931()
        {
            this.EventID = 11000931;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            int selection;

            if (!Beginner_01_mask.Test(Beginner_01.Have_had_the_first_conversation_with_Berial))
            {
                Conversation_with_Belial_for_the_first_time(pc);
            }
            else
            {
                Say(pc, 11000931, 131, "Oh? $R;" +
                                         "Does $R have anything to ask? $R;", "Berial");
            }

            if (Beginner_01_mask.Test(Beginner_01.Combat_teaching_begins) &&
                !Beginner_01_mask.Test(Beginner_01.Combat_teaching_completed))
            {
                戰鬥教學(pc);
                return;
            }

            if (Beginner_01_mask.Test(Beginner_01.Skill_teaching_begins) &&
                !Beginner_01_mask.Test(Beginner_01.Skill_teaching_completed))
            {
                技能教學(pc);
                return;
            }

            selection = Select(pc, "Which explanation do you want to hear?", "", "How to use [Items]", "How to fight", "How to use [Skills]", "About [reward points]", "Nothing I want to ask");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000931, 131, "Before explaining how to use items, $R;" +
                                               "Let’s take a look at the basic operations first! $R;", "Berial");

                        Say(pc, 11000931, 131, "[Items], [Skills]; [Equipment] $R;" +
                                               "Is the window open? $R;" +
                                               "$R all three can be switched on the main window! $R;" +
                                               "When $P talks to me, $R;" +
                                               "You can also operate on one side, $R;" +
                                               "Practice first! $R;" +
                                               "$P...$R;" +
                                               "$P doesn't work? $R;" +
                                               "$P now introduces $R;" +
                                               "[Items] and [Equipment] windows! $R;" +
                                               "$P I want to explain is $R;" +
                                               "About [equipment] and [consumable items]. $R;" +
                                               "$P [Equipment] is $R;" +
                                               "Weapons, armor, decorations $R;" +
                                               "Wait. With $R;" +
                                               "$R when you want to wear these, $R;" +
                                               "Just double-click the item. $R;" +
                                               "After most of the equipment of $P is worn, $R;" +
                                               "Appearance will change! $R;" +
                                               "$R collect good-looking equipment, $R;" +
                                               "It's also a happy thing! $R;" +
                                               "$P to confirm whether the device is correct, $R;" +
                                               "Open the [Equipment] window. $R;" +
                                               "$R self-wearing equipment $R;" +
                                               "You can tell at a glance. $R;" +
                                               "$P When detailed description of props is needed, $R;" +
                                               "Right click on the prop icon. $R;" +
                                               "$R can confirm, $R;" +
                                               "Attack/Defense/Effects when equipped with items, etc.. $R;" +
                                               "$P is next to [Consumable Items]. $R;" +
                                               "$P consumes items is $R;" +
                                               "Props used to get a certain effect. $R;" +
                                               "$R simply means restoring items! $R;" +
                                               "$P Basically, you can use it by double clicking. $R;" +
                                               "$R if it can't be used, $R;" +
                                               "It may be [material props] or [valuable items]. $R;" +
                                               "$P has news about material props, $R;" +
                                               "The [Item Refiner] is very clear! $R;" +
                                               "$R he has been in front of [Acropolis], $R;" +
                                               "Find him if you have time! $R;", "Berial");
                        break;

                    case 2:
                        if (!Beginner_01_mask.Test(Beginner_01.Have_already_conducted_combat_teaching_with_Belial))
                        {
                            Beginner_01_mask.SetValue(Beginner_01.Have_already_conducted_combat_teaching_with_Belial, true);
                            Beginner_01_mask.SetValue(Beginner_01.Combat_teaching_begins, true);

                            Say(pc, 11000931, 131, "Through actual combat, the easiest to understand, $R;" +
                                                   "But let me tell you some precautions first! $R;" +
                                                   "Main window [HP] [MP] [SP] $R;" +
                                                   "These three values. $R;" +
                                                   "$P must pay attention when fighting. $R;" +
                                                   "$P, especially when the HP reaches 0, will die. $R;" +
                                                   "$P uses [skills], $R;" +
                                                   "[MP] value and [SP] value will decrease! $R;" +
                                                   "It's okay even if the value of $R drops to 0, $R;" +
                                                   "But it can't fight effectively. $R;" +
                                                   "$P click on the enemy when fighting, $R;" +
                                                   "$R then bring the sword, $R;" +
                                                   "Fight [Mini Pururu]! $R;" +
                                                   "$P, you should have a sword? $R;" +
                                                   "After you are equipped, come out and practice! $R;" +
                                                   "$R can be installed by double-clicking the sword in the item window. $R;" +
                                                   "$P doesn’t take much effort to deal with these guys, $R;" +
                                                   "They are weak, not very strong! $R;", "Berial");
                            return;
                        }
                        else
                        {
                            Say(pc, 11000931, 131, "Do you still want to hear about fighting? Now let’s make a brief explanation. $R;" +
                                                   "When fighting $P, pay attention to the following points! $R;" +
                                                   "Main window [HP] [MP] [SP] $R;" +
                                                   "These three values. $R;" +
                                                   "$P must pay attention when fighting. $R;" +
                                                   "$P, especially when the HP reaches 0, will die. $R;" +
                                                   "$P uses [skills], $R;" +
                                                   "[MP] value and [SP] value will decrease! $R;" +
                                                   "It's okay even if the value of $R drops to 0, $R;" +
                                                   "But it can't fight effectively. $R;" +
                                                   "$P tell you first, $R;" +
                                                   "How to recover HP lost in battle! $R;" +
                                                   "$R wants to restore HP, you can use props, $R;" +
                                                   "You can also recover by [sit down.] $R;" +
                                                   "$P Just click the [Insert] button to sit down. $R;" +
                                                   "$P If you click again, $R;" +
                                                   "Or do other actions, it will be automatically released. $R;" +
                                                   "$P and, in the city or in a tent, $R;" +
                                                   "It will be restored automatically! $R;" +
                                                   "$R is both cost-free and effective. $! R;", "Berial");
                        }
                        break;

                    case 3:
                        if (!Beginner_01_mask.Test(Beginner_01.Already_had_skills_teaching_with_Belial))
                        {
                            Beginner_01_mask.SetValue(Beginner_01.Already_had_skills_teaching_with_Belial, true);

                            Say(pc, 11000931, 131, "Then explain the [skills] for you now! $R;" +
                                                    "$P [Novice] can only attack normally. $R;" +
                                                    "After $R is transferred, you can use various skills! $R;" +
                                                    "How about $P, do you want to try it? $R;", "Berial");

                            switch (Select(pc, "Want to try the skill?", "", "Want to try", "It is enough to listen to the instructions"))
                            {
                                case 1:
                                    Beginner_01_mask.SetValue(Beginner_01.Skill_teaching_begins, true);

                                    PlaySound(pc, 2040, false, 100, 50);
                                    GiveItem(pc, 20050006, 1);
                                    Say(pc, 0, 0, "Get the skill stone [Power Wall] $R;", "");

                                    Say(pc, 11000931, 131, "The skill stone I just gave you, $R;" +
                                                           "It is an item that can only be used once! $R;" +
                                                           "$P can use this stone to be $R;" +
                                                           "Experience the [strength boost] skills! $R;" +
                                                           "$R can raise $R immediately;" +
                                                           "Original potential skills, $R;" +
                                                           "Mainly used for yourself or team members. $R;" +
                                                           "$P use this skill, $R;" +
                                                           "Try fighting with monsters! $R;", "Berial");
                                    return;

                                case 2:
                                    Say(pc, 11000931, 131, "The skill refers to, $R;" +
                                                           "Consumption of SP value and MP value $R;" +
                                                           "The [technology] and [magic] to use! $R;", "Berial");

                                    Say(pc, 11000931, 131, "You can now open the [Skill Window], $R;" +
                                                           "Familiar with or use skills. $R;" +
                                                           "$P wants to know the effect, $R;" +
                                                           "You can click the icon in the [Skill Window] to confirm. $R;" +
                                                           "Also, there are some skills, $R;" +
                                                           "If you don't wear specific equipment, you can't use it! $R;" +
                                                           "$R before learning skills, please confirm! $R;" +
                                                           "$P when using skills, $R;" +
                                                           "Be sure to pay attention to the MP value and SP value! $R;" +
                                                           "The recovery method of $P is the same as the HP value, $R;" +
                                                           "You can sit back and recover, $R;" +
                                                           "You can also use props, $R;" +
                                                           "Or automatically recover in the city/tent! $R;", "Berial");
                                    break;
                            }
                        }
                        else 
                        {
                            Say(pc, 11000931, 131, "Do you want to hear it again? $R;" +
                                                   "$R can't test items for the second time. $R;" +
                                                   "Then make a brief explanation! $R;" +
                                                   "Skill refers to the consumption of SP value and MP value, $R;" +
                                                   "The [technology] and [magic] to use! $R;", "Berial");

                            Say(pc, 11000931, 131, "You can now open the [Skill Window], $R;" +
                                                   "Familiar with or use skills. $R;" +
                                                   "$P wants to know the effect, $R;" +
                                                   "You can click the icon in the [Skill Window] to confirm. $R;" +
                                                   "Also, there are some skills, $R;" +
                                                   "If you don't wear specific equipment, you can't use it! $R;" +
                                                   "$R before learning skills, please confirm! $R;" +
                                                   "$P when using skills, $R;" +
                                                   "Be sure to pay attention to the MP value and SP value! $R;" +
                                                   "The recovery method of $P is the same as the HP value, $R;" +
                                                   "You can sit back and recover, $R;" +
                                                   "You can also use props, $R;" +
                                                   "Or automatically recover in the city/tent! $R;", "Berial");
                        }
                        break;

                    case 4:
                        Say(pc, 11000931, 131, "[Reward Points] is to increase oneself $R;" +
                                               "Very important points for various abilities! $R;" +
                                               "$P first temporarily open the status window! $R;" +
                                               "Is $P open? $R;" +
                                               "There is a small " + " button at the bottom right of the $P window, right? $R;" +
                                               "$R click it! $R;" +
                                               "$P can allocate [reward points] here. $R;" +
                                               "$R click the flashing [△] to increase the value, $R;" +
                                               "The rising value will turn blue. $R;" +
                                               "$P Don't click [Confirm] immediately! $R;" +
                                               "$R after clicking [confirm], $R;" +
                                               "It can no longer be modified, $R;" +
                                               "So you have to think about it before assigning it! $R;" +
                                               "If you want to modify $P, please click [Cancel] or $R;" +
                                               "[ - ] next to the [Reward Points] window, $R;" +
                                               "It will return to the starting state! $R;" +
                                               "The value of $P will have a great impact on the future, $R;" +
                                               "Don't allocate it now! $R;" +
                                               "$P when upgrading, $R;" +
                                               "You will automatically get [reward points]! $R;" +
                                               "$R is not good enough to allocate the ability value, it is impossible, $R;" +
                                               "Now we still need to increase our strength! $R;", "Berial");
                        break;
                }

                selection = Select(pc, "Which explanation do you want to hear?", "", "How to use [Items]", "How to fight", "How to use [Skills]", "About [reward points]", "Nothing I want to ask");
            }
            

            if (!Beginner_01_mask.Test(Beginner_01.Belial_gives_the_novice_ribbon))
            {
                Belial_gives_the_novice_ribbon(pc);
            }

            Say(pc, 11000931, 131, "The next map, Masha will tell you. $R;" +
                                   "$P she is an Emir girl, $R;" +
                                   "Although it is a bit troublesome! $R;" +
                                   "Don't tell Masha what $R just said! $R;" +
                                   "$R Masha will tell you about $R;" +
                                   "The news of [Flying Garden]! $R;" +
                                   "$P goes straight along this road, $R;" +
                                   "She is in front, $R;" +
                                   "You won't get lost while walking and looking at the mini map. $R;" +
                                   "$P, that's right! $R;" +
                                   "When you meet other people on the road on $R, talk to them too! $R;" +
                                   "$R they will tell you the history of this world, $R;" +
                                   "Or system or something! $R;" +
                                   "Don't worry if $P goes the wrong way. $R;" +
                                   "$R just now, did you hear Lulier say? $R;" +
                                   "The red dot on the minimap indicates the location of the NPC. $R;" +
                                   "When $P gets lost, $R;" +
                                   "Just walk to that point. $R;" +
                                   "$R, go on! $R;", "Berial");
        }

        void Conversation_with_Belial_for_the_first_time(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01. Have_had_the_first_conversation_with_Berial, true);

            byte x, y;

            Say(pc, 11000931, 131, "It’s nice to see you for the first time, $R;" +
                                   "My name is Belial. $R;" +
                                   "$R has been commissioned from Emil, $R;" +
                                   "Introduce various things to the newcomers! $R;" +
                                   "$P is a long story...$R;" +
                                   "Is it okay? $R;", "Berial");

            switch (Select(pc, "Where do you want to start listening?", "", "I want to start listening from the beginning!", "Skip the battle instruction part", "Go to the last part of the instruction"))
            {
                case 1:
                    Say(pc, 11000931, 131, "Are you ready? Let's start now! $R;" +
                                           "$R I only tell you the basic concepts, $R;" +
                                           "Others, you can practice yourself! $R;", "Berial");
                    break;

                case 2:
                    Say(pc, 11000931, 131, "Oh? Really? $R;" +
                                           "It has nothing to do with me...$R;", "Berial");

                    switch (Select(pc, "Do you want to hear the explanation?", "", "I want to hear", "To the next stage"))
                    {
                        case 1:
                            Say(pc, 11000931, 131, "Are you ready? Let's start now! $R;" +
                                                   "$R I only tell you the basic concepts, $R;" +
                                                   "Others, you can practice yourself! $R;", "Berial");
                            break;

                        case 2:
                            Say(pc, 11000931, 131, "Is that right? $R;" +
                                                   "The next map, Masha will tell you. $R;" +
                                                   "$P she is an Emir girl, $R;" +
                                                   "Although it is a bit troublesome! $R;" +
                                                   "Don't tell Masha what $R just said! $R;" +
                                                   "$R Masha will tell you about $R;" +
                                                   "The news of [Flying Garden]! $R;" +
                                                   "$P goes straight along this road, $R;" +
                                                   "She is in front, $R;" +
                                                   "You won't get lost while walking and looking at the mini map. $R;" +
                                                   "$P, that's right! $R;" +
                                                   "When you meet other people on the road on $R, talk to them too! $R;" +
                                                   "$R they will tell you the history of this world, $R;" +
                                                   "Or system or something! $R;" +
                                                   "Don't worry if $P goes the wrong way. $R;" +
                                                   "$R just now, did you hear Lulier say? $R;" +
                                                   "The red dot on the minimap indicates the location of the NPC. $R;" +
                                                   "When $P gets lost, $R;" +
                                                   "Just walk to that point. $R;" +
                                                   "$R, go on! $R;", "Berial");
                            break;
                    }
                    break;

                case 3:
                    Say(pc, 11000931, 131, "Really? $R;" +
                                           "I don't care. $R;", "Berial");

                    switch (Select(pc, "Do you want to continue listening to the instructions?", "", "Continue listening", "Quit"))
                    {
                        case 1:
                            Say(pc, 11000931, 131, "Are you ready? Let's start now! $R;" +
                                                   "$R I only tell you the basic concepts, $R;" +
                                                   "Others, you can practice yourself! $R;", "Berial");
                            break;

                        case 2:
                            Say(pc, 11000931, 131, "I know $R;" +
                                                   "$R, I will send you to [Acropolis]! $R;" +
                                                   "$P won't say I'm driving you away? $R;" +
                                                   "$P Titus in front...$R;" +
                                                   "Just ask him, $R;" +
                                                   "$R, go ahead! $R;", "Berial");

                            x = (byte)Global.Random.Next(18, 29);
                            y = (byte)Global.Random.Next(124, 130);

                            Warp(pc, 10025001, x, y);
                            break;
                    }
                    break;
            }
        }     

        void 戰鬥教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.HP_recovery_teaching_completed))
            {
                Beginner_01_mask.SetValue(Beginner_01.HP_recovery_teaching_completed, true);

                Say(pc, 11000931, 131, "Probably know what it means? $R;" +
                                        "When your HP drops, just eat this! $R;", "Berial");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10006350, 1);
                Say(pc, 0, 0, "Get [meat]! $R;", "");
            }
            else
            {
                Beginner_01_mask.SetValue(Beginner_01.Combat_teaching_completed, true);

                Say(pc, 11000931, 131, "Have you recovered your HP? $R;" +
                                        "$R wants to restore HP, you can use props, $R;" +
                                        "You can also recover by [sit down.] $R;" +
                                        "$P Just click the [Insert] button to sit down. $R;" +
                                        "$P If you click again, $R;" +
                                        "Or do other actions, it will be automatically released. $R;" +
                                        "$P and, in the city or in a tent, $R;" +
                                        "It will be restored automatically! $R;" +
                                        "$R is both cost-free and effective! $R;", "Berial");
            }
        }

        void 技能教學(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.Get_the_second_skill_stone))
            {
                Beginner_01_mask.SetValue(Beginner_01.Get_the_second_skill_stone, true);

                Say(pc, 11000931, 131, "How about? $R;" +
                                       "Has the attack power become stronger? $R;", "Berial");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 20050005, 1);
                Say(pc, 0, 0, "Get the skill stone [Flint] $R;", "");

                Say(pc, 11000931, 131, "The second skill is [Burning City with Flames]! $R;" +
                                       "$R is one of the [magic] skills, $R;" +
                                       "Used to catalyze the various forces that exist in the world. $R;" +
                                       "$P chooses the profession of the magician series, $R;" +
                                       "You can learn it! $R;" +
                                       "The way to use $P is to double-click the skill stone, $R;" +
                                       "The buoy will become a magic wand, $R;" +
                                       "Then click where the skill is cast. $R;" +
                                       "$R time control is not easy, $R;" +
                                       "But try it! $R;", "Berial");
                return;
            }

            if (!Beginner_01_mask.Test(Beginner_01.Get_the_third_skill_gem))
            {
                Beginner_01_mask.SetValue(Beginner_01.Get_the_third_skill_gem, true);

                Say(pc, 11000931, 131, "How about? Use gorgeous skills, $R;" +
                                       "Very cool, isn't it? $R;", "Berial");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 20050001, 1);
                Say(pc, 0, 0, "Obtained the skill stone [Whirlwind Sword] $R;", "");

                Say(pc, 11000931, 131, "The third skill is [Whirlwind Sword]! $R;" +
                                       "$P use this skill, $R;" +
                                       "It will give monsters a very strong blow! $R;" +
                                       "$R is a skill called [technology], $R;" +
                                       "Mainly for the skills learned for the Warrior series! $R;" +
                                       "$P is a skill that needs to be cast through a sword. $R;" +
                                       "$R can't be used by anyone without a sword. Pay attention! $R;" +
                                       "$P when you need to specify, $R;" +
                                       "Just right-click on the skill icon! $R;" +
                                       "The conditions and effects of $R are detailed. $R;" +
                                       "When $P is surrounded by enemies, $R;" +
                                       "Using this skill is very effective! $R;" +
                                       "$R try it! $R;", "Berial");
                return;
            }
            else
            {
                Beginner_01_mask.SetValue(Beginner_01.Skill_teaching_completed, true);

                Say(pc, 11000931, 131, "Are there any skills you like? $R;" +
                                        "Experimental skills have been completed. $R;" +
                                        "$P use skills for each occupation, $R;" +
                                        "Both MP and SP will be consumed! $R;" +
                                        "$R is not only the HP value, but $R;" +
                                        "At the same time, pay attention to the MP value and SP value! $R;" +
                                        "The recovery method of $P is the same as the HP value, $R;" +
                                        "You can sit and recover, you can also use props, $R;" +
                                        "It can also be automatically restored in the city or in a tent! $R;", "Berial");
            }
        }

        void Belial_gives_the_novice_ribbon(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.Belial_gives_the_novice_ribbon, true);

            Say(pc, 11000931, 131, "Basic concepts, $R;" +
                                   "It's probably clear now? $R;" +
                                   "$P right, and this one for you...$R;", "Berial");

            PlaySound(pc, 2040, false, 100, 50);
            GiveItem(pc, 50053600, 1);
            Say(pc, 0, 0, "Get the [Novice Ribbon]! $R;", "");

            Say(pc, 11000931, 131, "The [Novice Ribbon] for you, $R;" +
                                   "Just look at the name and you will know that it is the equipment for the novice. $R;" +
                                   "$R when equipped with [Novice Ribbon], $R;" +
                                   "Just double-click the item, $R;" +
                                   "Bring it later! $R;" +
                                   "$P just bring this, $R;" +
                                   "Others will definitely help! $R;" +
                                   "$P... By the way, $R;" +
                                   "$P waits for you to grow stronger. $R;" +
                                   "Seeing someone else is carrying it, $R;" +
                                   "You can know that the person needs help! $R;" +
                                   "$R go and help him then, $R;" +
                                   "Novices who are just starting an adventure, $R;" +
                                   "I'm always a little uneasy, right? $R;", "Berial");
        }  
    }
}
