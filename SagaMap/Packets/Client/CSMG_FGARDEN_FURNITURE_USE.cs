namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_FGARDEN_FURNITURE_USE" />.
    /// </summary>
    public class CSMG_FGARDEN_FURNITURE_USE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_FGARDEN_FURNITURE_USE"/> class.
        /// </summary>
        public CSMG_FGARDEN_FURNITURE_USE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the ActorID.
        /// </summary>
        public uint ActorID
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
            return (Packet)new CSMG_FGARDEN_FURNITURE_USE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnFGardenFurnitureUse(this);
        }
    }
}
