namespace SagaDB.Actor
{
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Quests;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ActorPC" />.
    /// </summary>
    [Serializable]
    public class ActorPC : SagaDB.Actor.Actor, IStats
    {
        /// <summary>
        /// Defines the jointJob.
        /// </summary>
        private PC_JOB jointJob = PC_JOB.NONE;

        /// <summary>
        /// Defines the nextMarionetteTime.
        /// </summary>
        private DateTime nextMarionetteTime = DateTime.Now;

        /// <summary>
        /// Defines the Skills.
        /// </summary>
        public Dictionary<uint, SagaDB.Skill.Skill> Skills = new Dictionary<uint, SagaDB.Skill.Skill>();

        /// <summary>
        /// Defines the Skills2.
        /// </summary>
        public Dictionary<uint, SagaDB.Skill.Skill> Skills2 = new Dictionary<uint, SagaDB.Skill.Skill>();

        /// <summary>
        /// Defines the SkillsReserve.
        /// </summary>
        public Dictionary<uint, SagaDB.Skill.Skill> SkillsReserve = new Dictionary<uint, SagaDB.Skill.Skill>();

        /// <summary>
        /// Defines the npcStates.
        /// </summary>
        private Dictionary<uint, Dictionary<uint, bool>> npcStates = new Dictionary<uint, Dictionary<uint, bool>>();

        /// <summary>
        /// Defines the aStrVar.
        /// </summary>
        private VariableHolder<string, string> aStrVar = new VariableHolder<string, string>("");

        /// <summary>
        /// Defines the aIntVar.
        /// </summary>
        private VariableHolder<string, int> aIntVar = new VariableHolder<string, int>(0);

        /// <summary>
        /// Defines the aMaskVar.
        /// </summary>
        private VariableHolderA<string, BitMask> aMaskVar = new VariableHolderA<string, BitMask>();

        /// <summary>
        /// Defines the cStrVar.
        /// </summary>
        private VariableHolder<string, string> cStrVar = new VariableHolder<string, string>("");

        /// <summary>
        /// Defines the cMaskVar.
        /// </summary>
        private VariableHolderA<string, BitMask> cMaskVar = new VariableHolderA<string, BitMask>();

        /// <summary>
        /// Defines the cIntVar.
        /// </summary>
        private VariableHolder<string, int> cIntVar = new VariableHolder<string, int>(0);

        /// <summary>
        /// Defines the tStrVar.
        /// </summary>
        private VariableHolder<string, string> tStrVar = new VariableHolder<string, string>("");

        /// <summary>
        /// Defines the tIntVar.
        /// </summary>
        private VariableHolder<string, int> tIntVar = new VariableHolder<string, int>(0);

        /// <summary>
        /// Defines the tMask.
        /// </summary>
        private VariableHolderA<string, BitMask> tMask = new VariableHolderA<string, BitMask>();

        /// <summary>
        /// Defines the stamp.
        /// </summary>
        private Stamp stamp = new Stamp();

        /// <summary>
        /// Defines the charID.
        /// </summary>
        private uint charID;

        /// <summary>
        /// Defines the account.
        /// </summary>
        [NonSerialized]
        private Account account;

        /// <summary>
        /// Defines the race.
        /// </summary>
        private PC_RACE race;

        /// <summary>
        /// Defines the gender.
        /// </summary>
        private PC_GENDER gender;

        /// <summary>
        /// Defines the hairStyle.
        /// </summary>
        private byte hairStyle;

        /// <summary>
        /// Defines the hairColor.
        /// </summary>
        private byte hairColor;

        /// <summary>
        /// Defines the wig.
        /// </summary>
        private byte wig;

        /// <summary>
        /// Defines the face.
        /// </summary>
        private byte face;

        /// <summary>
        /// Defines the job.
        /// </summary>
        private PC_JOB job;

        /// <summary>
        /// Defines the lv.
        /// </summary>
        private byte lv;

        /// <summary>
        /// Defines the dlv.
        /// </summary>
        private byte dlv;

        /// <summary>
        /// Defines the djlv.
        /// </summary>
        private byte djlv;

        /// <summary>
        /// Defines the jjlv.
        /// </summary>
        private byte jjlv;

        /// <summary>
        /// Defines the jlv1.
        /// </summary>
        private byte jlv1;

        /// <summary>
        /// Defines the jlv2x.
        /// </summary>
        private byte jlv2x;

        /// <summary>
        /// Defines the jlv2t.
        /// </summary>
        private byte jlv2t;

        /// <summary>
        /// Defines the questRemaining.
        /// </summary>
        private ushort questRemaining;

        /// <summary>
        /// Defines the fame.
        /// </summary>
        private uint fame;

