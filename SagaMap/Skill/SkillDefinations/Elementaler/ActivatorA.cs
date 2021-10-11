namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaDB.Skill;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ActivatorA" />.
    /// </summary>
    internal class ActivatorA : MultiRunTask
    {
        /// <summary>
        /// Defines the count.
        /// </summary>
        private int count = 0;

        /// <summary>
        /// Defines the countMax.
        /// </summary>
        private int countMax = 3;

        /// <summary>
        /// Defines the factor.
        /// </summary>
        private float factor = 1f;

        /// <summary>
        /// Defines the SkillFireBolt.
        /// </summary>
        private SkillArg SkillFireBolt = new SkillArg();

        /// <summary>
        /// Defines the SkillBody.
        /// </summary>
        private ActorSkill SkillBody;

        /// <summary>
        /// Defines the Arg.
        /// </summary>
        private SkillArg Arg;

        /// <summary>
        /// Defines the AimActor.
        /// </summary>
        private SagaDB.Actor.Actor AimActor;

        /// <summary>
        /// Defines the map.
        /// </summary>
        private Map map;

        /// <summary>
        /// Defines the sActor.
        /// </summary>
        private SagaDB.Actor.Actor sActor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatorA"/> class.
        /// </summary>
        /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public ActivatorA(ActorSkill actor, SagaDB.Actor.Actor dActor, SagaDB.Actor.Actor sActor, SkillArg args, byte level)
        {
            this.dueTime = 500;
            this.period = 1000;
            this.AimActor = dActor;
            this.Arg = args;
            this.SkillBody = actor;
            this.sActor = sActor;
            this.map = Singleton<MapManager>.Instance.GetMap(this.AimActor.MapID);
            ActorPC actorPc = (ActorPC)sActor;
            List<int> intList = new List<int>();
            intList.Add(3006);
            intList.Add(3013);
            intList.Add(3009);
            intList.Add(3016);
            intList.Add(3011);
            intList.Add(3008);
            int num = 0;
            foreach (uint key in intList)
            {
                if (actorPc.Skills.ContainsKey(key))
                    num += (int)actorPc.Skills[key].BaseData.lv;
                if (actorPc.Skills2.ContainsKey(key))
                    num += (int)actorPc.Skills2[key].BaseData.lv;
            }
            if (num >= 5 && num >= 1)
                this.factor = 1f;
            else if (num >= 8 && num >= 6)
                this.factor = 1.5f;
            else if (num >= 11 && num >= 9)
                this.factor = 2f;
            else if (num >= 35 && num >= 12)
                this.factor = 2.5f;
            switch (level)
            {
                case 1:
                    this.factor *= 1.6f;
                    this.countMax = 4;
                    break;
                case 2:
                    this.factor *= 1.7f;
                    this.countMax = 5;
                    break;
                case 3:
                    this.factor *= 1.8f;
                    this.countMax = 5;
                    break;
                case 4:
                    this.factor *= 1.9f;
                    this.countMax = 6;
                    break;
                case 5:
                    this.factor *= 2f;
                    this.countMax = 7;
                    break;
            }
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            short num = Map.Distance((SagaDB.Actor.Actor)this.SkillBody, this.AimActor);
            if (this.count <= this.countMax)
            {
                if (num <= (short)600)
                {
                    this.SkillFireBolt.skill = Singleton<SkillFactory>.Instance.GetSkill(3009U, (byte)1);
                    this.SkillFireBolt.argType = SkillArg.ArgType.Active;
                    this.SkillFireBolt.sActor = this.SkillBody.ActorID;
                    this.SkillFireBolt.dActor = this.AimActor.ActorID;
                    this.SkillFireBolt.x = byte.MaxValue;
                    this.SkillFireBolt.y = byte.MaxValue;
                    Singleton<SkillHandler>.Instance.MagicAttack(this.sActor, this.AimActor, this.SkillFireBolt, Elements.Fire, this.factor);
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.SkillFireBolt, (SagaDB.Actor.Actor)this.SkillBody, true);
                    if (this.SkillFireBolt.flag.Contains(AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT | AttackFlag.DIE))
                    {
                        this.map.DeleteActor((SagaDB.Actor.Actor)this.SkillBody);
                        this.Deactivate();
                    }
                }
                ++this.count;
            }
            else
            {
                this.map.DeleteActor((SagaDB.Actor.Actor)this.SkillBody);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
