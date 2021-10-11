namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_EVENT_TITLE_CHANGE" />.
    /// </summary>
    public class SSMG_ACTOR_EVENT_TITLE_CHANGE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_EVENT_TITLE_CHANGE"/> class.
        /// </summary>
        public SSMG_ACTOR_EVENT_TITLE_CHANGE()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)3002;
        }

        /// <summary>
        /// Sets the Actor.
        /// </summary>
        public ActorEvent Actor
        {
            set
            {
                byte[] bytes = Global.Unicode.GetBytes(value.Title + "\0");
                byte[] numArray = new byte[7 + bytes.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutByte((byte)bytes.Length);
                this.PutBytes(bytes);
            }
        }
    }
}
