namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaLib;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_INFO" />.
    /// </summary>
    public class SSMG_PLAYER_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_INFO"/> class.
        /// </summary>
        public SSMG_PLAYER_INFO()
        {
            if (Singleton<Configuration>.Instance.Version == SagaLib.Version.Saga6)
            {
                this.data = new byte[210];
                this.offset = (ushort)2;
                this.ID = (ushort)511;
            }
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga9)
            {
                this.data = new byte[219];
                this.offset = (ushort)2;
                this.ID = (ushort)511;
            }
            if (Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga10)
                return;
            this.data = new byte[222];
            this.offset = (ushort)2;
            this.ID = (ushort)511;
        }

        /// <summary>
        /// Sets the Player.
        /// </summary>
        public ActorPC Player
        {
            set
            {
                if (Singleton<Configuration>.Instance.Version == SagaLib.Version.Saga6)
                {
                    SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(value.MapID);
                    this.PutUInt(value.ActorID, (ushort)2);
                    this.PutUInt(value.CharID, (ushort)6);
                    string s = value.Name.Replace("\0", "");
                    byte[] bytes = Global.Unicode.GetBytes(s);
                    byte[] numArray = new byte[211 + bytes.Length];
                    this.data.CopyTo((Array)numArray, 0);
                    this.data = numArray;
                    ushort index1 = (ushort)(12 + bytes.Length);
                    this.PutByte((byte)(bytes.Length + 1), (ushort)10);
                    this.PutBytes(bytes, (ushort)11);
                    this.PutByte((byte)value.Race, index1);
                    this.PutByte((byte)value.Gender, (ushort)((uint)index1 + 1U));
                    this.PutByte(value.HairStyle, (ushort)((uint)index1 + 2U));
                    this.PutByte(value.HairColor, (ushort)((uint)index1 + 3U));
                    this.PutByte(value.Wig, (ushort)((uint)index1 + 4U));
                    this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 5U));
                    this.PutByte(value.Face, (ushort)((uint)index1 + 6U));
                    this.PutUInt(value.MapID, (ushort)((uint)index1 + 10U));
                    this.PutByte(Global.PosX16to8(value.X, map.Width), (ushort)((uint)index1 + 14U));
                    this.PutByte(Global.PosY16to8(value.Y, map.Height), (ushort)((uint)index1 + 15U));
                    this.PutByte((byte)((uint)value.Dir / 45U), (ushort)((uint)index1 + 16U));
                    this.PutUInt(value.HP, (ushort)((uint)index1 + 17U));
                    this.PutUInt(value.MaxHP, (ushort)((uint)index1 + 21U));
                    this.PutUInt(value.MP, (ushort)((uint)index1 + 25U));
                    this.PutUInt(value.MaxMP, (ushort)((uint)index1 + 29U));
                    this.PutUInt(value.SP, (ushort)((uint)index1 + 33U));
                    this.PutUInt(value.MaxSP, (ushort)((uint)index1 + 37U));
                    this.PutByte((byte)8, (ushort)((uint)index1 + 41U));
                    this.PutUShort(value.Str, (ushort)((uint)index1 + 42U));
                    this.PutUShort(value.Dex, (ushort)((uint)index1 + 44U));
                    this.PutUShort(value.Int, (ushort)((uint)index1 + 46U));
                    this.PutUShort(value.Vit, (ushort)((uint)index1 + 48U));
                    this.PutUShort(value.Agi, (ushort)((uint)index1 + 50U));
                    this.PutUShort(value.Mag, (ushort)((uint)index1 + 52U));
                    this.PutUShort((ushort)13, (ushort)((uint)index1 + 54U));
                    this.PutUShort((ushort)0, (ushort)((uint)index1 + 56U));
                    this.PutByte((byte)20, (ushort)((uint)index1 + 58U));
                    if (value.PossessionTarget == 0U)
                        this.PutUInt(uint.MaxValue, (ushort)((uint)index1 + 101U));
                    else if (map.GetActor(value.PossessionTarget).type != ActorType.ITEM)
                        this.PutUInt(value.PossessionTarget, (ushort)((uint)index1 + 101U));
                    else
                        this.PutUInt(value.ActorID, (ushort)((uint)index1 + 101U));
                    if (value.PossessionTarget == 0U)
                        this.PutByte(byte.MaxValue, (ushort)((uint)index1 + 105U));
                    else
                        this.PutByte((byte)value.PossessionPosition, (ushort)((uint)index1 + 105U));
                    this.PutUInt((uint)value.Gold, (ushort)((uint)index1 + 106U));
                    this.PutByte((byte)value.Status.attackType, (ushort)((uint)index1 + 110U));
                    this.PutByte((byte)13, (ushort)((uint)index1 + 111U));
                    for (int index2 = 0; index2 < 13; ++index2)
                    {
                        if (value.Inventory.Equipments.ContainsKey((EnumEquipSlot)index2))
                        {
                            SagaDB.Item.Item equipment = value.Inventory.Equipments[(EnumEquipSlot)index2];
                            if (equipment.Stack != (ushort)0)
                            {
                                if (equipment.PictID == 0U)
                                    this.PutUInt(equipment.BaseData.imageID, (ushort)((int)index1 + 112 + index2 * 4));
                                else
                                    this.PutUInt(equipment.PictID, (ushort)((int)index1 + 112 + index2 * 4));
                            }
                        }
                    }
                    this.PutByte((byte)3, (ushort)((uint)index1 + 164U));
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        SagaDB.Item.Item equipment = value.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                        this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index1 + 165U));
                        this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index1 + 166U));
                    }
                    else
                        this.PutByte((byte)0, (ushort)((uint)index1 + 165U));
                    this.PutByte((byte)3, (ushort)((uint)index1 + 168U));
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        SagaDB.Item.Item equipment = value.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                        this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index1 + 169U));
                        this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index1 + 170U));
                    }
                    else
                        this.PutByte((byte)0, (ushort)((uint)index1 + 169U));
                    this.PutByte((byte)3, (ushort)((uint)index1 + 172U));
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null && value.Pet.Ride)
                    {
                        SagaDB.Item.Item equipment = value.Inventory.Equipments[EnumEquipSlot.PET];
                        this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index1 + 173U));
                        this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index1 + 174U));
                    }
                    if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null && value.Pet.Ride)
                        this.PutUInt(value.Inventory.Equipments[EnumEquipSlot.PET].ItemID, (ushort)((uint)index1 + 176U));
                    this.PutUInt(value.Range, (ushort)((uint)index1 + 181U));
                    switch (value.Mode)
                    {
                        case PlayerMode.NORMAL:
                            this.PutInt(2, (ushort)((uint)index1 + 189U));
                            this.PutInt(0, (ushort)((uint)index1 + 193U));
                            break;
                        case PlayerMode.KNIGHT_WAR:
                            this.PutInt(34, (ushort)((uint)index1 + 189U));
                            this.PutInt(2, (ushort)((uint)index1 + 193U));
                            break;
                        case PlayerMode.COLISEUM_MODE:
                            this.PutInt(66, (ushort)((uint)index1 + 189U));
                            this.PutInt(1, (ushort)((uint)index1 + 193U));
                            break;
                        case PlayerMode.WRP:
                            this.PutInt(258, (ushort)((uint)index1 + 189U));
                            this.PutInt(4, (ushort)((uint)index1 + 193U));
                            break;
                    }
                }
                if (Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga9)
                    return;
                SagaMap.Map map1 = Singleton<MapManager>.Instance.GetMap(value.MapID);
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutUInt(value.CharID, (ushort)6);
                string s1 = value.Name.Replace("\0", "");
                byte[] bytes1 = Global.Unicode.GetBytes(s1);
                byte[] numArray1 = new byte[this.data.Length + bytes1.Length];
                this.data.CopyTo((Array)numArray1, 0);
                this.data = numArray1;
                ushort index3 = (ushort)(12 + bytes1.Length);
                this.PutByte((byte)(bytes1.Length + 1), (ushort)10);
                this.PutBytes(bytes1, (ushort)11);
                this.PutByte((byte)value.Race, index3);
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                {
                    ++index3;
                    this.PutByte((byte)value.Form, index3);
                }
                this.PutByte((byte)value.Gender, (ushort)((uint)index3 + 1U));
                this.PutByte(value.HairStyle, (ushort)((uint)index3 + 2U));
                this.PutByte(value.HairColor, (ushort)((uint)index3 + 3U));
                this.PutByte(value.Wig, (ushort)((uint)index3 + 4U));
                this.PutByte(byte.MaxValue, (ushort)((uint)index3 + 5U));
                this.PutByte(value.Face, (ushort)((uint)index3 + 6U));
                this.PutUInt(value.MapID, (ushort)((uint)index3 + 10U));
                this.PutByte(Global.PosX16to8(value.X, map1.Width), (ushort)((uint)index3 + 14U));
                this.PutByte(Global.PosY16to8(value.Y, map1.Height), (ushort)((uint)index3 + 15U));
                this.PutByte((byte)((uint)value.Dir / 45U), (ushort)((uint)index3 + 16U));
                this.PutUInt(value.HP, (ushort)((uint)index3 + 17U));
                this.PutUInt(value.MaxHP, (ushort)((uint)index3 + 21U));
                this.PutUInt(value.MP, (ushort)((uint)index3 + 25U));
                this.PutUInt(value.MaxMP, (ushort)((uint)index3 + 29U));
                this.PutUInt(value.SP, (ushort)((uint)index3 + 33U));
                this.PutUInt(value.MaxSP, (ushort)((uint)index3 + 37U));
                this.PutUInt(value.EP, (ushort)((uint)index3 + 41U));
                this.PutUInt(value.MaxEP, (ushort)((uint)index3 + 45U));
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                {
                    if (Singleton<MapManager>.Instance.GetMap(value.MapID).Info.Flag.Test(MapFlags.Dominion))
                        this.PutShort(value.DominionCL, (ushort)((uint)index3 + 49U));
                    else
                        this.PutShort(value.CL, (ushort)((uint)index3 + 49U));
                    index3 += (ushort)2;
                }
                this.PutByte((byte)8, (ushort)((uint)index3 + 49U));
                this.PutUShort(value.Str, (ushort)((uint)index3 + 50U));
                this.PutUShort(value.Dex, (ushort)((uint)index3 + 52U));
                this.PutUShort(value.Int, (ushort)((uint)index3 + 54U));
                this.PutUShort(value.Vit, (ushort)((uint)index3 + 56U));
                this.PutUShort(value.Agi, (ushort)((uint)index3 + 58U));
                this.PutUShort(value.Mag, (ushort)((uint)index3 + 60U));
                this.PutUShort((ushort)13, (ushort)((uint)index3 + 62U));
                this.PutUShort((ushort)0, (ushort)((uint)index3 + 64U));
                this.PutByte((byte)20, (ushort)((uint)index3 + 66U));
                if (value.PossessionTarget == 0U)
                    this.PutUInt(uint.MaxValue, (ushort)((uint)index3 + 109U));
                else if (map1.GetActor(value.PossessionTarget).type != ActorType.ITEM)
                    this.PutUInt(value.PossessionTarget, (ushort)((uint)index3 + 109U));
                else
                    this.PutUInt(value.ActorID, (ushort)((uint)index3 + 109U));
                if (value.PossessionTarget == 0U)
                    this.PutByte(byte.MaxValue, (ushort)((uint)index3 + 113U));
                else
                    this.PutByte((byte)value.PossessionPosition, (ushort)((uint)index3 + 113U));
                this.PutUInt((uint)value.Gold, (ushort)((uint)index3 + 114U));
                this.PutByte((byte)value.Status.attackType, (ushort)((uint)index3 + 118U));
                this.PutByte((byte)13, (ushort)((uint)index3 + 119U));
                for (int index1 = 0; index1 < 13; ++index1)
                {
                    if (value.Inventory.Equipments.ContainsKey((EnumEquipSlot)index1))
                    {
                        SagaDB.Item.Item equipment = value.Inventory.Equipments[(EnumEquipSlot)index1];
                        if (equipment.Stack != (ushort)0)
                        {
                            if (equipment.PictID == 0U)
                                this.PutUInt(equipment.BaseData.imageID, (ushort)((int)index3 + 120 + index1 * 4));
                            else
                                this.PutUInt(equipment.PictID, (ushort)((int)index3 + 120 + index1 * 4));
                        }
                    }
                }
                this.PutByte((byte)3, (ushort)((uint)index3 + 172U));
                if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                {
                    SagaDB.Item.Item equipment = value.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                    this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index3 + 173U));
                    this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index3 + 173U));
                }
                else
                    this.PutByte((byte)0, (ushort)((uint)index3 + 173U));
                this.PutByte((byte)3, (ushort)((uint)index3 + 176U));
                if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    SagaDB.Item.Item equipment = value.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                    this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index3 + 177U));
                    this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index3 + 178U));
                }
                else
                    this.PutByte((byte)0, (ushort)((uint)index3 + 177U));
                this.PutByte((byte)3, (ushort)((uint)index3 + 180U));
                if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null && value.Pet.Ride)
                {
                    SagaDB.Item.Item equipment = value.Inventory.Equipments[EnumEquipSlot.PET];
                    this.PutByte(equipment.BaseData.handMotion, (ushort)((uint)index3 + 181U));
                    this.PutByte(equipment.BaseData.handMotion2, (ushort)((uint)index3 + 182U));
                }
                if (value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && value.Pet != null && value.Pet.Ride)
                    this.PutUInt(value.Inventory.Equipments[EnumEquipSlot.PET].ItemID, (ushort)((uint)index3 + 184U));
                this.PutUInt(value.Range, (ushort)((uint)index3 + 189U));
                switch (value.Mode)
                {
                    case PlayerMode.NORMAL:
                        this.PutInt(2, (ushort)((uint)index3 + 197U));
                        this.PutInt(0, (ushort)((uint)index3 + 201U));
                        break;
                    case PlayerMode.KNIGHT_WAR:
                        this.PutInt(34, (ushort)((uint)index3 + 197U));
                        this.PutInt(2, (ushort)((uint)index3 + 201U));
                        break;
                    case PlayerMode.COLISEUM_MODE:
                        this.PutInt(66, (ushort)((uint)index3 + 197U));
                        this.PutInt(1, (ushort)((uint)index3 + 201U));
                        break;
                    case PlayerMode.WRP:
                        this.PutInt(258, (ushort)((uint)index3 + 197U));
                        this.PutInt(4, (ushort)((uint)index3 + 201U));
                        break;
                }
            }
        }
    }
}
