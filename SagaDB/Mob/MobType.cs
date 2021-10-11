namespace SagaDB.Mob
{
    /// <summary>
    /// Defines the MobType.
    /// </summary>
    public enum MobType
    {
        /// <summary>
        /// Defines the HUMAN.
        /// </summary>
        HUMAN,

        /// <summary>
        /// Defines the HUMAN_SKILL.
        /// </summary>
        HUMAN_SKILL,

        /// <summary>
        /// Defines the HUMAN_BOSS.
        /// </summary>
        HUMAN_BOSS,

        /// <summary>
        /// Defines the HUMAN_BOSS_SKILL.
        /// </summary>
        HUMAN_BOSS_SKILL,

        /// <summary>
        /// Defines the HUMAN_NOTOUCH.
        /// </summary>
        HUMAN_NOTOUCH,

        /// <summary>
        /// Defines the HUMAN_CHAMP.
        /// </summary>
        HUMAN_CHAMP,

        /// <summary>
        /// Defines the HUMAN_BOSS_CHAMP.
        /// </summary>
        HUMAN_BOSS_CHAMP,

        /// <summary>
        /// Defines the HUMAN_SKILL_CHAMP.
        /// </summary>
        HUMAN_SKILL_CHAMP,

        /// <summary>
        /// Defines the HUMAN_SKILL_BOSS_CHAMP.
        /// </summary>
        HUMAN_SKILL_BOSS_CHAMP,

        /// <summary>
        /// Defines the HUMAN_SMARK_HETERODOXY.
        /// </summary>
        HUMAN_SMARK_HETERODOXY,

        /// <summary>
        /// Defines the HUMAN_SMARK_BOSS_HETERODOXY.
        /// </summary>
        HUMAN_SMARK_BOSS_HETERODOXY,

        /// <summary>
        /// Defines the HUMAN_BOSS_MIRROR.
        /// </summary>
        HUMAN_BOSS_MIRROR,

        /// <summary>
        /// Defines the HUMAN_BOSS_SKILL_CHAMP.
        /// </summary>
        HUMAN_BOSS_SKILL_CHAMP,

        /// <summary>
        /// Defines the PLANT.
        /// </summary>
        PLANT,

        /// <summary>
        /// Defines the PLANT_MARK.
        /// </summary>
        PLANT_MARK,

        /// <summary>
        /// Defines the PLANT_BOSS.
        /// </summary>
        PLANT_BOSS,

        /// <summary>
        /// Defines the PLANT_SKILL.
        /// </summary>
        PLANT_SKILL,

        /// <summary>
        /// Defines the PLANT_NOTOUCH.
        /// </summary>
        PLANT_NOTOUCH,

        /// <summary>
        /// Defines the PLANT_MATERIAL.
        /// </summary>
        PLANT_MATERIAL,

        /// <summary>
        /// Defines the PLANT_UNITE.
        /// </summary>
        PLANT_UNITE,

        /// <summary>
        /// Defines the PLANT_NOTPTDROPRANGE.
        /// </summary>
        PLANT_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the PLANT_BOSS_SKILL.
        /// </summary>
        PLANT_BOSS_SKILL,

        /// <summary>
        /// Defines the PLANT_BOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        PLANT_BOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the PLANT_MATERIAL_EAST.
        /// </summary>
        PLANT_MATERIAL_EAST,

        /// <summary>
        /// Defines the PLANT_MATERIAL_WEST.
        /// </summary>
        PLANT_MATERIAL_WEST,

        /// <summary>
        /// Defines the PLANT_MATERIAL_SOUTH.
        /// </summary>
        PLANT_MATERIAL_SOUTH,

        /// <summary>
        /// Defines the PLANT_MATERIAL_NORTH.
        /// </summary>
        PLANT_MATERIAL_NORTH,

        /// <summary>
        /// Defines the PLANT_MATERIAL_SKILL.
        /// </summary>
        PLANT_MATERIAL_SKILL,

        /// <summary>
        /// Defines the PLANT_MATERIAL_HETERODOXY.
        /// </summary>
        PLANT_MATERIAL_HETERODOXY,

        /// <summary>
        /// Defines the PLANT_MATERIAL_NOTPTDROPRANGE.
        /// </summary>
        PLANT_MATERIAL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the PLANT_MATERIAL_BOSS_MARK.
        /// </summary>
        PLANT_MATERIAL_BOSS_MARK,

        /// <summary>
        /// Defines the PLANT_MATERIAL_EAST_BOSS_SKILL_WALL.
        /// </summary>
        PLANT_MATERIAL_EAST_BOSS_SKILL_WALL,

        /// <summary>
        /// Defines the PLANT_MATERIAL_WEST_BOSS_SKILL_WALL.
        /// </summary>
        PLANT_MATERIAL_WEST_BOSS_SKILL_WALL,

        /// <summary>
        /// Defines the PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL.
        /// </summary>
        PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL,

        /// <summary>
        /// Defines the PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL.
        /// </summary>
        PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL,

        /// <summary>
        /// Defines the ROCK.
        /// </summary>
        ROCK,

        /// <summary>
        /// Defines the ROCK_MATERIAL.
        /// </summary>
        ROCK_MATERIAL,

        /// <summary>
        /// Defines the ROCK_SKILL.
        /// </summary>
        ROCK_SKILL,

        /// <summary>
        /// Defines the ROCK_BOMB_SKILL.
        /// </summary>
        ROCK_BOMB_SKILL,

        /// <summary>
        /// Defines the ROCK_MATERIAL_SKILL.
        /// </summary>
        ROCK_MATERIAL_SKILL,

        /// <summary>
        /// Defines the ROCK_MATERIAL_NORTH_NOTOUCH.
        /// </summary>
        ROCK_MATERIAL_NORTH_NOTOUCH,

        /// <summary>
        /// Defines the ROCK_MATERIAL_SOUTH_NOTOUCH.
        /// </summary>
        ROCK_MATERIAL_SOUTH_NOTOUCH,

        /// <summary>
        /// Defines the ROCK_MATERIAL_EAST_NOTOUCH.
        /// </summary>
        ROCK_MATERIAL_EAST_NOTOUCH,

        /// <summary>
        /// Defines the ROCK_MATERIAL_WEST_NOTOUCH.
        /// </summary>
        ROCK_MATERIAL_WEST_NOTOUCH,

        /// <summary>
        /// Defines the ROCK_NOTPTDROPRANGE.
        /// </summary>
        ROCK_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ROCK_BOSS_SKILL_WALL.
        /// </summary>
        ROCK_BOSS_SKILL_WALL,

        /// <summary>
        /// Defines the ROCK_MATERIAL_BOSS_NOTPTDROPRANGE.
        /// </summary>
        ROCK_MATERIAL_BOSS_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ROCK_MATERIAL_BOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        ROCK_MATERIAL_BOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ROCK_BOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        ROCK_BOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ROCK_MATERIAL_BOSS.
        /// </summary>
        ROCK_MATERIAL_BOSS,

        /// <summary>
        /// Defines the BIRD.
        /// </summary>
        BIRD,

        /// <summary>
        /// Defines the BIRD_BOSS.
        /// </summary>
        BIRD_BOSS,

        /// <summary>
        /// Defines the BIRD_UNITE.
        /// </summary>
        BIRD_UNITE,

        /// <summary>
        /// Defines the BIRD_NOTOUCH.
        /// </summary>
        BIRD_NOTOUCH,

        /// <summary>
        /// Defines the BIRD_BOSS_SKILL.
        /// </summary>
        BIRD_BOSS_SKILL,

        /// <summary>
        /// Defines the BIRD_SPBOSS_SKILL.
        /// </summary>
        BIRD_SPBOSS_SKILL,

        /// <summary>
        /// Defines the ANIMAL.
        /// </summary>
        ANIMAL,

        /// <summary>
        /// Defines the ANIMAL_BOSS.
        /// </summary>
        ANIMAL_BOSS,

        /// <summary>
        /// Defines the ANIMAL_NOTOUCH.
        /// </summary>
        ANIMAL_NOTOUCH,

        /// <summary>
        /// Defines the ANIMAL_SKILL.
        /// </summary>
        ANIMAL_SKILL,

        /// <summary>
        /// Defines the ANIMAL_BOSS_SKILL.
        /// </summary>
        ANIMAL_BOSS_SKILL,

        /// <summary>
        /// Defines the ANIMAL_BOMB_SKILL.
        /// </summary>
        ANIMAL_BOMB_SKILL,

        /// <summary>
        /// Defines the ANIMAL_RIDE_BREEDER.
        /// </summary>
        ANIMAL_RIDE_BREEDER,

        /// <summary>
        /// Defines the ANIMAL_SPBOSS_SKILL.
        /// </summary>
        ANIMAL_SPBOSS_SKILL,

        /// <summary>
        /// Defines the ANIMAL_NOTPTDROPRANGE.
        /// </summary>
        ANIMAL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ANIMAL_BOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        ANIMAL_BOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ANIMAL_BOSS_NOTPTDROPRANGE.
        /// </summary>
        ANIMAL_BOSS_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the WATER_ANIMAL.
        /// </summary>
        WATER_ANIMAL,

        /// <summary>
        /// Defines the WATER_ANIMAL_BOSS.
        /// </summary>
        WATER_ANIMAL_BOSS,

        /// <summary>
        /// Defines the WATER_ANIMAL_SKILL.
        /// </summary>
        WATER_ANIMAL_SKILL,

        /// <summary>
        /// Defines the WATER_ANIMAL_NOTOUCH.
        /// </summary>
        WATER_ANIMAL_NOTOUCH,

        /// <summary>
        /// Defines the WATER_ANIMAL_BOSS_SKILL.
        /// </summary>
        WATER_ANIMAL_BOSS_SKILL,

        /// <summary>
        /// Defines the WATER_ANIMAL_LVDIFF.
        /// </summary>
        WATER_ANIMAL_LVDIFF,

        /// <summary>
        /// Defines the INSECT.
        /// </summary>
        INSECT,

        /// <summary>
        /// Defines the INSECT_SKILL.
        /// </summary>
        INSECT_SKILL,

        /// <summary>
        /// Defines the INSECT_BOSS.
        /// </summary>
        INSECT_BOSS,

        /// <summary>
        /// Defines the INSECT_UNITE.
        /// </summary>
        INSECT_UNITE,

        /// <summary>
        /// Defines the INSECT_NOTOUCH.
        /// </summary>
        INSECT_NOTOUCH,

        /// <summary>
        /// Defines the INSECT_BOSS_SKILL.
        /// </summary>
        INSECT_BOSS_SKILL,

        /// <summary>
        /// Defines the INSECT_NOTPTDROPRANGE.
        /// </summary>
        INSECT_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the INSECT_BOSS_NOTPTDROPRANGE.
        /// </summary>
        INSECT_BOSS_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the INSECT_BOSS_CHAMP.
        /// </summary>
        INSECT_BOSS_CHAMP,

        /// <summary>
        /// Defines the MAGIC_CREATURE.
        /// </summary>
        MAGIC_CREATURE,

        /// <summary>
        /// Defines the MAGIC_CREATURE_SKILL.
        /// </summary>
        MAGIC_CREATURE_SKILL,

        /// <summary>
        /// Defines the MAGIC_CREATURE_BOSS.
        /// </summary>
        MAGIC_CREATURE_BOSS,

        /// <summary>
        /// Defines the MAGIC_CREATURE_LVDIFF.
        /// </summary>
        MAGIC_CREATURE_LVDIFF,

        /// <summary>
        /// Defines the MAGIC_CREATURE_NOTOUCH.
        /// </summary>
        MAGIC_CREATURE_NOTOUCH,

        /// <summary>
        /// Defines the MAGIC_CREATURE_MATERIAL.
        /// </summary>
        MAGIC_CREATURE_MATERIAL,

        /// <summary>
        /// Defines the MAGIC_CREATURE_BOSS_SKILL.
        /// </summary>
        MAGIC_CREATURE_BOSS_SKILL,

        /// <summary>
        /// Defines the MAGIC_CREATURE_NOTPTDROPRANGE.
        /// </summary>
        MAGIC_CREATURE_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the MAGIC_CREATURE_BOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        MAGIC_CREATURE_BOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the MAGIC_CREATURE_BOMB_SKILL.
        /// </summary>
        MAGIC_CREATURE_BOMB_SKILL,

        /// <summary>
        /// Defines the UNDEAD.
        /// </summary>
        UNDEAD,

        /// <summary>
        /// Defines the UNDEAD_BOSS.
        /// </summary>
        UNDEAD_BOSS,

        /// <summary>
        /// Defines the UNDEAD_SKILL.
        /// </summary>
        UNDEAD_SKILL,

        /// <summary>
        /// Defines the UNDEAD_NOTOUCH.
        /// </summary>
        UNDEAD_NOTOUCH,

        /// <summary>
        /// Defines the UNDEAD_BOSS_SKILL.
        /// </summary>
        UNDEAD_BOSS_SKILL,

        /// <summary>
        /// Defines the UNDEAD_BOSS_BOMB_SKILL.
        /// </summary>
        UNDEAD_BOSS_BOMB_SKILL,

        /// <summary>
        /// Defines the UNDEAD_BOSS_SKILL_CHAMP.
        /// </summary>
        UNDEAD_BOSS_SKILL_CHAMP,

        /// <summary>
        /// Defines the UNDEAD_BOSS_CHAMP_BOMB_SKILL_NOTPTDROPRANGE.
        /// </summary>
        UNDEAD_BOSS_CHAMP_BOMB_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the UNDEAD_BOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        UNDEAD_BOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the MACHINE.
        /// </summary>
        MACHINE,

        /// <summary>
        /// Defines the MACHINE_BOSS.
        /// </summary>
        MACHINE_BOSS,

        /// <summary>
        /// Defines the MACHINE_NOTOUCH.
        /// </summary>
        MACHINE_NOTOUCH,

        /// <summary>
        /// Defines the MACHINE_MATERIAL.
        /// </summary>
        MACHINE_MATERIAL,

        /// <summary>
        /// Defines the MACHINE_SKILL.
        /// </summary>
        MACHINE_SKILL,

        /// <summary>
        /// Defines the MACHINE_BOSS_SKILL.
        /// </summary>
        MACHINE_BOSS_SKILL,

        /// <summary>
        /// Defines the MACHINE_BOSS_CHAMP.
        /// </summary>
        MACHINE_BOSS_CHAMP,

        /// <summary>
        /// Defines the MACHINE_NOTPTDROPRANGE.
        /// </summary>
        MACHINE_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the MACHINE_SKILL_BOSS.
        /// </summary>
        MACHINE_SKILL_BOSS,

        /// <summary>
        /// Defines the MACHINE_SMARK_BOSS_SKILL_HETERODOXY_NONBLAST.
        /// </summary>
        MACHINE_SMARK_BOSS_SKILL_HETERODOXY_NONBLAST,

        /// <summary>
        /// Defines the MACHINE_BOSS_SKILL_NOTPTDROPRANGE.
        /// </summary>
        MACHINE_BOSS_SKILL_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ELEMENT.
        /// </summary>
        ELEMENT,

        /// <summary>
        /// Defines the ELEMENT_NOTOUCH.
        /// </summary>
        ELEMENT_NOTOUCH,

        /// <summary>
        /// Defines the ELEMENT_NOTOUCH_SKILL.
        /// </summary>
        ELEMENT_NOTOUCH_SKILL,

        /// <summary>
        /// Defines the ELEMENT_MATERIAL_NOTOUCH_SKILL.
        /// </summary>
        ELEMENT_MATERIAL_NOTOUCH_SKILL,

        /// <summary>
        /// Defines the ELEMENT_BOSS_SKILL.
        /// </summary>
        ELEMENT_BOSS_SKILL,

        /// <summary>
        /// Defines the ELEMENT_SKILL.
        /// </summary>
        ELEMENT_SKILL,

        /// <summary>
        /// Defines the ELEMENT_NOTPTDROPRANGE.
        /// </summary>
        ELEMENT_NOTPTDROPRANGE,

        /// <summary>
        /// Defines the ELEMENT_SKILL_BOSS.
        /// </summary>
        ELEMENT_SKILL_BOSS,

        /// <summary>
        /// Defines the NONE_INFO_MATERIAL.
        /// </summary>
        NONE_INFO_MATERIAL,

        /// <summary>
        /// Defines the TREE.
        /// </summary>
        TREE,

        /// <summary>
        /// Defines the EVENT_BOSS.
        /// </summary>
        EVENT_BOSS,

        /// <summary>
        /// Defines the TREE_MATERIAL.
        /// </summary>
        TREE_MATERIAL,

        /// <summary>
        /// Defines the KNIGHTS_WAR_MATERIAL.
        /// </summary>
        KNIGHTS_WAR_MATERIAL,

        /// <summary>
        /// Defines the KNIGHTS_WAR_INFO_MATERIAL.
        /// </summary>
        KNIGHTS_WAR_INFO_MATERIAL,

        /// <summary>
        /// Defines the LANT_MATERIAL_SKILL.
        /// </summary>
        LANT_MATERIAL_SKILL,

        /// <summary>
        /// Defines the TREASURE_BOX.
        /// </summary>
        TREASURE_BOX,

        /// <summary>
        /// Defines the TREASURE_BOX_MATERIAL.
        /// </summary>
        TREASURE_BOX_MATERIAL,

        /// <summary>
        /// Defines the CONTAINER.
        /// </summary>
        CONTAINER,

        /// <summary>
        /// Defines the CONTAINER_MATERIAL.
        /// </summary>
        CONTAINER_MATERIAL,

        /// <summary>
        /// Defines the TIMBER_BOX_MATERIAL.
        /// </summary>
        TIMBER_BOX_MATERIAL,

        /// <summary>
        /// Defines the COLISEUM_MATERIAL.
        /// </summary>
        COLISEUM_MATERIAL,

        /// <summary>
        /// Defines the CULTURE_PLANT.
        /// </summary>
        CULTURE_PLANT,

        /// <summary>
        /// Defines the CULTURE_TREE_MATERIAL.
        /// </summary>
        CULTURE_TREE_MATERIAL,

        /// <summary>
        /// Defines the CULTURE_PLANT_MATERIAL.
        /// </summary>
        CULTURE_PLANT_MATERIAL,

        /// <summary>
        /// Defines the INSECT_RIDE.
        /// </summary>
        INSECT_RIDE,

        /// <summary>
        /// Defines the ANIMAL_RIDE.
        /// </summary>
        ANIMAL_RIDE,

        /// <summary>
        /// Defines the MACHINE_RIDE_ROBOT.
        /// </summary>
        MACHINE_RIDE_ROBOT,

        /// <summary>
        /// Defines the MACHINE_RIDE.
        /// </summary>
        MACHINE_RIDE,

        /// <summary>
        /// Defines the MAGIC_CREATURE_RIDE.
        /// </summary>
        MAGIC_CREATURE_RIDE,

        /// <summary>
        /// Defines the WATER_ANIMAL_RIDE.
        /// </summary>
        WATER_ANIMAL_RIDE,

        /// <summary>
        /// Defines the HUMAN_RIDE.
        /// </summary>
        HUMAN_RIDE,
    }
}
