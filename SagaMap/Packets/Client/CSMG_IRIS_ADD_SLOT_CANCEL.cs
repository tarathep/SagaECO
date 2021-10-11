namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_IRIS_ADD_SLOT_CANCEL" />.
    /// </summary>
    public class CSMG_IRIS_ADD_SLOT_CANCEL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_IRIS_ADD_SLOT_CANCEL"/> class.
        /// </summary>
        public CSMG_IRIS_ADD_SLOT_CANCEL()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_IRIS_ADD_SLOT_CANCEL();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnIrisAddSlotCancel(this);
        }
    }
}
