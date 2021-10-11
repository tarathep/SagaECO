namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaDB.Map;
    using SagaLib;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_ACTOR_APPEAR" />.
    /// </summary>
    public class SSMG_ITEM_ACTOR_APPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_ACTOR_APPEAR"/> class.
        /// </summary>
        public SSMG_ITEM_ACTOR_APPEAR()
        {
            this.data = new byte[26];
            this.offset = (ushort)2;
            this.ID = (ushort)2005;
        }

        /// <summary>
        /// Sets the Item.
        /// </summary>
        public ActorItem Item
        {
            set
            {
                MapInfo info = Singleton<MapManager>.Instance.GetMap(value.MapID).Info;
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutUInt(value.Item.ItemID, (ushort)6);
                this.PutByte(Global.PosX16to8(value.X, info.width), (ushort)10);
                this.PutByte(Global.PosY16to8(value.Y, info.height), (ushort)11);
                this.PutUShort(value.Item.Stack, (ushort)12);
                this.PutUInt(10U, (ushort)14);
                if (value.PossessionItem)
                    this.PutByte((byte)1, (ushort)22);
                else
                    this.PutByte((byte)0, (ushort)22);
                byte[] bytes = Global.Unicode.GetBytes(value.Comment + "\0");
                byte length = (byte)bytes.Length;
                byte[] numArray = new byte[26 + (int)length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte(length, (ushort)23);
                this.PutBytes(bytes, (ushort)24);
                this.PutByte(value.Item.identified, (ushort)(24U + (uint)length));
                byte b = 0;
                if (value.Item.PictID != 0U)
                    b = (byte)1;
                this.PutByte(b, (ushort)(25U + (uint)length));
            }
        }
    }
}
