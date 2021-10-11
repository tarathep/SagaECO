namespace SagaMap.Skill.SkillDefinations.Striker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PetBirdAtkRowCircle2" />.
    /// </summary>
    public class PetBirdAtkRowCircle2 : ISkill
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
            int lifetime = 30000;
            foreach (SagaDB.Actor.Actor actor in Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)200, false))
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, actor))
                {
                    DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, "PetBirdAtkRowCircle", lifetime);
                    defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
                    defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
                    SkillHandler.ApplyAddition(actor, (Addition)defaultBuff);
                    Confuse confuse = new Confuse(args.skill, actor, lifetime);
                    SkillHandler.ApplyAddition(actor, (Addition)confuse);
                }
            }
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num = -(6 * (int)skill.skill.Level);
            if (skill.Variable.ContainsKey("PetBirdAtkRowCircle_hit_melee"))
                skill.Variable.Remove("PetBirdAtkRowCircle_hit_melee");
            skill.Variable.Add("PetBirdAtkRowCircle_hit_melee", num);
            actor.Status.hit_melee_skill += (short)num;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_melee_skill -= (short)skill.Variable["PetBirdAtkRowCircle_hit_melee"];
        }
    }
}
