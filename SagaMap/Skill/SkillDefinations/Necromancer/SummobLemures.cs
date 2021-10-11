namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SummobLemures" />.
    /// </summary>
    public class SummobLemures : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }

        /// <summary>
        /// The Proc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            SummobLemures.SummobLemuresBuff summobLemuresBuff = new SummobLemures.SummobLemuresBuff(args.skill, sActor, args.x, args.y);
            SkillHandler.ApplyAddition(sActor, (Addition)summobLemuresBuff);
        }

        /// <summary>
        /// Defines the <see cref="SummobLemuresBuff" />.
        /// </summary>
        public class SummobLemuresBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the MobID.
            /// </summary>
            private uint[] MobID = new uint[6]
      {
        0U,
        10200400U,
        10690002U,
        10180101U,
        10350101U,
        10420901U
      };

            /// <summary>
            /// Defines the MobHP.
            /// </summary>
            private uint[] MobHP = new uint[6]
      {
        0U,
        450U,
        500U,
        600U,
        700U,
        1000U
      };

            /// <summary>
            /// Defines the mob.
            /// </summary>
            public ActorMob mob;

            /// <summary>
            /// Defines the x.
            /// </summary>
            private short x;

            /// <summary>
            /// Defines the y.
            /// </summary>
            private short y;

            /// <summary>
            /// Initializes a new instance of the <see cref="SummobLemuresBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="x">The x<see cref="byte"/>.</param>
            /// <param name="y">The y<see cref="byte"/>.</param>
            public SummobLemuresBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, byte x, byte y)
        : base(skill, actor, nameof(SummobLemures), int.MaxValue)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.x = SagaLib.Global.PosX8to16(x, map.Width);
                this.y = SagaLib.Global.PosY8to16(y, map.Height);
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.mob = map.SpawnMob(this.MobID[(int)skill.skill.Level], this.x, this.y, (short)2500, actor);
                uint num = this.MobHP[(int)skill.skill.Level];
                if (actor.type == ActorType.PC)
                {
                    ActorPC actorPc = (ActorPC)actor;
                    uint key1 = 961;
                    if (actorPc.Skills2.ContainsKey(key1))
                        num += (uint)actorPc.Skills2[key1].Level * 50U;
                    else if (actorPc.SkillsReserve.ContainsKey(key1))
                        num += (uint)actorPc.SkillsReserve[key1].Level * 50U;
                    uint key2 = 963;
                    if (actorPc.Skills2.ContainsKey(key2))
                    {
                        this.mob.Status.max_matk_skill += (short)((double)actorPc.Skills2[key2].Level * 0.300000011920929 * (double)actor.Status.max_matk);
                        this.mob.Status.min_matk_skill += (short)((double)actorPc.Skills2[key2].Level * 0.300000011920929 * (double)actor.Status.min_matk);
                    }
                    else if (actorPc.SkillsReserve.ContainsKey(key2))
                    {
                        this.mob.Status.max_matk_skill += (short)((double)actorPc.SkillsReserve[key2].Level * 0.300000011920929 * (double)actor.Status.max_matk);
                        this.mob.Status.min_matk_skill += (short)((double)actorPc.SkillsReserve[key2].Level * 0.300000011920929 * (double)actor.Status.min_matk);
                    }
                    uint key3 = 962;
                    if (actorPc.Skills2.ContainsKey(key3))
                    {
                        this.mob.Status.max_atk1_skill += (short)((double)((int)actor.Status.max_atk_ori * (int)actorPc.Skills2[key3].Level) * 0.300000011920929);
                        this.mob.Status.max_atk2_skill += (short)((double)((int)actor.Status.max_atk_ori * (int)actorPc.Skills2[key3].Level) * 0.300000011920929);
                        this.mob.Status.max_atk3_skill += (short)((double)((int)actor.Status.max_atk_ori * (int)actorPc.Skills2[key3].Level) * 0.300000011920929);
                        this.mob.Status.min_atk1_skill += (short)((double)((int)actor.Status.min_atk_ori * (int)actorPc.Skills2[key3].Level) * 0.300000011920929);
                        this.mob.Status.min_atk2_skill += (short)((double)((int)actor.Status.min_atk_ori * (int)actorPc.Skills2[key3].Level) * 0.300000011920929);
                        this.mob.Status.min_atk3_skill += (short)((double)((int)actor.Status.min_atk_ori * (int)actorPc.Skills2[key3].Level) * 0.300000011920929);
                    }
                    else if (actorPc.SkillsReserve.ContainsKey(key3))
                    {
                        this.mob.Status.max_atk1_skill += (short)((double)((int)actor.Status.max_atk_ori * (int)actorPc.SkillsReserve[key3].Level) * 0.300000011920929);
                        this.mob.Status.max_atk2_skill += (short)((double)((int)actor.Status.max_atk_ori * (int)actorPc.SkillsReserve[key3].Level) * 0.300000011920929);
                        this.mob.Status.max_atk3_skill += (short)((double)((int)actor.Status.max_atk_ori * (int)actorPc.SkillsReserve[key3].Level) * 0.300000011920929);
                        this.mob.Status.min_atk1_skill += (short)((double)((int)actor.Status.min_atk_ori * (int)actorPc.SkillsReserve[key3].Level) * 0.300000011920929);
                        this.mob.Status.min_atk2_skill += (short)((double)((int)actor.Status.min_atk_ori * (int)actorPc.SkillsReserve[key3].Level) * 0.300000011920929);
                        this.mob.Status.min_atk3_skill += (short)((double)((int)actor.Status.min_atk_ori * (int)actorPc.SkillsReserve[key3].Level) * 0.300000011920929);
                    }
                }
                this.mob.MaxHP = num;
                this.mob.HP = this.mob.MaxHP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.mob, true);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, (MapEventArgs)null, (SagaDB.Actor.Actor)this.mob, true);
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.mob.ClearTaskAddition();
                map.DeleteActor((SagaDB.Actor.Actor)this.mob);
            }
        }
    }
}
