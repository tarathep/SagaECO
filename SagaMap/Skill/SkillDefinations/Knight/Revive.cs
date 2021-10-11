namespace SagaMap.Skill.SkillDefinations.Knight
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Revive" />.
    /// </summary>
    public class Revive : ISkill
    {
        /// <summary>
        /// Defines the SkillLv.
        /// </summary>
        private int SkillLv = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Revive"/> class.
        /// </summary>
        /// <param name="level">技能等級(0表示由玩家選擇).</param>
        public Revive(int level)
        {
            this.SkillLv = level;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Revive"/> class.
        /// </summary>
        public Revive()
        {
        }

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
            int lifetime = 30000 * (int)level;
            SagaDB.Actor.Actor actor = dActor;
            if (sActor.type == ActorType.PET)
                actor = (SagaDB.Actor.Actor)((ActorPet)sActor).Owner;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, nameof(Revive), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(actor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num = this.SkillLv == 0 ? (int)skill.skill.Level : this.SkillLv;
            actor.Status.autoReviveRate += (short)(10 * num);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num = this.SkillLv == 0 ? (int)skill.skill.Level : this.SkillLv;
            actor.Status.autoReviveRate -= (short)(10 * num);
        }
    }
}
