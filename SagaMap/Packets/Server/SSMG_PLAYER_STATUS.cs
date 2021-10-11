namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_STATUS" />.
    /// </summary>
    public class SSMG_PLAYER_STATUS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_STATUS"/> class.
        /// </summary>
        public SSMG_PLAYER_STATUS()
        {
            this.data = new byte[53];
            this.offset = (ushort)2;
            this.ID = (ushort)530;
        }

        /// <summary>
        /// Sets the StrBase.
        /// </summary>
        public ushort StrBase
        {
            set
            {
                this.PutByte((byte)8, (ushort)2);
                this.PutByte((byte)8, (ushort)19);
                this.PutByte((byte)8, (ushort)36);
                this.PutUShort(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the DexBase.
        /// </summary>
        public ushort DexBase
        {
            set
            {
                this.PutUShort(value, (ushort)5);
            }
        }

        /// <summary>
        /// Sets the IntBase.
        /// </summary>
        public ushort IntBase
        {
            set
            {
                this.PutUShort(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the VitBase.
        /// </summary>
        public ushort VitBase
        {
            set
            {
                this.PutUShort(value, (ushort)9);
            }
        }

        /// <summary>
        /// Sets the AgiBase.
        /// </summary>
        public ushort AgiBase
        {
            set
            {
                this.PutUShort(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the MagBase.
        /// </summary>
        public ushort MagBase
        {
            set
            {
                this.PutUShort(value, (ushort)13);
            }
        }

        /// <summary>
        /// Sets the LukBase.
        /// </summary>
        public ushort LukBase
        {
            set
            {
                this.PutUShort(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the ChaBase.
        /// </summary>
        public ushort ChaBase
        {
            set
            {
                this.PutUShort(value, (ushort)17);
            }
        }

        /// <summary>
        /// Sets the StrRevide.
        /// </summary>
        public short StrRevide
        {
            set
            {
                this.PutShort(value, (ushort)20);
            }
        }

        /// <summary>
        /// Sets the DexRevide.
        /// </summary>
        public short DexRevide
        {
            set
            {
                this.PutShort(value, (ushort)22);
            }
        }

        /// <summary>
        /// Sets the IntRevide.
        /// </summary>
        public short IntRevide
        {
            set
            {
                this.PutShort(value, (ushort)24);
            }
        }

        /// <summary>
        /// Sets the VitRevide.
        /// </summary>
        public short VitRevide
        {
            set
            {
                this.PutShort(value, (ushort)26);
            }
        }

        /// <summary>
        /// Sets the AgiRevide.
        /// </summary>
        public short AgiRevide
        {
            set
            {
                this.PutShort(value, (ushort)28);
            }
        }

        /// <summary>
        /// Sets the MagRevide.
        /// </summary>
        public short MagRevide
        {
            set
            {
                this.PutShort(value, (ushort)30);
            }
        }

        /// <summary>
        /// Sets the LukRevide.
        /// </summary>
        public short LukRevide
        {
            set
            {
                this.PutShort(value, (ushort)32);
            }
        }

        /// <summary>
        /// Sets the ChaRevide.
        /// </summary>
        public short ChaRevide
        {
            set
            {
                this.PutShort(value, (ushort)34);
            }
        }

        /// <summary>
        /// Sets the StrBonus.
        /// </summary>
        public ushort StrBonus
        {
            set
            {
                this.PutUShort(value, (ushort)37);
            }
        }

        /// <summary>
        /// Sets the DexBonus.
        /// </summary>
        public ushort DexBonus
        {
            set
            {
                this.PutUShort(value, (ushort)39);
            }
        }

        /// <summary>
        /// Sets the IntBonus.
        /// </summary>
        public ushort IntBonus
        {
            set
            {
                this.PutUShort(value, (ushort)41);
            }
        }

        /// <summary>
        /// Sets the VitBonus.
        /// </summary>
        public ushort VitBonus
        {
            set
            {
                this.PutUShort(value, (ushort)43);
            }
        }

        /// <summary>
        /// Sets the AgiBonus.
        /// </summary>
        public ushort AgiBonus
        {
            set
            {
                this.PutUShort(value, (ushort)45);
            }
        }

        /// <summary>
        /// Sets the MagBonus.
        /// </summary>
        public ushort MagBonus
        {
            set
            {
                this.PutUShort(value, (ushort)47);
            }
        }

        /// <summary>
        /// Sets the LukBonus.
        /// </summary>
        public ushort LukBonus
        {
            set
            {
                this.PutUShort(value, (ushort)49);
            }
        }

        /// <summary>
        /// Sets the ChaBonus.
        /// </summary>
        public ushort ChaBonus
        {
            set
            {
                this.PutUShort(value, (ushort)51);
            }
        }
    }
}