        /// <summary>
        /// Defines the sign.
        /// </summary>
        private string sign;

        /// <summary>
        /// Defines the form.
        /// </summary>
        private DEM_FORM form;

        /// <summary>
        /// Defines the cl.
        /// </summary>
        private short cl;

        /// <summary>
        /// Defines the dcl.
        /// </summary>
        private short dcl;

        /// <summary>
        /// Defines the epUsed.
        /// </summary>
        private short epUsed;

        /// <summary>
        /// Defines the depUsed.
        /// </summary>
        private short depUsed;

        /// <summary>
        /// Defines the motion.
        /// </summary>
        private MotionType motion;

        /// <summary>
        /// Defines the motion_loop.
        /// </summary>
        private bool motion_loop;

        /// <summary>
        /// Defines the online.
        /// </summary>
        private bool online;

        /// <summary>
        /// Defines the str.
        /// </summary>
        private ushort str;

        /// <summary>
        /// Defines the dex.
        /// </summary>
        private ushort dex;

        /// <summary>
        /// Defines the intel.
        /// </summary>
        private ushort intel;

        /// <summary>
        /// Defines the vit.
        /// </summary>
        private ushort vit;

        /// <summary>
        /// Defines the agi.
        /// </summary>
        private ushort agi;

        /// <summary>
        /// Defines the mag.
        /// </summary>
        private ushort mag;

        /// <summary>
        /// Defines the dstr.
        /// </summary>
        private ushort dstr;

        /// <summary>
        /// Defines the ddex.
        /// </summary>
        private ushort ddex;

        /// <summary>
        /// Defines the dintel.
        /// </summary>
        private ushort dintel;

        /// <summary>
        /// Defines the dvit.
        /// </summary>
        private ushort dvit;

        /// <summary>
        /// Defines the dagi.
        /// </summary>
        private ushort dagi;

        /// <summary>
        /// Defines the dmag.
        /// </summary>
        private ushort dmag;

        /// <summary>
        /// Defines the statspoints.
        /// </summary>
        private ushort statspoints;

        /// <summary>
        /// Defines the skillpoint.
        /// </summary>
        private ushort skillpoint;

        /// <summary>
        /// Defines the skillpoint2x.
        /// </summary>
        private ushort skillpoint2x;

        /// <summary>
        /// Defines the skillpoint2t.
        /// </summary>
        private ushort skillpoint2t;

        /// <summary>
        /// Defines the dstatspoints.
        /// </summary>
        private ushort dstatspoints;

        /// <summary>
        /// Defines the dreseve.
        /// </summary>
        private bool dreseve;

        /// <summary>
        /// Defines the wrpRanking.
        /// </summary>
        private uint wrpRanking;

        /// <summary>
        /// Defines the marionette.
        /// </summary>
        private SagaDB.Marionette.Marionette marionette;

        /// <summary>
        /// Defines the pet.
        /// </summary>
        private ActorPet pet;

        /// <summary>
        /// Defines the quest.
        /// </summary>
        private Quest quest;

        /// <summary>
        /// Defines the questNextTime.
        /// </summary>
        private DateTime questNextTime;

        /// <summary>
        /// Defines the epLoginDate.
        /// </summary>
        private DateTime epLoginDate;

        /// <summary>
        /// Defines the epGreetingDate.
        /// </summary>
        private DateTime epGreetingDate;

        /// <summary>
        /// Defines the size.
        /// </summary>
        private uint size;

        /// <summary>
        /// Defines the cexp.
        /// </summary>
        private uint cexp;

        /// <summary>
        /// Defines the jexp.
        /// </summary>
        private uint jexp;

        /// <summary>
        /// Defines the cp.
        /// </summary>
        private uint cp;

        /// <summary>
        /// Defines the ecoin.
        /// </summary>
        private uint ecoin;

        /// <summary>
        /// Defines the dcexp.
        /// </summary>
        private uint dcexp;

        /// <summary>
        /// Defines the djexp.
        /// </summary>
        private uint djexp;

        /// <summary>
        /// Defines the jjexp.
        /// </summary>
        private uint jjexp;

        /// <summary>
        /// Defines the wrp.
        /// </summary>
        private int wrp;

        /// <summary>
        /// Defines the gold.
        /// </summary>
        private int gold;

        /// <summary>
        /// Defines the slot.
        /// </summary>
        private byte slot;

        /// <summary>
        /// Defines the battleStatus.
        /// </summary>
        private byte battleStatus;

        /// <summary>
        /// Defines the save_map.
        /// </summary>
        private uint save_map;

        /// <summary>
        /// Defines the save_x.
        /// </summary>
        private byte save_x;

        /// <summary>
        /// Defines the save_y.
        /// </summary>
        private byte save_y;

