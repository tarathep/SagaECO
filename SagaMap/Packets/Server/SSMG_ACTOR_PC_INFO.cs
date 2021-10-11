namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaLib;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_PC_INFO" />.
    /// </summary>
    public class SSMG_ACTOR_PC_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_PC_INFO"/> class.
        /// </summary>
        public SSMG_ACTOR_PC_INFO()
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[137];
            else
                this.data = new byte[136];
            this.offset = (ushort)2;
            this.ID = (ushort)526;
        }

        /// <summary>
        /// Sets the SetShadow.
        /// </summary>
        private ActorShadow SetShadow
        {
            set
            {
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutUInt(uint.MaxValue, (ushort)6);
                byte[] bytes = Global.Unicode.GetBytes(value.Name + "\0");
                byte length = (byte)bytes.Length;
                byte[] numArray = new byte[this.data.Length - 1 + (int)length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte(length, (ushort)10);
                this.PutBytes(bytes, (ushort)11);
                ushort index1 = (ushort)(11U + (uint)length);
                if (value.Owner.Marionette == null)
                {
                    this.PutByte((byte)value.Owner.Race, index1);
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        ++index1;
                        this.PutByte((byte)value.Owner.Form, index1);
                    }
                    this.PutByte((byte)value.Owner.Gender, (ushort)((uint)index1 + 1U));
                    this.PutByte(value.Owner.HairStyle, (ushort)((uint)index1 + 2U));
                    this.PutByte(value.Owner.HairColor, (ushort)((uint)index1 + 3U));
                    this.PutByte(value.Owner.Wig, (ushort)((uint)index1 + 4U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 5U));
                    this.PutByte(value.Owner.Face, (ushort)((uint)index1 + 6U));
                }
                else
                {
                    this.PutByte(byte.MaxValue, index1);
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        ++index1;
                        this.PutByte(byte.MaxValue, index1);
                    }
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 1U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 2U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 3U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 4U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 5U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 6U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 7U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 8U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 9U));
                }
                this.PutByte((byte)13, (ushort)((uint)index1 + 10U));
                if (value.Owner.Marionette == null)
                {
                    for (int index2 = 0; index2 < 13; ++index2)
                    {
                        if (value.Owner.Inventory.Equipments.ContainsKey((EnumEquipSlot)index2))
                        {
                            SagaDB.Item.Item equipment = value.Owner.Inventory.Equipments[(EnumEquipSlot)index2];
                            if (equipment.Stack != (ushort)0)
                            {
                                if (equipment.PictID == 0U)
                                    this.PutUInt(equipment.BaseData.imageID, (ushort)((int)index1 + 11 + index2 * 4));
                                else
                                    this.PutUInt(equipment.PictID, (ushort)((int)index1 + 11 + index2 * 4));
                            }
                        }
                    }
                }
                else
                    this.PutUInt(value.Owner.Marionette.PictID, (ushort)((uint)index1 + 11U));
                this.PutByte((byte)3, (ushort)((uint)index1 + 63U));
                if (value.Owner.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                {
                    if (value.Owner.Marionette == null)
                    {
                        SagaDB.Item.Item equipment = value.Owner.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                        this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index1 + 64U));
                        this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index1 + 65U));
                    }
                }
                else
                    this.PutByte((byte)0, (ushort)((uint)index1 + 64U));
                this.PutByte((byte)3, (ushort)((uint)index1 + 67U));
                if (value.Owner.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    if (value.Owner.Marionette == null)
                    {
                        SagaDB.Item.Item equipment = value.Owner.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                        this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index1 + 68U));
                        this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index1 + 69U));
                    }
                }
                else
                    this.PutByte((byte)0, (ushort)((uint)index1 + 68U));
                this.PutByte((byte)3, (ushort)((uint)index1 + 71U));
                this.PutByte((byte)0, (ushort)((uint)index1 + 80U));
                this.PutByte((byte)1, (ushort)((uint)index1 + 81U));
                this.PutByte((byte)0, (ushort)((uint)index1 + 83U));
                this.PutByte((byte)1, (ushort)((uint)index1 + 88U));
                this.PutByte((byte)0, (ushort)((uint)index1 + 90U));
                this.PutByte((byte)1, (ushort)((uint)index1 + 91U));
                this.PutByte((byte)1, (ushort)((uint)index1 + 93U));
                this.PutUInt(500U, (ushort)((uint)index1 + 96U));
                this.PutUInt(2U, (ushort)((uint)index1 + 106U));
                this.PutUShort((ushort)0, (ushort)((uint)index1 + 110U));
            }
        }

        /// <summary>
        /// Sets the SetPC.
        /// </summary>
        private ActorPC SetPC
        {
            set
            {
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutUInt(value.CharID, (ushort)6);
                byte[] bytes1 = Global.Unicode.GetBytes(value.Name + "\0");
                byte length = (byte)bytes1.Length;
                byte[] numArray1 = new byte[this.data.Length - 1 + (int)length];
                this.data.CopyTo((Array)numArray1, 0);
                this.data = numArray1;
                this.PutByte(length, (ushort)10);
                this.PutBytes(bytes1, (ushort)11);
                ushort index1 = (ushort)(11U + (uint)length);
                if (value.Marionette == null && value.TranceID == 0U)
                {
                    this.PutByte((byte)value.Race, index1);
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        ++index1;
                        this.PutByte((byte)value.Form, index1);
                    }
                    this.PutByte((byte)value.Gender, (ushort)((uint)index1 + 1U));
                    this.PutByte(value.HairStyle, (ushort)((uint)index1 + 2U));
                    this.PutByte(value.HairColor, (ushort)((uint)index1 + 3U));
                    this.PutByte(value.Wig, (ushort)((uint)index1 + 4U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 5U));
                    this.PutByte(value.Face, (ushort)((uint)index1 + 6U));
                }
                else
                {
                    this.PutByte(byte.MaxValue, index1);
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        ++index1;
                        this.PutByte(byte.MaxValue, index1);
                    }
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 1U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 2U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 3U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 4U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 5U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 6U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 7U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 8U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 9U));
                }
                Dictionary<EnumEquipSlot, SagaDB.Item.Item> dictionary = value.Form == DEM_FORM.MACHINA_FORM ? value.Inventory.Parts : value.Inventory.Equipments;
                this.PutByte((byte)13, (ushort)((uint)index1 + 10U));
                if (value.Marionette == null)
                {
                    if (value.TranceID == 0U)
                    {
                        for (int index2 = 0; index2 < 13; ++index2)
                        {
                            if (dictionary.ContainsKey((EnumEquipSlot)index2))
                            {
                                SagaDB.Item.Item obj = dictionary[(EnumEquipSlot)index2];
                                if (obj.Stack != (ushort)0)
                                {
                                    if (obj.PictID == 0U)
                                        this.PutUInt(obj.BaseData.imageID, (ushort)((int)index1 + 11 + index2 * 4));
                                    else
                                        this.PutUInt(obj.PictID, (ushort)((int)index1 + 11 + index2 * 4));
                                }
                            }
                        }
                    }
                    else
                        this.PutUInt(value.TranceID, (ushort)((uint)index1 + 11U));
                }
                else
                    this.PutUInt(value.Marionette.PictID, (ushort)((uint)index1 + 11U));
                this.PutByte((byte)3, (ushort)((uint)index1 + 63U));
                if (dictionary.ContainsKey(EnumEquipSlot.LEFT_HAND))
                {
                    if (value.Marionette == null)
                    {
                        SagaDB.Item.Item obj = dictionary[EnumEquipSlot.LEFT_HAND];
                        this.PutByte(obj.BaseData.handMotion, (ushort)((uint)index1 + 64U));
                        this.PutByte(obj.BaseData.handMotion2, (ushort)((uint)index1 + 65U));
                    }
                }
                else
                    this.PutByte((byte)0, (ushort)((uint)index1 + 64U));
                this.PutByte((byte)3, (ushort)((uint)index1 + 67U));
                if (dictionary.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    if (value.Marionette == null)
                    {
                        SagaDB.Item.Item obj = dictionary[EnumEquipSlot.RIGHT_HAND];
                        this.PutByte(obj.BaseData.handMotion, (ushort)((uint)index1 + 68U));
                        this.PutByte(obj.BaseData.handMotion2, (ushort)((uint)index1 + 69U));
                    }
                }
                else
                    this.PutByte((byte)0, (ushort)((uint)index1 + 68U));
                this.PutByte((byte)3, (ushort)((uint)index1 + 71U));
                if (dictionary.ContainsKey(EnumEquipSlot.PET) && value.Pet != null && value.Pet.Ride)
                {
                    SagaDB.Item.Item obj = dictionary[EnumEquipSlot.PET];
                    this.PutByte(obj.BaseData.handMotion, (ushort)((uint)index1 + 72U));
                    this.PutByte(obj.BaseData.handMotion2, (ushort)((uint)index1 + 73U));
                }
                if (dictionary.ContainsKey(EnumEquipSlot.PET) && value.Pet != null && value.Pet.Ride)
                    this.PutUInt(dictionary[EnumEquipSlot.PET].ItemID, (ushort)((uint)index1 + 75U));
                this.PutByte(value.BattleStatus, (ushort)((uint)index1 + 80U));
                if (value.Party == null)
                {
                    this.PutByte((byte)1, (ushort)((uint)index1 + 81U));
                    this.PutByte((byte)1, (ushort)((uint)index1 + 83U));
                }
                else
                {
                    byte[] bytes2 = Global.Unicode.GetBytes(value.Party.Name + "\0");
                    byte[] numArray2 = new byte[this.data.Length + bytes2.Length];
                    this.data.CopyTo((Array)numArray2, 0);
                    this.data = numArray2;
                    this.PutByte((byte)bytes2.Length, (ushort)((uint)index1 + 81U));
                    this.PutBytes(bytes2, (ushort)((uint)index1 + 82U));
                    index1 += (ushort)(bytes2.Length - 1);
                    if (value == value.Party.Leader)
                        this.PutByte((byte)1, (ushort)((uint)index1 + 83U));
                    else
                        this.PutByte((byte)0, (ushort)((uint)index1 + 83U));
                }
                if (value.Ring == null)
                {
                    this.PutByte((byte)1, (ushort)((uint)index1 + 88U));
                    this.PutByte((byte)1, (ushort)((uint)index1 + 90U));
                }
                else
                {
                    byte[] bytes2 = Global.Unicode.GetBytes(value.Ring.Name + "\0");
                    byte[] numArray2 = new byte[this.data.Length + bytes2.Length];
                    this.data.CopyTo((Array)numArray2, 0);
                    this.data = numArray2;
                    this.PutByte((byte)bytes2.Length, (ushort)((uint)index1 + 88U));
                    this.PutBytes(bytes2, (ushort)((uint)index1 + 89U));
                    index1 += (ushort)(bytes2.Length - 1);
                    if (value == value.Ring.Leader)
                        this.PutByte((byte)1, (ushort)((uint)index1 + 90U));
                    else
                        this.PutByte((byte)0, (ushort)((uint)index1 + 90U));
                }
                byte[] bytes3 = Global.Unicode.GetBytes(value.Sign + "\0");
                byte[] numArray3 = new byte[this.data.Length + bytes3.Length];
                this.data.CopyTo((Array)numArray3, 0);
                this.data = numArray3;
                this.PutByte((byte)bytes3.Length, (ushort)((uint)index1 + 91U));
                this.PutBytes(bytes3, (ushort)((uint)index1 + 92U));
                ushort num = (ushort)((uint)index1 + (uint)(ushort)(bytes3.Length - 1));
                this.PutByte((byte)1, (ushort)((uint)num + 93U));
                this.PutUInt(1000U, (ushort)((uint)num + 96U));
                this.PutUShort((ushort)value.Motion, (ushort)((uint)num + 100U));
                this.PutUInt(0U, (ushort)((uint)num + 102U));
                switch (value.Mode)
                {
                    case PlayerMode.NORMAL:
                        this.PutInt(2, (ushort)((uint)num + 106U));
                        this.PutInt(0, (ushort)((uint)num + 110U));
                        break;
                    case PlayerMode.KNIGHT_WAR:
                        this.PutInt(34, (ushort)((uint)num + 106U));
                        this.PutInt(2, (ushort)((uint)num + 110U));
                        break;
                    case PlayerMode.COLISEUM_MODE:
                        this.PutInt(66, (ushort)((uint)num + 106U));
                        this.PutInt(1, (ushort)((uint)num + 110U));
                        break;
                    case PlayerMode.WRP:
                        this.PutInt(258, (ushort)((uint)num + 106U));
                        this.PutInt(4, (ushort)((uint)num + 110U));
                        break;
                }
                this.PutByte((byte)0, (ushort)((uint)num + 114U));
                this.PutByte((byte)0, (ushort)((uint)num + 115U));
                this.PutByte((byte)0, (ushort)((uint)num + 116U));
                this.PutByte((byte)0, (ushort)((uint)num + 117U));
                this.PutByte((byte)0, (ushort)((uint)num + 118U));
                if (Singleton<MapManager>.Instance.GetMap(value.MapID).Info.Flag.Test(MapFlags.Dominion))
                    this.PutByte(value.DominionLevel, (ushort)((uint)num + 119U));
                else
                    this.PutByte(value.Level, (ushort)((uint)num + 119U));
                this.PutUInt(value.WRPRanking, (ushort)((uint)num + 120U));
            }
        }

        /// <summary>
        /// Sets the SetPet.
        /// </summary>
        private ActorPet SetPet
        {
            set
            {
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutUInt(uint.MaxValue, (ushort)6);
                byte[] bytes = Global.Unicode.GetBytes(value.Name + "\0");
                byte length = (byte)bytes.Length;
                byte[] numArray = new byte[this.data.Length - 1 + (int)length];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte(length, (ushort)10);
                this.PutBytes(bytes, (ushort)11);
                ushort index = (ushort)(11U + (uint)length);
                this.PutByte(byte.MaxValue, index);
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                {
                    ++index;
                    this.PutByte(byte.MaxValue, index);
                }
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 1U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 2U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 3U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 4U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 5U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 6U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 7U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 8U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index + 9U));
                this.PutByte((byte)13, (ushort)((uint)index + 10U));
                if (value.BaseData.pictid != 0U)
                    this.PutUInt(value.BaseData.pictid, (ushort)((uint)index + 11U));
                else
                    this.PutUInt(value.PetID, (ushort)((uint)index + 11U));
                this.PutByte((byte)3, (ushort)((uint)index + 63U));
                this.PutByte((byte)3, (ushort)((uint)index + 67U));
                this.PutByte((byte)3, (ushort)((uint)index + 71U));
                this.PutByte((byte)0, (ushort)((uint)index + 80U));
                this.PutByte((byte)1, (ushort)((uint)index + 81U));
                this.PutByte((byte)1, (ushort)((uint)index + 83U));
                this.PutByte((byte)1, (ushort)((uint)index + 88U));
                this.PutByte((byte)1, (ushort)((uint)index + 90U));
                this.PutByte((byte)1, (ushort)((uint)index + 91U));
                this.PutByte((byte)1, (ushort)((uint)index + 93U));
                this.PutUInt(1000U, (ushort)((uint)index + 96U));
                this.PutUInt(2U, (ushort)((uint)index + 106U));
                this.PutUShort((ushort)0, (ushort)((uint)index + 110U));
                this.PutByte((byte)0, (ushort)((uint)index + 114U));
                this.PutByte((byte)0, (ushort)((uint)index + 115U));
                this.PutByte((byte)0, (ushort)((uint)index + 116U));
                this.PutByte((byte)0, (ushort)((uint)index + 117U));
                this.PutByte((byte)0, (ushort)((uint)index + 118U));
                this.PutByte((byte)1, (ushort)((uint)index + 119U));
                this.PutUInt(uint.MaxValue, (ushort)((uint)index + 120U));
            }
        }

        /// <summary>
        /// Sets the Actor.
        /// </summary>
        public SagaDB.Actor.Actor Actor
        {
            set
            {
                if (value.type == ActorType.PC)
                    this.SetPC = (ActorPC)value;
                else if (value.type == ActorType.PET)
                {
                    this.SetPet = (ActorPet)value;
                }
                else
                {
                    if (value.type != ActorType.SHADOW)
                        return;
                    this.SetShadow = (ActorShadow)value;
                }
            }
        }
    }
}
