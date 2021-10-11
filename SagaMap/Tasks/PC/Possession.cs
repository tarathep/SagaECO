namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="Possession" />.
    /// </summary>
    public class Possession : MultiRunTask
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Defines the target.
        /// </summary>
        private ActorPC target;

        /// <summary>
        /// Defines the pos.
        /// </summary>
        private PossessionPosition pos;

        /// <summary>
        /// Defines the comment.
        /// </summary>
        private string comment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Possession"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="target">The target<see cref="ActorPC"/>.</param>
        /// <param name="position">The position<see cref="PossessionPosition"/>.</param>
        /// <param name="comment">The comment<see cref="string"/>.</param>
        /// <param name="reduce">The reduce<see cref="int"/>.</param>
        public Possession(MapClient client, ActorPC target, PossessionPosition position, string comment, int reduce)
        {
            this.dueTime = 30000 - reduce * 1000;
            this.period = 30000 - reduce * 1000;
            this.client = client;
            this.target = target;
            this.pos = position;
            this.comment = comment;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                this.client.Character.Buff.憑依準備 = false;
                this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.client.Character, true);
                this.client.PossessionPerform(this.target, this.pos, this.comment);
                if (this.client.Character.Tasks.ContainsKey(nameof(Possession)))
                    this.client.Character.Tasks.Remove(nameof(Possession));
                this.Deactivate();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
