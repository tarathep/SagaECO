namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_ELEMENTS" />.
    /// </summary>
    public class SSMG_PLAYER_ELEMENTS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_ELEMENTS"/> class.
        /// </summary>
        public SSMG_PLAYER_ELEMENTS()
        {
            this.data = new byte[32];
            this.offset = (ushort)2;
            this.ID = (ushort)547;
            this.PutByte((byte)7, (ushort)2);
            this.PutByte((byte)7, (ushort)17);
        }

        /// <summary>
        /// Sets the AttackElements.
        /// </summary>
        public Dictionary<Elements, int> AttackElements
        {
            set
            {
                int num = 0;
                foreach (Elements key in value.Keys)
                    this.PutShort((short)value[key], (ushort)(3 + num++ * 2));
            }
        }

        /// <summary>
        /// Sets the DefenceElements.
        /// </summary>
        public Dictionary<Elements, int> DefenceElements
        {
            set
            {
                int num = 0;
                foreach (Elements key in value.Keys)
                    this.PutShort((short)value[key], (ushort)(18 + num++ * 2));
            }
        }
    }
}
