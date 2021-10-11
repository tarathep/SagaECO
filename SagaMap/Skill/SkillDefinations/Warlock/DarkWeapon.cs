namespace SagaMap.Skill.SkillDefinations.Warlock
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DarkWeapon" />.
    /// </summary>
    public class DarkWeapon : ISkill
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
            SkillHandler.RemoveAddition(dActor, "HolyWeapon");
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(DarkWeapon), 50000);
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
            (attackElements = actor.AttackElements)[Elements.Dark] = attackElements[Elements.Dark] + num8;
            if (skill.Variable.ContainsKey("DarkWeaponATK1"))
                skill.Variable.Remove("DarkWeaponATK1");
            skill.Variable.Add("DarkWeaponATK1", num2);
            actor.Status.min_atk1_skill += (short)num2;
            if (skill.Variable.ContainsKey("DarkWeaponATK2"))
                skill.Variable.Remove("DarkWeaponATK2");
            skill.Variable.Add("DarkWeaponATK2", num3);
            actor.Status.max_atk1_skill += (short)num3;
            if (skill.Variable.ContainsKey("DarkWeaponATK3"))
                skill.Variable.Remove("DarkWeaponATK3");
            skill.Variable.Add("DarkWeaponATK3", num4);
            actor.Status.min_atk2_skill += (short)num4;
            if (skill.Variable.ContainsKey("DarkWeaponATK4"))
                skill.Variable.Remove("DarkWeaponATK4");
            skill.Variable.Add("DarkWeaponATK4", num5);
            actor.Status.max_atk2_skill += (short)num5;
            if (skill.Variable.ContainsKey("DarkWeaponATK5"))
                skill.Variable.Remove("DarkWeaponATK5");
            skill.Variable.Add("DarkWeaponATK5", num6);
            actor.Status.min_atk3_skill += (short)num6;
            if (skill.Variable.ContainsKey("DarkWeaponATK6"))
                skill.Variable.Remove("DarkWeaponATK6");
            skill.Variable.Add("DarkWeaponATK6", num7);
            actor.Status.max_atk3_skill += (short)num7;
            actor.Buff.武器の闇属性上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num1 = skill.Variable["DarkWeaponATK1"];
            actor.Status.min_atk1_skill -= (short)num1;
            int num2 = skill.Variable["DarkWeaponATK2"];
            actor.Status.max_atk1_skill -= (short)num2;
            int num3 = skill.Variable["DarkWeaponATK3"];
            actor.Status.min_atk2_skill -= (short)num3;
            int num4 = skill.Variable["DarkWeaponATK4"];
            actor.Status.max_atk2_skill -= (short)num4;
            int num5 = skill.Variable["DarkWeaponATK5"];
            actor.Status.min_atk3_skill -= (short)num5;
            int num6 = skill.Variable["DarkWeaponATK6"];
            actor.Status.max_atk3_skill -= (short)num6;
            int num7 = skill.Variable["WeaponEle"];
            Dictionary<Elements, int> attackElements;
            (attackElements = actor.AttackElements)[Elements.Dark] = attackElements[Elements.Dark] - num7;
            actor.Buff.武器の闇属性上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
