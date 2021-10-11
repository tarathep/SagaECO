namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Zen" />.
    /// </summary>
    public class Zen : ISkill
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
            if (sActor.type != ActorType.PC)
                return;
            SagaDB.Actor.Actor possesionedActor = (SagaDB.Actor.Actor)Singleton<SkillHandler>.Instance.GetPossesionedActor((ActorPC)sActor);
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, possesionedActor, nameof(Zen), 15000);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(possesionedActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.zenList.Add((ushort)3001);
            actor.Status.zenList.Add((ushort)3281);
            actor.Status.zenList.Add((ushort)3127);
            actor.Status.zenList.Add((ushort)3126);
            actor.Status.zenList.Add((ushort)3169);
            actor.Status.zenList.Add((ushort)3293);
            actor.Status.zenList.Add((ushort)3312);
            actor.Status.zenList.Add((ushort)3315);
            actor.Status.zenList.Add((ushort)3006);
            actor.Status.zenList.Add((ushort)3029);
            actor.Status.zenList.Add((ushort)3041);
            actor.Status.zenList.Add((ushort)3017);
            actor.Status.zenList.Add((ushort)3020);
            actor.Status.zenList.Add((ushort)3044);
            actor.Status.zenList.Add((ushort)3032);
            actor.Status.zenList.Add((ushort)3009);
            actor.Status.zenList.Add((ushort)3013);
            actor.Status.zenList.Add((ushort)3036);
            actor.Status.zenList.Add((ushort)3049);
            actor.Status.zenList.Add((ushort)3025);
            actor.Status.zenList.Add((ushort)3261);
            actor.Status.zenList.Add((ushort)3260);
            actor.Status.zenList.Add((ushort)3306);
            actor.Status.zenList.Add((ushort)3296);
            actor.Status.zenList.Add((ushort)3318);
            actor.Status.zenList.Add((ushort)3319);
            actor.Status.zenList.Add((ushort)3073);
            actor.Status.zenList.Add((ushort)3078);
            actor.Status.zenList.Add((ushort)3266);
            actor.Status.zenList.Add((ushort)3323);
            actor.Status.zenList.Add((ushort)3083);
            actor.Status.zenList.Add((ushort)3085);
            actor.Status.zenList.Add((ushort)3310);
            actor.Status.zenList.Add((ushort)3332);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.zenList.Clear();
        }
    }
}
