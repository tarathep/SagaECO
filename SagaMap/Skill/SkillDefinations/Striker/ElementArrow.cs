namespace SagaMap.Skill.SkillDefinations.Striker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ElementArrow" />.
    /// </summary>
    public class ElementArrow : ISkill
    {
        /// <summary>
        /// Defines the ArrowElement.
        /// </summary>
        private Elements ArrowElement = Elements.Neutral;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementArrow"/> class.
        /// </summary>
        /// <param name="e">The e<see cref="Elements"/>.</param>
        public ElementArrow(Elements e)
        {
            this.ArrowElement = e;
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
            int lifetime = 18750000 * (int)level;
            args.argType = SkillArg.ArgType.Attack;
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, this.ArrowElement, 1f);
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(ElementArrow), lifetime);
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
            if (!actor.Elements.ContainsKey(this.ArrowElement))
                return;
            Dictionary<Elements, int> elements;
            Elements arrowElement;
            (elements = actor.Elements)[arrowElement = this.ArrowElement] = elements[arrowElement] + 60;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            if (!actor.Elements.ContainsKey(this.ArrowElement))
                return;
            Dictionary<Elements, int> elements;
            Elements arrowElement;
            (elements = actor.Elements)[arrowElement = this.ArrowElement] = elements[arrowElement] - 60;
        }
    }
}
