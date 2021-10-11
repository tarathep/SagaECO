namespace SagaMap.Skill.SkillDefinations.Marionest
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MarioCancel" />.
    /// </summary>
    public class MarioCancel : ISkill
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
            uint key = 2371;
            float MATKBonus = (float)(1.70000004768372 + 0.300000011920929 * (double)level);
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills2.ContainsKey(key))
                MATKBonus += 0.3f * (float)actorPc.Skills2[key].Level;
            if (actorPc.SkillsReserve.ContainsKey(key))
                MATKBonus += 0.3f * (float)actorPc.SkillsReserve[key].Level;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Neutral, MATKBonus);
            int num = 30 + 10 * (int)level;
            if (SagaLib.Global.Random.Next(0, 99) < num && dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                if (pc.Marionette != null)
                    MapClient.FromActorPC(pc).MarionetteDeactivate();
            }
            int rate = 40 + 5 * (int)level;
            if (dActor.type != ActorType.MOB || !Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.硬直, rate))
                return;
            硬直 硬直 = new 硬直(args.skill, sActor, 4000 + 1000 * (int)level);
            SkillHandler.ApplyAddition(dActor, (Addition)硬直);
        }
    }
}
