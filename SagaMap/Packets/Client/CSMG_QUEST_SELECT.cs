namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_QUEST_SELECT" />.
    /// </summary>
    public class CSMG_QUEST_SELECT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_QUEST_SELECT"/> class.
        /// </summary>
        public CSMG_QUEST_SELECT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the QuestID.
        /// </summary>
        public uint QuestID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_QUEST_SELECT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnQuestSelect(this);
        }
    }
}
