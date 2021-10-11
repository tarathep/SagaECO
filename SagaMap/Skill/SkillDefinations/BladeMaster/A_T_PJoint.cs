namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="A_T_PJoint" />.
    /// </summary>
    public class A_T_PJoint : ISkill
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
            if (sActor.PossessionTarget == 0U)
                return -23;
            return !dActor.Status.Additions.ContainsKey(nameof(A_T_PJoint)) ? 0 : -24;
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
            int lifetime = 15000 + (int)level * 5000;
            SagaDB.Actor.Actor possesionedActor = (SagaDB.Actor.Actor)Singleton<SkillHandler>.Instance.GetPossesionedActor((ActorPC)sActor);
            A_T_PJoint.PJointBuff pjointBuff = new A_T_PJoint.PJointBuff(args.skill, sActor, possesionedActor, lifetime);
            SkillHandler.ApplyAddition(possesionedActor, (Addition)pjointBuff);
        }

        /// <summary>
        /// Defines the <see cref="PJointBuff" />.
        /// </summary>
        public class PJointBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Initializes a new instance of the <see cref="PJointBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public PJointBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor actor, int lifetime)
        : base(skill, actor, nameof(A_T_PJoint), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.sActor = sActor;
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                float num1 = (float)(0.150000005960464 + (double)skill.skill.Level * 0.0500000007450581);
                int num2 = (int)((double)this.sActor.Status.max_atk_ori * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_atk1"))
                    skill.Variable.Remove("A_T_PJoint_max_atk1");
                skill.Variable.Add("A_T_PJoint_max_atk1", num2);
                actor.Status.max_atk1_skill += (short)num2;
                int num3 = (int)((double)this.sActor.Status.max_atk_ori * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_atk2"))
                    skill.Variable.Remove("A_T_PJoint_max_atk2");
                skill.Variable.Add("A_T_PJoint_max_atk2", num3);
                actor.Status.max_atk2_skill += (short)num3;
                int num4 = (int)((double)this.sActor.Status.max_atk_ori * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_atk3"))
                    skill.Variable.Remove("A_T_PJoint_max_atk3");
                skill.Variable.Add("A_T_PJoint_max_atk3", num4);
                actor.Status.max_atk3_skill += (short)num4;
                int num5 = (int)((double)this.sActor.Status.min_atk_ori * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_atk1"))
                    skill.Variable.Remove("A_T_PJoint_min_atk1");
                skill.Variable.Add("A_T_PJoint_min_atk1", num5);
                actor.Status.min_atk1_skill += (short)num5;
                int num6 = (int)((double)this.sActor.Status.min_atk_ori * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_atk2"))
                    skill.Variable.Remove("A_T_PJoint_min_atk2");
                skill.Variable.Add("A_T_PJoint_min_atk2", num6);
                actor.Status.min_atk2_skill += (short)num6;
                int num7 = (int)((double)this.sActor.Status.min_atk_ori * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_atk3"))
                    skill.Variable.Remove("A_T_PJoint_min_atk3");
                skill.Variable.Add("A_T_PJoint_min_atk3", num7);
                actor.Status.min_atk3_skill += (short)num7;
                int num8 = (int)((double)this.sActor.Status.min_matk * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_min_matk"))
                    skill.Variable.Remove("A_T_PJoint_min_matk");
                skill.Variable.Add("A_T_PJoint_min_matk", num8);
                actor.Status.min_matk_skill += (short)num8;
                int num9 = (int)((double)this.sActor.Status.max_matk * (double)num1);
                if (skill.Variable.ContainsKey("A_T_PJoint_max_matk"))
                    skill.Variable.Remove("A_T_PJoint_max_matk");
                skill.Variable.Add("A_T_PJoint_max_matk", num9);
                actor.Status.max_matk_skill += (short)num9;
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                actor.Status.max_atk1_skill -= (short)skill.Variable["A_T_PJoint_max_atk1"];
                actor.Status.max_atk2_skill -= (short)skill.Variable["A_T_PJoint_max_atk2"];
                actor.Status.max_atk3_skill -= (short)skill.Variable["A_T_PJoint_max_atk3"];
                actor.Status.min_atk1_skill -= (short)skill.Variable["A_T_PJoint_min_atk1"];
                actor.Status.min_atk2_skill -= (short)skill.Variable["A_T_PJoint_min_atk2"];
                actor.Status.min_atk3_skill -= (short)skill.Variable["A_T_PJoint_min_atk3"];
                actor.Status.min_matk_skill -= (short)skill.Variable["A_T_PJoint_min_matk"];
                actor.Status.max_matk_skill -= (short)skill.Variable["A_T_PJoint_max_matk"];
            }
        }
    }
}
