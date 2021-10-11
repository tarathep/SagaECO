namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_KNIGHTWAR_RESULT" />.
    /// </summary>
    public class SSMG_KNIGHTWAR_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_KNIGHTWAR_RESULT"/> class.
        /// </summary>
        public SSMG_KNIGHTWAR_RESULT()
        {
            this.data = new byte[40];
            this.offset = (ushort)2;
            this.ID = (ushort)7020;
            this.PutByte((byte)4, (ushort)2);
            this.PutByte((byte)4, (ushort)7);
        }

        /// <summary>
        /// Sets the Rank1Country.
        /// </summary>
        public Country Rank1Country
        {
            set
            {
                this.PutByte((byte)value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Rank2Country.
        /// </summary>
        public Country Rank2Country
        {
            set
            {
                this.PutByte((byte)value, (ushort)4);
            }
        }

        /// <summary>
        /// Sets the Rank3ountry.
        /// </summary>
        public Country Rank3ountry
        {
            set
            {
                this.PutByte((byte)value, (ushort)5);
            }
        }

        /// <summary>
        /// Sets the Rank4Country.
        /// </summary>
        public Country Rank4Country
        {
            set
            {
                this.PutByte((byte)value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Rank1Point.
        /// </summary>
        public int Rank1Point
        {
            set
            {
                this.PutInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the Rank2Point.
        /// </summary>
        public int Rank2Point
        {
            set
            {
                this.PutInt(value, (ushort)12);
            }
        }

        /// <summary>
        /// Sets the Rank3Point.
        /// </summary>
        public int Rank3Point
        {
            set
            {
                this.PutInt(value, (ushort)16);
            }
        }

        /// <summary>
        /// Sets the Rank4Point.
        /// </summary>
        public int Rank4Point
        {
            set
            {
                this.PutInt(value, (ushort)20);
            }
        }

        /// <summary>
        /// Sets the RankingBonus.
        /// </summary>
        public int RankingBonus
        {
            set
            {
                this.PutInt(value, (ushort)24);
            }
        }

        /// <summary>
        /// Sets the DeathPenalty.
        /// </summary>
        public int DeathPenalty
        {
            set
            {
                this.PutInt(value, (ushort)28);
            }
        }

        /// <summary>
        /// Sets the ScoreBonus.
        /// </summary>
        public int ScoreBonus
        {
            set
            {
                this.PutInt(value, (ushort)32);
            }
        }

        /// <summary>
        /// Sets the RewardBonus.
        /// </summary>
        public int RewardBonus
        {
            set
            {
                this.PutInt(value, (ushort)36);
            }
        }
    }
}
