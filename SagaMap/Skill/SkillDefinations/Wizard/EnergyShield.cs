namespace SagaMap.Skill.SkillDefinations.Wizard
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="EnergyShield" />.
    /// </summary>
    public class EnergyShield : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnergyShield"/> class.
        /// </summary>
        public EnergyShield()
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnergyShield"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public EnergyShield(bool MobUse)
        {
            this.MobUse = MobUse;
        }

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
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
            int lifetime = 0;
            if (this.MobUse)
                level = (byte)5;
            switch (level)
            {
                case 1:
                    lifetime = 600000;
                    break;
                case 2:
                    lifetime = 500000;
                    break;
                case 3:
                    lifetime = 400000;
                    break;
                case 4:
                    lifetime = 300000;
                    break;
                case 5:
                    lifetime = 200000;
                    break;
            }
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(EnergyShield), lifetime);
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
            int num1 = 0;
            int num2 = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    num1 = 3;
                    num2 = 5;
                    break;
                case 2:
                    num1 = 3;
                    num2 = 10;
                    break;
                case 3:
                    num1 = 6;
                    num2 = 10;
                    break;
                case 4:
                    num1 = 6;
                    num2 = 15;
                    break;
                case 5:
                    num1 = 9;
                    num2 = 15;
                    break;
            }
            if (skill.Variable.ContainsKey("Def"))
                skill.Variable.Remove("Def");
            skill.Variable.Add("Def", num1);
            actor.Status.def_skill += (short)num1;
            if (skill.Variable.ContainsKey("DefAdd"))
                skill.Variable.Remove("DefAdd");
            skill.Variable.Add("DefAdd", num2);
            actor.Status.def_add_skill += (short)num2;
            actor.Buff.防御力上昇 = true;
            actor.Buff.防御率上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num1 = skill.Variable["Def"];
            actor.Status.def_skill -= (short)num1;
            int num2 = skill.Variable["DefAdd"];
            actor.Status.def_add_skill -= (short)num2;
            actor.Buff.防御力上昇 = false;
            actor.Buff.防御率上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The RemoveAddition.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="additionName">The additionName<see cref="string"/>.</param>
        public void RemoveAddition(SagaDB.Actor.Actor actor, string additionName)
        {
            if (!actor.Status.Additions.ContainsKey(additionName))
                return;
            Addition addition = actor.Status.Additions[additionName];
            actor.Status.Additions.Remove(additionName);
            if (addition.Activated)
                addition.AdditionEnd();
            addition.Activated = false;
        }
    }
}
