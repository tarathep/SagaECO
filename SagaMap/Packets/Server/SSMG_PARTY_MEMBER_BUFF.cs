namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_MEMBER_BUFF" />.
    /// </summary>
    public class SSMG_PARTY_MEMBER_BUFF : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_MEMBER_BUFF"/> class.
        /// </summary>
        public SSMG_PARTY_MEMBER_BUFF()
        {
            this.data = new byte[42];
            this.offset = (ushort)2;
            this.ID = (ushort)6650;
        }

        /// <summary>
        /// Sets the Actor.
        /// </summary>
        public ActorPC Actor
        {
            set
            {
                this.PutUInt((uint)value.Party.IndexOf(value), (ushort)2);
                this.PutUInt(value.CharID, (ushort)6);
                this.PutInt(value.Buff.Buffs[0].Value, (ushort)10);
                this.PutInt(value.Buff.Buffs[1].Value, (ushort)14);
                this.PutInt(value.Buff.Buffs[2].Value, (ushort)18);
                this.PutInt(value.Buff.Buffs[3].Value, (ushort)22);
                this.PutInt(value.Buff.Buffs[4].Value, (ushort)26);
                this.PutInt(value.Buff.Buffs[5].Value, (ushort)30);
                this.PutInt(value.Buff.Buffs[6].Value, (ushort)34);
            }
        }
    }
}
