namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="SpellCancel" />.
    /// </summary>
    internal class SpellCancel : ISkill
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
            MapClient client = ((PCEventHandler)sActor.e).Client;
            client.SendSkillDummy(args.skill.ID, args.skill.Level);
            if (!client.Character.Tasks.ContainsKey("SkillCast") || !client.Character.Tasks["SkillCast"].Activated())
                return;
            client.Character.Tasks["SkillCast"].Deactivate();
            client.Character.Tasks.Remove("SkillCast");
        }
    }
}
