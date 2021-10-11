namespace SagaMap.Skill.SkillDefinations.Machinery
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="RobotChaff" />.
    /// </summary>
    public class RobotChaff : ISkill
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
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet((SagaDB.Actor.Actor)sActor);
            return pet == null || !Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, "MACHINE_RIDE_ROBOT") ? -53 : 0;
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
            int lifetime = 5000 + 5000 * (int)level;
            foreach (SagaDB.Actor.Actor actor in Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)200, false))
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, actor))
                {
                    RobotChaff.RobotChaffBuff robotChaffBuff = new RobotChaff.RobotChaffBuff(args.skill, actor, lifetime);
                    SkillHandler.ApplyAddition(actor, (Addition)robotChaffBuff);
                }
            }
        }

        /// <summary>
        /// Defines the <see cref="RobotChaffBuff" />.
        /// </summary>
        public class RobotChaffBuff : DefaultBuff
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RobotChaffBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public RobotChaffBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
        : base(skill, actor, nameof(RobotChaffBuff), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                int level = (int)skill.skill.Level;
                int num1 = -(int)((double)actor.Status.hit_melee * 0.100000001490116 * (double)level);
                if (skill.Variable.ContainsKey("RobotChaff_hit_melee"))
                    skill.Variable.Remove("RobotChaff_hit_melee");
                skill.Variable.Add("RobotChaff_hit_melee", num1);
                actor.Status.hit_melee_skill += (short)num1;
                int num2 = -(int)((double)actor.Status.hit_ranged * 0.100000001490116 * (double)level);
                if (skill.Variable.ContainsKey("RobotChaff_hit_ranged"))
                    skill.Variable.Remove("RobotChaff_hit_ranged");
                skill.Variable.Add("RobotChaff_hit_ranged", num2);
                actor.Status.hit_ranged_skill += (short)num2;
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                actor.Status.hit_melee_skill -= (short)skill.Variable["RobotChaff_hit_melee"];
                actor.Status.hit_ranged_skill -= (short)skill.Variable["RobotChaff_hit_ranged"];
            }
        }
    }
}
