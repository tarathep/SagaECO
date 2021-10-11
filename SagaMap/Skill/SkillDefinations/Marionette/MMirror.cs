namespace SagaMap.Skill.SkillDefinations.Marionette
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="MMirror" />.
    /// </summary>
    public class MMirror : ISkill
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
            int lifetime = 60000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(MMirror), lifetime);
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
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Slave.Add((SagaDB.Actor.Actor)map.SpawnMob(26160006U, (short)((int)actor.X + SagaLib.Global.Random.Next(-1, 1)), (short)((int)actor.Y + SagaLib.Global.Random.Next(-1, 1)), (short)2500, actor));
            actor.Slave.Add((SagaDB.Actor.Actor)map.SpawnMob(26160006U, (short)((int)actor.X + SagaLib.Global.Random.Next(-1, 1)), (short)((int)actor.Y + SagaLib.Global.Random.Next(-1, 1)), (short)2500, actor));
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            foreach (ActorMob actorMob in actor.Slave.Cast<SagaDB.Actor.Actor>().Where<SagaDB.Actor.Actor>((Func<SagaDB.Actor.Actor, bool>)(a => a.type == ActorType.MOB)).Cast<ActorMob>().Where<ActorMob>((Func<ActorMob, bool>)(m => m.MobID == 26160006U)))
            {
                actorMob.ClearTaskAddition();
                map.DeleteActor((SagaDB.Actor.Actor)actorMob);
            }
        }
    }
}
