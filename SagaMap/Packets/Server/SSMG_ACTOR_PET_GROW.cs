namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_PET_GROW" />.
    /// </summary>
    public class SSMG_ACTOR_PET_GROW : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_PET_GROW"/> class.
        /// </summary>
        public SSMG_ACTOR_PET_GROW()
        {
            this.data = new byte[18];
            this.offset = (ushort)2;
            this.ID = (ushort)4800;
        }

        /// <summary>
        /// Sets the PetActorID.
        /// </summary>
        public uint PetActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the OwnerActorID.
        /// </summary>
        public uint OwnerActorID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Type.
        /// </summary>
        public SSMG_ACTOR_PET_GROW.GrowType Type
        {
            set
            {
                this.PutUInt((uint)value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Value.
        /// </summary>
        public uint Value
        {
            set
            {
                this.PutUInt(value, (ushort)14);
            }
        }

        /// <summary>
        /// Defines the GrowType.
        /// </summary>
        public enum GrowType
        {
            /// <summary>
            /// Defines the HP.
            /// </summary>
            HP,

            /// <summary>
            /// Defines the SP.
            /// </summary>
            SP,

            /// <summary>
            /// Defines the MP.
            /// </summary>
            MP,

            /// <summary>
            /// Defines the Speed.
            /// </summary>
            Speed,

            /// <summary>
            /// Defines the ATK1.
            /// </summary>
            ATK1,

            /// <summary>
            /// Defines the ATK2.
            /// </summary>
            ATK2,

            /// <summary>
            /// Defines the ATK3.
            /// </summary>
            ATK3,

            /// <summary>
            /// Defines the MATK.
            /// </summary>
            MATK,

            /// <summary>
            /// Defines the Def.
            /// </summary>
            Def,

            /// <summary>
            /// Defines the MDef.
            /// </summary>
            MDef,

            /// <summary>
            /// Defines the HitMelee.
            /// </summary>
            HitMelee,

            /// <summary>
            /// Defines the HitRanged.
            /// </summary>
            HitRanged,

            /// <summary>
            /// Defines the HitMagic.
            /// </summary>
            HitMagic,

            /// <summary>
            /// Defines the AvoidMelee.
            /// </summary>
            AvoidMelee,

            /// <summary>
            /// Defines the AvoidRanged.
            /// </summary>
            AvoidRanged,

            /// <summary>
            /// Defines the AvoidMagic.
            /// </summary>
            AvoidMagic,

            /// <summary>
            /// Defines the Critical.
            /// </summary>
            Critical,

            /// <summary>
            /// Defines the AvoidCri.
            /// </summary>
            AvoidCri,

            /// <summary>
            /// Defines the Recover.
            /// </summary>
            Recover,

            /// <summary>
            /// Defines the MPRecover.
            /// </summary>
            MPRecover,

            /// <summary>
            /// Defines the Stamina.
            /// </summary>
            Stamina,

            /// <summary>
            /// Defines the ASPD.
            /// </summary>
            ASPD,

            /// <summary>
            /// Defines the CSPD.
            /// </summary>
            CSPD,
        }
    }
}
