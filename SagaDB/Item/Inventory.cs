namespace SagaDB.Item
{
    using SagaDB.Actor;
    using SagaDB.DEMIC;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Inventory" />.
    /// </summary>
    [Serializable]
    public class Inventory
    {
        /// <summary>
        /// Defines the version.
        /// </summary>
        private static ushort version = 3;

        /// <summary>
        /// Defines the items.
        /// </summary>
        private Dictionary<ContainerType, List<SagaDB.Item.Item>> items = new Dictionary<ContainerType, List<SagaDB.Item.Item>>();

        /// <summary>
        /// Defines the equipments.
        /// </summary>
        private Dictionary<EnumEquipSlot, SagaDB.Item.Item> equipments = new Dictionary<EnumEquipSlot, SagaDB.Item.Item>();

        /// <summary>
        /// Defines the parts.
        /// </summary>
        private Dictionary<EnumEquipSlot, SagaDB.Item.Item> parts = new Dictionary<EnumEquipSlot, SagaDB.Item.Item>();

        /// <summary>
        /// Defines the ware.
        /// </summary>
        private Dictionary<WarehousePlace, List<SagaDB.Item.Item>> ware = new Dictionary<WarehousePlace, List<SagaDB.Item.Item>>();

        /// <summary>
        /// Defines the demicChips.
        /// </summary>
        private Dictionary<byte, DEMICPanel> demicChips = new Dictionary<byte, DEMICPanel>();

        /// <summary>
        /// Defines the ddemicChips.
        /// </summary>
        private Dictionary<byte, DEMICPanel> ddemicChips = new Dictionary<byte, DEMICPanel>();

        /// <summary>
        /// Defines the index.
        /// </summary>
        private uint index = 1;

        /// <summary>
        /// Defines the wareIndex.
        /// </summary>
        [NonSerialized]
        public uint wareIndex = 200000001;

        /// <summary>
        /// Defines the golemWareIndex.
        /// </summary>
        private uint golemWareIndex = 300000001;

        /// <summary>
        /// Defines the payload.
        /// </summary>
        private Dictionary<ContainerType, uint> payload = new Dictionary<ContainerType, uint>();

        /// <summary>
        /// Defines the maxPayload.
        /// </summary>
        private Dictionary<ContainerType, uint> maxPayload = new Dictionary<ContainerType, uint>();

        /// <summary>
        /// Defines the volume.
        /// </summary>
        private Dictionary<ContainerType, uint> volume = new Dictionary<ContainerType, uint>();

        /// <summary>
        /// Defines the maxVolume.
        /// </summary>
        private Dictionary<ContainerType, uint> maxVolume = new Dictionary<ContainerType, uint>();

        /// <summary>
        /// Defines the owner.
        /// </summary>
        [NonSerialized]
        private ActorPC owner;

        /// <summary>
        /// Defines the needSave.
        /// </summary>
        private bool needSave;

        /// <summary>
        /// Defines the needSaveWare.
        /// </summary>
        private bool needSaveWare;

        /// <summary>
        /// Defines the lastItem.
        /// </summary>
        private SagaDB.Item.Item lastItem;

        /// <summary>
        /// Gets the Items.
        /// </summary>
        public Dictionary<ContainerType, List<SagaDB.Item.Item>> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Gets or sets the Owner.
        /// </summary>
        public ActorPC Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        /// <summary>
        /// Gets the Payload.
        /// </summary>
        public Dictionary<ContainerType, uint> Payload
        {
            get
            {
                return this.payload;
            }
        }

        /// <summary>
        /// Gets the MaxPayload.
        /// </summary>
        public Dictionary<ContainerType, uint> MaxPayload
        {
            get
            {
                return this.maxPayload;
            }
        }

        /// <summary>
        /// Gets the Volume.
        /// </summary>
        public Dictionary<ContainerType, uint> Volume
        {
            get
            {
                return this.volume;
            }
        }

        /// <summary>
        /// Gets the MaxVolume.
        /// </summary>
        public Dictionary<ContainerType, uint> MaxVolume
        {
            get
            {
                return this.maxVolume;
            }
        }

        /// <summary>
        /// Gets or sets the WareHouse.
        /// </summary>
        public Dictionary<WarehousePlace, List<SagaDB.Item.Item>> WareHouse
        {
            get
            {
                return this.ware;
            }
            set
            {
                this.ware = value;
            }
        }

        /// <summary>
        /// Gets the DemicChips.
        /// </summary>
        public Dictionary<byte, DEMICPanel> DemicChips
        {
            get
            {
                int num = (int)this.owner.CL / 81 + 1;
                for (int index = 0; index < num; ++index)
                {
                    if (!this.demicChips.ContainsKey((byte)index))
                        this.demicChips.Add((byte)index, new DEMICPanel());
                }
                return this.demicChips;
            }
        }

        /// <summary>
        /// Gets the DominionDemicChips.
        /// </summary>
        public Dictionary<byte, DEMICPanel> DominionDemicChips
        {
            get
            {
                int num = (int)this.owner.DominionCL / 81 + 1;
                for (int index = 0; index < num; ++index)
                {
                    if (!this.ddemicChips.ContainsKey((byte)index))
                        this.ddemicChips.Add((byte)index, new DEMICPanel());
                }
                return this.ddemicChips;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsEmpty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                foreach (List<SagaDB.Item.Item> objList in this.items.Values)
                {
                    if (objList.Count > 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether NeedSave.
        /// </summary>
        public bool NeedSave
        {
            get
            {
                return this.needSave;
            }
        }

        /// <summary>
        /// Gets a value indicating whether NeedSaveWare.
        /// </summary>
        public bool NeedSaveWare
        {
            get
            {
                return this.needSave;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsWarehouseEmpty.
        /// </summary>
        public bool IsWarehouseEmpty
        {
            get
            {
                foreach (List<SagaDB.Item.Item> objList in this.ware.Values)
                {
                    if (objList.Count > 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Gets the WareTotalCount.
        /// </summary>
        public int WareTotalCount
        {
            get
            {
                int num = 0;
                foreach (List<SagaDB.Item.Item> objList in this.ware.Values)
                    num += objList.Count;
                return num;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inventory"/> class.
        /// </summary>
        /// <param name="owner">The owner<see cref="ActorPC"/>.</param>
        public Inventory(ActorPC owner)
        {
            this.owner = owner;
            this.items.Add(ContainerType.BODY, new List<SagaDB.Item.Item>());
            this.items.Add(ContainerType.LEFT_BAG, new List<SagaDB.Item.Item>());
            this.items.Add(ContainerType.RIGHT_BAG, new List<SagaDB.Item.Item>());
            this.items.Add(ContainerType.BACK_BAG, new List<SagaDB.Item.Item>());
            this.items.Add(ContainerType.GOLEMWAREHOUSE, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.Acropolis, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.FederalOfIronSouth, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.FarEast, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.IronSouth, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.KingdomOfNorthan, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.MiningCamp, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.Morg, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.Northan, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.RepublicOfFarEast, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.Tonka, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.ECOTown, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.MaimaiCamp, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.MermaidsHome, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.TowerGoesToHeaven, new List<SagaDB.Item.Item>());
            this.ware.Add(WarehousePlace.WestFord, new List<SagaDB.Item.Item>());
            this.payload.Add(ContainerType.BODY, 0U);
            this.payload.Add(ContainerType.LEFT_BAG, 0U);
            this.payload.Add(ContainerType.RIGHT_BAG, 0U);
            this.payload.Add(ContainerType.BACK_BAG, 0U);
            this.maxPayload.Add(ContainerType.BODY, 0U);
            this.maxPayload.Add(ContainerType.LEFT_BAG, 0U);
            this.maxPayload.Add(ContainerType.RIGHT_BAG, 0U);
            this.maxPayload.Add(ContainerType.BACK_BAG, 0U);
            this.volume.Add(ContainerType.BODY, 0U);
            this.volume.Add(ContainerType.LEFT_BAG, 0U);
            this.volume.Add(ContainerType.RIGHT_BAG, 0U);
            this.volume.Add(ContainerType.BACK_BAG, 0U);
            this.maxVolume.Add(ContainerType.BODY, 0U);
            this.maxVolume.Add(ContainerType.LEFT_BAG, 0U);
            this.maxVolume.Add(ContainerType.RIGHT_BAG, 0U);
            this.maxVolume.Add(ContainerType.BACK_BAG, 0U);
            this.demicChips.Add((byte)0, new DEMICPanel());
            this.demicChips.Add((byte)100, new DEMICPanel());
            this.demicChips.Add((byte)101, new DEMICPanel());
            this.ddemicChips.Add((byte)0, new DEMICPanel());
            this.ddemicChips.Add((byte)100, new DEMICPanel());
            this.ddemicChips.Add((byte)101, new DEMICPanel());
        }

        /// <summary>
        /// The CalcPayloadVolume.
        /// </summary>
        public void CalcPayloadVolume()
        {
            List<SagaDB.Item.Item> objList1 = this.items[ContainerType.BODY];
            uint num1 = 0;
            uint num2 = 0;
            foreach (SagaDB.Item.Item obj in objList1)
            {
                num1 += (uint)obj.BaseData.weight * (uint)obj.Stack;
                num2 += (uint)obj.BaseData.volume * (uint)obj.Stack;
            }
            if (this.owner.Form == DEM_FORM.NORMAL_FORM)
            {
                foreach (SagaDB.Item.Item obj in this.equipments.Values)
                {
                    num1 += (uint)obj.BaseData.weight * (uint)obj.Stack;
                    num2 += (uint)obj.BaseData.equipVolume * (uint)obj.Stack;
                }
            }
            else
            {
                foreach (SagaDB.Item.Item obj in this.parts.Values)
                {
                    num1 += (uint)obj.BaseData.weight * (uint)obj.Stack;
                    num2 += (uint)obj.BaseData.equipVolume * (uint)obj.Stack;
                }
            }
            this.payload[ContainerType.BODY] = num1;
            this.volume[ContainerType.BODY] = num2;
            for (int index1 = 3; index1 < 6; ++index1)
            {
                ContainerType index2 = (ContainerType)index1;
                List<SagaDB.Item.Item> objList2 = this.items[index2];
                uint num3 = 0;
                uint num4 = 0;
                foreach (SagaDB.Item.Item obj in objList2)
                {
                    num3 += (uint)obj.BaseData.weight * (uint)obj.Stack;
                    num4 += (uint)obj.BaseData.volume * (uint)obj.Stack;
                }
                this.payload[index2] = num3;
                this.volume[index2] = num4;
            }
        }

        /// <summary>
        /// The AddWareItem.
        /// </summary>
        /// <param name="place">仓库地点.</param>
        /// <param name="item">要添加的道具.</param>
        /// <returns>添加结果，需要注意的只有MIXED，MIXED的话，item则被改为叠加的道具，Inventory.LastItem则是多余的新道具.</returns>
        public InventoryAddResult AddWareItem(WarehousePlace place, SagaDB.Item.Item item)
        {
            try
            {
                this.needSaveWare = true;
                if (item.Stack > (ushort)0)
                    Logger.LogWarehousePut(this.owner.Name + "," + (object)this.owner.CharID, item.BaseData.name + "(" + (object)item.ItemID + ")", string.Format("WarehousePlace:{0} Count:{1}", (object)place, (object)item.Stack));
                IEnumerable<SagaDB.Item.Item> source = this.ware[place].Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it =>
               {
                   if ((int)it.ItemID == (int)item.ItemID)
                       return it.Stack < (ushort)999;
                   return false;
               }));
                if (source.Count<SagaDB.Item.Item>() != 0 && item.Stackable)
                {
                    SagaDB.Item.Item obj1 = source.First<SagaDB.Item.Item>();
                    obj1.Stack += item.Stack;
                    if (obj1.Stack <= (ushort)999)
                    {
                        item.Stack = obj1.Stack;
                        item.Slot = obj1.Slot;
                        return InventoryAddResult.STACKED;
                    }
                    ushort num = (ushort)((uint)obj1.Stack - 999U);
                    if (num > (ushort)999)
                    {
                        Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + (object)item.Stack + "), setting count to the maximal value(999)");
                        num = (ushort)999;
                    }
                    obj1.Stack = (ushort)999;
                    item.Stack = obj1.Stack;
                    item.Slot = obj1.Slot;
                    SagaDB.Item.Item obj2 = item.Clone();
                    obj2.Stack = num;
                    obj2.Slot = this.wareIndex;
                    ++this.wareIndex;
                    this.ware[place].Add(obj2);
                    this.lastItem = obj2;
                    return InventoryAddResult.MIXED;
                }
                if (item.Stack > (ushort)999)
                {
                    Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + (object)item.Stack + "), setting count to the maximal value(999)");
                    item.Stack = (ushort)999;
                }
                this.ware[place].Add(item);
                item.Slot = this.wareIndex;
                ++this.wareIndex;
                return InventoryAddResult.NEW_INDEX;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return InventoryAddResult.ERROR;
            }
        }

        /// <summary>
        /// The DeleteWareItem.
        /// </summary>
        /// <param name="place">仓库地点.</param>
        /// <param name="slot">物品Slot.</param>
        /// <param name="amount">数量.</param>
        /// <returns>删除结果.</returns>
        public InventoryDeleteResult DeleteWareItem(WarehousePlace place, uint slot, int amount)
        {
            this.needSaveWare = true;
            IEnumerable<SagaDB.Item.Item> source = this.ware[place].Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (int)it.Slot == (int)slot));
            if (source.Count<SagaDB.Item.Item>() == 0)
                return InventoryDeleteResult.ERROR;
            SagaDB.Item.Item obj = source.First<SagaDB.Item.Item>();
            if (obj.Stack > (ushort)0)
                Logger.LogWarehouseGet(this.owner.Name + "(" + (object)this.owner.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("WarehousePlace:{0} Count:{1}", (object)place, (object)obj.Stack));
            if ((int)obj.Stack > amount)
            {
                obj.Stack -= (ushort)amount;
                return InventoryDeleteResult.STACK_UPDATED;
            }
            this.ware[place].Remove(obj);
            return InventoryDeleteResult.ALL_DELETED;
        }

        /// <summary>
        /// The AddItem.
        /// </summary>
        /// <param name="container">容器.</param>
        /// <param name="item">道具.</param>
        /// <param name="newIndex">是否生成新索引.</param>
        /// <returns>添加结果，需要注意的只有MIXED，MIXED的话，item则被改为叠加的道具，Inventory.LastItem则是多余的新道具.</returns>
        public InventoryAddResult AddItem(ContainerType container, SagaDB.Item.Item item, bool newIndex)
        {
            this.needSave = true;
            switch (container)
            {
                case ContainerType.BODY:
                case ContainerType.RIGHT_BAG:
                case ContainerType.LEFT_BAG:
                case ContainerType.BACK_BAG:
                case ContainerType.GOLEMWAREHOUSE:
                    List<SagaDB.Item.Item> source1 = this.items[container];
                    IEnumerable<SagaDB.Item.Item> source2 = source1.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it =>
                   {
                       if ((int)it.ItemID == (int)item.ItemID)
                           return it.Stack < (ushort)999;
                       return false;
                   }));
                    if (source2.Count<SagaDB.Item.Item>() != 0 && item.Stackable)
                    {
                        SagaDB.Item.Item obj1 = source2.First<SagaDB.Item.Item>();
                        obj1.Stack += item.Stack;
                        if (obj1.Stack <= (ushort)999)
                        {
                            item.Stack = obj1.Stack;
                            item.Slot = obj1.Slot;
                            this.lastItem = obj1;
                            if (obj1.Identified)
                                item.identified = obj1.identified;
                            return InventoryAddResult.STACKED;
                        }
                        ushort num = (ushort)((uint)obj1.Stack - 999U);
                        if (num > (ushort)999)
                        {
                            Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + (object)item.Stack + "), setting count to the maximal value(999)");
                            num = (ushort)999;
                        }
                        obj1.Stack = (ushort)999;
                        item.Stack = obj1.Stack;
                        item.Slot = obj1.Slot;
                        if (obj1.Identified)
                            item.identified = obj1.identified;
                        SagaDB.Item.Item obj2 = item.Clone();
                        obj2.Stack = num;
                        if (container == ContainerType.GOLEMWAREHOUSE)
                        {
                            obj2.Slot = this.golemWareIndex;
                            ++this.golemWareIndex;
                        }
                        else
                        {
                            obj2.Slot = this.index;
                            ++this.index;
                        }
                        source1.Add(obj2);
                        this.lastItem = obj2;
                        return InventoryAddResult.MIXED;
                    }
                    if (item.Stack > (ushort)999)
                    {
                        Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + (object)item.Stack + "), setting count to the maximal value(999)");
                        item.Stack = (ushort)999;
                    }
                    source1.Add(item);
                    this.lastItem = item;
                    if (newIndex)
                    {
                        if (container == ContainerType.GOLEMWAREHOUSE)
                        {
                            item.Slot = this.golemWareIndex;
                            ++this.golemWareIndex;
                        }
                        else
                        {
                            item.Slot = this.index;
                            ++this.index;
                        }
                    }
                    return InventoryAddResult.NEW_INDEX;
                case ContainerType.HEAD:
                case ContainerType.HEAD_ACCE:
                case ContainerType.FACE_ACCE:
                case ContainerType.FACE:
                case ContainerType.CHEST_ACCE:
                case ContainerType.UPPER_BODY:
                case ContainerType.LOWER_BODY:
                case ContainerType.BACK:
                case ContainerType.RIGHT_HAND:
                case ContainerType.LEFT_HAND:
                case ContainerType.SHOES:
                case ContainerType.SOCKS:
                case ContainerType.PET:
                    if (this.equipments.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())))
                    {
                        if (item.BaseData.itemType != ItemType.BULLET && item.BaseData.itemType != ItemType.ARROW)
                            Logger.ShowDebug("Container:" + container.ToString() + " must be empty before adding item!", Logger.CurrentLogger);
                        else if ((int)this.equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())].ItemID == (int)item.ItemID)
                            this.equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())].Stack += item.Stack;
                        else
                            this.equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())] = item;
                    }
                    else
                    {
                        this.equipments.Add((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString()), item);
                        this.lastItem = item;
                        if (newIndex)
                        {
                            item.Slot = this.index;
                            ++this.index;
                        }
                    }
                    return InventoryAddResult.NEW_INDEX;
                case ContainerType.HEAD2:
                case ContainerType.HEAD_ACCE2:
                case ContainerType.FACE_ACCE2:
                case ContainerType.FACE2:
                case ContainerType.CHEST_ACCE2:
                case ContainerType.UPPER_BODY2:
                case ContainerType.LOWER_BODY2:
                case ContainerType.BACK2:
                case ContainerType.RIGHT_HAND2:
                case ContainerType.LEFT_HAND2:
                case ContainerType.SHOES2:
                case ContainerType.SOCKS2:
                case ContainerType.PET2:
                    string str1 = container.ToString();
                    string str2 = str1.Substring(0, str1.Length - 1);
                    if (this.parts.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2)))
                    {
                        if (item.BaseData.itemType != ItemType.BULLET && item.BaseData.itemType != ItemType.ARROW)
                            Logger.ShowDebug("Container:" + container.ToString() + " must be empty before adding item!", Logger.CurrentLogger);
                        else if ((int)this.parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2)].ItemID == (int)item.ItemID)
                            this.parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2)].Stack += item.Stack;
                        else
                            this.parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2)] = item;
                    }
                    else
                    {
                        this.parts.Add((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2), item);
                        this.lastItem = item;
                        if (newIndex)
                        {
                            item.Slot = this.index;
                            ++this.index;
                        }
                    }
                    return InventoryAddResult.NEW_INDEX;
                default:
                    throw new ArgumentException("Unsupported container!");
            }
        }

        /// <summary>
        /// The AddItem.
        /// </summary>
        /// <param name="container">容器.</param>
        /// <param name="item">道具.</param>
        /// <returns>添加结果，需要注意的只有MIXED，MIXED的话，item则被改为叠加的道具，Inventory.LastItem则是多余的新道具.</returns>
        public InventoryAddResult AddItem(ContainerType container, SagaDB.Item.Item item)
        {
            return this.AddItem(container, item, true);
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="ID">The ID<see cref="uint"/>.</param>
        /// <param name="type">The type<see cref="Inventory.SearchType"/>.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        public SagaDB.Item.Item GetItem(uint ID, Inventory.SearchType type)
        {
            for (int index = 2; index < 32; ++index)
            {
                if (index < 6 || index == 31)
                {
                    List<SagaDB.Item.Item> source1 = this.items[(ContainerType)index];
                    List<SagaDB.Item.Item> source2 = new List<SagaDB.Item.Item>();
                    switch (type)
                    {
                        case Inventory.SearchType.ITEM_ID:
                            source2 = source1.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (int)it.ItemID == (int)ID)).ToList<SagaDB.Item.Item>();
                            break;
                        case Inventory.SearchType.SLOT_ID:
                            source2 = source1.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (int)it.Slot == (int)ID)).ToList<SagaDB.Item.Item>();
                            break;
                    }
                    if (source2.Count<SagaDB.Item.Item>() != 0)
                        return source2.First<SagaDB.Item.Item>();
                }
            }
            for (int index = 0; index < 13; ++index)
            {
                if (this.equipments.ContainsKey((EnumEquipSlot)index))
                {
                    SagaDB.Item.Item equipment = this.equipments[(EnumEquipSlot)index];
                    if (type == Inventory.SearchType.SLOT_ID && (int)equipment.Slot == (int)ID || type == Inventory.SearchType.ITEM_ID && (int)equipment.ItemID == (int)ID)
                        return equipment;
                }
            }
            for (int index = 0; index < 13; ++index)
            {
                if (this.parts.ContainsKey((EnumEquipSlot)index))
                {
                    SagaDB.Item.Item part = this.parts[(EnumEquipSlot)index];
                    if (type == Inventory.SearchType.SLOT_ID && (int)part.Slot == (int)ID || type == Inventory.SearchType.ITEM_ID && (int)part.ItemID == (int)ID)
                        return part;
                }
            }
            return (SagaDB.Item.Item)null;
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="slotID">The slotID<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        public SagaDB.Item.Item GetItem(uint slotID)
        {
            return this.GetItem(slotID, Inventory.SearchType.SLOT_ID);
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="place">The place<see cref="WarehousePlace"/>.</param>
        /// <param name="slotID">The slotID<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        public SagaDB.Item.Item GetItem(WarehousePlace place, uint slotID)
        {
            IEnumerable<SagaDB.Item.Item> source = this.ware[place].Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (int)it.Slot == (int)slotID));
            if (source.Count<SagaDB.Item.Item>() == 0)
                return (SagaDB.Item.Item)null;
            return source.First<SagaDB.Item.Item>();
        }

        /// <summary>
        /// The DeleteItem.
        /// </summary>
        /// <param name="container">The container<see cref="ContainerType"/>.</param>
        /// <param name="itemID">The itemID<see cref="uint"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="InventoryDeleteResult"/>.</returns>
        public InventoryDeleteResult DeleteItem(ContainerType container, uint itemID, int count)
        {
            return this.DeleteItem(container, (int)itemID, count, Inventory.SearchType.ITEM_ID);
        }

        /// <summary>
        /// The DeleteItem.
        /// </summary>
        /// <param name="slotID">The slotID<see cref="uint"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="InventoryDeleteResult"/>.</returns>
        public InventoryDeleteResult DeleteItem(uint slotID, int count)
        {
            this.needSave = true;
            for (int index = 2; index < 32; ++index)
            {
                if (index < 6 || index == 31)
                {
                    List<SagaDB.Item.Item> source = this.items[(ContainerType)index];
                    List<SagaDB.Item.Item> objList = new List<SagaDB.Item.Item>();
                    List<SagaDB.Item.Item> list = source.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (int)it.Slot == (int)slotID)).ToList<SagaDB.Item.Item>();
                    if (list.Count<SagaDB.Item.Item>() != 0)
                    {
                        SagaDB.Item.Item obj = list.First<SagaDB.Item.Item>();
                        if ((int)obj.Stack > count)
                        {
                            obj.Stack -= (ushort)count;
                            return InventoryDeleteResult.STACK_UPDATED;
                        }
                        int stack = (int)obj.Stack;
                        source.Remove(obj);
                        return InventoryDeleteResult.ALL_DELETED;
                    }
                }
            }
            for (int index = 0; index < 13; ++index)
            {
                if (this.equipments.ContainsKey((EnumEquipSlot)index))
                {
                    SagaDB.Item.Item equipment = this.equipments[(EnumEquipSlot)index];
                    if ((int)equipment.Slot == (int)slotID)
                    {
                        if (equipment.Stack > (ushort)1)
                        {
                            --equipment.Stack;
                            return InventoryDeleteResult.STACK_UPDATED;
                        }
                        foreach (EnumEquipSlot key in equipment.EquipSlot)
                            this.equipments.Remove(key);
                        if (this.equipments.ContainsKey((EnumEquipSlot)index))
                            this.equipments.Remove((EnumEquipSlot)index);
                        return InventoryDeleteResult.ALL_DELETED;
                    }
                }
            }
            return InventoryDeleteResult.ALL_DELETED;
        }

        /// <summary>
        /// The DeleteItem.
        /// </summary>
        /// <param name="container">The container<see cref="ContainerType"/>.</param>
        /// <param name="ID">The ID<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <param name="type">The type<see cref="Inventory.SearchType"/>.</param>
        /// <returns>The <see cref="InventoryDeleteResult"/>.</returns>
        private InventoryDeleteResult DeleteItem(ContainerType container, int ID, int count, Inventory.SearchType type)
        {
            switch (container)
            {
                case ContainerType.BODY:
                case ContainerType.RIGHT_BAG:
                case ContainerType.LEFT_BAG:
                case ContainerType.BACK_BAG:
                    List<SagaDB.Item.Item> source1 = this.items[container];
                    List<SagaDB.Item.Item> source2 = new List<SagaDB.Item.Item>();
                    switch (type)
                    {
                        case Inventory.SearchType.ITEM_ID:
                            source2 = source1.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (long)it.ItemID == (long)ID)).ToList<SagaDB.Item.Item>();
                            break;
                        case Inventory.SearchType.SLOT_ID:
                            source2 = source1.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (long)it.Slot == (long)ID)).ToList<SagaDB.Item.Item>();
                            break;
                    }
                    if (source2.Count<SagaDB.Item.Item>() == 0)
                        throw new ArgumentException("No such item");
                    SagaDB.Item.Item obj = source2.First<SagaDB.Item.Item>();
                    if ((int)obj.Stack > count)
                    {
                        obj.Stack -= (ushort)count;
                        return InventoryDeleteResult.STACK_UPDATED;
                    }
                    int stack = (int)obj.Stack;
                    source1.Remove(obj);
                    return InventoryDeleteResult.ALL_DELETED;
                case ContainerType.HEAD:
                case ContainerType.HEAD_ACCE:
                case ContainerType.FACE_ACCE:
                case ContainerType.FACE:
                case ContainerType.CHEST_ACCE:
                case ContainerType.UPPER_BODY:
                case ContainerType.LOWER_BODY:
                case ContainerType.BACK:
                case ContainerType.RIGHT_HAND:
                case ContainerType.LEFT_HAND:
                case ContainerType.SHOES:
                case ContainerType.SOCKS:
                case ContainerType.PET:
                    EnumEquipSlot key1 = (EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString());
                    if (this.equipments[key1].Stack > (ushort)1)
                    {
                        --this.equipments[key1].Stack;
                        return InventoryDeleteResult.STACK_UPDATED;
                    }
                    this.equipments.Remove(key1);
                    return InventoryDeleteResult.ALL_DELETED;
                case ContainerType.HEAD2:
                case ContainerType.HEAD_ACCE2:
                case ContainerType.FACE_ACCE2:
                case ContainerType.FACE2:
                case ContainerType.CHEST_ACCE2:
                case ContainerType.UPPER_BODY2:
                case ContainerType.LOWER_BODY2:
                case ContainerType.BACK2:
                case ContainerType.RIGHT_HAND2:
                case ContainerType.LEFT_HAND2:
                case ContainerType.SHOES2:
                case ContainerType.SOCKS2:
                case ContainerType.PET2:
                    string str = container.ToString();
                    EnumEquipSlot key2 = (EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str.Substring(0, str.Length - 1));
                    if (this.parts[key2].Stack > (ushort)1)
                    {
                        --this.parts[key2].Stack;
                        return InventoryDeleteResult.STACK_UPDATED;
                    }
                    this.parts.Remove(key2);
                    return InventoryDeleteResult.ALL_DELETED;
                default:
                    return InventoryDeleteResult.ALL_DELETED;
            }
        }

        /// <summary>
        /// The MoveItem.
        /// </summary>
        /// <param name="src">The src<see cref="ContainerType"/>.</param>
        /// <param name="itemID">The itemID<see cref="uint"/>.</param>
        /// <param name="dst">The dst<see cref="ContainerType"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool MoveItem(ContainerType src, uint itemID, ContainerType dst, int count)
        {
            return this.MoveItem(src, (int)itemID, dst, count, Inventory.SearchType.ITEM_ID);
        }

        /// <summary>
        /// The MoveItem.
        /// </summary>
        /// <param name="src">The src<see cref="ContainerType"/>.</param>
        /// <param name="slotID">The slotID<see cref="int"/>.</param>
        /// <param name="dst">The dst<see cref="ContainerType"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool MoveItem(ContainerType src, int slotID, ContainerType dst, int count)
        {
            return this.MoveItem(src, slotID, dst, count, Inventory.SearchType.SLOT_ID);
        }

        /// <summary>
        /// The MoveItem.
        /// </summary>
        /// <param name="src">The src<see cref="ContainerType"/>.</param>
        /// <param name="ID">The ID<see cref="int"/>.</param>
        /// <param name="dst">The dst<see cref="ContainerType"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <param name="type">The type<see cref="Inventory.SearchType"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool MoveItem(ContainerType src, int ID, ContainerType dst, int count, Inventory.SearchType type)
        {
            try
            {
                if (src == dst)
                {
                    Logger.ShowDebug("Source container is equal to Destination container! Transfer aborted!", Logger.CurrentLogger);
                    return false;
                }
                List<SagaDB.Item.Item> source1;
                switch (src)
                {
                    case ContainerType.BODY:
                    case ContainerType.RIGHT_BAG:
                    case ContainerType.LEFT_BAG:
                    case ContainerType.BACK_BAG:
                        source1 = this.items[src];
                        break;
                    case ContainerType.HEAD:
                    case ContainerType.HEAD_ACCE:
                    case ContainerType.FACE_ACCE:
                    case ContainerType.FACE:
                    case ContainerType.CHEST_ACCE:
                    case ContainerType.UPPER_BODY:
                    case ContainerType.LOWER_BODY:
                    case ContainerType.BACK:
                    case ContainerType.RIGHT_HAND:
                    case ContainerType.LEFT_HAND:
                    case ContainerType.SHOES:
                    case ContainerType.SOCKS:
                    case ContainerType.PET:
                        source1 = new List<SagaDB.Item.Item>();
                        source1.Add(this.equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), src.ToString())]);
                        this.equipments.Remove((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), src.ToString()));
                        break;
                    case ContainerType.HEAD2:
                    case ContainerType.HEAD_ACCE2:
                    case ContainerType.FACE_ACCE2:
                    case ContainerType.FACE2:
                    case ContainerType.CHEST_ACCE2:
                    case ContainerType.UPPER_BODY2:
                    case ContainerType.LOWER_BODY2:
                    case ContainerType.BACK2:
                    case ContainerType.RIGHT_HAND2:
                    case ContainerType.LEFT_HAND2:
                    case ContainerType.SHOES2:
                    case ContainerType.SOCKS2:
                    case ContainerType.PET2:
                        string str1 = src.ToString();
                        string str2 = str1.Substring(0, str1.Length - 1);
                        source1 = new List<SagaDB.Item.Item>();
                        source1.Add(this.parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2)]);
                        this.parts.Remove((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2));
                        break;
                    default:
                        throw new ArgumentException("Unsupported Source Container!");
                }
                List<SagaDB.Item.Item> source2 = new List<SagaDB.Item.Item>();
                switch (type)
                {
                    case Inventory.SearchType.ITEM_ID:
                        source2 = source1.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (long)it.ItemID == (long)ID)).ToList<SagaDB.Item.Item>();
                        break;
                    case Inventory.SearchType.SLOT_ID:
                        source2 = source1.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (long)it.Slot == (long)ID)).ToList<SagaDB.Item.Item>();
                        break;
                }
                if (source2.Count == 0)
                    throw new ArgumentException("The source container doesn't contain such item");
                SagaDB.Item.Item obj1 = source2.First<SagaDB.Item.Item>();
                SagaDB.Item.Item obj2 = obj1.Clone();
                if (count > (int)obj1.Stack || count == 0)
                    count = (int)obj1.Stack;
                obj2.Stack = (ushort)count;
                obj1.Stack -= (ushort)count;
                if (obj1.Stack == (ushort)0)
                {
                    source1.Remove(obj1);
                    obj2.Slot = obj1.Slot;
                    int num = (int)this.AddItem(dst, obj2, false);
                    obj1.Slot = obj2.Slot;
                }
                else
                {
                    int num1 = (int)this.AddItem(dst, obj2, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
            }
        }

        /// <summary>
        /// The GetContainer.
        /// </summary>
        /// <param name="container">The container<see cref="ContainerType"/>.</param>
        /// <returns>The <see cref="List{SagaDB.Item.Item}"/>.</returns>
        public List<SagaDB.Item.Item> GetContainer(ContainerType container)
        {
            switch (container)
            {
                case ContainerType.BODY:
                case ContainerType.RIGHT_BAG:
                case ContainerType.LEFT_BAG:
                case ContainerType.BACK_BAG:
                case ContainerType.GOLEMWAREHOUSE:
                    return this.items[container];
                case ContainerType.HEAD:
                case ContainerType.HEAD_ACCE:
                case ContainerType.FACE_ACCE:
                case ContainerType.FACE:
                case ContainerType.CHEST_ACCE:
                case ContainerType.UPPER_BODY:
                case ContainerType.LOWER_BODY:
                case ContainerType.BACK:
                case ContainerType.RIGHT_HAND:
                case ContainerType.LEFT_HAND:
                case ContainerType.SHOES:
                case ContainerType.SOCKS:
                case ContainerType.PET:
                    List<SagaDB.Item.Item> objList1 = new List<SagaDB.Item.Item>();
                    if (this.equipments.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())))
                    {
                        SagaDB.Item.Item equipment = this.equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())];
                        objList1.Add(equipment);
                    }
                    return objList1;
                case ContainerType.HEAD2:
                case ContainerType.HEAD_ACCE2:
                case ContainerType.FACE_ACCE2:
                case ContainerType.FACE2:
                case ContainerType.CHEST_ACCE2:
                case ContainerType.UPPER_BODY2:
                case ContainerType.LOWER_BODY2:
                case ContainerType.BACK2:
                case ContainerType.RIGHT_HAND2:
                case ContainerType.LEFT_HAND2:
                case ContainerType.SHOES2:
                case ContainerType.SOCKS2:
                case ContainerType.PET2:
                    List<SagaDB.Item.Item> objList2 = new List<SagaDB.Item.Item>();
                    string str1 = container.ToString();
                    string str2 = str1.Substring(0, str1.Length - 1);
                    if (this.parts.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2)))
                    {
                        SagaDB.Item.Item part = this.parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), str2)];
                        objList2.Add(part);
                    }
                    return objList2;
                default:
                    return new List<SagaDB.Item.Item>();
            }
        }

        /// <summary>
        /// Gets the Equipments.
        /// </summary>
        public Dictionary<EnumEquipSlot, SagaDB.Item.Item> Equipments
        {
            get
            {
                return this.equipments;
            }
        }

        /// <summary>
        /// Gets the Parts.
        /// </summary>
        public Dictionary<EnumEquipSlot, SagaDB.Item.Item> Parts
        {
            get
            {
                return this.parts;
            }
        }

        /// <summary>
        /// The GetContainerType.
        /// </summary>
        /// <param name="slotID">The slotID<see cref="uint"/>.</param>
        /// <returns>The <see cref="ContainerType"/>.</returns>
        public ContainerType GetContainerType(uint slotID)
        {
            for (int index = 2; index < 6; ++index)
            {
                List<SagaDB.Item.Item> source = this.items[(ContainerType)index];
                List<SagaDB.Item.Item> objList = new List<SagaDB.Item.Item>();
                if (source.Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (int)it.Slot == (int)slotID)).ToList<SagaDB.Item.Item>().Count<SagaDB.Item.Item>() != 0)
                    return (ContainerType)index;
            }
            for (int index = 0; index < 13; ++index)
            {
                if (this.equipments.ContainsKey((EnumEquipSlot)index) && (int)this.equipments[(EnumEquipSlot)index].Slot == (int)slotID)
                    return (ContainerType)Enum.Parse(typeof(ContainerType), ((EnumEquipSlot)index).ToString());
            }
            for (int index = 0; index < 13; ++index)
            {
                if (this.parts.ContainsKey((EnumEquipSlot)index) && (int)this.parts[(EnumEquipSlot)index].Slot == (int)slotID)
                    return (ContainerType)Enum.Parse(typeof(ContainerType), ((EnumEquipSlot)index).ToString()) + 200;
            }
            return ContainerType.OTHER_WAREHOUSE;
        }

        /// <summary>
        /// Gets the LastItem.
        /// </summary>
        public SagaDB.Item.Item LastItem
        {
            get
            {
                return this.lastItem;
            }
        }

        /// <summary>
        /// The IsContainerEquip.
        /// </summary>
        /// <param name="type">The type<see cref="ContainerType"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsContainerEquip(ContainerType type)
        {
            return type >= ContainerType.HEAD && type <= ContainerType.PET;
        }

        /// <summary>
        /// The IsContainerParts.
        /// </summary>
        /// <param name="type">The type<see cref="ContainerType"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsContainerParts(ContainerType type)
        {
            return type >= ContainerType.HEAD2 && type <= ContainerType.PET2;
        }

        /// <summary>
        /// The panelCount.
        /// </summary>
        /// <param name="page">The page<see cref="byte"/>.</param>
        /// <param name="dominion">The dominion<see cref="bool"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int panelCount(byte page, bool dominion)
        {
            int num1 = !dominion ? (int)this.owner.CL : (int)this.owner.DominionCL;
            if (num1 <= (int)page * 81)
                return 0;
            int num2 = num1 - (int)page * 81;
            if (num2 > 81)
                return 81;
            return num2;
        }

        /// <summary>
        /// The validTable.
        /// </summary>
        /// <param name="page">The page<see cref="byte"/>.</param>
        /// <returns>The <see cref="bool[,]"/>.</returns>
        public bool[,] validTable(byte page)
        {
            return this.validTable(page, this.owner.InDominionWorld);
        }

        /// <summary>
        /// The validTable.
        /// </summary>
        /// <param name="page">The page<see cref="byte"/>.</param>
        /// <param name="dominion">The dominion<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool[,]"/>.</returns>
        public bool[,] validTable(byte page, bool dominion)
        {
            int num1 = this.panelCount(page, dominion);
            bool[,] flagArray;
            int num2;
            int index1;
            int index2;
            int num3;
            int num4;
            if (page == (byte)0)
            {
                flagArray = new bool[9, 9]
                {
          {
            true,
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            true,
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            true,
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
          },
          {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false
          }
                };
                num2 = 9;
                index1 = 3;
                index2 = 0;
                num3 = 3;
                num4 = 3;
            }
            else
            {
                flagArray = new bool[9, 9];
                num2 = 0;
                index1 = 0;
                index2 = 0;
                num3 = 0;
                num4 = 0;
            }
            for (int index3 = num2; index3 < num1; ++index3)
            {
                flagArray[index1, index2] = true;
                if (index2 < num4)
                    ++index2;
                if (index2 == num4 && index1 >= num3)
                {
                    ++num3;
                    if (num4 == 0)
                        ++num4;
                    if (index1 > 0)
                        index1 = num3 - 1;
                    else
                        ++index1;
                }
                else
                {
                    if (index1 >= 0 && index2 >= num4)
                        --index1;
                    if (index1 == -1 && index2 >= num4)
                    {
                        index1 = num3;
                        ++num4;
                        if (index2 > 0)
                            index2 = 0;
                        else
                            ++index2;
                    }
                }
            }
            return flagArray;
        }

        /// <summary>
        /// The GetChipList.
        /// </summary>
        /// <param name="page">The page<see cref="byte"/>.</param>
        /// <returns>The <see cref="short[,]"/>.</returns>
        public short[,] GetChipList(byte page)
        {
            return this.GetChipList(page, this.owner.InDominionWorld);
        }

        /// <summary>
        /// The GetChipList.
        /// </summary>
        /// <param name="page">The page<see cref="byte"/>.</param>
        /// <param name="dominion">The dominion<see cref="bool"/>.</param>
        /// <returns>The <see cref="short[,]"/>.</returns>
        public short[,] GetChipList(byte page, bool dominion)
        {
            Dictionary<byte, DEMICPanel> dictionary = !dominion ? this.demicChips : this.ddemicChips;
            short[,] numArray = new short[9, 9];
            if (dictionary.ContainsKey(page))
            {
                if (dictionary[page].EngageTask1 != byte.MaxValue)
                {
                    int index1 = (int)dictionary[page].EngageTask1 % 9;
                    int index2 = (int)dictionary[page].EngageTask1 / 9;
                    numArray[index1, index2] = (short)10000;
                }
                if (dictionary[page].EngageTask2 != byte.MaxValue)
                {
                    int index1 = (int)dictionary[page].EngageTask2 % 9;
                    int index2 = (int)dictionary[page].EngageTask2 / 9;
                    numArray[index1, index2] = (short)10000;
                }
                foreach (Chip chip in dictionary[page].Chips)
                    numArray[(int)chip.X, (int)chip.Y] = chip.ChipID;
            }
            return numArray;
        }

        /// <summary>
        /// The CountChip.
        /// </summary>
        /// <param name="chipID">The chipID<see cref="short"/>.</param>
        /// <param name="dominion">The dominion<see cref="bool"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int CountChip(short chipID, bool dominion)
        {
            DEMICPanel[] demicPanelArray = !dominion ? this.demicChips.Values.ToArray<DEMICPanel>() : this.ddemicChips.Values.ToArray<DEMICPanel>();
            int num = 0;
            foreach (DEMICPanel demicPanel in demicPanelArray)
            {
                foreach (Chip chip in demicPanel.Chips)
                {
                    if ((int)chip.ChipID == (int)chipID)
                        ++num;
                }
            }
            return num;
        }

        /// <summary>
        /// The InsertChip.
        /// </summary>
        /// <param name="page">DEMIC页.</param>
        /// <param name="chip">芯片.</param>
        /// <returns>是否成功.</returns>
        public bool InsertChip(byte page, Chip chip)
        {
            return this.InsertChip(page, chip, this.owner.InDominionWorld);
        }

        /// <summary>
        /// The InsertChip.
        /// </summary>
        /// <param name="page">DEMIC页.</param>
        /// <param name="chip">芯片.</param>
        /// <param name="dominion">是否在恶魔界.</param>
        /// <returns>是否成功.</returns>
        public bool InsertChip(byte page, Chip chip, bool dominion)
        {
            return this.InsertChip(page, chip, this.validTable(page, dominion), dominion);
        }

        /// <summary>
        /// The InsertChip.
        /// </summary>
        /// <param name="page">DEMIC页.</param>
        /// <param name="chip">芯片.</param>
        /// <param name="table">ＤＥＭＩＣ有效表.</param>
        /// <param name="dominion">是否在恶魔界.</param>
        /// <returns>是否成功.</returns>
        public bool InsertChip(byte page, Chip chip, bool[,] table, bool dominion)
        {
            bool flag = false;
            int num1 = this.CountChip(chip.ChipID, dominion);
            if (num1 >= 10 || chip.Data.type == (byte)30 && num1 >= 1)
                return false;
            Dictionary<byte, DEMICPanel> dictionary = !dominion ? this.demicChips : this.ddemicChips;
            if (!dictionary.ContainsKey(page))
                return false;
            byte num2 = byte.MaxValue;
            byte num3 = byte.MaxValue;
            byte num4 = byte.MaxValue;
            byte num5 = byte.MaxValue;
            if (dictionary[page].EngageTask1 != byte.MaxValue)
            {
                num2 = (byte)((uint)dictionary[page].EngageTask1 % 9U);
                num3 = (byte)((uint)dictionary[page].EngageTask1 / 9U);
            }
            if (dictionary[page].EngageTask2 != byte.MaxValue)
            {
                num4 = (byte)((uint)dictionary[page].EngageTask2 % 9U);
                num5 = (byte)((uint)dictionary[page].EngageTask2 / 9U);
            }
            foreach (Chip chip1 in dictionary[page].Chips)
            {
                foreach (byte[] cell1 in chip.Model.Cells)
                {
                    int index1 = (int)chip.X + (int)cell1[0] - (int)chip.Model.CenterX;
                    int index2 = (int)chip.Y + (int)cell1[1] - (int)chip.Model.CenterY;
                    if (!flag && ((num2 != byte.MaxValue || num3 != byte.MaxValue) && (index1 == (int)num2 && index2 == (int)num3) || (num4 != byte.MaxValue || num5 != byte.MaxValue) && (index1 == (int)num4 && index2 == (int)num5) || !table[index1, index2]))
                        return false;
                    foreach (byte[] cell2 in chip1.Model.Cells)
                    {
                        int num6 = (int)chip1.X + (int)cell2[0] - (int)chip1.Model.CenterX;
                        int num7 = (int)chip1.Y + (int)cell2[1] - (int)chip1.Model.CenterY;
                        if (num6 == index1 && num7 == index2)
                            return false;
                    }
                }
                flag = true;
            }
            dictionary[page].Chips.Add(chip);
            return true;
        }

        /// <summary>
        /// The countPossessionItem.
        /// </summary>
        /// <param name="items">The items<see cref="List{SagaDB.Item.Item}"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int countPossessionItem(List<SagaDB.Item.Item> items)
        {
            int num = 0;
            foreach (SagaDB.Item.Item obj in items)
            {
                if (obj.PossessionOwner != null && (int)obj.PossessionOwner.CharID != (int)this.owner.CharID)
                    ++num;
            }
            return num;
        }

        /// <summary>
        /// The ToBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] ToBytes()
        {
            string[] names = Enum.GetNames(typeof(ContainerType));
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter((Stream)memoryStream);
            binaryWriter.Write(Inventory.version);
            binaryWriter.Write(names.Length);
            foreach (string str in names)
            {
                ContainerType container1 = (ContainerType)Enum.Parse(typeof(ContainerType), str);
                List<SagaDB.Item.Item> container2 = this.GetContainer(container1);
                binaryWriter.Write((int)container1);
                binaryWriter.Write(container2.Count - this.countPossessionItem(container2));
                foreach (SagaDB.Item.Item obj in container2)
                {
                    if (obj.PossessionOwner == null || (int)obj.PossessionOwner.CharID == (int)this.owner.CharID)
                        obj.ToStream((Stream)memoryStream);
                }
            }
            binaryWriter.Write((byte)this.demicChips.Count);
            foreach (byte key in this.demicChips.Keys)
            {
                binaryWriter.Write(key);
                binaryWriter.Write(this.demicChips[key].EngageTask1);
                binaryWriter.Write(this.demicChips[key].EngageTask2);
                binaryWriter.Write((byte)this.demicChips[key].Chips.Count);
                foreach (Chip chip in this.demicChips[key].Chips)
                {
                    binaryWriter.Write(chip.ChipID);
                    binaryWriter.Write(chip.X);
                    binaryWriter.Write(chip.Y);
                }
            }
            binaryWriter.Write((byte)this.ddemicChips.Count);
            foreach (byte key in this.ddemicChips.Keys)
            {
                binaryWriter.Write(key);
                binaryWriter.Write(this.ddemicChips[key].EngageTask1);
                binaryWriter.Write(this.ddemicChips[key].EngageTask2);
                binaryWriter.Write((byte)this.ddemicChips[key].Chips.Count);
                foreach (Chip chip in this.ddemicChips[key].Chips)
                {
                    binaryWriter.Write(chip.ChipID);
                    binaryWriter.Write(chip.X);
                    binaryWriter.Write(chip.Y);
                }
            }
            memoryStream.Flush();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// The FromStream.
        /// </summary>
        /// <param name="ms">The ms<see cref="Stream"/>.</param>
        public void FromStream(Stream ms)
        {
            try
            {
                BinaryReader binaryReader = new BinaryReader(ms);
                ushort num1 = binaryReader.ReadUInt16();
                if (num1 >= (ushort)1)
                {
                    int num2 = binaryReader.ReadInt32();
                    for (int index1 = 0; index1 < num2; ++index1)
                    {
                        ContainerType container = (ContainerType)binaryReader.ReadInt32();
                        int num3 = binaryReader.ReadInt32();
                        for (int index2 = 0; index2 < num3; ++index2)
                        {
                            SagaDB.Item.Item obj = new SagaDB.Item.Item();
                            obj.FromStream(ms);
                            if (obj.RentalTime > DateTime.Now || !obj.Rental)
                            {
                                int num4 = (int)this.AddItem(container, obj);
                            }
                        }
                    }
                }
                if (num1 < (ushort)2)
                    return;
                this.demicChips.Clear();
                this.ddemicChips.Clear();
                byte num5 = binaryReader.ReadByte();
                for (int index1 = 0; index1 < (int)num5; ++index1)
                {
                    byte num2 = binaryReader.ReadByte();
                    DEMICPanel demicPanel = new DEMICPanel();
                    if (num1 >= (ushort)3)
                    {
                        demicPanel.EngageTask1 = binaryReader.ReadByte();
                        demicPanel.EngageTask2 = binaryReader.ReadByte();
                    }
                    byte num3 = binaryReader.ReadByte();
                    bool[,] table = this.validTable(num2, false);
                    this.demicChips.Add(num2, demicPanel);
                    for (int index2 = 0; index2 < (int)num3; ++index2)
                    {
                        short key = binaryReader.ReadInt16();
                        byte num4 = binaryReader.ReadByte();
                        byte num6 = binaryReader.ReadByte();
                        if (Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID.ContainsKey(key))
                        {
                            if (!this.InsertChip(num2, new Chip(Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID[key])
                            {
                                X = num4,
                                Y = num6
                            }, table, false))
                                Logger.ShowWarning(string.Format("Cannot insert chip:{0} for character:{1}, droped!!!", (object)key, (object)this.owner.Name));
                        }
                    }
                }
                byte num7 = binaryReader.ReadByte();
                for (int index1 = 0; index1 < (int)num7; ++index1)
                {
                    byte num2 = binaryReader.ReadByte();
                    DEMICPanel demicPanel = new DEMICPanel();
                    if (num1 >= (ushort)3)
                    {
                        demicPanel.EngageTask1 = binaryReader.ReadByte();
                        demicPanel.EngageTask2 = binaryReader.ReadByte();
                    }
                    bool[,] table = this.validTable(num2, true);
                    byte num3 = binaryReader.ReadByte();
                    this.ddemicChips.Add(num2, demicPanel);
                    for (int index2 = 0; index2 < (int)num3; ++index2)
                    {
                        short key = binaryReader.ReadInt16();
                        byte num4 = binaryReader.ReadByte();
                        byte num6 = binaryReader.ReadByte();
                        if (Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID.ContainsKey(key))
                        {
                            if (!this.InsertChip(num2, new Chip(Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID[key])
                            {
                                X = num4,
                                Y = num6
                            }, table, true))
                                Logger.ShowWarning(string.Format("Cannot insert chip:{0} for character:{1}, droped!!!", (object)key, (object)this.owner.Name));
                        }
                    }
                }
                if (!this.demicChips.ContainsKey((byte)100))
                    this.demicChips.Add((byte)100, new DEMICPanel());
                if (this.demicChips.ContainsKey((byte)101))
                    return;
                this.demicChips.Add((byte)101, new DEMICPanel());
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The WareToBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] WareToBytes()
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter((Stream)memoryStream);
            binaryWriter.Write(Inventory.version);
            binaryWriter.Write(this.ware.Count);
            foreach (WarehousePlace key in this.ware.Keys)
            {
                List<SagaDB.Item.Item> objList = this.ware[key];
                binaryWriter.Write((byte)key);
                binaryWriter.Write((ushort)objList.Count);
                foreach (SagaDB.Item.Item obj in objList)
                    obj.ToStream((Stream)memoryStream);
            }
            memoryStream.Flush();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// The WareFromSteam.
        /// </summary>
        /// <param name="ms">The ms<see cref="Stream"/>.</param>
        public void WareFromSteam(Stream ms)
        {
            try
            {
                BinaryReader binaryReader = new BinaryReader(ms);
                if (binaryReader.ReadUInt16() < (ushort)1)
                    return;
                int num1 = binaryReader.ReadInt32();
                for (int index1 = 0; index1 < num1; ++index1)
                {
                    WarehousePlace place = (WarehousePlace)binaryReader.ReadByte();
                    ushort num2 = binaryReader.ReadUInt16();
                    for (int index2 = 0; index2 < (int)num2; ++index2)
                    {
                        SagaDB.Item.Item obj = new SagaDB.Item.Item();
                        obj.FromStream(ms);
                        int num3 = (int)this.AddWareItem(place, obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// Defines the SearchType.
        /// </summary>
        public enum SearchType
        {
            /// <summary>
            /// Defines the ITEM_ID.
            /// </summary>
            ITEM_ID,

            /// <summary>
            /// Defines the SLOT_ID.
            /// </summary>
            SLOT_ID,
        }
    }
}
