namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AtkFly" />.
    /// </summary>
    public class AtkFly : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)sActor, dActor) ? 0 : -14;
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
            float ATKBonus = (float)(1.39999997615814 + 0.100000001490116 * (double)level);
            int num = 1;
            if (dActor.type == ActorType.MOB)
            {
                ActorMob actorMob = (ActorMob)dActor;
                if (actorMob.BaseData.mobType == MobType.BIRD || actorMob.BaseData.mobType == MobType.BIRD_BOSS || (actorMob.BaseData.mobType == MobType.BIRD_BOSS_SKILL || actorMob.BaseData.mobType == MobType.BIRD_NOTOUCH) || actorMob.BaseData.mobType == MobType.BIRD_SPBOSS_SKILL || actorMob.BaseData.mobType == MobType.BIRD_UNITE)
                    num = 2;
            }
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            for (int index = 0; index < num; ++index)
                dActor1.Add(dActor);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
