namespace SagaMap.Skill.SkillDefinations.Bard
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AttractMarch" />.
    /// </summary>
    public class AttractMarch : ISkill
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
            return Singleton<SkillHandler>.Instance.isEquipmentRight((SagaDB.Actor.Actor)sActor, ItemType.STRINGS) || sActor.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0 ? 0 : -5;
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
            int lifetime = 1000 * (int)level;
            int rate = 30 + 10 * (int)level;
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, nameof(AttractMarch), rate))
                return;
            if (dActor.type == ActorType.PC)
            {
                dActor.HP = 0U;
                dActor.e.OnDie();
                args.affectedActors.Add(dActor);
                args.Init();
                args.flag[0] = AttackFlag.DIE;
            }
            else
            {
                AttractMarch.AttractMarchBuff attractMarchBuff = new AttractMarch.AttractMarchBuff(args.skill, sActor, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)attractMarchBuff);
            }
        }

        /// <summary>
        /// Defines the <see cref="AttractMarchBuff" />.
        /// </summary>
        public class AttractMarchBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Initializes a new instance of the <see cref="AttractMarchBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public AttractMarchBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, int lifetime)
        : base(skill, dActor, nameof(AttractMarch), lifetime)
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
                if (actor.type != ActorType.MOB)
                    return;
                ((MobEventHandler)actor.e).AI.Master = this.sActor;
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                if (actor.type != ActorType.MOB)
                    return;
                ((MobEventHandler)actor.e).AI.Master = (SagaDB.Actor.Actor)null;
            }
        }
    }
}