        /// <summary>
        /// Defines the inventory.
        /// </summary>
        private Inventory inventory;

        /// <summary>
        /// Defines the possessionTarget.
        /// </summary>
        private uint possessionTarget;

        /// <summary>
        /// Defines the possessionPosition.
        /// </summary>
        private PossessionPosition possessionPosition;

        /// <summary>
        /// Defines the party.
        /// </summary>
        private SagaDB.Party.Party party;

        /// <summary>
        /// Defines the ring.
        /// </summary>
        private SagaDB.Ring.Ring ring;

        /// <summary>
        /// Defines the fgarden.
        /// </summary>
        private SagaDB.FGarden.FGarden fgarden;

        /// <summary>
        /// Defines the vpoints.
        /// </summary>
        private uint vpoints;

        /// <summary>
        /// Defines the usedVPoints.
        /// </summary>
        private uint usedVPoints;

        /// <summary>
        /// Defines the golem.
        /// </summary>
        private ActorGolem golem;

        /// <summary>
        /// Defines the dungeonID.
        /// </summary>
        private uint dungeonID;

        /// <summary>
        /// Defines the tranceID.
        /// </summary>
        private uint tranceID;

        /// <summary>
        /// Defines the mode.
        /// </summary>
        private PlayerMode mode;

        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        public uint Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorPC"/> class.
        /// </summary>
        public ActorPC()
        {
            this.type = ActorType.PC;
            this.sightRange = Global.MAX_SIGHT_RANGE;
            this.Speed = (ushort)420;
            this.inventory = new Inventory(this);
        }

        /// <summary>
        /// Gets or sets the CharID.
        /// </summary>
        public uint CharID
        {
            get
            {
                return this.charID;
            }
            set
            {
                this.charID = value;
            }
        }

        /// <summary>
        /// Gets or sets the Account.
        /// </summary>
        public Account Account
        {
            get
            {
                return this.account;
            }
            set
            {
                this.account = value;
            }
        }

