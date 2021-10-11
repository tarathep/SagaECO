using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaMap;

using SagaLib;
//所在地圖:鬥技場(20080000) NPC基本信息:主人(11000405) X:25 Y:8
namespace SagaScript.M20080000
{
    public class S11000405 : Event
    {
        public S11000405()
        {
            this.EventID = 11000405;
        }

        public override void OnEvent(ActorPC pc)
        {
            byte x, y;

            if (pc.Mode == PlayerMode.COLISEUM_MODE)
            {
                CancelBattleMode(pc);
                return;
            }

            Say(pc, 11000405, 131, "Welcome to the Arena!!$R;" +
                                   "$R has multiple ways to play, $R;" +
                                   "A battle space for adventurers to learn from each other. $R;" +
                                   "$P but you have to apply to me first, $R;" +
                                   "Otherwise, you won't be able to participate in the war. $R;" +
                                   "$P duel is available, $R;" +
                                   "Divide the contestants into $R;" +
                                   "The [Knights Practice] of the East, South, West, and North Knights. $R;" +
                                   "$R also transformed all the contestants into Pi Lulu, $R;" +
                                   "A [Dream Rhapsody] that divides into two competitions. $R;" +
                                   "How about $P? $R;" +
                                   "$R want to participate? $R;", "Master");

            switch (Select(pc, "'Which competitive activity do you want to participate in?", "", "Participate in [Arena]", "Participate in [Knights Practice]", "Participate in [Dream Rhapsody]", "From Go out of the Arena", "Do nothing"))
            {
                case 1:
                    ArenaModeSwitch(pc);
                    break;

                case 2:
                    KnightsExercise(pc);
                    break;

                case 3:
                   // GOTO EVT11000405202;
                    break;

                case 4:
                    x = (byte)Global.Random.Next(134, 135);
                    y = (byte)Global.Random.Next(47, 50);

                    Warp(pc, 10024000, x, y);
                    break;

                case 5:
                    break;
            }
        }

