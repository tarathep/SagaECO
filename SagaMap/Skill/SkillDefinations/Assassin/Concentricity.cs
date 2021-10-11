namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Concentricity" />.
    /// </summary>
    public class Concentricity : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="Concentricity"/> class.
        /// </summary>
        public Concentricity()
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Concentricity"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public Concentricity(bool MobUse)
        {
            this.MobUse = MobUse;
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
            if (this.MobUse)
                level = (byte)5;
            int lifetime = 0;
            switch (level)
            {
                case 1:
                    lifetime = 12000;
                    break;
                case 2:
                    lifetime = 15000;
                    break;
                case 3:
                    lifetime = 20000;
                    break;
                case 4:
                    lifetime = 25000;
                    break;
                case 5:
                    lifetime = 30000;
                    break;
            }
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(Concentricity), lifetime);
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
            actor.Buff.クリティカル率上昇 = true;
            actor.Status.cri_skill += (short)30;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Buff.クリティカル率上昇 = false;
            actor.Status.cri_skill -= (short)30;
        }
    }
}
