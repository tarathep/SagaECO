namespace SagaMap.Skill.SkillDefinations.Vates
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="HolyWeapon" />.
    /// </summary>
    public class HolyWeapon : ISkill
    {
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
            SkillHandler.RemoveAddition(dActor, "FireWeapon");
            SkillHandler.RemoveAddition(dActor, "WindWeapon");
            SkillHandler.RemoveAddition(dActor, "WaterWeapon");
            SkillHandler.RemoveAddition(dActor, "EarthWeapon");
            SkillHandler.RemoveAddition(dActor, "DarkWeapon");
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(HolyWeapon), 50000);
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
            float num1 = (float)skill.skill.Level * 0.025f;
            int num2 = (int)((double)actor.Status.min_atk_ori * (double)num1);
            int num3 = (int)((double)actor.Status.max_atk_ori * (double)num1);
            int num4 = (int)((double)actor.Status.min_atk_ori * (double)num1);
            int num5 = (int)((double)actor.Status.max_atk_ori * (double)num1);
            int num6 = (int)((double)actor.Status.min_atk_ori * (double)num1);
            int num7 = (int)((double)actor.Status.max_atk_ori * (double)num1);
            int num8 = (int)skill.skill.Level * 5;
            if (skill.Variable.ContainsKey("WeaponEle"))
                skill.Variable.Remove("WeaponEle");
            skill.Variable.Add("WeaponEle", num8);
            Dictionary<Elements, int> attackElements;
            (attackElements = actor.AttackElements)[Elements.Holy] = attackElements[Elements.Holy] + num8;
            if (skill.Variable.ContainsKey("HolyWeaponATK1"))
                skill.Variable.Remove("HolyWeaponATK1");
            skill.Variable.Add("HolyWeaponATK1", num2);
            actor.Status.min_atk1_skill += (short)num2;
            if (skill.Variable.ContainsKey("HolyWeaponATK2"))
                skill.Variable.Remove("HolyWeaponATK2");
            skill.Variable.Add("HolyWeaponATK2", num3);
            actor.Status.max_atk1_skill += (short)num3;
            if (skill.Variable.ContainsKey("HolyWeaponATK3"))
                skill.Variable.Remove("HolyWeaponATK3");
            skill.Variable.Add("HolyWeaponATK3", num4);
            actor.Status.min_atk2_skill += (short)num4;
            if (skill.Variable.ContainsKey("HolyWeaponATK4"))
                skill.Variable.Remove("HolyWeaponATK4");
            skill.Variable.Add("HolyWeaponATK4", num5);
            actor.Status.max_atk2_skill += (short)num5;
            if (skill.Variable.ContainsKey("HolyWeaponATK5"))
                skill.Variable.Remove("HolyWeaponATK5");
            skill.Variable.Add("HolyWeaponATK5", num6);
            actor.Status.min_atk3_skill += (short)num6;
            if (skill.Variable.ContainsKey("HolyWeaponATK6"))
                skill.Variable.Remove("HolyWeaponATK6");
            skill.Variable.Add("HolyWeaponATK6", num7);
            actor.Status.max_atk3_skill += (short)num7;
            actor.Buff.武器の光属性上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num1 = skill.Variable["HolyWeaponATK1"];
            actor.Status.min_atk1_skill -= (short)num1;
            int num2 = skill.Variable["HolyWeaponATK2"];
            actor.Status.max_atk1_skill -= (short)num2;
            int num3 = skill.Variable["HolyWeaponATK3"];
            actor.Status.min_atk2_skill -= (short)num3;
            int num4 = skill.Variable["HolyWeaponATK4"];
            actor.Status.max_atk2_skill -= (short)num4;
            int num5 = skill.Variable["HolyWeaponATK5"];
            actor.Status.min_atk3_skill -= (short)num5;
            int num6 = skill.Variable["HolyWeaponATK6"];
            actor.Status.max_atk3_skill -= (short)num6;
            int num7 = skill.Variable["WeaponEle"];
            Dictionary<Elements, int> attackElements;
            (attackElements = actor.AttackElements)[Elements.Holy] = attackElements[Elements.Holy] - num7;
            actor.Buff.武器の光属性上昇 = false;
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
