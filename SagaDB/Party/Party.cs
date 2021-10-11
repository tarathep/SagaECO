namespace SagaDB.Party
{
    using SagaDB.Actor;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Party" />.
    /// </summary>
    public class Party
    {
        /// <summary>
        /// Defines the members.
        /// </summary>
        private Dictionary<byte, ActorPC> members = new Dictionary<byte, ActorPC>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the leader.
        /// </summary>
        private ActorPC leader;

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }


        /// <summary>取得指定队伍成员</summary>
        /// <param name="index">索引ID</param>
        /// <returns>成员玩家</returns>
        public ActorPC this[byte index]
        {
            get
            {
                if (this.members.ContainsKey(index))
                    return this.members[index];
                return (ActorPC)null;
            }
        }
        /// <summary>
        /// Gets or sets the Leader.
        /// </summary>
        public ActorPC Leader
        {
            get
            {
                return this.leader;
            }
            set
            {
                this.leader = value;
            }
        }

        /// <summary>
        /// Gets the Members.
        /// </summary>
        public Dictionary<byte, ActorPC> Members
        {
            get
            {
                return this.members;
            }
        }

        /// <summary>
        /// The IsMember.
        /// </summary>
        /// <param name="char_id">玩家的CharID.</param>
        /// <returns>是否是队伍成员.</returns>
        public bool IsMember(uint char_id)
        {
            return this.members.Values.Where<ActorPC>((Func<ActorPC, bool>)(c => (int)c.CharID == (int)char_id)).Count<ActorPC>() != 0;
        }

        /// <summary>
        /// The IsMember.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>是否是队伍成员.</returns>
        public bool IsMember(ActorPC pc)
        {
            return this.IsMember(pc.CharID);
        }

        /// <summary>
        /// Gets the MemberCount.
        /// </summary>
        public int MemberCount
        {
            get
            {
                return this.members.Count;
            }
        }

        /// <summary>
        /// The IndexOf.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>成员ID，如果不是队伍成员则返回-1.</returns>
        public byte IndexOf(ActorPC pc)
        {
            foreach (byte key in this.members.Keys)
            {
                if ((int)this.members[key].CharID == (int)pc.CharID)
                    return key;
            }
            return byte.MaxValue;
        }

        /// <summary>
        /// The IndexOf.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>成员ID，如果不是队伍成员则返回-1.</returns>
        public byte IndexOf(uint pc)
        {
            foreach (byte key in this.members.Keys)
            {
                if ((int)this.members[key].CharID == (int)pc)
                    return key;
            }
            return byte.MaxValue;
        }

        /// <summary>
        /// The MemberOnline.
        /// </summary>
        /// <param name="newPC">新Actor.</param>
        public void MemberOnline(ActorPC newPC)
        {
            if (!this.IsMember(newPC))
                return;
            this.members[this.IndexOf(newPC)] = newPC;
            if ((int)this.leader.CharID != (int)newPC.CharID)
                return;
            this.leader = newPC;
        }

        /// <summary>
        /// The NewMember.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>队伍中的索引.</returns>
        public byte NewMember(ActorPC pc)
        {
            if (this.IsMember(pc))
                return this.IndexOf(pc);
            for (byte key = 0; key < (byte)8; ++key)
            {
                if (!this.members.ContainsKey(key))
                {
                    this.members.Add(key, pc);
                    return key;
                }
            }
            return byte.MaxValue;
        }

        /// <summary>
        /// The DeleteMemeber.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void DeleteMemeber(ActorPC pc)
        {
            this.members.Remove(this.IndexOf(pc));
        }

        /// <summary>
        /// The DeleteMemeber.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void DeleteMemeber(uint pc)
        {
            this.members.Remove(this.IndexOf(pc));
        }
    }
}
