namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PetPlantDefupSelf" />.
    /// </summary>
    public class PetPlantDefupSelf : ISkill
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
            int lifetime = 90000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, sActor, nameof(PetPlantDefupSelf), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(sActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int level = (int)skill.skill.Level;
            int num1 = 2 + 3 * level;
            if (skill.Variable.ContainsKey("PetPlantDefupSelf_def"))
                skill.Variable.Remove("PetPlantDefupSelf_def");
            skill.Variable.Add("PetPlantDefupSelf_def", num1);
            actor.Status.def_skill += (short)num1;
            int num2 = 2 + 3 * level;
            if (skill.Variable.ContainsKey("PetPlantDefupSelf_def_add"))
                skill.Variable.Remove("PetPlantDefupSelf_def_add");
            skill.Variable.Add("PetPlantDefupSelf_def_add", num2);
            actor.Status.def_add_skill += (short)num2;
            int num3 = 2 + 3 * level;
            if (skill.Variable.ContainsKey("PetPlantDefupSelf_mdef"))
                skill.Variable.Remove("PetPlantDefupSelf_mdef");
            skill.Variable.Add("PetPlantDefupSelf_mdef", num3);
            actor.Status.mdef_skill += (short)num3;
            int num4 = 2 + 3 * level;
            if (skill.Variable.ContainsKey("PetPlantDefupSelf_mdef_add"))
                skill.Variable.Remove("PetPlantDefupSelf_mdef_add");
            skill.Variable.Add("PetPlantDefupSelf_mdef_add", num4);
            actor.Status.mdef_add_skill += (short)num4;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["PetPlantDefupSelf_def"];
            actor.Status.def_add_skill -= (short)skill.Variable["PetPlantDefupSelf_def_add"];
            actor.Status.mdef_skill -= (short)skill.Variable["PetPlantDefupSelf_mdef"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["PetPlantDefupSelf_mdef_add"];
        }
    }
}
