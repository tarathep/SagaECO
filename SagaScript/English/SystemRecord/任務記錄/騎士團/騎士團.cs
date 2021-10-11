﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Knights
    {
        //共通標示
        Was_told_not_to_join_the_Knights = 0x1,//_1A00
        聽取騎士團說明 = 0x2,//_0A06
        Consider_joining_the_Knights = 0x4,//_0A75
        已經加入騎士團 = 0x8,//_0A44

        Get_the_Uptown_Pass = 0x10,//_0A00
        獲得騎士團皮盔甲 = 0x20,//_1A38

        //東軍
        聽取東軍騎士團說明 = 0x40,//_0A14
        加入東軍騎士團 = 0x80,//_0A34
        

        //南軍
        告知無法加入南軍 = 0x100,//_1A02
        聽取南軍騎士團說明 = 0x200,//_0A16
        加入南軍騎士團 = 0x400,//_0A36

        //西軍
        聽取西軍騎士團說明 = 0x800,//_0A15
        告知無法加入西軍 = 0x1000,//_1A01
        加入西軍騎士團 = 0x2000,//_0A35

        //北軍
        告知無法加入北軍 = 0x4000,//_1A03
        聽取北軍騎士團說明 = 0x8000,//_0A17
        加入北軍騎士團 = 0x10000,//_0A37

        //追加
        Tell_me_how_to_join_the_Knights = 0x20000,//_0A07
        Tell_the_group_leader_the_reason_for_ignoring_you = 0x40000,//_1A04
        Informed_that_there_is_no_pass = 0x80000,//_0A01

        //過境
        北國過境檢查完成 = 0x100000,//_2A44
        南國過境檢查完成 = 0x200000,//_2a61
        西國過境檢查完成 = 0x400000,//_6a53
        東國過境檢查完成 = 0x800000,//_5a20

        //許可證
        出示西國許可證 = 0x1000000,//_6a58
    }
}