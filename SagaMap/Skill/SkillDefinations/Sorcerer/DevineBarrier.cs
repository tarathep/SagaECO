namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DevineBarrier" />.
    /// </summary>
    public class DevineBarrier : ISkill
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
            this.RemoveAddition(dActor, "SoulOfEarth");
            this.RemoveAddition(dActor, "SoulOfWater");
            this.RemoveAddition(dActor, "MagicBarrier");
            this.RemoveAddition(dActor, "EnergyBarrier");
            int lifetime = 125000;
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)100, true);
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            ActorPC actorPc = (ActorPC)sActor;
            foreach (SagaDB.Actor.Actor actor in actorsArea)
            {
                if (actor.type == ActorType.PC && actorPc.PossessionTarget == 0U)
                {
                    DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, nameof(DevineBarrier), lifetime);
                    defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
                    defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
                    SkillHandler.ApplyAddition(actor, (Addition)defaultBuff);
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
            short num1 = 0;
            short num2 = 0;
            short num3 = 0;
            short num4 = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    num1 = (short)13;
                    num2 = (short)5;
                    num3 = (short)15;
                    num4 = (short)10;
                    break;
                case 2:
                    num1 = (short)13;
                    num2 = (short)10;
                    num3 = (short)20;
                    num4 = (short)15;
                    break;
                case 3:
                    num1 = (short)16;
                    num2 = (short)10;
                    num3 = (short)25;
                    num4 = (short)15;
                    break;
                case 4:
                    num1 = (short)16;
                    num2 = (short)15;
                    num3 = (short)30;
                    num4 = (short)20;
                    break;
                case 5:
                    num1 = (short)19;
                    num2 = (short)20;
                    num3 = (short)35;
                    num4 = (short)20;
                    break;
            }
            if (skill.Variable.ContainsKey("DevineBarrier_LDef"))
                skill.Variable.Remove("DevineBarrier_LDef");
            skill.Variable.Add("DevineBarrier_LDef", (int)num1);
            if (skill.Variable.ContainsKey("DevineBarrier_RDef"))
                skill.Variable.Remove("DevineBarrier_RDef");
            skill.Variable.Add("DevineBarrier_RDef", (int)num2);
            if (skill.Variable.ContainsKey("DevineBarrier_LMDef"))
                skill.Variable.Remove("DevineBarrier_LMDef");
            skill.Variable.Add("DevineBarrier_LMDef", (int)num3);
            if (skill.Variable.ContainsKey("DevineBarrier_RMDef"))
                skill.Variable.Remove("DevineBarrier_RMDef");
            skill.Variable.Add("DevineBarrier_RMDef", (int)num4);
            actor.Status.def_skill += num1;
            actor.Status.def_add_skill += num2;
            actor.Status.mdef_skill += num3;
            actor.Status.mdef_add_skill += num4;
            actor.Buff.防御率上昇 = true;
            actor.Buff.防御力上昇 = true;
            actor.Buff.魔法防御力上昇 = true;
            actor.Buff.魔法防御力上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["DevineBarrier_LDef"];
            actor.Status.def_add_skill -= (short)skill.Variable["DevineBarrier_RDef"];
            actor.Status.mdef_skill -= (short)skill.Variable["DevineBarrier_LMDef"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["DevineBarrier_RMDef"];
            actor.Buff.防御率上昇 = false;
            actor.Buff.防御力上昇 = false;
            actor.Buff.魔法防御力上昇 = false;
            actor.Buff.魔法防御力上昇 = false;
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
