﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20170000
{
    public class S12001162 : Event
    {
        public S12001162()
        {
            this.EventID = 12001162;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "控制面板的$R;" +
            "電源還連接著。$R;", "");
            switch (Select(pc, "要操作畫面么？", "", "不做", "按上方的按鈕", "按下方的按鈕"))
            {

                case 2:
                    ShowEffect(pc, 5289);

                    Say(pc, 0, 131, "嗶……。$R;" +
                    "$P……。$R;" +
                    "$P…………。$R;" +
                    "$P………………。$R;" +
                    "$P訪問成功。$R;" +
                    "開始傳送到塔尼亞界。$R;", " ");
                    ShowEffect(pc, 5314);
                    Wait(pc, 990);
                    Warp(pc, 11058000, 128, 157);
                    break;
                case 3:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        Say(pc, 131, "解除凭依之后才能通过哦.");
                        return;
                    }
                    if (pc.Quest != null)
                        Say(pc, 131, "要交掉任务以后才能通过.");
                    else
                    {
                        ShowEffect(pc, 5289);

                        Say(pc, 0, 0, "ピッ……。$R;" +
                        "$P……。$R;" +
                        "$P…………。$R;" +
                        "$P………………。$R;" +
                        "$Pアクセス成功。$R;" +
                        "ドミニオンワールドニ転移シマスカ？$R;", "");
                        if (Select(pc, "転移シマスカ？", "", "次元転生ついて", "行く") == 2)
                        {
                            Say(pc, 0, 0, "現在あなたのドミニオン界でのＬＶは$R;" +
                            "$RＬＶ$R;" + pc.DominionLevel.ToString() +
                            "$RＪＯＢＬＶ$R;" + pc.DominionJobLevel.ToString() +
                            "。$R;", "");
                            ShowEffect(pc, 5313);
                            Wait(pc, 990);
                            Warp(pc, 12058000, 127, 155);
                            SetHomePoint(pc, 12058000, 128, 155);
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.BACK))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.BACK].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.BACK);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.CHEST_ACCE);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.FACE))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.FACE].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.FACE);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.FACE_ACCE))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.FACE_ACCE].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.FACE_ACCE);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.HEAD);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.HEAD_ACCE))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.HEAD_ACCE].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.HEAD_ACCE);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.LEFT_HAND);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LOWER_BODY))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.LOWER_BODY].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.LOWER_BODY);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.RIGHT_HAND);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.SHOES))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.SHOES].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.SHOES);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.SOCKS))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.SOCKS].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.SOCKS);
                            }
                            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                            {
                                if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].BaseData.possibleLv > pc.DominionLevel)
                                    RemoveEquipment(pc, EnumEquipSlot.UPPER_BODY);
                            }
                        }
                    }
                    break;

            }
        }
    }
}