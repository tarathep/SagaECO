namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_NAME" />.
    /// </summary>
    public class SSMG_RING_NAME : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_NAME"/> class.
        /// </summary>
        public SSMG_RING_NAME()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)6865;
        }

        /// <summary>
        /// Sets the Player.
        /// </summary>
        public ActorPC Player
        {
            set
            {
                this.PutUInt(value.ActorID, (ushort)2);
                if (value.Ring != null)
                    this.PutUInt(value.Ring.ID);
                else
                    this.PutUInt(0U);
                byte[] bdata = value.Ring == null ? new byte[1] : Global.Unicode.GetBytes(value.Ring.Name + "\0");
                byte[] numArray = new byte[12 + bdata.Length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)bdata.Length);
                this.PutBytes(bdata);
                if (value.Ring == null || value != value.Ring.Leader)
                    return;
                this.PutByte((byte)1);
            }
        }
    }
}
