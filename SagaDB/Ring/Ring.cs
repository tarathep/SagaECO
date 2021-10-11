namespace SagaDB.Ring
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Ring" />.
    /// </summary>
    public class Ring
    {
        /// <summary>
        /// Defines the members.
        /// </summary>
        private Dictionary<int, ActorPC> members = new Dictionary<int, ActorPC>();

        /// <summary>
        /// Defines the rights.
        /// </summary>
        private Dictionary<int, BitMask<RingRight>> rights = new Dictionary<int, BitMask<RingRight>>();

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
        /// Defines the fame.
        /// </summary>
        private uint fame;

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

        /// <summary>
        /// Gets or sets the Fame.
        /// </summary>
        public uint Fame
        {
            get
            {
                return this.fame;
            }
            set
            {
                this.fame = value;
            }
        }


        /// <summary>取得指定军团成员</summary>
        /// <param name="index">索引ID</param>
        /// <returns>成员玩家</returns>
        public ActorPC this[int index]
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
        public Dictionary<int, ActorPC> Members
        {
            get
            {
                return this.members;
            }
        }

        /// <summary>
        /// Gets the Rights.
        /// </summary>
        public Dictionary<int, BitMask<RingRight>> Rights
        {
            get
            {
                return this.rights;
            }
        }

        /// <summary>
        /// Gets the MaxMemberCount.
        /// </summary>
        public int MaxMemberCount
        {
            get
            {
                int num = 1;
                while (Factory<RingFameTable, RingFame>.Instance.Items.ContainsKey((uint)num) && this.fame >= Factory<RingFameTable, RingFame>.Instance.Items[(uint)num].Fame)
                    ++num;
                return num - 1;
            }
        }

        /// <summary>
        /// The GetMember.
        /// </summary>
        /// <param name="char_id">The char_id<see cref="uint"/>.</param>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        public ActorPC GetMember(uint char_id)
        {
            IEnumerable<ActorPC> source = this.members.Values.Where<ActorPC>((Func<ActorPC, bool>)(c => (int)c.CharID == (int)char_id));
            if (source.Count<ActorPC>() == 0)
                return (ActorPC)null;
            return source.First<ActorPC>();
        }

        /// <summary>
        /// The IsMember.
        /// </summary>
        /// <param name="char_id">玩家的CharID.</param>
        /// <returns>是否是军团成员.</returns>
        public bool IsMember(uint char_id)
        {
            return this.members.Values.Where<ActorPC>((Func<ActorPC, bool>)(c => (int)c.CharID == (int)char_id)).Count<ActorPC>() != 0;
        }

        /// <summary>
        /// The IsMember.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>是否是军团成员.</returns>
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
        /// <returns>成员ID，如果不是军团成员则返回-1.</returns>
        public int IndexOf(ActorPC pc)
        {
            foreach (byte key in this.members.Keys)
            {
                if ((int)this.members[(int)key].CharID == (int)pc.CharID)
                    return (int)key;
            }
            return -1;
        }

        /// <summary>
        /// The IndexOf.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>成员ID，如果不是军团成员则返回-1.</returns>
        public int IndexOf(uint pc)
        {
            foreach (byte key in this.members.Keys)
            {
                if ((int)this.members[(int)key].CharID == (int)pc)
                    return (int)key;
            }
            return -1;
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
        /// <returns>军团中的索引.</returns>
        public int NewMember(ActorPC pc)
        {
            if (this.IsMember(pc))
                return (int)(byte)this.IndexOf(pc);
            int maxMemberCount = this.MaxMemberCount;
            for (int key = 8; key < maxMemberCount + 8; ++key)
            {
                if (!this.members.ContainsKey(key))
                {
                    this.members.Add(key, pc);
                    this.rights.Add(key, new BitMask<RingRight>(new BitMask()));
                    return key;
                }
            }
            return -1;
        }

        /// <summary>
        /// The DeleteMemeber.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void DeleteMemeber(ActorPC pc)
        {
            this.rights.Remove(this.IndexOf(pc));
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
