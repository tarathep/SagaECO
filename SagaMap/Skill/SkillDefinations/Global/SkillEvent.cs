namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;
    using System;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="SkillEvent" />.
    /// </summary>
    public class SkillEvent : ISkill
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
            return MapClient.FromActorPC(pc).scriptThread != null ? -59 : 0;
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
            args.dActor = 0U;
            args.showEffect = false;
            SkillEvent.Parameter parameter = new SkillEvent.Parameter();
            parameter.sActor = sActor;
            parameter.dActor = dActor;
            parameter.args = args;
            parameter.level = level;
            MapClient.FromActorPC((ActorPC)sActor).scriptThread = new Thread(new ParameterizedThreadStart(this.Run));
            MapClient.FromActorPC((ActorPC)sActor).scriptThread.Start((object)parameter);
        }

        /// <summary>
        /// The Run.
        /// </summary>
        /// <param name="par">The par<see cref="object"/>.</param>
        private void Run(object par)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                this.RunScript((SkillEvent.Parameter)par);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
            MapClient.FromActorPC((ActorPC)((SkillEvent.Parameter)par).sActor).scriptThread = (Thread)null;
        }

        /// <summary>
        /// The RunScript.
        /// </summary>
        /// <param name="para">The para<see cref="SkillEvent.Parameter"/>.</param>
        protected virtual void RunScript(SkillEvent.Parameter para)
        {
        }

        /// <summary>
        /// Defines the <see cref="Parameter" />.
        /// </summary>
        public class Parameter
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            public SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Defines the dActor.
            /// </summary>
            public SagaDB.Actor.Actor dActor;

            /// <summary>
            /// Defines the args.
            /// </summary>
            public SkillArg args;

            /// <summary>
            /// Defines the level.
            /// </summary>
            public byte level;
        }
    }
}
