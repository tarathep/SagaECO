namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ElementRise" />.
    /// </summary>
    public class ElementRise : ISkill
    {
        /// <summary>
        /// Defines the element.
        /// </summary>
        public Elements element;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementRise"/> class.
        /// </summary>
        /// <param name="e">The e<see cref="Elements"/>.</param>
        public ElementRise(Elements e)
        {
            this.element = e;
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
            int lifetime = 5000 + 1000 * (int)level;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, this.element.ToString() + "Rise", lifetime);
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
            int num = 10 * (int)skill.skill.Level;
            if (skill.Variable.ContainsKey("ElementRise_Element"))
                skill.Variable.Remove("ElementRise_Element");
            skill.Variable.Add("ElementRise_Element", actor.Elements[this.element]);
            Dictionary<Elements, int> elements;
            Elements element;
            (elements = actor.Elements)[element = this.element] = elements[element] + num;
            if (actor.Elements[this.element] <= 100)
                return;
            actor.Elements[this.element] = 100;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Elements[this.element] = (int)(short)skill.Variable["ElementRise_Element"];
        }
    }
}
