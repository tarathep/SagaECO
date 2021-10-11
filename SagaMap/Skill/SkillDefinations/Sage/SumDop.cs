namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SumDop" />.
    /// </summary>
    public class SumDop : ISkill
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
            int lifetime = 300000;
            SumDop.SumDopBuff sumDopBuff = new SumDop.SumDopBuff(args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, (Addition)sumDopBuff);
        }

        /// <summary>
        /// Defines the <see cref="SumDopBuff" />.
        /// </summary>
        public class SumDopBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the shadow.
            /// </summary>
            private ActorShadow shadow;

            /// <summary>
            /// Initializes a new instance of the <see cref="SumDopBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public SumDopBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
        : base(skill, actor, nameof(SumDop), lifetime)
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
                this.shadow = this.SummonMe((ActorPC)actor, skill.skill.Level);
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.shadow.ClearTaskAddition();
                map.DeleteActor((SagaDB.Actor.Actor)this.shadow);
            }

            /// <summary>
            /// The SummonMe.
            /// </summary>
            /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            /// <returns>The <see cref="ActorShadow"/>.</returns>
            public ActorShadow SummonMe(ActorPC pc, byte level)
            {
                ActorShadow actorShadow = new ActorShadow(pc);
                Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
                actorShadow.Name = pc.Name;
                actorShadow.MapID = pc.MapID;
                actorShadow.X = pc.X;
                actorShadow.Y = pc.Y;
                actorShadow.MaxHP = (uint)((double)pc.MaxHP * (0.100000001490116 + 0.200000002980232 * (double)level));
                actorShadow.HP = actorShadow.MaxHP;
                actorShadow.Status.max_matk_skill += (short)((double)actorShadow.Status.max_matk * 1.5);
                actorShadow.Status.min_matk_skill += (short)((double)actorShadow.Status.min_matk * 1.5);
                actorShadow.Speed = pc.Speed;
                actorShadow.BaseData.mobSize = 1.5f;
                PetEventHandler petEventHandler = new PetEventHandler((ActorPet)actorShadow);
                actorShadow.e = (ActorEventHandler)petEventHandler;
                petEventHandler.AI.Mode = new AIMode(1);
                switch (this.skill.Level)
                {
                    case 1:
                        petEventHandler.AI.Mode.EventAttacking.Add(3281U, 100);
                        break;
                    case 2:
                        petEventHandler.AI.Mode.EventAttacking.Add(3281U, 50);
                        petEventHandler.AI.Mode.EventAttacking.Add(3127U, 50);
                        break;
                    case 3:
                        petEventHandler.AI.Mode.EventAttacking.Add(3281U, 30);
                        petEventHandler.AI.Mode.EventAttacking.Add(3127U, 30);
                        petEventHandler.AI.Mode.EventAttacking.Add(3291U, 40);
                        break;
                }
                petEventHandler.AI.Mode.EventAttackingSkillRate = 100;
                petEventHandler.AI.Master = (SagaDB.Actor.Actor)pc;
                map.RegisterActor((SagaDB.Actor.Actor)actorShadow);
                actorShadow.invisble = false;
                map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorShadow);
                map.SendVisibleActorsToActor((SagaDB.Actor.Actor)actorShadow);
                petEventHandler.AI.Start();
                return actorShadow;
            }
        }
    }
}
