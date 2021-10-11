namespace SagaDB
{
    using SagaDB.Actor;
    using SagaDB.BBS;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ActorDB" />.
    /// </summary>
    public interface ActorDB
    {
        /// <summary>
        /// The SaveChar.
        /// </summary>
        /// <param name="aChar">Character that needs to be writen.</param>
        void SaveChar(ActorPC aChar);

        /// <summary>
        /// The SaveChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        /// <param name="fullinfo">The fullinfo<see cref="bool"/>.</param>
        void SaveChar(ActorPC aChar, bool fullinfo);

        /// <summary>
        /// The SaveChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        /// <param name="itemInfo">The itemInfo<see cref="bool"/>.</param>
        /// <param name="fullinfo">The fullinfo<see cref="bool"/>.</param>
        void SaveChar(ActorPC aChar, bool itemInfo, bool fullinfo);

        /// <summary>
        /// The CreateChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        /// <param name="account_id">The account_id<see cref="int"/>.</param>
        void CreateChar(ActorPC aChar, int account_id);

        /// <summary>
        /// The DeleteChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        void DeleteChar(ActorPC aChar);

        /// <summary>
        /// The GetChar.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        ActorPC GetChar(uint charID);

        /// <summary>
        /// The GetChar.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <param name="fullinfo">The fullinfo<see cref="bool"/>.</param>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        ActorPC GetChar(uint charID, bool fullinfo);

        /// <summary>
        /// The CharExists.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool CharExists(string name);

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        uint GetAccountID(ActorPC pc);

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        uint GetAccountID(uint charID);

        /// <summary>
        /// The GetCharIDs.
        /// </summary>
        /// <param name="account_id">The account_id<see cref="int"/>.</param>
        /// <returns>The <see cref="uint[]"/>.</returns>
        uint[] GetCharIDs(int account_id);

        /// <summary>
        /// The GetCharName.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string GetCharName(uint id);

        /// <summary>
        /// The Connect.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool Connect();

        /// <summary>
        /// The isConnected.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool isConnected();

        /// <summary>
        /// The GetFriendList.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>好友列表.</returns>
        List<ActorPC> GetFriendList(ActorPC pc);

        /// <summary>
        /// The GetFriendList2.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>玩家列表.</returns>
        List<ActorPC> GetFriendList2(ActorPC pc);

        /// <summary>
        /// The AddFriend.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        void AddFriend(ActorPC pc, uint charID);

        /// <summary>
        /// The IsFriend.
        /// </summary>
        /// <param name="char1">The char1<see cref="uint"/>.</param>
        /// <param name="char2">The char2<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsFriend(uint char1, uint char2);

        /// <summary>
        /// The DeleteFriend.
        /// </summary>
        /// <param name="char1">The char1<see cref="uint"/>.</param>
        /// <param name="char2">The char2<see cref="uint"/>.</param>
        void DeleteFriend(uint char1, uint char2);

        /// <summary>
        /// The GetParty.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Party.Party"/>.</returns>
        SagaDB.Party.Party GetParty(uint id);

        /// <summary>
        /// The NewParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        void NewParty(SagaDB.Party.Party party);

        /// <summary>
        /// The SaveParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        void SaveParty(SagaDB.Party.Party party);

        /// <summary>
        /// The DeleteParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        void DeleteParty(SagaDB.Party.Party party);

        /// <summary>
        /// The GetRing.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Ring.Ring"/>.</returns>
        SagaDB.Ring.Ring GetRing(uint id);

        /// <summary>
        /// The NewRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        void NewRing(SagaDB.Ring.Ring ring);

        /// <summary>
        /// The SaveRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="saveMembers">The saveMembers<see cref="bool"/>.</param>
        void SaveRing(SagaDB.Ring.Ring ring, bool saveMembers);

        /// <summary>
        /// The DeleteRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        void DeleteRing(SagaDB.Ring.Ring ring);

        /// <summary>
        /// The RingEmblemUpdate.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="buf">The buf<see cref="byte[]"/>.</param>
        void RingEmblemUpdate(SagaDB.Ring.Ring ring, byte[] buf);

        /// <summary>
        /// The GetRingEmblem.
        /// </summary>
        /// <param name="ring_id">The ring_id<see cref="uint"/>.</param>
        /// <param name="date">The date<see cref="DateTime"/>.</param>
        /// <param name="needUpdate">The needUpdate<see cref="bool"/>.</param>
        /// <param name="newTime">The newTime<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        byte[] GetRingEmblem(uint ring_id, DateTime date, out bool needUpdate, out DateTime newTime);

        /// <summary>
        /// The GetBBSPage.
        /// </summary>
        /// <param name="bbsID">The bbsID<see cref="uint"/>.</param>
        /// <param name="page">The page<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Post}"/>.</returns>
        List<Post> GetBBSPage(uint bbsID, int page);

        /// <summary>
        /// The BBSNewPost.
        /// </summary>
        /// <param name="poster">The poster<see cref="ActorPC"/>.</param>
        /// <param name="bbsID">The bbsID<see cref="uint"/>.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="content">The content<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool BBSNewPost(ActorPC poster, uint bbsID, string title, string content);

        /// <summary>
        /// The LoadServerVar.
        /// </summary>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        ActorPC LoadServerVar();

        /// <summary>
        /// The SaveServerVar.
        /// </summary>
        /// <param name="fakepc">The fakepc<see cref="ActorPC"/>.</param>
        void SaveServerVar(ActorPC fakepc);

        /// <summary>
        /// The GetVShop.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        void GetVShop(ActorPC pc);

        /// <summary>
        /// The SaveVShop.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        void SaveVShop(ActorPC pc);

        /// <summary>
        /// The SaveWRP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        void SaveWRP(ActorPC pc);

        /// <summary>
        /// The GetWRPRanking.
        /// </summary>
        /// <returns>The <see cref="List{ActorPC}"/>.</returns>
        List<ActorPC> GetWRPRanking();
    }
}
