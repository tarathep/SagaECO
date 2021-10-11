namespace SagaMap.Skill.SkillDefinations.Event
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Network.Client;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Kyrie" />.
    /// </summary>
    public class Kyrie : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Kyrie"/> class.
        /// </summary>
        public Kyrie()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kyrie"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public Kyrie(bool MobUse)
        {
            this.MobUse = MobUse;
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
            level = (byte)5;
            int lifetime = 7000 + 1000 * (int)level;
            if (this.MobUse)
                lifetime = 16000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, "MobKyrie", lifetime);
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
            if (this.MobUse)
            {
                skill["MobKyrie"] = 7;
            }
            else
            {
                int[] numArray = new int[6] { 0, 5, 5, 6, 6, 7 };
                skill["MobKyrie"] = numArray[(int)skill.skill.Level];
                if (actor.type == ActorType.PC)
                    MapClient.FromActorPC((ActorPC)actor).SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.SKILL_STATUS_ENTER, (object)skill.skill.Name));
            }
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            if (actor.type != ActorType.PC)
                return;
            MapClient.FromActorPC((ActorPC)actor).SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.SKILL_STATUS_LEAVE, (object)skill.skill.Name));
        }
    }
}
