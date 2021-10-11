namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_NPC_JOB_SWITCH" />.
    /// </summary>
    public class CSMG_NPC_JOB_SWITCH : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_NPC_JOB_SWITCH"/> class.
        /// </summary>
        public CSMG_NPC_JOB_SWITCH()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Unknown.
        /// </summary>
        public int Unknown
        {
            get
            {
                return this.GetInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the ItemUseCount.
        /// </summary>
        public uint ItemUseCount
        {
            get
            {
                return this.GetUInt((ushort)6);
            }
        }

        /// <summary>
        /// Gets the Skills.
        /// </summary>
        public ushort[] Skills
        {
            get
            {
                byte num = this.GetByte((ushort)10);
                ushort[] numArray = new ushort[(int)num];
                for (int index = 0; index < (int)num; ++index)
                    numArray[index] = this.GetUShort((ushort)(11 + index * 2));
                return numArray;
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_NPC_JOB_SWITCH();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnNPCJobSwitch(this);
        }
    }
}
