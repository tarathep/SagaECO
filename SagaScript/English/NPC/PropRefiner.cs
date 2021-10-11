using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 道具精製師 : Event
    {
        public 道具精製師()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            string free;
            int money;

            if (!World_01_mask.Test(World_01.Complete_the_hair_conditioner_synthesis_task))
            {
                Conditioner_synthesis_task(pc);
                return;
            }

            if (!World_01_mask.Test(World_01.First_story_of_the_progress_of_the_refiner_of_tools))
            {
                Talk_to_the_prop_refiner_for_the_first_time(pc);
                return;
            }

            if (pc.Level < 16)
            {
                free = "Free";
                money = 0;
            }
            else
            {
                free = "100 gold coins";
                money = 100;
            }

            switch (Select(pc, "What is good?", "", "Open the treasure chest (" + free + ")", "Refined props", "Medicine synthesis", "Wood processing", "Purchase repair kit ", "give up"))
            {
                case 1:
                    if (pc.Gold >= money)
                    {
                        pc.Gold -= money;

                        OpenTreasureBox(pc);
                    }
                    else
                    {
                        Say(pc, 131, "Insufficient funds! $R;");
                    }
                    break;

                case 2:
                    Synthese(pc, 2009, 4);
                    break;

                case 3:
                    Synthese(pc, 2022, 5);
                    break;

                case 4:
                    Synthese(pc, 2020, 3);
                    break;

                case 5:
                    OpenShopBuy(pc, 6);
                    break;

                case 6:
                    break;
            }
        }

        void Talk_to_the_prop_refiner_for_the_first_time(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            int selection;

            World_01_mask.SetValue(World_01.First_story_of_the_progress_of_the_refiner_of_tools, true);

            Say(pc, 131, "I am $R from [Acropolis City];" +
                           "$R sent by the [Guild Council];" +
                           "[Item Refiner]. $R;" +
                           "$P is responsible for processing the [refining] and [compositing] of the props, $R;" +
                           "Besides, $R;" +
                           "And responsible for the opening of the [treasure chest]. $R;" +
                           "$R in case there is a [treasure box] that cannot be opened, $R;" +
                           "Get it to me! $R;", "Item Refiner");

            selection = Select(pc, "Anything I want to ask?", "", "What is [refined]?", "What is [synthesis]?", "Entrust the method of [refining] and [synthesis]", "What is [Treasure Box]?", "Nothing I want to ask");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "The so-called [refined], $R;" +
                                     "It is to remove the impurities from the props, $R;" +
                                     "It becomes better and useful material. $R;" +
                                     "$P If you are a [production profession], $R;" +
                                     "Someday, $R;" +
                                     "You can make it yourself. $R;" +
                                     "$P Besides, $R;" +
                                     "At the same time, [weight] and [volume] $R;" +
                                     "The effect becomes smaller, so when you want to move the props, $R;" +
                                     "Learning to [refined] will be more convenient! $R;", "Item Refiner");
                        break;

                    case 2:
                        Say(pc, 131, "The so-called [synthesis], $R;" +
                                     "Is using more than 2 items to $R;" +
                                     "The meaning of making new props. $R;" +
                                     "$R I am responsible for the synthesis of $R;" +
                                     "[Medicine Synthesis] and [Wood Processing]. $R;" +
                                     "$P [Synthesis of drugs] is to combine the effective $R;" +
                                     "Powder and liquid mixed together, $R;" +
                                     "A way to make drug with better effects. $R;" +
                                     "$R [Wood processing] is the raw material of wood, $R;" +
                                     "Method of processing into other forms of parts. $R;" +
                                     "$P if you are a [production profession], $R;" +
                                     "Use different skills, $R;" +
                                     "You can perform different synthesis, $R;" +
                                     "You can do it by yourself. $R;", "Item Refiner");
                        break;

                    case 3:
                        Say(pc, 131, "The method of delegation is very simple! $R;" +
                                     "$R is holding the production materials, $R;" +
                                     "Just talk to me. $R;" +
                                     "$P use the material you are holding, $R;" +
                                     "I will tell you you can $R;" +
                                     "The secret recipe of [refined] or [synthetic]. $R;" +
                                     "$R then decide what to do and do a few, $R;" +
                                     "Press [ok] again. $R;" +
                                     "When the $P items are insufficient, $R;" +
                                     "Will tell you what is lacking. $R;" +
                                     "$R so just use that as a reference! $R;" +
                                     "$P[Refined] and [Synthetic]$R;" +
                                     "It is possible to fail. $R;" +
                                     "$R in order to prevent failure, $R;" +
                                     "If you prepare more materials, $R;" +
                                     "It will be more at ease. $R;", "Item Refiner");
                        break;

                    case 4:
                        Say(pc, 131, "The so-called [treasure box], $R;" +
                                     "It was dropped in a field or a dungeon, $R;" +
                                     "Box with props. $R;" +
                                     "$P contains various props, $R;" +
                                     "But if you don't open it, $R;" +
                                     "No one knows what items are in it. $R;" +
                                     "$R! You can sell it without opening it! $R;" +
                                     "$P [Treasure Box] has three types, $R;" +
                                     "[Wooden Box], [Treasure Box], [Container]. $R;" +
                                     "$R is especially [container], $R;" +
                                     "It is a product of the mechanical age. $R;" +
                                     "$R contains valuable items called [Excavations]! $R;" +
                                     "$P needs [100 gold coins] to open 1 [treasure box], $R;" +
                                     "But there are special rewards for beginners! $R;" +
                                     "$R is free until [LV16], $R;" +
                                     "So just take it! $R;", "Item Refiner");
                        break;
                }

                selection = Select(pc, "Anything I want to ask?", "", "What is [refined]?", "What is [synthesis]?", "The method of entrusting [refining] and [synthesis]", "What is [Treasure Box]?", "Nothing I want to ask");
            }
        }

        void Conditioner_synthesis_task(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.The_hair_conditioner_synthesis_task_begins))
            {
                Say(pc, 131, "Hello, I am a prop refiner. $R;" +
                             "$R now holds $R for novices;" +
                             "Send props gifts activity. $R;" +
                             "$P do you want to do a synthesis to see? $R;", "Item Refiner");

                switch (Select(pc, "How to do it?", "", "Do it and see", "Don't do it"))
                {
                    case 1:
                        if (CheckInventory(pc, 10000604, 1) &&
                            CheckInventory(pc, 10001905, 1) &&
                            CheckInventory(pc, 10035400, 1))
                        {
                            World_01_mask.SetValue(World_01.The_hair_conditioner_synthesis_task_begins, true);

                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10000604, 1);
                            GiveItem(pc, 10001905, 1);
                            GiveItem(pc, 10035400, 1);
                            Say(pc, 0, 65535, "Get [Honey], $R;" +
                                              "『Bark Essence』、$R;" +
                                              "『Resurrection Potion』!$R;", "");

                            Say(pc, 131, "What to do this time is $R;" +
                                         "Mysterious potion for hair growth and rapid growth $R;" +
                                         "『Hair conditioner』.$R;" +
                                         "$P killer bee off has a good moisturizing effect $R;" +
                                         "『Honey』!$R;" +
                                         "$R grows on a small tree, $R;" +
                                         "$R; makes hair soft and shiny" +
                                         "『Bark Essence』!$R;" +
                                         "$P and $R to make dead pores alive;" +
                                         "[Resurrection Potion]! $R;" +
                                         "$P uses the [medicine synthesis] skill for these items, $R;" +
                                         "Synthesis, right? $R;", "Item Refiner");

                            switch (Select(pc, "How to do it?", "", "Do and see", "Receive only props"))
                            {
                                case 1:
                                    Synthese(pc, 2022, 5);
                                    break;

                                case 2:
                                    World_01_mask.SetValue(World_01.Complete_the_hair_conditioner_synthesis_task, true);

                                    pc.Fame = pc.Fame - 1;

                                    Say(pc, 131, "Too much...!$R;", "Item Refiner");
                                    break;
                            }
                        }
                        else
                        {
                            Say(pc, 131, "Huh?$R;" +
                                         "$R can't give you props! $R;" +
                                         "Come on after reducing the things on your body! $R;", "Item Refiner");
                        }
                        break;

                    case 2:
                        World_01_mask.SetValue(World_01.Complete_the_hair_conditioner_synthesis_task, true);
                        break;
                }
            }
            else
            {
                World_01_mask.SetValue(World_01.Complete_the_hair_conditioner_synthesis_task, true);

                Say(pc, 131, "It's all ready, do you want to take a look? $R;" +
                             "If $R is synthesized in this way, $R;" +
                             "You can make interesting things. $R;" +
                             "$R, you can also experience various synthesis! $R;", "Item Refiner");
            }
        }
    }
}