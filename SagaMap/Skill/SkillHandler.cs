namespace SagaMap.Skill
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Mob;
    using SagaDB.Skill;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Client;
    using SagaMap.Packets.Server;
    using SagaMap.PC;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Skill.SkillDefinations;
    using SagaMap.Skill.SkillDefinations.Alchemist;
    using SagaMap.Skill.SkillDefinations.Archer;
    using SagaMap.Skill.SkillDefinations.Assassin;
    using SagaMap.Skill.SkillDefinations.Bard;
    using SagaMap.Skill.SkillDefinations.Blacksmith;
    using SagaMap.Skill.SkillDefinations.BladeMaster;
    using SagaMap.Skill.SkillDefinations.BountyHunter;
    using SagaMap.Skill.SkillDefinations.Breeder;
    using SagaMap.Skill.SkillDefinations.Cabalist;
    using SagaMap.Skill.SkillDefinations.Command;
    using SagaMap.Skill.SkillDefinations.DarkStalker;
    using SagaMap.Skill.SkillDefinations.Druid;
    using SagaMap.Skill.SkillDefinations.Elementaler;
    using SagaMap.Skill.SkillDefinations.Enchanter;
    using SagaMap.Skill.SkillDefinations.Event;
    using SagaMap.Skill.SkillDefinations.Explorer;
    using SagaMap.Skill.SkillDefinations.Farmasist;
    using SagaMap.Skill.SkillDefinations.Fencer;
    using SagaMap.Skill.SkillDefinations.FGarden;
    using SagaMap.Skill.SkillDefinations.Gambler;
    using SagaMap.Skill.SkillDefinations.Gardener;
    using SagaMap.Skill.SkillDefinations.Global;
    using SagaMap.Skill.SkillDefinations.Gunner;
    using SagaMap.Skill.SkillDefinations.Knight;
    using SagaMap.Skill.SkillDefinations.Machinery;
    using SagaMap.Skill.SkillDefinations.Marionest;
    using SagaMap.Skill.SkillDefinations.Marionette;
    using SagaMap.Skill.SkillDefinations.Merchant;
    using SagaMap.Skill.SkillDefinations.Monster;
    using SagaMap.Skill.SkillDefinations.Necromancer;
    using SagaMap.Skill.SkillDefinations.Ranger;
    using SagaMap.Skill.SkillDefinations.Sage;
    using SagaMap.Skill.SkillDefinations.Scout;
    using SagaMap.Skill.SkillDefinations.Shaman;
    using SagaMap.Skill.SkillDefinations.Sorcerer;
    using SagaMap.Skill.SkillDefinations.Striker;
    using SagaMap.Skill.SkillDefinations.Swordman;
    using SagaMap.Skill.SkillDefinations.Tatarabe;
    using SagaMap.Skill.SkillDefinations.Trader;
    using SagaMap.Skill.SkillDefinations.TreasureHunter;
    using SagaMap.Skill.SkillDefinations.Vates;
    using SagaMap.Skill.SkillDefinations.Warlock;
    using SagaMap.Skill.SkillDefinations.Wizard;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="SkillHandler" />.
    /// </summary>
    public class SkillHandler : Singleton<SkillHandler>
    {
        /// <summary>
        /// Defines the skillHandlers.
        /// </summary>
        private Dictionary<uint, ISkill> skillHandlers = new Dictionary<uint, ISkill>();

        /// <summary>
        /// Defines the elementEffects.
        /// </summary>
        private int[,] elementEffects = new int[7, 7]
        {
      {
        6,
        6,
        6,
        6,
        6,
        6,
        6
      },
      {
        6,
        3,
        0,
        4,
        6,
        1,
        5
      },
      {
        6,
        4,
        3,
        6,
        0,
        1,
        5
      },
      {
        6,
        0,
        6,
        3,
        4,
        1,
        5
      },
      {
        6,
        6,
        4,
        0,
        3,
        1,
        5
      },
      {
        6,
        4,
        4,
        4,
        4,
        3,
        0
      },
      {
        6,
        2,
        2,
        2,
        2,
        4,
        3
      }
        };

        /// <summary>
        /// Defines the elementFactor.
        /// </summary>
        private float[][] elementFactor = new float[7][]
        {
      new float[20]
      {
        1.2f,
        1.3f,
        1.4f,
        1.5f,
        1.6f,
        1.7f,
        1.8f,
        1.9f,
        2f,
        2.1f,
        2.2f,
        2.3f,
        2.4f,
        2.5f,
        2.65f,
        2.8f,
        2.95f,
        3.1f,
        3.3f,
        3.5f
      },
      new float[20]
      {
        1.05f,
        1.1f,
        1.15f,
        1.2f,
        1.25f,
        1.3f,
        1.35f,
        1.4f,
        1.45f,
        1.5f,
        1.55f,
        1.6f,
        1.65f,
        1.7f,
        1.75f,
        1.8f,
        1.85f,
        1.9f,
        1.95f,
        2f
      },
      new float[20]
      {
        1.05f,
        1.1f,
        1.15f,
        1.2f,
        1.25f,
        1.3f,
        1.35f,
        1.4f,
        1.45f,
        1.5f,
        1.55f,
        1.6f,
        1.65f,
        1.7f,
        1.75f,
        1.8f,
        1.85f,
        1.9f,
        1.95f,
        2f
      },
      new float[20]
      {
        0.9f,
        0.8f,
        0.7f,
        0.6f,
        0.5f,
        0.4f,
        0.3f,
        0.2f,
        0.1f,
        0.0f,
        0.0f,
        0.0f,
        0.0f,
        0.0f,
        0.0f,
        0.0f,
        0.0f,
        0.0f,
        0.0f,
        0.0f
      },
      new float[20]
      {
        0.95f,
        0.9f,
        0.85f,
        0.8f,
        0.75f,
        0.7f,
        0.65f,
        0.6f,
        0.55f,
        0.5f,
        0.45f,
        0.4f,
        0.35f,
        0.3f,
        0.25f,
        0.2f,
        0.15f,
        0.1f,
        0.5f,
        0.0f
      },
      new float[20]
      {
        0.97f,
        0.94f,
        0.91f,
        0.88f,
        0.85f,
        0.82f,
        0.79f,
        0.76f,
        0.73f,
        0.7f,
        0.67f,
        0.64f,
        0.61f,
        0.58f,
        0.55f,
        0.52f,
        0.49f,
        0.46f,
        0.43f,
        0.4f
      },
      new float[20]
      {
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f
      }
        };

        /// <summary>
        /// Defines the physicalCounter.
        /// </summary>
        internal int physicalCounter = 0;

        /// <summary>
        /// Defines the magicalCounter.
        /// </summary>
        internal int magicalCounter = 0;

        /// <summary>
        /// The CalcAttackResult.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="ranged">The ranged<see cref="bool"/>.</param>
        /// <returns>The <see cref="SkillHandler.AttackResult"/>.</returns>
        private SkillHandler.AttackResult CalcAttackResult(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, bool ranged)
        {
            SkillHandler.AttackResult attackResult = SkillHandler.AttackResult.Hit;
            int num1 = 5 + (int)sActor.Status.cri_skill;
            int num2 = 0;
            if (SagaLib.Global.Random.Next(0, 99) <= num1)
            {
                attackResult = SkillHandler.AttackResult.Critical;
                num2 = 35;
            }
            int num3;
            int num4;
            if (ranged)
            {
                num3 = (int)sActor.Status.hit_ranged;
                num4 = (int)dActor.Status.avoid_ranged;
            }
            else
            {
                num3 = (int)sActor.Status.hit_melee;
                num4 = (int)dActor.Status.avoid_melee;
            }
            if (sActor.type == ActorType.MOB && dActor.type == ActorType.PC)
            {
                ActorMob actorMob = (ActorMob)sActor;
                foreach (Addition addition in dActor.Status.Additions.Values.ToArray<Addition>())
                {
                    if (addition.GetType() == typeof(SomeTypeAvoUp))
                    {
                        SomeTypeAvoUp someTypeAvoUp = (SomeTypeAvoUp)addition;
                        if (someTypeAvoUp.MobTypes.ContainsKey(actorMob.BaseData.mobType))
                            num4 += (int)((double)num4 * ((double)someTypeAvoUp.MobTypes[actorMob.BaseData.mobType] / 100.0));
                    }
                }
            }
            if (dActor.type == ActorType.MOB && sActor.type == ActorType.PC)
            {
                ActorMob actorMob = (ActorMob)dActor;
                foreach (Addition addition in sActor.Status.Additions.Values.ToArray<Addition>())
                {
                    if (addition.GetType() == typeof(SomeTypeHitUp))
                    {
                        SomeTypeHitUp someTypeHitUp = (SomeTypeHitUp)addition;
                        if (someTypeHitUp.MobTypes.ContainsKey(actorMob.BaseData.mobType))
                            num3 += (int)((double)num3 * ((double)someTypeHitUp.MobTypes[actorMob.BaseData.mobType] / 100.0));
                    }
                }
            }
            int num5 = 50 + (num3 - num4) + ((int)sActor.Level - (int)dActor.Level) + num2;
            if (num5 < 5)
                num5 = 5;
            if (num5 > 95)
                num5 = 95;
            if (dActor.Status.attackingActors.Count > 2)
                num5 += (dActor.Status.attackingActors.Count - 2) * 15;
            if (dActor.Status.Additions.ContainsKey("Parry"))
            {
                dActor.Status.Additions["Parry"].AdditionEnd();
                num5 = 30;
            }
            if (SagaLib.Global.Random.Next(0, 99) <= num5)
            {
                if (attackResult != SkillHandler.AttackResult.Critical)
                    attackResult = SkillHandler.AttackResult.Hit;
            }
            else
            {
                bool flag = false;
                if (dActor.type == ActorType.PC)
                {
                    ActorPC actorPc = (ActorPC)dActor;
                    if (actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND) && actorPc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.SHIELD && SagaLib.Global.Random.Next(0, 99) <= 5 + (int)actorPc.Status.guard_item)
                        flag = true;
                }
                attackResult = !flag ? (SagaLib.Global.Random.Next(0, 99) > 30 ? SkillHandler.AttackResult.Miss : SkillHandler.AttackResult.Avoid) : SkillHandler.AttackResult.Guard;
            }
            if (sActor.Status.Additions.ContainsKey("PrecisionFire"))
                attackResult = SkillHandler.AttackResult.Hit;
            else if (sActor.Status.Additions.ContainsKey("AffterCritical"))
            {
                attackResult = SkillHandler.AttackResult.Critical;
                SkillHandler.RemoveAddition(sActor, "AffterCritical");
            }
            return attackResult;
        }

        /// <summary>
        /// The ProcessAttackPossession.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="List{SagaDB.Actor.Actor}"/>.</returns>
        private List<SagaDB.Actor.Actor> ProcessAttackPossession(SagaDB.Actor.Actor dActor)
        {
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            if (dActor.type != ActorType.PC)
                return actorList;
            foreach (ActorPC possesionedActor in ((ActorPC)dActor).PossesionedActors)
            {
                if (possesionedActor.Online && SagaLib.Global.Random.Next(0, 99) >= (int)possesionedActor.Status.possessionCancel)
                {
                    switch (possesionedActor.PossessionPosition)
                    {
                        case PossessionPosition.RIGHT_HAND:
                            if (SagaLib.Global.Random.Next(0, 99) < 15)
                            {
                                actorList.Add((SagaDB.Actor.Actor)possesionedActor);
                                break;
                            }
                            break;
                        case PossessionPosition.LEFT_HAND:
                            if (SagaLib.Global.Random.Next(0, 99) < 18)
                            {
                                actorList.Add((SagaDB.Actor.Actor)possesionedActor);
                                break;
                            }
                            break;
                        case PossessionPosition.NECK:
                            if (SagaLib.Global.Random.Next(0, 99) < 8)
                            {
                                actorList.Add((SagaDB.Actor.Actor)possesionedActor);
                                break;
                            }
                            break;
                        case PossessionPosition.CHEST:
                            if (SagaLib.Global.Random.Next(0, 99) < 12)
                            {
                                actorList.Add((SagaDB.Actor.Actor)possesionedActor);
                                break;
                            }
                            break;
                    }
                }
            }
            return actorList;
        }

        /// <summary>
        /// The Attack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        public void Attack(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg)
        {
            int comboCount = (int)this.GetComboCount(sActor);
            arg.sActor = sActor.ActorID;
            arg.dActor = dActor.ActorID;
            for (int index = 0; index < comboCount; ++index)
                arg.affectedActors.Add(dActor);
            arg.type = sActor.Status.attackType;
            arg.delayRate = (float)(1.0 + (double)comboCount / 2.0);
            this.PhysicalAttack(sActor, arg.affectedActors, arg, Elements.Neutral, 1f);
            if ((arg.flag[0] | AttackFlag.MISS) == arg.flag[0] || (arg.flag[0] | AttackFlag.AVOID) == arg.flag[0] || !sActor.Status.Additions.ContainsKey("EnchantWeapon"))
                return;
            switch (((DefaultBuff)sActor.Status.Additions["EnchantWeapon"]).skill.Level)
            {
                case 1:
                    鈍足 鈍足 = new 鈍足((SagaDB.Skill.Skill)null, dActor, 6000);
                    SkillHandler.ApplyAddition(dActor, (Addition)鈍足);
                    break;
                case 2:
                    Freeze freeze = new Freeze((SagaDB.Skill.Skill)null, dActor, 4000);
                    SkillHandler.ApplyAddition(dActor, (Addition)freeze);
                    break;
                case 3:
                    Stun stun = new Stun((SagaDB.Skill.Skill)null, dActor, 2000);
                    SkillHandler.ApplyAddition(dActor, (Addition)stun);
                    break;
            }
        }

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg)
        {
            if (this.skillHandlers.ContainsKey(arg.skill.ID) && sActor.type == ActorType.PC)
                return this.skillHandlers[arg.skill.ID].TryCast((ActorPC)sActor, dActor, arg);
            return 0;
        }

        /// <summary>
        /// The SetNextComboSkill.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="id">The id<see cref="uint"/>.</param>
        public void SetNextComboSkill(SagaDB.Actor.Actor actor, uint id)
        {
            if (actor.type != ActorType.PC)
                return;
            MapClient.FromActorPC((ActorPC)actor).nextCombo = id;
        }

        /// <summary>
        /// The SkillCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        public void SkillCast(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg)
        {
            arg.sActor = sActor.ActorID;
            if (arg.dActor != uint.MaxValue)
                arg.dActor = dActor.ActorID;
            if (this.skillHandlers.ContainsKey(arg.skill.ID))
            {
                this.skillHandlers[arg.skill.ID].Proc(sActor, dActor, arg, arg.skill.Level);
                if (arg.affectedActors.Count != 0 || arg.dActor == 0U)
                    return;
                arg.affectedActors.Add(dActor);
                arg.Init();
            }
            else
            {
                arg.affectedActors.Add(dActor);
                arg.Init();
                Logger.ShowWarning("No defination for skill:" + arg.skill.Name + "(ID:" + (object)arg.skill.ID + ")", (Logger)null);
            }
        }

        /// <summary>
        /// The GetComboCount.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        private byte GetComboCount(SagaDB.Actor.Actor actor)
        {
            if (actor.type != ActorType.PC)
                return 1;
            ActorPC actorPc = (ActorPC)actor;
            byte num;
            if (actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                switch (actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType)
                {
                    case ItemType.CLAW:
                    case ItemType.DUALGUN:
                        num = (byte)2;
                        break;
                    default:
                        num = (byte)1;
                        break;
                }
            }
            else
                num = (byte)1;
            if (SagaLib.Global.Random.Next(0, 99) < (int)actor.Status.combo_rate_skill)
                num = (byte)actor.Status.combo_skill;
            return num;
        }

        /// <summary>
        /// The CastPassiveSkills.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void CastPassiveSkills(ActorPC pc)
        {
            Singleton<StatusFactory>.Instance.CalcStatus(pc);
            foreach (string index in pc.Status.Additions.Keys.ToList<string>())
            {
                if (pc.Status.Additions[index].GetType() == typeof(DefaultPassiveSkill))
                    SkillHandler.RemoveAddition((SagaDB.Actor.Actor)pc, pc.Status.Additions[index]);
            }
            foreach (SagaDB.Skill.Skill skill in pc.Skills.Values)
            {
                if (!skill.BaseData.active && this.skillHandlers.ContainsKey(skill.ID))
                    this.skillHandlers[skill.ID].Proc((SagaDB.Actor.Actor)pc, (SagaDB.Actor.Actor)pc, new SkillArg()
                    {
                        skill = skill
                    }, skill.Level);
            }
            foreach (SagaDB.Skill.Skill skill in pc.Skills2.Values)
            {
                if (!skill.BaseData.active && this.skillHandlers.ContainsKey(skill.ID))
                    this.skillHandlers[skill.ID].Proc((SagaDB.Actor.Actor)pc, (SagaDB.Actor.Actor)pc, new SkillArg()
                    {
                        skill = skill
                    }, skill.Level);
            }
            foreach (SagaDB.Skill.Skill skill in pc.SkillsReserve.Values)
            {
                if (!skill.BaseData.active && this.skillHandlers.ContainsKey(skill.ID))
                    this.skillHandlers[skill.ID].Proc((SagaDB.Actor.Actor)pc, (SagaDB.Actor.Actor)pc, new SkillArg()
                    {
                        skill = skill
                    }, skill.Level);
            }
            Singleton<StatusFactory>.Instance.CalcStatus(pc);
        }

        /// <summary>
        /// The CheckBuffValid.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void CheckBuffValid(ActorPC pc)
        {
            foreach (string index in pc.Status.Additions.Keys.ToList<string>())
            {
                if (index != null && pc.Status.Additions[index].GetType() == typeof(DefaultBuff))
                {
                    DefaultBuff addition = (DefaultBuff)pc.Status.Additions[index];
                    if (addition.OnCheckValid != null)
                    {
                        int result;
                        addition.OnCheckValid(pc, (SagaDB.Actor.Actor)pc, out result);
                        if (result != 0)
                            SkillHandler.RemoveAddition((SagaDB.Actor.Actor)pc, pc.Status.Additions[index]);
                    }
                }
            }
        }

        /// <summary>
        /// The ApplyAddition.
        /// </summary>
        /// <param name="actor">Actor which the addition should be applied to.</param>
        /// <param name="addition">Addition to be applied.</param>
        public static void ApplyAddition(SagaDB.Actor.Actor actor, Addition addition)
        {
            if (actor.Status.Additions.ContainsKey(addition.Name))
            {
                Addition addition1 = actor.Status.Additions[addition.Name];
                if (addition1.Activated)
                    addition1.AdditionEnd();
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();
                actor.Status.Additions.Remove(addition.Name);
                actor.Status.Additions.Add(addition.Name, addition);
                if (blocked)
                    return;
                ClientManager.LeaveCriticalArea();
            }
            else
            {
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();
                actor.Status.Additions.Add(addition.Name, addition);
                if (!blocked)
                    ClientManager.LeaveCriticalArea();
            }
        }

        /// <summary>
        /// The RemoveAddition.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public static void RemoveAddition(SagaDB.Actor.Actor actor, string name)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            if (actor.Status.Additions.ContainsKey(name))
                SkillHandler.RemoveAddition(actor, actor.Status.Additions[name]);
            if (blocked)
                return;
            ClientManager.LeaveCriticalArea();
        }

        /// <summary>
        /// The RemoveAddition.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="addition">The addition<see cref="Addition"/>.</param>
        public static void RemoveAddition(SagaDB.Actor.Actor actor, Addition addition)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            SkillHandler.RemoveAddition(actor, addition, false);
            if (blocked)
                return;
            ClientManager.LeaveCriticalArea();
        }

        /// <summary>
        /// The RemoveAddition.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="addition">The addition<see cref="Addition"/>.</param>
        /// <param name="removeOnly">The removeOnly<see cref="bool"/>.</param>
        public static void RemoveAddition(SagaDB.Actor.Actor actor, Addition addition, bool removeOnly)
        {
            if (actor.Status == null || !actor.Status.Additions.ContainsKey(addition.Name))
                return;
            actor.Status.Additions.Remove(addition.Name);
            if (addition.Activated && !removeOnly)
                addition.AdditionEnd();
            addition.Activated = false;
        }

        /// <summary>
        /// The PushBack.
        /// </summary>
        /// <param name="ori">The ori<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dest">The dest<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="step">The step<see cref="int"/>.</param>
        public void PushBack(SagaDB.Actor.Actor ori, SagaDB.Actor.Actor dest, int step)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(ori.MapID);
            if (dest.type == ActorType.MOB)
            {
                MobEventHandler e = (MobEventHandler)dest.e;
                if (e.AI.Mode.Symbol || e.AI.Mode.SymbolTrash)
                    return;
            }
            byte pos1 = SagaLib.Global.PosX16to8(dest.X, map.Width);
            byte pos2 = SagaLib.Global.PosY16to8(dest.Y, map.Height);
            int num1 = (int)pos1 - (int)SagaLib.Global.PosX16to8(ori.X, map.Width);
            int num2 = (int)pos2 - (int)SagaLib.Global.PosY16to8(ori.Y, map.Height);
            if (num1 != 0)
                num1 /= Math.Abs(num1);
            if (num2 != 0)
                num2 /= Math.Abs(num2);
            for (int index = 0; index < step; ++index)
            {
                pos1 += (byte)num1;
                pos2 += (byte)num2;
                if ((int)pos1 >= (int)map.Width || (int)pos2 >= (int)map.Height || map.Info.walkable[(int)pos1, (int)pos2] != (byte)2)
                {
                    pos1 -= (byte)num1;
                    pos2 -= (byte)num2;
                    break;
                }
            }
            short[] pos3 = new short[2]
            {
       SagaLib.Global.PosX8to16(pos1, map.Width),
        SagaLib.Global.PosY8to16(pos2, map.Height)
            };
            map.MoveActor(SagaMap.Map.MOVE_TYPE.START, dest, pos3, (ushort)500, (ushort)500, true);
            if (dest.type == ActorType.MOB)
                ((MobEventHandler)dest.e).AI.OnPathInterupt();
            if (dest.type != ActorType.PET && dest.type != ActorType.SHADOW)
                return;
            ((PetEventHandler)dest.e).AI.OnPathInterupt();
        }

        /// <summary>
        /// The CheckValidAttackTarget.
        /// </summary>
        /// <param name="sActor">攻击者.</param>
        /// <param name="dActor">被攻击者.</param>
        /// <returns>.</returns>
        public bool CheckValidAttackTarget(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor)
        {
            if (sActor == dActor || (sActor == null || dActor == null || dActor.type == ActorType.PC && !((ActorPC)dActor).Online || (dActor.type == ActorType.ITEM || dActor.Buff.Dead)))
                return false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC actorPc1 = (ActorPC)sActor;
                switch (dActor.type)
                {
                    case ActorType.PC:
                        ActorPC actorPc2 = (ActorPC)dActor;
                        return (actorPc1.Mode == PlayerMode.COLISEUM_MODE && actorPc2.Mode == PlayerMode.COLISEUM_MODE || actorPc1.Mode == PlayerMode.WRP && actorPc2.Mode == PlayerMode.WRP || actorPc1.Mode == PlayerMode.KNIGHT_WAR && actorPc2.Mode == PlayerMode.KNIGHT_WAR) && ((actorPc1.Party != actorPc2.Party || actorPc1.Party == null) && actorPc2.PossessionTarget == 0U);
                    case ActorType.MOB:
                        return !((MobEventHandler)dActor.e).AI.Mode.Symbol;
                    case ActorType.ITEM:
                    case ActorType.SKILL:
                        return false;
                    case ActorType.PET:
                        ActorPet actorPet = (ActorPet)dActor;
                        return (actorPc1.Mode == PlayerMode.COLISEUM_MODE && actorPet.Owner.Mode == PlayerMode.COLISEUM_MODE || actorPc1.Mode == PlayerMode.WRP && actorPet.Owner.Mode == PlayerMode.WRP || actorPc1.Mode == PlayerMode.KNIGHT_WAR && actorPet.Owner.Mode == PlayerMode.KNIGHT_WAR) && actorPc1.Party != actorPet.Owner.Party;
                    case ActorType.SHADOW:
                        ActorShadow actorShadow = (ActorShadow)dActor;
                        return (actorPc1.Mode == PlayerMode.COLISEUM_MODE && actorShadow.Owner.Mode == PlayerMode.COLISEUM_MODE || actorPc1.Mode == PlayerMode.WRP && actorShadow.Owner.Mode == PlayerMode.WRP || actorPc1.Mode == PlayerMode.KNIGHT_WAR && actorShadow.Owner.Mode == PlayerMode.KNIGHT_WAR) && actorPc1.Party != actorShadow.Owner.Party;
                }
            }
            else if (sActor.type == ActorType.MOB)
            {
                bool flag = false;
                MobEventHandler e = (MobEventHandler)sActor.e;
                if (e.AI.Master != null && e.AI.Master.type == ActorType.PC)
                    flag = true;
                if (!flag)
                {
                    switch (dActor.type)
                    {
                        case ActorType.PC:
                            return ((ActorPC)dActor).PossessionTarget == 0U;
                        case ActorType.MOB:
                            return ((MobEventHandler)dActor.e).AI.Mode.Symbol;
                        case ActorType.PET:
                        case ActorType.SHADOW:
                            return true;
                        default:
                            return false;
                    }
                }
                else
                    return dActor.type == ActorType.MOB;
            }
            else if (sActor.type == ActorType.PET)
            {
                switch (dActor.type)
                {
                    case ActorType.PC:
                    case ActorType.PET:
                        return false;
                    case ActorType.MOB:
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this.skillHandlers.Add(100U, (ISkill)new MaxHPUp());
            this.skillHandlers.Add(101U, (ISkill)new MaxMPUp());
            this.skillHandlers.Add(102U, (ISkill)new MaxSPUp());
            this.skillHandlers.Add(103U, (ISkill)new HPRecoverUP());
            this.skillHandlers.Add(104U, (ISkill)new MPRecoverUP());
            this.skillHandlers.Add(105U, (ISkill)new SPRecoverUP());
            this.skillHandlers.Add(107U, (ISkill)new SwordMastery());
            this.skillHandlers.Add(108U, (ISkill)new AxeMastery());
            this.skillHandlers.Add(109U, (ISkill)new SpearMastery());
            this.skillHandlers.Add(110U, (ISkill)new ShortSwordMastery());
            this.skillHandlers.Add(111U, (ISkill)new MaceMastery());
            this.skillHandlers.Add(112U, (ISkill)new ThrowMastery());
            this.skillHandlers.Add(119U, (ISkill)new TwoHandMastery());
            this.skillHandlers.Add(120U, (ISkill)new TwoSpearMastery());
            this.skillHandlers.Add(121U, (ISkill)new TwoMaceMastery());
            this.skillHandlers.Add(122U, (ISkill)new TwoAxeMastery());
            this.skillHandlers.Add(123U, (ISkill)new RapierMastery());
            this.skillHandlers.Add(128U, (ISkill)new TwoHandGunMastery());
            this.skillHandlers.Add(200U, (ISkill)new SwordHitUP());
            this.skillHandlers.Add(202U, (ISkill)new SpearHitUP());
            this.skillHandlers.Add(204U, (ISkill)new AxeHitUP());
            this.skillHandlers.Add(206U, (ISkill)new ShortSwordHitUP());
            this.skillHandlers.Add(208U, (ISkill)new MaceHitUP());
            this.skillHandlers.Add(113U, (ISkill)new BowMastery());
            this.skillHandlers.Add(903U, (ISkill)new Identify());
            this.skillHandlers.Add(907U, (ISkill)new ThrowHitUP());
            this.skillHandlers.Add(2021U, (ISkill)new Synthese());
            this.skillHandlers.Add(2035U, (ISkill)new Synthese());
            this.skillHandlers.Add(4026U, (ISkill)new A_T_PJoint());
            this.skillHandlers.Add(978U, (ISkill)new AtkUpByPt());
            this.skillHandlers.Add(959U, (ISkill)new RiskAversion());
            this.skillHandlers.Add(1602U, (ISkill)new EventMoveSkill());
            this.skillHandlers.Add(3250U, (ISkill)new FGRope());
            this.skillHandlers.Add(7500U, (ISkill)new PoisonPerfume());
            this.skillHandlers.Add(7501U, (ISkill)new RockStone());
            this.skillHandlers.Add(7502U, (ISkill)new ParaizBan());
            this.skillHandlers.Add(7503U, (ISkill)new SleepStrike());
            this.skillHandlers.Add(7504U, (ISkill)new SilentGreen());
            this.skillHandlers.Add(7505U, (ISkill)new SlowLogic());
            this.skillHandlers.Add(7506U, (ISkill)new ConfuseStack());
            this.skillHandlers.Add(7507U, (ISkill)new IceFade());
            this.skillHandlers.Add(7508U, (ISkill)new StunAttack());
            this.skillHandlers.Add(7509U, (ISkill)new EnergyAttack());
            this.skillHandlers.Add(7510U, (ISkill)new FireAttack());
            this.skillHandlers.Add(7511U, (ISkill)new WaterAttack());
            this.skillHandlers.Add(7512U, (ISkill)new WindAttack());
            this.skillHandlers.Add(7513U, (ISkill)new EarthAttack());
            this.skillHandlers.Add(7514U, (ISkill)new LightBallad());
            this.skillHandlers.Add(7515U, (ISkill)new DarkBallad());
            this.skillHandlers.Add(7516U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.Blow());
            this.skillHandlers.Add(7517U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.ConfuseBlow());
            this.skillHandlers.Add(7518U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.StunBlow());
            this.skillHandlers.Add(7519U, (ISkill)new MobHealing());
            this.skillHandlers.Add(7520U, (ISkill)new MobHealing1());
            this.skillHandlers.Add(7521U, (ISkill)new MobAshibarai());
            this.skillHandlers.Add(7522U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.Brandish());
            this.skillHandlers.Add(7523U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.Rush());
            this.skillHandlers.Add(7524U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.Iai());
            this.skillHandlers.Add(7525U, (ISkill)new KabutoWari());
            this.skillHandlers.Add(7526U, (ISkill)new MobBokeboke());
            this.skillHandlers.Add(7527U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.ShockWave());
            this.skillHandlers.Add(7528U, (ISkill)new aStormSword(true));
            this.skillHandlers.Add(7529U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.Phalanx());
            this.skillHandlers.Add(7530U, (ISkill)new WarCry());
            this.skillHandlers.Add(7531U, (ISkill)new ExciaMation());
            this.skillHandlers.Add(7532U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.IceArrow());
            this.skillHandlers.Add(7533U, (ISkill)new DarkOne());
            this.skillHandlers.Add(7534U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.WaterStorm());
            this.skillHandlers.Add(7535U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.DarkStorm());
            this.skillHandlers.Add(7536U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.WaterGroove());
            this.skillHandlers.Add(7537U, (ISkill)new MobParalyzeblow());
            this.skillHandlers.Add(7538U, (ISkill)new MobFireart());
            this.skillHandlers.Add(7539U, (ISkill)new MobWaterart());
            this.skillHandlers.Add(7540U, (ISkill)new MobEarthart());
            this.skillHandlers.Add(7541U, (ISkill)new MobWindart());
            this.skillHandlers.Add(7542U, (ISkill)new MobLightart());
            this.skillHandlers.Add(7543U, (ISkill)new MobDarkart());
            this.skillHandlers.Add(7544U, (ISkill)new MobTrSilenceAtk());
            this.skillHandlers.Add(7545U, (ISkill)new MobTrPoisonAtk());
            this.skillHandlers.Add(7546U, (ISkill)new MobTrPoisonCircle());
            this.skillHandlers.Add(7547U, (ISkill)new MobTrStuinCircle());
            this.skillHandlers.Add(7548U, (ISkill)new MobTrSleepCircle());
            this.skillHandlers.Add(7549U, (ISkill)new MobTrSilenceCircle());
            this.skillHandlers.Add(7550U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.MagPoison());
            this.skillHandlers.Add(7551U, (ISkill)new MagSleep());
            this.skillHandlers.Add(7552U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.MagSlow());
            this.skillHandlers.Add(7553U, (ISkill)new StoneCircle());
            this.skillHandlers.Add(7554U, (ISkill)new HiPoisonCircie());
            this.skillHandlers.Add(7555U, (ISkill)new IceCircle());
            this.skillHandlers.Add(7556U, (ISkill)new HiPoisonCircie());
            this.skillHandlers.Add(7557U, (ISkill)new DeadlyPoison());
            this.skillHandlers.Add(7558U, (ISkill)new MobPerfectcritical());
            this.skillHandlers.Add(7559U, (ISkill)new SumSlaveMob(10010100U));
            this.skillHandlers.Add(7560U, (ISkill)new SumSlaveMob(26000000U));
            this.skillHandlers.Add(7561U, (ISkill)new SumSlaveMob(10080100U));
            this.skillHandlers.Add(7562U, (ISkill)new SumSlaveMob(10040100U));
            this.skillHandlers.Add(7563U, (ISkill)new SumSlaveMob(10030400U));
            this.skillHandlers.Add(7564U, (ISkill)new FireHighStorm());
            this.skillHandlers.Add(7565U, (ISkill)new WindHighWave());
            this.skillHandlers.Add(7566U, (ISkill)new WindHighStorm());
            this.skillHandlers.Add(7567U, (ISkill)new FireOne());
            this.skillHandlers.Add(7568U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.FireStorm());
            this.skillHandlers.Add(7569U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.WindStorm());
            this.skillHandlers.Add(7570U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.EarthStorm());
            this.skillHandlers.Add(7571U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.LightOne());
            this.skillHandlers.Add(7572U, (ISkill)new DarkHighOne());
            this.skillHandlers.Add(7573U, (ISkill)new PoisonMash(true));
            this.skillHandlers.Add(7574U, (ISkill)new MobAvoupSelf());
            this.skillHandlers.Add(7575U, (ISkill)new EnergyShield(true));
            this.skillHandlers.Add(7576U, (ISkill)new MagicShield(true));
            this.skillHandlers.Add(7577U, (ISkill)new MobAtkupOne());
            this.skillHandlers.Add(7578U, (ISkill)new MobCharge());
            this.skillHandlers.Add(7579U, (ISkill)new SumSlaveMob(10030903U));
            this.skillHandlers.Add(7580U, (ISkill)new SumSlaveMob(26180002U));
            this.skillHandlers.Add(7581U, (ISkill)new SumSlaveMob(26100003U));
            this.skillHandlers.Add(7582U, (ISkill)new MobTrSleep());
            this.skillHandlers.Add(7583U, (ISkill)new MobTrStun());
            this.skillHandlers.Add(7584U, (ISkill)new MobTrSilence());
            this.skillHandlers.Add(7585U, (ISkill)new MobConfPoisonCircle());
            this.skillHandlers.Add(7586U, (ISkill)new ElementCircle(Elements.Fire, true));
            this.skillHandlers.Add(7587U, (ISkill)new ElementCircle(Elements.Wind, true));
            this.skillHandlers.Add(7588U, (ISkill)new ElementCircle(Elements.Water, true));
            this.skillHandlers.Add(7589U, (ISkill)new ElementCircle(Elements.Earth, true));
            this.skillHandlers.Add(7590U, (ISkill)new ElementCircle(Elements.Holy, true));
            this.skillHandlers.Add(7591U, (ISkill)new ElementCircle(Elements.Dark, true));
            this.skillHandlers.Add(7592U, (ISkill)new SumSlaveMob(26180000U));
            this.skillHandlers.Add(7593U, (ISkill)new SumSlaveMob(26100000U));
            this.skillHandlers.Add(7594U, (ISkill)new SumSlaveMob(10030900U));
            this.skillHandlers.Add(7595U, (ISkill)new SumSlaveMob(10310006U));
            this.skillHandlers.Add(7596U, (ISkill)new SumSlaveMob(10250003U));
            this.skillHandlers.Add(7597U, (ISkill)new SumSlaveMob(30490000U, 1));
            this.skillHandlers.Add(7598U, (ISkill)new SumSlaveMob(30500000U, 1));
            this.skillHandlers.Add(7599U, (ISkill)new SumSlaveMob(30510000U, 1));
            this.skillHandlers.Add(7600U, (ISkill)new SumSlaveMob(30520000U, 1));
            this.skillHandlers.Add(7601U, (ISkill)new SumSlaveMob(30530000U, 1));
            this.skillHandlers.Add(7605U, (ISkill)new SumSlaveMob(30150005U, 4));
            this.skillHandlers.Add(7606U, (ISkill)new MobMeteo());
            this.skillHandlers.Add(7607U, (ISkill)new MobDoughnutFireWall());
            this.skillHandlers.Add(7608U, (ISkill)new MobReflection());
            this.skillHandlers.Add(7609U, (ISkill)new MobElementLoad(7664U));
            this.skillHandlers.Add(7610U, (ISkill)new MobElementLoad(7665U));
            this.skillHandlers.Add(7611U, (ISkill)new MobElementLoad(7666U));
            this.skillHandlers.Add(7612U, (ISkill)new MobElementLoad(7667U));
            this.skillHandlers.Add(7613U, (ISkill)new MobElementLoad(7668U));
            this.skillHandlers.Add(7614U, (ISkill)new MobElementRandcircle(7669U, 5));
            this.skillHandlers.Add(7615U, (ISkill)new MobElementRandcircle(7670U, 5));
            this.skillHandlers.Add(7616U, (ISkill)new MobElementRandcircle(7671U, 5));
            this.skillHandlers.Add(7617U, (ISkill)new MobElementRandcircle(7672U, 5));
            this.skillHandlers.Add(7618U, (ISkill)new MobElementRandcircle(7673U, 5));
            this.skillHandlers.Add(7619U, (ISkill)new MobElementRandcircle(7674U, 3));
            this.skillHandlers.Add(7620U, (ISkill)new MobElementRandcircle(7675U, 3));
            this.skillHandlers.Add(7621U, (ISkill)new MobElementRandcircle(7676U, 3));
            this.skillHandlers.Add(7622U, (ISkill)new MobElementRandcircle(7677U, 3));
            this.skillHandlers.Add(7623U, (ISkill)new MobElementRandcircle(7678U, 3));
            this.skillHandlers.Add(7648U, (ISkill)new MobVitdownOne());
            this.skillHandlers.Add(7649U, (ISkill)new MobCircleAtkup());
            this.skillHandlers.Add(7650U, (ISkill)new GravityFall(true));
            this.skillHandlers.Add(7651U, (ISkill)new SagaMap.Skill.SkillDefinations.Sage.AReflection());
            this.skillHandlers.Add(7652U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.DelayCancel());
            this.skillHandlers.Add(7653U, (ISkill)new MobCharge3());
            this.skillHandlers.Add(7654U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30150007U));
            this.skillHandlers.Add(7655U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30130003U));
            this.skillHandlers.Add(7656U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30130005U));
            this.skillHandlers.Add(7657U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30130007U));
            this.skillHandlers.Add(7658U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30070052U));
            this.skillHandlers.Add(7659U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30070054U));
            this.skillHandlers.Add(7660U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30070056U));
            this.skillHandlers.Add(7661U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30070058U));
            this.skillHandlers.Add(7662U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30070060U));
            this.skillHandlers.Add(7663U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30070062U));
            this.skillHandlers.Add(7664U, (ISkill)new MobElementLoadSeq(Elements.Fire));
            this.skillHandlers.Add(7665U, (ISkill)new MobElementLoadSeq(Elements.Water));
            this.skillHandlers.Add(7666U, (ISkill)new MobElementLoadSeq(Elements.Wind));
            this.skillHandlers.Add(7667U, (ISkill)new MobElementLoadSeq(Elements.Earth));
            this.skillHandlers.Add(7668U, (ISkill)new MobElementLoadSeq(Elements.Dark));
            this.skillHandlers.Add(7669U, (ISkill)new MobElementRandcircleSeq(Elements.Fire));
            this.skillHandlers.Add(7670U, (ISkill)new MobElementRandcircleSeq(Elements.Water));
            this.skillHandlers.Add(7671U, (ISkill)new MobElementRandcircleSeq(Elements.Wind));
            this.skillHandlers.Add(7672U, (ISkill)new MobElementRandcircleSeq(Elements.Earth));
            this.skillHandlers.Add(7673U, (ISkill)new MobElementRandcircleSeq(Elements.Dark));
            this.skillHandlers.Add(7674U, (ISkill)new MobElementRandcircleSeq(Elements.Fire));
            this.skillHandlers.Add(7675U, (ISkill)new MobElementRandcircleSeq(Elements.Water));
            this.skillHandlers.Add(7676U, (ISkill)new MobElementRandcircleSeq(Elements.Wind));
            this.skillHandlers.Add(7677U, (ISkill)new MobElementRandcircleSeq(Elements.Earth));
            this.skillHandlers.Add(7678U, (ISkill)new MobElementRandcircleSeq(Elements.Dark));
            this.skillHandlers.Add(7679U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.FireArrow());
            this.skillHandlers.Add(7680U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.WaterArrow());
            this.skillHandlers.Add(7681U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.EarthArrow());
            this.skillHandlers.Add(7682U, (ISkill)new SagaMap.Skill.SkillDefinations.Monster.WindArrow());
            this.skillHandlers.Add(7683U, (ISkill)new LightArrow());
            this.skillHandlers.Add(7684U, (ISkill)new DarkArrow());
            this.skillHandlers.Add(7685U, (ISkill)new MobConArrow());
            this.skillHandlers.Add(7686U, (ISkill)new MobChargeArrow());
            this.skillHandlers.Add(7687U, (ISkill)new SumSlaveMob(26050003U));
            this.skillHandlers.Add(7688U, (ISkill)new MobTrDrop());
            this.skillHandlers.Add(7689U, (ISkill)new MobConfCircle());
            this.skillHandlers.Add(7690U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(90010000U));
            this.skillHandlers.Add(7691U, (ISkill)new MobMedic());
            this.skillHandlers.Add(7692U, (ISkill)new MobWindHighStorm2());
            this.skillHandlers.Add(7693U, (ISkill)new MobElementLoad(7694U));
            this.skillHandlers.Add(7694U, (ISkill)new MobWindHighStorm2());
            this.skillHandlers.Add(7695U, (ISkill)new MobElementRandcircle(7696U, 5));
            this.skillHandlers.Add(7696U, (ISkill)new MobWindRandcircleSeq2());
            this.skillHandlers.Add(7697U, (ISkill)new MobElementRandcircle(7698U, 5));
            this.skillHandlers.Add(7698U, (ISkill)new MobWindCrosscircleSeq2());
            this.skillHandlers.Add(7706U, (ISkill)new SumSlaveMob(10136901U));
            this.skillHandlers.Add(7709U, (ISkill)new MobComaStun());
            this.skillHandlers.Add(7710U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(90010000U));
            this.skillHandlers.Add(7711U, (ISkill)new MobSelfDarkHighStorm());
            this.skillHandlers.Add(7712U, (ISkill)new SagaMap.Skill.SkillDefinations.Cabalist.DarkStorm(true));
            this.skillHandlers.Add(7713U, (ISkill)new MobSelfMagStun());
            this.skillHandlers.Add(7714U, (ISkill)new TrDrop2(true));
            this.skillHandlers.Add(7715U, (ISkill)new MobHpPerDown());
            this.skillHandlers.Add(7716U, (ISkill)new MobDropOut());
            this.skillHandlers.Add(7717U, (ISkill)new SumSlaveMob(26180000U));
            this.skillHandlers.Add(7719U, (ISkill)new MobTalkSnmnpapa());
            this.skillHandlers.Add(7720U, (ISkill)new SumSlaveMob(10120100U));
            this.skillHandlers.Add(7721U, (ISkill)new SumSlaveMob(10210002U));
            this.skillHandlers.Add(7722U, (ISkill)new SumSlaveMob(10431000U));
            this.skillHandlers.Add(7723U, (ISkill)new SumSlaveMob(10680900U));
            this.skillHandlers.Add(7724U, (ISkill)new SumSlaveMob(10251000U));
            this.skillHandlers.Add(7725U, (ISkill)new SumSlaveMob(10000006U));
            this.skillHandlers.Add(7726U, (ISkill)new SumSlaveMob(10001005U));
            this.skillHandlers.Add(7727U, (ISkill)new SumSlaveMob(10211400U));
            this.skillHandlers.Add(7728U, (ISkill)new ThunderBall(true));
            this.skillHandlers.Add(7729U, (ISkill)new EarthBlast(true));
            this.skillHandlers.Add(7730U, (ISkill)new FireBolt(true));
            this.skillHandlers.Add(7731U, (ISkill)new StoneSkin(true));
            this.skillHandlers.Add(7732U, (ISkill)new MobCancelChgstateAll());
            this.skillHandlers.Add(7733U, (ISkill)new SumSlaveMob(10580500U, 4));
            this.skillHandlers.Add(7734U, (ISkill)new MobCircleSelfAtk());
            this.skillHandlers.Add(7735U, (ISkill)new SumSlaveMob(10431400U, 1));
            this.skillHandlers.Add(7736U, (ISkill)new SumSlaveMob(10030901U, 1));
            this.skillHandlers.Add(7737U, (ISkill)new MobCircleSelfAtk());
            this.skillHandlers.Add(7738U, (ISkill)new EnergyShock(true));
            this.skillHandlers.Add(7739U, (ISkill)new Concentricity(true));
            this.skillHandlers.Add(7740U, (ISkill)new MobComboConShot());
            this.skillHandlers.Add(7741U, (ISkill)new MobConShot());
            this.skillHandlers.Add(7742U, (ISkill)new MobComboConAtk());
            this.skillHandlers.Add(7743U, (ISkill)new MobConAtk());
            this.skillHandlers.Add(7744U, (ISkill)new SumSlaveMob(10580500U));
            this.skillHandlers.Add(7745U, (ISkill)new AcidMist());
            this.skillHandlers.Add(7746U, (ISkill)new MobEarthDurable());
            this.skillHandlers.Add(7747U, (ISkill)new LifeSteal(true));
            this.skillHandlers.Add(7748U, (ISkill)new MobChargeCircle());
            this.skillHandlers.Add(7749U, (ISkill)new PetPlantPoison(true));
            this.skillHandlers.Add(7750U, (ISkill)new MobStrVitAgiDownOne());
            this.skillHandlers.Add(7751U, (ISkill)new MobAtkupSelf());
            this.skillHandlers.Add(7752U, (ISkill)new MobDefUpSelf(true));
            this.skillHandlers.Add(7753U, (ISkill)new SagaMap.Skill.SkillDefinations.Sorcerer.Kyrie(true));
            this.skillHandlers.Add(7754U, (ISkill)new MobHolyfeather());
            this.skillHandlers.Add(7755U, (ISkill)new MobSalvoFire());
            this.skillHandlers.Add(7756U, (ISkill)new MobAmobm());
            this.skillHandlers.Add(7757U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(90010001U));
            this.skillHandlers.Add(7758U, (ISkill)new EnergyStorm(true));
            this.skillHandlers.Add(7759U, (ISkill)new EnergyBlast(true));
            this.skillHandlers.Add(7760U, (ISkill)new SumSlaveMob(10990000U));
            this.skillHandlers.Add(7761U, (ISkill)new SumSlaveMob(10960000U));
            this.skillHandlers.Add(7766U, (ISkill)new SumSlaveMob(19070500U));
            this.skillHandlers.Add(7798U, (ISkill)new Caputrue());
            this.skillHandlers.Add(7805U, (ISkill)new SumSlaveMob(14160500U));
            this.skillHandlers.Add(7806U, (ISkill)new SumSlaveMob(14160000U));
            this.skillHandlers.Add(7807U, (ISkill)new SumSlaveMob(10060600U));
            this.skillHandlers.Add(7808U, (ISkill)new SumSlaveMob(10060200U));
            this.skillHandlers.Add(7810U, (ISkill)new Abusoryutoteritori());
            this.skillHandlers.Add(7811U, (ISkill)new SumSlaveMob(14110003U, 8));
            this.skillHandlers.Add(7813U, (ISkill)new CanisterShot(true));
            this.skillHandlers.Add(7818U, (ISkill)new SumSlaveMob(14320200U));
            this.skillHandlers.Add(7819U, (ISkill)new SumSlaveMob(14330500U));
            this.skillHandlers.Add(7822U, (ISkill)new IsSeN(true));
            this.skillHandlers.Add(7830U, (ISkill)new SumSlaveMob(14560900U));
            this.skillHandlers.Add(7831U, (ISkill)new Animadorein());
            this.skillHandlers.Add(8500U, (ISkill)new MobHpHeal());
            this.skillHandlers.Add(8501U, (ISkill)new MobBerserk());
            this.skillHandlers.Add(9000U, (ISkill)new CswarSleep(true));
            this.skillHandlers.Add(9001U, (ISkill)new CswarSleep(true));
            this.skillHandlers.Add(9002U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(26160003U));
            this.skillHandlers.Add(9004U, (ISkill)new SagaMap.Skill.SkillDefinations.Druid.AreaHeal(true));
            this.skillHandlers.Add(9106U, (ISkill)new EventSelfDarkStorm(true));
            this.skillHandlers.Add(5008U, (ISkill)new SagaMap.Skill.SkillDefinations.Marionette.HPRecovery());
            this.skillHandlers.Add(5009U, (ISkill)new SagaMap.Skill.SkillDefinations.Marionette.SPRecovery());
            this.skillHandlers.Add(5010U, (ISkill)new SagaMap.Skill.SkillDefinations.Marionette.MPRecovery());
            this.skillHandlers.Add(5507U, (ISkill)new MExclamation());
            this.skillHandlers.Add(5513U, (ISkill)new MBokeboke());
            this.skillHandlers.Add(5515U, (ISkill)new MMirror());
            this.skillHandlers.Add(5516U, (ISkill)new MMirrorSkill());
            this.skillHandlers.Add(5522U, (ISkill)new MDarkCrosscircle());
            this.skillHandlers.Add(5523U, (ISkill)new MDarkCrosscircleSeq());
            this.skillHandlers.Add(5524U, (ISkill)new MCharge3());
            this.skillHandlers.Add(1500U, (ISkill)new WeaCreUp());
            this.skillHandlers.Add(1501U, (ISkill)new HitUpRateDown());
            this.skillHandlers.Add(1603U, (ISkill)new Ryuugankaihou());
            this.skillHandlers.Add(1604U, (ISkill)new RyuugankaihouShin());
            this.skillHandlers.Add(2457U, (ISkill)new SymbolRepair());
            this.skillHandlers.Add(3269U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.ChgTrance());
            this.skillHandlers.Add(6415U, (ISkill)new MoveUp2());
            this.skillHandlers.Add(6428U, (ISkill)new MoveUp3());
            this.skillHandlers.Add(9100U, (ISkill)new MiniMum());
            this.skillHandlers.Add(9101U, (ISkill)new MaxiMum());
            this.skillHandlers.Add(9102U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9103U, (ISkill)new Invisible());
            this.skillHandlers.Add(9105U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9108U, (ISkill)new Dango());
            this.skillHandlers.Add(9109U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9114U, (ISkill)new Invisible());
            this.skillHandlers.Add(9117U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.ExpUp());
            this.skillHandlers.Add(9126U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9127U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9128U, (ISkill)new Invisible());
            this.skillHandlers.Add(9129U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(19010001U, 9130U));
            this.skillHandlers.Add(9130U, (ISkill)new HpRecoveryMax());
            this.skillHandlers.Add(9131U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(19010002U, 9132U));
            this.skillHandlers.Add(9132U, (ISkill)new HpRecoveryMax());
            this.skillHandlers.Add(9133U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(19010003U, 9134U));
            this.skillHandlers.Add(9134U, (ISkill)new HpRecoveryMax());
            this.skillHandlers.Add(9140U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(19010008U, 9143U, 50, 9146U, 50));
            this.skillHandlers.Add(9141U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(19010009U, 9144U, 50, 9147U, 50));
            this.skillHandlers.Add(9142U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(19010010U, 9145U, 50, 9148U, 50));
            this.skillHandlers.Add(9143U, (ISkill)new DefMdefUp());
            this.skillHandlers.Add(9144U, (ISkill)new DefMdefUp());
            this.skillHandlers.Add(9145U, (ISkill)new DefMdefUp());
            this.skillHandlers.Add(9146U, (ISkill)new DefMdefUp());
            this.skillHandlers.Add(9147U, (ISkill)new DefMdefUp());
            this.skillHandlers.Add(9148U, (ISkill)new DefMdefUp());
            this.skillHandlers.Add(9139U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9151U, (ISkill)new ILoveYou());
            this.skillHandlers.Add(9152U, (ISkill)new HpRecoveryMax());
            this.skillHandlers.Add(9153U, (ISkill)new HpRecoveryMax());
            this.skillHandlers.Add(9154U, (ISkill)new HpRecoveryMax());
            this.skillHandlers.Add(9155U, (ISkill)new SagaMap.Skill.SkillDefinations.Vates.Healing());
            this.skillHandlers.Add(9157U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9162U, (ISkill)new SagaMap.Skill.SkillDefinations.Vates.Healing());
            this.skillHandlers.Add(9163U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9174U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9178U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9182U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9185U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(9190U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMob(30740000U));
            this.skillHandlers.Add(9191U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(30750000U, 9192U, 90, 9193U, 10));
            this.skillHandlers.Add(9192U, (ISkill)new WeepingWillow1());
            this.skillHandlers.Add(9193U, (ISkill)new WeepingWillow2());
            this.skillHandlers.Add(9197U, (ISkill)new Invisible());
            this.skillHandlers.Add(9208U, (ISkill)new SagaMap.Skill.SkillDefinations.Global.SumMobCastSkill(19010028U, 9209U));
            this.skillHandlers.Add(9209U, (ISkill)new HpRecoveryMax());
            this.skillHandlers.Add(9219U, (ISkill)new MoveUp4());
            this.skillHandlers.Add(9220U, (ISkill)new RiceSeed());
            this.skillHandlers.Add(9221U, (ISkill)new PanTick());
            this.skillHandlers.Add(9223U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.Gravity());
            this.skillHandlers.Add(9224U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.Kyrie());
            this.skillHandlers.Add(9225U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.AReflection());
            this.skillHandlers.Add(9226U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.STR_VIT_AGI_UP());
            this.skillHandlers.Add(9227U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.MAG_INT_DEX_UP());
            this.skillHandlers.Add(9228U, (ISkill)new SagaMap.Skill.SkillDefinations.Event.AreaHeal());
            this.skillHandlers.Add(10500U, (ISkill)new HerosProtection());
            this.skillHandlers.Add(2000U, (ISkill)new HitMeleeUp());
            this.skillHandlers.Add(2002U, (ISkill)new ATKUp());
            this.skillHandlers.Add(2005U, (ISkill)new SwordCancel());
            this.skillHandlers.Add(2100U, (ISkill)new Parry());
            this.skillHandlers.Add(2101U, (ISkill)new Counter());
            this.skillHandlers.Add(2102U, (ISkill)new Feint());
            this.skillHandlers.Add(2107U, (ISkill)new Provocation());
            this.skillHandlers.Add(2110U, (ISkill)new SagaMap.Skill.SkillDefinations.Swordman.Blow());
            this.skillHandlers.Add(2111U, (ISkill)new BanishBlow());
            this.skillHandlers.Add(2112U, (ISkill)new SagaMap.Skill.SkillDefinations.Swordman.ConfuseBlow());
            this.skillHandlers.Add(2113U, (ISkill)new SagaMap.Skill.SkillDefinations.Swordman.StunBlow());
            this.skillHandlers.Add(2114U, (ISkill)new SlowBlow());
            this.skillHandlers.Add(2115U, (ISkill)new SagaMap.Skill.SkillDefinations.Swordman.Iai());
            this.skillHandlers.Add(2120U, (ISkill)new Charge());
            this.skillHandlers.Add(2201U, (ISkill)new Iai2());
            this.skillHandlers.Add(2202U, (ISkill)new Iai3());
            this.skillHandlers.Add(2117U, (ISkill)new CutDown());
            this.skillHandlers.Add(2124U, (ISkill)new Sinkuha());
            this.skillHandlers.Add(2134U, (ISkill)new aEarthAngry());
            this.skillHandlers.Add(2231U, (ISkill)new aWoodHack());
            this.skillHandlers.Add(2116U, (ISkill)new aStormSword());
            this.skillHandlers.Add(2232U, (ISkill)new aFalconLongSword());
            this.skillHandlers.Add(2233U, (ISkill)new aMistBehead());
            this.skillHandlers.Add(2234U, (ISkill)new aAnimalCrushing());
            this.skillHandlers.Add(2235U, (ISkill)new aHundredSpriteCry());
            this.skillHandlers.Add(700U, (ISkill)new P_BERSERK());
            this.skillHandlers.Add(2239U, (ISkill)new Transporter());
            this.skillHandlers.Add(2066U, (ISkill)new PetMeditatioon());
            this.skillHandlers.Add(2109U, (ISkill)new PetWarCry());
            this.skillHandlers.Add(2236U, (ISkill)new AtkFly());
            this.skillHandlers.Add(2237U, (ISkill)new SwordEaseSp());
            this.skillHandlers.Add(2238U, (ISkill)new EaseCt());
            this.skillHandlers.Add(2379U, (ISkill)new DoubleCutDown());
            this.skillHandlers.Add(2380U, (ISkill)new DoubleCutDownSeq());
            this.skillHandlers.Add(2272U, (ISkill)new ArmSlash());
            this.skillHandlers.Add(2271U, (ISkill)new BodySlash());
            this.skillHandlers.Add(2273U, (ISkill)new ChestSlash());
            this.skillHandlers.Add(2122U, (ISkill)new MiNeUChi());
            this.skillHandlers.Add(2274U, (ISkill)new ShieldSlash());
            this.skillHandlers.Add(2269U, (ISkill)new DefUpAvoDown());
            this.skillHandlers.Add(2268U, (ISkill)new AtkUpHitDown());
            this.skillHandlers.Add(2352U, (ISkill)new BeatUp());
            this.skillHandlers.Add(2401U, (ISkill)new MuSoU());
            this.skillHandlers.Add(2396U, (ISkill)new SordAssail());
            this.skillHandlers.Add(2136U, (ISkill)new AShiBaRaI());
            this.skillHandlers.Add(956U, (ISkill)new ConSword());
            this.skillHandlers.Add(2354U, (ISkill)new SagaMap.Skill.SkillDefinations.BountyHunter.Gravity());
            this.skillHandlers.Add(400U, (ISkill)new SomeKindDamUp("HumDamUp", new MobType[5]
            {
        MobType.HUMAN,
        MobType.HUMAN_BOSS,
        MobType.HUMAN_BOSS_SKILL,
        MobType.HUMAN_RIDE,
        MobType.HUMAN_SKILL
            }));
            this.skillHandlers.Add(2275U, (ISkill)new PartsSlash());
            this.skillHandlers.Add(2355U, (ISkill)new StrikeBlow());
            this.skillHandlers.Add(2353U, (ISkill)new SolidBody());
            this.skillHandlers.Add(2400U, (ISkill)new IsSeN());
            this.skillHandlers.Add(2179U, (ISkill)new EdgedSlash());
            this.skillHandlers.Add(2270U, (ISkill)new ComboIai());
            this.skillHandlers.Add(2001U, (ISkill)new CriUp());
            this.skillHandlers.Add(2004U, (ISkill)new AvoidMeleeUp());
            this.skillHandlers.Add(2008U, (ISkill)new ShortSwordCancel());
            this.skillHandlers.Add(2042U, (ISkill)new Hiding());
            this.skillHandlers.Add(2119U, (ISkill)new SagaMap.Skill.SkillDefinations.Scout.Brandish());
            this.skillHandlers.Add(2126U, (ISkill)new VitalAttack());
            this.skillHandlers.Add(2127U, (ISkill)new ConThrow());
            this.skillHandlers.Add(2139U, (ISkill)new ConThrust());
            this.skillHandlers.Add(2143U, (ISkill)new SummerSaltKick());
            this.skillHandlers.Add(2133U, (ISkill)new WalkAround());
            this.skillHandlers.Add(2045U, (ISkill)new PoisonReate());
            this.skillHandlers.Add(2046U, (ISkill)new PosionReate2());
            this.skillHandlers.Add(2047U, (ISkill)new PoisonReate3());
            this.skillHandlers.Add(2069U, (ISkill)new Concentricity());
            this.skillHandlers.Add(947U, (ISkill)new ConClaw());
            this.skillHandlers.Add(2062U, (ISkill)new WillPower());
            this.skillHandlers.Add(2140U, (ISkill)new PosionNeedle());
            this.skillHandlers.Add(977U, (ISkill)new AvoidUp());
            this.skillHandlers.Add(312U, (ISkill)new CriDamUp());
            this.skillHandlers.Add(118U, (ISkill)new ClawMastery());
            this.skillHandlers.Add(908U, (ISkill)new ThrowRangeUp());
            this.skillHandlers.Add(910U, (ISkill)new PoisonRegiUp());
            this.skillHandlers.Add(2250U, (ISkill)new UTuSeMi());
            this.skillHandlers.Add(2068U, (ISkill)new BackAtk());
            this.skillHandlers.Add(2044U, (ISkill)new AppliePoison());
            this.skillHandlers.Add(2142U, (ISkill)new ScatterPoison());
            this.skillHandlers.Add(920U, (ISkill)new PoisonRateUp());
            this.skillHandlers.Add(2251U, (ISkill)new EventSumNinJa());
            this.skillHandlers.Add(2384U, (ISkill)new Raid());
            this.skillHandlers.Add(2043U, (ISkill)new Cloaking());
            this.skillHandlers.Add((uint)sbyte.MaxValue, (ISkill)new HandGunDamUp());
            this.skillHandlers.Add(2137U, (ISkill)new Tackle());
            this.skillHandlers.Add(125U, (ISkill)new MartialArtDamUp());
            this.skillHandlers.Add(2141U, (ISkill)new SagaMap.Skill.SkillDefinations.Command.Rush());
            this.skillHandlers.Add(2282U, (ISkill)new FlashHandGrenade());
            this.skillHandlers.Add(2362U, (ISkill)new SetBomb());
            this.skillHandlers.Add(2378U, (ISkill)new SetBomb2());
            this.skillHandlers.Add(2359U, (ISkill)new UpperCut());
            this.skillHandlers.Add(2360U, (ISkill)new CyclOne());
            this.skillHandlers.Add(2413U, (ISkill)new WildDance());
            this.skillHandlers.Add(2414U, (ISkill)new WildDance2());
            this.skillHandlers.Add(3095U, (ISkill)new LandTrap());
            this.skillHandlers.Add(2280U, (ISkill)new FullNelson());
            this.skillHandlers.Add(2281U, (ISkill)new Combination());
            this.skillHandlers.Add(3131U, (ISkill)new ChokingGas());
            this.skillHandlers.Add(2283U, (ISkill)new Sliding());
            this.skillHandlers.Add(2284U, (ISkill)new ClayMore());
            this.skillHandlers.Add(2361U, (ISkill)new SealHMSp());
            this.skillHandlers.Add(2409U, (ISkill)new RushBom());
            this.skillHandlers.Add(2410U, (ISkill)new RushBomSeq());
            this.skillHandlers.Add(2411U, (ISkill)new RushBomSeq2());
            this.skillHandlers.Add(2408U, (ISkill)new SumCommand());
            this.skillHandlers.Add(401U, (ISkill)new HumHitUp());
            this.skillHandlers.Add(402U, (ISkill)new HumAvoUp());
            this.skillHandlers.Add(3001U, (ISkill)new EnergyOne());
            this.skillHandlers.Add(3002U, (ISkill)new EnergyGroove());
            this.skillHandlers.Add(3005U, (ISkill)new Decoy());
            this.skillHandlers.Add(3113U, (ISkill)new EnergyShield());
            this.skillHandlers.Add(3279U, (ISkill)new EnergyShield());
            this.skillHandlers.Add(3114U, (ISkill)new MagicShield());
            this.skillHandlers.Add(3280U, (ISkill)new MagicShield());
            this.skillHandlers.Add(3123U, (ISkill)new EnergyShock());
            this.skillHandlers.Add(3125U, (ISkill)new DancingSword());
            this.skillHandlers.Add(3124U, (ISkill)new EnergySpear());
            this.skillHandlers.Add(3127U, (ISkill)new EnergyBlast());
            this.skillHandlers.Add(3135U, (ISkill)new SagaMap.Skill.SkillDefinations.Wizard.MagPoison());
            this.skillHandlers.Add(3136U, (ISkill)new MagStone());
            this.skillHandlers.Add(3139U, (ISkill)new MagSilence());
            this.skillHandlers.Add(801U, (ISkill)new MaGaNiInfo());
            this.skillHandlers.Add(3101U, (ISkill)new Heating());
            this.skillHandlers.Add(3281U, (ISkill)new MobEnergyShock());
            this.skillHandlers.Add(3100U, (ISkill)new IntenseHeatSheld());
            this.skillHandlers.Add(3033U, (ISkill)new Aqualung());
            this.skillHandlers.Add(3003U, (ISkill)new EnergyWall());
            this.skillHandlers.Add(503U, (ISkill)new ManDamUp());
            this.skillHandlers.Add(504U, (ISkill)new ManHitUp());
            this.skillHandlers.Add(505U, (ISkill)new ManAvoUp());
            this.skillHandlers.Add(3126U, (ISkill)new RavingSword());
            this.skillHandlers.Add(3300U, (ISkill)new DevineBarrier());
            this.skillHandlers.Add(3253U, (ISkill)new Teleport());
            this.skillHandlers.Add(3097U, (ISkill)new Invisible());
            this.skillHandlers.Add(3275U, (ISkill)new EnergyBarrier());
            this.skillHandlers.Add(3276U, (ISkill)new MagicBarrier());
            this.skillHandlers.Add(3256U, (ISkill)new MagIntDexDownOne());
            this.skillHandlers.Add(3255U, (ISkill)new StrVitAgiDownOne());
            this.skillHandlers.Add(3298U, (ISkill)new EnergyBarn());
            this.skillHandlers.Add(3299U, (ISkill)new EnergyBarnSEQ());
            this.skillHandlers.Add(3158U, (ISkill)new Desist());
            this.skillHandlers.Add(3254U, (ISkill)new SagaMap.Skill.SkillDefinations.Sorcerer.Kyrie());
            this.skillHandlers.Add(2208U, (ISkill)new MaganiAnalysis());
            this.skillHandlers.Add(3171U, (ISkill)new MobInvisibleBreak());
            this.skillHandlers.Add(3004U, (ISkill)new WallSweep());
            this.skillHandlers.Add(3094U, (ISkill)new HexaGram());
            this.skillHandlers.Add(2252U, (ISkill)new SagaMap.Skill.SkillDefinations.Sorcerer.DelayCancel());
            this.skillHandlers.Add(3054U, (ISkill)new SagaMap.Skill.SkillDefinations.Vates.Healing());
            this.skillHandlers.Add(3055U, (ISkill)new Resurrection());
            this.skillHandlers.Add(3073U, (ISkill)new SagaMap.Skill.SkillDefinations.Vates.LightOne());
            this.skillHandlers.Add(3075U, (ISkill)new HolyWeapon());
            this.skillHandlers.Add(3076U, (ISkill)new HolyShield());
            this.skillHandlers.Add(3082U, (ISkill)new HolyGroove());
            this.skillHandlers.Add(3066U, (ISkill)new CureStatus("Sleep"));
            this.skillHandlers.Add(3060U, (ISkill)new CureStatus("Poison"));
            this.skillHandlers.Add(3058U, (ISkill)new CureStatus("Stun"));
            this.skillHandlers.Add(3062U, (ISkill)new CureStatus("Silence"));
            this.skillHandlers.Add(3150U, (ISkill)new CureStatus("Stone"));
            this.skillHandlers.Add(3064U, (ISkill)new CureStatus("Confuse"));
            this.skillHandlers.Add(3152U, (ISkill)new CureStatus("鈍足"));
            this.skillHandlers.Add(3154U, (ISkill)new CureStatus("Frosen"));
            this.skillHandlers.Add(803U, (ISkill)new UndeadInfo());
            this.skillHandlers.Add(3065U, (ISkill)new StatusRegi("Sleep"));
            this.skillHandlers.Add(3059U, (ISkill)new StatusRegi("Poison"));
            this.skillHandlers.Add(3057U, (ISkill)new StatusRegi("Stun"));
            this.skillHandlers.Add(3061U, (ISkill)new StatusRegi("Silence"));
            this.skillHandlers.Add(3149U, (ISkill)new StatusRegi("Stone"));
            this.skillHandlers.Add(3063U, (ISkill)new StatusRegi("Confuse"));
            this.skillHandlers.Add(3151U, (ISkill)new StatusRegi("鈍足"));
            this.skillHandlers.Add(3153U, (ISkill)new StatusRegi("Frosen"));
            this.skillHandlers.Add(3078U, (ISkill)new TurnUndead());
            this.skillHandlers.Add(3006U, (ISkill)new FireBolt());
            this.skillHandlers.Add(3007U, (ISkill)new FireShield());
            this.skillHandlers.Add(3008U, (ISkill)new FireWeapon());
            this.skillHandlers.Add(3009U, (ISkill)new FireBlast());
            this.skillHandlers.Add(3029U, (ISkill)new SagaMap.Skill.SkillDefinations.Shaman.IceArrow());
            this.skillHandlers.Add(3030U, (ISkill)new WaterShield());
            this.skillHandlers.Add(3031U, (ISkill)new WaterWeapon());
            this.skillHandlers.Add(3032U, (ISkill)new ColdBlast());
            this.skillHandlers.Add(3041U, (ISkill)new LandKlug());
            this.skillHandlers.Add(3042U, (ISkill)new EarthShield());
            this.skillHandlers.Add(3043U, (ISkill)new EarthWeapon());
            this.skillHandlers.Add(3044U, (ISkill)new EarthBlast());
            this.skillHandlers.Add(3017U, (ISkill)new ThunderBall());
            this.skillHandlers.Add(3018U, (ISkill)new WindShield());
            this.skillHandlers.Add(3019U, (ISkill)new WindWeapon());
            this.skillHandlers.Add(3020U, (ISkill)new LightningBlast());
            this.skillHandlers.Add(3011U, (ISkill)new FireWall());
            this.skillHandlers.Add(3047U, (ISkill)new StoneWall());
            this.skillHandlers.Add(802U, (ISkill)new ElementIInfo());
            this.skillHandlers.Add(3000U, (ISkill)new SenseElement());
            this.skillHandlers.Add(3162U, (ISkill)new ElementAllUp());
            this.skillHandlers.Add(3016U, (ISkill)new FireGroove());
            this.skillHandlers.Add(3028U, (ISkill)new WindGroove());
            this.skillHandlers.Add(3040U, (ISkill)new SagaMap.Skill.SkillDefinations.Elementaler.WaterGroove());
            this.skillHandlers.Add(3053U, (ISkill)new EarthGroove());
            this.skillHandlers.Add(3265U, (ISkill)new LavaFlow());
            this.skillHandlers.Add(3036U, (ISkill)new SagaMap.Skill.SkillDefinations.Elementaler.WaterStorm());
            this.skillHandlers.Add(3025U, (ISkill)new SagaMap.Skill.SkillDefinations.Elementaler.WindStorm());
            this.skillHandlers.Add(3049U, (ISkill)new SagaMap.Skill.SkillDefinations.Elementaler.EarthStorm());
            this.skillHandlers.Add(3013U, (ISkill)new SagaMap.Skill.SkillDefinations.Elementaler.FireStorm());
            this.skillHandlers.Add(3261U, (ISkill)new ChainLightning());
            this.skillHandlers.Add(3260U, (ISkill)new CatlingGun());
            this.skillHandlers.Add(3262U, (ISkill)new WaterNum());
            this.skillHandlers.Add(3263U, (ISkill)new EarthNum());
            this.skillHandlers.Add(3264U, (ISkill)new WaterWindTurable());
            this.skillHandlers.Add(3311U, (ISkill)new SpellCancel());
            this.skillHandlers.Add(3159U, (ISkill)new Zen());
            this.skillHandlers.Add(2209U, (ISkill)new ElementAnalysis());
            this.skillHandlers.Add(3301U, (ISkill)new AquaWave());
            this.skillHandlers.Add(3306U, (ISkill)new CycloneGrooveEarth());
            this.skillHandlers.Add(939U, (ISkill)new ElementLimitUp(Elements.Earth));
            this.skillHandlers.Add(936U, (ISkill)new ElementLimitUp(Elements.Fire));
            this.skillHandlers.Add(937U, (ISkill)new ElementLimitUp(Elements.Water));
            this.skillHandlers.Add(938U, (ISkill)new ElementLimitUp(Elements.Wind));
            this.skillHandlers.Add(3318U, (ISkill)new GravityFall());
            this.skillHandlers.Add(3319U, (ISkill)new ElementalWrath());
            this.skillHandlers.Add(3294U, (ISkill)new SpdUp_AvoUp_AtkDown_DefDown());
            this.skillHandlers.Add(3295U, (ISkill)new AtkUp_DefUp_SpdDown_AvoDown());
            this.skillHandlers.Add(2305U, (ISkill)new SoulOfEarth());
            this.skillHandlers.Add(2304U, (ISkill)new SoulOfWind());
            this.skillHandlers.Add(2303U, (ISkill)new SoulOfWater());
            this.skillHandlers.Add(2302U, (ISkill)new SoulOfFire());
            this.skillHandlers.Add(3046U, (ISkill)new PoisonMash());
            this.skillHandlers.Add(3155U, (ISkill)new Bind());
            this.skillHandlers.Add(3052U, (ISkill)new AcidMist());
            this.skillHandlers.Add(3010U, (ISkill)new FirePillar());
            this.skillHandlers.Add(3048U, (ISkill)new ElementCircle(Elements.Earth));
            this.skillHandlers.Add(3012U, (ISkill)new ElementCircle(Elements.Fire));
            this.skillHandlers.Add(3035U, (ISkill)new ElementCircle(Elements.Water));
            this.skillHandlers.Add(3024U, (ISkill)new ElementCircle(Elements.Wind));
            this.skillHandlers.Add(3296U, (ISkill)new ElementBall());
            this.skillHandlers.Add(3317U, (ISkill)new EnchantWeapon());
            this.skillHandlers.Add(3110U, (ISkill)new ElementRise(Elements.Earth));
            this.skillHandlers.Add(3107U, (ISkill)new ElementRise(Elements.Fire));
            this.skillHandlers.Add(3109U, (ISkill)new ElementRise(Elements.Water));
            this.skillHandlers.Add(3108U, (ISkill)new ElementRise(Elements.Wind));
            this.skillHandlers.Add(114U, (ISkill)new LAvoUp());
            this.skillHandlers.Add(2049U, (ISkill)new LHitUp());
            this.skillHandlers.Add(2050U, (ISkill)new BowCancel());
            this.skillHandlers.Add(2128U, (ISkill)new ConArrow());
            this.skillHandlers.Add(2129U, (ISkill)new ChargeArrow());
            this.skillHandlers.Add(2144U, (ISkill)new SagaMap.Skill.SkillDefinations.Archer.FireArrow());
            this.skillHandlers.Add(2145U, (ISkill)new SagaMap.Skill.SkillDefinations.Archer.WaterArrow());
            this.skillHandlers.Add(2146U, (ISkill)new SagaMap.Skill.SkillDefinations.Archer.EarthArrow());
            this.skillHandlers.Add(2147U, (ISkill)new SagaMap.Skill.SkillDefinations.Archer.WindArrow());
            this.skillHandlers.Add(2206U, (ISkill)new DistanceArrow());
            this.skillHandlers.Add(3083U, (ISkill)new BlackWidow());
            this.skillHandlers.Add(3085U, (ISkill)new ShadowBlast());
            this.skillHandlers.Add(3088U, (ISkill)new DarkWeapon());
            this.skillHandlers.Add(3093U, (ISkill)new DarkGroove());
            this.skillHandlers.Add(3133U, (ISkill)new DarkShield());
            this.skillHandlers.Add(3134U, (ISkill)new ChaosWidow());
            this.skillHandlers.Add(3140U, (ISkill)new SagaMap.Skill.SkillDefinations.Warlock.MagSlow());
            this.skillHandlers.Add(3141U, (ISkill)new MagConfuse());
            this.skillHandlers.Add(3142U, (ISkill)new MagFreeze());
            this.skillHandlers.Add(3143U, (ISkill)new MagStun());
            this.skillHandlers.Add(3112U, (ISkill)new ElementRise(Elements.Dark));
            this.skillHandlers.Add(941U, (ISkill)new ElementLimitUp(Elements.Dark));
            this.skillHandlers.Add(2229U, (ISkill)new GrimReaper());
            this.skillHandlers.Add(2230U, (ISkill)new SoulSteal());
            this.skillHandlers.Add(3092U, (ISkill)new ElementCircle(Elements.Dark));
            this.skillHandlers.Add(3087U, (ISkill)new Fanaticism());
            this.skillHandlers.Add(3089U, (ISkill)new SagaMap.Skill.SkillDefinations.Cabalist.DarkStorm());
            this.skillHandlers.Add(3274U, (ISkill)new MoveDownCircle());
            this.skillHandlers.Add(3021U, (ISkill)new SleepCloud());
            this.skillHandlers.Add(949U, (ISkill)new AllRateUp());
            this.skillHandlers.Add(3167U, (ISkill)new DarkChopMark());
            this.skillHandlers.Add(3166U, (ISkill)new ChopMark());
            this.skillHandlers.Add(3270U, (ISkill)new HitAndAway());
            this.skillHandlers.Add(3273U, (ISkill)new StoneSkin());
            this.skillHandlers.Add(3272U, (ISkill)new RandMark());
            this.skillHandlers.Add(3309U, (ISkill)new ChgstRand());
            this.skillHandlers.Add(3310U, (ISkill)new EventSelfDarkStorm());
            this.skillHandlers.Add(10000U, (ISkill)new EffDarkChopMark());
            this.skillHandlers.Add(2007U, (ISkill)new SpearCancel());
            this.skillHandlers.Add(2121U, (ISkill)new ChargeStrike());
            this.skillHandlers.Add(2138U, (ISkill)new LightningSpear());
            this.skillHandlers.Add(2003U, (ISkill)new MobDefUpSelf());
            this.skillHandlers.Add(116U, (ISkill)new ShieldGuardUp());
            this.skillHandlers.Add(106U, (ISkill)new GuardUp());
            this.skillHandlers.Add(2064U, (ISkill)new AstuteStab());
            this.skillHandlers.Add(2228U, (ISkill)new HolyBlade());
            this.skillHandlers.Add(3170U, (ISkill)new SagaMap.Skill.SkillDefinations.Knight.Healing());
            this.skillHandlers.Add(2123U, (ISkill)new SagaMap.Skill.SkillDefinations.Knight.ShockWave());
            this.skillHandlers.Add(2247U, (ISkill)new AtkUnDead());
            this.skillHandlers.Add(946U, (ISkill)new ConSpear());
            this.skillHandlers.Add(2065U, (ISkill)new AstuteBlow());
            this.skillHandlers.Add(934U, (ISkill)new ElementAddUp(Elements.Holy, "LightAddUp"));
            this.skillHandlers.Add(2041U, (ISkill)new DifrectArrow());
            this.skillHandlers.Add(2063U, (ISkill)new AstuteSlash());
            this.skillHandlers.Add(2245U, (ISkill)new CutDownSpear());
            this.skillHandlers.Add(4025U, (ISkill)new DJoint());
            this.skillHandlers.Add(4029U, (ISkill)new AProtect());
            this.skillHandlers.Add(2248U, (ISkill)new HoldShield());
            this.skillHandlers.Add(2246U, (ISkill)new HitRow());
            this.skillHandlers.Add(2125U, (ISkill)new Valkyrie());
            this.skillHandlers.Add(2249U, (ISkill)new StrikeSpear());
            this.skillHandlers.Add(3251U, (ISkill)new VicariouslyResu());
            this.skillHandlers.Add(2383U, (ISkill)new Appeal());
            this.skillHandlers.Add(2381U, (ISkill)new DirlineRandSeq());
            this.skillHandlers.Add(2382U, (ISkill)new DirlineRandSeq2());
            this.skillHandlers.Add(2061U, (ISkill)new Revive());
            this.skillHandlers.Add(2009U, (ISkill)new Synthese());
            this.skillHandlers.Add(2051U, (ISkill)new Synthese());
            this.skillHandlers.Add(2083U, (ISkill)new Synthese());
            this.skillHandlers.Add(2185U, (ISkill)new AtkRow());
            this.skillHandlers.Add(800U, (ISkill)new RockInfo());
            this.skillHandlers.Add(2200U, (ISkill)new EventCampfire());
            this.skillHandlers.Add(2071U, (ISkill)new PosturetorToise());
            this.skillHandlers.Add(905U, (ISkill)new GoRiKi());
            this.skillHandlers.Add(2177U, (ISkill)new StoneThrow());
            this.skillHandlers.Add(2135U, (ISkill)new ThrowDirt());
            this.skillHandlers.Add(2010U, (ISkill)new Synthese());
            this.skillHandlers.Add(2017U, (ISkill)new Synthese());
            this.skillHandlers.Add(3342U, (ISkill)new FrameHart());
            this.skillHandlers.Add(2224U, (ISkill)new RockCrash());
            this.skillHandlers.Add(2198U, (ISkill)new FirstAid());
            this.skillHandlers.Add(2194U, (ISkill)new KnifeGrinder());
            this.skillHandlers.Add(2388U, (ISkill)new ThrowNugget());
            this.skillHandlers.Add(2387U, (ISkill)new EearthCrash());
            this.skillHandlers.Add(6050U, (ISkill)new PetAttack());
            this.skillHandlers.Add(6051U, (ISkill)new PetBack());
            this.skillHandlers.Add(2207U, (ISkill)new RockAnalysis());
            this.skillHandlers.Add(6102U, (ISkill)new PetCastSkill(6103U, "MACHINE"));
            this.skillHandlers.Add(6103U, (ISkill)new PetMacAtk());
            this.skillHandlers.Add(6101U, (ISkill)new PetMacLHitUp());
            this.skillHandlers.Add(930U, (ISkill)new FireAddup());
            this.skillHandlers.Add(6104U, (ISkill)new PetCastSkill(6105U, "MACHINE"));
            this.skillHandlers.Add(6105U, (ISkill)new PetMacCircleAtk());
            this.skillHandlers.Add(2262U, (ISkill)new SupportRockInfo());
            this.skillHandlers.Add(2253U, (ISkill)new GuideRock());
            this.skillHandlers.Add(2261U, (ISkill)new DurDownCancel());
            this.skillHandlers.Add(2395U, (ISkill)new Balls());
            this.skillHandlers.Add(942U, (ISkill)new BoostPower());
            this.skillHandlers.Add(409U, (ISkill)new StoDamUp());
            this.skillHandlers.Add(410U, (ISkill)new StoHitUp());
            this.skillHandlers.Add(411U, (ISkill)new StoAvoUp());
            this.skillHandlers.Add(2039U, (ISkill)new Synthese());
            this.skillHandlers.Add(809U, (ISkill)new MachineInfo());
            this.skillHandlers.Add(132U, (ISkill)new RobotDamUp());
            this.skillHandlers.Add(970U, (ISkill)new RobotRecUp());
            this.skillHandlers.Add(964U, (ISkill)new RobotHpUp());
            this.skillHandlers.Add(2326U, (ISkill)new RobotAmobm());
            this.skillHandlers.Add(968U, (ISkill)new RobotHitUp());
            this.skillHandlers.Add(966U, (ISkill)new RobotDefUp());
            this.skillHandlers.Add(2323U, (ISkill)new RobotChaff());
            this.skillHandlers.Add(969U, (ISkill)new RobotAvoUp());
            this.skillHandlers.Add(965U, (ISkill)new RobotAtkUp());
            this.skillHandlers.Add(2324U, (ISkill)new MirrorSkill());
            this.skillHandlers.Add(2325U, (ISkill)new RobotTeleport());
            this.skillHandlers.Add(2322U, (ISkill)new RobotBerserk());
            this.skillHandlers.Add(2368U, (ISkill)new RobotChillLaser());
            this.skillHandlers.Add(2327U, (ISkill)new RobotSalvoFire());
            this.skillHandlers.Add(2369U, (ISkill)new RobotEcm());
            this.skillHandlers.Add(2422U, (ISkill)new RobotFireRadiation());
            this.skillHandlers.Add(2424U, (ISkill)new RobotOverTune());
            this.skillHandlers.Add(2423U, (ISkill)new RobotSparkBall());
            this.skillHandlers.Add(2425U, (ISkill)new RobotLovageCannon());
            this.skillHandlers.Add(506U, (ISkill)new MciDamUp());
            this.skillHandlers.Add(507U, (ISkill)new MciHitUp());
            this.skillHandlers.Add(508U, (ISkill)new MciAvoUp());
            this.skillHandlers.Add(2020U, (ISkill)new Synthese());
            this.skillHandlers.Add(2034U, (ISkill)new Synthese());
            this.skillHandlers.Add(2040U, (ISkill)new Synthese());
            this.skillHandlers.Add(2054U, (ISkill)new Synthese());
            this.skillHandlers.Add(2085U, (ISkill)new Synthese());
            this.skillHandlers.Add(2086U, (ISkill)new Synthese());
            this.skillHandlers.Add(2089U, (ISkill)new Synthese());
            this.skillHandlers.Add(3128U, (ISkill)new Cultivation());
            this.skillHandlers.Add(807U, (ISkill)new PlantInfo());
            this.skillHandlers.Add(804U, (ISkill)new TreeInfo());
            this.skillHandlers.Add(2169U, (ISkill)new GrassTrap());
            this.skillHandlers.Add(2170U, (ISkill)new PitTrap());
            this.skillHandlers.Add(2196U, (ISkill)new HealingTree());
            this.skillHandlers.Add(2022U, (ISkill)new Synthese());
            this.skillHandlers.Add(2118U, (ISkill)new SagaMap.Skill.SkillDefinations.Alchemist.Phalanx());
            this.skillHandlers.Add(3096U, (ISkill)new DelayTrap());
            this.skillHandlers.Add(2389U, (ISkill)new DustExplosion());
            this.skillHandlers.Add(2214U, (ISkill)new PlantAnalysis());
            this.skillHandlers.Add(954U, (ISkill)new FoodThrow());
            this.skillHandlers.Add(909U, (ISkill)new PotionFighter());
            this.skillHandlers.Add(406U, (ISkill)new PlaDamUp());
            this.skillHandlers.Add(407U, (ISkill)new PlaHitUp());
            this.skillHandlers.Add(408U, (ISkill)new PlaAvoUp());
            this.skillHandlers.Add(6202U, (ISkill)new PetCastSkill(6203U, "PLANT"));
            this.skillHandlers.Add(6203U, (ISkill)new PetPlantAtk());
            this.skillHandlers.Add(6206U, (ISkill)new PetCastSkill(6207U, "PLANT"));
            this.skillHandlers.Add(6207U, (ISkill)new PetPlantDefupSelf());
            this.skillHandlers.Add(6204U, (ISkill)new PetCastSkill(6205U, "PLANT"));
            this.skillHandlers.Add(6205U, (ISkill)new PetPlantHealing());
            this.skillHandlers.Add(2263U, (ISkill)new SupportPlantInfo());
            this.skillHandlers.Add(943U, (ISkill)new BoostMagic());
            this.skillHandlers.Add(2390U, (ISkill)new ThrowChemical());
            this.skillHandlers.Add(3343U, (ISkill)new SumChemicalPlant());
            this.skillHandlers.Add(3344U, (ISkill)new SumChemicalPlant2());
            this.skillHandlers.Add(6200U, (ISkill)new PetCastSkill(6201U, "PLANT"));
            this.skillHandlers.Add(6201U, (ISkill)new PetPlantPoison());
            this.skillHandlers.Add(2211U, (ISkill)new TreeAnalysis());
            this.skillHandlers.Add(2038U, (ISkill)new Synthese());
            this.skillHandlers.Add(133U, (ISkill)new MarioDamUp());
            this.skillHandlers.Add(2328U, (ISkill)new MarioCtrl());
            this.skillHandlers.Add(967U, (ISkill)new MarioCtrlMove());
            this.skillHandlers.Add(2329U, (ISkill)new MarioOver());
            this.skillHandlers.Add(2331U, (ISkill)new EnemyCharming());
            this.skillHandlers.Add(2335U, (ISkill)new MarioEarthWater());
            this.skillHandlers.Add(2334U, (ISkill)new MarioWindEarth());
            this.skillHandlers.Add(2333U, (ISkill)new MarioFireWind());
            this.skillHandlers.Add(2332U, (ISkill)new MarioWaterFire());
            this.skillHandlers.Add(2371U, (ISkill)new Puppet());
            this.skillHandlers.Add(976U, (ISkill)new MarioTimeUp());
            this.skillHandlers.Add(2370U, (ISkill)new MarionetteHarmony());
            this.skillHandlers.Add(980U, (ISkill)new ChangeMarionette());
            this.skillHandlers.Add(3333U, (ISkill)new MarioCancel());
            this.skillHandlers.Add(981U, (ISkill)new MariostateUp());
            this.skillHandlers.Add(3334U, (ISkill)new SumMario(26040001U, 3335U));
            this.skillHandlers.Add(3335U, (ISkill)new SumMarioCont(Elements.Fire));
            this.skillHandlers.Add(3336U, (ISkill)new SumMario(26100009U, 3337U));
            this.skillHandlers.Add(3337U, (ISkill)new SumMarioCont(Elements.Water));
            this.skillHandlers.Add(3338U, (ISkill)new SumMario(26100009U, 3339U));
            this.skillHandlers.Add(3339U, (ISkill)new SumMarioCont(Elements.Wind));
            this.skillHandlers.Add(3340U, (ISkill)new SumMario(26070003U, 3341U));
            this.skillHandlers.Add(3341U, (ISkill)new SumMarioCont(Elements.Earth));
            this.skillHandlers.Add(2088U, (ISkill)new Synthese());
            this.skillHandlers.Add(713U, (ISkill)new Bivouac());
            this.skillHandlers.Add(805U, (ISkill)new InsectInfo());
            this.skillHandlers.Add(806U, (ISkill)new BirdInfo());
            this.skillHandlers.Add(808U, (ISkill)new AnimalInfo());
            this.skillHandlers.Add(812U, (ISkill)new WataniInfo());
            this.skillHandlers.Add(816U, (ISkill)new TreasureInfo());
            this.skillHandlers.Add(2197U, (ISkill)new CswarSleep());
            this.skillHandlers.Add(2103U, (ISkill)new Unlock());
            this.skillHandlers.Add(403U, (ISkill)new AniDamUp());
            this.skillHandlers.Add(404U, (ISkill)new AniHitUp());
            this.skillHandlers.Add(405U, (ISkill)new AniAvoUp());
            this.skillHandlers.Add(415U, (ISkill)new WanDamUp());
            this.skillHandlers.Add(416U, (ISkill)new WanHitUp());
            this.skillHandlers.Add(417U, (ISkill)new WanAvoUp());
            this.skillHandlers.Add(3146U, (ISkill)new CureAll());
            this.skillHandlers.Add(3307U, (ISkill)new RegiAllUp());
            this.skillHandlers.Add(3308U, (ISkill)new SagaMap.Skill.SkillDefinations.Druid.AreaHeal());
            this.skillHandlers.Add(3257U, (ISkill)new SagaMap.Skill.SkillDefinations.Druid.STR_VIT_AGI_UP());
            this.skillHandlers.Add(3258U, (ISkill)new SagaMap.Skill.SkillDefinations.Druid.MAG_INT_DEX_UP());
            this.skillHandlers.Add(3056U, (ISkill)new HolyFeather());
            this.skillHandlers.Add(3164U, (ISkill)new FlashLight());
            this.skillHandlers.Add(3080U, (ISkill)new ElementCircle(Elements.Holy));
            this.skillHandlers.Add(3163U, (ISkill)new SunLightShower());
            this.skillHandlers.Add(3268U, (ISkill)new CriAvoDownOne());
            this.skillHandlers.Add(3266U, (ISkill)new LightHigeCircle());
            this.skillHandlers.Add(3118U, (ISkill)new Seal());
            this.skillHandlers.Add(2210U, (ISkill)new UndeadAnalysis());
            this.skillHandlers.Add(3267U, (ISkill)new UndeadMdefDownOne());
            this.skillHandlers.Add(3077U, (ISkill)new ClairvoYance());
            this.skillHandlers.Add(3119U, (ISkill)new SealMagic());
            this.skillHandlers.Add(950U, (ISkill)new TranceSpdUp());
            this.skillHandlers.Add(940U, (ISkill)new ElementLimitUp(Elements.Holy));
            this.skillHandlers.Add(509U, (ISkill)new UndDamUp());
            this.skillHandlers.Add(510U, (ISkill)new UndHitUp());
            this.skillHandlers.Add(511U, (ISkill)new UndAvoUp());
            this.skillHandlers.Add(2310U, (ISkill)new DefUpCircle());
            this.skillHandlers.Add(3323U, (ISkill)new DeadMarch());
            this.skillHandlers.Add(2313U, (ISkill)new HPSPMPUPCircle());
            this.skillHandlers.Add(2311U, (ISkill)new AVOUPCircle());
            this.skillHandlers.Add(2312U, (ISkill)new CRIUPCircle());
            this.skillHandlers.Add(2309U, (ISkill)new ATKUPCircle());
            this.skillHandlers.Add(2367U, (ISkill)new MusicalBlow());
            this.skillHandlers.Add(2366U, (ISkill)new Shout());
            this.skillHandlers.Add(2365U, (ISkill)new HMSPRateUp());
            this.skillHandlers.Add(2315U, (ISkill)new BardSession());
            this.skillHandlers.Add(3321U, (ISkill)new ORaToRiO());
            this.skillHandlers.Add(2308U, (ISkill)new MAGINTDEXUPCircle());
            this.skillHandlers.Add(2307U, (ISkill)new STRVITAGIUPCircle());
            this.skillHandlers.Add(2306U, (ISkill)new ChangeMusic());
            this.skillHandlers.Add(131U, (ISkill)new MusicalDamUp());
            this.skillHandlers.Add(2314U, (ISkill)new Requiem());
            this.skillHandlers.Add(3322U, (ISkill)new AttractMarch());
            this.skillHandlers.Add(3320U, (ISkill)new LoudSong());
            this.skillHandlers.Add(2030U, (ISkill)new Synthese());
            this.skillHandlers.Add(2031U, (ISkill)new Synthese());
            this.skillHandlers.Add(2032U, (ISkill)new Synthese());
            this.skillHandlers.Add(2033U, (ISkill)new Synthese());
            this.skillHandlers.Add(2296U, (ISkill)new IntelRides());
            this.skillHandlers.Add(3169U, (ISkill)new EnergyStorm());
            this.skillHandlers.Add(2330U, (ISkill)new EnergyFreak());
            this.skillHandlers.Add(3291U, (ISkill)new EnergyFlare());
            this.skillHandlers.Add(3292U, (ISkill)new ChgstBlock());
            this.skillHandlers.Add(3312U, (ISkill)new LuminaryNova());
            this.skillHandlers.Add(3315U, (ISkill)new LastInQuest());
            this.skillHandlers.Add(3313U, (ISkill)new SagaMap.Skill.SkillDefinations.Sage.AReflection());
            this.skillHandlers.Add(130U, (ISkill)new ReadDamup());
            this.skillHandlers.Add(2295U, (ISkill)new StaffCtrl());
            this.skillHandlers.Add(2297U, (ISkill)new Provide());
            this.skillHandlers.Add(2294U, (ISkill)new MonsterSketch());
            this.skillHandlers.Add(3293U, (ISkill)new MagHitUpCircle());
            this.skillHandlers.Add(3314U, (ISkill)new SumDop());
            this.skillHandlers.Add(3331U, (ISkill)new Dejion());
            this.skillHandlers.Add(2316U, (ISkill)new AtkMp());
            this.skillHandlers.Add(2317U, (ISkill)new AtkSp());
            this.skillHandlers.Add(3288U, (ISkill)new DarkLight());
            this.skillHandlers.Add(3330U, (ISkill)new EvilSoul());
            this.skillHandlers.Add(3332U, (ISkill)new ChaosGait());
            this.skillHandlers.Add(2318U, (ISkill)new AbsorbHpWeapon());
            this.skillHandlers.Add(2320U, (ISkill)new SummobLemures());
            this.skillHandlers.Add(961U, (ISkill)new LemuresHpUp());
            this.skillHandlers.Add(963U, (ISkill)new LemuresMatkUp());
            this.skillHandlers.Add(962U, (ISkill)new LemuresAtkUp());
            this.skillHandlers.Add(2321U, (ISkill)new HealLemures());
            this.skillHandlers.Add(2319U, (ISkill)new Rebone());
            this.skillHandlers.Add(3121U, (ISkill)new NeKuRoMaNShi());
            this.skillHandlers.Add(315U, (ISkill)new ChgstDamUp());
            this.skillHandlers.Add(3122U, (ISkill)new TrDrop2());
            this.skillHandlers.Add(3297U, (ISkill)new Terror());
            this.skillHandlers.Add(3324U, (ISkill)new SumDeath());
            this.skillHandlers.Add(3325U, (ISkill)new SumDeath2());
            this.skillHandlers.Add(3326U, (ISkill)new SumDeath3());
            this.skillHandlers.Add(3327U, (ISkill)new SumDeath4());
            this.skillHandlers.Add(3328U, (ISkill)new SumDeath5());
            this.skillHandlers.Add(3329U, (ISkill)new SumDeath6());
            this.skillHandlers.Add(4450U, (ISkill)new Soul());
            this.skillHandlers.Add(4451U, (ISkill)new Soul());
            this.skillHandlers.Add(4452U, (ISkill)new Soul());
            this.skillHandlers.Add(4453U, (ISkill)new Soul());
            this.skillHandlers.Add(4454U, (ISkill)new Soul());
            this.skillHandlers.Add(4455U, (ISkill)new Soul());
            this.skillHandlers.Add(4460U, (ISkill)new Soul());
            this.skillHandlers.Add(4461U, (ISkill)new Soul());
            this.skillHandlers.Add(4462U, (ISkill)new Soul());
            this.skillHandlers.Add(4463U, (ISkill)new Soul());
            this.skillHandlers.Add(4464U, (ISkill)new Soul());
            this.skillHandlers.Add(4465U, (ISkill)new Soul());
            this.skillHandlers.Add(2276U, (ISkill)new DarkVacuum());
            this.skillHandlers.Add(2357U, (ISkill)new DarkMist());
            this.skillHandlers.Add(2356U, (ISkill)new LifeSteal());
            this.skillHandlers.Add(3289U, (ISkill)new DegradetionDarkFlare());
            this.skillHandlers.Add(3290U, (ISkill)new DegradetionDarkFlare());
            this.skillHandlers.Add(2403U, (ISkill)new FlareSting());
            this.skillHandlers.Add(2404U, (ISkill)new FlareSting2());
            this.skillHandlers.Add(2405U, (ISkill)new BloodAbsrd());
            this.skillHandlers.Add(3120U, (ISkill)new Spell());
            this.skillHandlers.Add(957U, (ISkill)new NecroResu());
            this.skillHandlers.Add(314U, (ISkill)new ChgstSwoDamUp());
            this.skillHandlers.Add(2277U, (ISkill)new CancelLightCircle());
            this.skillHandlers.Add(958U, (ISkill)new DarkProtect());
            this.skillHandlers.Add(935U, (ISkill)new ElementAddUp(Elements.Dark, "DarkAddUp"));
            this.skillHandlers.Add(2278U, (ISkill)new LightSeal());
            this.skillHandlers.Add(2279U, (ISkill)new BradStigma());
            this.skillHandlers.Add(2358U, (ISkill)new HpLostDamUp());
            this.skillHandlers.Add(979U, (ISkill)new HpDownToDamUp());
            this.skillHandlers.Add(2406U, (ISkill)new DarknessOfNight());
            this.skillHandlers.Add(2407U, (ISkill)new DarknessOfNight2());
            this.skillHandlers.Add(500U, (ISkill)new EleDamUp());
            this.skillHandlers.Add(501U, (ISkill)new EleHitUp());
            this.skillHandlers.Add(502U, (ISkill)new EleAvoUp());
            this.skillHandlers.Add(2148U, (ISkill)new PluralityArrow());
            this.skillHandlers.Add(2149U, (ISkill)new ElementArrow(Elements.Fire));
            this.skillHandlers.Add(2150U, (ISkill)new ElementArrow(Elements.Water));
            this.skillHandlers.Add(2151U, (ISkill)new ElementArrow(Elements.Earth));
            this.skillHandlers.Add(2152U, (ISkill)new ElementArrow(Elements.Wind));
            this.skillHandlers.Add(2220U, (ISkill)new PotionArrow());
            this.skillHandlers.Add(2266U, (ISkill)new BlastArrow());
            this.skillHandlers.Add(2190U, (ISkill)new LightDarkArrow(Elements.Holy));
            this.skillHandlers.Add(2191U, (ISkill)new LightDarkArrow(Elements.Dark));
            this.skillHandlers.Add(951U, (ISkill)new ShotStance());
            this.skillHandlers.Add(313U, (ISkill)new HuntingTactics());
            this.skillHandlers.Add(2267U, (ISkill)new BowCastCancelOne());
            this.skillHandlers.Add(310U, (ISkill)new ChgstArrDamUp());
            this.skillHandlers.Add(2386U, (ISkill)new ArrowGroove());
            this.skillHandlers.Add(2385U, (ISkill)new ArmBreak());
            this.skillHandlers.Add(6500U, (ISkill)new PetBirdAtkRowCircle());
            this.skillHandlers.Add(6501U, (ISkill)new PetBirdAtkRowCircle2());
            this.skillHandlers.Add(6306U, (ISkill)new DogHateUpCircle());
            this.skillHandlers.Add(6307U, (ISkill)new PetDogHateUpCircle());
            this.skillHandlers.Add(6502U, (ISkill)new BirdAtk());
            this.skillHandlers.Add(6503U, (ISkill)new PetBirdAtk());
            this.skillHandlers.Add(6308U, (ISkill)new PetCastSkill(6309U, "ANIMAL"));
            this.skillHandlers.Add(6309U, (ISkill)new PetDogAtkCircle());
            this.skillHandlers.Add(6550U, (ISkill)new BirdDamUp());
            this.skillHandlers.Add(6350U, (ISkill)new DogHpUp());
            this.skillHandlers.Add(6310U, (ISkill)new PetCastSkill(6311U, "ANIMAL"));
            this.skillHandlers.Add(6311U, (ISkill)new PetDogDefUp());
            this.skillHandlers.Add(2285U, (ISkill)new FastDraw());
            this.skillHandlers.Add(2286U, (ISkill)new PluralityShot());
            this.skillHandlers.Add(2287U, (ISkill)new ChargeShot());
            this.skillHandlers.Add(2289U, (ISkill)new GrenadeShot());
            this.skillHandlers.Add(2291U, (ISkill)new GrenadeSlow());
            this.skillHandlers.Add(2292U, (ISkill)new GrenadeStan());
            this.skillHandlers.Add(2163U, (ISkill)new StunShot());
            this.skillHandlers.Add(2288U, (ISkill)new BurstShot());
            this.skillHandlers.Add(974U, (ISkill)new CQB());
            this.skillHandlers.Add(2364U, (ISkill)new GunCancel());
            this.skillHandlers.Add(2418U, (ISkill)new VitalShot());
            this.skillHandlers.Add(2419U, (ISkill)new ClothCrest());
            this.skillHandlers.Add(126U, (ISkill)new RifleGunDamUp());
            this.skillHandlers.Add(2290U, (ISkill)new ApiBullet());
            this.skillHandlers.Add(210U, (ISkill)new GunHitUp());
            this.skillHandlers.Add(2293U, (ISkill)new OverRange());
            this.skillHandlers.Add(2363U, (ISkill)new BulletDance());
            this.skillHandlers.Add(2420U, (ISkill)new PrecisionFire());
            this.skillHandlers.Add(2421U, (ISkill)new CanisterShot());
            this.skillHandlers.Add(2222U, (ISkill)new CaveHiding());
            this.skillHandlers.Add(2392U, (ISkill)new Blinding());
            this.skillHandlers.Add(2221U, (ISkill)new CaveBivouac());
            this.skillHandlers.Add(2212U, (ISkill)new InsectAnalysis());
            this.skillHandlers.Add(2213U, (ISkill)new BirdAnalysis());
            this.skillHandlers.Add(2215U, (ISkill)new AnimalAnalysis());
            this.skillHandlers.Add(2264U, (ISkill)new SupportInfo());
            this.skillHandlers.Add(953U, (ISkill)new BaitTrap());
            this.skillHandlers.Add(311U, (ISkill)new TrapDamUp());
            this.skillHandlers.Add(944U, (ISkill)new BoostHp());
            this.skillHandlers.Add(2391U, (ISkill)new FakeDeath());
            this.skillHandlers.Add(6300U, (ISkill)new PetCastSkill(6301U, "ANIMAL"));
            this.skillHandlers.Add(6301U, (ISkill)new PetDogSlash());
            this.skillHandlers.Add(6302U, (ISkill)new PetCastSkill(6303U, "ANIMAL"));
            this.skillHandlers.Add(6303U, (ISkill)new PetDogStan());
            this.skillHandlers.Add(6304U, (ISkill)new PetCastSkill(6305U, "ANIMAL"));
            this.skillHandlers.Add(6305U, (ISkill)new PetDogLineatk());
            this.skillHandlers.Add(2171U, (ISkill)new BarbedTrap());
            this.skillHandlers.Add(2172U, (ISkill)new Bungestac());
            this.skillHandlers.Add(412U, (ISkill)new InsDamUp());
            this.skillHandlers.Add(413U, (ISkill)new InsHitUp());
            this.skillHandlers.Add(414U, (ISkill)new InsAvoUp());
            this.skillHandlers.Add(2336U, (ISkill)new BackRush());
            this.skillHandlers.Add(2337U, (ISkill)new Catch());
            this.skillHandlers.Add(2340U, (ISkill)new ConthWhip());
            this.skillHandlers.Add(2373U, (ISkill)new WhipFlourish());
            this.skillHandlers.Add(2426U, (ISkill)new Caution());
            this.skillHandlers.Add(2372U, (ISkill)new Snatch());
            this.skillHandlers.Add(2427U, (ISkill)new PullWhip());
            this.skillHandlers.Add(2430U, (ISkill)new SonicWhip());
            this.skillHandlers.Add(129U, (ISkill)new RopeDamUp());
            this.skillHandlers.Add(2341U, (ISkill)new WeaponRemove());
            this.skillHandlers.Add(2342U, (ISkill)new ArmorRemove());
            this.skillHandlers.Add(134U, (ISkill)new UnlockDamUp());
            this.skillHandlers.Add(2429U, (ISkill)new Escape());
            this.skillHandlers.Add(702U, (ISkill)new Packing());
            this.skillHandlers.Add(703U, (ISkill)new BuyRateDown());
            this.skillHandlers.Add(704U, (ISkill)new SellRateUp());
            this.skillHandlers.Add(2173U, (ISkill)new AtkUpOne());
            this.skillHandlers.Add(2180U, (ISkill)new SunSofbley());
            this.skillHandlers.Add(2186U, (ISkill)new Magrow());
            this.skillHandlers.Add(2394U, (ISkill)new BugRand());
            this.skillHandlers.Add(705U, (ISkill)new Trust());
            this.skillHandlers.Add(906U, (ISkill)new BagDamUp());
            this.skillHandlers.Add(811U, (ISkill)new HumanInfo());
            this.skillHandlers.Add(2223U, (ISkill)new Shift());
            this.skillHandlers.Add(2225U, (ISkill)new AgiDexUpOne());
            this.skillHandlers.Add(948U, (ISkill)new BagCapDamup());
            this.skillHandlers.Add(2227U, (ISkill)new Abetment());
            this.skillHandlers.Add(706U, (ISkill)new Connection());
            this.skillHandlers.Add(2218U, (ISkill)new HumanAnalysis());
            this.skillHandlers.Add(945U, (ISkill)new BoostCritical());
            this.skillHandlers.Add(6400U, (ISkill)new HumCustomary());
            this.skillHandlers.Add(6401U, (ISkill)new PetHumCustomary());
            this.skillHandlers.Add(6404U, (ISkill)new PetAtkupSelf());
            this.skillHandlers.Add(6405U, (ISkill)new PetHitupSelf());
            this.skillHandlers.Add(6406U, (ISkill)new PetDefupSelf());
            this.skillHandlers.Add(6402U, (ISkill)new HumAdditional());
            this.skillHandlers.Add(6403U, (ISkill)new PetHumAdditional());
            this.skillHandlers.Add(6407U, (ISkill)new PetSlash());
            this.skillHandlers.Add(6408U, (ISkill)new PetIai());
            this.skillHandlers.Add(6409U, (ISkill)new PetProvocation());
            this.skillHandlers.Add(6410U, (ISkill)new PetSennpuuken());
            this.skillHandlers.Add(6411U, (ISkill)new PetMeditation());
            this.skillHandlers.Add(6450U, (ISkill)new HumHealRateUp());
            this.skillHandlers.Add(3286U, (ISkill)new RandHeal());
            this.skillHandlers.Add(3287U, (ISkill)new RouletteHeal());
            this.skillHandlers.Add(2348U, (ISkill)new AtkDownOne());
            this.skillHandlers.Add(2374U, (ISkill)new CardBoomEran());
            this.skillHandlers.Add(2350U, (ISkill)new RandDamOne());
            this.skillHandlers.Add(2377U, (ISkill)new DoubleUp());
            this.skillHandlers.Add(2375U, (ISkill)new CoinShot());
            this.skillHandlers.Add(972U, (ISkill)new Blackleg());
            this.skillHandlers.Add(2347U, (ISkill)new SagaMap.Skill.SkillDefinations.Gambler.Slot());
            this.skillHandlers.Add(973U, (ISkill)new BadLucky());
            this.skillHandlers.Add(2376U, (ISkill)new SkillBreak());
            this.skillHandlers.Add(2436U, (ISkill)new TrickDice());
            this.skillHandlers.Add(2433U, (ISkill)new SumArcanaCard());
            this.skillHandlers.Add(2432U, (ISkill)new SumArcanaCard2());
            this.skillHandlers.Add(2431U, (ISkill)new SumArcanaCard3());
            this.skillHandlers.Add(2434U, (ISkill)new SumArcanaCard4());
            this.skillHandlers.Add(2435U, (ISkill)new SumArcanaCard5());
            this.skillHandlers.Add(2351U, (ISkill)new RandChgstateCircle());
            this.skillHandlers.Add(2439U, (ISkill)new FlowerCard());
            this.skillHandlers.Add(2440U, (ISkill)new FlowerCardSEQ());
            this.skillHandlers.Add(2441U, (ISkill)new FlowerCardSEQ2());
            this.skillHandlers.Add(6424U, (ISkill)new Revive(2));
            this.skillHandlers.Add(6425U, (ISkill)new Revive(5));
            this.skillHandlers.Add(1000U, (ISkill)new GrowUp());
            this.skillHandlers.Add(1001U, (ISkill)new Biology());
            this.skillHandlers.Add(1002U, (ISkill)new LionPower());
            this.skillHandlers.Add(1003U, (ISkill)new Reins());
            this.skillHandlers.Add(2442U, (ISkill)new TheTrust());
            this.skillHandlers.Add(2443U, (ISkill)new Metamorphosis());
            this.skillHandlers.Add(2444U, (ISkill)new PetDelayCancel());
            this.skillHandlers.Add(2445U, (ISkill)new Akurobattoibeijon());
            this.skillHandlers.Add(2446U, (ISkill)new HealFire());
            this.skillHandlers.Add(2447U, (ISkill)new Encouragement());
            this.skillHandlers.Add(2453U, (ISkill)new IAmTree());
            this.skillHandlers.Add(2449U, (ISkill)new GardenerSkill());
            this.skillHandlers.Add(1006U, (ISkill)new MoogCoalUp());
            this.skillHandlers.Add(2025U, (ISkill)new Synthese());
            this.skillHandlers.Add(2455U, (ISkill)new Cabin());
            this.skillHandlers.Add(1004U, (ISkill)new GadenMaster());
            this.skillHandlers.Add(1005U, (ISkill)new Topiary());
            this.skillHandlers.Add(1007U, (ISkill)new Gardening());
            this.skillHandlers.Add(2452U, (ISkill)new WeatherControl());
            this.skillHandlers.Add(2451U, (ISkill)new HeavenlyControl());
            this.skillHandlers.Add(2454U, (ISkill)new Gathering());
        }

        /// <summary>
        /// The SendPetGrowth.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="growType">The growType<see cref="SSMG_ACTOR_PET_GROW.GrowType"/>.</param>
        /// <param name="value">The value<see cref="uint"/>.</param>
        private void SendPetGrowth(SagaDB.Actor.Actor actor, SSMG_ACTOR_PET_GROW.GrowType growType, uint value)
        {
            if (actor.type != ActorType.PET)
                return;
            ActorPet actorPet = (ActorPet)actor;
            if (actorPet.Owner == null || !actorPet.Owner.Online)
                return;
            MapClient.FromActorPC(actorPet.Owner).netIO.SendPacket((Packet)new SSMG_ACTOR_PET_GROW()
            {
                PetActorID = (!actorPet.Ride ? actorPet.ActorID : actorPet.Owner.ActorID),
                OwnerActorID = actorPet.Owner.ActorID,
                Type = growType,
                Value = value
            });
        }

        /// <summary>
        /// The ProcessPetGrowth.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="reason">The reason<see cref="PetGrowthReason"/>.</param>
        public void ProcessPetGrowth(SagaDB.Actor.Actor actor, PetGrowthReason reason)
        {
            if (actor.type != ActorType.PET)
                return;
            ActorPet actorPet = (ActorPet)actor;
            if (!actorPet.Owner.Online)
                return;
            SSMG_ACTOR_PET_GROW.GrowType growType = SSMG_ACTOR_PET_GROW.GrowType.HP;
            if (!actorPet.Owner.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            SagaDB.Item.Item equipment = actorPet.Owner.Inventory.Equipments[EnumEquipSlot.PET];
            int num1;
            switch (reason)
            {
                case PetGrowthReason.PhysicalBeenHit:
                case PetGrowthReason.MagicalBeenHit:
                case PetGrowthReason.PhysicalHit:
                case PetGrowthReason.ItemRecover:
                    num1 = 3;
                    break;
                case PetGrowthReason.UseSkill:
                case PetGrowthReason.SkillHit:
                case PetGrowthReason.CriticalHit:
                    num1 = 10;
                    break;
            }
            int num2 = !actorPet.Ride ? 5 : 5;
            if (SagaLib.Global.Random.Next(0, 99) < num2)
            {
                equipment.Refine = (ushort)1;
                if (!actorPet.Ride)
                {
                    if (reason == PetGrowthReason.PhysicalBeenHit)
                    {

                    }
                    else if (reason == PetGrowthReason.MagicalBeenHit)
                    {
                        switch (SagaLib.Global.Random.Next(0, 4))
                        {
                            case 0:
                                if ((long)actorPet.Limits.hp > (long)equipment.HP)
                                {
                                    ++equipment.HP;
                                    ++actorPet.MaxHP;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HP;
                                    break;
                                }
                                break;
                            case 1:
                                if ((int)actorPet.Limits.atk_max > (int)equipment.Atk1)
                                {
                                    ++equipment.Atk1;
                                    ++equipment.Atk2;
                                    ++equipment.Atk3;
                                    ++actorPet.Status.min_atk1;
                                    ++actorPet.Status.max_atk1;
                                    ++actorPet.Status.min_atk2;
                                    ++actorPet.Status.max_atk2;
                                    ++actorPet.Status.min_atk3;
                                    ++actorPet.Status.max_atk3;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.ATK1;
                                    break;
                                }
                                break;
                            case 2:
                                if ((int)actorPet.Limits.hit_melee > (int)equipment.HitMelee)
                                {
                                    ++equipment.HitMelee;
                                    ++actorPet.Status.hit_melee;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HitMelee;
                                    break;
                                }
                                break;
                            case 3:
                                if ((int)actorPet.Limits.hit_ranged > (int)equipment.HitRanged)
                                {
                                    ++equipment.HitRanged;
                                    ++actorPet.Status.hit_ranged;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HitRanged;
                                    break;
                                }
                                break;
                            case 4:
                                if ((int)actorPet.Limits.aspd > (int)equipment.ASPD)
                                {
                                    ++equipment.ASPD;
                                    ++actorPet.Status.aspd;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.ASPD;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (reason == PetGrowthReason.UseSkill)
                    {
                        switch (SagaLib.Global.Random.Next(0, 2))
                        {
                            case 0:
                                if ((int)actorPet.Limits.matk_max > (int)equipment.MAtk)
                                {
                                    ++equipment.MAtk;
                                    ++actorPet.Status.min_matk;
                                    ++actorPet.Status.max_matk;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MATK;
                                    break;
                                }
                                break;
                            case 1:
                                if ((int)actorPet.Limits.hit_ranged > (int)equipment.HitMagic)
                                {
                                    ++equipment.HitMagic;
                                    ++actorPet.Status.hit_magic;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HitMagic;
                                    break;
                                }
                                break;
                            case 2:
                                if ((int)actorPet.Limits.cspd > (int)equipment.CSPD)
                                {
                                    ++equipment.CSPD;
                                    ++actorPet.Status.cspd;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.CSPD;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (reason == PetGrowthReason.PhysicalHit)
                    {
                        switch (SagaLib.Global.Random.Next(0, 3))
                        {
                            case 0:
                                if ((long)actorPet.Limits.hp > (long)equipment.HP)
                                {
                                    ++equipment.HP;
                                    ++actorPet.MaxHP;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HP;
                                    break;
                                }
                                break;
                            case 1:
                                if ((int)actorPet.Limits.def_add > (int)equipment.Def)
                                {
                                    ++equipment.Def;
                                    ++actorPet.Status.def_add;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.Def;
                                    break;
                                }
                                break;
                            case 2:
                                if ((int)actorPet.Limits.avoid_melee > (int)equipment.AvoidMelee)
                                {
                                    ++equipment.AvoidMelee;
                                    ++actorPet.Status.avoid_melee;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.AvoidMelee;
                                    break;
                                }
                                break;
                            case 3:
                                if ((int)actorPet.Limits.avoid_ranged > (int)equipment.AvoidRanged)
                                {
                                    ++equipment.AvoidRanged;
                                    ++actorPet.Status.avoid_ranged;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.AvoidRanged;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (reason == PetGrowthReason.SkillHit)
                    {
                        switch (SagaLib.Global.Random.Next(0, 4))
                        {
                            case 0:
                                if ((long)actorPet.Limits.hp > (long)equipment.HP)
                                {
                                    ++equipment.HP;
                                    ++actorPet.MaxHP;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HP;
                                    break;
                                }
                                break;
                            case 1:
                                if ((int)actorPet.Limits.mdef_add > (int)equipment.MDef)
                                {
                                    ++equipment.MDef;
                                    ++actorPet.Status.mdef_add;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MDef;
                                    break;
                                }
                                break;
                            case 2:
                                if ((long)actorPet.Limits.mp > (long)equipment.MPRecover)
                                {
                                    ++equipment.MPRecover;
                                    ++actorPet.Status.mp_recover_skill;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MPRecover;
                                    break;
                                }
                                break;
                            case 3:
                                if ((int)actorPet.Limits.avoid_ranged > (int)equipment.AvoidRanged)
                                {
                                    ++equipment.AvoidRanged;
                                    ++actorPet.Status.avoid_ranged;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.AvoidRanged;
                                    break;
                                }
                                break;
                            case 4:
                                if ((int)actorPet.Limits.def_add > (int)equipment.Def)
                                {
                                    ++equipment.Def;
                                    ++actorPet.Status.def_add;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.Def;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    if (reason == PetGrowthReason.PhysicalBeenHit)
                    {
                        switch (SagaLib.Global.Random.Next(0, 8))
                        {
                            case 0:
                                if ((long)actorPet.Limits.hp > (long)equipment.HP)
                                {
                                    ++equipment.HP;
                                    ++actorPet.MaxHP;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HP;
                                    break;
                                }
                                break;
                            case 1:
                                if ((int)actorPet.Limits.hit_melee > (int)equipment.HitMelee)
                                {
                                    ++equipment.HitMelee;
                                    ++actorPet.Status.hit_melee;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HitMelee;
                                    break;
                                }
                                break;
                            case 2:
                                if ((int)actorPet.Limits.hit_ranged > (int)equipment.HitRanged)
                                {
                                    ++equipment.HitRanged;
                                    ++actorPet.Status.hit_ranged;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HitRanged;
                                    break;
                                }
                                break;
                            case 3:
                                if ((int)actorPet.Limits.aspd > (int)equipment.ASPD)
                                {
                                    ++equipment.ASPD;
                                    ++actorPet.Status.aspd;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.ASPD;
                                    break;
                                }
                                break;
                            case 4:
                                if ((int)actorPet.Limits.matk_max > (int)equipment.MAtk)
                                {
                                    ++equipment.MAtk;
                                    ++actorPet.Status.min_matk;
                                    ++actorPet.Status.max_matk;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MATK;
                                    break;
                                }
                                break;
                            case 5:
                                if ((int)actorPet.Limits.hit_ranged > (int)equipment.HitMagic)
                                {
                                    ++equipment.HitMagic;
                                    ++actorPet.Status.hit_magic;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HitMagic;
                                    break;
                                }
                                break;
                            case 6:
                                if ((int)actorPet.Limits.cspd > (int)equipment.CSPD)
                                {
                                    ++equipment.CSPD;
                                    ++actorPet.Status.cspd;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.CSPD;
                                    break;
                                }
                                break;
                            case 7:
                                if ((int)actorPet.Limits.aspd > (int)equipment.ASPD)
                                {
                                    ++equipment.ASPD;
                                    ++actorPet.Status.aspd;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.ASPD;
                                    break;
                                }
                                break;
                            case 8:
                                if ((int)actorPet.Limits.atk_max > (int)equipment.Atk1)
                                {
                                    ++equipment.Atk1;
                                    ++equipment.Atk2;
                                    ++equipment.Atk3;
                                    ++actorPet.Status.min_atk1;
                                    ++actorPet.Status.max_atk1;
                                    ++actorPet.Status.min_atk2;
                                    ++actorPet.Status.max_atk2;
                                    ++actorPet.Status.min_atk3;
                                    ++actorPet.Status.max_atk3;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.ATK1;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (reason == PetGrowthReason.UseSkill)
                    {
                        switch (SagaLib.Global.Random.Next(0, 4))
                        {
                            case 0:
                                if ((int)actorPet.Limits.hit_ranged > (int)equipment.HitMagic)
                                {
                                    ++equipment.HitMagic;
                                    ++actorPet.Status.hit_magic;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HitMagic;
                                    break;
                                }
                                break;
                            case 1:
                                if ((int)actorPet.Limits.matk_max > (int)equipment.MAtk)
                                {
                                    ++equipment.MAtk;
                                    ++actorPet.Status.min_matk;
                                    ++actorPet.Status.max_matk;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MATK;
                                    break;
                                }
                                break;
                            case 2:
                                if ((int)actorPet.Limits.cspd > (int)equipment.CSPD)
                                {
                                    ++equipment.CSPD;
                                    ++actorPet.Status.cspd;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.CSPD;
                                    break;
                                }
                                break;
                            case 3:
                                if ((int)actorPet.Limits.mdef_add > (int)equipment.MDef)
                                {
                                    ++equipment.MDef;
                                    ++actorPet.Status.mdef_add;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MDef;
                                    break;
                                }
                                break;
                            case 4:
                                if ((long)actorPet.Limits.mp > (long)equipment.MPRecover)
                                {
                                    ++equipment.MPRecover;
                                    ++actorPet.Status.mp_recover_skill;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MPRecover;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (reason == PetGrowthReason.ItemRecover)
                    {
                        switch (SagaLib.Global.Random.Next(0, 2))
                        {
                            case 0:
                                if ((long)actorPet.Limits.hp > (long)equipment.HP)
                                {
                                    ++equipment.HP;
                                    ++actorPet.MaxHP;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.HP;
                                    break;
                                }
                                break;
                            case 1:
                                if ((long)actorPet.Limits.mp > (long)equipment.MPRecover)
                                {
                                    ++equipment.MPRecover;
                                    ++actorPet.Status.mp_recover_skill;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.MPRecover;
                                    break;
                                }
                                break;
                            case 2:
                                if ((long)actorPet.Limits.hp > (long)equipment.HPRecover)
                                {
                                    ++equipment.HPRecover;
                                    ++actorPet.Status.hp_recover_skill;
                                    growType = SSMG_ACTOR_PET_GROW.GrowType.Recover;
                                    break;
                                }
                                break;
                        }
                    }
                }
                if (actorPet.Owner.Online)
                {
                    if (actorPet.Ride)
                    {
                        Singleton<StatusFactory>.Instance.CalcStatus(actorPet.Owner);
                        MapClient.FromActorPC(actorPet.Owner).SendPlayerInfo();
                    }
                    MapClient.FromActorPC(actorPet.Owner).SendItemInfo(equipment);
                }
                this.SendPetGrowth(actor, growType, 1U);
            }
        }

        /// <summary>
        /// The ItemUse.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        public void ItemUse(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg)
        {
            this.ItemUse(sActor, new List<SagaDB.Actor.Actor>() { dActor }, arg);
        }

        /// <summary>
        /// The ItemUse.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        public void ItemUse(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg)
        {
            int index1 = 0;
            arg.affectedActors = dActor;
            arg.Init();
            foreach (SagaDB.Actor.Actor sActor1 in dActor)
            {
                sActor1.HP = (uint)((ulong)sActor1.HP + (ulong)arg.item.BaseData.hp);
                sActor1.SP = (uint)((ulong)sActor1.SP + (ulong)arg.item.BaseData.sp);
                sActor1.MP = (uint)((ulong)sActor1.MP + (ulong)arg.item.BaseData.mp);
                if (sActor1.HP > sActor1.MaxHP)
                    sActor1.HP = sActor1.MaxHP;
                if (sActor1.SP > sActor1.MaxSP)
                    sActor1.SP = sActor1.MaxSP;
                if (sActor1.MP > sActor1.MaxMP)
                    sActor1.MP = sActor1.MaxMP;
                if (arg.item.BaseData.hp > (short)0)
                {
                    List<AttackFlag> flag;
                    int index2;
                    (flag = arg.flag)[index2 = index1] = flag[index2] | AttackFlag.HP_HEAL;
                    arg.hp[index1] = (int)-arg.item.BaseData.hp;
                }
                else if (arg.item.BaseData.hp < (short)0)
                {
                    List<AttackFlag> flag;
                    int index2;
                    (flag = arg.flag)[index2 = index1] = flag[index2] | AttackFlag.HP_DAMAGE;
                    arg.hp[index1] = (int)-arg.item.BaseData.hp;
                }
                if (arg.item.BaseData.sp > (short)0)
                {
                    List<AttackFlag> flag;
                    int index2;
                    (flag = arg.flag)[index2 = index1] = flag[index2] | AttackFlag.SP_HEAL;
                    arg.sp[index1] = (int)-arg.item.BaseData.sp;
                }
                else if (arg.item.BaseData.sp < (short)0)
                {
                    List<AttackFlag> flag;
                    int index2;
                    (flag = arg.flag)[index2 = index1] = flag[index2] | AttackFlag.SP_DAMAGE;
                    arg.sp[index1] = (int)-arg.item.BaseData.sp;
                }
                if (arg.item.BaseData.mp > (short)0)
                {
                    List<AttackFlag> flag;
                    int index2;
                    (flag = arg.flag)[index2 = index1] = flag[index2] | AttackFlag.MP_HEAL;
                    arg.mp[index1] = (int)-arg.item.BaseData.mp;
                }
                else if (arg.item.BaseData.mp < (short)0)
                {
                    List<AttackFlag> flag;
                    int index2;
                    (flag = arg.flag)[index2 = index1] = flag[index2] | AttackFlag.MP_DAMAGE;
                    arg.mp[index1] = (int)-arg.item.BaseData.mp;
                }
                ++index1;
                Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, sActor1, true);
            }
            arg.delay = arg.item.BaseData.delay;
        }

        /// <summary>
        /// The elementOld.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float elementOld(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, Elements element)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(dActor.MapID);
            float num1 = 1f;
            byte num2 = SagaLib.Global.PosX16to8(dActor.X, map.Width);
            byte num3 = SagaLib.Global.PosY16to8(dActor.Y, map.Height);
            byte num4;
            switch (element)
            {
                case Elements.Fire:
                    byte num5 = 0;
                    byte num6 = 0;
                    byte num7 = 0;
                    byte num8 = 0;
                    byte num9 = 0;
                    if ((int)num2 < (int)map.Info.width && (int)num3 < (int)map.Height)
                    {
                        num5 = map.Info.fire[(int)num2, (int)num3];
                        num6 = map.Info.water[(int)num2, (int)num3];
                        num7 = map.Info.wind[(int)num2, (int)num3];
                        num8 = map.Info.holy[(int)num2, (int)num3];
                        num9 = map.Info.dark[(int)num2, (int)num3];
                    }
                    byte num10 = (byte)((uint)num5 + (uint)(byte)dActor.Elements[Elements.Fire]);
                    byte num11 = (byte)((uint)num6 + (uint)(byte)dActor.Elements[Elements.Water]);
                    byte num12 = (byte)((uint)num7 + (uint)(byte)dActor.Elements[Elements.Wind]);
                    byte num13 = (byte)((uint)num8 + (uint)(byte)dActor.Elements[Elements.Holy]);
                    byte num14 = (byte)((uint)num9 + (uint)(byte)dActor.Elements[Elements.Dark]);
                    if (num10 > (byte)100)
                        num10 = (byte)100;
                    if (num11 > (byte)100)
                        num11 = (byte)100;
                    if (num12 > (byte)100)
                        num12 = (byte)100;
                    if (num13 > (byte)100)
                        num13 = (byte)100;
                    if (num14 > (byte)100)
                        num14 = (byte)100;
                    num1 = (float)(1.0 + ((double)num11 * 0.660000026226044 - (double)num10 - (double)num12 * 0.5) / 100.0 * (1.0 + ((double)num13 * 0.330000013113022 - (double)num14 * 0.330000013113022) / 100.0));
                    break;
                case Elements.Water:
                    byte num15 = 0;
                    byte num16 = 0;
                    byte num17 = 0;
                    byte num18 = 0;
                    byte num19 = 0;
                    if ((int)num2 < (int)map.Info.width && (int)num3 < (int)map.Height)
                    {
                        num15 = map.Info.fire[(int)num2, (int)num3];
                        num16 = map.Info.water[(int)num2, (int)num3];
                        num17 = map.Info.earth[(int)num2, (int)num3];
                        num18 = map.Info.holy[(int)num2, (int)num3];
                        num19 = map.Info.dark[(int)num2, (int)num3];
                    }
                    byte num20 = (byte)((uint)num15 + (uint)(byte)dActor.Elements[Elements.Fire]);
                    byte num21 = (byte)((uint)num16 + (uint)(byte)dActor.Elements[Elements.Water]);
                    byte num22 = (byte)((uint)num17 + (uint)(byte)dActor.Elements[Elements.Earth]);
                    byte num23 = (byte)((uint)num18 + (uint)(byte)dActor.Elements[Elements.Holy]);
                    byte num24 = (byte)((uint)num19 + (uint)(byte)dActor.Elements[Elements.Dark]);
                    if (num20 > (byte)100)
                        num20 = (byte)100;
                    if (num21 > (byte)100)
                        num21 = (byte)100;
                    if (num22 > (byte)100)
                        num22 = (byte)100;
                    if (num23 > (byte)100)
                        num23 = (byte)100;
                    if (num24 > (byte)100)
                        num24 = (byte)100;
                    num1 = (float)(1.0 + ((double)num22 * 0.660000026226044 - (double)num21 - (double)num20 * 0.5) / 100.0 * (1.0 + ((double)num23 * 0.330000013113022 - (double)num24 * 0.330000013113022) / 100.0));
                    break;
                case Elements.Wind:
                    byte num25 = 0;
                    byte num26 = 0;
                    byte num27 = 0;
                    byte num28 = 0;
                    byte num29 = 0;
                    if ((int)num2 < (int)map.Info.width && (int)num3 < (int)map.Height)
                    {
                        num25 = map.Info.fire[(int)num2, (int)num3];
                        num26 = map.Info.earth[(int)num2, (int)num3];
                        num27 = map.Info.wind[(int)num2, (int)num3];
                        num28 = map.Info.holy[(int)num2, (int)num3];
                        num29 = map.Info.dark[(int)num2, (int)num3];
                    }
                    byte num30 = (byte)((uint)num25 + (uint)(byte)dActor.Elements[Elements.Fire]);
                    byte num31 = (byte)((uint)num27 + (uint)(byte)dActor.Elements[Elements.Wind]);
                    byte num32 = (byte)((uint)num26 + (uint)(byte)dActor.Elements[Elements.Earth]);
                    byte num33 = (byte)((uint)num28 + (uint)(byte)dActor.Elements[Elements.Holy]);
                    byte num34 = (byte)((uint)num29 + (uint)(byte)dActor.Elements[Elements.Dark]);
                    if (num30 > (byte)100)
                        num30 = (byte)100;
                    if (num31 > (byte)100)
                        num31 = (byte)100;
                    if (num32 > (byte)100)
                        num32 = (byte)100;
                    if (num33 > (byte)100)
                        num33 = (byte)100;
                    if (num34 > (byte)100)
                        num34 = (byte)100;
                    num1 = (float)(1.0 + ((double)num30 * 0.660000026226044 - (double)num31 - (double)num32 * 0.5) / 100.0 * (1.0 + ((double)num33 * 0.330000013113022 - (double)num34 * 0.330000013113022) / 100.0));
                    break;
                case Elements.Earth:
                    byte num35 = 0;
                    byte num36 = 0;
                    byte num37 = 0;
                    byte num38 = 0;
                    byte num39 = 0;
                    if ((int)num2 < (int)map.Info.width && (int)num3 < (int)map.Height)
                    {
                        num37 = map.Info.wind[(int)num2, (int)num3];
                        num36 = map.Info.water[(int)num2, (int)num3];
                        num35 = map.Info.earth[(int)num2, (int)num3];
                        num38 = map.Info.holy[(int)num2, (int)num3];
                        num39 = map.Info.dark[(int)num2, (int)num3];
                    }
                    byte num40 = (byte)((uint)num36 + (uint)(byte)dActor.Elements[Elements.Water]);
                    byte num41 = (byte)((uint)num37 + (uint)(byte)dActor.Elements[Elements.Wind]);
                    byte num42 = (byte)((uint)num35 + (uint)(byte)dActor.Elements[Elements.Earth]);
                    byte num43 = (byte)((uint)num38 + (uint)(byte)dActor.Elements[Elements.Holy]);
                    byte num44 = (byte)((uint)num39 + (uint)(byte)dActor.Elements[Elements.Dark]);
                    if (num40 > (byte)100)
                        num40 = (byte)100;
                    if (num41 > (byte)100)
                        num41 = (byte)100;
                    if (num42 > (byte)100)
                        num42 = (byte)100;
                    if (num43 > (byte)100)
                        num43 = (byte)100;
                    if (num44 > (byte)100)
                        num44 = (byte)100;
                    num1 = (float)(1.0 + ((double)num41 * 0.660000026226044 - (double)num42 - (double)num40 * 0.5) / 100.0 * (1.0 + ((double)num43 * 0.330000013113022 - (double)num44 * 0.330000013113022) / 100.0));
                    break;
                case Elements.Holy:
                    byte num45 = 0;
                    byte num46 = 0;
                    byte num47 = 0;
                    byte num48 = 0;
                    byte num49 = 0;
                    byte num50 = 0;
                    num4 = (byte)0;
                    if ((int)num2 < (int)map.Info.width && (int)num3 < (int)map.Height)
                    {
                        num45 = map.Info.fire[(int)num2, (int)num3];
                        num46 = map.Info.water[(int)num2, (int)num3];
                        num48 = map.Info.earth[(int)num2, (int)num3];
                        num47 = map.Info.wind[(int)num2, (int)num3];
                        num49 = map.Info.holy[(int)num2, (int)num3];
                        num50 = map.Info.dark[(int)num2, (int)num3];
                    }
                    byte num51 = (byte)((uint)num45 + (uint)(byte)dActor.Elements[Elements.Fire]);
                    byte num52 = (byte)((uint)num46 + (uint)(byte)dActor.Elements[Elements.Water]);
                    byte num53 = (byte)((uint)num47 + (uint)(byte)dActor.Elements[Elements.Wind]);
                    byte num54 = (byte)((uint)num48 + (uint)(byte)dActor.Elements[Elements.Earth]);
                    byte num55 = (byte)((uint)num49 + (uint)(byte)dActor.Elements[Elements.Holy]);
                    byte num56 = (byte)((uint)num50 + (uint)(byte)dActor.Elements[Elements.Dark]);
                    if (num51 > (byte)100)
                        num51 = (byte)100;
                    if (num52 > (byte)100)
                        num52 = (byte)100;
                    if (num53 > (byte)100)
                        num53 = (byte)100;
                    if (num54 > (byte)100)
                        num54 = (byte)100;
                    if (num55 > (byte)100)
                        num55 = (byte)100;
                    if (num56 > (byte)100)
                        num56 = (byte)100;
                    byte num57 = num51;
                    if ((int)num52 > (int)num57)
                        num57 = num52;
                    if ((int)num54 > (int)num57)
                        num57 = num54;
                    if ((int)num53 > (int)num57)
                        num57 = num53;
                    num1 = (float)(1.0 + (((double)num56 * 0.660000026226044 - (double)num55) / 100.0 - (double)num57 * 0.5 / 100.0));
                    break;
                case Elements.Dark:
                    byte num58 = 0;
                    byte num59 = 0;
                    byte num60 = 0;
                    byte num61 = 0;
                    byte num62 = 0;
                    byte num63 = 0;
                    num4 = (byte)0;
                    if ((int)num2 < (int)map.Info.width && (int)num3 < (int)map.Height)
                    {
                        num58 = map.Info.fire[(int)num2, (int)num3];
                        num59 = map.Info.water[(int)num2, (int)num3];
                        num60 = map.Info.earth[(int)num2, (int)num3];
                        num61 = map.Info.wind[(int)num2, (int)num3];
                        num62 = map.Info.holy[(int)num2, (int)num3];
                        num63 = map.Info.dark[(int)num2, (int)num3];
                    }
                    byte num64 = (byte)((uint)num58 + (uint)(byte)dActor.Elements[Elements.Fire]);
                    byte num65 = (byte)((uint)num59 + (uint)(byte)dActor.Elements[Elements.Water]);
                    byte num66 = (byte)((uint)num61 + (uint)(byte)dActor.Elements[Elements.Wind]);
                    byte num67 = (byte)((uint)num60 + (uint)(byte)dActor.Elements[Elements.Earth]);
                    byte num68 = (byte)((uint)num62 + (uint)(byte)dActor.Elements[Elements.Holy]);
                    byte num69 = (byte)((uint)num63 + (uint)(byte)dActor.Elements[Elements.Dark]);
                    if (num64 > (byte)100)
                        num64 = (byte)100;
                    if (num65 > (byte)100)
                        num65 = (byte)100;
                    if (num66 > (byte)100)
                        num66 = (byte)100;
                    if (num67 > (byte)100)
                        num67 = (byte)100;
                    if (num68 > (byte)100)
                        num68 = (byte)100;
                    if (num69 > (byte)100)
                        num69 = (byte)100;
                    byte num70 = num64;
                    if ((int)num65 > (int)num70)
                        num70 = num65;
                    if ((int)num67 > (int)num70)
                        num70 = num67;
                    if ((int)num66 > (int)num70)
                        num70 = num66;
                    num1 = (float)(1.0 + (((double)-num68 * 0.660000026226044 - (double)num69) / 100.0 + (double)num70 * 0.5 / 100.0));
                    break;
            }
            if ((double)num1 < 0.0)
                num1 = 0.0f;
            return num1;
        }

        /// <summary>
        /// The elementField.
        /// </summary>
        /// <param name="map">The map<see cref="SagaMap.Map"/>.</param>
        /// <param name="x">The x<see cref="byte"/>.</param>
        /// <param name="y">The y<see cref="byte"/>.</param>
        /// <param name="value">The value<see cref="int"/>.</param>
        /// <returns>The <see cref="Elements"/>.</returns>
        private Elements elementField(SagaMap.Map map, byte x, byte y, out int value)
        {
            Elements elements = Elements.Neutral;
            value = 0;
            if (map.Info.fire[(int)x, (int)y] != (byte)0)
            {
                elements = Elements.Fire;
                value = (int)map.Info.fire[(int)x, (int)y];
            }
            if (value < (int)map.Info.water[(int)x, (int)y])
            {
                elements = Elements.Water;
                value = (int)map.Info.water[(int)x, (int)y];
            }
            if (value < (int)map.Info.wind[(int)x, (int)y])
            {
                elements = Elements.Wind;
                value = (int)map.Info.wind[(int)x, (int)y];
            }
            if (value < (int)map.Info.earth[(int)x, (int)y])
            {
                elements = Elements.Earth;
                value = (int)map.Info.earth[(int)x, (int)y];
            }
            if (value < (int)map.Info.holy[(int)x, (int)y])
            {
                elements = Elements.Holy;
                value = (int)map.Info.holy[(int)x, (int)y];
            }
            if (value < (int)map.Info.dark[(int)x, (int)y])
            {
                elements = Elements.Dark;
                value = (int)map.Info.dark[(int)x, (int)y];
            }
            return elements;
        }

        /// <summary>
        /// The elementEffect.
        /// </summary>
        /// <param name="src">The src<see cref="Elements"/>.</param>
        /// <param name="dst">The dst<see cref="Elements"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int elementEffect(Elements src, Elements dst)
        {
            return this.elementEffects[(int)src, (int)dst];
        }

        /// <summary>
        /// The elementLevel.
        /// </summary>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int elementLevel(int elementValue)
        {
            if (elementValue > 100)
                elementValue = 100;
            if (elementValue < 10)
                return 0;
            return elementValue / 5 - 1;
        }

        /// <summary>
        /// The elementNew.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="heal">The heal<see cref="bool"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float elementNew(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, Elements element, int elementValue, bool heal)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(dActor.MapID);
            byte x = SagaLib.Global.PosX16to8(dActor.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(dActor.Y, map.Height);
            int num1 = 0;
            Elements elements = this.elementField(map, x, y, out num1);
            Elements src = Elements.Neutral;
            int num2 = 0;
            foreach (Elements key in sActor.AttackElements.Keys)
            {
                if (num2 < sActor.AttackElements[key] + sActor.Status.attackElements_item[key])
                {
                    src = key;
                    num2 = sActor.AttackElements[key] + sActor.Status.attackElements_item[key];
                }
            }
            if (element != Elements.Neutral)
            {
                if (element == src)
                {
                    num2 += elementValue;
                }
                else
                {
                    src = element;
                    num2 = elementValue;
                }
            }
            if (src != Elements.Neutral && src == elements)
                num2 += num1;
            Elements dst = Elements.Neutral;
            int elementValue1 = 0;
            foreach (Elements key in dActor.Elements.Keys)
            {
                if (elementValue1 < dActor.Elements[key] + dActor.Status.elements_item[key])
                {
                    dst = key;
                    elementValue1 = dActor.Elements[key] + dActor.Status.elements_item[key];
                }
            }
            if (dst != Elements.Neutral && dst == elements)
                elementValue1 += num1;
            int index;
            float num3;
            if (heal)
            {
                index = this.elementEffect(Elements.Neutral, Elements.Neutral);
                num3 = this.elementFactor[index][this.elementLevel(0)];
            }
            else
            {
                index = this.elementEffect(src, dst);
                num3 = this.elementFactor[index][this.elementLevel(elementValue1)];
            }
            if (index <= 1)
                num3 += (float)num2 / 100f;
            if (index == 2)
                num3 += (float)num2 / 400f;
            if (heal)
            {
                switch (dst)
                {
                    case Elements.Holy:
                        num3 += 0.1f * (float)this.elementLevel(elementValue1);
                        break;
                    case Elements.Dark:
                        num3 -= 0.05f * (float)this.elementLevel(elementValue1);
                        break;
                }
            }
            if ((double)num3 < 0.0)
                num3 = 0.0f;
            return num3;
        }

        /// <summary>
        /// The CalcElementBonus.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="heal">The heal<see cref="bool"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float CalcElementBonus(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, Elements element, int elementValue, bool heal)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga9_Iris)
                return this.elementNew(sActor, dActor, element, elementValue, heal);
            if (heal)
                return 0.0f;
            return this.elementOld(sActor, dActor, element);
        }

        /// <summary>
        /// The GetDirection.
        /// </summary>
        /// <param name="sActor">人物.</param>
        /// <returns>方向.</returns>
        public SkillHandler.ActorDirection GetDirection(SagaDB.Actor.Actor sActor)
        {
            return (SkillHandler.ActorDirection)Math.Ceiling((double)((int)sActor.Dir / 45));
        }

        /// <summary>
        /// The GetXYDiff.
        /// </summary>
        /// <param name="map">地圖.</param>
        /// <param name="sActor">使用技能的角色.</param>
        /// <param name="dActor">目標角色.</param>
        /// <param name="XDiff">回傳X的差異(格).</param>
        /// <param name="YDiff">回傳Y的差異(格).</param>
        public void GetXYDiff(SagaMap.Map map, SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, out int XDiff, out int YDiff)
        {
            XDiff = (int)SagaLib.Global.PosX16to8(dActor.X, map.Width) - (int)SagaLib.Global.PosX16to8(sActor.X, map.Width);
            YDiff = (int)SagaLib.Global.PosY16to8(sActor.Y, map.Height) - (int)SagaLib.Global.PosY16to8(dActor.Y, map.Height);
        }

        /// <summary>
        /// The CalcPosHashCode.
        /// </summary>
        /// <param name="x">X座標.</param>
        /// <param name="y">Y座標.</param>
        /// <param name="SkillRange">技能範圍(EX.3x3=3) .</param>
        /// <returns>座標之Hash值.</returns>
        public int CalcPosHashCode(int x, int y, int SkillRange)
        {
            return (x + SkillRange) * 100 + (y + SkillRange);
        }

        /// <summary>
        /// The GetRelatedPos.
        /// </summary>
        /// <param name="sActor">基準人物(原點).</param>
        /// <param name="XDiff">X座標偏移量(單位：格).</param>
        /// <param name="YDiff">Y座標偏移量(單位：格).</param>
        /// <param name="nx">回傳X.</param>
        /// <param name="ny">回傳Y.</param>
        /// <returns>是否正常(無溢位發生).</returns>
        public bool GetRelatedPos(SagaDB.Actor.Actor sActor, int XDiff, int YDiff, out short nx, out short ny)
        {
            byte ny1 = 0;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            byte sx = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte sy = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            byte nx1;
            bool relatedPos = this.GetRelatedPos(sActor, XDiff, YDiff, sx, sy, out nx1, out ny1);
            nx = SagaLib.Global.PosX8to16(nx1, map.Width);
            ny = SagaLib.Global.PosY8to16(ny1, map.Height);
            return relatedPos;
        }

        /// <summary>
        /// The GetRelatedPos.
        /// </summary>
        /// <param name="sActor">基準人物(原點).</param>
        /// <param name="XDiff">X座標偏移量(單位：格).</param>
        /// <param name="YDiff">Y座標偏移量(單位：格).</param>
        /// <param name="nx">回傳X.</param>
        /// <param name="ny">回傳Y.</param>
        /// <returns>是否正常(無溢位發生).</returns>
        public bool GetRelatedPos(SagaDB.Actor.Actor sActor, int XDiff, int YDiff, out byte nx, out byte ny)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            byte sx = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte sy = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            return this.GetRelatedPos(sActor, XDiff, YDiff, sx, sy, out nx, out ny);
        }

        /// <summary>
        /// The GetRelatedPos.
        /// </summary>
        /// <param name="sActor">基準人物.</param>
        /// <param name="XDiff">X座標偏移量(單位：格).</param>
        /// <param name="YDiff">Y座標偏移量(單位：格).</param>
        /// <param name="sx">原點X.</param>
        /// <param name="sy">原點Y.</param>
        /// <param name="nx">回傳X.</param>
        /// <param name="ny">回傳Y.</param>
        /// <returns>是否正常(無溢位發生).</returns>
        public bool GetRelatedPos(SagaDB.Actor.Actor sActor, int XDiff, int YDiff, byte sx, byte sy, out byte nx, out byte ny)
        {
            Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            byte num1 = sx;
            byte num2 = sy;
            if (num1 == (byte)0 && XDiff < 0 || num1 == byte.MaxValue && XDiff > 0 || num2 == (byte)0 && YDiff < 0 || num2 == byte.MaxValue && YDiff > 0)
            {
                nx = num1;
                ny = num2;
                return false;
            }
            nx = (byte)((uint)num1 + (uint)XDiff);
            ny = (byte)((uint)num2 + (uint)YDiff);
            return true;
        }

        /// <summary>
        /// The GetPossesionedActor.
        /// </summary>
        /// <param name="sActor">角色.</param>
        /// <returns>被憑依者.</returns>
        public ActorPC GetPossesionedActor(ActorPC sActor)
        {
            if (sActor.PossessionTarget == 0U)
                return sActor;
            SagaDB.Actor.Actor actor = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActor(sActor.PossessionTarget);
            if (actor != null && actor.type == ActorType.PC)
                return (ActorPC)actor;
            return sActor;
        }

        /// <summary>
        /// The AttractMob.
        /// </summary>
        /// <param name="sActor">施放技能者.</param>
        /// <param name="dActor">目標.</param>
        public void AttractMob(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor)
        {
            this.AttractMob(sActor, dActor, 1000);
        }

        /// <summary>
        /// The AttractMob.
        /// </summary>
        /// <param name="sActor">施放技能者.</param>
        /// <param name="dActor">目標.</param>
        /// <param name="damage">給予的傷害.</param>
        public void AttractMob(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, int damage)
        {
            if (dActor.type != ActorType.MOB)
                return;
            MobEventHandler e = (MobEventHandler)dActor.e;
            e.AI.OnAttacked(sActor, 10);
            Dictionary<uint, int> damageTable;
            uint actorId;
            (damageTable = e.AI.DamageTable)[actorId = sActor.ActorID] = damageTable[actorId] + damage;
        }

        /// <summary>
        /// The isBossMob.
        /// </summary>
        /// <param name="mob">.</param>
        /// <returns>.</returns>
        public bool isBossMob(SagaDB.Actor.Actor mob)
        {
            if (mob.type != ActorType.MOB)
                return false;
            return this.isBossMob((ActorMob)mob);
        }

        /// <summary>
        /// The isBossMob.
        /// </summary>
        /// <param name="mob">.</param>
        /// <returns>.</returns>
        public bool isBossMob(ActorMob mob)
        {
            return this.CheckMobType(mob, "boss");
        }

        /// <summary>
        /// The CanAdditionApply.
        /// </summary>
        /// <param name="sActor">賦予者.</param>
        /// <param name="dActor">目標.</param>
        /// <param name="AdditionName">狀態名稱.</param>
        /// <param name="rate">原始成功率.</param>
        /// <returns>是否可被賦予.</returns>
        public bool CanAdditionApply(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, string AdditionName, int rate)
        {
            if (Singleton<SkillHandler>.Instance.isBossMob(dActor) || rate <= 0)
                return false;
            float num = (float)rate;
            if (dActor.Status.Additions.ContainsKey(AdditionName + "Regi"))
                num *= 0.1f;
            if (sActor != dActor && AdditionName == "Poison" && sActor.Status.Additions.ContainsKey("PoisonRateUp"))
                num *= 1.5f;
            if (sActor.Status.Additions.ContainsKey("MagHitUpCircle"))
            {
                MagHitUpCircle.MagHitUpCircleBuff addition = (MagHitUpCircle.MagHitUpCircleBuff)sActor.Status.Additions["MagHitUpCircle"];
                num *= addition.Rate;
            }
            if (sActor.Status.Additions.ContainsKey("AllRateUp"))
            {
                AllRateUp.AllRateUpBuff addition = (AllRateUp.AllRateUpBuff)sActor.Status.Additions["AllRateUp"];
                num *= addition.Rate;
            }
            return (double)SagaLib.Global.Random.Next(0, 99) < (double)num;
        }

        /// <summary>
        /// The CanAdditionApply.
        /// </summary>
        /// <param name="sActor">賦予者.</param>
        /// <param name="dActor">目標.</param>
        /// <param name="theAddition">狀態類型.</param>
        /// <param name="rate">原始成功率.</param>
        /// <returns>是否可被賦予.</returns>
        public bool CanAdditionApply(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillHandler.DefaultAdditions theAddition, int rate)
        {
            if (dActor.type == ActorType.MOB)
            {
                ActorMob actorMob = (ActorMob)dActor;
                int num = (int)theAddition;
                if (num < 9)
                {
                    AbnormalStatus index = (AbnormalStatus)Enum.ToObject(typeof(AbnormalStatus), num);
                    short abnormalStatu = actorMob.AbnormalStatus[index];
                    if (abnormalStatu == (short)100)
                        return false;
                    rate = (int)((double)rate * (1.0 - (double)((int)abnormalStatu / 100)));
                }
            }
            return this.CanAdditionApply(sActor, dActor, theAddition.ToString(), rate);
        }

        /// <summary>
        /// The isEquipmentRight.
        /// </summary>
        /// <param name="sActor">人物.</param>
        /// <param name="ItemType">裝備種類.</param>
        /// <returns>.</returns>
        public bool isEquipmentRight(SagaDB.Actor.Actor sActor, params ItemType[] ItemType)
        {
            if (sActor.type != ActorType.PC)
                return true;
            ActorPC actorPc = (ActorPC)sActor;
            if (!actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                return false;
            foreach (ItemType itemType in ItemType)
            {
                if (actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == itemType)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The CountItem.
        /// </summary>
        /// <param name="pc">人物.</param>
        /// <param name="itemID">道具ID.</param>
        /// <returns>數量.</returns>
        public int CountItem(ActorPC pc, uint itemID)
        {
            SagaDB.Item.Item obj = pc.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (obj != null)
                return (int)obj.Stack;
            return 0;
        }

        /// <summary>
        /// The TakeItem.
        /// </summary>
        /// <param name="pc">人物.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">數量.</param>
        public void TakeItem(ActorPC pc, uint itemID, ushort count)
        {
            MapClient mapClient = MapClient.FromActorPC(pc);
            Logger.LogItemLost(Logger.EventType.ItemNPCLost, pc.Name + "(" + (object)pc.CharID + ")", "(" + (object)itemID + ")", string.Format("SkillTake Count:{0}", (object)count), true);
            mapClient.DeleteItemID(itemID, count, true);
        }

        /// <summary>
        /// The GiveItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">个数.</param>
        /// <param name="identified">是否鉴定.</param>
        /// <returns>The <see cref="List{SagaDB.Item.Item}"/>.</returns>
        public List<SagaDB.Item.Item> GiveItem(ActorPC pc, uint itemID, ushort count, bool identified)
        {
            MapClient mapClient = MapClient.FromActorPC(pc);
            List<SagaDB.Item.Item> objList = new List<SagaDB.Item.Item>();
            for (int index = 0; index < (int)count; ++index)
            {
                SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(itemID);
                obj.Stack = (ushort)1;
                obj.Identified = identified;
                Logger.LogItemGet(Logger.EventType.ItemNPCGet, pc.Name + "(" + (object)pc.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("SkillGive Count:{0}", (object)obj.Stack), true);
                mapClient.AddItem(obj, true);
                objList.Add(obj);
            }
            return objList;
        }

        /// <summary>
        /// The CreateAutoCastInfo.
        /// </summary>
        /// <param name="skillID">技能ID.</param>
        /// <param name="level">等級.</param>
        /// <param name="delay">延遲.</param>
        /// <returns>自動使用技能的資訊.</returns>
        public AutoCastInfo CreateAutoCastInfo(uint skillID, byte level, int delay)
        {
            return new AutoCastInfo()
            {
                delay = delay,
                level = level,
                skillID = skillID
            };
        }

        /// <summary>
        /// The CreateAutoCastInfo.
        /// </summary>
        /// <param name="skillID">技能ID.</param>
        /// <param name="level">等級.</param>
        /// <param name="delay">延遲.</param>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <returns>自動使用技能的資訊.</returns>
        public AutoCastInfo CreateAutoCastInfo(uint skillID, byte level, int delay, byte x, byte y)
        {
            return new AutoCastInfo()
            {
                delay = delay,
                level = level,
                skillID = skillID,
                x = x,
                y = y
            };
        }

        /// <summary>
        /// The FixAttack.
        /// </summary>
        /// <param name="sActor">攻擊者.</param>
        /// <param name="dActor">被攻擊者.</param>
        /// <param name="arg">技能參數.</param>
        /// <param name="element">屬性.</param>
        /// <param name="Damage">傷害.</param>
        public void FixAttack(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg, Elements element, float Damage)
        {
            this.FixAttack(sActor, new List<SagaDB.Actor.Actor>() { dActor }, arg, element, Damage);
        }

        /// <summary>
        /// The FixAttack.
        /// </summary>
        /// <param name="sActor">攻擊者.</param>
        /// <param name="dActor">被攻擊者.</param>
        /// <param name="arg">技能參數.</param>
        /// <param name="element">屬性.</param>
        /// <param name="Damage">傷害.</param>
        public void FixAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, Elements element, float Damage)
        {
            this.MagicAttack(sActor, dActor, arg, SkillHandler.DefType.IgnoreAll, element, 50, Damage, 0, true);
        }

        /// <summary>
        /// The GetPet.
        /// </summary>
        /// <param name="sActor">玩家.</param>
        /// <returns>寵物.</returns>
        public ActorPet GetPet(SagaDB.Actor.Actor sActor)
        {
            if (sActor.type != ActorType.PC)
                return (ActorPet)null;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Pet == null)
                return (ActorPet)null;
            return actorPc.Pet;
        }

        /// <summary>
        /// The GetMobAI.
        /// </summary>
        /// <param name="sActor">玩家.</param>
        /// <returns>寵物AI.</returns>
        public MobAI GetMobAI(SagaDB.Actor.Actor sActor)
        {
            return this.GetMobAI(this.GetPet(sActor));
        }

        /// <summary>
        /// The GetMobAI.
        /// </summary>
        /// <param name="pet">寵物.</param>
        /// <returns>寵物AI.</returns>
        public MobAI GetMobAI(ActorPet pet)
        {
            if (pet == null || pet.Ride)
                return (MobAI)null;
            return ((PetEventHandler)pet.e).AI;
        }

        /// <summary>
        /// The CheckMobType.
        /// </summary>
        /// <param name="mob">怪物.</param>
        /// <param name="MobType">The MobType<see cref="string"/>.</param>
        /// <returns>類型是否正確.</returns>
        public bool CheckMobType(ActorMob mob, string MobType)
        {
            return mob.BaseData.mobType.ToString().ToLower().IndexOf(MobType.ToLower()) > -1;
        }

        /// <summary>
        /// The CheckMobType.
        /// </summary>
        /// <param name="pet">怪物.</param>
        /// <param name="type">怪物類型.</param>
        /// <returns>類型是否正確.</returns>
        public bool CheckMobType(ActorMob pet, params MobType[] type)
        {
            if (pet == null)
                return false;
            foreach (MobType mobType in type)
            {
                if (pet.BaseData.mobType == mobType)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The PossessionCancel.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="pos">部位(None=全部).</param>
        public void PossessionCancel(ActorPC pc, PossessionPosition pos)
        {
            if (pos == PossessionPosition.NONE)
            {
                this.PossessionCancel(pc, PossessionPosition.CHEST);
                this.PossessionCancel(pc, PossessionPosition.LEFT_HAND);
                this.PossessionCancel(pc, PossessionPosition.NECK);
                this.PossessionCancel(pc, PossessionPosition.RIGHT_HAND);
            }
            else
                MapClient.FromActorPC(pc).OnPossessionCancel(new CSMG_POSSESSION_CANCEL()
                {
                    PossessionPosition = pos
                });
        }

        /// <summary>
        /// The ChangePlayerSize.
        /// </summary>
        /// <param name="dActor">人物.</param>
        /// <param name="playersize">大小.</param>
        public void ChangePlayerSize(ActorPC dActor, uint playersize)
        {
            MapClient mapClient = MapClient.FromActorPC(dActor);
            mapClient.Character.Size = playersize;
            mapClient.SendPlayerSizeUpdate();
        }

        /// <summary>
        /// The isInRange.
        /// </summary>
        /// <param name="sActor">來源Actor.</param>
        /// <param name="dActor">目的Actor.</param>
        /// <param name="Range">範圍.</param>
        /// <returns>是否在範圍內.</returns>
        public bool isInRange(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, short Range)
        {
            return Math.Abs((int)sActor.X - (int)dActor.X) < (int)Range || Math.Abs((int)sActor.Y - (int)dActor.Y) < (int)Range;
        }

        /// <summary>
        /// The MoveItem.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="item">道具.</param>
        public void MoveItem(SagaDB.Actor.Actor dActor, SagaDB.Item.Item item)
        {
            if (dActor.type != ActorType.PC)
                return;
            CSMG_ITEM_MOVE p = new CSMG_ITEM_MOVE();
            MapClient.FromActorPC((ActorPC)dActor).OnItemMove(p);
        }

        /// <summary>
        /// The Warp.
        /// </summary>
        /// <param name="dActor">目標玩家.</param>
        /// <param name="MapID">地圖.</param>
        /// <param name="x">X座標.</param>
        /// <param name="y">Y座標.</param>
        public void Warp(SagaDB.Actor.Actor dActor, uint MapID, byte x, byte y)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(dActor.MapID);
            map.SendActorToMap(dActor, MapID, SagaLib.Global.PosX8to16(x, map.Width), SagaLib.Global.PosY8to16(y, map.Height));
        }

        /// <summary>
        /// The TranceMob.
        /// </summary>
        /// <param name="sActor">目標玩家.</param>
        /// <param name="MobID">怪物ID (0為變回原形).</param>
        public void TranceMob(SagaDB.Actor.Actor sActor, uint MobID)
        {
            if (sActor.type != ActorType.PC)
                return;
            ActorPC pc = (ActorPC)sActor;
            if (MobID == 0U)
            {
                pc.TranceID = 0U;
            }
            else
            {
                MobData mobData = Singleton<MobFactory>.Instance.GetMobData(MobID);
                pc.TranceID = mobData.pictid;
            }
            MapClient.FromActorPC(pc).SendCharInfoUpdate();
        }

        /// <summary>
        /// The IsRidePet.
        /// </summary>
        /// <param name="mob">.</param>
        /// <returns>.</returns>
        public bool IsRidePet(SagaDB.Actor.Actor mob)
        {
            if (mob.type != ActorType.PET)
                return false;
            return ((ActorMob)mob).BaseData.mobType.ToString().ToUpper().IndexOf("RIDE") > 1;
        }

        /// <summary>
        /// The NPCMotion.
        /// </summary>
        /// <param name="dActor">.</param>
        /// <param name="motion">.</param>
        public void NPCMotion(SagaDB.Actor.Actor dActor, MotionType motion)
        {
            SSMG_CHAT_MOTION ssmgChatMotion = new SSMG_CHAT_MOTION();
            ssmgChatMotion.ActorID = dActor.ActorID;
            ssmgChatMotion.Motion = motion;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(dActor.MapID);
            Logger.ShowInfo(motion.ToString());
            foreach (SagaDB.Actor.Actor actor in map.GetActorsArea(dActor, (short)10000, true))
            {
                if (actor.type == ActorType.PC)
                    MapClient.FromActorPC((ActorPC)actor).netIO.SendPacket((Packet)ssmgChatMotion);
            }
        }

        /// <summary>
        /// The CheckDEMRightEquip.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="type">The type<see cref="ItemType"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckDEMRightEquip(SagaDB.Actor.Actor sActor, ItemType type)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC actorPc = (ActorPC)sActor;
                if (actorPc.Race == PC_RACE.DEM)
                {
                    List<SagaDB.Item.Item> container = actorPc.Inventory.GetContainer(ContainerType.RIGHT_HAND2);
                    if (container.Count > 0 && container[0].BaseData.itemType == type)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// The PhysicalAttack.
        /// </summary>
        /// <param name="sActor">原目标.</param>
        /// <param name="dActor">对象目标.</param>
        /// <param name="arg">技能参数.</param>
        /// <param name="element">元素.</param>
        /// <param name="ATKBonus">攻击加成.</param>
        public void PhysicalAttack(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg, Elements element, float ATKBonus)
        {
            this.PhysicalAttack(sActor, new List<SagaDB.Actor.Actor>()
      {
        dActor
      }, arg, element, ATKBonus);
        }

        /// <summary>
        /// The PhysicalAttack.
        /// </summary>
        /// <param name="sActor">原目标.</param>
        /// <param name="dActor">对象目标集合.</param>
        /// <param name="arg">技能参数.</param>
        /// <param name="element">元素.</param>
        /// <param name="ATKBonus">The ATKBonus<see cref="float"/>.</param>
        public void PhysicalAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, Elements element, float ATKBonus)
        {
            this.PhysicalAttack(sActor, dActor, arg, element, 0, ATKBonus);
        }

        /// <summary>
        /// The PhysicalAttack.
        /// </summary>
        /// <param name="sActor">原目标.</param>
        /// <param name="dActor">对象目标集合.</param>
        /// <param name="arg">技能参数.</param>
        /// <param name="element">元素.</param>
        /// <param name="index">arg中参数偏移.</param>
        /// <param name="ATKBonus">攻击加成.</param>
        public void PhysicalAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, Elements element, int index, float ATKBonus)
        {
            this.PhysicalAttack(sActor, dActor, arg, SkillHandler.DefType.Def, element, index, ATKBonus, false);
        }

        /// <summary>
        /// The PhysicalAttack.
        /// </summary>
        /// <param name="sActor">原目标.</param>
        /// <param name="dActor">对象目标集合.</param>
        /// <param name="arg">技能参数.</param>
        /// <param name="defType">使用的防御类型.</param>
        /// <param name="element">元素.</param>
        /// <param name="index">arg中参数偏移.</param>
        /// <param name="ATKBonus">攻击加成.</param>
        /// <param name="setAtk">The setAtk<see cref="bool"/>.</param>
        public void PhysicalAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, int index, float ATKBonus, bool setAtk)
        {
            if (dActor.Count == 0 || sActor.Status == null)
                return;
            int damage = 0;
            int min = 0;
            int max = 0;
            int num1 = 0;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(dActor[0].MapID);
            if (index == 0)
            {
                arg.affectedActors = new List<SagaDB.Actor.Actor>();
                foreach (SagaDB.Actor.Actor actor in dActor)
                    arg.affectedActors.Add(actor);
                arg.Init();
            }
            switch (arg.type)
            {
                case ATTACK_TYPE.BLOW:
                    min = (int)sActor.Status.min_atk1;
                    max = (int)sActor.Status.max_atk1;
                    break;
                case ATTACK_TYPE.SLASH:
                    min = (int)sActor.Status.min_atk2;
                    max = (int)sActor.Status.max_atk2;
                    break;
                case ATTACK_TYPE.STAB:
                    min = (int)sActor.Status.min_atk3;
                    max = (int)sActor.Status.max_atk3;
                    break;
            }
            if (min > max)
                max = min;
            foreach (SagaDB.Actor.Actor actor1 in dActor)
            {
                if (actor1.Status != null)
                {
                    if (sActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)sActor;
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.THROW)
                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot, (ushort)1, false);
                    }
                    if (sActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)sActor;
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                        {
                            if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.BOW)
                            {
                                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                                {
                                    if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.ARROW)
                                    {
                                        if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack > (ushort)0)
                                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Slot, (ushort)1, false);
                                    }
                                    else
                                    {
                                        if (num1 == 0)
                                        {
                                            arg.result = (short)-1;
                                            continue;
                                        }
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (num1 == 0)
                                    {
                                        arg.result = (short)-1;
                                        continue;
                                    }
                                    continue;
                                }
                            }
                            if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.GUN || pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.DUALGUN || pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.RIFLE)
                            {
                                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                                {
                                    if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.BULLET)
                                    {
                                        if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack > (ushort)0)
                                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Slot, (ushort)1, false);
                                    }
                                    else
                                    {
                                        if (num1 == 0)
                                        {
                                            arg.result = (short)-1;
                                            continue;
                                        }
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (num1 == 0)
                                    {
                                        arg.result = (short)-1;
                                        continue;
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                    SkillHandler.AttackResult attackResult = this.CalcAttackResult(sActor, actor1, sActor.Range > 2U);
                    SagaDB.Actor.Actor actor2 = actor1;
                    if (attackResult == SkillHandler.AttackResult.Miss || attackResult == SkillHandler.AttackResult.Avoid || attackResult == SkillHandler.AttackResult.Guard)
                    {
                        switch (attackResult)
                        {
                            case SkillHandler.AttackResult.Miss:
                                arg.flag[index + num1] = AttackFlag.MISS;
                                break;
                            case SkillHandler.AttackResult.Avoid:
                                arg.flag[index + num1] = AttackFlag.AVOID;
                                break;
                            default:
                                arg.flag[index + num1] = AttackFlag.GUARD;
                                break;
                        }
                    }
                    else
                    {
                        int num2 = 0;
                        if (actor1.Status.Additions.ContainsKey("MobKyrie"))
                        {
                            DefaultBuff addition = (DefaultBuff)actor1.Status.Additions["MobKyrie"];
                            num2 = addition["MobKyrie"];
                            arg.flag[index + num1] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                            if (num2 > 0)
                            {
                                --addition["MobKyrie"];
                                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
                                {
                                    effectID = 4173U,
                                    actorID = actor1.ActorID
                                }, actor1, true);
                                if (num2 == 1)
                                    SkillHandler.RemoveAddition(actor1, "MobKyrie");
                            }
                        }
                        if (num2 == 0)
                        {
                            bool flag1 = false;
                            bool flag2 = false;
                            if (actor1.type == ActorType.PC)
                            {
                                ActorPC actorPc = (ActorPC)actor1;
                                if (actorPc.PossesionedActors.Count > 0 && actorPc.PossessionTarget == 0U)
                                {
                                    flag1 = true;
                                    flag2 = true;
                                }
                                if (actorPc.PossessionTarget != 0U)
                                {
                                    flag1 = true;
                                    flag2 = false;
                                }
                            }
                            if (flag2 && flag1 && (double)ATKBonus > 0.0)
                            {
                                List<SagaDB.Actor.Actor> dActor1 = this.ProcessAttackPossession(actor1);
                                if (dActor1.Count > 0)
                                {
                                    arg.Remove(actor1);
                                    int count = arg.flag.Count;
                                    arg.Extend(dActor1.Count);
                                    foreach (SagaDB.Actor.Actor actor3 in dActor1)
                                    {
                                        if (SagaLib.Global.Random.Next(0, 99) < (int)actor1.Status.possessionTakeOver)
                                            arg.affectedActors.Add(actor1);
                                        else
                                            arg.affectedActors.Add(actor3);
                                    }
                                    this.PhysicalAttack(sActor, dActor1, arg, element, count, ATKBonus);
                                    continue;
                                }
                            }
                            int num3;
                            if (!setAtk)
                            {
                                num3 = (int)(short)((double)(short)SagaLib.Global.Random.Next(min, max) * (double)this.CalcElementBonus(sActor, actor1, Elements.Neutral, 0, false) * (double)ATKBonus);
                                if (actor1.Status.Additions.ContainsKey("Frosen") && element == Elements.Fire)
                                    SkillHandler.RemoveAddition(actor1, actor1.Status.Additions["Frosen"]);
                                if (actor1.Status.Additions.ContainsKey("Stone") && element == Elements.Water)
                                    SkillHandler.RemoveAddition(actor1, actor1.Status.Additions["Stone"]);
                                if (arg.skill != null && sActor.Status.doubleUpList.Contains((ushort)arg.skill.ID))
                                    num3 *= 2;
                            }
                            else
                                num3 = (int)ATKBonus;
                            switch (defType)
                            {
                                case SkillHandler.DefType.Def:
                                    if (attackResult == SkillHandler.AttackResult.Hit)
                                    {
                                        damage = (int)Math.Ceiling((double)(num3 - (int)actor1.Status.def_add) * (1.0 - (double)actor1.Status.def / 100.0));
                                        break;
                                    }
                                    num3 = (int)((double)num3 * 1.20000004768372);
                                    if (sActor.Status.cri_dmg_skill != (short)0)
                                        num3 = (int)((double)num3 * (1.0 + (double)sActor.Status.cri_dmg_skill / 100.0));
                                    damage = (int)Math.Ceiling((double)num3 * (1.0 - (double)actor1.Status.def / 100.0));
                                    break;
                                case SkillHandler.DefType.MDef:
                                    if (attackResult == SkillHandler.AttackResult.Hit)
                                    {
                                        damage = (int)Math.Ceiling((double)(num3 - (int)actor1.Status.mdef_add) * (1.0 - (double)actor1.Status.mdef / 100.0));
                                        break;
                                    }
                                    num3 = (int)((double)num3 * 1.20000004768372);
                                    if (sActor.Status.cri_dmg_skill != (short)0)
                                        num3 = (int)((double)num3 * (1.0 + (double)sActor.Status.cri_dmg_skill / 100.0));
                                    damage = (int)Math.Ceiling((double)num3 * (1.0 - (double)actor1.Status.mdef / 100.0));
                                    break;
                                case SkillHandler.DefType.IgnoreAll:
                                    damage = num3;
                                    break;
                                case SkillHandler.DefType.IgnoreLeft:
                                    if (attackResult == SkillHandler.AttackResult.Hit)
                                    {
                                        damage = (int)Math.Ceiling((double)(num3 - (int)actor1.Status.def_add));
                                        break;
                                    }
                                    num3 = (int)((double)num3 * 1.20000004768372);
                                    if (sActor.Status.cri_dmg_skill != (short)0)
                                        num3 = (int)((double)num3 * (1.0 + (double)sActor.Status.cri_dmg_skill / 100.0));
                                    damage = (int)Math.Ceiling((double)num3);
                                    break;
                                case SkillHandler.DefType.IgnoreRight:
                                    if (attackResult == SkillHandler.AttackResult.Hit)
                                    {
                                        damage = (int)Math.Ceiling((double)num3 * (1.0 - (double)actor1.Status.def / 100.0));
                                        break;
                                    }
                                    num3 = (int)((double)num3 * 1.20000004768372);
                                    if (sActor.Status.cri_dmg_skill != (short)0)
                                        num3 = (int)((double)num3 * (1.0 + (double)sActor.Status.cri_dmg_skill / 100.0));
                                    damage = (int)Math.Ceiling((double)num3 * (1.0 - (double)actor1.Status.def / 100.0));
                                    break;
                            }
                            if (damage > num3)
                                damage = 0;
                            IStats stats = (IStats)actor1;
                            switch (arg.type)
                            {
                                case ATTACK_TYPE.BLOW:
                                    damage = (int)((double)damage * (1.0 - (double)actor1.Status.damage_atk1_discount));
                                    break;
                                case ATTACK_TYPE.SLASH:
                                    damage = (int)((double)damage * (1.0 - (double)actor1.Status.damage_atk2_discount));
                                    break;
                                case ATTACK_TYPE.STAB:
                                    damage = (int)((double)damage * (1.0 - (double)actor1.Status.damage_atk3_discount));
                                    break;
                            }
                            damage -= (int)stats.Vit / 3;
                            if (sActor.type == ActorType.PC && actor2.type == ActorType.PC)
                                damage = (int)((double)damage * (double)Singleton<Configuration>.Instance.PVPDamageRatePhysic);
                            if (actor2.type == ActorType.MOB && sActor.type == ActorType.PC)
                            {
                                ActorMob actorMob = (ActorMob)actor2;
                                foreach (Addition addition in sActor.Status.Additions.Values.ToArray<Addition>())
                                {
                                    if (addition.GetType() == typeof(SomeTypeDamUp))
                                    {
                                        SomeTypeDamUp someTypeDamUp = (SomeTypeDamUp)addition;
                                        if (someTypeDamUp.MobTypes.ContainsKey(actorMob.BaseData.mobType))
                                            damage += (int)((double)damage * ((double)someTypeDamUp.MobTypes[actorMob.BaseData.mobType] / 100.0));
                                    }
                                }
                            }
                            if (damage <= 0)
                                damage = 1;
                            if (flag1 && flag2 && actor2.Status.Additions.ContainsKey("DJoint"))
                            {
                                DefaultBuff addition = (DefaultBuff)actor2.Status.Additions["DJoint"];
                                if (SagaLib.Global.Random.Next(0, 99) < addition["Rate"])
                                {
                                    SagaDB.Actor.Actor actor3 = map.GetActor((uint)addition["Target"]);
                                    if (actor3 != null)
                                    {
                                        actor2 = actor3;
                                        arg.affectedActors[index + num1] = actor2;
                                    }
                                }
                            }
                            if (sActor.Status.Additions.ContainsKey("HpLostDamUp") && !setAtk)
                            {
                                DefaultBuff addition = (DefaultBuff)sActor.Status.Additions["HpLostDamUp"];
                                if ((long)sActor.HP > (long)addition["HPLost"])
                                {
                                    sActor.HP -= (uint)addition["HPLost"];
                                    damage += addition["DamUp"];
                                    SkillArg skillArg = new SkillArg();
                                    skillArg.sActor = sActor.ActorID;
                                    skillArg.dActor = uint.MaxValue;
                                    skillArg.x = arg.x;
                                    skillArg.y = arg.y;
                                    skillArg.argType = SkillArg.ArgType.Active;
                                    skillArg.autoCast = arg.autoCast;
                                    skillArg.skill = Singleton<SkillFactory>.Instance.GetSkill(2200U, (byte)1);
                                    skillArg.affectedActors.Add(sActor);
                                    skillArg.Init();
                                    skillArg.hp[0] = addition["HPLost"];
                                    skillArg.flag[0] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SKILL, (MapEventArgs)skillArg, sActor, true);
                                }
                            }
                            bool flag3 = false;
                            if (actor2.type == ActorType.PC)
                            {
                                ActorPC actorPc = (ActorPC)actor2;
                                if (actorPc.Pet != null)
                                    flag3 = actorPc.Pet.Ride;
                            }
                            if (attackResult == SkillHandler.AttackResult.Critical)
                            {
                                if (sActor.type == ActorType.PET)
                                    this.ProcessPetGrowth(sActor, PetGrowthReason.CriticalHit);
                                if (actor1.type == ActorType.PET && damage > 0)
                                    this.ProcessPetGrowth(actor1, PetGrowthReason.PhysicalBeenHit);
                                if (actor1.type == ActorType.PC && damage > 0)
                                {
                                    ActorPC actorPc = (ActorPC)actor2;
                                    if (flag3)
                                        this.ProcessPetGrowth((SagaDB.Actor.Actor)actorPc.Pet, PetGrowthReason.PhysicalBeenHit);
                                }
                            }
                            else
                            {
                                if (sActor.type == ActorType.PET)
                                    this.ProcessPetGrowth(sActor, PetGrowthReason.PhysicalHit);
                                if (actor1.type == ActorType.PET && damage > 0)
                                    this.ProcessPetGrowth(actor1, PetGrowthReason.PhysicalBeenHit);
                                if (actor1.type == ActorType.PC && damage > 0)
                                {
                                    ActorPC actorPc = (ActorPC)actor2;
                                    if (flag3)
                                        this.ProcessPetGrowth((SagaDB.Actor.Actor)actorPc.Pet, PetGrowthReason.PhysicalBeenHit);
                                }
                            }
                            if (sActor.type == ActorType.PC && actor2.type == ActorType.MOB && (((ActorMob)actor2).BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.チャンプモンスターキラー状態))
                                damage /= 10;
                            if (sActor.type == ActorType.PC)
                            {
                                int delta = damage / 100;
                                if (delta == 0)
                                    delta = 1;
                                Singleton<ODWarManager>.Instance.UpdateScore(sActor.MapID, sActor.ActorID, delta);
                            }
                            if (actor2.HP != 0U)
                            {
                                arg.hp[index + num1] = damage;
                                if ((long)actor2.HP > (long)damage)
                                {
                                    arg.flag[index + num1] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                    if (attackResult == SkillHandler.AttackResult.Critical)
                                    {
                                        List<AttackFlag> flag4;
                                        int index1;
                                        (flag4 = arg.flag)[index1 = index + num1] = flag4[index1] | AttackFlag.CRITICAL;
                                    }
                                }
                                else
                                {
                                    damage = (int)actor2.HP;
                                    int num4 = flag3 ? 1 : (actor2.Buff.リボーン ? 1 : 0);
                                    arg.flag[index + num1] = num4 != 0 ? AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT : AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT | AttackFlag.DIE;
                                    if (attackResult == SkillHandler.AttackResult.Critical)
                                    {
                                        List<AttackFlag> flag4;
                                        int index1;
                                        (flag4 = arg.flag)[index1 = index + num1] = flag4[index1] | AttackFlag.CRITICAL;
                                    }
                                }
                                if (actor2.HP != 0U)
                                    actor2.HP = (uint)((ulong)actor2.HP - (ulong)damage);
                                if (actor2.Status.Additions.ContainsKey("Counter"))
                                {
                                    actor2.Status.Additions["Counter"].AdditionEnd();
                                    SkillArg skillArg = new SkillArg();
                                    Singleton<SkillHandler>.Instance.Attack(actor2, sActor, skillArg);
                                    Singleton<MapManager>.Instance.GetMap(actor2.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.ATTACK, (MapEventArgs)skillArg, actor2, true);
                                }
                            }
                            else
                            {
                                int num4 = flag3 ? 1 : (actor2.Buff.リボーン ? 1 : 0);
                                arg.flag[index + num1] = num4 != 0 ? AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT : AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT | AttackFlag.DIE;
                                if (attackResult == SkillHandler.AttackResult.Critical)
                                {
                                    List<AttackFlag> flag4;
                                    int index1;
                                    (flag4 = arg.flag)[index1 = index + num1] = flag4[index1] | AttackFlag.CRITICAL;
                                }
                                arg.hp[index + num1] = damage;
                            }
                            if (sActor.Status.Additions.ContainsKey("BloodLeech"))
                            {
                                BloodLeech addition = (BloodLeech)sActor.Status.Additions["BloodLeech"];
                                int num4 = (int)((double)damage * (double)addition.rate);
                                arg.affectedActors.Add(sActor);
                                arg.hp.Add(num4);
                                arg.sp.Add(0);
                                arg.mp.Add(0);
                                arg.flag.Add(AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE);
                                sActor.HP += (uint)num4;
                                if (sActor.HP > sActor.MaxHP)
                                    sActor.HP = sActor.MaxHP;
                                Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, sActor, true);
                            }
                        }
                    }
                    this.ApplyDamage(sActor, actor2, damage);
                    ++num1;
                    Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor2, true);
                }
            }
            short num5 = (short)((int)sActor.Status.aspd + (int)sActor.Status.aspd_skill);
            if (num5 > (short)960)
                num5 = (short)960;
            if (sActor.type == ActorType.PC)
            {
                ActorPC actorPc = (ActorPC)sActor;
                arg.delay = !actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) ? 2000U - (uint)((double)(2000 * (int)num5) * (1.0 / 1000.0)) : (!actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand ? 2000U - (uint)((double)(2000 * (int)num5) * (1.0 / 1000.0)) : 2400U - (uint)((double)(2400 * (int)num5) * (1.0 / 1000.0)));
            }
            else
                arg.delay = 2000U - (uint)((double)(2000 * (int)num5) * (1.0 / 1000.0));
            arg.delay = (uint)((double)arg.delay * (double)arg.delayRate);
            if ((double)sActor.Status.aspd_skill_perc == 0.0)
                return;
            arg.delay = (uint)((double)arg.delay / (double)sActor.Status.aspd_skill_perc);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            this.MagicAttack(sActor, dActor, arg, element, 50, MATKBonus);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus)
        {
            this.MagicAttack(sActor, new List<SagaDB.Actor.Actor>() { dActor }, arg, element, elementValue, MATKBonus);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, float MATKBonus)
        {
            this.MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, int elementValue, float MATKBonus)
        {
            this.MagicAttack(sActor, new List<SagaDB.Actor.Actor>() { dActor }, arg, defType, element, elementValue, MATKBonus);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            this.MagicAttack(sActor, dActor, arg, element, 50, MATKBonus);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus)
        {
            this.MagicAttack(sActor, dActor, arg, element, elementValue, MATKBonus, 0);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, float MATKBonus)
        {
            this.MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, int elementValue, float MATKBonus)
        {
            this.MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, 0);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus, int index)
        {
            this.MagicAttack(sActor, dActor, arg, SkillHandler.DefType.MDef, element, elementValue, MATKBonus, index);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, int elementValue, float MATKBonus, int index)
        {
            this.MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, false);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="setAtk">The setAtk<see cref="bool"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, float MATKBonus, int index, bool setAtk)
        {
            this.MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus, index, setAtk);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="setAtk">The setAtk<see cref="bool"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk)
        {
            this.MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, setAtk, false);
        }

        /// <summary>
        /// The MagicAttack.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="List{SagaDB.Actor.Actor}"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        /// <param name="defType">The defType<see cref="SkillHandler.DefType"/>.</param>
        /// <param name="element">The element<see cref="Elements"/>.</param>
        /// <param name="elementValue">The elementValue<see cref="int"/>.</param>
        /// <param name="MATKBonus">The MATKBonus<see cref="float"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="setAtk">The setAtk<see cref="bool"/>.</param>
        /// <param name="noReflect">The noReflect<see cref="bool"/>.</param>
        public void MagicAttack(SagaDB.Actor.Actor sActor, List<SagaDB.Actor.Actor> dActor, SkillArg arg, SkillHandler.DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk, bool noReflect)
        {
            if (dActor.Count == 0 || sActor.Status == null)
                return;
            int num1 = 0;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(dActor[0].MapID);
            if (index == 0)
            {
                arg.affectedActors = new List<SagaDB.Actor.Actor>();
                foreach (SagaDB.Actor.Actor actor in dActor)
                    arg.affectedActors.Add(actor);
                arg.Init();
            }
            int minMatk = (int)sActor.Status.min_matk;
            int max = (int)sActor.Status.max_matk;
            if (minMatk > max)
                max = minMatk;
            foreach (SagaDB.Actor.Actor actor1 in dActor)
            {
                bool flag1 = false;
                bool flag2 = false;
                SagaDB.Actor.Actor actor2 = actor1;
                if (actor1.Status != null)
                {
                    if (actor1.type == ActorType.PC)
                    {
                        ActorPC actorPc = (ActorPC)actor1;
                        if (actorPc.PossesionedActors.Count > 0 && actorPc.PossessionTarget == 0U)
                        {
                            flag1 = true;
                            flag2 = true;
                        }
                        if (actorPc.PossessionTarget != 0U)
                        {
                            flag1 = true;
                            flag2 = false;
                        }
                    }
                    if (flag2 && flag1 && (double)MATKBonus > 0.0)
                    {
                        List<SagaDB.Actor.Actor> dActor1 = this.ProcessAttackPossession(actor1);
                        if (dActor1.Count > 0)
                        {
                            arg.Remove(actor1);
                            int count = arg.flag.Count;
                            arg.Extend(dActor1.Count);
                            foreach (SagaDB.Actor.Actor actor3 in dActor1)
                            {
                                if (SagaLib.Global.Random.Next(0, 99) < (int)actor1.Status.possessionTakeOver)
                                    arg.affectedActors.Add(actor1);
                                else
                                    arg.affectedActors.Add(actor3);
                            }
                            this.MagicAttack(sActor, dActor1, arg, element, elementValue, MATKBonus, count);
                            continue;
                        }
                    }
                    if (actor1.Status.Additions.ContainsKey("MagicReflect") && actor1 != sActor && !noReflect)
                    {
                        arg.Remove(actor1);
                        int count = arg.flag.Count;
                        arg.Extend(1);
                        arg.affectedActors.Add(sActor);
                        List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                        dActor1.Add(sActor);
                        SkillHandler.RemoveAddition(actor1, "MagicReflect");
                        this.MagicAttack(sActor, dActor1, arg, SkillHandler.DefType.MDef, element, elementValue, MATKBonus, count, false, true);
                    }
                    else
                    {
                        int num2;
                        if (!setAtk)
                        {
                            num2 = (int)((double)SagaLib.Global.Random.Next(minMatk, max) * (double)this.CalcElementBonus(sActor, actor1, element, 50, (double)MATKBonus < 0.0) * (double)MATKBonus);
                            if (sActor.Status.zenList.Contains((ushort)arg.skill.ID))
                                num2 *= 2;
                            if (sActor.Status.darkZenList.Contains((ushort)arg.skill.ID))
                                num2 *= 2;
                        }
                        else
                            num2 = (int)MATKBonus;
                        int damage;
                        switch (defType)
                        {
                            case SkillHandler.DefType.Def:
                                damage = (int)Math.Ceiling((double)(num2 - (int)actor1.Status.def_add) * (1.0 - (double)actor1.Status.def / 100.0));
                                break;
                            case SkillHandler.DefType.MDef:
                                damage = (int)Math.Ceiling((double)(num2 - (int)actor1.Status.mdef_add) * (1.0 - (double)actor1.Status.mdef / 100.0));
                                break;
                            case SkillHandler.DefType.IgnoreLeft:
                                damage = (int)Math.Ceiling((double)(num2 - (int)actor1.Status.mdef_add));
                                break;
                            case SkillHandler.DefType.IgnoreRight:
                                damage = (int)Math.Ceiling((double)num2 * (1.0 - (double)actor1.Status.mdef / 100.0));
                                break;
                            default:
                                damage = num2;
                                break;
                        }
                        if (actor1.Status.Additions.ContainsKey("Frosen") && element == Elements.Fire)
                            SkillHandler.RemoveAddition(actor1, actor1.Status.Additions["Frosen"]);
                        if (actor1.Status.Additions.ContainsKey("Stone") && element == Elements.Water)
                            SkillHandler.RemoveAddition(actor1, actor1.Status.Additions["Stone"]);
                        if (sActor.type == ActorType.PC && actor2.type == ActorType.PC && damage > 0)
                            damage = (int)((double)damage * (double)Singleton<Configuration>.Instance.PVPDamageRateMagic);
                        if (actor2.type == ActorType.MOB && sActor.type == ActorType.PC && damage > 0)
                        {
                            ActorMob actorMob = (ActorMob)actor2;
                            foreach (Addition addition in sActor.Status.Additions.Values.ToArray<Addition>())
                            {
                                if (addition.GetType() == typeof(SomeTypeDamUp))
                                {
                                    SomeTypeDamUp someTypeDamUp = (SomeTypeDamUp)addition;
                                    if (someTypeDamUp.MobTypes.ContainsKey(actorMob.BaseData.mobType))
                                        damage += (int)((double)damage * ((double)someTypeDamUp.MobTypes[actorMob.BaseData.mobType] / 100.0));
                                }
                            }
                        }
                        if (damage < 0 && (double)MATKBonus >= 0.0)
                            damage = 0;
                        if (flag1 && flag2 && actor2.Status.Additions.ContainsKey("DJoint"))
                        {
                            DefaultBuff addition = (DefaultBuff)actor2.Status.Additions["DJoint"];
                            if (SagaLib.Global.Random.Next(0, 99) < addition["Rate"])
                            {
                                SagaDB.Actor.Actor actor3 = map.GetActor((uint)addition["Target"]);
                                if (actor3 != null)
                                {
                                    actor2 = actor3;
                                    arg.affectedActors[index + num1] = actor2;
                                }
                            }
                        }
                        if (sActor.type == ActorType.PET)
                            this.ProcessPetGrowth(sActor, PetGrowthReason.SkillHit);
                        if (actor1.type == ActorType.PET && damage > 0)
                            this.ProcessPetGrowth(actor1, PetGrowthReason.MagicalBeenHit);
                        bool flag3 = false;
                        if (actor2.type == ActorType.PC)
                        {
                            ActorPC actorPc = (ActorPC)actor2;
                            if (actorPc.Pet != null)
                                flag3 = actorPc.Pet.Ride;
                        }
                        if (sActor.type == ActorType.PC && actor2.type == ActorType.MOB && (((ActorMob)actor2).BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.チャンプモンスターキラー状態))
                            damage /= 10;
                        if (sActor.type == ActorType.PC)
                        {
                            int num3 = damage / 100;
                            if (num3 == 0 && damage != 0)
                                num3 = 1;
                            Singleton<ODWarManager>.Instance.UpdateScore(sActor.MapID, sActor.ActorID, Math.Abs(num3));
                        }
                        if (actor2.HP != 0U)
                        {
                            arg.hp[index + num1] = damage;
                            if (damage >= 0)
                            {
                                if ((long)actor2.HP > (long)damage)
                                {
                                    arg.flag[index + num1] = AttackFlag.HP_DAMAGE;
                                }
                                else
                                {
                                    damage = (int)actor2.HP;
                                    int num3 = flag3 ? 1 : (actor2.Buff.リボーン ? 1 : 0);
                                    arg.flag[index + num1] = num3 != 0 ? AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT : AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT | AttackFlag.DIE;
                                }
                            }
                            else
                                arg.flag[index + num1] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                            if (actor2.HP != 0U)
                                actor2.HP = (uint)((ulong)actor2.HP - (ulong)damage);
                            if (actor2.HP > actor2.MaxHP)
                                actor2.HP = actor2.MaxHP;
                        }
                        else
                        {
                            arg.flag[index + num1] = AttackFlag.NO_DAMAGE;
                            arg.hp[index + num1] = 0;
                        }
                        this.ApplyDamage(sActor, actor2, damage);
                        Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor2, true);
                        ++num1;
                    }
                }
            }
        }

        /// <summary>
        /// The ApplyDamage.
        /// </summary>
        /// <param name="sActor">原目标.</param>
        /// <param name="dActor">对象目标.</param>
        /// <param name="damage">伤害值.</param>
        protected void ApplyDamage(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, int damage)
        {
            if ((DateTime.Now - dActor.Status.attackStamp).TotalSeconds > 5.0)
            {
                dActor.Status.attackStamp = DateTime.Now;
                dActor.Status.attackingActors.Clear();
                if (!dActor.Status.attackingActors.Contains(sActor))
                    dActor.Status.attackingActors.Add(sActor);
            }
            else if (!dActor.Status.attackingActors.Contains(sActor))
                dActor.Status.attackingActors.Add(sActor);
            if ((dActor.type == ActorType.MOB || dActor.type == ActorType.PET) && damage >= 0)
            {
                SagaDB.Actor.Actor sActor1;
                if (sActor.type == ActorType.PC)
                {
                    ActorPC actorPc = (ActorPC)sActor;
                    if (actorPc.PossessionTarget != 0U)
                    {
                        SagaDB.Actor.Actor actor = Singleton<MapManager>.Instance.GetMap(actorPc.MapID).GetActor(actorPc.PossessionTarget);
                        sActor1 = actor == null ? sActor : (actor.type != ActorType.PC ? sActor : actor);
                    }
                    else
                        sActor1 = sActor;
                }
                else
                    sActor1 = sActor;
                if (dActor.type == ActorType.MOB)
                    ((MobEventHandler)dActor.e).AI.OnAttacked(sActor1, damage);
                else
                    ((PetEventHandler)dActor.e).AI.OnAttacked(sActor1, damage);
            }
            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                if (pc.Online)
                {
                    MapClient mapClient = MapClient.FromActorPC(pc);
                    if (mapClient.Character.Buff.憑依準備)
                    {
                        mapClient.Character.Buff.憑依準備 = false;
                        mapClient.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)mapClient.Character, true);
                        if (mapClient.Character.Tasks.ContainsKey("Possession"))
                        {
                            mapClient.Character.Tasks["Possession"].Deactivate();
                            mapClient.Character.Tasks.Remove("Possession");
                        }
                    }
                    if (dActor.Status.Additions.ContainsKey("Hiding"))
                    {
                        dActor.Status.Additions["Hiding"].AdditionEnd();
                        dActor.Status.Additions.Remove("Hiding");
                    }
                    if (dActor.Status.Additions.ContainsKey("Cloaking"))
                    {
                        dActor.Status.Additions["Cloaking"].AdditionEnd();
                        dActor.Status.Additions.Remove("Cloaking");
                    }
                    if (dActor.Status.Additions.ContainsKey("IAmTree"))
                    {
                        dActor.Status.Additions["IAmTree"].AdditionEnd();
                        dActor.Status.Additions.Remove("IAmTree");
                    }
                    if (dActor.Status.Additions.ContainsKey("Invisible"))
                    {
                        dActor.Status.Additions["Invisible"].AdditionEnd();
                        dActor.Status.Additions.Remove("Invisible");
                    }
                }
            }
            if (dActor.HP != 0U || dActor.Buff.Dead)
                return;
            if (dActor.type == ActorType.PC)
            {
                ActorPC actorPc = (ActorPC)dActor;
                if (actorPc.Pet != null && actorPc.Pet.Ride)
                {
                    PCEventHandler e = (PCEventHandler)actorPc.e;
                    CSMG_ITEM_MOVE p = new CSMG_ITEM_MOVE();
                    p.data = new byte[11];
                    if (!actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                        return;
                    SagaDB.Item.Item equipment = actorPc.Inventory.Equipments[EnumEquipSlot.PET];
                    if (equipment.Durability != (ushort)0)
                        --equipment.Durability;
                    e.Client.SendItemInfo(equipment);
                    e.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.PET_FRIENDLY_DOWN, (object)equipment.BaseData.name));
                    e.OnShowEffect((SagaDB.Actor.Actor)e.Client.Character, (MapEventArgs)new EffectArg()
                    {
                        actorID = e.Client.Character.ActorID,
                        effectID = 8044U
                    });
                    p.InventoryID = equipment.Slot;
                    p.Target = ContainerType.BODY;
                    p.Count = (ushort)1;
                    e.Client.OnItemMove(p);
                    return;
                }
                if (actorPc.PossessionTarget != 0U)
                    MapClient.FromActorPC(actorPc).OnPossessionCancel(new CSMG_POSSESSION_CANCEL()
                    {
                        PossessionPosition = PossessionPosition.NONE
                    });
                if (((ActorPC)dActor).Mode != PlayerMode.COLISEUM_MODE && ((ActorPC)dActor).Mode != PlayerMode.KNIGHT_WAR)
                    Singleton<ExperienceManager>.Instance.DeathPenalty((ActorPC)dActor);
                if (sActor.type == ActorType.PC && Singleton<MapManager>.Instance.GetMap(actorPc.MapID).Info.Flag.Test(MapFlags.Wrp))
                    Singleton<ExperienceManager>.Instance.ProcessWrp((ActorPC)sActor, actorPc);
            }
            if (dActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)dActor;
                if (sActor.type == ActorType.PC)
                {
                    ActorPC actorPc1 = (ActorPC)sActor;
                    PCEventHandler e = (PCEventHandler)actorPc1.e;
                    if (actorPc1.Party != null)
                    {
                        foreach (ActorPC actorPc2 in actorPc1.Party.Members.Values)
                        {
                            if (actorPc2.Online && actorPc2 != actorPc1)
                                ((PCEventHandler)actorPc2.e).Client.QuestMobKilled(mob, true);
                        }
                        e.Client.QuestMobKilled(mob, false);
                    }
                    else
                        e.Client.QuestMobKilled(mob, false);
                }
                Singleton<ExperienceManager>.Instance.ProcessMobExp(mob);
            }
            dActor.e.OnDie();
        }

        /// <summary>
        /// Defines the AttackResult.
        /// </summary>
        private enum AttackResult
        {
            /// <summary>
            /// Defines the Hit.
            /// </summary>
            Hit,

            /// <summary>
            /// Defines the Miss.
            /// </summary>
            Miss,

            /// <summary>
            /// Defines the Avoid.
            /// </summary>
            Avoid,

            /// <summary>
            /// Defines the Critical.
            /// </summary>
            Critical,

            /// <summary>
            /// Defines the Guard.
            /// </summary>
            Guard,
        }

        /// <summary>人物的方向</summary>
        public enum ActorDirection
        {
            /// <summary>
            /// Defines the South.
            /// </summary>
            South,

            /// <summary>
            /// Defines the SouthWest.
            /// </summary>
            SouthWest,

            /// <summary>
            /// Defines the West.
            /// </summary>
            West,

            /// <summary>
            /// Defines the NorthWest.
            /// </summary>
            NorthWest,

            /// <summary>
            /// Defines the North.
            /// </summary>
            North,

            /// <summary>
            /// Defines the NorthEast.
            /// </summary>
            NorthEast,

            /// <summary>
            /// Defines the East.
            /// </summary>
            East,

            /// <summary>
            /// Defines the SouthEast.
            /// </summary>
            SouthEast,
        }

        /// <summary>
        /// Defines the DefaultAdditions.
        /// </summary>
        public enum DefaultAdditions
        {
            /// <summary>
            /// Defines the Poison.
            /// </summary>
            Poison = 0,

            /// <summary>
            /// Defines the Stone.
            /// </summary>
            Stone = 1,

            /// <summary>
            /// Defines the Paralyse.
            /// </summary>
            Paralyse = 2,

            /// <summary>
            /// Defines the Sleep.
            /// </summary>
            Sleep = 3,

            /// <summary>
            /// Defines the Silence.
            /// </summary>
            Silence = 4,

            /// <summary>
            /// Defines the 鈍足.
            /// </summary>
            鈍足 = 5,

            /// <summary>
            /// Defines the Confuse.
            /// </summary>
            Confuse = 6,

            /// <summary>
            /// Defines the Frosen.
            /// </summary>
            Frosen = 7,

            /// <summary>
            /// Defines the Stun.
            /// </summary>
            Stun = 8,

            /// <summary>
            /// Defines the 硬直.
            /// </summary>
            硬直 = 14, // 0x0000000E

            /// <summary>
            /// Defines the CannotMove.
            /// </summary>
            CannotMove = 15, // 0x0000000F
        }

        /// <summary>
        /// Defines the DefType.
        /// </summary>
        public enum DefType
        {
            /// <summary>
            /// Defines the Def.
            /// </summary>
            Def,

            /// <summary>
            /// Defines the MDef.
            /// </summary>
            MDef,

            /// <summary>
            /// Defines the IgnoreAll.
            /// </summary>
            IgnoreAll,

            /// <summary>
            /// Defines the IgnoreLeft.
            /// </summary>
            IgnoreLeft,

            /// <summary>
            /// Defines the IgnoreRight.
            /// </summary>
            IgnoreRight,
        }
    }
}
