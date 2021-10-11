namespace SagaDB.Actor
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Status" />.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Defines the zenList.
        /// </summary>
        public List<ushort> zenList = new List<ushort>();

        /// <summary>
        /// Defines the darkZenList.
        /// </summary>
        public List<ushort> darkZenList = new List<ushort>();

        /// <summary>
        /// Defines the doubleUpList.
        /// </summary>
        public List<ushort> doubleUpList = new List<ushort>();

        /// <summary>
        /// Defines the delayCancelList.
        /// </summary>
        public Dictionary<ushort, int> delayCancelList = new Dictionary<ushort, int>();

        /// <summary>
        /// Defines the hp_rate_item.
        /// </summary>
        public short hp_rate_item = 100;

        /// <summary>
        /// Defines the sp_rate_item.
        /// </summary>
        public short sp_rate_item = 100;

        /// <summary>
        /// Defines the mp_rate_item.
        /// </summary>
        public short mp_rate_item = 100;

        /// <summary>
        /// Defines the attackStamp.
        /// </summary>
        public DateTime attackStamp = DateTime.Now;

        /// <summary>
        /// Defines the attackingActors.
        /// </summary>
        public List<SagaDB.Actor.Actor> attackingActors = new List<SagaDB.Actor.Actor>();

        /// <summary>
        /// Defines the elements_item.
        /// </summary>
        public Dictionary<Elements, int> elements_item = new Dictionary<Elements, int>();

        /// <summary>
        /// Defines the attackElements_item.
        /// </summary>
        public Dictionary<Elements, int> attackElements_item = new Dictionary<Elements, int>();

        /// <summary>
        /// Defines the Additions.
        /// </summary>
        public Dictionary<string, Addition> Additions = new Dictionary<string, Addition>();

        /// <summary>
        /// Defines the owner.
        /// </summary>
        private SagaDB.Actor.Actor owner;

        /// <summary>
        /// Defines the min_atk1.
        /// </summary>
        public ushort min_atk1;

        /// <summary>
        /// Defines the min_atk2.
        /// </summary>
        public ushort min_atk2;

        /// <summary>
        /// Defines the min_atk3.
        /// </summary>
        public ushort min_atk3;

        /// <summary>
        /// Defines the max_atk1.
        /// </summary>
        public ushort max_atk1;

        /// <summary>
        /// Defines the max_atk2.
        /// </summary>
        public ushort max_atk2;

        /// <summary>
        /// Defines the max_atk3.
        /// </summary>
        public ushort max_atk3;

        /// <summary>
        /// Defines the min_matk.
        /// </summary>
        public ushort min_matk;

        /// <summary>
        /// Defines the max_matk.
        /// </summary>
        public ushort max_matk;

        /// <summary>
        /// Defines the min_atk_ori.
        /// </summary>
        public ushort min_atk_ori;

        /// <summary>
        /// Defines the max_atk_ori.
        /// </summary>
        public ushort max_atk_ori;

        /// <summary>
        /// Defines the min_matk_ori.
        /// </summary>
        public ushort min_matk_ori;

        /// <summary>
        /// Defines the max_matk_ori.
        /// </summary>
        public ushort max_matk_ori;

        /// <summary>
        /// Defines the atk1_item.
        /// </summary>
        public short atk1_item;

        /// <summary>
        /// Defines the atk2_item.
        /// </summary>
        public short atk2_item;

        /// <summary>
        /// Defines the atk3_item.
        /// </summary>
        public short atk3_item;

        /// <summary>
        /// Defines the matk_item.
        /// </summary>
        public short matk_item;

        /// <summary>
        /// Defines the guard_item.
        /// </summary>
        public short guard_item;

        /// <summary>
        /// Defines the min_atk1_mario.
        /// </summary>
        public short min_atk1_mario;

        /// <summary>
        /// Defines the min_atk2_mario.
        /// </summary>
        public short min_atk2_mario;

        /// <summary>
        /// Defines the min_atk3_mario.
        /// </summary>
        public short min_atk3_mario;

        /// <summary>
        /// Defines the max_atk1_mario.
        /// </summary>
        public short max_atk1_mario;

        /// <summary>
        /// Defines the max_atk2_mario.
        /// </summary>
        public short max_atk2_mario;

        /// <summary>
        /// Defines the max_atk3_mario.
        /// </summary>
        public short max_atk3_mario;

        /// <summary>
        /// Defines the min_matk_mario.
        /// </summary>
        public short min_matk_mario;

        /// <summary>
        /// Defines the max_matk_mario.
        /// </summary>
        public short max_matk_mario;

        /// <summary>
        /// Defines the min_atk1_skill.
        /// </summary>
        public short min_atk1_skill;

        /// <summary>
        /// Defines the min_atk2_skill.
        /// </summary>
        public short min_atk2_skill;

        /// <summary>
        /// Defines the min_atk3_skill.
        /// </summary>
        public short min_atk3_skill;

        /// <summary>
        /// Defines the max_atk1_skill.
        /// </summary>
        public short max_atk1_skill;

        /// <summary>
        /// Defines the max_atk2_skill.
        /// </summary>
        public short max_atk2_skill;

        /// <summary>
        /// Defines the max_atk3_skill.
        /// </summary>
        public short max_atk3_skill;

        /// <summary>
        /// Defines the min_matk_skill.
        /// </summary>
        public short min_matk_skill;

        /// <summary>
        /// Defines the max_matk_skill.
        /// </summary>
        public short max_matk_skill;

        /// <summary>
        /// Defines the def.
        /// </summary>
        public ushort def;

        /// <summary>
        /// Defines the mdef.
        /// </summary>
        public ushort mdef;

        /// <summary>
        /// Defines the def_add.
        /// </summary>
        public short def_add;

        /// <summary>
        /// Defines the mdef_add.
        /// </summary>
        public short mdef_add;

        /// <summary>
        /// Defines the def_mario.
        /// </summary>
        public short def_mario;

        /// <summary>
        /// Defines the def_add_mario.
        /// </summary>
        public short def_add_mario;

        /// <summary>
        /// Defines the mdef_mario.
        /// </summary>
        public short mdef_mario;

        /// <summary>
        /// Defines the mdef_add_mario.
        /// </summary>
        public short mdef_add_mario;

        /// <summary>
        /// Defines the def_skill.
        /// </summary>
        public short def_skill;

        /// <summary>
        /// Defines the def_add_skill.
        /// </summary>
        public short def_add_skill;

        /// <summary>
        /// Defines the mdef_skill.
        /// </summary>
        public short mdef_skill;

        /// <summary>
        /// Defines the mdef_add_skill.
        /// </summary>
        public short mdef_add_skill;

        /// <summary>
        /// Defines the hit_melee.
        /// </summary>
        public ushort hit_melee;

        /// <summary>
        /// Defines the hit_ranged.
        /// </summary>
        public ushort hit_ranged;

        /// <summary>
        /// Defines the hit_magic.
        /// </summary>
        public ushort hit_magic;

        /// <summary>
        /// Defines the hit_critical.
        /// </summary>
        public ushort hit_critical;

        /// <summary>
        /// Defines the hit_melee_item.
        /// </summary>
        public short hit_melee_item;

        /// <summary>
        /// Defines the hit_ranged_item.
        /// </summary>
        public short hit_ranged_item;

        /// <summary>
        /// Defines the hit_melee_skill.
        /// </summary>
        public short hit_melee_skill;

        /// <summary>
        /// Defines the hit_ranged_skill.
        /// </summary>
        public short hit_ranged_skill;

        /// <summary>
        /// Defines the avoid_melee.
        /// </summary>
        public ushort avoid_melee;

        /// <summary>
        /// Defines the avoid_ranged.
        /// </summary>
        public ushort avoid_ranged;

        /// <summary>
        /// Defines the avoid_magic.
        /// </summary>
        public ushort avoid_magic;

        /// <summary>
        /// Defines the avoid_critical.
        /// </summary>
        public ushort avoid_critical;

        /// <summary>
        /// Defines the avoid_melee_item.
        /// </summary>
        public short avoid_melee_item;

        /// <summary>
        /// Defines the avoid_ranged_item.
        /// </summary>
        public short avoid_ranged_item;

        /// <summary>
        /// Defines the avoid_melee_skill.
        /// </summary>
        public short avoid_melee_skill;

        /// <summary>
        /// Defines the avoid_ranged_skill.
        /// </summary>
        public short avoid_ranged_skill;

        /// <summary>
        /// Defines the cri_skill.
        /// </summary>
        public short cri_skill;

        /// <summary>
        /// Defines the aspd.
        /// </summary>
        public short aspd;

        /// <summary>
        /// Defines the cspd.
        /// </summary>
        public short cspd;

        /// <summary>
        /// Defines the aspd_skill.
        /// </summary>
        public short aspd_skill;

        /// <summary>
        /// Defines the cspd_skill.
        /// </summary>
        public short cspd_skill;

        /// <summary>
        /// Defines the aspd_skill_limit.
        /// </summary>
        public short aspd_skill_limit;

        /// <summary>
        /// Defines the cspd_skill_limit.
        /// </summary>
        public short cspd_skill_limit;

        /// <summary>
        /// Defines the aspd_skill_perc.
        /// </summary>
        public float aspd_skill_perc;

        /// <summary>
        /// Defines the str_rev.
        /// </summary>
        public ushort str_rev;

        /// <summary>
        /// Defines the dex_rev.
        /// </summary>
        public ushort dex_rev;

        /// <summary>
        /// Defines the int_rev.
        /// </summary>
        public ushort int_rev;

        /// <summary>
        /// Defines the vit_rev.
        /// </summary>
        public ushort vit_rev;

        /// <summary>
        /// Defines the agi_rev.
        /// </summary>
        public ushort agi_rev;

        /// <summary>
        /// Defines the mag_rev.
        /// </summary>
        public ushort mag_rev;

        /// <summary>
        /// Defines the str_item.
        /// </summary>
        public short str_item;

        /// <summary>
        /// Defines the dex_item.
        /// </summary>
        public short dex_item;

        /// <summary>
        /// Defines the int_item.
        /// </summary>
        public short int_item;

        /// <summary>
        /// Defines the vit_item.
        /// </summary>
        public short vit_item;

        /// <summary>
        /// Defines the agi_item.
        /// </summary>
        public short agi_item;

        /// <summary>
        /// Defines the mag_item.
        /// </summary>
        public short mag_item;

        /// <summary>
        /// Defines the m_str_chip.
        /// </summary>
        public short m_str_chip;

        /// <summary>
        /// Defines the m_dex_chip.
        /// </summary>
        public short m_dex_chip;

        /// <summary>
        /// Defines the m_int_chip.
        /// </summary>
        public short m_int_chip;

        /// <summary>
        /// Defines the m_vit_chip.
        /// </summary>
        public short m_vit_chip;

        /// <summary>
        /// Defines the m_agi_chip.
        /// </summary>
        public short m_agi_chip;

        /// <summary>
        /// Defines the m_mag_chip.
        /// </summary>
        public short m_mag_chip;

        /// <summary>
        /// Defines the str_mario.
        /// </summary>
        public short str_mario;

        /// <summary>
        /// Defines the dex_mario.
        /// </summary>
        public short dex_mario;

        /// <summary>
        /// Defines the int_mario.
        /// </summary>
        public short int_mario;

        /// <summary>
        /// Defines the vit_mario.
        /// </summary>
        public short vit_mario;

        /// <summary>
        /// Defines the agi_mario.
        /// </summary>
        public short agi_mario;

        /// <summary>
        /// Defines the mag_mario.
        /// </summary>
        public short mag_mario;

        /// <summary>
        /// Defines the hp_item.
        /// </summary>
        public short hp_item;

        /// <summary>
        /// Defines the sp_item.
        /// </summary>
        public short sp_item;

        /// <summary>
        /// Defines the mp_item.
        /// </summary>
        public short mp_item;

        /// <summary>
        /// Defines the payl_item.
        /// </summary>
        public short payl_item;

        /// <summary>
        /// Defines the volume_item.
        /// </summary>
        public short volume_item;

        /// <summary>
        /// Defines the hp_skill.
        /// </summary>
        public short hp_skill;

        /// <summary>
        /// Defines the sp_skill.
        /// </summary>
        public short sp_skill;

        /// <summary>
        /// Defines the mp_skill.
        /// </summary>
        public short mp_skill;

        /// <summary>
        /// Defines the hp_mario.
        /// </summary>
        public short hp_mario;

        /// <summary>
        /// Defines the sp_mario.
        /// </summary>
        public short sp_mario;

        /// <summary>
        /// Defines the mp_mario.
        /// </summary>
        public short mp_mario;

        /// <summary>
        /// Defines the hp_recover_skill.
        /// </summary>
        public short hp_recover_skill;

        /// <summary>
        /// Defines the sp_recover_skill.
        /// </summary>
        public short sp_recover_skill;

        /// <summary>
        /// Defines the mp_recover_skill.
        /// </summary>
        public short mp_recover_skill;

        /// <summary>
        /// Defines the hp_recover_mario.
        /// </summary>
        public short hp_recover_mario;

        /// <summary>
        /// Defines the mp_recover_mario.
        /// </summary>
        public short mp_recover_mario;

        /// <summary>
        /// Defines the autoReviveRate.
        /// </summary>
        public short autoReviveRate;

        /// <summary>
        /// Defines the battleStatus.
        /// </summary>
        public BATTLE_STATUS battleStatus;

        /// <summary>
        /// Defines the attackType.
        /// </summary>
        public ATTACK_TYPE attackType;

        /// <summary>
        /// Defines the possessionTakeOver.
        /// </summary>
        public short possessionTakeOver;

        /// <summary>
        /// Defines the possessionCancel.
        /// </summary>
        public short possessionCancel;

        /// <summary>
        /// Defines the buy_rate.
        /// </summary>
        public short buy_rate;

        /// <summary>
        /// Defines the sell_rate.
        /// </summary>
        public short sell_rate;

        /// <summary>
        /// Defines the undead.
        /// </summary>
        public bool undead;

        /// <summary>
        /// Defines the str_skill.
        /// </summary>
        public short str_skill;

        /// <summary>
        /// Defines the dex_skill.
        /// </summary>
        public short dex_skill;

        /// <summary>
        /// Defines the int_skill.
        /// </summary>
        public short int_skill;

        /// <summary>
        /// Defines the vit_skill.
        /// </summary>
        public short vit_skill;

        /// <summary>
        /// Defines the agi_skill.
        /// </summary>
        public short agi_skill;

        /// <summary>
        /// Defines the mag_skill.
        /// </summary>
        public short mag_skill;

        /// <summary>
        /// Defines the min_atk1_possession.
        /// </summary>
        public short min_atk1_possession;

        /// <summary>
        /// Defines the min_atk2_possession.
        /// </summary>
        public short min_atk2_possession;

        /// <summary>
        /// Defines the min_atk3_possession.
        /// </summary>
        public short min_atk3_possession;

        /// <summary>
        /// Defines the max_atk1_possession.
        /// </summary>
        public short max_atk1_possession;

        /// <summary>
        /// Defines the max_atk2_possession.
        /// </summary>
        public short max_atk2_possession;

        /// <summary>
        /// Defines the max_atk3_possession.
        /// </summary>
        public short max_atk3_possession;

        /// <summary>
        /// Defines the min_matk_possession.
        /// </summary>
        public short min_matk_possession;

        /// <summary>
        /// Defines the max_matk_possession.
        /// </summary>
        public short max_matk_possession;

        /// <summary>
        /// Defines the hit_melee_possession.
        /// </summary>
        public short hit_melee_possession;

        /// <summary>
        /// Defines the hit_ranged_possession.
        /// </summary>
        public short hit_ranged_possession;

        /// <summary>
        /// Defines the avoid_melee_possession.
        /// </summary>
        public short avoid_melee_possession;

        /// <summary>
        /// Defines the avoid_ranged_possession.
        /// </summary>
        public short avoid_ranged_possession;

        /// <summary>
        /// Defines the hp_possession.
        /// </summary>
        public short hp_possession;

        /// <summary>
        /// Defines the sp_possession.
        /// </summary>
        public short sp_possession;

        /// <summary>
        /// Defines the mp_possession.
        /// </summary>
        public short mp_possession;

        /// <summary>
        /// Defines the def_possession.
        /// </summary>
        public short def_possession;

        /// <summary>
        /// Defines the def_add_possession.
        /// </summary>
        public short def_add_possession;

        /// <summary>
        /// Defines the mdef_possession.
        /// </summary>
        public short mdef_possession;

        /// <summary>
        /// Defines the mdef_add_possession.
        /// </summary>
        public short mdef_add_possession;

        /// <summary>
        /// Defines the combo_skill.
        /// </summary>
        public short combo_skill;

        /// <summary>
        /// Defines the combo_rate_skill.
        /// </summary>
        public short combo_rate_skill;

        /// <summary>
        /// Defines the cri_dmg_skill.
        /// </summary>
        public short cri_dmg_skill;

        /// <summary>
        /// Defines the speed_up.
        /// </summary>
        public int speed_up;

        /// <summary>
        /// Defines the damage_atk1_discount.
        /// </summary>
        public float damage_atk1_discount;

        /// <summary>
        /// Defines the damage_atk2_discount.
        /// </summary>
        public float damage_atk2_discount;

        /// <summary>
        /// Defines the damage_atk3_discount.
        /// </summary>
        public float damage_atk3_discount;

        /// <summary>
        /// Gets or sets the str_chip.
        /// </summary>
        public short str_chip
        {
            get
            {
                if (this.owner.type != ActorType.PC)
                    return 0;
                ActorPC owner = (ActorPC)this.owner;
                if (owner.Race == PC_RACE.DEM && owner.Form != DEM_FORM.NORMAL_FORM)
                    return this.m_str_chip;
                return 0;
            }
            set
            {
                this.m_str_chip = value;
            }
        }

        /// <summary>
        /// Gets or sets the dex_chip.
        /// </summary>
        public short dex_chip
        {
            get
            {
                if (this.owner.type != ActorType.PC)
                    return 0;
                ActorPC owner = (ActorPC)this.owner;
                if (owner.Race == PC_RACE.DEM && owner.Form != DEM_FORM.NORMAL_FORM)
                    return this.m_dex_chip;
                return 0;
            }
            set
            {
                this.m_dex_chip = value;
            }
        }

        /// <summary>
        /// Gets or sets the int_chip.
        /// </summary>
        public short int_chip
        {
            get
            {
                if (this.owner.type != ActorType.PC)
                    return 0;
                ActorPC owner = (ActorPC)this.owner;
                if (owner.Race == PC_RACE.DEM && owner.Form != DEM_FORM.NORMAL_FORM)
                    return this.m_int_chip;
                return 0;
            }
            set
            {
                this.m_int_chip = value;
            }
        }

        /// <summary>
        /// Gets or sets the vit_chip.
        /// </summary>
        public short vit_chip
        {
            get
            {
                if (this.owner.type != ActorType.PC)
                    return 0;
                ActorPC owner = (ActorPC)this.owner;
                if (owner.Race == PC_RACE.DEM && owner.Form != DEM_FORM.NORMAL_FORM)
                    return this.m_vit_chip;
                return 0;
            }
            set
            {
                this.m_vit_chip = value;
            }
        }

        /// <summary>
        /// Gets or sets the agi_chip.
        /// </summary>
        public short agi_chip
        {
            get
            {
                if (this.owner.type != ActorType.PC)
                    return 0;
                ActorPC owner = (ActorPC)this.owner;
                if (owner.Race == PC_RACE.DEM && owner.Form != DEM_FORM.NORMAL_FORM)
                    return this.m_agi_chip;
                return 0;
            }
            set
            {
                this.m_agi_chip = value;
            }
        }

        /// <summary>
        /// Gets or sets the mag_chip.
        /// </summary>
        public short mag_chip
        {
            get
            {
                if (this.owner.type != ActorType.PC)
                    return 0;
                ActorPC owner = (ActorPC)this.owner;
                if (owner.Race == PC_RACE.DEM && owner.Form != DEM_FORM.NORMAL_FORM)
                    return this.m_mag_chip;
                return 0;
            }
            set
            {
                this.m_mag_chip = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="owner">The owner<see cref="SagaDB.Actor.Actor"/>.</param>
        public Status(SagaDB.Actor.Actor owner)
        {
            this.owner = owner;
            this.elements_item.Add(Elements.Neutral, 0);
            this.elements_item.Add(Elements.Fire, 0);
            this.elements_item.Add(Elements.Water, 0);
            this.elements_item.Add(Elements.Wind, 0);
            this.elements_item.Add(Elements.Earth, 0);
            this.elements_item.Add(Elements.Holy, 0);
            this.elements_item.Add(Elements.Dark, 0);
            this.attackElements_item.Add(Elements.Neutral, 0);
            this.attackElements_item.Add(Elements.Fire, 0);
            this.attackElements_item.Add(Elements.Water, 0);
            this.attackElements_item.Add(Elements.Wind, 0);
            this.attackElements_item.Add(Elements.Earth, 0);
            this.attackElements_item.Add(Elements.Holy, 0);
            this.attackElements_item.Add(Elements.Dark, 0);
        }

        /// <summary>
        /// The ClearItem.
        /// </summary>
        public void ClearItem()
        {
            this.atk1_item = (short)0;
            this.atk2_item = (short)0;
            this.atk3_item = (short)0;
            this.matk_item = (short)0;
            this.def_add = (short)0;
            this.mdef_add = (short)0;
            this.hit_melee_item = (short)0;
            this.hit_ranged_item = (short)0;
            this.avoid_melee_item = (short)0;
            this.avoid_ranged_item = (short)0;
            this.str_item = (short)0;
            this.dex_item = (short)0;
            this.vit_item = (short)0;
            this.int_item = (short)0;
            this.agi_item = (short)0;
            this.mag_item = (short)0;
            this.str_chip = (short)0;
            this.m_dex_chip = (short)0;
            this.m_vit_chip = (short)0;
            this.m_int_chip = (short)0;
            this.m_agi_chip = (short)0;
            this.m_mag_chip = (short)0;
            this.hp_item = (short)0;
            this.sp_item = (short)0;
            this.mp_item = (short)0;
            this.guard_item = (short)0;
            this.payl_item = (short)0;
            this.volume_item = (short)0;
            this.speed_up = 0;
            this.hp_rate_item = (short)100;
            this.sp_rate_item = (short)100;
            this.mp_rate_item = (short)100;
            this.elements_item[Elements.Neutral] = 0;
            this.elements_item[Elements.Fire] = 0;
            this.elements_item[Elements.Water] = 0;
            this.elements_item[Elements.Wind] = 0;
            this.elements_item[Elements.Earth] = 0;
            this.elements_item[Elements.Holy] = 0;
            this.elements_item[Elements.Dark] = 0;
            this.attackElements_item[Elements.Neutral] = 0;
            this.attackElements_item[Elements.Fire] = 0;
            this.attackElements_item[Elements.Water] = 0;
            this.attackElements_item[Elements.Wind] = 0;
            this.attackElements_item[Elements.Earth] = 0;
            this.attackElements_item[Elements.Holy] = 0;
            this.attackElements_item[Elements.Dark] = 0;
        }
    }
}
