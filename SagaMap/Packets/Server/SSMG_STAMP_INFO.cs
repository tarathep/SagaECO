namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_STAMP_INFO" />.
    /// </summary>
    public class SSMG_STAMP_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_STAMP_INFO"/> class.
        /// </summary>
        public SSMG_STAMP_INFO()
        {
            this.data = new byte[25];
            this.offset = (ushort)2;
            this.ID = (ushort)7100;
            this.PutByte((byte)11, (ushort)2);
        }

        /// <summary>
        /// Sets the Stamp.
        /// </summary>
        public Stamp Stamp
        {
            set
            {
                this.PutShort((short)value[StampGenre.Special].Value, (ushort)3);
                this.PutShort((short)value[StampGenre.Pururu].Value, (ushort)5);
                this.PutShort((short)value[StampGenre.Field].Value, (ushort)7);
                this.PutShort((short)value[StampGenre.Coast].Value, (ushort)9);
                this.PutShort((short)value[StampGenre.Wild].Value, (ushort)11);
                this.PutShort((short)value[StampGenre.Cave].Value, (ushort)13);
                this.PutShort((short)value[StampGenre.Snow].Value, (ushort)15);
                this.PutShort((short)value[StampGenre.Colliery].Value, (ushort)17);
                this.PutShort((short)value[StampGenre.Northan].Value, (ushort)19);
                this.PutShort((short)value[StampGenre.IronSouth].Value, (ushort)21);
                this.PutShort((short)value[StampGenre.SouthDungeon].Value, (ushort)23);
            }
        }
    }
}
