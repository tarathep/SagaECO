namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MagicBarrier" />.
    /// </summary>
    public class MagicBarrier : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="MagicBarrier"/> class.
        /// </summary>
        public MagicBarrier()
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagicBarrier"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public MagicBarrier(bool MobUse)
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
                    lifetime = 300000;
                    break;
                case 2:
                    lifetime = 250000;
                    break;
                case 3:
                    lifetime = 200000;
                    break;
                case 4:
                    lifetime = 150000;
                    break;
                case 5:
                    lifetime = 100000;
                    break;
            }
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)100, true);
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor actor in actorsArea)
            {
                if (actor.type == ActorType.PC)
                {
                    SkillHandler.RemoveAddition(actor, "DevineBarrier");
                    DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, nameof(MagicBarrier), lifetime);
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
            int num1 = 0;
            int num2 = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    num1 = 15;
                    num2 = 5;
                    break;
                case 2:
                    num1 = 20;
                    num2 = 10;
                    break;
                case 3:
                    num1 = 25;
                    num2 = 10;
                    break;
                case 4:
                    num1 = 30;
                    num2 = 15;
                    break;
                case 5:
                    num1 = 35;
                    num2 = 15;
                    break;
            }
            if (skill.Variable.ContainsKey("MDef"))
                skill.Variable.Remove("MDef");
            skill.Variable.Add("MDef", num1);
            actor.Status.mdef_skill += (short)num1;
            if (skill.Variable.ContainsKey("MDefAdd"))
                skill.Variable.Remove("MDefAdd");
            skill.Variable.Add("MDefAdd", num2);
            actor.Status.mdef_add_skill += (short)num2;
            actor.Buff.魔法防御力上昇 = true;
            actor.Buff.魔法防御率上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num1 = skill.Variable["MDef"];
            actor.Status.mdef_skill -= (short)num1;
            int num2 = skill.Variable["MDefAdd"];
            actor.Status.mdef_add_skill -= (short)num2;
            actor.Buff.魔法防御力上昇 = false;
            actor.Buff.魔法防御率上昇 = false;
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
