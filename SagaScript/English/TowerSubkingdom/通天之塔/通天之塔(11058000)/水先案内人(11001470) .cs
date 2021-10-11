using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001470 : Event
    {
        public S11001470()
        {
            this.EventID = 11001470;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "乗ってく？$R;" +
            "あたしたちの島に連れてったげる♪$R;", "水先案内人");
            if (Select(pc, "乗ってく？", "", "行かない", "行く") == 2)
            {
                Warp(pc, 11053000, 19, 230);
            }
        }
    }
}