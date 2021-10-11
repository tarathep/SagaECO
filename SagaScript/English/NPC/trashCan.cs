using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class trash_can : Event
    {
        public trash_can()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_amask = new BitMask<Neko_01>(pc.AMask["Neko_01"]);
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            BitMask<Neko_06> Neko_06_amask = pc.AMask["Neko_06"];
            BitMask<Neko_06> Neko_06_cmask = pc.CMask["Neko_06"];

            if (!World_01_mask.Test(World_01.Using_the_trash_can_for_the_first_time))
            {
                Primary_use_tub(pc);
                return;
            }

            if (Neko_06_amask.Test(Neko_06.Kyoko_mission_begins) &&
                !Neko_06_amask.Test(Neko_06.GetApricots) &&
                Neko_01_amask.Test(Neko_01.Peach_mission_completed) &&
                Neko_06_cmask.Test(Neko_06.Learn_how_to_recover) &&
                !Neko_06_cmask.Test(Neko_06.Get_peach_pieces))
            {
                Wait(pc, 990);
                ShowEffect(pc, 5058);
                Wait(pc, 990);
                ShowEffect(pc, 5179);
                Wait(pc, 1980);

                Say(pc, 0, 131, "(... oh! $ R;" +
                 "$ R I feel nostalgic ...?! $ R;" +
                 "$ R This sign is ......... Peach !!) $ R;", "");

                Say(pc, 0, 131, "Nyao ~~~ n! $ R;", "");
                Wait(pc, 990);
                PlaySound(pc, 4012, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 5940);

                Say(pc, 0, 131, "[Nekomata(peach)] regained the heart of $ R! $ R;", "");

                Say(pc, 0, 131, "Sister! $ R;", "Nekomata");

                Say(pc, 0, 131, "Apricot !? $ R;" +
                "$ R That? ... Why did I ...? $ R;" +
                "$ R Why in the trash ... $ R;", "Nekomata (peach)");

                Say(pc, 0, 131, "(... good.) $ R;", "");
                Neko_06_cmask.SetValue(Neko_06.Get_peach_pieces, true);
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.Momoko_mission_started) &&
                !Neko_01_amask.Test(Neko_01.Peach_mission_completed) &&
                pc.Fame > 9 &&
                pc.Level > 19)
            {
                MomokoMission_1(pc);
                return;
            }

            if (!Neko_01_amask.Test(Neko_01.Peach_mission_completed) &&
                !Neko_01_amask.Test(Neko_01.Temple_who_got_the_unknown_object) &&
                pc.Fame > 9 &&
                pc.Level > 19)
            {
                桃子任務2(pc);
            }

            pc.CInt["Disposition"] += 1;

            NPCTrade(pc);
        }

        void Primary_use_tub(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = pc.CMask["World_01"];

            World_01_mask.SetValue(World_01.Using_the_trash_can_for_the_first_time, true);

            Say(pc, 0, 65535, "This is [trash can], $R;" +
                              "To keep this world clean, $R;" +
                              "Please throw the trash in trash_can. $R;" +
                              "$R God is guarding you whenever and wherever. $R;", "");

            switch (Select(pc, "Do you want to see how to use it?", "", "Watch", "Don't watch"))
            {
                case 1:
                    Say(pc, 0, 65535, "In trash_can, you can throw anything. $R;" +
                                      "$R is a special item that can't be thrown away normally, $R;" +
                                      "Of course you can also throw it into trash_can. $R;" +
                                      "$P discarded items, $R;" +
                                      "[Never] cannot be restored. $R;" +
                                      "$R be careful when throwing away items! $R;" +
                                      "$P as long as you think it is garbage, $R;" +
                                      "Even though it was expensive, $R;" +
                                      "It's just rubbish. $R;" +
                                      "$P rubbish, you can throw it away through the transaction window. $R;" +
                                      "$P pull the item you want to throw away to the trading window, $R;" +
                                      "Press the [Confirm] button and the [Trade] button to $R;", "");
                    NPCTrade(pc);
                    break;

                case 2:
                    NPCTrade(pc);
                    break;
            }
        }

        void MomokoMission_1(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];

            Neko_01_amask.SetValue(Neko_01.Momoko_mission_started, true);

            Say(pc, 0, 65535, "Meow!$R;", "");
            Say(pc, 0, 65535, "……?!$R;", "");
            Say(pc, 0, 65535, "Where is the crying...?$R;", "");
        }

        void 桃子任務2(ActorPC pc)
        {
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];

            int WORK0;

            WORK0 = Global.Random.Next(1, 100);

            if (WORK0 < 10)
            {
                if (CheckInventory(pc, 10035610, 1))
                {
                    Neko_01_amask.SetValue(Neko_01.Temple_who_got_the_unknown_object, true);

                    Say(pc, 0, 65535, "……?$R;", "");
                    Say(pc, 0, 65535, "What's the flash of a boom? $R;", "");

                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10035610, 1);
                    Say(pc, 0, 65535, "Get the [Temple of Unidentified Object]! $R;", "");
                }
            }
        }

    }
}