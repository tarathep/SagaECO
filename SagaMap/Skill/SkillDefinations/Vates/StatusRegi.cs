namespace SagaMap.Skill.SkillDefinations.Vates
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="StatusRegi" />.
    /// </summary>
    public class StatusRegi : ISkill
    {
        /// <summary>
        /// Defines the StatusName.
        /// </summary>
        private string StatusName;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusRegi"/> class.
        /// </summary>
        /// <param name="StatusName">The StatusName<see cref="string"/>.</param>
        public StatusRegi(string StatusName)
        {
            this.StatusName = StatusName;
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
            int lifetime = 40000 + 20000 * (int)level;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, this.StatusName + "Regi", lifetime);
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
            this.ChangeBuffIcon(actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            this.ChangeBuffIcon(actor, false);
        }

        /// <summary>
        /// The ChangeBuffIcon.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="OnOff">The OnOff<see cref="bool"/>.</param>
        private void ChangeBuffIcon(SagaDB.Actor.Actor actor, bool OnOff)
        {
            switch (this.StatusName)
            {
                case "Sleep":
                    actor.Buff.SleepResist = OnOff;
                    break;
                case "Poison":
                    actor.Buff.ParalysisResist = OnOff;
                    break;
                case "Silence":
                    actor.Buff.SilenceResist = OnOff;
                    break;
                case "Stone":
                    actor.Buff.StoneResist = OnOff;
                    break;
                case "Confuse":
                    actor.Buff.ConfuseResist = OnOff;
                    break;
                case "鈍足":
                    actor.Buff.SpeedDownResist = OnOff;
                    break;
                case "Frosen":
                    actor.Buff.FrosenResist = OnOff;
                    break;
            }
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