        void KnightsExercise(ActorPC pc)
        {
            Say(pc, 11000405, 131, "Do you want to participate in the practice of the Knights? $R;" +
                 "Let me tell you the rules in detail? $R;");
            int sel2;
            do
            {
                sel2 = Select(pc, "Listen to the knights practice rules?", "", "Listen to the knights practice rules.", "Knights practice registration method", "let's give up");
                switch (sel2)
                {
                    case 1:
                        Say(pc, 11000405, 131, "Knights practice is divided into southeast and northwest groups $R;" +
                            "Competition for scoring within the specified time, $R;" +
                            "There are several ways to score $P $R;" +
                            "[Knock down the opponent player] 2 points $R;" +
                            "[Destroying Garnet] 1 point $R;" +
                            "[Destroy Sapphire] 3 points $R;" +
                            "[Destroy the emerald] 5 points $R;" +
                            "[Destroy tourmaline] 50 points $R;" +
                            "Just complete the above behavior $R;" +
                            "You can get scores for your own team, $R;" +
                            "$P still want to hear more detailed? $R;");
                        int sel;
                        do
                        {
                            sel = Select(pc, "Continue listening?", "", "What is garnet?", "Why only tourmaline can get 50 points?", "What happens if I get knocked down?", "Is there any Participate in the prize?", "Okay");
                            switch (sel)
                            {
                                case 1:
                                    Say(pc, 11000405, 131, "[Garnet] is generated during war $R;" +
                                        "Rare magical substance $R;" +
                                        "In addition to $R, there are also [Sapphire] and $R;" +
                                        "[Emerald], [tourmaline], etc. $R;" +
                                        "It was also produced in the war $R;" +
                                        "$P destroy these gems $R;" +
                                        "You can get points for your own team $R;" +
                                        "$P and if it is a person with knowledge and skills $R;" +
                                        "If you destroy these magical substances, $R;" +
                                        "You can also get props $R;" +
                                        "These props are $R; after the knights have practiced them" +
                                        "It will not disappear $R;" +
                                        "So the production department, the props must be $R;" +
                                        "Save to the end, $R;");
                                    break;
                                case 2:
                                    Say(pc, 11000405, 131, "[Tourmaline] is the magic substance with the highest score, $R;" +
                                        "You can know the location of all the contestants $R;" +
                                        "$P and the person who destroyed the [tourmaline] $R;" +
                                        "When participating in the competition, the score will be doubled. $R;" +
                                        "$R but there are several conditions $R;" +
                                        "$P1) Destroy tourmaline by the person in Yi, $R;" +
                                        "The owner can get double the score $R;" +
                                        "$P2) If the person who scored twice logs out, $R;" +
                                        "The effect will be transferred to the surrounding contestants. $R;" +
                                        "$P3) And if the person with twice the score dies, $R;" +
                                        "The effect will be transferred to the person who defeated him. $R;");
                                    break;
                                case 3:
                                    Say(pc, 11000405, 131, "During knight training, even if you are killed $R;" +
                                        "You can also get help from your companion after the resurrection $R;" +
                                        "Return to storage point 唷$R;" +
                                        "$P But if you choose to return to the storage point, $R;" +
                                        "When you return to the atrium of the registered building $R;" +
                                        "$P to return to the front line, you must wait there for 3 minutes $R;" +
                                        ", then speak to the registration staff $R;" +
                                        "$P rest assured, the knights practice without death penalty $R;" +
                                        "$P is not like a fighting arena, $R;" +
                                        "Weapon and armor $R may reduce their durability. $R;");
                                    break;
                                case 4:
                                    Say(pc, 11000405, 131, "After the knights practice is over, $R;" +
                                        "Basic experience points will also be assigned to all participants. $R;" +
                                        "$P allocation standard is based on the following conditions to determine $R;" +
                                        "1) The number of participants in the knights' practice is $R;" +
                                        "2) Order of own group $R;" +
                                        "3) The number of people in the group $R;" +
                                        "4) Own score $R;" +
                                        "5) Have you got various rewards $R;" +
                                        "There are several conditions for $P to be rewarded, $R;" +
                                        "The number of personal deaths, $R;" +
                                        "Hold instant maximum attack damage $R;" +
                                        "The number of props held by the individual $R;" +
                                        "Individual cumulative amount of damage $R;");
                                    break;
                                case 5:
                                    Say(pc, 11000405, 131, "Then, do you want to take a try? $R;");
                                    break;
                            }
                        }
                        while (sel != 5);
                        break;
                    case 2:
                        //KWAR_ENTRY 0
                        if (true)
                        {
                            Say(pc, 11000405, 131, "Sorry, it's not the registration time, $R;" +
                                 "Please wait until you can sign up, come again $R;");
                            return;
                        }
                        if (true)
                        {
                            Say(pc, 11000405, 131, "Sorry, next time the Knights practice $R;" +
                                "There are level restrictions, $R;" +
                                "$R, your strength is too strong, so you can't participate. $R;");
                            return;
                        }
                        Say(pc, 11000405, 131, "Then sign up in the atrium of this building. $R;");
                        switch (Global.Random.Next(1, 4))
                        {
                            case 1:
                                //WARP 752
                                break;
                            case 2:
                                //WARP 753
                                break;
                            case 3:
                                //WARP 754
                                break;
                            case 4:
                                //WARP 755
                                break;
                        }
                        break;
                }
            } while (sel2 == 1);
        }

        void ArenaModeSwitch(ActorPC pc)
        {
            if (pc.PossesionedActors.Count != 0)
            {
                Say(pc, 11000405, 131, "Sorry, $R;" +
                                         "Can you please lift Pingyi? $R;" +
                                         "$R, please apply for the competition and then rely on it? $R;", "Master");
            }
            else
            {
                Say(pc, 11000405, 131, "I got it. $R;" +
                                       "Now I will give you 15 seconds to prepare. $R;" +
                                       "$P get ready!! $R;", "Master");

                pc.Mode = PlayerMode.COLISEUM_MODE;

                //SagaMap.Tasks.PC.PVPTime task = new SagaMap.Tasks.PC.PVPTime();
                //pc.Tasks.Add("PVPTime", task);
                //task.Activate();
            }
        }

        void CancelBattleMode(ActorPC pc)
        {
            Say(pc, 11000405, 131, "What's wrong? $R;" +
                                    "$R don't want to participate? $R;", "Master");

            switch (Select(pc, "Give up to participate?", "", "Don't give up", "Give up to participate"))
            {
                case 1:
                    break;

                case 2:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        Say(pc, 11000405, 131, "Sorry, $R;" +
                                               "Can you please lift Pingyi? $R;", "Master");
                    }
                    else
                    {
                        pc.Mode = PlayerMode.NORMAL;

                        Say(pc, 11000405, 131, "I look forward to your next participation!!$R;", "Master");
                    }
                    break;
            }
        }


    }
}
