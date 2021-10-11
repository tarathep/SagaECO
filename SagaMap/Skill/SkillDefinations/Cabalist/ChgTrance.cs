namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    using SagaDB.Actor;
    using SagaDB.Skill;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ChgTrance" />.
    /// </summary>
    public class ChgTrance : ISkill
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
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(ChgTrance), lifetime);
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
            ActorPC actor1 = (ActorPC)actor;
            switch (skill.skill.Level)
            {
                case 1:
                    this.AddSkill(actor1, 952U, (byte)1);
                    this.AddSkill(actor1, 3282U, (byte)1);
                    break;
                case 2:
                    this.AddSkill(actor1, 6309U, (byte)1);
                    this.AddSkill(actor1, 6303U, (byte)1);
                    Singleton<SkillHandler>.Instance.TranceMob((SagaDB.Actor.Actor)actor1, 10136901U);
                    break;
                case 3:
                    this.AddSkill(actor1, 3279U, (byte)1);
                    this.AddSkill(actor1, 3280U, (byte)1);
                    this.AddSkill(actor1, 7738U, (byte)1);
                    Singleton<SkillHandler>.Instance.TranceMob((SagaDB.Actor.Actor)actor1, 10410000U);
                    break;
            }
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            ActorPC actor1 = (ActorPC)actor;
            switch (skill.skill.Level)
            {
                case 1:
                    this.DelSkill(actor1, 952U);
                    this.DelSkill(actor1, 3282U);
                    break;
                case 2:
                    this.DelSkill(actor1, 6309U);
                    this.DelSkill(actor1, 6303U);
                    break;
                case 3:
                    this.DelSkill(actor1, 3279U);
                    this.DelSkill(actor1, 3280U);
                    this.DelSkill(actor1, 7738U);
                    break;
            }
            Singleton<SkillHandler>.Instance.TranceMob((SagaDB.Actor.Actor)actor1, 0U);
        }

        /// <summary>
        /// The AddSkill.
        /// </summary>
        /// <param name="actor">The actor<see cref="ActorPC"/>.</param>
        /// <param name="SkillID">The SkillID<see cref="uint"/>.</param>
        /// <param name="lv">The lv<see cref="byte"/>.</param>
        private void AddSkill(ActorPC actor, uint SkillID, byte lv)
        {
            SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(SkillID, lv);
            skill.NoSave = true;
            actor.Skills.Add(skill.ID, skill);
        }

        /// <summary>
        /// The DelSkill.
        /// </summary>
        /// <param name="actor">The actor<see cref="ActorPC"/>.</param>
        /// <param name="SkillID">The SkillID<see cref="uint"/>.</param>
        private void DelSkill(ActorPC actor, uint SkillID)
        {
            SagaDB.Skill.Skill skill = actor.Skills.Cast<SagaDB.Skill.Skill>().Where<SagaDB.Skill.Skill>((Func<SagaDB.Skill.Skill, bool>)(x => (int)x.ID == (int)SkillID)).First<SagaDB.Skill.Skill>();
            actor.Skills.Remove(skill.ID);
        }
    }
}
