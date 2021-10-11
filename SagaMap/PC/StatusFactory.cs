namespace SagaMap.PC
{
    using SagaDB.Actor;
    using SagaDB.DEMIC;
    using SagaDB.Iris;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Skill;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="StatusFactory" />.
    /// </summary>
    public class StatusFactory : Singleton<StatusFactory>
    {
        /// <summary>
        /// The CalcStatus.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void CalcStatus(ActorPC pc)
        {
            this.CalcEquipBonus(pc);
            this.CalcMarionetteBonus(pc);
            this.CalcRange(pc);
            this.CalcStatsRev(pc);
            this.CalcPayV(pc);
            this.CalcHPMPSP(pc);
            this.CalcStats(pc);
            pc.Inventory.CalcPayloadVolume();
        }

        /// <summary>
        /// The CalcRange.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void CalcRange(ActorPC pc)
        {
            Dictionary<EnumEquipSlot, SagaDB.Item.Item> dictionary = pc.Form != DEM_FORM.NORMAL_FORM ? pc.Inventory.Parts : pc.Inventory.Equipments;
            if (dictionary.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                SagaDB.Item.Item obj = dictionary[EnumEquipSlot.RIGHT_HAND];
                pc.Range = (uint)obj.BaseData.range;
            }
            else if (dictionary.ContainsKey(EnumEquipSlot.LEFT_HAND))
            {
                SagaDB.Item.Item obj = dictionary[EnumEquipSlot.LEFT_HAND];
                pc.Range = (uint)obj.BaseData.range;
            }
            else
                pc.Range = 1U;
        }

        /// <summary>
        /// The checkPositive.
        /// </summary>
        /// <param name="num">The num<see cref="double"/>.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        private ushort checkPositive(double num)
        {
            if (num > 0.0)
                return (ushort)num;
            return 0;
        }

        /// <summary>
        /// The CalcStats.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void CalcStats(ActorPC pc)
        {
            if (pc.Pet != null && pc.Pet.Ride)
            {
                pc.Status.min_atk1 = pc.Pet.Status.min_atk1;
                pc.Status.min_atk2 = pc.Pet.Status.min_atk2;
                pc.Status.min_atk3 = pc.Pet.Status.min_atk3;
                pc.Status.max_atk1 = pc.Pet.Status.max_atk1;
                pc.Status.max_atk2 = pc.Pet.Status.max_atk2;
                pc.Status.max_atk3 = pc.Pet.Status.max_atk3;
                pc.Status.min_matk = pc.Pet.Status.min_matk;
                pc.Status.max_matk = pc.Pet.Status.min_matk;
                pc.Status.def = pc.Pet.Status.def;
                pc.Status.def_add = pc.Pet.Status.def_add;
                pc.Status.mdef = pc.Pet.Status.mdef;
                pc.Status.mdef_add = pc.Pet.Status.mdef_add;
                pc.Status.aspd = pc.Pet.Status.aspd;
                pc.Status.cspd = pc.Pet.Status.cspd;
                pc.Status.hit_melee = pc.Pet.Status.hit_melee;
                pc.Status.hit_ranged = pc.Pet.Status.hit_ranged;
                pc.Status.avoid_melee = pc.Pet.Status.avoid_melee;
                pc.Status.avoid_ranged = pc.Pet.Status.avoid_ranged;
                pc.Speed = pc.Pet.Speed;
            }
            else
            {
                ushort num1 = this.checkPositive((double)((int)pc.Str + (int)pc.Status.str_item + (int)pc.Status.str_chip + (int)pc.Status.str_rev + (int)pc.Status.str_mario + (int)pc.Status.str_skill + ((int)pc.Str + (int)pc.Status.str_item + (int)pc.Status.str_chip + (int)pc.Status.str_rev + (int)pc.Status.str_mario + (int)pc.Status.str_skill) / 9 * (((int)pc.Str + (int)pc.Status.str_item + (int)pc.Status.str_chip + (int)pc.Status.str_rev + (int)pc.Status.str_mario + (int)pc.Status.str_skill) / 9)) * (1.0 + (double)this.CalcATKRate(pc)));
                pc.Status.min_atk_ori = this.checkPositive((double)((int)pc.Str + (int)pc.Status.str_rev + ((int)pc.Str + (int)pc.Status.str_rev) / 9 * (((int)pc.Str + (int)pc.Status.str_rev) / 9)) * (1.0 + (double)this.CalcATKRate(pc)));
                pc.Status.min_atk1 = this.checkPositive((double)((int)num1 + (int)pc.Status.atk1_item + (int)pc.Status.min_atk1_mario + (int)pc.Status.min_atk1_skill));
                pc.Status.min_atk2 = this.checkPositive((double)((int)num1 + (int)pc.Status.atk2_item + (int)pc.Status.min_atk2_mario + (int)pc.Status.min_atk2_skill));
                pc.Status.min_atk3 = this.checkPositive((double)((int)num1 + (int)pc.Status.atk3_item + (int)pc.Status.min_atk3_mario + (int)pc.Status.min_atk3_skill));
                ushort num2 = this.checkPositive((double)((int)pc.Str + (int)pc.Status.str_item + (int)pc.Status.str_chip + (int)pc.Status.str_rev + (int)pc.Status.str_mario + (int)pc.Status.str_skill + ((int)pc.Str + (int)pc.Status.str_item + (int)pc.Status.str_chip + (int)pc.Status.str_rev + (int)pc.Status.str_mario + (int)pc.Status.str_skill + 14) / 5 * (((int)pc.Str + (int)pc.Status.str_item + (int)pc.Status.str_chip + (int)pc.Status.str_rev + (int)pc.Status.str_mario + (int)pc.Status.str_skill + 14) / 5)));
                pc.Status.max_atk_ori = this.checkPositive((double)((int)pc.Str + (int)pc.Status.str_rev + ((int)pc.Str + (int)pc.Status.str_rev + 14) / 5 * (((int)pc.Str + (int)pc.Status.str_rev + 14) / 5)));
                pc.Status.max_atk1 = this.checkPositive((double)((int)num2 + (int)pc.Status.atk1_item + (int)pc.Status.max_atk1_mario + (int)pc.Status.max_atk1_skill));
                pc.Status.max_atk2 = this.checkPositive((double)((int)num2 + (int)pc.Status.atk2_item + (int)pc.Status.max_atk2_mario + (int)pc.Status.max_atk2_skill));
                pc.Status.max_atk3 = this.checkPositive((double)((int)num2 + (int)pc.Status.atk3_item + (int)pc.Status.max_atk3_mario + (int)pc.Status.max_atk3_skill));
                ushort num3 = this.checkPositive((double)((int)pc.Mag + (int)pc.Status.mag_item + (int)pc.Status.mag_chip + (int)pc.Status.mag_rev + (int)pc.Status.mag_mario + (int)pc.Status.mag_skill + ((int)pc.Mag + (int)pc.Status.mag_item + (int)pc.Status.mag_chip + (int)pc.Status.mag_rev + (int)pc.Status.mag_mario + (int)pc.Status.mag_skill + 9) / 8 * (((int)pc.Mag + (int)pc.Status.mag_item + (int)pc.Status.mag_chip + (int)pc.Status.mag_rev + (int)pc.Status.mag_mario + (int)pc.Status.mag_skill + 9) / 8)) * (1.0 + (double)((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_chip + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill) * 1.20000004768372 / 320.0));
                pc.Status.min_matk_ori = num3;
                pc.Status.min_matk = this.checkPositive((double)((int)num3 + (int)pc.Status.matk_item + (int)pc.Status.min_matk_mario + (int)pc.Status.min_matk_skill));
                ushort num4 = this.checkPositive((double)((int)pc.Mag + (int)pc.Status.mag_item + (int)pc.Status.mag_chip + (int)pc.Status.mag_rev + (int)pc.Status.mag_mario + (int)pc.Status.mag_skill + ((int)pc.Mag + (int)pc.Status.mag_item + (int)pc.Status.mag_chip + (int)pc.Status.mag_rev + (int)pc.Status.mag_mario + (int)pc.Status.mag_skill + 17) / 6 * (((int)pc.Mag + (int)pc.Status.mag_item + (int)pc.Status.mag_rev + (int)pc.Status.mag_mario + (int)pc.Status.mag_skill + 17) / 6)));
                pc.Status.max_matk_ori = num4;
                pc.Status.max_matk = this.checkPositive((double)((int)num4 + (int)pc.Status.matk_item + (int)pc.Status.max_matk_mario + (int)pc.Status.max_matk_skill));
                ushort num5 = this.checkPositive((double)((int)pc.Dex + (int)pc.Status.dex_item + (int)pc.Status.dex_chip + (int)pc.Status.dex_rev + (int)pc.Status.dex_mario + (int)pc.Status.dex_skill + ((int)pc.Dex + (int)pc.Status.dex_item + (int)pc.Status.dex_chip + (int)pc.Status.dex_rev + (int)pc.Status.dex_mario + (int)pc.Status.dex_skill) / 10 * 11 + (int)pc.Level + 3));
                pc.Status.hit_melee = this.checkPositive((double)((int)num5 + (int)pc.Status.hit_melee_item + (int)pc.Status.hit_melee_skill));
                ushort num6 = this.checkPositive((double)((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_chip + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill + ((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_chip + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill) / 10 * 11 + (int)pc.Level + 3));
                pc.Status.hit_ranged = this.checkPositive((double)((int)num6 + (int)pc.Status.hit_ranged_item + (int)pc.Status.hit_ranged_skill));
                pc.Status.def = this.checkPositive((double)(((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) / 3 + ((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) / 9 * 2));
                pc.Status.def = this.checkPositive((double)((int)pc.Status.def + ((int)pc.Status.def_mario + (int)pc.Status.def_skill)));
                pc.Status.def_add = (short)this.checkPositive((double)((int)pc.Status.def_add + ((int)pc.Status.def_add_mario + (int)pc.Status.def_add_skill)));
                pc.Status.mdef = this.checkPositive((double)(((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_chip + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill) / 3 + ((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) / 4));
                pc.Status.mdef = this.checkPositive((double)((int)pc.Status.mdef + ((int)pc.Status.mdef_mario + (int)pc.Status.mdef_skill)));
                pc.Status.mdef_add = (short)this.checkPositive((double)((int)pc.Status.mdef_add + ((int)pc.Status.mdef_add_mario + (int)pc.Status.mdef_add_skill)));
                ushort num7 = this.checkPositive((double)((int)pc.Agi + (int)pc.Status.agi_item + (int)pc.Status.agi_chip + (int)pc.Status.agi_rev + (int)pc.Status.agi_mario + (int)pc.Status.agi_skill + ((int)pc.Agi + (int)pc.Status.agi_item + (int)pc.Status.agi_chip + (int)pc.Status.agi_rev + (int)pc.Status.agi_mario + (int)pc.Status.agi_skill + 18) / 9 * (((int)pc.Agi + (int)pc.Status.agi_item + (int)pc.Status.agi_chip + (int)pc.Status.agi_rev + (int)pc.Status.agi_mario + (int)pc.Status.agi_skill + 18) / 9) + (int)pc.Level / 3 - 1));
                pc.Status.avoid_melee = this.checkPositive((double)((int)num7 + (int)pc.Status.avoid_melee_item + (int)pc.Status.avoid_melee_skill));
                ushort num8 = this.checkPositive((double)(((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_chip + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill) * 5 / 3 + ((int)pc.Agi + (int)pc.Status.agi_item + (int)pc.Status.int_chip + (int)pc.Status.agi_rev + (int)pc.Status.agi_mario + (int)pc.Status.agi_skill) + (int)pc.Level / 3 + 3));
                pc.Status.avoid_ranged = this.checkPositive((double)((int)num8 + (int)pc.Status.avoid_ranged_item + (int)pc.Status.avoid_ranged_skill));
                pc.Status.aspd = (short)(((int)pc.Agi + (int)pc.Status.agi_item + (int)pc.Status.agi_chip + (int)pc.Status.agi_rev + (int)pc.Status.agi_mario + (int)pc.Status.agi_skill) * 3 + ((int)pc.Agi + (int)pc.Status.agi_item + (int)pc.Status.agi_chip + (int)pc.Status.agi_rev + (int)pc.Status.agi_mario + (int)pc.Status.agi_skill + 63) / 9 * (((int)pc.Agi + (int)pc.Status.agi_item + (int)pc.Status.agi_chip + (int)pc.Status.agi_rev + (int)pc.Status.agi_mario + (int)pc.Status.agi_skill + 63) / 9) + 129 + (int)pc.Status.aspd_skill_limit);
                pc.Status.cspd = (short)(((int)pc.Dex + (int)pc.Status.dex_item + (int)pc.Status.dex_chip + (int)pc.Status.dex_rev + (int)pc.Status.dex_mario + (int)pc.Status.dex_skill) * 3 + ((int)pc.Dex + (int)pc.Status.dex_item + (int)pc.Status.dex_chip + (int)pc.Status.dex_rev + (int)pc.Status.dex_mario + (int)pc.Status.dex_skill + 63) / 9 * (((int)pc.Dex + (int)pc.Status.dex_item + (int)pc.Status.dex_chip + (int)pc.Status.dex_rev + (int)pc.Status.dex_mario + (int)pc.Status.dex_skill + 63) / 9) + 129 + (int)pc.Status.cspd_skill_limit);
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    SagaDB.Item.Item equipment = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                    if (equipment.BaseData.itemType == ItemType.DUALGUN || equipment.BaseData.itemType == ItemType.CLAW)
                        pc.Status.aspd = (short)((double)pc.Status.aspd * 0.670000016689301);
                }
                if (pc.Status.avoid_melee > (ushort)400)
                    pc.Status.avoid_melee = (ushort)400;
                if (pc.Status.avoid_ranged > (ushort)400)
                    pc.Status.avoid_ranged = (ushort)400;
                if (pc.Status.aspd > (short)800)
                    pc.Status.aspd = (short)800;
                if (pc.Status.cspd > (short)800)
                    pc.Status.cspd = (short)800;
                foreach (ActorPC possesionedActor in pc.PossesionedActors)
                {
                    if (possesionedActor != pc)
                    {
                        pc.Status.min_atk1 = this.checkPositive((double)((int)pc.Status.min_atk1 + (int)possesionedActor.Status.min_atk1_possession));
                        pc.Status.min_atk2 = this.checkPositive((double)((int)pc.Status.min_atk2 + (int)possesionedActor.Status.min_atk2_possession));
                        pc.Status.min_atk3 = this.checkPositive((double)((int)pc.Status.min_atk3 + (int)possesionedActor.Status.min_atk3_possession));
                        pc.Status.max_atk1 = this.checkPositive((double)((int)pc.Status.max_atk1 + (int)possesionedActor.Status.max_atk1_possession));
                        pc.Status.max_atk2 = this.checkPositive((double)((int)pc.Status.max_atk2 + (int)possesionedActor.Status.max_atk2_possession));
                        pc.Status.max_atk3 = this.checkPositive((double)((int)pc.Status.max_atk3 + (int)possesionedActor.Status.max_atk3_possession));
                        pc.Status.min_matk = this.checkPositive((double)((int)pc.Status.min_matk + (int)possesionedActor.Status.min_matk_possession));
                        pc.Status.max_matk = this.checkPositive((double)((int)pc.Status.max_matk + (int)possesionedActor.Status.max_matk_possession));
                        pc.Status.hit_melee = this.checkPositive((double)((int)pc.Status.hit_melee + (int)possesionedActor.Status.hit_melee_possession));
                        pc.Status.hit_ranged = this.checkPositive((double)((int)pc.Status.hit_ranged + (int)possesionedActor.Status.hit_ranged_possession));
                        pc.Status.avoid_melee = this.checkPositive((double)((int)pc.Status.avoid_melee + (int)possesionedActor.Status.avoid_melee_possession));
                        pc.Status.avoid_ranged = this.checkPositive((double)((int)pc.Status.avoid_ranged + (int)possesionedActor.Status.avoid_ranged_possession));
                        pc.Status.def = this.checkPositive((double)((int)pc.Status.def + (int)possesionedActor.Status.def_possession));
                        pc.Status.def_add = (short)this.checkPositive((double)((int)pc.Status.def_add + (int)possesionedActor.Status.def_add_possession));
                        pc.Status.mdef = this.checkPositive((double)((int)pc.Status.mdef + (int)possesionedActor.Status.mdef_possession));
                        pc.Status.mdef_add = (short)this.checkPositive((double)((int)pc.Status.mdef_add + (int)possesionedActor.Status.mdef_add_possession));
                    }
                }
            }
        }

        /// <summary>
        /// The RequiredBonusPoint.
        /// </summary>
        /// <param name="current">The current<see cref="ushort"/>.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        public ushort RequiredBonusPoint(ushort current)
        {
            return (ushort)((int)current / 6 + 1);
        }

        /// <summary>
        /// The GetTotalBonusPointForStats.
        /// </summary>
        /// <param name="start">The start<see cref="ushort"/>.</param>
        /// <param name="stat">The stat<see cref="ushort"/>.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        public ushort GetTotalBonusPointForStats(ushort start, ushort stat)
        {
            int num = 0;
            for (ushort current = start; (int)current < (int)stat; ++current)
                num += (int)this.RequiredBonusPoint(current);
            return (ushort)num;
        }

        /// <summary>
        /// The CalcATKRate.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float CalcATKRate(ActorPC pc)
        {
            bool flag = false;
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                SagaDB.Item.Item equipment = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                if (equipment.BaseData.itemType == ItemType.BOW || equipment.BaseData.itemType == ItemType.GUN || (equipment.BaseData.itemType == ItemType.DUALGUN || equipment.BaseData.itemType == ItemType.RIFLE) || equipment.BaseData.itemType == ItemType.THROW)
                    flag = true;
            }
            if (!flag)
                return (float)((double)((int)pc.Dex + (int)pc.Status.dex_item + (int)pc.Status.dex_rev + (int)pc.Status.dex_mario + (int)pc.Status.dex_skill) * 1.5 / 160.0);
            return (float)((double)((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill) * 1.5 / 160.0);
        }

        /// <summary>
        /// The CalcPayV.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void CalcPayV(ActorPC pc)
        {
            this.CalcPayl(pc);
            this.CalcVolume(pc);
        }

        /// <summary>
        /// The CalcVolume.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void CalcVolume(ActorPC pc)
        {
            pc.Inventory.MaxVolume[ContainerType.BODY] = (uint)((double)(((int)pc.Dex + (int)pc.Status.dex_item + (int)pc.Status.dex_chip + (int)pc.Status.dex_rev + (int)pc.Status.dex_mario + (int)pc.Status.dex_skill) / 5 + ((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_chip + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill) / 10 + 200) * (double)this.VolumeJobFactor(pc) * (double)Singleton<Configuration>.Instance.VolumeRate * 10.0);
            if (!pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            pc.Inventory.MaxVolume[ContainerType.BODY] = (uint)((ulong)pc.Inventory.MaxVolume[ContainerType.BODY] + (ulong)pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.volumeUp);
        }

        /// <summary>
        /// The CalcPayl.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void CalcPayl(ActorPC pc)
        {
            pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)((double)(((int)pc.Str + (int)pc.Status.str_item + (int)pc.Status.str_chip + (int)pc.Status.str_rev + (int)pc.Status.str_mario + (int)pc.Status.str_skill) * 2 / 3 + ((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) / 3 + 400) * (double)this.PayLoadRaceFactor(pc.Race) * (double)Singleton<Configuration>.Instance.PayloadRate * (double)this.PayLoadJobFactor(pc) * 10.0);
            if (pc.Status.Additions.ContainsKey("GoRiKi"))
            {
                DefaultPassiveSkill addition = (DefaultPassiveSkill)pc.Status.Additions["GoRiKi"];
                pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)((double)pc.Inventory.MaxPayload[ContainerType.BODY] * (1.0 + (double)addition["GoRiKi"] / 100.0));
            }
            if (!pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)((ulong)pc.Inventory.MaxPayload[ContainerType.BODY] + (ulong)pc.Inventory.Equipments[EnumEquipSlot.PET].BaseData.weightUp);
        }

        /// <summary>
        /// The PayLoadRaceFactor.
        /// </summary>
        /// <param name="race">The race<see cref="PC_RACE"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float PayLoadRaceFactor(PC_RACE race)
        {
            switch (race)
            {
                case PC_RACE.EMIL:
                    return 1.3f;
                case PC_RACE.TITANIA:
                    return 0.9f;
                case PC_RACE.DOMINION:
                    return 1.1f;
                default:
                    return 1f;
            }
        }

        /// <summary>
        /// The PayLoadJobFactor.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float PayLoadJobFactor(ActorPC pc)
        {
            switch (pc.JobJoint != PC_JOB.NONE ? pc.JobJoint : pc.Job)
            {
                case PC_JOB.NOVICE:
                    return 0.7f;
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
                    return 1f;
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
                    return 0.8f;
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
                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                    return 1.3f;
                default:
                    return 1f;
            }
        }

        /// <summary>
        /// The VolumeJobFactor.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float VolumeJobFactor(ActorPC pc)
        {
            switch (pc.JobJoint != PC_JOB.NONE ? pc.JobJoint : pc.Job)
            {
                case PC_JOB.NOVICE:
                    return 0.85f;
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
                    return 1f;
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
                    return 1f;
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
                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                    return 1.13f;
                default:
                    return 1f;
            }
        }

        /// <summary>
        /// The CalcHPMPSP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void CalcHPMPSP(ActorPC pc)
        {
            pc.MaxHP = this.CalcMaxHP(pc);
            pc.MaxMP = this.CalcMaxMP(pc);
            pc.MaxSP = this.CalcMaxSP(pc);
            pc.MaxEP = this.CalcMaxEP(pc);
            if (pc.HP > pc.MaxHP)
                pc.HP = pc.MaxHP;
            if (pc.MP > pc.MaxMP)
                pc.MP = pc.MaxMP;
            if (pc.SP > pc.MaxSP)
                pc.SP = pc.MaxSP;
            if (pc.EP <= pc.MaxEP)
                return;
            pc.EP = pc.MaxEP;
        }

        /// <summary>
        /// The CalcMaxEP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint CalcMaxEP(ActorPC pc)
        {
            if (pc.Ring == null)
                return 30;
            return (uint)(30 + pc.Ring.MemberCount * 2);
        }

        /// <summary>
        /// The CalcMaxHP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint CalcMaxHP(ActorPC pc)
        {
            short num1 = 0;
            byte num2 = !Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion) ? pc.Level : pc.DominionLevel;
            if (pc.Pet != null && pc.Pet.Ride && pc.Pet.MaxHP != 0U)
                return pc.Pet.MaxHP;
            foreach (ActorPC possesionedActor in pc.PossesionedActors)
            {
                if (possesionedActor != pc && possesionedActor.Status != null)
                    num1 += possesionedActor.Status.hp_possession;
            }
            return (uint)((double)(((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) * 3 + ((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) / 5 * (((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) / 5) + (int)num2 * 2 + (int)num2 / 5 * ((int)num2 / 5) + 50) * (double)this.HPJobFactor(pc) * (double)((int)pc.Status.hp_rate_item / 100) + (double)pc.Status.hp_item + (double)pc.Status.hp_skill + (double)pc.Status.hp_mario + (double)num1);
        }

        /// <summary>
        /// The CalcMaxMP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint CalcMaxMP(ActorPC pc)
        {
            short num1 = 0;
            byte num2 = !Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion) ? pc.Level : pc.DominionLevel;
            if (pc.Pet != null && pc.Pet.Ride && pc.Pet.MaxMP != 0U)
                return pc.Pet.MaxMP;
            foreach (ActorPC possesionedActor in pc.PossesionedActors)
            {
                if (possesionedActor != pc && possesionedActor.Status != null)
                    num1 += possesionedActor.Status.mp_possession;
            }
            return (uint)((double)(((int)pc.Mag + (int)pc.Status.mag_item + (int)pc.Status.mag_chip + (int)pc.Status.mag_rev + (int)pc.Status.mag_mario + (int)pc.Status.mag_skill) * 3 + (int)num2 + (int)num2 / 9 * ((int)num2 / 9) + 30) * (double)this.MPJobFactor(pc) * (double)((int)pc.Status.mp_rate_item / 100) + (double)pc.Status.mp_item + (double)pc.Status.mp_skill + (double)pc.Status.mp_mario + (double)num1);
        }

        /// <summary>
        /// The CalcMaxSP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint CalcMaxSP(ActorPC pc)
        {
            short num1 = 0;
            byte num2 = !Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion) ? pc.Level : pc.DominionLevel;
            if (pc.Pet != null && pc.Pet.Ride && pc.Pet.MaxSP != 0U)
                return pc.Pet.MaxSP;
            foreach (ActorPC possesionedActor in pc.PossesionedActors)
            {
                if (possesionedActor != pc)
                    num1 += possesionedActor.Status.sp_possession;
            }
            return (uint)((double)((int)pc.Int + (int)pc.Status.int_item + (int)pc.Status.int_chip + (int)pc.Status.int_rev + (int)pc.Status.int_mario + (int)pc.Status.int_skill + ((int)pc.Vit + (int)pc.Status.vit_item + (int)pc.Status.vit_chip + (int)pc.Status.vit_rev + (int)pc.Status.vit_mario + (int)pc.Status.vit_skill) + (int)num2 + (int)num2 / 9 * ((int)num2 / 9) + 20) * (double)this.SPJobFactor(pc) * (double)((int)pc.Status.sp_rate_item / 100) + (double)pc.Status.sp_item + (double)pc.Status.sp_skill + (double)pc.Status.sp_mario + (double)num1);
        }

        /// <summary>
        /// The HPJobFactor.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float HPJobFactor(ActorPC pc)
        {
            switch (pc.JobJoint != PC_JOB.NONE ? pc.JobJoint : pc.Job)
            {
                case PC_JOB.NOVICE:
                    return 1f;
                case PC_JOB.SWORDMAN:
                    return 1.8f;
                case PC_JOB.BLADEMASTER:
                    return 3.05f;
                case PC_JOB.BOUNTYHUNTER:
                    return 2.9f;
                case PC_JOB.FENCER:
                    return 1.65f;
                case PC_JOB.KNIGHT:
                    return 3.3f;
                case PC_JOB.DARKSTALKER:
                    return 3f;
                case PC_JOB.SCOUT:
                    return 1.45f;
                case PC_JOB.ASSASSIN:
                    return 2.45f;
                case PC_JOB.COMMAND:
                    return 2.5f;
                case PC_JOB.ARCHER:
                    return 1.35f;
                case PC_JOB.STRIKER:
                    return 2.25f;
                case PC_JOB.GUNNER:
                    return 2.15f;
                case PC_JOB.WIZARD:
                    return 1.1f;
                case PC_JOB.SORCERER:
                    return 1.85f;
                case PC_JOB.SAGE:
                    return 1.95f;
                case PC_JOB.SHAMAN:
                    return 1.05f;
                case PC_JOB.ELEMENTER:
                    return 1.8f;
                case PC_JOB.ENCHANTER:
                    return 1.85f;
                case PC_JOB.VATES:
                    return 1.15f;
                case PC_JOB.DRUID:
                    return 1.95f;
                case PC_JOB.BARD:
                    return 2.15f;
                case PC_JOB.WARLOCK:
                    return 1.3f;
                case PC_JOB.CABALIST:
                    return 2.6f;
                case PC_JOB.NECROMANCER:
                    return 2.3f;
                case PC_JOB.TATARABE:
                    return 1.5f;
                case PC_JOB.BLACKSMITH:
                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                    return 3f;
                case PC_JOB.MACHINERY:
                    return 2.6f;
                case PC_JOB.FARMASIST:
                    return 1.4f;
                case PC_JOB.ALCHEMIST:
                    return 2.5f;
                case PC_JOB.MARIONEST:
                    return 2.15f;
                case PC_JOB.RANGER:
                    return 1.25f;
                case PC_JOB.EXPLORER:
                    return 2.8f;
                case PC_JOB.TREASUREHUNTER:
                    return 2.3f;
                case PC_JOB.MERCHANT:
                    return 1.2f;
                case PC_JOB.TRADER:
                    return 2.4f;
                case PC_JOB.GAMBLER:
                    return 2.4f;
                default:
                    return 1f;
            }
        }

        /// <summary>
        /// The MPJobFactor.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float MPJobFactor(ActorPC pc)
        {
            switch (pc.JobJoint != PC_JOB.NONE ? pc.JobJoint : pc.Job)
            {
                case PC_JOB.NOVICE:
                    return 1f;
                case PC_JOB.SWORDMAN:
                    return 1.05f;
                case PC_JOB.BLADEMASTER:
                    return 1.25f;
                case PC_JOB.BOUNTYHUNTER:
                    return 1.25f;
                case PC_JOB.FENCER:
                    return 1.05f;
                case PC_JOB.KNIGHT:
                    return 1.3f;
                case PC_JOB.DARKSTALKER:
                    return 1.25f;
                case PC_JOB.SCOUT:
                    return 1.05f;
                case PC_JOB.ASSASSIN:
                    return 1.3f;
                case PC_JOB.COMMAND:
                    return 1.25f;
                case PC_JOB.ARCHER:
                    return 1.1f;
                case PC_JOB.STRIKER:
                    return 1.4f;
                case PC_JOB.GUNNER:
                    return 1.25f;
                case PC_JOB.WIZARD:
                    return 1.2f;
                case PC_JOB.SORCERER:
                    return 2.35f;
                case PC_JOB.SAGE:
                    return 2.3f;
                case PC_JOB.SHAMAN:
                    return 1.15f;
                case PC_JOB.ELEMENTER:
                    return 2.4f;
                case PC_JOB.ENCHANTER:
                    return 2.35f;
                case PC_JOB.VATES:
                    return 1.15f;
                case PC_JOB.DRUID:
                    return 2.2f;
                case PC_JOB.BARD:
                    return 2.1f;
                case PC_JOB.WARLOCK:
                    return 1.15f;
                case PC_JOB.CABALIST:
                    return 2f;
                case PC_JOB.NECROMANCER:
                    return 2.3f;
                case PC_JOB.TATARABE:
                    return 1.05f;
                case PC_JOB.BLACKSMITH:
                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                    return 1.2f;
                case PC_JOB.MACHINERY:
                    return 1.5f;
                case PC_JOB.FARMASIST:
                    return 1.1f;
                case PC_JOB.ALCHEMIST:
                    return 1.5f;
                case PC_JOB.MARIONEST:
                    return 2.1f;
                case PC_JOB.RANGER:
                    return 1.05f;
                case PC_JOB.EXPLORER:
                    return 1.3f;
                case PC_JOB.TREASUREHUNTER:
                    return 1.5f;
                case PC_JOB.MERCHANT:
                    return 1.1f;
                case PC_JOB.TRADER:
                    return 1.3f;
                case PC_JOB.GAMBLER:
                    return 1.9f;
                default:
                    return 1f;
            }
        }

        /// <summary>
        /// The SPJobFactor.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float SPJobFactor(ActorPC pc)
        {
            switch (pc.JobJoint != PC_JOB.NONE ? pc.JobJoint : pc.Job)
            {
                case PC_JOB.NOVICE:
                    return 1f;
                case PC_JOB.SWORDMAN:
                    return 1.1f;
                case PC_JOB.BLADEMASTER:
                    return 1.75f;
                case PC_JOB.BOUNTYHUNTER:
                    return 1.8f;
                case PC_JOB.FENCER:
                    return 1.15f;
                case PC_JOB.KNIGHT:
                    return 1.5f;
                case PC_JOB.DARKSTALKER:
                    return 1.7f;
                case PC_JOB.SCOUT:
                    return 1.2f;
                case PC_JOB.ASSASSIN:
                    return 1.7f;
                case PC_JOB.COMMAND:
                    return 1.8f;
                case PC_JOB.ARCHER:
                    return 1.15f;
                case PC_JOB.STRIKER:
                    return 1.6f;
                case PC_JOB.GUNNER:
                    return 2.3f;
                case PC_JOB.WIZARD:
                    return 1.05f;
                case PC_JOB.SORCERER:
                    return 1.25f;
                case PC_JOB.SAGE:
                    return 1.25f;
                case PC_JOB.SHAMAN:
                    return 1.1f;
                case PC_JOB.ELEMENTER:
                    return 1.2f;
                case PC_JOB.ENCHANTER:
                    return 1.25f;
                case PC_JOB.VATES:
                    return 1.1f;
                case PC_JOB.DRUID:
                    return 1.35f;
                case PC_JOB.BARD:
                    return 1.25f;
                case PC_JOB.WARLOCK:
                    return 1.1f;
                case PC_JOB.CABALIST:
                    return 1.4f;
                case PC_JOB.NECROMANCER:
                    return 1.35f;
                case PC_JOB.TATARABE:
                    return 1.15f;
                case PC_JOB.BLACKSMITH:
                case PC_JOB.BREEDER:
                case PC_JOB.GARDNER:
                    return 1.6f;
                case PC_JOB.MACHINERY:
                    return 1.9f;
                case PC_JOB.FARMASIST:
                    return 1.1f;
                case PC_JOB.ALCHEMIST:
                    return 1.8f;
                case PC_JOB.MARIONEST:
                    return 1.7f;
                case PC_JOB.RANGER:
                    return 1.15f;
                case PC_JOB.EXPLORER:
                    return 1.85f;
                case PC_JOB.TREASUREHUNTER:
                    return 2.1f;
                case PC_JOB.MERCHANT:
                    return 1.1f;
                case PC_JOB.TRADER:
                    return 1.9f;
                case PC_JOB.GAMBLER:
                    return 1.7f;
                default:
                    return 1f;
            }
        }

        /// <summary>
        /// The CalcStatsRev.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void CalcStatsRev(ActorPC pc)
        {
            if (pc.JobJoint == PC_JOB.NONE)
            {
                byte num1;
                byte num2;
                byte num3;
                if (Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion))
                {
                    num1 = pc.DominionJobLevel;
                    num2 = pc.DominionJobLevel;
                    num3 = pc.DominionJobLevel;
                }
                else
                {
                    num1 = pc.JobLevel1;
                    num2 = pc.JobLevel2X;
                    num3 = pc.JobLevel2T;
                }
                switch (pc.Job)
                {
                    case PC_JOB.NOVICE:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.0700000002980232);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.0700000002980232);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.0700000002980232);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0700000002980232);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.0700000002980232);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0700000002980232);
                        break;
                    case PC_JOB.SWORDMAN:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.259999990463257);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.0299999993294477);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.189999997615814);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0199999995529652);
                        break;
                    case PC_JOB.BLADEMASTER:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.300000011920929);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.239999994635582);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.0399999991059303);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.200000002980232);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.200000002980232);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.0199999995529652);
                        break;
                    case PC_JOB.BOUNTYHUNTER:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.230000004172325);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.230000004172325);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.119999997317791);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.150000005960464);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.25);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.0199999995529652);
                        break;
                    case PC_JOB.FENCER:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.209999993443489);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.180000007152557);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.0500000007450581);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.180000007152557);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.159999996423721);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0199999995529652);
                        break;
                    case PC_JOB.KNIGHT:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.170000001788139);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.200000002980232);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.0799999982118607);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.319999992847443);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.140000000596046);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.0900000035762787);
                        break;
                    case PC_JOB.DARKSTALKER:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.180000007152557);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.230000004172325);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.0799999982118607);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.239999994635582);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.170000001788139);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.100000001490116);
                        break;
                    case PC_JOB.SCOUT:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.200000002980232);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.200000002980232);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.0500000007450581);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0700000002980232);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.259999990463257);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0199999995529652);
                        break;
                    case PC_JOB.ASSASSIN:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.209999993443489);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.219999998807907);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.140000000596046);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.109999999403954);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.300000011920929);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.0199999995529652);
                        break;
                    case PC_JOB.COMMAND:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.219999998807907);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.239999994635582);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.159999996423721);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.119999997317791);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.230000004172325);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.0299999993294477);
                        break;
                    case PC_JOB.ARCHER:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.119999997317791);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.259999990463257);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.100000001490116);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0199999995529652);
                        break;
                    case PC_JOB.STRIKER:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.209999993443489);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.140000000596046);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.300000011920929);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.119999997317791);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.209999993443489);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.0199999995529652);
                        break;
                    case PC_JOB.GUNNER:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.200000002980232);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.159999996423721);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.300000011920929);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.119999997317791);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.159999996423721);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.0599999986588955);
                        break;
                    case PC_JOB.WIZARD:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.0199999995529652);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.100000001490116);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.209999993443489);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0700000002980232);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.100000001490116);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.300000011920929);
                        break;
                    case PC_JOB.SORCERER:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.0299999993294477);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.159999996423721);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.239999994635582);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.150000005960464);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.119999997317791);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.300000011920929);
                        break;
                    case PC_JOB.SAGE:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.0599999986588955);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.239999994635582);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.239999994635582);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.0599999986588955);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.140000000596046);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.259999990463257);
                        break;
                    case PC_JOB.SHAMAN:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.0199999995529652);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.129999995231628);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.180000007152557);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0599999986588955);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.129999995231628);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.280000001192093);
                        break;
                    case PC_JOB.ELEMENTER:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.0299999993294477);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.25);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.200000002980232);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.0799999982118607);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.150000005960464);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.28999999165535);
                        break;
                    case PC_JOB.ENCHANTER:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.0500000007450581);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.270000010728836);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.25);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.0599999986588955);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.100000001490116);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.270000010728836);
                        break;
                    case PC_JOB.VATES:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.0500000007450581);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.109999999403954);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.25);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0199999995529652);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.109999999403954);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.259999990463257);
                        break;
                    case PC_JOB.DRUID:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.119999997317791);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.159999996423721);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.259999990463257);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.0599999986588955);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.119999997317791);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.280000001192093);
                        break;
                    case PC_JOB.BARD:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.0799999982118607);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.200000002980232);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.219999998807907);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.100000001490116);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.200000002980232);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.200000002980232);
                        break;
                    case PC_JOB.WARLOCK:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.140000000596046);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.129999995231628);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0799999982118607);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.119999997317791);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.180000007152557);
                        break;
                    case PC_JOB.CABALIST:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.209999993443489);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.170000001788139);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.140000000596046);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.140000000596046);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.0900000035762787);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.25);
                        break;
                    case PC_JOB.NECROMANCER:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.140000000596046);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.129999995231628);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.180000007152557);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.0599999986588955);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.219999998807907);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.270000010728836);
                        break;
                    case PC_JOB.TATARABE:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.25);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.0599999986588955);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.180000007152557);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.140000000596046);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0199999995529652);
                        break;
                    case PC_JOB.BLACKSMITH:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.189999997615814);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.280000001192093);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.119999997317791);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.230000004172325);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.0599999986588955);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.119999997317791);
                        break;
                    case PC_JOB.MACHINERY:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.159999996423721);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.230000004172325);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.209999993443489);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.159999996423721);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.200000002980232);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.0399999991059303);
                        break;
                    case PC_JOB.FARMASIST:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.180000007152557);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.170000001788139);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.200000002980232);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.0799999982118607);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0199999995529652);
                        break;
                    case PC_JOB.ALCHEMIST:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.159999996423721);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.25);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.239999994635582);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.209999993443489);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.0799999982118607);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.0599999986588955);
                        break;
                    case PC_JOB.MARIONEST:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.140000000596046);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.170000001788139);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.219999998807907);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.0799999982118607);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.209999993443489);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.180000007152557);
                        break;
                    case PC_JOB.RANGER:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.100000001490116);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.239999994635582);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.129999995231628);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0799999982118607);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.219999998807907);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0299999993294477);
                        break;
                    case PC_JOB.EXPLORER:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.140000000596046);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.270000010728836);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.159999996423721);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.119999997317791);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.259999990463257);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.0500000007450581);
                        break;
                    case PC_JOB.TREASUREHUNTER:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.0799999982118607);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.159999996423721);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.159999996423721);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.25);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.270000010728836);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.0799999982118607);
                        break;
                    case PC_JOB.MERCHANT:
                        pc.Status.str_rev = (ushort)((double)num1 * 0.119999997317791);
                        pc.Status.dex_rev = (ushort)((double)num1 * 0.209999993443489);
                        pc.Status.int_rev = (ushort)((double)num1 * 0.25);
                        pc.Status.vit_rev = (ushort)((double)num1 * 0.0500000007450581);
                        pc.Status.agi_rev = (ushort)((double)num1 * 0.150000005960464);
                        pc.Status.mag_rev = (ushort)((double)num1 * 0.0199999995529652);
                        break;
                    case PC_JOB.TRADER:
                        pc.Status.str_rev = (ushort)((double)((int)num2 + 30) * 0.150000005960464);
                        pc.Status.dex_rev = (ushort)((double)((int)num2 + 30) * 0.259999990463257);
                        pc.Status.int_rev = (ushort)((double)((int)num2 + 30) * 0.300000011920929);
                        pc.Status.vit_rev = (ushort)((double)((int)num2 + 30) * 0.0599999986588955);
                        pc.Status.agi_rev = (ushort)((double)((int)num2 + 30) * 0.200000002980232);
                        pc.Status.mag_rev = (ushort)((double)((int)num2 + 30) * 0.0299999993294477);
                        break;
                    case PC_JOB.GAMBLER:
                        pc.Status.str_rev = (ushort)((double)((int)num3 + 30) * 0.100000001490116);
                        pc.Status.dex_rev = (ushort)((double)((int)num3 + 30) * 0.200000002980232);
                        pc.Status.int_rev = (ushort)((double)((int)num3 + 30) * 0.209999993443489);
                        pc.Status.vit_rev = (ushort)((double)((int)num3 + 30) * 0.140000000596046);
                        pc.Status.agi_rev = (ushort)((double)((int)num3 + 30) * 0.259999990463257);
                        pc.Status.mag_rev = (ushort)((double)((int)num3 + 30) * 0.0900000035762787);
                        break;
                }
            }
            else
            {
                switch (pc.JobJoint)
                {
                    case PC_JOB.BREEDER:
                        pc.Status.str_rev = (ushort)(3.0 + (double)((int)pc.JointJobLevel + 3) * 0.143);
                        pc.Status.dex_rev = (ushort)(6.0 + (double)((int)pc.JointJobLevel + 1) * 0.25);
                        pc.Status.int_rev = (ushort)(1.0 + (double)pc.JointJobLevel * 0.04);
                        pc.Status.vit_rev = (ushort)(6.0 + (double)((int)pc.JointJobLevel + 1) * 0.25);
                        pc.Status.agi_rev = (ushort)(7.0 + (double)pc.JointJobLevel * 0.28);
                        pc.Status.mag_rev = (ushort)(1.0 + (double)pc.JointJobLevel * 0.04);
                        break;
                    case PC_JOB.GARDNER:
                        pc.Status.str_rev = (ushort)(3.0 + (double)((int)pc.JointJobLevel + 2) * 0.125);
                        pc.Status.dex_rev = (ushort)(6.0 + (double)((int)pc.JointJobLevel + 1) * 0.25);
                        pc.Status.int_rev = (ushort)(6.0 + (double)((int)pc.JointJobLevel + 1) * 0.25);
                        pc.Status.vit_rev = (ushort)(5.0 + (double)((int)pc.JointJobLevel + 2) * 0.25);
                        pc.Status.agi_rev = (ushort)(3.0 + (double)((int)pc.JointJobLevel - 1) * 0.125);
                        pc.Status.mag_rev = (ushort)((double)pc.JointJobLevel * 0.04);
                        break;
                }
            }
        }

        /// <summary>
        /// The CalcEquipBonus.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void CalcEquipBonus(ActorPC pc)
        {
            pc.Status.ClearItem();
            pc.Inventory.MaxPayload[ContainerType.BODY] = 0U;
            pc.Inventory.MaxPayload[ContainerType.BACK_BAG] = 0U;
            pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] = 0U;
            pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] = 0U;
            pc.Inventory.MaxVolume[ContainerType.BODY] = 0U;
            pc.Inventory.MaxVolume[ContainerType.BACK_BAG] = 0U;
            pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] = 0U;
            pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] = 0U;
            Dictionary<EnumEquipSlot, SagaDB.Item.Item> dictionary = pc.Form != DEM_FORM.NORMAL_FORM ? pc.Inventory.Parts : pc.Inventory.Equipments;
            foreach (EnumEquipSlot key1 in dictionary.Keys)
            {
                SagaDB.Item.Item obj = dictionary[key1];
                if (obj.Stack != (ushort)0)
                {
                    if (key1 != EnumEquipSlot.PET || obj.BaseData.itemType == ItemType.BACK_DEMON)
                    {
                        pc.Status.atk1_item = (short)((int)pc.Status.atk1_item + (int)obj.BaseData.atk1 + (int)obj.Atk1);
                        pc.Status.atk2_item = (short)((int)pc.Status.atk2_item + (int)obj.BaseData.atk2 + (int)obj.Atk2);
                        pc.Status.atk3_item = (short)((int)pc.Status.atk3_item + (int)obj.BaseData.atk3 + (int)obj.Atk3);
                        pc.Status.matk_item = (short)((int)pc.Status.matk_item + (int)obj.BaseData.matk + (int)obj.MAtk);
                        pc.Status.def_add = (short)((int)pc.Status.def_add + (int)obj.BaseData.def + (int)obj.Def);
                        pc.Status.mdef_add = (short)((int)pc.Status.mdef_add + (int)obj.BaseData.mdef + (int)obj.MDef);
                        pc.Status.hit_melee_item = (short)((int)pc.Status.hit_melee_item + (int)obj.BaseData.hitMelee + (int)obj.HitMelee);
                        pc.Status.hit_ranged_item = (short)((int)pc.Status.hit_ranged_item + (int)obj.BaseData.hitRanged + (int)obj.HitRanged);
                        pc.Status.avoid_melee_item = (short)((int)pc.Status.avoid_melee_item + (int)obj.BaseData.avoidMelee + (int)obj.AvoidMelee);
                        pc.Status.avoid_ranged_item = (short)((int)pc.Status.avoid_ranged_item + (int)obj.BaseData.avoidRanged + (int)obj.AvoidRanged);
                        pc.Status.str_item = (short)((int)pc.Status.str_item + (int)obj.BaseData.str + (int)obj.Str);
                        pc.Status.agi_item = (short)((int)pc.Status.agi_item + (int)obj.BaseData.agi + (int)obj.Agi);
                        pc.Status.dex_item = (short)((int)pc.Status.dex_item + (int)obj.BaseData.dex + (int)obj.Dex);
                        pc.Status.vit_item = (short)((int)pc.Status.vit_item + (int)obj.BaseData.vit + (int)obj.Vit);
                        pc.Status.int_item = (short)((int)pc.Status.int_item + (int)obj.BaseData.intel + (int)obj.Int);
                        pc.Status.mag_item = (short)((int)pc.Status.mag_item + (int)obj.BaseData.mag + (int)obj.Mag);
                        pc.Status.hp_item = (short)((int)pc.Status.hp_item + (int)obj.BaseData.hp + (int)obj.HP);
                        pc.Status.sp_item = (short)((int)pc.Status.sp_item + (int)obj.BaseData.sp + (int)obj.SP);
                        pc.Status.mp_item = (short)((int)pc.Status.mp_item + (int)obj.BaseData.mp + (int)obj.MP);
                        pc.Status.speed_up = pc.Status.speed_up + (int)obj.BaseData.speedUp + (int)obj.SpeedUp;
                        if ((obj.BaseData.speedUp != (short)0 || obj.SpeedUp != (short)0) && pc.Online)
                            pc.e.PropertyUpdate(UpdateEvent.SPEED, 0);
                        if (obj.IsArmor)
                        {
                            foreach (Elements key2 in pc.Elements.Keys)
                            {
                                Dictionary<Elements, int> elementsItem;
                                Elements index;
                                (elementsItem = pc.Status.elements_item)[index = key2] = elementsItem[index] + (int)obj.BaseData.element[key2];
                            }
                        }
                        if (obj.IsWeapon)
                        {
                            foreach (Elements key2 in pc.Elements.Keys)
                            {
                                Dictionary<Elements, int> attackElementsItem;
                                Elements index;
                                (attackElementsItem = pc.Status.attackElements_item)[index = key2] = attackElementsItem[index] + (int)obj.BaseData.element[key2];
                            }
                        }
                    }
                    if (obj.BaseData.weightUp != (short)0)
                    {
                        switch (key1)
                        {
                            case EnumEquipSlot.BACK:
                                pc.Inventory.MaxPayload[ContainerType.BACK_BAG] = (uint)((ulong)pc.Inventory.MaxPayload[ContainerType.BACK_BAG] + (ulong)obj.BaseData.weightUp);
                                break;
                            case EnumEquipSlot.RIGHT_HAND:
                                pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] = (uint)((ulong)pc.Inventory.MaxPayload[ContainerType.RIGHT_BAG] + (ulong)obj.BaseData.weightUp);
                                break;
                            case EnumEquipSlot.LEFT_HAND:
                                pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] = (uint)((ulong)pc.Inventory.MaxPayload[ContainerType.LEFT_BAG] + (ulong)obj.BaseData.weightUp);
                                break;
                            case EnumEquipSlot.PET:
                                pc.Inventory.MaxPayload[ContainerType.BODY] = (uint)((ulong)pc.Inventory.MaxPayload[ContainerType.BODY] + (ulong)obj.BaseData.weightUp);
                                break;
                        }
                    }
                    if (obj.BaseData.volumeUp != (short)0)
                    {
                        float num = 0.0f;
                        if (pc.Status.Additions.ContainsKey("Packing"))
                            num = (float)((DefaultPassiveSkill)pc.Status.Additions["Packing"])["Packing"] / 100f;
                        switch (key1)
                        {
                            case EnumEquipSlot.BACK:
                                pc.Inventory.MaxVolume[ContainerType.BACK_BAG] = (uint)((double)pc.Inventory.MaxVolume[ContainerType.BACK_BAG] + (double)obj.BaseData.volumeUp * (1.0 + (double)num));
                                break;
                            case EnumEquipSlot.RIGHT_HAND:
                                pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] = (uint)((double)pc.Inventory.MaxVolume[ContainerType.RIGHT_BAG] + (double)obj.BaseData.volumeUp * (1.0 + (double)num));
                                break;
                            case EnumEquipSlot.LEFT_HAND:
                                pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] = (uint)((double)pc.Inventory.MaxVolume[ContainerType.LEFT_BAG] + (double)obj.BaseData.volumeUp * (1.0 + (double)num));
                                break;
                            case EnumEquipSlot.PET:
                                pc.Inventory.MaxVolume[ContainerType.BODY] = (uint)((double)pc.Inventory.MaxVolume[ContainerType.BODY] + (double)obj.BaseData.volumeUp * (1.0 + (double)num));
                                break;
                        }
                    }
                    this.ApplyIrisCard(pc, obj);
                }
            }
            this.CalcDemicChips(pc);
        }

        /// <summary>
        /// The ApplyIrisCard.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        private void ApplyIrisCard(ActorPC pc, SagaDB.Item.Item item)
        {
            if (!item.Locked)
                return;
            Dictionary<ReleaseAbility, int> dictionary1 = item.ReleaseAbilities(false);
            foreach (ReleaseAbility key in dictionary1.Keys)
            {
                int num = dictionary1[key];
                switch (key)
                {
                    case ReleaseAbility.HP最大値上昇Plus:
                        pc.Status.hp_item += (short)num;
                        break;
                    case ReleaseAbility.MP最大値上昇Plus:
                        pc.Status.mp_item += (short)num;
                        break;
                    case ReleaseAbility.SP最大値上昇Plus:
                        pc.Status.sp_item += (short)num;
                        break;
                    case ReleaseAbility.STR上昇Plus:
                        pc.Status.str_item += (short)num;
                        break;
                    case ReleaseAbility.DEX上昇Plus:
                        pc.Status.dex_item += (short)num;
                        break;
                    case ReleaseAbility.INT上昇Plus:
                        pc.Status.int_item += (short)num;
                        break;
                    case ReleaseAbility.VIT上昇Plus:
                        pc.Status.vit_item += (short)num;
                        break;
                    case ReleaseAbility.AGI上昇Plus:
                        pc.Status.agi_item += (short)num;
                        break;
                    case ReleaseAbility.MAG上昇Plus:
                        pc.Status.mag_item += (short)num;
                        break;
                    case ReleaseAbility.ATK値上昇Plus:
                        pc.Status.atk1_item += (short)num;
                        pc.Status.atk2_item += (short)num;
                        pc.Status.atk3_item += (short)num;
                        break;
                    case ReleaseAbility.MATK値上昇Plus:
                        pc.Status.matk_item += (short)num;
                        break;
                    case ReleaseAbility.SHIT値上昇Plus:
                        pc.Status.hit_melee_item += (short)num;
                        break;
                    case ReleaseAbility.SHIT値上昇Perc:
                        pc.Status.hit_melee_item += (short)((double)pc.Status.hit_melee * ((double)num / 100.0 + 1.0));
                        break;
                    case ReleaseAbility.LHIT値上昇Plus:
                        pc.Status.hit_ranged_item += (short)num;
                        break;
                    case ReleaseAbility.LHIT値上昇Perc:
                        pc.Status.hit_ranged_item += (short)((double)pc.Status.hit_ranged * ((double)num / 100.0 + 1.0));
                        break;
                    case ReleaseAbility.DEF上昇Plus:
                        pc.Status.def_add += (short)num;
                        break;
                    case ReleaseAbility.武器攻撃力上昇Plus:
                        pc.Status.atk1_item += (short)num;
                        pc.Status.atk2_item += (short)num;
                        pc.Status.atk3_item += (short)num;
                        break;
                    case ReleaseAbility.武器攻撃力上昇Perc:
                        pc.Status.atk1_item += (short)((double)item.BaseData.atk1 * ((double)num / 100.0));
                        pc.Status.atk2_item += (short)((double)item.BaseData.atk2 * ((double)num / 100.0));
                        pc.Status.atk3_item += (short)((double)item.BaseData.atk3 * ((double)num / 100.0));
                        break;
                    case ReleaseAbility.防具防御力上昇Plus:
                        pc.Status.def_add += (short)num;
                        break;
                    case ReleaseAbility.防具防御力上昇Perc:
                        pc.Status.def_add += (short)((double)item.BaseData.def * ((double)num / 100.0));
                        break;
                    case ReleaseAbility.ガード率上昇Perc:
                        pc.Status.guard_item += (short)num;
                        break;
                    case ReleaseAbility.ペイロード上昇Perc:
                        pc.Status.payl_item += (short)num;
                        break;
                    case ReleaseAbility.キャパシティ上昇Perc:
                        pc.Status.volume_item += (short)num;
                        break;
                }
            }
            Dictionary<Elements, int> dictionary2 = item.Elements(false);
            if (!item.IsArmor && !item.IsWeapon)
                return;
            if (item.IsWeapon)
            {
                foreach (Elements key in dictionary2.Keys)
                {
                    Dictionary<Elements, int> attackElementsItem;
                    Elements index;
                    (attackElementsItem = pc.Status.attackElements_item)[index = key] = attackElementsItem[index] + dictionary2[key];
                }
            }
            if (item.IsArmor)
            {
                foreach (Elements key in dictionary2.Keys)
                {
                    Dictionary<Elements, int> elementsItem;
                    Elements index;
                    (elementsItem = pc.Status.elements_item)[index = key] = elementsItem[index] + dictionary2[key];
                }
            }
        }

        /// <summary>
        /// The CalcDemicChips.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void CalcDemicChips(ActorPC pc)
        {
            Dictionary<byte, DEMICPanel> dictionary1 = !pc.InDominionWorld ? pc.Inventory.DemicChips : pc.Inventory.DominionDemicChips;
            Dictionary<uint, int> dictionary2 = new Dictionary<uint, int>();
            foreach (byte key in dictionary1.Keys)
            {
                foreach (Chip chip1 in dictionary1[key].Chips)
                {
                    byte x1 = byte.MaxValue;
                    byte y1 = byte.MaxValue;
                    byte x2 = byte.MaxValue;
                    byte y2 = byte.MaxValue;
                    if (dictionary1[key].EngageTask1 != byte.MaxValue)
                    {
                        x1 = (byte)((uint)dictionary1[key].EngageTask1 % 9U);
                        y1 = (byte)((uint)dictionary1[key].EngageTask1 / 9U);
                    }
                    if (dictionary1[key].EngageTask2 != byte.MaxValue)
                    {
                        x2 = (byte)((uint)dictionary1[key].EngageTask2 % 9U);
                        y2 = (byte)((uint)dictionary1[key].EngageTask2 / 9U);
                    }
                    bool flag = chip1.IsNear(x1, y1) || chip1.IsNear(x2, y2);
                    if (chip1.Data.type < (byte)20)
                    {
                        int num = 1;
                        if (flag)
                            num = 2;
                        pc.Status.m_str_chip += (short)(num * (int)chip1.Data.str);
                        pc.Status.m_agi_chip += (short)(num * (int)chip1.Data.agi);
                        pc.Status.m_vit_chip += (short)(num * (int)chip1.Data.vit);
                        pc.Status.m_dex_chip += (short)(num * (int)chip1.Data.dex);
                        pc.Status.m_int_chip += (short)(num * (int)chip1.Data.intel);
                        pc.Status.m_mag_chip += (short)(num * (int)chip1.Data.mag);
                    }
                    else if (chip1.Data.type < (byte)30)
                    {
                        int num = 1;
                        if (flag)
                            num = 2;
                        if (dictionary2.ContainsKey(chip1.Data.skill1))
                        {
                            Dictionary<uint, int> dictionary3;
                            uint skill1;
                            (dictionary3 = dictionary2)[skill1 = chip1.Data.skill1] = dictionary3[skill1] + num;
                        }
                        else if (chip1.Data.skill1 != 0U)
                            dictionary2.Add(chip1.Data.skill1, num);
                        if (dictionary2.ContainsKey(chip1.Data.skill2))
                        {
                            Dictionary<uint, int> dictionary3;
                            uint skill2;
                            (dictionary3 = dictionary2)[skill2 = chip1.Data.skill2] = dictionary3[skill2] + num;
                        }
                        else if (chip1.Data.skill2 != 0U)
                            dictionary2.Add(chip1.Data.skill2, num);
                        if (dictionary2.ContainsKey(chip1.Data.skill3))
                        {
                            Dictionary<uint, int> dictionary3;
                            uint skill3;
                            (dictionary3 = dictionary2)[skill3 = chip1.Data.skill3] = dictionary3[skill3] + num;
                        }
                        else if (chip1.Data.skill3 != 0U)
                            dictionary2.Add(chip1.Data.skill3, num);
                    }
                    else
                    {
                        Chip chip2 = !Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID.ContainsKey(chip1.Data.engageTaskChip) || !flag ? chip1 : new Chip(Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID[chip1.Data.engageTaskChip]);
                        pc.Status.m_str_chip += chip2.Data.str;
                        pc.Status.m_agi_chip += chip2.Data.agi;
                        pc.Status.m_vit_chip += chip2.Data.vit;
                        pc.Status.m_dex_chip += chip2.Data.dex;
                        pc.Status.m_int_chip += chip2.Data.intel;
                        pc.Status.m_mag_chip += chip2.Data.mag;
                        pc.Status.hp_rate_item += chip2.Data.hp;
                        pc.Status.sp_rate_item += chip2.Data.sp;
                        pc.Status.mp_rate_item += chip2.Data.mp;
                    }
                }
            }
            foreach (uint key in dictionary2.Keys)
            {
                int num = pc.Form == DEM_FORM.MACHINA_FORM ? dictionary2[key] : 0;
                if (pc.Skills.ContainsKey(key))
                {
                    if ((int)pc.Skills[key].Level != num)
                    {
                        pc.Skills[key].Level = (byte)num;
                        if ((int)pc.Skills[key].Level > (int)pc.Skills[key].MaxLevel)
                            pc.Skills[key].Level = pc.Skills[key].MaxLevel;
                    }
                }
                else
                {
                    SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(key, (byte)1);
                    skill.Level = (byte)num;
                    if ((int)skill.Level > (int)skill.MaxLevel)
                        skill.Level = skill.MaxLevel;
                    skill.NoSave = true;
                    pc.Skills.Add(key, skill);
                }
            }
        }

        /// <summary>
        /// The CalcMarionetteBonus.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void CalcMarionetteBonus(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                pc.Status.agi_mario = pc.Marionette.agi;
                pc.Status.def_add_mario = pc.Marionette.def_add;
                pc.Status.def_mario = pc.Marionette.def;
                pc.Status.dex_mario = pc.Marionette.dex;
                pc.Status.hp_mario = pc.Marionette.hp;
                pc.Status.hp_recover_mario = pc.Marionette.hp_recover;
                pc.Status.int_mario = pc.Marionette.intel;
                pc.Status.mag_mario = pc.Marionette.mag;
                pc.Status.max_atk1_mario = pc.Marionette.max_atk1;
                pc.Status.max_atk2_mario = pc.Marionette.max_atk2;
                pc.Status.max_atk3_mario = pc.Marionette.max_atk3;
                pc.Status.min_atk1_mario = pc.Marionette.min_atk1;
                pc.Status.min_atk2_mario = pc.Marionette.min_atk2;
                pc.Status.min_atk3_mario = pc.Marionette.min_atk3;
                pc.Status.max_matk_mario = pc.Marionette.max_matk;
                pc.Status.min_matk_mario = pc.Marionette.min_matk;
                pc.Status.mdef_add_mario = pc.Marionette.mdef_add;
                pc.Status.mdef_mario = pc.Marionette.mdef;
                pc.Status.mp_mario = pc.Marionette.mp;
                pc.Status.mp_recover_mario = pc.Marionette.mp_recover;
                pc.Status.sp_mario = pc.Marionette.sp;
                pc.Status.str_mario = pc.Marionette.str;
                pc.Status.vit_mario = pc.Marionette.vit;
            }
            else
            {
                pc.Status.agi_mario = (short)0;
                pc.Status.def_add_mario = (short)0;
                pc.Status.def_mario = (short)0;
                pc.Status.dex_mario = (short)0;
                pc.Status.hp_mario = (short)0;
                pc.Status.hp_recover_mario = (short)0;
                pc.Status.int_mario = (short)0;
                pc.Status.mag_mario = (short)0;
                pc.Status.max_atk1_mario = (short)0;
                pc.Status.max_atk2_mario = (short)0;
                pc.Status.max_atk3_mario = (short)0;
                pc.Status.min_atk1_mario = (short)0;
                pc.Status.min_atk2_mario = (short)0;
                pc.Status.min_atk3_mario = (short)0;
                pc.Status.max_matk_mario = (short)0;
                pc.Status.min_matk_mario = (short)0;
                pc.Status.mdef_add_mario = (short)0;
                pc.Status.mdef_mario = (short)0;
                pc.Status.mp_mario = (short)0;
                pc.Status.mp_recover_mario = (short)0;
                pc.Status.sp_mario = (short)0;
                pc.Status.str_mario = (short)0;
                pc.Status.vit_mario = (short)0;
            }
        }
    }
}
