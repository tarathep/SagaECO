namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SET_EVENT_AREA" />.
    /// </summary>
    public class SSMG_NPC_SET_EVENT_AREA : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SET_EVENT_AREA"/> class.
        /// </summary>
        public SSMG_NPC_SET_EVENT_AREA()
        {
            this.data = new byte[26];
            this.offset = (ushort)2;
            this.ID = (ushort)3200;
        }

        /// <summary>
        /// Sets the EventID.
        /// </summary>
        public uint EventID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the StartX.
        /// </summary>
        public uint StartX
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the StartY.
        /// </summary>
        public uint StartY
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the EndX.
        /// </summary>
        public uint EndX
        {
            set
            {
                this.PutUInt(value, (ushort)14);
            }
        }

        /// <summary>
        /// Sets the EndY.
        /// </summary>
        public uint EndY
        {
            set
            {
                this.PutUInt(value, (ushort)18);
            }
        }

        /// <summary>
        /// Sets the EffectID.
        /// </summary>
        public uint EffectID
        {
            set
            {
                this.PutUInt(value, (ushort)22);
            }
        }
    }
}
