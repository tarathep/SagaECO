namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_BUFF" />.
    /// </summary>
    public class SSMG_ACTOR_BUFF : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_BUFF"/> class.
        /// </summary>
        public SSMG_ACTOR_BUFF()
        {
            this.data = new byte[38];
            this.offset = (ushort)2;
            this.ID = (ushort)5500;
        }

        /// <summary>
        /// Sets the Actor.
        /// </summary>
        public SagaDB.Actor.Actor Actor
        {
            set
            {
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutInt(value.Buff.Buffs[0].Value, (ushort)6);
                this.PutInt(value.Buff.Buffs[1].Value, (ushort)10);
                this.PutInt(value.Buff.Buffs[2].Value, (ushort)14);
                this.PutInt(value.Buff.Buffs[3].Value, (ushort)18);
                this.PutInt(value.Buff.Buffs[4].Value, (ushort)22);
                this.PutInt(value.Buff.Buffs[5].Value, (ushort)26);
                this.PutInt(value.Buff.Buffs[6].Value, (ushort)30);
            }
        }
    }
}
