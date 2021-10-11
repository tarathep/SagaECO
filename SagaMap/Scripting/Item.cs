namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="Item" />.
    /// </summary>
    public class Item : Event
    {
        /// <summary>
        /// Defines the Handler.
        /// </summary>
        public Item.OnEventHandler Handler;

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="eventID">The eventID<see cref="uint"/>.</param>
        /// <param name="handler">The handler<see cref="Item.OnEventHandler"/>.</param>
        protected void Init(uint eventID, Item.OnEventHandler handler)
        {
            Item obj = new Item();
            obj.EventID = eventID;
            obj.Handler += handler;
            if (Singleton<ScriptManager>.Instance.Events.ContainsKey(eventID))
                return;
            Singleton<ScriptManager>.Instance.Events.Add(eventID, (Event)obj);
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public override void OnEvent(ActorPC pc)
        {
            if (this.Handler == null)
                return;
            this.Handler(pc);
        }

        /// <summary>
        /// The OnEventHandler.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public delegate void OnEventHandler(ActorPC pc);
    }
}
