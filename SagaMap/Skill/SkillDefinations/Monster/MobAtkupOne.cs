namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MobAtkupOne" />.
    /// </summary>
    public class MobAtkupOne : ISkill
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
            int lifetime = 60000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(MobAtkupOne), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(dActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill += (short)10;
            actor.Status.max_atk2_skill += (short)10;
            actor.Status.max_atk3_skill += (short)10;
            actor.Status.min_atk1_skill += (short)10;
            actor.Status.min_atk2_skill += (short)10;
            actor.Status.min_atk3_skill += (short)10;
            actor.Status.max_matk_skill += (short)10;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)10;
            actor.Status.max_atk2_skill -= (short)10;
            actor.Status.max_atk3_skill -= (short)10;
            actor.Status.min_atk1_skill -= (short)10;
            actor.Status.min_atk2_skill -= (short)10;
            actor.Status.min_atk3_skill -= (short)10;
            actor.Status.max_matk_skill -= (short)10;
        }
    }
}
