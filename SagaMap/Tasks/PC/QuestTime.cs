namespace SagaMap.Tasks.PC
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="QuestTime" />.
    /// </summary>
    public class QuestTime : MultiRunTask
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestTime"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        public QuestTime(MapClient client)
        {
            this.dueTime = 60000;
            this.period = 60000;
            this.client = client;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            if (this.client.Character.Quest != null)
                this.client.SendQuestTime();
            ClientManager.LeaveCriticalArea();
        }
    }
}
