namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_STATUS_EXTEND" />.
    /// </summary>
    public class SSMG_PLAYER_STATUS_EXTEND : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_STATUS_EXTEND"/> class.
        /// </summary>
        public SSMG_PLAYER_STATUS_EXTEND()
        {
            this.data = new byte[63];
            this.offset = (ushort)2;
            this.ID = (ushort)535;
            this.PutByte((byte)30, (ushort)2);
        }

        /// <summary>
        /// Sets the Speed.
        /// </summary>
        public ushort Speed
        {
            set
            {
                this.PutUShort(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the ATK1Min.
        /// </summary>
        public ushort ATK1Min
        {
            set
            {
                this.PutUShort(value, (ushort)5);
            }
        }

        /// <summary>
        /// Sets the ATK2Min.
        /// </summary>
        public ushort ATK2Min
        {
            set
            {
                this.PutUShort(value, (ushort)7);
            }
        }

        /// <summary>
        /// Sets the ATK3Min.
        /// </summary>
        public ushort ATK3Min
        {
            set
            {
                this.PutUShort(value, (ushort)9);
            }
        }

        /// <summary>
        /// Sets the ATK1Max.
        /// </summary>
        public ushort ATK1Max
        {
            set
            {
                this.PutUShort(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets the ATK2Max.
        /// </summary>
        public ushort ATK2Max
        {
            set
            {
                this.PutUShort(value, (ushort)13);
            }
        }

        /// <summary>
        /// Sets the ATK3Max.
        /// </summary>
        public ushort ATK3Max
        {
            set
            {
                this.PutUShort(value, (ushort)15);
            }
        }

        /// <summary>
        /// Sets the MATKMin.
        /// </summary>
        public ushort MATKMin
        {
            set
            {
                this.PutUShort(value, (ushort)17);
            }
        }

        /// <summary>
        /// Sets the MATKMax.
        /// </summary>
        public ushort MATKMax
        {
            set
            {
                this.PutUShort(value, (ushort)19);
            }
        }

        /// <summary>
        /// Sets the DefBase.
        /// </summary>
        public ushort DefBase
        {
            set
            {
                this.PutUShort(value, (ushort)21);
            }
        }

        /// <summary>
        /// Sets the DefAddition.
        /// </summary>
        public ushort DefAddition
        {
            set
            {
                this.PutUShort(value, (ushort)23);
            }
        }

        /// <summary>
        /// Sets the MDefBase.
        /// </summary>
        public ushort MDefBase
        {
            set
            {
                this.PutUShort(value, (ushort)25);
            }
        }

        /// <summary>
        /// Sets the MDefAddition.
        /// </summary>
        public ushort MDefAddition
        {
            set
            {
                this.PutUShort(value, (ushort)27);
            }
        }

        /// <summary>
        /// Sets the HitMelee.
        /// </summary>
        public ushort HitMelee
        {
            set
            {
                this.PutUShort(value, (ushort)29);
            }
        }

        /// <summary>
        /// Sets the HitRanged.
        /// </summary>
        public ushort HitRanged
        {
            set
            {
                this.PutUShort(value, (ushort)31);
            }
        }

        /// <summary>
        /// Sets the HitMagic.
        /// </summary>
        public ushort HitMagic
        {
            set
            {
                this.PutUShort(value, (ushort)33);
            }
        }

        /// <summary>
        /// Sets the HitCritical.
        /// </summary>
        public ushort HitCritical
        {
            set
            {
                this.PutUShort(value, (ushort)35);
            }
        }

        /// <summary>
        /// Sets the AvoidMelee.
        /// </summary>
        public ushort AvoidMelee
        {
            set
            {
                this.PutUShort(value, (ushort)37);
            }
        }

        /// <summary>
        /// Sets the AvoidRanged.
        /// </summary>
        public ushort AvoidRanged
        {
            set
            {
                this.PutUShort(value, (ushort)39);
            }
        }

        /// <summary>
        /// Sets the AvoidMagic.
        /// </summary>
        public ushort AvoidMagic
        {
            set
            {
                this.PutUShort(value, (ushort)41);
            }
        }

        /// <summary>
        /// Sets the AvoidCritical.
        /// </summary>
        public ushort AvoidCritical
        {
            set
            {
                this.PutUShort(value, (ushort)43);
            }
        }

        /// <summary>
        /// Sets the HealHP.
        /// </summary>
        public ushort HealHP
        {
            set
            {
                this.PutUShort(value, (ushort)45);
            }
        }

        /// <summary>
        /// Sets the HealMP.
        /// </summary>
        public ushort HealMP
        {
            set
            {
                this.PutUShort(value, (ushort)47);
            }
        }

        /// <summary>
        /// Sets the HealSP.
        /// </summary>
        public ushort HealSP
        {
            set
            {
                this.PutUShort(value, (ushort)49);
            }
        }

        /// <summary>
        /// Sets the ASPD.
        /// </summary>
        public short ASPD
        {
            set
            {
                this.PutShort(value, (ushort)51);
            }
        }

        /// <summary>
        /// Sets the CSPD.
        /// </summary>
        public short CSPD
        {
            set
            {
                this.PutShort(value, (ushort)53);
            }
        }
    }
}
