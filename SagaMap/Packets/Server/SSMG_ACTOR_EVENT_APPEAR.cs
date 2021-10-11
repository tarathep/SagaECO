namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaDB.Map;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_EVENT_APPEAR" />.
    /// </summary>
    public class SSMG_ACTOR_EVENT_APPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_EVENT_APPEAR"/> class.
        /// </summary>
        public SSMG_ACTOR_EVENT_APPEAR()
        {
            this.data = new byte[19];
            this.offset = (ushort)2;
            this.ID = (ushort)3000;
        }

        /// <summary>
        /// Sets the Actor.
        /// </summary>
        public ActorEvent Actor
        {
            set
            {
                byte[] bdata = (byte[])null;
                byte[] bytes = Global.Unicode.GetBytes(value.Title + "\0");
                MapInfo mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[value.MapID];
                switch (value.Type)
                {
                    case ActorEventTypes.ROPE:
                        bdata = Global.Unicode.GetBytes("fg_rope_01\0");
                        break;
                    case ActorEventTypes.TENT:
                        bdata = Global.Unicode.GetBytes("33_tent01\0");
                        break;
                }
                byte[] numArray = new byte[19 + bdata.Length + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutByte((byte)bdata.Length);
                this.PutBytes(bdata);
                this.PutByte(Global.PosX16to8(value.X, mapInfo.width));
                this.PutByte(Global.PosY16to8(value.Y, mapInfo.height));
                switch (value.Type)
                {
                    case ActorEventTypes.ROPE:
                        this.PutByte((byte)6);
                        break;
                    case ActorEventTypes.TENT:
                        this.PutByte((byte)4);
                        break;
                }
                this.PutUInt(value.EventID);
                this.PutByte((byte)bytes.Length);
                this.PutBytes(bytes);
                this.PutUInt(value.Caster.CharID);
            }
        }
    }
}