        /// <summary>
        /// Gets or sets the Race.
        /// </summary>
        public PC_RACE Race
        {
            get
            {
                return this.race;
            }
            set
            {
                this.race = value;
            }
        }

        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        public PC_GENDER Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the HairStyle.
        /// </summary>
        public byte HairStyle
        {
            get
            {
                return this.hairStyle;
            }
            set
            {
                this.hairStyle = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// Gets or sets the HairColor.
        /// </summary>
        public byte HairColor
        {
            get
            {
                return this.hairColor;
            }
            set
            {
                this.hairColor = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Wig.
        /// </summary>
        public byte Wig
        {
            get
            {
                return this.wig;
            }
            set
            {
                this.wig = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Face.
        /// </summary>
        public byte Face
        {
            get
            {
                return this.face;
            }
            set
            {
                this.face = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Job.
        /// </summary>
        public PC_JOB Job
        {
            get
            {
                return this.job;
            }
            set
            {
                this.job = value;
            }
        }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        public override byte Level
        {
            get
            {
                return this.lv;
            }
            set
            {
                this.lv = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// Gets or sets the DominionLevel.
        /// </summary>
        public byte DominionLevel
        {
            get
            {
                return this.dlv;
            }
            set
            {
                this.dlv = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// Gets or sets the DominionJobLevel.
        /// </summary>
        public byte DominionJobLevel
        {
            get
            {
                return this.djlv;
            }
            set
            {
                this.djlv = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// Gets or sets the JointJobLevel.
        /// </summary>
        public byte JointJobLevel
        {
            get
            {
                return this.jjlv;
            }
            set
            {
                this.jjlv = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// Gets the CurrentJobLevel.
        /// </summary>
        public byte CurrentJobLevel
        {
            get
            {
                if (this.Job == this.JobBasic)
                    return this.JobLevel1;
                if (this.Job == this.Job2X)
                    return this.JobLevel2X;
                if (this.Job == this.Job2T)
                    return this.JobLevel2T;
                return this.JobLevel1;
            }
        }

        /// <summary>
        /// Gets or sets the JobLevel1.
        /// </summary>
        public byte JobLevel1
        {
            get
            {
                return this.jlv1;
            }
            set
            {
                this.jlv1 = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// Gets or sets the QuestRemaining.
        /// </summary>
        public ushort QuestRemaining
        {
            get
            {
                return this.questRemaining;
            }
            set
            {
                this.questRemaining = value;
            }
        }

        /// <summary>
        /// Gets or sets the JobLevel2X.
        /// </summary>
        public byte JobLevel2X
        {
            get
            {
                return this.jlv2x;
            }
            set
            {
                this.jlv2x = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// Gets or sets the JobLevel2T.
        /// </summary>
        public byte JobLevel2T
        {
            get
            {
                return this.jlv2t;
            }
            set
            {
                this.jlv2t = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.LEVEL, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Slot.
        /// </summary>
        public byte Slot
        {
            get
            {
                return this.slot;
            }
            set
            {
                this.slot = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether InDominionWorld.
        /// </summary>
        public bool InDominionWorld
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID))
                    return Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion);
                uint key = this.MapID / 1000U * 1000U;
                return Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(key) && Singleton<MapInfoFactory>.Instance.MapInfo[key].Flag.Test(MapFlags.Dominion);
            }
        }

        /// <summary>
        /// Gets the PossesionedActors.
        /// </summary>
        public List<ActorPC> PossesionedActors
        {
            get
            {
                List<ActorPC> actorPcList = new List<ActorPC>();
                if (this.inventory != null)
                {
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE) && this.inventory.Equipments[EnumEquipSlot.CHEST_ACCE].PossessionedActor != null && !actorPcList.Contains(this.inventory.Equipments[EnumEquipSlot.CHEST_ACCE].PossessionedActor))
                        actorPcList.Add(this.inventory.Equipments[EnumEquipSlot.CHEST_ACCE].PossessionedActor);
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY) && this.inventory.Equipments[EnumEquipSlot.UPPER_BODY].PossessionedActor != null && !actorPcList.Contains(this.inventory.Equipments[EnumEquipSlot.UPPER_BODY].PossessionedActor))
                        actorPcList.Add(this.inventory.Equipments[EnumEquipSlot.UPPER_BODY].PossessionedActor);
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && this.inventory.Equipments[EnumEquipSlot.RIGHT_HAND].PossessionedActor != null && !actorPcList.Contains(this.inventory.Equipments[EnumEquipSlot.RIGHT_HAND].PossessionedActor))
                        actorPcList.Add(this.inventory.Equipments[EnumEquipSlot.RIGHT_HAND].PossessionedActor);
                    if (this.inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND) && this.inventory.Equipments[EnumEquipSlot.LEFT_HAND].PossessionedActor != null && !actorPcList.Contains(this.inventory.Equipments[EnumEquipSlot.LEFT_HAND].PossessionedActor))
                        actorPcList.Add(this.inventory.Equipments[EnumEquipSlot.LEFT_HAND].PossessionedActor);
                }
                return actorPcList;
            }
        }

        /// <summary>
        /// Gets or sets the JobJoint.
        /// </summary>
        public PC_JOB JobJoint
        {
            get
            {
                return this.jointJob;
            }
            set
            {
                this.jointJob = value;
            }
        }

        /// <summary>
        /// Gets the JobBasic.
        /// </summary>
        public PC_JOB JobBasic
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                        return PC_JOB.SWORDMAN;
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                        return PC_JOB.FENCER;
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                        return PC_JOB.SCOUT;
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                        return PC_JOB.ARCHER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                        return PC_JOB.WIZARD;
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                        return PC_JOB.SHAMAN;
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                        return PC_JOB.VATES;
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                        return PC_JOB.WARLOCK;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                        return PC_JOB.TATARABE;
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                        return PC_JOB.FARMASIST;
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                        return PC_JOB.RANGER;
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                        return PC_JOB.MERCHANT;
                    default:
                        return PC_JOB.NOVICE;
                }
            }
        }

        /// <summary>
        /// Gets the Job2X.
        /// </summary>
        public PC_JOB Job2X
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                        return PC_JOB.BLADEMASTER;
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                        return PC_JOB.KNIGHT;
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                        return PC_JOB.ASSASSIN;
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                        return PC_JOB.STRIKER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                        return PC_JOB.SORCERER;
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                        return PC_JOB.ELEMENTER;
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                        return PC_JOB.DRUID;
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                        return PC_JOB.CABALIST;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                        return PC_JOB.BLACKSMITH;
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                        return PC_JOB.ALCHEMIST;
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                        return PC_JOB.EXPLORER;
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                        return PC_JOB.TRADER;
                    default:
                        return PC_JOB.NOVICE;
                }
            }
        }

        /// <summary>
        /// Gets the Job2T.
        /// </summary>
        public PC_JOB Job2T
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                        return PC_JOB.BOUNTYHUNTER;
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                        return PC_JOB.DARKSTALKER;
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                        return PC_JOB.COMMAND;
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                        return PC_JOB.GUNNER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                        return PC_JOB.SAGE;
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                        return PC_JOB.ENCHANTER;
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                        return PC_JOB.BARD;
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                        return PC_JOB.NECROMANCER;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                        return PC_JOB.MACHINERY;
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                        return PC_JOB.MARIONEST;
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                        return PC_JOB.TREASUREHUNTER;
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                        return PC_JOB.GAMBLER;
                    default:
                        return PC_JOB.NOVICE;
                }
            }
        }

        /// <summary>
        /// Gets the JobType.
        /// </summary>
        public JobType JobType
        {
            get
            {
                switch (this.job)
                {
                    case PC_JOB.SWORDMAN:
                    case PC_JOB.BLADEMASTER:
                    case PC_JOB.BOUNTYHUNTER:
                    case PC_JOB.FENCER:
                    case PC_JOB.KNIGHT:
                    case PC_JOB.DARKSTALKER:
                    case PC_JOB.SCOUT:
                    case PC_JOB.ASSASSIN:
                    case PC_JOB.COMMAND:
                    case PC_JOB.ARCHER:
                    case PC_JOB.STRIKER:
                    case PC_JOB.GUNNER:
                        return JobType.FIGHTER;
                    case PC_JOB.WIZARD:
                    case PC_JOB.SORCERER:
                    case PC_JOB.SAGE:
                    case PC_JOB.SHAMAN:
                    case PC_JOB.ELEMENTER:
                    case PC_JOB.ENCHANTER:
                    case PC_JOB.VATES:
                    case PC_JOB.DRUID:
                    case PC_JOB.BARD:
                    case PC_JOB.WARLOCK:
                    case PC_JOB.CABALIST:
                    case PC_JOB.NECROMANCER:
                        return JobType.SPELLUSER;
                    case PC_JOB.TATARABE:
                    case PC_JOB.BLACKSMITH:
                    case PC_JOB.MACHINERY:
                    case PC_JOB.FARMASIST:
                    case PC_JOB.ALCHEMIST:
                    case PC_JOB.MARIONEST:
                    case PC_JOB.RANGER:
                    case PC_JOB.EXPLORER:
                    case PC_JOB.TREASUREHUNTER:
                    case PC_JOB.MERCHANT:
                    case PC_JOB.TRADER:
                    case PC_JOB.GAMBLER:
                        return JobType.BACKPACKER;
                    default:
                        return JobType.NOVICE;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Str.
        /// </summary>
        public ushort Str
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online && Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                    return this.dstr;
                return this.str;
            }
            set
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    if (Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                        this.dstr = value;
                    else
                        this.str = value;
                }
                else
                    this.str = value;
            }
        }

        /// <summary>
        /// Gets or sets the Dex.
        /// </summary>
        public ushort Dex
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online && Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                    return this.ddex;
                return this.dex;
            }
            set
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    if (Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                        this.ddex = value;
                    else
                        this.dex = value;
                }
                else
                    this.dex = value;
            }
        }

        /// <summary>
        /// Gets or sets the Int.
        /// </summary>
        public ushort Int
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online && Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                    return this.dintel;
                return this.intel;
            }
            set
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    if (Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                        this.dintel = value;
                    else
                        this.intel = value;
                }
                else
                    this.intel = value;
            }
        }

        /// <summary>
        /// Gets or sets the Vit.
        /// </summary>
        public ushort Vit
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online && Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                    return this.dvit;
                return this.vit;
            }
            set
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    if (Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                        this.dvit = value;
                    else
                        this.vit = value;
                }
                else
                    this.vit = value;
            }
        }

        /// <summary>
        /// Gets or sets the Agi.
        /// </summary>
        public ushort Agi
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online && Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                    return this.dagi;
                return this.agi;
            }
            set
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    if (Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                        this.dagi = value;
                    else
                        this.agi = value;
                }
                else
                    this.agi = value;
            }
        }

        /// <summary>
        /// Gets or sets the Mag.
        /// </summary>
        public ushort Mag
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online && Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                    return this.dmag;
                return this.mag;
            }
            set
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    if (Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                        this.dmag = value;
                    else
                        this.mag = value;
                }
                else
                    this.mag = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionStr.
        /// </summary>
        public ushort DominionStr
        {
            get
            {
                return this.dstr;
            }
            set
            {
                this.dstr = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionDex.
        /// </summary>
        public ushort DominionDex
        {
            get
            {
                return this.ddex;
            }
            set
            {
                this.ddex = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionInt.
        /// </summary>
        public ushort DominionInt
        {
            get
            {
                return this.dintel;
            }
            set
            {
                this.dintel = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionVit.
        /// </summary>
        public ushort DominionVit
        {
            get
            {
                return this.dvit;
            }
            set
            {
                this.dvit = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionAgi.
        /// </summary>
        public ushort DominionAgi
        {
            get
            {
                return this.dagi;
            }
            set
            {
                this.dagi = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionMag.
        /// </summary>
        public ushort DominionMag
        {
            get
            {
                return this.dmag;
            }
            set
            {
                this.dmag = value;
            }
        }

        /// <summary>
        /// Gets or sets the Gold.
        /// </summary>
        public int Gold
        {
            get
            {
                return this.gold;
            }
            set
            {
                if (value > 99999999)
                    value = 99999999;
                if (value < 0)
                    value = 0;
                if (value - this.gold != 0)
                    Logger.LogGoldChange(this.Name + "(" + (object)this.charID + ")", value - this.gold);
                this.gold = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.GOLD, 0);
            }
        }

        /// <summary>
        /// Gets or sets the CP.
        /// </summary>
        public uint CP
        {
            get
            {
                return this.cp;
            }
            set
            {
                if (value > 99999999U)
                    value = 99999999U;
                int para = (int)value - (int)this.cp;
                this.cp = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.CP, para);
            }
        }

        /// <summary>
        /// Gets or sets the ECoin.
        /// </summary>
        public uint ECoin
        {
            get
            {
                return this.ecoin;
            }
            set
            {
                if (value > 99999999U)
                    value = 99999999U;
                int para = (int)value - (int)this.ecoin;
                this.ecoin = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.ECoin, para);
            }
        }

        /// <summary>
        /// Gets or sets the EPUsed.
        /// </summary>
        public short EPUsed
        {
            get
            {
                return this.epUsed;
            }
            set
            {
                this.epUsed = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionEPUsed.
        /// </summary>
        public short DominionEPUsed
        {
            get
            {
                return this.depUsed;
            }
            set
            {
                this.depUsed = value;
            }
        }

        /// <summary>
        /// Gets or sets the CL.
        /// </summary>
        public short CL
        {
            get
            {
                return this.cl;
            }
            set
            {
                this.cl = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionCL.
        /// </summary>
        public short DominionCL
        {
            get
            {
                return this.dcl;
            }
            set
            {
                this.dcl = value;
            }
        }

        /// <summary>
        /// Gets or sets the CEXP.
        /// </summary>
        public uint CEXP
        {
            get
            {
                return this.cexp;
            }
            set
            {
                this.cexp = value;
            }
        }

        /// <summary>
        /// Gets or sets the JEXP.
        /// </summary>
        public uint JEXP
        {
            get
            {
                return this.jexp;
            }
            set
            {
                this.jexp = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionCEXP.
        /// </summary>
        public uint DominionCEXP
        {
            get
            {
                return this.dcexp;
            }
            set
            {
                this.dcexp = value;
            }
        }

        /// <summary>
        /// Gets or sets the DominionJEXP.
        /// </summary>
        public uint DominionJEXP
        {
            get
            {
                return this.djexp;
            }
            set
            {
                this.djexp = value;
            }
        }

        /// <summary>
        /// Gets or sets the JointJEXP.
        /// </summary>
        public uint JointJEXP
        {
            get
            {
                return this.jjexp;
            }
            set
            {
                this.jjexp = value;
            }
        }

        /// <summary>
        /// Gets or sets the WRP.
        /// </summary>
        public int WRP
        {
            get
            {
                return this.wrp;
            }
            set
            {
                int para = value - this.wrp;
                this.wrp = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.WRP, para);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Online.
        /// </summary>
        public bool Online
        {
            get
            {
                return this.online;
            }
            set
            {
                this.online = value;
            }
        }

        /// <summary>
        /// Gets or sets the SaveMap.
        /// </summary>
        public uint SaveMap
        {
            get
            {
                return this.save_map;
            }
            set
            {
                this.save_map = value;
            }
        }

        /// <summary>
        /// Gets or sets the SaveX.
        /// </summary>
        public byte SaveX
        {
            get
            {
                return this.save_x;
            }
            set
            {
                this.save_x = value;
            }
        }

        /// <summary>
        /// Gets or sets the SaveY.
        /// </summary>
        public byte SaveY
        {
            get
            {
                return this.save_y;
            }
            set
            {
                this.save_y = value;
            }
        }

        /// <summary>
        /// Gets or sets the Form.
        /// </summary>
        public DEM_FORM Form
        {
            get
            {
                return this.form;
            }
            set
            {
                this.form = value;
            }
        }

        /// <summary>
        /// Gets or sets the BattleStatus.
        /// </summary>
        public byte BattleStatus
        {
            get
            {
                return this.battleStatus;
            }
            set
            {
                this.battleStatus = value;
            }
        }

        /// <summary>
        /// Gets or sets the StatsPoint.
        /// </summary>
        public ushort StatsPoint
        {
            get
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online && Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                    return this.dstatspoints;
                return this.statspoints;
            }
            set
            {
                if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(this.MapID) && this.Online)
                {
                    if (Singleton<MapInfoFactory>.Instance.MapInfo[this.MapID].Flag.Test(MapFlags.Dominion))
                        this.dstatspoints = value;
                    else
                        this.statspoints = value;
                }
                else
                    this.statspoints = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.STAT_POINT, 0);
            }
        }

        /// <summary>
        /// Gets or sets the DominionStatsPoint.
        /// </summary>
        public ushort DominionStatsPoint
        {
            get
            {
                return this.dstatspoints;
            }
            set
            {
                this.dstatspoints = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.STAT_POINT, 0);
            }
        }

        /// <summary>
        /// Gets or sets the SkillPoint.
        /// </summary>
        public ushort SkillPoint
        {
            get
            {
                return this.skillpoint;
            }
            set
            {
                this.skillpoint = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.STAT_POINT, 0);
            }
        }

        /// <summary>
        /// Gets or sets the SkillPoint2X.
        /// </summary>
        public ushort SkillPoint2X
        {
            get
            {
                return this.skillpoint2x;
            }
            set
            {
                this.skillpoint2x = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.STAT_POINT, 0);
            }
        }

        /// <summary>
        /// Gets or sets the SkillPoint2T.
        /// </summary>
        public ushort SkillPoint2T
        {
            get
            {
                return this.skillpoint2t;
            }
            set
            {
                this.skillpoint2t = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.STAT_POINT, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Inventory.
        /// </summary>
        public Inventory Inventory
        {
            get
            {
                return this.inventory;
            }
            set
            {
                this.inventory = value;
            }
        }

        /// <summary>
        /// Gets or sets the Motion.
        /// </summary>
        public MotionType Motion
        {
            get
            {
                return this.motion;
            }
            set
            {
                this.motion = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether MotionLoop.
        /// </summary>
        public bool MotionLoop
        {
            get
            {
                return this.motion_loop;
            }
            set
            {
                this.motion_loop = value;
            }
        }

        /// <summary>
        /// Gets the AStr.
        /// </summary>
        public VariableHolder<string, string> AStr
        {
            get
            {
                return this.aStrVar;
            }
        }

        /// <summary>
        /// Gets the AInt.
        /// </summary>
        public VariableHolder<string, int> AInt
        {
            get
            {
                return this.aIntVar;
            }
        }

        /// <summary>
        /// Gets the CStr.
        /// </summary>
        public VariableHolder<string, string> CStr
        {
            get
            {
                return this.cStrVar;
            }
        }

        /// <summary>
        /// Gets the CInt.
        /// </summary>
        public VariableHolder<string, int> CInt
        {
            get
            {
                return this.cIntVar;
            }
        }

        /// <summary>
        /// Gets the TStr.
        /// </summary>
        public VariableHolder<string, string> TStr
        {
            get
            {
                return this.tStrVar;
            }
        }

        /// <summary>
        /// Gets the TInt.
        /// </summary>
        public VariableHolder<string, int> TInt
        {
            get
            {
                return this.tIntVar;
            }
        }

        /// <summary>
        /// Gets the TMask.
        /// </summary>
        public VariableHolderA<string, BitMask> TMask
        {
            get
            {
                return this.tMask;
            }
        }

        /// <summary>
        /// Gets the CMask.
        /// </summary>
        public VariableHolderA<string, BitMask> CMask
        {
            get
            {
                return this.cMaskVar;
            }
        }

        /// <summary>
        /// Gets the AMask.
        /// </summary>
        public VariableHolderA<string, BitMask> AMask
        {
            get
            {
                return this.aMaskVar;
            }
        }

        /// <summary>
        /// The ClearVarialbes.
        /// </summary>
        public void ClearVarialbes()
        {
            this.aIntVar = (VariableHolder<string, int>)null;
            this.aStrVar = (VariableHolder<string, string>)null;
            this.aMaskVar = (VariableHolderA<string, BitMask>)null;
            this.cIntVar = (VariableHolder<string, int>)null;
            this.cStrVar = (VariableHolder<string, string>)null;
            this.cMaskVar = (VariableHolderA<string, BitMask>)null;
            this.tIntVar = (VariableHolder<string, int>)null;
            this.tStrVar = (VariableHolder<string, string>)null;
            this.tMask = (VariableHolderA<string, BitMask>)null;
        }

        /// <summary>
        /// Gets or sets the Marionette.
        /// </summary>
        public SagaDB.Marionette.Marionette Marionette
        {
            get
            {
                return this.marionette;
            }
            set
            {
                this.marionette = value;
            }
        }

        /// <summary>
        /// Gets or sets the NextMarionetteTime.
        /// </summary>
        public DateTime NextMarionetteTime
        {
            get
            {
                return this.nextMarionetteTime;
            }
            set
            {
                this.nextMarionetteTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the Pet.
        /// </summary>
        public ActorPet Pet
        {
            get
            {
                return this.pet;
            }
            set
            {
                this.pet = value;
            }
        }

        /// <summary>
        /// Gets or sets the PossessionTarget.
        /// </summary>
        public uint PossessionTarget
        {
            get
            {
                return this.possessionTarget;
            }
            set
            {
                this.possessionTarget = value;
            }
        }

        /// <summary>
        /// Gets or sets the PossessionPosition.
        /// </summary>
        public PossessionPosition PossessionPosition
        {
            get
            {
                return this.possessionPosition;
            }
            set
            {
                this.possessionPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets the Quest.
        /// </summary>
        public Quest Quest
        {
            get
            {
                return this.quest;
            }
            set
            {
                this.quest = value;
            }
        }

        /// <summary>
        /// Gets or sets the QuestNextResetTime.
        /// </summary>
        public DateTime QuestNextResetTime
        {
            get
            {
                return this.questNextTime;
            }
            set
            {
                this.questNextTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the EPLoginTime.
        /// </summary>
        public DateTime EPLoginTime
        {
            get
            {
                return this.epLoginDate;
            }
            set
            {
                this.epLoginDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the EPGreetingTime.
        /// </summary>
        public DateTime EPGreetingTime
        {
            get
            {
                return this.epGreetingDate;
            }
            set
            {
                this.epGreetingDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the Fame.
        /// </summary>
        public uint Fame
        {
            get
            {
                return this.fame;
            }
            set
            {
                if (value > (uint)int.MaxValue)
                    this.fame = 0U;
                else
                    this.fame = value;
            }
        }

        /// <summary>
        /// Gets or sets the Party.
        /// </summary>
        public SagaDB.Party.Party Party
        {
            get
            {
                return this.party;
            }
            set
            {
                this.party = value;
            }
        }

        /// <summary>
        /// Gets or sets the Ring.
        /// </summary>
        public SagaDB.Ring.Ring Ring
        {
            get
            {
                return this.ring;
            }
            set
            {
                this.ring = value;
            }
        }

        /// <summary>
        /// Gets or sets the Sign.
        /// </summary>
        public string Sign
        {
            get
            {
                return this.sign;
            }
            set
            {
                this.sign = value;
            }
        }

        /// <summary>
        /// Gets or sets the Mode.
        /// </summary>
        public PlayerMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.MODE, 0);
            }
        }

        /// <summary>
        /// Gets or sets the FGarden.
        /// </summary>
        public SagaDB.FGarden.FGarden FGarden
        {
            get
            {
                return this.fgarden;
            }
            set
            {
                this.fgarden = value;
            }
        }

        /// <summary>
        /// Gets or sets the VShopPoints.
        /// </summary>
        public uint VShopPoints
        {
            get
            {
                if (this.e != null)
                    this.e.PropertyRead(UpdateEvent.VCASH_POINT);
                return this.vpoints;
            }
            set
            {
                this.vpoints = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.VCASH_POINT, 0);
            }
        }

        /// <summary>
        /// Gets or sets the UsedVShopPoints.
        /// </summary>
        public uint UsedVShopPoints
        {
            get
            {
                if (this.e != null)
                    this.e.PropertyRead(UpdateEvent.VCASH_POINT);
                return this.usedVPoints;
            }
            set
            {
                this.usedVPoints = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.VCASH_POINT, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Golem.
        /// </summary>
        public ActorGolem Golem
        {
            get
            {
                return this.golem;
            }
            set
            {
                this.golem = value;
            }
        }

        /// <summary>
        /// Gets or sets the DungeonID.
        /// </summary>
        public uint DungeonID
        {
            get
            {
                return this.dungeonID;
            }
            set
            {
                this.dungeonID = value;
            }
        }

        /// <summary>
        /// Gets the Stamp.
        /// </summary>
        public Stamp Stamp
        {
            get
            {
                return this.stamp;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DominionReserveSkill.
        /// </summary>
        public bool DominionReserveSkill
        {
            get
            {
                return this.dreseve;
            }
            set
            {
                this.dreseve = value;
            }
        }

        /// <summary>
        /// Gets or sets the WRPRanking.
        /// </summary>
        public uint WRPRanking
        {
            get
            {
                return this.wrpRanking;
            }
            set
            {
                this.wrpRanking = value;
            }
        }

        /// <summary>
        /// Gets or sets the TranceID.
        /// </summary>
        public uint TranceID
        {
            get
            {
                return this.tranceID;
            }
            set
            {
                this.tranceID = value;
            }
        }

        /// <summary>
        /// Gets the NPCStates.
        /// </summary>
        public Dictionary<uint, Dictionary<uint, bool>> NPCStates
        {
            get
            {
                return this.npcStates;
            }
        }
    }
}
