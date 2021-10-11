using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城東邊吊橋(10062000) NPC基本信息:初心者嚮導(11000532) X:80 Y:206
namespace SagaScript.M10023100
{
    public class S11000532 : Event
    {
        public S11000532()
        {
            this.EventID = 11000532;
        }

        public override void OnEvent(ActorPC pc)
        {
            {
                if (pc.Account.GMLevel >= 1)
                {
                    if (Select(pc, "What should I do?", "", "Enter VIP service", "Forget it") == 1)
                    {
                        ForVIP(pc);
                        return;
                    }
                }
                Say(pc, 11000405, 131, "Welcome!!$R;" +
                                       "$R I have many different services here $R;" +
                                       "The service will be constantly updated. $R;" +
                                       "$R want me to serve you? $R;", "Novice Guide");
                switch (Select(pc, "General Service", "", "Washing attribute points (Unlimited washing before level 20)", "Washing skill points (Unlimited washing before level 20)", "Teleport to the suspension bridge to the north", " do nothing"))
                {
                    case 1:
                        if (pc.Race == PC_RACE.DEM)
                        {
                            Say(pc, 131, "DEM can't wash some $R;");
                        }
                        if (pc.Level <= 20)
                        {
                            ResetStatusPoint(pc);
                            //STATUSRESET
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "Return to the initial state $R;");
                            break;
                        }
                        if (pc.Gold <= 100000)
                        {
                            Say(pc, 131, "There is not enough money $R;");
                            return;
                        }
                        ResetStatusPoint(pc);
                        //STATUSRESET
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        pc.Gold -= 100000;
                        Say(pc, 131, "Return to the initial state $R;");
                        break;
                    case 2:
                        if (pc.Race == PC_RACE.DEM)
                        {
                            Say(pc, 131, "DEM can't wash some $R;");
                        }
                        if (pc.Skills.Count == 0 &&
                            pc.Skills2.Count == 0)
                        {
                            Say(pc, 131, "I don't have a skill $R;");
                            return;
                        }
                        if (pc.Level <= 20)
                        {
                            ResetSkill(pc, 1);
                            ResetSkill(pc, 2);
                            //SKILLRESET_ALL
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "The skill is reset $R;");
                            break;
                        }
                        if (pc.Gold <= 100000)
                        {
                            Say(pc, 131, "There is not enough money $R;");
                            return;
                        }
                        ResetSkill(pc, 1);
                        ResetSkill(pc, 2);
                        //SKILLRESET_ALL
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        pc.Gold -= 100000;
                        Say(pc, 131, "The skill is reset $R;");
                        break;
                case 3:
                    Warp(pc, 10023400, 127, 9);
                    break;
                }
            }
        }
        void ForVIP(ActorPC pc)
        {
            if (pc.PossesionedActors.Count != 0)
            {
                PlaySound(pc, 2225, false, 100, 50);
                Say(pc, 131, "咇!!!!$R;" +
                    "Forbidden...forbid $R;" +
                    "$R can't pass depending on the status $R;" +
                    "$P...$R;" +
                    "This is forbidden zone $R;" +
                    "In order to prevent Pingyi from passing, $R;" +
                    "A special checkpoint has been set up for the inspection basis $R;" +
                    "$P pass by relying on $R refers to the place that generally cannot be passed, $R;" +
                    "You can pass $R; in a dependent state;" +
                    "$This is a very convenient technique, $R;" +
                    "But...$R;" +
                    "$P...$R;" +
                    "Go back if you know it $R;");
                return;
            }
            //EVT1100078669
            switch (Select(pc, "VIP Service", "", "Enter VIP Management Area", "Wash Attribute Point", "Wash Skill Point", "Teleport to the North Suspension Bridge", "Do nothing"))
            {
                case 1:
                    Warp(pc, 20000002, 64, 16);
                    break;
                case 2:
                    if (pc.Race == PC_RACE.DEM)
                    {
                        Say(pc, 131, "DEM can't wash some $R;");
                    }
                    ResetStatusPoint(pc);
                    //STATUSRESET
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    Say(pc, 131, "Return to the initial state $R;");
                    break;
                case 3:
                    if (pc.Race == PC_RACE.DEM)
                    {
                        Say(pc, 131, "DEM can't wash some $R;");
                    }
                    ResetSkill(pc, 1);
                    ResetSkill(pc, 2);
                    //SKILLRESET_ALL
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    Say(pc, 131, "The skill is reset $R;");
                    break;
                    case 4:
                        Warp(pc, 10023400, 127, 9);
                    break;
                        }

                }
            }
        }