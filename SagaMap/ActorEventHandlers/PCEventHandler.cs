namespace SagaMap.ActorEventHandlers
{
    using SagaDB.Actor;
    using SagaDB.Map;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Server;
    using SagaMap.PC;
    using SagaMap.Skill;
    using System;

    /// <summary>
    /// Defines the <see cref="PCEventHandler" />.
    /// </summary>
    public class PCEventHandler : ActorEventHandler
    {
        /// <summary>
        /// Defines the Client.
        /// </summary>
        public MapClient Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="PCEventHandler"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        public PCEventHandler(MapClient client)
        {
            this.Client = client;
        }

        /// <summary>
        /// The OnActorAppears.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorAppears(SagaDB.Actor.Actor aActor)
        {
            if (this.Client == null)
                return;
            if (!this.Client.Character.VisibleActors.Contains(aActor.ActorID))
                this.Client.Character.VisibleActors.Add(aActor.ActorID);
            switch (aActor.type)
            {
                case ActorType.PC:
                    ActorPC actorPc = (ActorPC)aActor;
                    if (!actorPc.Online && actorPc.PossessionTarget == 0U)
                        return;
                    SSMG_ACTOR_PC_APPEAR ssmgActorPcAppear = new SSMG_ACTOR_PC_APPEAR();
                    ssmgActorPcAppear.ActorID = actorPc.ActorID;
                    ssmgActorPcAppear.Dir = (byte)((uint)actorPc.Dir / 45U);
                    ssmgActorPcAppear.HP = actorPc.HP;
                    ssmgActorPcAppear.MaxHP = actorPc.MaxHP;
                    if (actorPc.PossessionTarget == 0U)
                    {
                        ssmgActorPcAppear.PossessionActorID = uint.MaxValue;
                        ssmgActorPcAppear.PossessionPosition = PossessionPosition.NONE;
                    }
                    else
                    {
                        SagaDB.Actor.Actor actor = this.Client.Map.GetActor(actorPc.PossessionTarget);
                        if (actor != null)
                        {
                            ssmgActorPcAppear.PossessionActorID = actor.type == ActorType.ITEM ? actorPc.ActorID : actorPc.PossessionTarget;
                            ssmgActorPcAppear.PossessionPosition = actorPc.PossessionPosition;
                        }
                        else
                        {
                            ssmgActorPcAppear.PossessionActorID = actorPc.PossessionTarget;
                            ssmgActorPcAppear.PossessionPosition = actorPc.PossessionPosition;
                        }
                    }
                    ssmgActorPcAppear.Speed = actorPc.Speed;
                    ssmgActorPcAppear.X = Global.PosX16to8(actorPc.X, this.Client.map.Width);
                    ssmgActorPcAppear.Y = Global.PosY16to8(actorPc.Y, this.Client.map.Height);
                    this.Client.netIO.SendPacket((Packet)ssmgActorPcAppear);
                    break;
                case ActorType.MOB:
                    ActorMob actorMob = (ActorMob)aActor;
                    MapInfo info1 = Singleton<MapManager>.Instance.GetMap(actorMob.MapID).Info;
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_MOB_APPEAR()
                    {
                        ActorID = actorMob.ActorID,
                        Dir = (byte)((uint)actorMob.Dir / 45U),
                        HP = actorMob.HP,
                        MaxHP = actorMob.MaxHP,
                        MobID = actorMob.MobID,
                        Speed = actorMob.BaseData.speed,
                        X = Global.PosX16to8(actorMob.X, info1.width),
                        Y = Global.PosY16to8(actorMob.Y, info1.height)
                    });
                    break;
                case ActorType.ITEM:
                    this.Client.netIO.SendPacket((Packet)new SSMG_ITEM_ACTOR_APPEAR()
                    {
                        Item = (ActorItem)aActor
                    });
                    break;
                case ActorType.PET:
                    ActorPet actorPet = (ActorPet)aActor;
                    MapInfo info2 = Singleton<MapManager>.Instance.GetMap(actorPet.MapID).Info;
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_PET_APPEAR()
                    {
                        ActorID = actorPet.ActorID,
                        Unknown = (byte)4,
                        OwnerActorID = actorPet.Owner.ActorID,
                        OwnerCharID = actorPet.Owner.CharID,
                        OwnerLevel = actorPet.Owner.Level,
                        OwnerWRP = actorPet.Owner.WRPRanking,
                        Dir = (byte)((uint)actorPet.Dir / 45U),
                        HP = actorPet.HP,
                        MaxHP = actorPet.MaxHP,
                        Speed = actorPet.Speed,
                        X = Global.PosX16to8(actorPet.X, info2.width),
                        Y = Global.PosY16to8(actorPet.Y, info2.height)
                    });
                    break;
                case ActorType.SKILL:
                    ActorSkill actorSkill = (ActorSkill)aActor;
                    MapInfo info3 = Singleton<MapManager>.Instance.GetMap(actorSkill.MapID).Info;
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_SKILL_APPEAR()
                    {
                        ActorID = actorSkill.ActorID,
                        Dir = (byte)((uint)actorSkill.Dir / 45U),
                        Speed = actorSkill.Speed,
                        SkillID = (ushort)actorSkill.Skill.ID,
                        SkillLv = actorSkill.Skill.Level,
                        X = Global.PosX16to8(actorSkill.X, info3.width),
                        Y = Global.PosY16to8(actorSkill.Y, info3.height)
                    });
                    break;
                case ActorType.SHADOW:
                    ActorShadow actorShadow = (ActorShadow)aActor;
                    MapInfo info4 = Singleton<MapManager>.Instance.GetMap(actorShadow.MapID).Info;
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_PET_APPEAR()
                    {
                        ActorID = actorShadow.ActorID,
                        Unknown = (byte)0,
                        OwnerActorID = actorShadow.Owner.ActorID,
                        OwnerCharID = actorShadow.Owner.CharID,
                        Dir = (byte)((uint)actorShadow.Dir / 45U),
                        HP = actorShadow.HP,
                        MaxHP = actorShadow.MaxHP,
                        Speed = actorShadow.Speed,
                        X = Global.PosX16to8(actorShadow.X, info4.width),
                        Y = Global.PosY16to8(actorShadow.Y, info4.height)
                    });
                    break;
                case ActorType.EVENT:
                    ActorEvent actorEvent = (ActorEvent)aActor;
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_EVENT_APPEAR()
                    {
                        Actor = actorEvent
                    });
                    break;
                case ActorType.FURNITURE:
                    ActorFurniture actorFurniture = (ActorFurniture)aActor;
                    this.Client.netIO.SendPacket((Packet)new SSMG_FG_ACTOR_APPEAR((byte)2)
                    {
                        ActorID = actorFurniture.ActorID,
                        ItemID = actorFurniture.ItemID,
                        PictID = actorFurniture.PictID,
                        X = actorFurniture.X,
                        Y = actorFurniture.Y,
                        Z = actorFurniture.Z,
                        Dir = actorFurniture.Dir,
                        Motion = actorFurniture.Motion,
                        Name = actorFurniture.Name
                    });
                    break;
                case ActorType.GOLEM:
                    ActorGolem actorGolem = (ActorGolem)aActor;
                    MapInfo info5 = Singleton<MapManager>.Instance.GetMap(actorGolem.MapID).Info;
                    this.Client.netIO.SendPacket((Packet)new SSMG_GOLEM_ACTOR_APPEAR()
                    {
                        ActorID = actorGolem.ActorID,
                        PictID = actorGolem.Item.BaseData.marionetteID,
                        X = Global.PosX16to8(actorGolem.X, info5.width),
                        Y = Global.PosY16to8(actorGolem.Y, info5.height),
                        Speed = actorGolem.Speed,
                        Dir = (byte)((uint)actorGolem.Dir / 45U),
                        GolemID = actorGolem.ActorID,
                        GolemType = actorGolem.GolemType,
                        CharName = actorGolem.Owner.Name,
                        Title = actorGolem.Title,
                        Unknown = 1U
                    });
                    break;
            }
            this.OnActorChangeBuff(aActor);
        }

        /// <summary>
        /// The OnActorChangeEquip.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeEquip(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            ActorPC actorPc = (ActorPC)sActor;
            this.Client.netIO.SendPacket((Packet)new SSMG_ITEM_ACTOR_EQUIP_UPDATE()
            {
                Player = actorPc
            });
        }

        /// <summary>
        /// The OnActorChat.
        /// </summary>
        /// <param name="cActor">The cActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChat(SagaDB.Actor.Actor cActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_CHAT_PUBLIC()
            {
                ActorID = cActor.ActorID,
                Message = ((ChatArg)args).content
            });
        }

        /// <summary>
        /// The OnActorDisappears.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorDisappears(SagaDB.Actor.Actor dActor)
        {
            if (this.Client == null)
                return;
            if (this.Client.Character.VisibleActors.Contains(dActor.ActorID))
                this.Client.Character.VisibleActors.Remove(dActor.ActorID);
            switch (dActor.type)
            {
                case ActorType.PC:
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_DELETE()
                    {
                        ActorID = dActor.ActorID
                    });
                    break;
                case ActorType.MOB:
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_MOB_DELETE()
                    {
                        ActorID = dActor.ActorID
                    });
                    break;
                case ActorType.ITEM:
                    SSMG_ITEM_ACTOR_DISAPPEAR itemActorDisappear = new SSMG_ITEM_ACTOR_DISAPPEAR();
                    ActorItem actorItem = (ActorItem)dActor;
                    itemActorDisappear.ActorID = actorItem.ActorID;
                    itemActorDisappear.Count = (byte)actorItem.Item.Stack;
                    itemActorDisappear.Looter = actorItem.LootedBy;
                    this.Client.netIO.SendPacket((Packet)itemActorDisappear);
                    break;
                case ActorType.PET:
                case ActorType.SHADOW:
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_PET_DELETE()
                    {
                        ActorID = dActor.ActorID
                    });
                    break;
                case ActorType.SKILL:
                    ActorSkill actorSkill = (ActorSkill)dActor;
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_SKILL_DELETE()
                    {
                        ActorID = dActor.ActorID
                    });
                    if (this.Client.Character != actorSkill.Caster || !(actorSkill.Name != "NOT_SHOW_DISAPPEAR"))
                        break;
                    this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.SKILL_ACTOR_DELETE, (object)actorSkill.Skill.Name));
                    break;
                case ActorType.EVENT:
                    this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_EVENT_DISAPPEAR()
                    {
                        ActorID = dActor.ActorID
                    });
                    break;
                case ActorType.FURNITURE:
                    this.Client.netIO.SendPacket((Packet)new SSMG_FG_ACTOR_DISAPPEAR()
                    {
                        ActorID = dActor.ActorID
                    });
                    break;
                case ActorType.GOLEM:
                    this.Client.netIO.SendPacket((Packet)new SSMG_GOLEM_ACTOR_DISAPPEAR()
                    {
                        ActorID = dActor.ActorID
                    });
                    break;
            }
        }

        /// <summary>
        /// The OnActorSkillUse.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorSkillUse(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            SkillArg skillArg = (SkillArg)args;
            switch (skillArg.argType)
            {
                case SkillArg.ArgType.Attack:
                    this.OnAttack(sActor, (MapEventArgs)skillArg);
                    break;
                case SkillArg.ArgType.Cast:
                    this.Client.netIO.SendPacket((Packet)new SSMG_SKILL_CAST_RESULT()
                    {
                        ActorID = sActor.ActorID,
                        Result = (byte)skillArg.result,
                        TargetID = skillArg.dActor,
                        SkillID = (ushort)skillArg.skill.ID,
                        CastTime = skillArg.delay,
                        SkillLv = skillArg.skill.Level,
                        X = skillArg.x,
                        Y = skillArg.y
                    });
                    break;
                case SkillArg.ArgType.Active:
                    if (skillArg.dActor != uint.MaxValue)
                    {
                        SSMG_SKILL_ACTIVE ssmgSkillActive = new SSMG_SKILL_ACTIVE((byte)skillArg.affectedActors.Count);
                        ssmgSkillActive.ActorID = sActor.ActorID;
                        ssmgSkillActive.AffectedID = skillArg.affectedActors;
                        ssmgSkillActive.AttackFlag(skillArg.flag);
                        ssmgSkillActive.SetHP(skillArg.hp);
                        ssmgSkillActive.SetMP(skillArg.mp);
                        ssmgSkillActive.SetSP(skillArg.sp);
                        if (skillArg.skill != null)
                        {
                            ssmgSkillActive.SkillID = (ushort)skillArg.skill.ID;
                            ssmgSkillActive.SkillLv = skillArg.skill.Level;
                        }
                        ssmgSkillActive.TargetID = skillArg.dActor;
                        ssmgSkillActive.X = skillArg.x;
                        ssmgSkillActive.Y = skillArg.y;
                        this.Client.netIO.SendPacket((Packet)ssmgSkillActive);
                        break;
                    }
                    SSMG_SKILL_ACTIVE_FLOOR skillActiveFloor = new SSMG_SKILL_ACTIVE_FLOOR((byte)skillArg.affectedActors.Count);
                    skillActiveFloor.ActorID = sActor.ActorID;
                    skillActiveFloor.AffectedID = skillArg.affectedActors;
                    skillActiveFloor.AttackFlag(skillArg.flag);
                    skillActiveFloor.SetHP(skillArg.hp);
                    skillActiveFloor.SetMP(skillArg.mp);
                    skillActiveFloor.SetSP(skillArg.sp);
                    if (skillArg.skill != null)
                    {
                        skillActiveFloor.SkillID = (ushort)skillArg.skill.ID;
                        skillActiveFloor.SkillLv = skillArg.skill.Level;
                    }
                    skillActiveFloor.X = skillArg.x;
                    skillActiveFloor.Y = skillArg.y;
                    this.Client.netIO.SendPacket((Packet)skillActiveFloor);
                    break;
                case SkillArg.ArgType.Item_Cast:
                    this.Client.netIO.SendPacket((Packet)new SSMG_ITEM_USE()
                    {
                        ItemID = skillArg.item.ItemID,
                        Form_ActorId = sActor.ActorID,
                        result = skillArg.result,
                        To_ActorID = skillArg.dActor,
                        SkillID = skillArg.item.BaseData.activateSkill,
                        Cast = skillArg.item.BaseData.cast,
                        SkillLV = (byte)1,
                        X = skillArg.x,
                        Y = skillArg.y
                    });
                    break;
                case SkillArg.ArgType.Item_Active:
                    if (skillArg.dActor != uint.MaxValue)
                    {
                        if ((int)skillArg.dActor == (int)this.Client.Character.ActorID)
                        {
                            SSMG_ITEM_ACTIVE_SELF ssmgItemActiveSelf = new SSMG_ITEM_ACTIVE_SELF((byte)skillArg.affectedActors.Count);
                            ssmgItemActiveSelf.ActorID = sActor.ActorID;
                            ssmgItemActiveSelf.AffectedID = skillArg.affectedActors;
                            ssmgItemActiveSelf.AttackFlag(skillArg.flag);
                            ssmgItemActiveSelf.ItemID = skillArg.item.ItemID;
                            ssmgItemActiveSelf.SetHP(skillArg.hp);
                            ssmgItemActiveSelf.SetMP(skillArg.mp);
                            ssmgItemActiveSelf.SetSP(skillArg.sp);
                            this.Client.netIO.SendPacket((Packet)ssmgItemActiveSelf);
                            break;
                        }
                        SSMG_ITEM_ACTIVE ssmgItemActive = new SSMG_ITEM_ACTIVE((byte)skillArg.affectedActors.Count);
                        ssmgItemActive.ActorID = sActor.ActorID;
                        ssmgItemActive.AffectedID = skillArg.affectedActors;
                        ssmgItemActive.AttackFlag(skillArg.flag);
                        ssmgItemActive.ItemID = skillArg.item.ItemID;
                        ssmgItemActive.SetHP(skillArg.hp);
                        ssmgItemActive.SetMP(skillArg.mp);
                        ssmgItemActive.SetSP(skillArg.sp);
                        this.Client.netIO.SendPacket((Packet)ssmgItemActive);
                        break;
                    }
                    SSMG_ITEM_ACTIVE_FLOOR ssmgItemActiveFloor = new SSMG_ITEM_ACTIVE_FLOOR((byte)skillArg.affectedActors.Count);
                    ssmgItemActiveFloor.ActorID = sActor.ActorID;
                    ssmgItemActiveFloor.AffectedID = skillArg.affectedActors;
                    ssmgItemActiveFloor.AttackFlag(skillArg.flag);
                    ssmgItemActiveFloor.SetHP(skillArg.hp);
                    ssmgItemActiveFloor.SetMP(skillArg.mp);
                    ssmgItemActiveFloor.SetSP(skillArg.sp);
                    ssmgItemActiveFloor.ItemID = skillArg.item.ItemID;
                    ssmgItemActiveFloor.X = skillArg.x;
                    ssmgItemActiveFloor.Y = skillArg.y;
                    this.Client.netIO.SendPacket((Packet)ssmgItemActiveFloor);
                    break;
                case SkillArg.ArgType.Actor_Active:
                    SSMG_SKILL_ACTIVE_ACTOR skillActiveActor = new SSMG_SKILL_ACTIVE_ACTOR((byte)skillArg.affectedActors.Count);
                    skillActiveActor.ActorID = sActor.ActorID;
                    skillActiveActor.AffectedID = skillArg.affectedActors;
                    skillActiveActor.AttackFlag(skillArg.flag);
                    skillActiveActor.SetHP(skillArg.hp);
                    skillActiveActor.SetMP(skillArg.mp);
                    skillActiveActor.SetSP(skillArg.sp);
                    this.Client.netIO.SendPacket((Packet)skillActiveActor);
                    break;
            }
        }

        /// <summary>
        /// The OnActorStartsMoving.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void OnActorStartsMoving(SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            if (this.Client == null)
                return;
            if (!this.Client.Character.VisibleActors.Contains(mActor.ActorID) && (int)mActor.ActorID != (int)this.Client.Character.ActorID)
                this.OnActorAppears(mActor);
            if (mActor.type == ActorType.FURNITURE)
                this.Client.netIO.SendPacket((Packet)new SSMG_FG_FURNITURE_RECONFIG()
                {
                    ActorID = mActor.ActorID,
                    X = pos[0],
                    Y = pos[1],
                    Z = pos[2],
                    Dir = dir
                });
            else if (mActor.type != ActorType.SKILL)
            {
                SSMG_ACTOR_MOVE ssmgActorMove = new SSMG_ACTOR_MOVE();
                ssmgActorMove.ActorID = mActor.ActorID;
                ssmgActorMove.X = pos[0];
                ssmgActorMove.Y = pos[1];
                if (dir <= (ushort)360)
                {
                    ssmgActorMove.Dir = dir;
                    if (mActor.type != ActorType.MOB && mActor.type != ActorType.SHADOW && mActor.type != ActorType.GOLEM && mActor.type != ActorType.PC)
                        ssmgActorMove.MoveType = MoveType.RUN;
                    else if (mActor.type == ActorType.SHADOW)
                        ssmgActorMove.MoveType = ((PetEventHandler)mActor.e).AI.AIActivity != Activity.BUSY ? MoveType.WALK : MoveType.RUN;
                    else if (mActor.type == ActorType.MOB || mActor.type == ActorType.GOLEM)
                    {
                        ssmgActorMove.MoveType = ((MobEventHandler)mActor.e).AI.AIActivity != Activity.BUSY ? MoveType.WALK : MoveType.RUN;
                    }
                    else
                    {
                        PCEventHandler e = (PCEventHandler)mActor.e;
                        ssmgActorMove.MoveType = e.Client.AI == null ? MoveType.RUN : (e.Client.AI.AIActivity != Activity.BUSY ? MoveType.WALK : MoveType.RUN);
                    }
                }
                else
                    ssmgActorMove.MoveType = MoveType.FORCE_MOVEMENT;
                this.Client.netIO.SendPacket((Packet)ssmgActorMove);
            }
            else
                this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_SKILL_MOVE()
                {
                    ActorID = mActor.ActorID,
                    X = pos[0],
                    Y = pos[1]
                });
        }

        /// <summary>
        /// The OnActorStopsMoving.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void OnActorStopsMoving(SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            if (this.Client == null)
                return;
            if (!this.Client.Character.VisibleActors.Contains(mActor.ActorID) && (int)mActor.ActorID != (int)this.Client.Character.ActorID)
                this.OnActorAppears(mActor);
            if (dir <= (ushort)360)
            {
                this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_MOVE()
                {
                    ActorID = mActor.ActorID,
                    X = pos[0],
                    Y = pos[1],
                    Dir = dir,
                    MoveType = MoveType.CHANGE_DIR
                });
            }
            else
            {
                Packet p = new Packet(11U);
                p.ID = (ushort)4602;
                p.PutByte(byte.MaxValue, (ushort)2);
                p.PutUInt(mActor.MapID, (ushort)3);
                p.PutShort(this.Client.Character.X, (ushort)7);
                p.PutShort(this.Client.Character.Y, (ushort)9);
                this.Client.netIO.SendPacket(p);
            }
        }

        /// <summary>
        /// The OnCreate.
        /// </summary>
        /// <param name="success">The success<see cref="bool"/>.</param>
        public void OnCreate(bool success)
        {
            if (this.Client == null || !success)
                return;
            if (this.Client.firstLogin)
            {
                this.Client.SendActorID();
                this.Client.SendActorMode();
                this.Client.SendCharOption();
                this.Client.SendItems();
                this.Client.SendCharInfo();
                this.Client.firstLogin = false;
            }
            else
            {
                this.Client.map = Singleton<MapManager>.Instance.GetMap(this.Client.Character.MapID);
                if (this.Client.map.ID / 10U == 7000000U || this.Client.map.ID / 10U == 7500000U)
                {
                    this.Client.SendGotoFG();
                    this.Client.netIO.SendPacket(new Packet()
                    {
                        data = new byte[3],
                        ID = (ushort)4650
                    });
                }
                else
                {
                    this.Client.SendChangeMap();
                    this.Client.netIO.SendPacket(new Packet()
                    {
                        data = new byte[3],
                        ID = (ushort)4650
                    });
                }
                if (this.Client.Character.Pet != null)
                    this.Client.DeletePet();
                if (this.Client.Character.Tasks.ContainsKey("Possession"))
                {
                    this.Client.Character.Tasks["Possession"].Deactivate();
                    this.Client.Character.Tasks.Remove("Possession");
                }
            }
        }

        /// <summary>
        /// The OnActorChangeEmotion.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeEmotion(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            ChatArg chatArg = (ChatArg)args;
            this.Client.netIO.SendPacket((Packet)new SSMG_CHAT_EMOTION()
            {
                ActorID = aActor.ActorID,
                Emotion = chatArg.emotion
            });
        }

        /// <summary>
        /// The OnActorChangeMotion.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeMotion(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            ChatArg chatArg = (ChatArg)args;
            this.Client.netIO.SendPacket((Packet)new SSMG_CHAT_MOTION()
            {
                ActorID = aActor.ActorID,
                Motion = chatArg.motion,
                Loop = chatArg.loop
            });
        }

        /// <summary>
        /// The OnDelete.
        /// </summary>
        public void OnDelete()
        {
        }

        /// <summary>
        /// The OnCharInfoUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnCharInfoUpdate(SagaDB.Actor.Actor aActor)
        {
            if (this.Client == null)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_PC_INFO()
            {
                Actor = aActor
            });
        }

        /// <summary>
        /// The OnPlayerSizeChange.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnPlayerSizeChange(SagaDB.Actor.Actor aActor)
        {
            if (this.Client == null)
                return;
            SSMG_PLAYER_SIZE ssmgPlayerSize = new SSMG_PLAYER_SIZE();
            ActorPC actorPc = (ActorPC)aActor;
            ssmgPlayerSize.ActorID = actorPc.ActorID;
            ssmgPlayerSize.Size = actorPc.Size;
            this.Client.netIO.SendPacket((Packet)ssmgPlayerSize);
        }

        /// <summary>
        /// The OnDie.
        /// </summary>
        public void OnDie()
        {
            if (this.Client == null)
                return;
            if (this.Client.Character.Marionette != null)
                this.Client.MarionetteDeactivate();
            this.Client.Character.ClearTaskAddition();
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Client.Character);
            this.Client.Character.BattleStatus = (byte)0;
            this.Client.SendChangeStatus();
            this.Client.Character.Buff.Dead = true;
            this.Client.Character.Motion = MotionType.DEAD;
            this.Client.Character.MotionLoop = true;
            this.Client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Client.Character, true);
            Singleton<ODWarManager>.Instance.UpdateScore(this.Client.map.ID, this.Client.Character.ActorID, -200);
            if (Global.Random.Next(0, 99) >= (int)this.Client.Character.Status.autoReviveRate)
                return;
            this.Client.Character.BattleStatus = (byte)0;
            this.Client.SendChangeStatus();
            this.Client.Character.TInt["Revive"] = 5;
            this.Client.EventActivate(4043309056U);
        }

        /// <summary>
        /// The OnKick.
        /// </summary>
        public void OnKick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnMapLoaded.
        /// </summary>
        public void OnMapLoaded()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnReSpawn.
        /// </summary>
        public void OnReSpawn()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnSendMessage.
        /// </summary>
        /// <param name="from">The from<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public void OnSendMessage(string from, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnSendWhisper.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="flag">The flag<see cref="byte"/>.</param>
        public void OnSendWhisper(string name, string message, byte flag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The OnTeleport.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void OnTeleport(short x, short y)
        {
            if (this.Client == null)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_MOVE()
            {
                ActorID = this.Client.Character.ActorID,
                Dir = this.Client.Character.Dir,
                X = x,
                Y = y,
                MoveType = MoveType.WARP
            });
        }

        /// <summary>
        /// The OnAttack.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnAttack(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            SkillArg skillArg = (SkillArg)args;
            if (skillArg.affectedActors.Count == 1)
            {
                this.Client.netIO.SendPacket((Packet)new SSMG_SKILL_ATTACK_RESULT()
                {
                    ActorID = skillArg.sActor,
                    AttackFlag = skillArg.flag[0],
                    AttackType = (skillArg.result < (short)0 ? (ATTACK_TYPE)skillArg.result : skillArg.type),
                    Delay = skillArg.delay,
                    Unknown = skillArg.delay,
                    HP = skillArg.hp[0],
                    MP = skillArg.mp[0],
                    SP = skillArg.sp[0],
                    TargetID = skillArg.affectedActors[0].ActorID
                });
            }
            else
            {
                SSMG_SKILL_COMBO_ATTACK_RESULT comboAttackResult = new SSMG_SKILL_COMBO_ATTACK_RESULT((byte)skillArg.affectedActors.Count);
                comboAttackResult.ActorID = skillArg.sActor;
                comboAttackResult.TargetID = skillArg.affectedActors;
                comboAttackResult.AttackFlag(skillArg.flag);
                comboAttackResult.AttackType = skillArg.result < (short)0 ? (ATTACK_TYPE)skillArg.result : skillArg.type;
                comboAttackResult.Delay = skillArg.delay;
                comboAttackResult.Unknown = skillArg.delay;
                comboAttackResult.SetHP(skillArg.hp);
                comboAttackResult.SetMP(skillArg.mp);
                comboAttackResult.SetSP(skillArg.sp);
                this.Client.netIO.SendPacket((Packet)comboAttackResult);
            }
        }

        /// <summary>
        /// The OnHPMPSPUpdate.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnHPMPSPUpdate(SagaDB.Actor.Actor sActor)
        {
            if (this.Client == null)
                return;
            this.Client.SendActorHPMPSP(sActor);
        }

        /// <summary>
        /// The OnPlayerChangeStatus.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnPlayerChangeStatus(ActorPC aActor)
        {
            if (this.Client == null)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_SKILL_CHANGE_BATTLE_STATUS()
            {
                ActorID = aActor.ActorID,
                Status = aActor.BattleStatus
            });
        }

        /// <summary>
        /// The OnActorChangeBuff.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorChangeBuff(SagaDB.Actor.Actor sActor)
        {
            if (this.Client == null)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_BUFF()
            {
                Actor = sActor
            });
            if (sActor.type != ActorType.PC)
                return;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Party != null && actorPc.Party.IsMember(this.Client.Character))
                this.Client.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_BUFF()
                {
                    Actor = actorPc
                });
        }

        /// <summary>
        /// The OnLevelUp.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnLevelUp(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            SkillArg skillArg = (SkillArg)args;
            this.Client.SendLvUP(sActor, skillArg.x);
        }

        /// <summary>
        /// The OnPlayerMode.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnPlayerMode(SagaDB.Actor.Actor aActor)
        {
            if (this.Client == null)
                return;
            SSMG_ACTOR_MODE ssmgActorMode = new SSMG_ACTOR_MODE();
            ActorPC actorPc = (ActorPC)aActor;
            ssmgActorMode.ActorID = aActor.ActorID;
            switch (actorPc.Mode)
            {
                case PlayerMode.NORMAL:
                    ssmgActorMode.Mode1 = 2;
                    ssmgActorMode.Mode2 = 0;
                    break;
                case PlayerMode.KNIGHT_WAR:
                    ssmgActorMode.Mode1 = 34;
                    ssmgActorMode.Mode2 = 2;
                    break;
                case PlayerMode.COLISEUM_MODE:
                    ssmgActorMode.Mode1 = 66;
                    ssmgActorMode.Mode2 = 1;
                    break;
                case PlayerMode.WRP:
                    ssmgActorMode.Mode1 = 258;
                    ssmgActorMode.Mode2 = 4;
                    break;
            }
            this.Client.netIO.SendPacket((Packet)ssmgActorMode);
        }

        /// <summary>
        /// The OnShowEffect.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnShowEffect(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            EffectArg effectArg = (EffectArg)args;
            this.Client.SendNPCShowEffect(effectArg.actorID, effectArg.x, effectArg.y, effectArg.effectID, effectArg.oneTime);
        }

        /// <summary>
        /// The OnActorPossession.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorPossession(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
            if (this.Client == null)
                return;
            PossessionArg possessionArg = (PossessionArg)args;
            if (!possessionArg.cancel)
                this.Client.netIO.SendPacket((Packet)new SSMG_POSSESSION_RESULT()
                {
                    FromID = possessionArg.fromID,
                    ToID = possessionArg.toID,
                    Result = possessionArg.result
                });
            else
                this.Client.netIO.SendPacket((Packet)new SSMG_POSSESSION_CANCEL()
                {
                    FromID = possessionArg.fromID,
                    ToID = possessionArg.toID,
                    Position = (PossessionPosition)possessionArg.result,
                    X = possessionArg.x,
                    Y = possessionArg.y,
                    Dir = possessionArg.dir
                });
        }

        /// <summary>
        /// The OnActorPartyUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorPartyUpdate(ActorPC aActor)
        {
            if (this.Client == null)
                return;
            SSMG_PARTY_NAME ssmgPartyName = new SSMG_PARTY_NAME();
            ssmgPartyName.Party(aActor.Party, (SagaDB.Actor.Actor)aActor);
            this.Client.netIO.SendPacket((Packet)ssmgPartyName);
        }

        /// <summary>
        /// The OnActorSpeedChange.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorSpeedChange(SagaDB.Actor.Actor mActor)
        {
            if (this.Client == null)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_SPEED()
            {
                ActorID = mActor.ActorID,
                Speed = mActor.Speed
            });
        }

        /// <summary>
        /// The OnSignUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnSignUpdate(SagaDB.Actor.Actor aActor)
        {
            if (this.Client == null)
                return;
            if (aActor.type == ActorType.PC)
            {
                this.Client.netIO.SendPacket((Packet)new SSMG_CHAT_SIGN()
                {
                    ActorID = aActor.ActorID,
                    Message = ((ActorPC)aActor).Sign
                });
            }
            else
            {
                if (aActor.type != ActorType.EVENT)
                    return;
                this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_EVENT_TITLE_CHANGE()
                {
                    Actor = (ActorEvent)aActor
                });
            }
        }

        /// <summary>
        /// The PropertyUpdate.
        /// </summary>
        /// <param name="arg">The arg<see cref="UpdateEvent"/>.</param>
        /// <param name="para">The para<see cref="int"/>.</param>
        public void PropertyUpdate(UpdateEvent arg, int para)
        {
            switch (arg)
            {
                case UpdateEvent.GOLD:
                    this.Client.SendGoldUpdate();
                    break;
                case UpdateEvent.CP:
                    if (para == 0)
                        break;
                    if (para > 0)
                    {
                        this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.NPC_SHOP_CP_GET, (object)para));
                        break;
                    }
                    this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.NPC_SHOP_CP_LOST, (object)-para));
                    break;
                case UpdateEvent.EP:
                    if (para > 0)
                        this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.EP_INCREASED, (object)para));
                    this.Client.SendActorHPMPSP((SagaDB.Actor.Actor)this.Client.Character);
                    break;
                case UpdateEvent.ECoin:
                    this.Client.SendEXP();
                    if (para > 0)
                    {
                        this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.NPC_SHOP_ECOIN_GET, (object)para));
                        break;
                    }
                    this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.NPC_SHOP_ECOIN_LOST, (object)-para));
                    break;
                case UpdateEvent.SPEED:
                    this.Client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SPEED_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Client.Character, true);
                    break;
                case UpdateEvent.CHAR_INFO:
                    this.Client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Client.Character, true);
                    break;
                case UpdateEvent.LEVEL:
                    Singleton<StatusFactory>.Instance.CalcStatus(this.Client.Character);
                    this.Client.SendPlayerInfo();
                    break;
                case UpdateEvent.STAT_POINT:
                    this.Client.SendPlayerLevel();
                    break;
                case UpdateEvent.MODE:
                    this.Client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Client.Character, true);
                    break;
                case UpdateEvent.VCASH_POINT:
                    MapServer.charDB.SaveVShop(this.Client.Character);
                    break;
                case UpdateEvent.WRP:
                    if (para == 0)
                        break;
                    MapServer.charDB.SaveWRP(this.Client.Character);
                    this.Client.SendEXP();
                    if (para > 0)
                        this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.WRP_GOT, (object)para));
                    else
                        this.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.WRP_LOST, (object)-para));
                    break;
            }
        }

        /// <summary>
        /// The PropertyRead.
        /// </summary>
        /// <param name="arg">The arg<see cref="UpdateEvent"/>.</param>
        public void PropertyRead(UpdateEvent arg)
        {
            if (this.Client == null || arg != UpdateEvent.VCASH_POINT)
                return;
            MapServer.charDB.GetVShop(this.Client.Character);
        }

        /// <summary>
        /// The OnActorRingUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorRingUpdate(ActorPC aActor)
        {
            if (this.Client == null)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_RING_NAME()
            {
                Player = aActor
            });
        }

        /// <summary>
        /// The OnActorWRPRankingUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorWRPRankingUpdate(ActorPC aActor)
        {
            if (this.Client == null)
                return;
            this.Client.SendWRPRanking(aActor);
        }

        /// <summary>
        /// The OnActorChangeAttackType.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorChangeAttackType(ActorPC aActor)
        {
            if (this.Client == null || this.Client.state == MapClient.SESSION_STATE.DISCONNECTED || !this.Client.Character.Online)
                return;
            this.Client.netIO.SendPacket((Packet)new SSMG_ACTOR_ATTACK_TYPE()
            {
                ActorID = aActor.ActorID,
                AttackType = aActor.Status.attackType
            });
        }
    }
}
